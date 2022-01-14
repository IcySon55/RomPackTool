using RomPackTool.Core.Processing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// Downloads a Vita Cartridge as a NoNpDrm RomPack archive.
    /// </summary>
    public class FtpDownloadCartToNoNpDrm : Process
    {
        /// <summary>
        /// 
        /// </summary>
        private string _ipAddress {  get; }

        private string _outputPath {  get; }

        public FtpDownloadCartToNoNpDrm(string ipAddress, string outputPath)
        {
            Name = "Vita Cart to NoNpDrm";
            ExclusivityGroup = "FTP"; // Only one FTP operation at a time (on the same IP)
            _ipAddress = ipAddress;
            _outputPath = outputPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Validate()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public override async Task<bool> Run(IProgress<ProgressReport> progress, CancellationToken token)
        {
            progress.Report(new ProgressReport { Percentage = 0 });

            await Task.Delay(1000);

            progress.Report(new ProgressReport { Percentage = 50 });

            await Task.Delay(1000);

            progress.Report(new ProgressReport { Percentage = 100 });

            await Task.Delay(200);

            return true;
        }
    }
}
