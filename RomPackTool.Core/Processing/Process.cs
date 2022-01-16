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
        /// Determines the current state of the running process.
        /// </summary>
        public ProcessState State { get; protected set; } = ProcessState.None;

        /// <summary>
        /// Reference to the <see cref="IProgress{T}"/> that was passed into <see cref="Run(IProgress{ProgressReport}, CancellationToken)"/>.
        /// </summary>
        public IProgress<ProgressReport> Progress { get; protected set; }

        /// <summary>
        /// Forces child processes to provide a progress object.
        /// </summary>
        /// <param name="progress"></param>
        public Process(IProgress<ProgressReport> progress)
        {
            Progress = progress ?? throw new ArgumentNullException(nameof(progress));
        }

        /// <summary>
        /// Called on by the UI to have the process validate its configuration.
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> Validate(CancellationToken token);

        /// <summary>
        /// The progress trackable execution of a process.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public abstract Task<bool> Run(CancellationToken token);

        /// <summary>
        /// Shortcut for simply reporting a message.
        /// </summary>
        /// <param name="message"></param>
        protected void Report(string message)
        {
            Progress?.Report(new ProgressReport
            {
                Message = message
            });
        }

        // TODO: Create more shortcuts for reporting
    }
}
