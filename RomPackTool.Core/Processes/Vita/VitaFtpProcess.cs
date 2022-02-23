using RomPackTool.Core.Processing;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// Centralizes Vita processes that require FTP access.
    /// </summary>
    public abstract class VitaFtpProcess : Process
    {
        /// <summary>
        /// Shortcut.
        /// </summary>
        protected string _ipAddress => Options?.IpAddress;

        /// <summary>
        /// Shortcut.
        /// </summary>
        protected string _outputPath => Options?.SyncPath;

        /// <summary>
        /// 
        /// </summary>
        protected VitaFtpOptions Options { get; }

        /// <summary>
        /// Validates the input variables received by the process.
        /// </summary>
        /// <returns></returns>
        protected override async Task<bool> Validate(CancellationToken token)
        {
            // Allows this method to run asynchronously.
            await Task.Delay(1, token);

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
        /// 
        /// </summary>
        /// <param name="options"></param>
        public VitaFtpProcess(VitaFtpOptions options)
        {
            Options = options ?? throw new ArgumentException($"The {nameof(options)} parameter can not be null.", paramName: nameof(options));

            ExclusivityGroup = $"FTP_{_ipAddress}"; // Only one FTP operation at a time (on the same IP)
        }
    }
}
