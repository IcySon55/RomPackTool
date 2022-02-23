namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// Transports options related to the Vita into the various methods.
    /// </summary>
    public class VitaFtpOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string IpAddress {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SyncPath {  get; set; }

        /// <summary>
        /// Allows empty construction.
        /// </summary>
        public VitaFtpOptions() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="outputPath"></param>
        public VitaFtpOptions(string ipAddress, string outputPath)
        {
            IpAddress = ipAddress;
            SyncPath = outputPath;
        }
    }
}
