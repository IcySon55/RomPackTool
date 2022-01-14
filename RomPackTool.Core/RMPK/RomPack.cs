using Komponent.IO;
using Komponent.IO.Streams;
using RomPackTool.Core.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomPackTool.Core.RMPK
{
    /// <summary>
    /// Main interaction class for RomPacks.
    /// </summary>
    public class RomPack
    {
        /// <summary>
        /// 
        /// </summary>
        public static int FileAlignment = 4;

        /// <summary>
        /// RomPack header.
        /// </summary>
        public Header Header;

        /// <summary>
        /// File list.
        /// </summary>
        public List<FileEntry> Files;

        /// <summary>
        /// Instantiates an empty RomPack.
        /// </summary>
        public RomPack()
        {
            Header = new Header();
            Files = new List<FileEntry>();
        }

        /// <summary>
        /// Loads valid RomPack metadata into memory.
        /// </summary>
        /// <param name="input"></param>
        public RomPack(Stream input)
        {
            // Make sure the input stream is not null.
            if (input == null) throw new ArgumentNullException("input");

            // Validate basic length requirements
            if (input.Length < Header.Size) throw new InvalidDataException("Invalid RomPack format. (File Size)");

            // Load our input into a BinaryReaderX.
            var br = new BinaryReaderX(input, Encoding.UTF8, true);

            // Read the header
            Header = br.ReadType<Header>();

            // Header validation
            if (Header.Magic != "RMPK") throw new InvalidDataException("Invalid RomPack format. (Header Corrupt)");
            if (Header.FileSize != br.BaseStream.Length) throw new InvalidDataException("Invalid RomPack format. (File Size)");

            // Read the file entries
            Files = br.ReadMultiple<FileEntry>(Header.FileCount).ToList();

            // File validation
            if (Header.FirstFileOffset != br.BaseStream.Position) throw new InvalidDataException("Invalid RomPack format. (File Entries Corrupt)");

            // Populate the file data streams.
            foreach (var file in Files)
                file.FileData = new SubStream(br.BaseStream, Header.FirstFileOffset + file.Offset, file.Size);

            // Otherwise assume everything is fine and we're now done loading.
        }

        /// <summary>
        /// Saves the current contents of this RomPack to an output <see cref="Stream"/>.
        /// </summary>
        /// <param name="oupput"></param>
        public async Task<bool> Save(Stream output, IProgress<ProgressReport> progress)
        {
            // Load our output into a BinaryWriterX.
            using var bw = new BinaryWriterX(output, Encoding.UTF8);

            // Sort our files by path according to a consistent invariant local.
            Files = Files.OrderBy(f => f.Path, StringComparer.InvariantCulture).ToList();

            // Find FirstFileOffset.
            var firstFileOffset = Header.Size + Files.Sum(f => f.EntrySize);

            // Reset position to FirstFileOffset.
            bw.BaseStream.Position = firstFileOffset;

            // Write out file data.
            for (int i = 0; i < Files.Count; i++)
            {
                var file = Files[i];

                // Update the file entry.
                file.Offset = bw.BaseStream.Position - firstFileOffset; // Relative to first file offset.
                file.Size = file.FileData.Length;

                // Write file data to the output stream.
                await file.FileData.CopyToAsync(bw.BaseStream);

                // Update the UI.
                progress.Report(new ProgressReport
                {
                    Message = $"File {i + 1} of {Files.Count}",
                    Percentage = i + 1
                });

                // Alignment.
                bw.WriteAlignment(FileAlignment);
            }

            // Store the file size for later.
            var fileSize = bw.BaseStream.Position;

            // Reset to write the header.
            bw.BaseStream.Position = 0;

            // Update the header.
            Header.Magic = "RMPK"; // Make sure no funny business is going on.
            Header.FileSize = fileSize;
            Header.FileCount = Files.Count;
            Header.FirstFileOffset = firstFileOffset;

            // Write out the header.
            bw.WriteType(Header);

            // Write out file entries.
            bw.WriteMultiple(Files);

            return true;
        }
    }
}
