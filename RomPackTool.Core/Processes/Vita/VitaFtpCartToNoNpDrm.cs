using FluentFTP;
using RomPackTool.Core.Processing;
using RomPackTool.Core.RMPK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// Downloads a Vita Cartridge as a NoNpDrm RomPack archive.
    /// </summary>
    public class VitaFtpCartToNoNpDrm : Process
    {
        /// <summary>
        /// 
        /// </summary>
        private string _ipAddress { get; }

        /// <summary>
        /// 
        /// </summary>
        private string _outputPath { get; }

        /// <summary>
        /// Instantiates a new <see cref="VitaFtpCartToNoNpDrm"/> process.
        /// </summary>
        /// <param name="ipAddress">Vita IP address.</param>
        /// <param name="outputPath">Path to create the dump in.</param>
        public VitaFtpCartToNoNpDrm(string ipAddress, string outputPath)
        {
            Name = "Vita Cart to NoNpDrm";
            ExclusivityGroup = $"FTP_{ipAddress}"; // Only one FTP operation at a time (on the same IP)
            _ipAddress = ipAddress;
            _outputPath = outputPath;
        }

        /// <summary>
        /// Validates the input variables received by the process.
        /// </summary>
        /// <returns></returns>
        protected override async Task<bool> Validate(CancellationToken token)
        {
            try
            {
                // IP Address
                if (!Regex.IsMatch(_ipAddress, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"))
                {
                    Report($"The IP address \"{_ipAddress}\" is malformed.");
                    State = ProcessState.Error;
                    return false;
                }

                // Output Path
                if (!Directory.Exists(_outputPath))
                {
                    Report("Output directory doesn't exist. Attempting to create it...");
                    Directory.CreateDirectory(_outputPath);
                }
            }
            catch (Exception ex)
            {
                Report(ex.Message);
                State = ProcessState.Error;
                return false;
            }

            return true;
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

                // Make sure a cart is inserted.
                valid = await client.DirectoryExistsAsync("/gro0:", token);
                if (!valid) //client.DirectoryExists("/gro0:"))
                {
                    Report("A game cart is not inserted.");
                    State = ProcessState.Error;
                    return false;
                }

                Report("Connected!");
                await Task.Delay(1, token);

                Report("Validating environment...");
                await Task.Delay(1, token);

                // Move to the app directory.
                await client.SetWorkingDirectoryAsync("/gro0:/app", token);

                // Get the TitleID.
                var titleID = client.GetListing().FirstOrDefault()?.Name;

                if (titleID == null)
                {
                    Report("A titleID could not be found on the game cart.");
                    State = ProcessState.Error;
                    return false;
                }

                Report($"Found {titleID} in the cart slot...");
                await Task.Delay(1, token);

                // Set the path to the app.
                var titlePath = $"/gro0:/app/{titleID}";

                // Check if the NoNpDrm license has been created.
                var licensePath = $"/ux0:/nonpdrm/license/app/{titleID}";
                var licenseFileName = "6488b73b912a753a492e2714e9b38bc7.rif";
                var licenseFilePath = $"{licensePath}/{licenseFileName}";

                if (!client.DirectoryExists(licensePath))
                {
                    Report($"Fake license not created. Please run the game to generate the license.");
                    State = ProcessState.Error;
                    return false;
                }

                // Moved to the license directory.
                client.SetWorkingDirectory(licensePath);
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
                var files = await ProcessDirectory(client, titlePath, token);

                // Build the RMP
                var rmp = new RomPack();
                rmp.Header.ContentDescriptor = "NoNpDrmPhysical";

                // Migrate the incoming FTP files.
                rmp.Files = files.ToList<FileEntry>();

                // Add the license file to the list.
                rmp.Files.Add(new FtpFileEntry
                {
                    FtpPath = licenseFilePath,
                    Path = $"/{titleID}/sce_sys/package/work.bin",
                    Size = result.First().Size
                });

                var fileCount = rmp.Files.Count;
                Report($"Found {fileCount} file(s)!");
                await Task.Delay(100, token);

                // Save the file...
                Report(0, fileCount);
                Report($"Transferring...");
                var noNpDrmPath = Path.Combine(_outputPath, titleID + ".nonpdrm");
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
        private async Task<IList<FtpFileEntry>> ProcessDirectory(FtpClient client, string path, CancellationToken token)
        {
            // Create our list of files
            var files = new List<FtpFileEntry>();

            // Return an empty list if the operation is cancelled.
            if (token.IsCancellationRequested)
                return files;

            // Move to the directory
            client.SetWorkingDirectory(path);

            // List the items
            var items = client.GetListing();

            // Iterate through them and create the necessary objects.
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case FtpFileSystemObjectType.File:
                        files.Add(new FtpFileEntry
                        {
                            FtpPath = item.FullName,
                            Path = item.FullName.Replace("/gro0:/app", string.Empty),
                            Size = item.Size
                        });
                        ReportFile(item.Name, fileSize: item.Size);
                        await Task.Delay(1, token);
                        break;
                    case FtpFileSystemObjectType.Directory:
                        var moreFiles = await ProcessDirectory(client, item.FullName, token);
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
