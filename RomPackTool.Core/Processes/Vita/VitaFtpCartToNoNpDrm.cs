using FluentFTP;
using RomPackTool.Core.Processing;
using System;
using System.IO;
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
        public VitaFtpCartToNoNpDrm(IProgress<ProgressReport> progress, string ipAddress, string outputPath) : base(progress)
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

            // Working variables.
            var titleID = string.Empty;

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
    }
}
