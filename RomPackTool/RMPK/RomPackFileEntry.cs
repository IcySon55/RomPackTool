using Komponent.IO.Attributes;
using System;
using System.IO;

namespace RomPackTool.RMPK
{
    /// <summary>
    /// RomPack file entry definition.
    /// </summary>
    [Alignment(4)]
    public class RomPackFileEntry : IComparable<RomPackFileEntry>
    {
        /// <summary>
        /// Header size.
        /// </summary>
        public int EntrySize => (sizeof(long) + sizeof(long) + 1 + Path.Length).Align(4);

        /// <summary>
        /// Relative file offset. Relative to the end of the file entry list.
        /// </summary>
        public long Offset;

        /// <summary>
        /// File size.
        /// </summary>
        public long Size;

        /// <summary>
        /// Stores the stream
        /// </summary>
        public Stream FileData { get; set; }

        /// <summary>
        /// Full path name of the file. Encoding is UTF-8.
        /// </summary>
        public string Path = string.Empty;

        /// <summary>
        /// Enables sorting of file entries based on their <see cref="Path"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RomPackFileEntry other) => Path.CompareTo(other.Path);
    }
}
