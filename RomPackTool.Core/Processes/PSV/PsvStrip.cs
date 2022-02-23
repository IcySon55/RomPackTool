using RomPackTool.Core.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.PSV
{
    /// <summary>
    /// Strips a PSV to prepare it for redump.
    /// </summary>
    public class PsvStrip : Process
    {
        /// <summary>
        /// 
        /// </summary>
        private string _psvPath { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="psvPath"></param>
        public PsvStrip(string psvPath)
        {
            _psvPath = psvPath ?? throw new ArgumentException(string.Empty, nameof(psvPath));

            Name = "PSV Strip";
            ExclusivityGroup = psvPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected override async Task<bool> Validate(CancellationToken token)
        {
            try
            {
                // PsvPath
                if (!File.Exists(_psvPath))
                {
                    Report($"The file \"{_psvPath}\" doesn't exist.");
                    State = ProcessState.Error;
                    return false;
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
        /// <param name="token"></param>
        /// <returns></returns>
        public override async Task<bool> Run(CancellationToken token)
        {



            throw new NotImplementedException();
        }
    }
}
