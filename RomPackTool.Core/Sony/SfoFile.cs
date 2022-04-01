using Komponent.IO;
using Komponent.IO.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RomPackTool.Core.Sony
{
    /// <summary>
    /// 
    /// </summary>
    public class SfoFile
    {
        /// <summary>
        /// 
        /// </summary>
        public SfoHeader Header { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, SfoValue> DataTable { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SfoFile(Stream input)
        {
            using var br = new BinaryReaderX(input);
            input.Position = 0;

            Header = br.ReadType<SfoHeader>();

            // Index Table
            var indexTable = br.ReadMultiple<SfoIndexEntry>(Header.TablesEntryCount).ToList();

            // Key Table
            var keyTable = new List<string>();
            for (var i = 0; i < indexTable.Count; i++)
            {
                var index = indexTable[i];
                br.BaseStream.Position = Header.KeyTableStart + index.KeyOffset;

                var keyLength = (i + 1 == indexTable.Count ? (Header.DataTableStart - Header.KeyTableStart) : indexTable[i + 1].KeyOffset) - index.KeyOffset;
                keyTable.Add(Encoding.ASCII.GetString(br.ReadBytes(keyLength)).Trim('\0'));
            }

            // Data Table
            DataTable = new Dictionary<string, SfoValue>();
            for (var i = 0; i < indexTable.Count; i++)
            {
                var index = indexTable[i];
                br.BaseStream.Position = Header.DataTableStart + index.DataOffset;

                var value = index.DataFormat switch
                {
                    SfoDataFormat.Int32 => new SfoValue { DataFormat = index.DataFormat, Value = br.ReadInt32() },
                    SfoDataFormat.Utf8 => new SfoValue
                    {
                        DataFormat = index.DataFormat,
                        Value = Encoding.UTF8.GetString(br.ReadBytes(index.DataLength)).Trim('\0')
                    },
                    SfoDataFormat.Utf8Null => new SfoValue
                    {
                        DataFormat = index.DataFormat,
                        Value = Encoding.UTF8.GetString(br.ReadBytes(index.DataLength)).Trim('\0')
                    },
                    SfoDataFormat.Ascii => new SfoValue
                    {
                        DataFormat = index.DataFormat,
                        Value = Encoding.ASCII.GetString(br.ReadBytes(index.DataLength)).Trim('\0')
                    },
                    _ => null
                };

                DataTable.Add(keyTable[i], value);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SfoHeader
    {
        /// <summary>
        /// Always PSF
        /// </summary>
        [FixedLength(4)]
        public string Magic = "\0PSF";

        /// <summary>
        /// Usually 1.1
        /// </summary>
        public int Version = 0x101;

        /// <summary>
        /// Start offset of key_table
        /// </summary>
        public int KeyTableStart;

        /// <summary>
        /// Number of entries in all tables
        /// </summary>
        public int DataTableStart;

        /// <summary>
        /// Number of entries in all tables
        /// </summary>
        public int TablesEntryCount;
    }

    /// <summary>
    /// 
    /// </summary>
    public class SfoIndexEntry
    {
        /// <summary>
        /// param_key offset (relative to start offset of key_table)
        /// </summary>
        public short KeyOffset;

        /// <summary>
        /// param_data data type
        /// </summary>
        public SfoDataFormat DataFormat;

        /// <summary>
        /// param_data used bytes
        /// </summary>
        public int DataLength;

        /// <summary>
        /// param_data total bytes
        /// </summary>
        public int DataMaxLength;

        /// <summary>
        /// param_data offset (relative to start offset of data_table)
        /// </summary>
        public int DataOffset;
    }

    /// <summary>
    /// 
    /// </summary>
    public class SfoValue
    {
        /// <summary>
        /// 
        /// </summary>
        public SfoDataFormat DataFormat;

        /// <summary>
        /// 
        /// </summary>
        public object Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetString() => DataFormat is SfoDataFormat.Utf8 or SfoDataFormat.Utf8Null or SfoDataFormat.Ascii ? (string)Value : null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetInt32() => DataFormat == SfoDataFormat.Int32 ? (int)Value : null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => DataFormat switch
        {
            SfoDataFormat.Int32 => ((int)Value).ToString("X2"),
            _ => Value.ToString(),
        };
    }

    /// <summary>
    /// Short that determines the data type of the field.
    /// </summary>
    public enum SfoDataFormat : short
    {
        Utf8 = 0x004,
        Utf8Null = 0x204,
        Ascii = 0x402,
        Int32 = 0x404
    }
}
