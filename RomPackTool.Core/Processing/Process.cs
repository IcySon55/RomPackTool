using System;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processing
{
    /// <summary>
    /// Encapsulates a process that is being performed.
    /// </summary>
    public abstract class Process
    {
        /// <summary>
        /// The name of the process as displayed on the UI.
        /// </summary>
        public string Name { get; protected set; } = string.Empty;

        /// <summary>
        /// Determines if more than one instance of this process can run simultaneously.
        /// </summary>
        public string ExclusivityGroup { get; protected set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public ProcessState State { get; protected set; } = ProcessState.None;

        /// <summary>
        /// Called on by the UI to have the process validate its configuration.
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> Validate();

        /// <summary>
        /// The progress trackable execution of a process.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public abstract Task<bool> Run(IProgress<ProgressReport> progress, CancellationToken token);
    }
}
