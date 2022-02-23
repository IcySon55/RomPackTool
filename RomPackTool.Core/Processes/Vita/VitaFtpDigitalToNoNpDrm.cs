using FluentFTP;
using RomPackTool.Core.Processing;
using RomPackTool.Core.RMPK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// Downloads a Vita Digital Game as a NoNpDrm RomPack archive.
    /// </summary>
    public class VitaFtpDigitalToNoNpDrm : VitaFtpProcess
    {
        /// <summary>
        /// 
        /// </summary>
        private VitaAppEntry _vitaApp { get; }

        /// <summary>
        /// Instantiates a new <see cref="VitaFtpDigitalToNoNpDrm"/> process.
        /// </summary>
        /// <param name="ipAddress">Vita IP address.</param>
        /// <param name="outputPath">Path to create the dump in.</param>
        public VitaFtpDigitalToNoNpDrm(VitaFtpOptions options, VitaAppEntry vitaApp) : base(options)
        {
            Name = "Vita Digital to NoNpDrm";
            _vitaApp = vitaApp;
        }

        /// <summary>
        /// Executes the process.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public override async Task<bool> Run(CancellationToken token)
        {
            // Update state.
            State = ProcessState.Running;

            try
            {
                // Parameter validation.
                var valid = await Validate(token);
                if (!valid) return false;

                Report($"Connecting to your Vita at {_ipAddress}:1337...");
                await Task.Delay(1, token);

                // Create the FTP client.
                var client = new FtpClient($"ftp://{_ipAddress}", 1337, new NetworkCredential(string.Empty, string.Empty))
                {
                    DataConnectionType = FtpDataConnectionType.AutoActive
                };
                await client.ConnectAsync(token);

                // Connected?
                if (client.IsAuthenticated)
                {
                    Report("Connected!");
                    await Task.Delay(1, token);
                }

                // Make sure the target title exists
                valid = await client.DirectoryExistsAsync($"/ux0:/app/{_vitaApp.TitleId}", token);
                if (!valid)
                {
                    Report($"\"{_vitaApp.Name}\" could not be found on ux0.");
                    State = ProcessState.Error;
                    return false;
                }

                Report("Validating environment...");
                await Task.Delay(1, token);

                Report($"Found {_vitaApp.TitleId} on the memory card...");
                await Task.Delay(1, token);

                // Set the path to the app.
                var titlePath = $"/ux0:/app/{_vitaApp.TitleId}";

                // Check if the NoNpDrm license has been created.
                var licensePath = $"/ux0:/nonpdrm/license/app/{_vitaApp.TitleId}";
                var licenseFileName = "6488b73b912a753a492e2714e9b38bc7.rif";
                var licenseFilePath = $"{licensePath}/{licenseFileName}";

                if (!await client.DirectoryExistsAsync(licensePath, token))
                {
                    Report($"Fake license not created. Please run the game to generate the license.");
                    State = ProcessState.Error;
                    return false;
                }

                // Moved to the license directory.
                await client.SetWorkingDirectoryAsync(licensePath, token);
                var result = await client.GetListingAsync(token);

                if (result.FirstOrDefault()?.Name != licenseFileName)
                {
                    Report($"Fake license not created. Please run the game to generate the license.");
                    State = ProcessState.Error;
                    return false;
                }

                // Set up the directories
                Directory.CreateDirectory(_outputPath);

                // Build a list of the files to dump.
                Report($"Getting list of files to transfer...");
                await Task.Delay(1, token);

                // Find all the files to be downloaded.
                var files = await ProcessDirectoryAsync(client, titlePath, token);

                // Build the RMP
                var rmp = new RomPack();
                rmp.Header.ContentDescriptor = "NoNpDrmDigital";

                // Migrate the incoming FTP files.
                rmp.Files = files.ToList<FileEntry>();

                // Add the license file to the list.
                rmp.Files.Add(new FtpFileEntry
                {
                    FtpPath = licenseFilePath,
                    Path = $"/{_vitaApp.TitleId}/sce_sys/package/work.bin",
                    Size = result.First().Size
                });

                var fileCount = rmp.Files.Count;
                Report($"Found {fileCount} file(s)!");
                await Task.Delay(1, token);

                // Save the file...
                Report(0, fileCount);
                Report($"Transferring...");
                await Task.Delay(1, token);

                var noNpDrmPath = Path.Combine(_outputPath, "NoNpDrm", _vitaApp.TitleId + ".psn.nonpdrm");
                var saveResult = await rmp.FtpSave(File.Create(noNpDrmPath), client, Progress, token);

                if (saveResult != ProcessState.Completed)
                {
                    State = saveResult;
                    return false;
                }

                Report($"Transfer complete!");
                await Task.Delay(1, token);

                Report($"Wrote file to {noNpDrmPath}");
                await Task.Delay(1, token);

                // Disconnect from the Vita, dumping is complete.
                await client.DisconnectAsync(token);

                State = ProcessState.Completed;
                return true;
            }
            catch (TaskCanceledException)
            {
                State = ProcessState.Cancelled;
                return false;
            }
            catch (Exception ex)
            {
                State = ProcessState.Error;
                Report(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<IList<FtpFileEntry>> ProcessDirectoryAsync(FtpClient client, string path, CancellationToken token)
        {
            // Create our list of files
            var files = new List<FtpFileEntry>();

            // Return an empty list if the operation is cancelled.
            if (token.IsCancellationRequested)
                return files;

            // Move to the directory
            await client.SetWorkingDirectoryAsync(path, token);

            // List the items
            var items = await client.GetListingAsync(token);

            // Iterate through them and create the necessary objects.
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case FtpFileSystemObjectType.File:
                        files.Add(new FtpFileEntry
                        {
                            FtpPath = item.FullName,
                            Path = item.FullName.Replace("/ux0:/app", string.Empty),
                            Size = item.Size
                        });
                        ReportFile(item.Name, fileSize: item.Size);
                        await Task.Delay(1, token);
                        break;
                    case FtpFileSystemObjectType.Directory:
                        var moreFiles = await ProcessDirectoryAsync(client, item.FullName, token);
                        files.AddRange(moreFiles);
                        break;
                    case FtpFileSystemObjectType.Link:
                        break;
                }
            }

            // Return our file list.
            return files;
        }
    }
}
