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
        public ProcessState State { get; set; } = ProcessState.None;

        /// <summary>
        /// Reference to the <see cref="IProgress{T}"/> that was passed into <see cref="Run(IProgress{ProgressReport}, CancellationToken)"/>.
        /// </summary>
        public IProgress<ProgressReport> Progress { get; protected set; } = new Progress<ProgressReport>();

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
        /// Shortcut for reporting a message.
        /// </summary>
        /// <param name="message"></param>
        protected void Report(string message)
        {
            Progress?.Report(new ProgressReport { Message = message });
        }

        /// <summary>
        /// Shortcut for reporting a filename to the UI.
        /// </summary>
        /// <param name="name"></param>
        protected void ReportFile(string name, int totalFiles = 0, int currentFile = 0, long fileSize = 0)
        {
            Progress?.Report(new FileReport { FileName = name, TotalFiles = totalFiles, CurrentFile = currentFile, FileSize = fileSize });
        }

        /// <summary>
        /// Shortcut for reporting a new progress value.
        /// </summary>
        /// <param name="value"></param>
        protected void Report(int value)
        {
            Progress?.Report(new ProgressReport { Value = value });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="value"></param>
        protected void Report(int value, int maxValue)
        {
            Progress?.Report(new ProgressReport { MaxValue = maxValue, Value = value });
        }
    }
}
