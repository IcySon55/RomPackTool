namespace RomPackTool.Core.Processing
{
    /// <summary>
    /// <see cref="FileReport"/> is a extension type that tells the UI to expect a file report.
    /// </summary>
    public class FileReport : ProgressReport
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long FileSize { get; set; }
    }
}
