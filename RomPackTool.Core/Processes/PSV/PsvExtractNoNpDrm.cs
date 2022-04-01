using ExFat.Filesystem;
using Komponent.IO;
using Komponent.IO.Streams;
using RomPackTool.Core.Processing;
using RomPackTool.Core.PSV;
using RomPackTool.Core.Sony;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.PSV
{
    /// <summary>
    /// Extracts a NoNpDrm dump from a PSV.
    /// </summary>
    public class PsvExtractNoNpDrm : Process
    {
        /// <summary>
        /// 
        /// </summary>
        private string _psvPath { get; }

        /// <summary>
        /// 
        /// </summary>
        private string _outputPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private PsvFlags _psvFlags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool _isStripped { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private int _fileCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private int _currentFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string _noNpDrmRifPath => Path.Combine(Path.GetDirectoryName(_psvPath), Path.GetFileNameWithoutExtension(_psvPath) + ".rif");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="psvPath"></param>
        public PsvExtractNoNpDrm(string psvPath, string outputPath)
        {
            _psvPath = psvPath ?? throw new ArgumentException(string.Empty, nameof(psvPath));
            _outputPath = outputPath;

            Name = "PSV Extract NoNpDrm";
            ExclusivityGroup = psvPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected override async Task<bool> Validate(CancellationToken token)
        {
            try
            {
                // PsvPath
                if (!File.Exists(_psvPath))
                {
                    Report($"The file \"{_psvPath}\" doesn't exist.");
                    State = ProcessState.Error;
                    return false;
                }

                // Determine PSV Type
                using var br = new BinaryReaderX(File.OpenRead(_psvPath));
                var psv = br.ReadType<PsvHeader>();
                br.Close();

                if (psv.Magic == "PSV\0")
                    _psvFlags = psv.Flags;
                else if (psv.Magic == "Sony")
                    _isStripped = true;
                else
                {
                    Report("ERORR: The selected file is not a recognized PSV type.");
                    State = ProcessState.Error;
                    return false;
                }

                // Do we need a NoNpDrm RIF?
                if ((_isStripped || !_psvFlags.HasFlag(PsvFlags.FLAG_NOINTRO)) && !File.Exists(_noNpDrmRifPath))
                {
                    Report($@"The selected PSV is a {(_isStripped ? "stripped" : "standard")} dump and requires a NoNpDrm rif for extraction.
It should be called {Path.GetFileName(_noNpDrmRifPath)} and be in the same directory as the PSV.");
                    Report($"The NoNpDrm RIF is missing. See the Log.");
                    State = ProcessState.Error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Report(ex.Message);
                State = ProcessState.Error;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public override async Task<bool> Run(CancellationToken token)
        {
            // Update state.
            State = ProcessState.Running;

            try
            {
                // Parameter validation.
                var valid = await Validate(token);
                if (!valid) return false;

                // ExFatOffset
                var exFatOffset = Constants.VitaExFatOffset + (!_isStripped ? PsvHeader.SectorSize : 0);

                // Volume size
                using var br = new BinaryReaderX(File.OpenRead(_psvPath));
                br.BaseStream.Position = exFatOffset + Constants.ExFatSectorCountOffset;
                var volumeSize = br.ReadInt64() * PsvHeader.SectorSize;

                // ExFat Partition
                var exFatStream = new SubStream(br.BaseStream, exFatOffset, volumeSize);
                using var partition = new ExFatPathFilesystem(exFatStream);

                // Title ID
                var titleDirectory = partition.EnumerateEntries(@"\app").First();
                var titleID = titleDirectory.Path.Substring(titleDirectory.Path.LastIndexOf(@"\") + 1);

                // Count files.
                _fileCount = CountExFatDirectory(partition, @"\app") + 1;

                // Dump files.
                await ProcessExFatDirectory(partition, @"\app", titleID);

                // work.bin transfer.
                if (_isStripped || !_psvFlags.HasFlag(PsvFlags.FLAG_NOINTRO))
                {
                    var workPath = Path.Combine(_outputPath, $@"{titleID}\sce_sys\package\work.bin");
                    File.Copy(_noNpDrmRifPath, workPath, true);
                }
                else
                {
                    var licenseFile = partition.EnumerateEntries($@"\license\app\{titleID}").First();

                    var workPath = Path.Combine(_outputPath, $@"{titleID}\sce_sys\package\work.bin");
                    var workFile = File.Create(workPath);

                    var licenseFileStream = partition.Open(licenseFile.Path, FileMode.Open, FileAccess.Read);
                    licenseFileStream.CopyTo(workFile);
                    workFile.Close();
                }
                _currentFile++;
                ReportFile($@"sce_sys\package\work.bin", _fileCount, _currentFile, PsvHeader.SectorSize);
                Report(_currentFile, _fileCount);

                exFatStream.Close();
                br.BaseStream.Close();

                State = ProcessState.Completed;
                return true;
            }
            catch (TaskCanceledException)
            {
                State = ProcessState.Cancelled;
                return false;
            }
            catch (Exception ex)
            {
                State = ProcessState.Error;
                Report(ex.Message);
                return false;
            }
        }

        private int CountExFatDirectory(ExFatPathFilesystem partition, string path)
        {
            var items = partition.EnumerateEntries(path);

            var count = 0;
            foreach (var item in items)
            {
                if (item.Attributes.HasFlag(FileAttributes.Directory))
                    count += CountExFatDirectory(partition, $@"\{item.Path}");
                else
                    count++;
            }

            return count;
        }

        private async Task ProcessExFatDirectory(ExFatPathFilesystem partition, string path, string titleID)
        {
            var items = partition.EnumerateEntries(path);

            foreach (var item in items)
            {
                if (item.Attributes.HasFlag(FileAttributes.Directory))
                    await ProcessExFatDirectory(partition, $@"\{item.Path}", titleID);
                else
                {
                    var outPath = Path.Combine(_outputPath, titleID, item.Path.Replace($@"app\{titleID}\", string.Empty));
                    Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                    var outFile = File.Create(outPath);
                    var file = partition.Open(item.Path, FileMode.Open, FileAccess.Read);
                    file.CopyTo(outFile);
                    outFile.Close();

                    await Task.Delay(1);

                    _currentFile++;

                    ReportFile(item.Path.Replace($@"app\{titleID}\", string.Empty), _fileCount, _currentFile, item.Length);
                    Report(_currentFile, _fileCount);
                }
            }
        }
    }
}
