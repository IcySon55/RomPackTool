﻿///
/// PSV Documentation from https://gist.github.com/yifanlu/d546e687f751f951b1109ffc8dd8d903
///

using Komponent.IO.Attributes;
using System;

namespace RomPackTool.Models
{
    [Alignment(0x200)]
    public class PsvHeader
    {
        public static long HeaderSize => 0x200;

        /// <summary>
        /// PSV Magic Header
        /// </summary>
        [FixedLength(4)]
        public string Magic = "PSV\0";

        /// <summary>
        /// PSV Version Number
        /// </summary>
        public int Version = 0x0;

        /// <summary>
        /// PSV Image Flags
        /// </summary>
        public PsvFlags Flags;

        /// <summary>
        /// For klicensee decryption.
        /// </summary>
        [FixedLength(0x10)]
        public byte[] Key1;

        /// <summary>
        /// For klicensee decryption.
        /// </summary>
        [FixedLength(0x10)]
        public byte[] Key2;

        /// <summary>
        /// Same as in RIF.
        /// </summary>
        [FixedLength(0x14)]
        public byte[] Signature;

        /// <summary>
        /// Optional consistancy check. sha256 over complete data (including any trimmed bytes) if cart dump, sha256 over the pkg if digital dump.
        /// </summary>
        [FixedLength(0x20)]
        public byte[] Hash;

        /// <summary>
        /// If trimmed, this will be actual size.
        /// </summary>
        public long ImageSize;

        /// <summary>
        /// Image (dump/pkg) offset in multiple of 512 bytes. must be > 0 if an actual image exists. == 0 if no image is included.
        /// </summary>
        public long ImageOffsetSector;
    }

    [Flags]
    public enum PsvFlags : int
    {
        FLAG_TRIMMED = 1,
        FLAG_DIGITAL = 2,
        FLAG_COMPRESSED = 4
    }
}
