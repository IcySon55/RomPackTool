namespace RomPackTool.Core.Processing
{
    /// <summary>
    /// The ProgressReport class passes completion percentages to the UI.
    /// </summary>
    public class ProgressReport
    {
        /// <summary>
        /// The maximum value.
        /// </summary>
        public double MaxValue { get; set; } = 100;

        /// <summary>
        /// The current progress value being reported. Usually between 0 and 100.
        /// </summary>
        public double Value { get; set; } = 0.0;

        /// <summary>
        /// The current status message of the step for this progress report.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Determines whether or not a new line is appended to messages received through a ProgressReport.
        /// </summary>
        public bool NewLine { get; set; } = true;

        /// <summary>
        /// Simple check for whether or not there is a message.
        /// </summary>
        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);
    }
}
