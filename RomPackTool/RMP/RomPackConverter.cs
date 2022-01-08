using Komponent.IO;
using Komponent.IO.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomPackTool.RMP
{
    /// <summary>
    /// Main interaction class for RomPacks.
    /// </summary>
    public class RomPackConverter
    {
        /// <summary>
        /// 
        /// </summary>
        private const int _alignment = 4;

        /// <summary>
        /// Loads valid RomPack metadata into memory.
        /// </summary>
        /// <param name="input"></param>
        public RomPack ReadOldRomPack(Stream input)
        {
            var rmp = new RomPack();

            // Make sure the input stream is not null.
            if (input == null) throw new ArgumentNullException("input");

            // Validate basic length requirements
            if (input.Length < RomPackHeader.Size) throw new InvalidDataException("Invalid RomPack format. (File Size)");

            // Load our input into a BinaryReaderX.
            var br = new BinaryReaderX(input, Encoding.UTF8, true);

            // Read the header manually
            var header = new RomPackHeader();
            header.Magic = br.ReadString(4, Encoding.ASCII);
            header.ContentDescriptor = br.ReadString(4, Encoding.ASCII);
            header.FileSize = br.ReadInt64();
            header.FileCount = br.ReadInt32();
            header.FirstFileOffset = br.ReadInt64();
            br.ReadInt32(); // Old alignment

            // Header validation
            if (header.Magic != "RMP\0") throw new InvalidDataException("Invalid RomPack format. (Header Corrupt)");
            if (header.FileSize != br.BaseStream.Length) throw new InvalidDataException("Invalid RomPack format. (File Size)");
            rmp.Header = header;

            // Read the file entries
            var files = br.ReadMultiple<RomPackFileEntry>(header.FileCount).ToList();

            // File validation
            if (header.FirstFileOffset != br.BaseStream.Position) throw new InvalidDataException("Invalid RomPack format. (File Entries Corrupt)");
            rmp.Files = files;

            // Populate the file data streams.
            foreach (var file in files)
                file.FileData = new SubStream(br.BaseStream, file.Offset, file.Size);

            // Otherwise assume everything is fine and we're now done loading.
            return rmp;
        }
    }
}
