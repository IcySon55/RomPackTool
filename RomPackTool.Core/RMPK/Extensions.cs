using FluentFTP;
using Komponent.IO;
using RomPackTool.Core.Processing;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.RMPK
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method that can save directly to RomPack via an FTP connection.
        /// </summary>
        /// <param name="rmp"></param>
        /// <param name="client"></param>
        /// <param name="output"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static async Task<ProcessState> FtpSave(this RomPack rmp, Stream output, FtpClient client, IProgress<ProgressReport> progress, CancellationToken token)
        {
            // Sanity checks
            if (!client.IsConnected)
            {
                progress.Report(new ProgressReport { Message = "The FTP client is not connected." });
                return ProcessState.Error;
            }

            if (output == null)
            {
                progress.Report(new ProgressReport { Message = "The output stream is null." });
                return ProcessState.Error;
            }

            try
            {
                // Load our output into a BinaryWriterX.
                using var bw = new BinaryWriterX(output, Encoding.UTF8);

                // Create local variables from the RomPack.
                var Header = rmp.Header;
                var Files = rmp.Files;

                // Sort our files by path according to a consistent invariant local.
                Files = Files.OrderBy(f => f.Path, StringComparer.InvariantCulture).ToList();

                // Find FirstFileOffset.
                var firstFileOffset = Header.Size + Files.Sum(f => f.EntrySize);

                // Reset position to FirstFileOffset.
                bw.BaseStream.Position = firstFileOffset;

                // Write out file data.
                for (int i = 0; i < Files.Count; i++)
                {
                    var file = Files[i];
                    var ftpFile = Files[i] as FtpFileEntry;

                    // Update the file offset.
                    file.Offset = bw.BaseStream.Position - firstFileOffset;

                    // Update the UI.
                    progress.Report(new FileReport
                    {
                        FileName = $"{Path.GetFileName(file.Path)}",
                        TotalFiles = Files.Count,
                        CurrentFile = i + 1,
                        FileSize = file.Size,
                        Value = i
                    });
                    await Task.Delay(1, token);

                    // Download the file over FTP;

                    var ftpProgress = new Progress<FtpProgress>();
                    ftpProgress.ProgressChanged += (sender, e) =>
                    {
                        //progress.Report(new ProgressReport() {  });
                    };

                    await client.DownloadAsync(bw.BaseStream, ftpFile.FtpPath, 0, ftpProgress, token);

                    // Update the UI.
                    progress.Report(new ProgressReport { Value = i + 1 });

                    // Alignment.
                    bw.WriteAlignment(RomPack.FileAlignment);
                }

                // Store the file size for later.
                var fileSize = bw.BaseStream.Position;

                // Reset to write the header.
                bw.BaseStream.Position = 0;

                // Update the header.
                Header.Magic = "RMPK"; // Make sure no funny business is going on.
                Header.FileSize = fileSize;
                Header.FileCount = Files.Count;
                Header.FirstFileOffset = firstFileOffset;

                // Write out the header.
                bw.WriteType(Header);

                // Write out file entries.
                bw.WriteMultiple(Files);

                // Close the stream.
                output.Close();

                return ProcessState.Completed;
            }
            catch (TaskCanceledException)
            {
                return ProcessState.Cancelled;
            }
            catch (Exception ex)
            {
                progress.Report(new ProgressReport { Message = ex.Message });
                return ProcessState.Error;
            }
        }
    }
}
