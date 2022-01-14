using ExFat.Filesystem;
using FluentFTP;
using Komponent.IO;
using Komponent.IO.Streams;
using RomPackTool.Core.Processes.Vita;
using RomPackTool.Core.Processing;
using RomPackTool.Core.PSV;
using RomPackTool.Core.RMPK;
using RomPackTool.Core.Sony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomPackTool.WinForms
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class Main : Form
    {


        /// <summary>
        /// Construct the form.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set a few startup variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;
            txtVitaIp.Text = Properties.Settings.Default.VitaIP;
            txtVitaDumpPath.Text = Properties.Settings.Default.VitaDumpDirectory;
        }

        /// <summary>
        /// Handle closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.VitaIP = txtVitaIp.Text.Trim();
            Properties.Settings.Default.Save();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            

            if (!Directory.Exists(txtVitaDumpPath.Text.Trim()))
            {
                textBox1.AppendText($"The selected dump directory doesn't exist.");
                return;
            }
            else
            {
                Properties.Settings.Default.VitaDumpDirectory = txtVitaDumpPath.Text.Trim();
                Properties.Settings.Default.Save();
            }

            textBox1.Clear();
            textBox1.AppendText($"Connecting to your Vita at {txtVitaIp.Text.Trim()}:1337...\r\n");

            var client = new FtpClient($"ftp://{txtVitaIp.Text.Trim()}", 1337, new NetworkCredential(string.Empty, string.Empty))
            {
                DataConnectionType = FtpDataConnectionType.AutoActive
            };

            textBox1.AppendText($"Connected!\r\nValidating environment...\r\n");

            // Make sure a cart is inserted.
            if (!client.DirectoryExists("/gro0:"))
            {
                textBox1.Text = "A game cart is not inserted.";
                return;
            }

            // Get the titleID.
            client.SetWorkingDirectory("/gro0:/app");
            var titleID = client.GetListing().FirstOrDefault()?.Name;

            if (titleID == null)
            {
                textBox1.Text = "A titleID could not be found on the game cart.";
                return;
            }
            textBox1.AppendText($"Dumping title: {titleID}.\r\n");

            var titlePath = $"/gro0:/app/{titleID}";

            // Check if the NoNpDrm license has been created.
            var licensePath = $"/ux0:/nonpdrm/license/app/{titleID}";
            var licenseFileName = "6488b73b912a753a492e2714e9b38bc7.rif";
            var licenseFilePath = $"{licensePath}/{licenseFileName}";

            if (!client.DirectoryExists(licensePath))
            {
                textBox1.Text = "Fake license not created. Run the game.";
                return;
            }

            client.SetWorkingDirectory(licensePath);
            var result = await client.GetListingAsync();

            if (result.FirstOrDefault()?.Name != licenseFileName)
            {
                textBox1.Text = "Fake license not created. Run the game.";
                return;
            }

            // Set up the directories
            var dumpPath = Path.Combine(Properties.Settings.Default.VitaDumpDirectory, "Dumps", "Vita");
            Directory.CreateDirectory(dumpPath);

            // Build a list of the files to dump.
            textBox1.AppendText($"Listing files to dump...\r\n");
            var listingProgress = new Progress<string>(p => { textBox1.AppendText(p + "\r\n"); });
            var files = await ProcessDirectory(client, titlePath, listingProgress);

            // Build the RMP
            var rmp = new RomPack();
            rmp.Header.ContentDescriptor = "NoNpDrmGame";

            // Migrate the incoming FTP files.
            rmp.Files = files.ToList<FileEntry>();

            // Add the license file to the list.
            rmp.Files.Add(new FtpFileEntry
            {
                FtpPath = licenseFilePath,
                Path = $"/{titleID}/sce_sys/package/work.bin",
                Size = result.First().Size
            });

            var fileCount = rmp.Files.Count();
            textBox1.AppendText($"Found {fileCount} file(s)!\r\n");

            progressBar1.Maximum = fileCount;
            var savingProgress = new Progress<ProgressReport>(p =>
            {
                progressBar1.Value = (int)p.Percentage;
                if (p.HasMessage)
                    textBox1.AppendText(p.Message + "\r\n");
            });
            var nndPath = Path.Combine(dumpPath, titleID + ".nonpdrm");
            await rmp.FtpSave(client, File.Create(nndPath), savingProgress);

            textBox1.AppendText($"Dumping complete!\r\nWrote dump to {nndPath}\r\n");

            // Disconnect from the Vita, dumping is complete.
            await client.DisconnectAsync();
        }

        private async Task<IEnumerable<FtpFileEntry>> ProcessDirectory(FtpClient client, string path, IProgress<string> progress)
        {
            // Create our list of files
            var files = new List<FtpFileEntry>();

            // Move to the directory
            client.SetWorkingDirectory(path);

            // List the items
            var items = client.GetListing();

            // Iterate through them and create the necessary objects.
            foreach (var item in items)
            {
                switch (item.Type)
                {
                    case FtpFileSystemObjectType.File:
                        files.Add(new FtpFileEntry
                        {
                            FtpPath = item.FullName,
                            Path = item.FullName.Replace("/gro0:/app", string.Empty),
                            Size = item.Size
                        });
                        progress.Report($"Added {item.Name}");
                        await Task.Delay(1);
                        break;
                    case FtpFileSystemObjectType.Directory:
                        var moreFiles = await ProcessDirectory(client, item.FullName, progress);
                        files.AddRange(moreFiles);
                        break;
                    case FtpFileSystemObjectType.Link:
                        break;
                }
            }

            // Return our file list.
            return files;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "RomPack [Vita NoNpDrm] (*.nonpdrm)|*.nonpdrm|All Files (*.*)|*.*",
                Title = "Select a RomPack to extract..."
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Clear();
                var rmp = new RomPack(File.OpenRead(ofd.FileName));

                // Dump the files from the RomPack.
                foreach (var file in rmp.Files)
                {
                    var outPath = Path.Combine(Path.GetDirectoryName(ofd.FileName), file.Path.TrimStart('/'));

                    Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                    using var outFile = File.Create(outPath);
                    file.FileData.CopyTo(outFile);

                    outFile.Close();
                    textBox1.AppendText($"Extracting {Path.GetFileName(file.Path)}...\r\n");
                }

                textBox1.AppendText($"{Path.GetFileName(ofd.FileName)} was successfully extracted!\r\n");
            }
        }

        //private async Task RunProcess(RomPack rmp, Stream output)
        //{
        //    progressBar1.Maximum = rmp.Header.FileCount;
        //    var savingProgress = new Progress<ProgressReport>(p =>
        //    {
        //        progressBar1.Value = (int)p.Percentage;
        //        if (p.HasMessage)
        //            textBox1.AppendText(p.Message + "\r\n");
        //    });

        //    await rmp.Save(output, savingProgress);
        //}

        private void btnBuildRomPack_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var basePath = fbd.SelectedPath;
                var sce_sys = Path.Combine(basePath, "sce_sys");
                var package = Path.Combine(sce_sys, "package");
                var work_bin = Path.Combine(package, "work.bin");

                if (Directory.Exists(sce_sys))
                {

                }
            }
        }

        private async void btnCreateNoIntroPSV_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Vita Image (*.psv)|*.psv|All Files (*.*)|*.*",
                Title = "Select a PSV to convert..."
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Clear();
                btnCreateNoIntroPSV.Enabled = false;
                progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                progressBar1.Maximum = 1000;

                var progress = new Progress<ProgressReport>(p =>
                {
                    if (p.Percentage > 0)
                    {
                        progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                        progressBar1.Value = (int)p.Percentage;
                    }
                    if (p.HasMessage)
                        textBox1.AppendText(p.Message + "\r\n");
                });
                await Task.Run(() => ConvertToNoIntro(ofd.FileName, progress));

                btnCreateNoIntroPSV.Enabled = true;
            }
        }

        private async Task ConvertToNoIntro(string psvPath, IProgress<ProgressReport> progress)
        {
            // Variables
            var noNpDrmRifPath = Path.Combine(Path.GetDirectoryName(psvPath), Path.GetFileNameWithoutExtension(psvPath) + ".rif");
            var newPsvPath = Path.Combine(Path.GetDirectoryName(psvPath), Path.GetFileNameWithoutExtension(psvPath) + ".no-intro.psv");

            // PSV
            await Task.Delay(1);
            using var br = new BinaryReaderX(File.OpenRead(psvPath), false);
            var psv = br.ReadType<PsvHeader>();
            progress.Report(new ProgressReport { Message = $"Loaded {Path.GetFileName(psvPath)}..." });

            if (br.BaseStream.Length - PsvHeader.SectorSize != psv.ImageSize || psv.Flags.HasFlag(PsvFlags.FLAG_TRIMMED | PsvFlags.FLAG_COMPRESSED))
            {
                progress.Report(new ProgressReport { Message = $"ERROR: Invalid PSV file. (Was it trimmed?)" });
                return;
            }

            if (psv.Key1.SequenceEqual(new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }))
            {
                progress.Report(new ProgressReport { Message = $"ERROR: Invalid PSV file. (Was it already converted for No-Intro?)" });
                return;
            }

            if (!File.Exists(noNpDrmRifPath))
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The NoNpDrm rif is missing. It should be called {Path.GetFileName(noNpDrmRifPath)} and in the same directory as the PSV." });
                return;
            }

            using var nndBr = new BinaryReaderX(File.OpenRead(noNpDrmRifPath));
            var noNpDrmRif = nndBr.ReadType<VitaRif>();
            nndBr.BaseStream.Position = 0;
            progress.Report(new ProgressReport { Message = $"Loaded {Path.GetFileName(noNpDrmRifPath)}..." });

            // Locating the license
            // Find FF FF 00 01 00 01 04 02 00 00 00 00 00 00 00 00
            var rifHeader = new byte[] { 0xFF, 0xFF, 0x00, 0x01, 0x00, 0x01, 0x04, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            progress.Report(new ProgressReport { Message = $"Locating the license offset..." });

            var blockSize = 0x1000;
            var totalSize = 0L;
            var sector = 0x10;
            var sectorSize = 0x200;
            var licenseOffset = 0;
            byte[] block;

            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                br.BaseStream.Position = sector * sectorSize;

                var bytesRemaining = br.BaseStream.Length - br.BaseStream.Position;
                var readBytes = Math.Min(blockSize, bytesRemaining);

                block = br.ReadBytes((int)readBytes);

                var found = false;
                var sectors = block.Length / sectorSize;
                for (var i = 0; i < sectors; i++)
                {
                    var blockOffset = i * sectorSize;
                    if (block.Skip(blockOffset).Take(rifHeader.Length).SequenceEqual(rifHeader))
                    {
                        licenseOffset = (sector * sectorSize) + blockOffset;
                        found = true;
                        break;
                    }
                }
                if (found) break;

                sector += sectors;
            }

            if (licenseOffset <= 0)
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The license could not be found in the PSV file." });
                return;
            }
            progress.Report(new ProgressReport { Message = $"License offset found!" });

            br.BaseStream.Position = licenseOffset;
            var ogRif = br.ReadType<VitaRif>();

            // Final sanity check.
            if (ogRif.TitleID != noNpDrmRif.TitleID)
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The PSV is of {ogRif.TitleID.Trim('\0')} but the NoNpDrm RIF provided is for {noNpDrmRif.TitleID.Trim('\0')}." });
                return;
            }

            // ALL SET!

            // Writing
            var size = (double)br.BaseStream.Length;
            using var bw = new BinaryWriterX(File.Create(newPsvPath));

            // Reset our PSV reader back to the beginning.
            br.BaseStream.Position = 0;
            progress.Report(new ProgressReport { Message = $"Saving No-Intro PSV...", Percentage = 0 });

            // PSV Header
            var newHeader = new PsvHeader
            {
                Flags = PsvFlags.PLAG_NOINTRO,
                ImageSize = psv.ImageSize,
                ImageOffsetSector = psv.ImageOffsetSector,
                Version = psv.Version
            };
            bw.WriteType(newHeader);
            progress.Report(new ProgressReport { Message = "Wrote new PSV header..." });

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Nullify 0xC for 0x54.
            //for (var i = 0; i < 0x54; i++)
            //    bw.Write((byte)0x0);
            progress.Report(new ProgressReport { Message = "Copying data..." });

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy structure until 0x1E00.
            bw.Write(br.ReadBytes(0x1E00 - (int)br.BaseStream.Position));
            progress.Report(new ProgressReport { Percentage = bw.BaseStream.Position / size * 1000 });

            // Nullify 0x1E00 for 0x260.
            progress.Report(new ProgressReport { Message = "Nullifying unknown unique block..." });
            for (var i = 0; i < 0x260; i++)
                bw.Write((byte)0x0);
            progress.Report(new ProgressReport { Message = "Copying data..." });

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy data until we reach the license in blocks.
            blockSize = 0x100000;
            totalSize = licenseOffset - br.BaseStream.Position;
            var updateInterval = 4;
            for (var i = 0; i < totalSize; i += blockSize)
            {
                var bytesRemaining = licenseOffset - br.BaseStream.Position;
                var readBytes = Math.Min(blockSize, bytesRemaining);
                bw.Write(br.ReadBytes((int)readBytes));
                if (i % (blockSize * updateInterval) == 0)
                    progress.Report(new ProgressReport { Percentage = bw.BaseStream.Position / size * 1000 });
            }

            // Write the license.
            progress.Report(new ProgressReport { Message = "Injecting NoNpDrm license..." });
            bw.Write(nndBr.ReadAllBytes());
            progress.Report(new ProgressReport { Message = "Copying data...", Percentage = bw.BaseStream.Position / size * 1000 });

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy the rest of the game data in blocks.
            blockSize = 0x100000;
            totalSize = br.BaseStream.Length - br.BaseStream.Position;
            for (var i = 0; i < totalSize; i += blockSize)
            {
                var bytesRemaining = br.BaseStream.Length - br.BaseStream.Position;
                var readBytes = Math.Min(blockSize, bytesRemaining);
                bw.Write(br.ReadBytes((int)readBytes));
                if (i % (blockSize * updateInterval) == 0)
                    progress.Report(new ProgressReport { Percentage = bw.BaseStream.Position / size * 1000 });
            }

            bw.Close();
            progress.Report(new ProgressReport { Message = $"No-Intro PSV created successfully!", Percentage = 1000 });
        }

        private void btnBrowseVitaDumps_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog()
            {
                SelectedPath = Properties.Settings.Default.VitaDumpDirectory
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtVitaDumpPath.Text = ofd.SelectedPath;
                Properties.Settings.Default.VitaDumpDirectory = ofd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void btnExtractNoNpDrm_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Vita No-Intro Image (*.no-intro.psv)|*.no-intro.psv",
                Title = "Select a No-Intro PSV to extract..."
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Clear();

                using var br = new BinaryReaderX(File.OpenRead(ofd.FileName));

                var psv = br.ReadType<PsvHeader>();

                if (psv.Magic != "PSV\0" && !psv.Flags.HasFlag(PsvFlags.PLAG_NOINTRO))
                {
                    textBox1.AppendText($"ERROR: The selected file is not a No-Intro PSV.");
                    return;
                }

                br.BaseStream.Position = psv.ImageOffsetSector * PsvHeader.SectorSize;

                var scei = br.ReadType<VitaCartHeader>();
                br.BaseStream.Position = scei.ExFatOffset + PsvHeader.SectorSize + 0x48;

                var volumeSize = br.ReadInt64() * PsvHeader.SectorSize;
                var exFatStream = new SubStream(br.BaseStream, scei.ExFatOffset + PsvHeader.SectorSize, volumeSize);

                using var partition = new ExFatPathFilesystem(exFatStream);

                // Title ID
                var titleDirectory = partition.EnumerateEntries(@"\app").First();
                var titleID = titleDirectory.Path.Substring(titleDirectory.Path.LastIndexOf(@"\") + 1);

                ProcessExFatDirectory(partition, @"\app", titleID);

                // work.bin transfer.
                var licenseFile = partition.EnumerateEntries($@"\license\app\{titleID}").First();

                var workPath = Path.Combine(Properties.Settings.Default.VitaDumpDirectory, $@"Vita\{titleID}\sce_sys\package\work.bin");
                var workFile = File.Create(workPath);

                var licenseFileStream = partition.Open(licenseFile.Path, FileMode.Open, FileAccess.Read);
                licenseFileStream.CopyTo(workFile);
                workFile.Close();

                textBox1.AppendText($"\\{$@"sce_sys\package\work.bin"}\r\n");

                exFatStream.Close();
                br.BaseStream.Close();
            }
        }

        private void ProcessExFatDirectory(ExFatPathFilesystem partition, string path, string titleID)
        {
            var items = partition.EnumerateEntries(path);

            foreach (var item in items)
            {
                if (item.Attributes.HasFlag(FileAttributes.Directory))
                    ProcessExFatDirectory(partition, $@"\{item.Path}", titleID);
                else
                {
                    var outPath = Path.Combine(txtVitaDumpPath.Text.Trim(), "Vita", titleID, item.Path.Replace($@"app\{titleID}\", string.Empty));
                    Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                    var outFile = File.Create(outPath);
                    var file = partition.Open(item.Path, FileMode.Open, FileAccess.Read);
                    file.CopyTo(outFile);
                    outFile.Close();
                    textBox1.AppendText($"\\{item.Path.Replace($@"app\{titleID}\", string.Empty)}\r\n");
                }
            }
        }

        private void btnTestTask_Click(object sender, EventArgs e)
        {
            var ipAddress = txtVitaIp.Text.Trim();
            var outputPath = txtVitaDumpPath.Text.Trim();
            var process = new FtpDownloadCartToNoNpDrm(ipAddress, outputPath);

            var pm = new ProcessMonitor(process);
            flpProcesses.Controls.Add(pm);
        }

        //private void StartProcess(Process process)
        //{

        //}
    }
}
