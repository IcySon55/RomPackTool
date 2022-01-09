using Komponent.IO.Attributes;

namespace RomPackTool.Models.Vita
{
    /// <summary>
    /// Vita License RIF
    /// </summary>
    public class VitaRIF
    {
        /// <summary>
        /// Unknown bytes.
        /// </summary>
        [FixedLength(0x10)]
        public byte[] Header;

        /// <summary>
        /// The long TitleID.
        /// </summary>
        [FixedLength(0x30, StringEncoding = StringEncoding.ASCII)]
        public string TitleID;

        /// <summary>
        /// Unknown bytes.
        /// </summary>
        [FixedLength(0x10)]
        public byte[] Unk2;

        /// <summary>
        /// Unknown bytes.
        /// </summary>
        [FixedLength(0x10)]
        public byte[] NoNpDrmLicense;

        /// <summary>
        /// Unknown bytes.
        /// </summary>
        [FixedLength(0x1A0)]
        public byte[] Padding;
    }
}
