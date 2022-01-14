using Komponent.IO.Attributes;

namespace RomPackTool.Core.RMPK
{
    /// <summary>
    /// RomPack header definition.
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Header size.
        /// </summary>
        public static int Size => 4 + 16 + sizeof(long) + sizeof(int) + sizeof(long);

        /// <summary>
        /// Magic identifier for the format.
        /// </summary>
        [FixedLength(4)]
        public string Magic = "RMPK";

        /// <summary>
        /// An alphanumeric descriptor of the content that is contained in the RomPack.
        /// </summary>
        [FixedLength(16)]
        public string ContentDescriptor = string.Empty;
        
        /// <summary>
        /// Size of the entire RomPack.
        /// </summary>
        public long FileSize;

        /// <summary>
        /// Number of files stored in the RomPack.
        /// </summary>
        public int FileCount;

        /// <summary>
        /// Offset used to validate <see cref="FileCount"/>. File offsets are relative to this offset.
        /// </summary>
        public long FirstFileOffset;

        /// <summary>
        /// Returns the absolute offset of the given file.
        /// </summary>
        public long GetFileOffset(FileEntry entry) => FirstFileOffset + entry.Offset;
    }
}
