using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomPackTool.Models
{
    public class VitaRIF
    {
        [FixedLength(0x10)]
        public byte[] Header;

        [FixedLength(0x30, StringEncoding = StringEncoding.ASCII)]
        public string TitleID;

        [FixedLength(0x10)]
        public byte[] Unk2;

        [FixedLength(0x10)]
        public byte[] NoNpDrmLicense;

        [FixedLength(0x1A0)]
        public byte[] Padding;
    }
}
