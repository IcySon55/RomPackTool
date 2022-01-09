namespace RomPackTool.RMPK
{
    /// <summary>
    /// Extends the standard RomPackFileEntry to store a dedicated FtpPath.
    /// </summary>
    public class RomPackFtpFileEntry : RomPackFileEntry
    {
        /// <summary>
        /// The FTP path to the file that will be stored in the RomPack.
        /// </summary>
        public string FtpPath { get; set; }
    }
}
