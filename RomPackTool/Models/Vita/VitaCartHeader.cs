using Komponent.IO.Attributes;
using Kontract.Models.IO;

namespace RomPackTool.Models.Vita
{
    /// <summary>
    /// SCEI Header
    /// </summary>
    [Alignment(0x200)]
    public class VitaCartHeader
    {
        /// <summary>
        /// SCEI
        /// </summary>
        [FixedLength(0x20)]
        public string Magic = "Sony Computer Entertainment Inc.";

        public int Unk1;
        public int Unk2;
        [FixedLength(0x28)]
        public byte[] Unk3;
        public int Unk4;
        public int Unk5;
        public int Unk6;
        public int Unk7;
        public int Unk8;

        /// <summary>
        /// Offset to the exFAT filesystem.
        /// </summary>
        [Endianness(ByteOrder = ByteOrder.BigEndian)]
        public int ExFatOffset;

        public int Unk9;
        public int Unk10;
        [FixedLength(0x18C)]
        public byte[] Unk11;
        public int Unk12;
    }
}
