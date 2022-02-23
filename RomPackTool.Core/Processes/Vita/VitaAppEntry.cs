using System.Drawing;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// 
    /// </summary>
    public class VitaAppEntry
    {
        /// <summary>
        /// 
        /// </summary>
        public Image Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TitleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Inserted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public VitaAppEntry() { }

        /// <summary>
        /// 
        /// </summary>
        public VitaAppEntry(Bitmap icon, string source, string titleId, string name)
        {
            Icon = icon;
            Source = source;
            TitleId = titleId;
            Name = name;
        }
    }
}
