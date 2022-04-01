using ExFat.Filesystem;
using Komponent.IO;
using Komponent.IO.Streams;
using RomPackTool.Core.Processes;
using RomPackTool.Core.Processes.PSV;
using RomPackTool.Core.Processes.Vita;
using RomPackTool.Core.Processing;
using RomPackTool.Core.PSV;
using RomPackTool.Core.RMPK;
using RomPackTool.Core.Sony;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        /// 
        /// </summary>
        private List<Process> _processes = new();

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
            txtVitaSyncPath.Text = Properties.Settings.Default.VitaDumpDirectory;
        }

        /// <summary>
        /// Handle closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Update settings from UI before closing.
            Properties.Settings.Default.VitaIP = txtVitaIp.Text.Trim();
            Properties.Settings.Default.VitaDumpDirectory = txtVitaSyncPath.Text.Trim();

            // Save settings.
            Properties.Settings.Default.Save();
        }

        #region Vita

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private VitaFtpOptions BuildVitaFtpOptions() => new(txtVitaIp.Text.Trim(), txtVitaSyncPath.Text.Trim());

        /// <summary>
        /// Refresh the list of games on the Vita.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVitaRefresh_Click(object sender, EventArgs e)
        {
            var options = BuildVitaFtpOptions();
            var process = new VitaFtpGetAppList(options, Path.Combine(options.SyncPath, "Cache"));
            await RunProcess(process);

            var apps = process.Apps;

            dgvVita.AutoGenerateColumns = false;
            dgvVita.DataSource = apps;
            dgvVita.AutoResizeColumns();

            var cartRow = dgvVita.Rows.Cast<DataGridViewRow>().Where(r => ((VitaAppEntry)r.DataBoundItem).Inserted).FirstOrDefault();
            foreach (DataGridViewCell cell in cartRow.Cells)
                cell.Style.BackColor = Color.LightGreen;
        }

        /// <summary>
        /// Start a 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVitaFtpToNoNpDrm_Click(object sender, EventArgs e)
        {
            if (dgvVita.SelectedRows.Count > 0)
            {
                VitaFtpProcess process = null;
                var selectedApp = (VitaAppEntry)dgvVita.SelectedRows[0].DataBoundItem;

                if (selectedApp.Source.StartsWith("Cartridge"))
                    process = new VitaFtpCartToNoNpDrm(BuildVitaFtpOptions(),selectedApp);
                else if (selectedApp.Source.Contains("PSN"))
                    process = new VitaFtpDigitalToNoNpDrm(BuildVitaFtpOptions(), selectedApp);

                if (process != null)
                    await RunProcess(process);
            }

            // TEST CODE:
            //var formatOptions = new ExFat.ExFatFormatOptions
            //{
            //    BytesPerSector = 0x200,
            //    VolumeSpace = 0x10000
            //};
            //var ex = ExFat.Partition.ExFatPartition.Format(new MemoryStream(10 * 1024 * 1024), formatOptions);
        }

        #endregion

        #region PSV

        /// <summary>
        /// 
        /// </summary>
        private const string PsvFilter = "Vita Image (*.psv)|*.psv|All Files (*.*)|*.*";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVitaStripPSV_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = PsvFilter, Title = "Select a PSV to strip..." };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var process = new PsvStrip(ofd.FileName);
                await RunProcess(process);
            }
        }


        /// <summary>
        /// Extracts a NoNpDrm copy of three different PSV types.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExtractNoNpDrm_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Vita Image (*.psv)|*.psv",
                Title = "Select a PSV to extract..."
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var process = new PsvExtractNoNpDrm(ofd.FileName, Properties.Settings.Default.VitaDumpDirectory);

                if (process != null)
                    await RunProcess(process);
            }
        }

        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVita_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVita.SelectedRows.Count > 0)
            {
                var selectedApp = (VitaAppEntry)dgvVita.SelectedRows[0].DataBoundItem;
                btnVitaFtpToNoNpDrm.Enabled = !selectedApp.Source.Contains("NoNpDrm");
            }
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
                txtLog.Clear();
                var rmp = new RomPack(File.OpenRead(ofd.FileName));

                // Dump the files from the RomPack.
                foreach (var file in rmp.Files)
                {
                    var outPath = Path.Combine(Path.GetDirectoryName(ofd.FileName), file.Path.TrimStart('/'));

                    Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                    using var outFile = File.Create(outPath);
                    file.FileData.CopyTo(outFile);

                    outFile.Close();
                    txtLog.AppendText($"Extracting {Path.GetFileName(file.Path)}...\r\n");
                }

                txtLog.AppendText($"{Path.GetFileName(ofd.FileName)} was successfully extracted!\r\n");
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
                Title = "Select a stripped PSV to convert..."
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtLog.Clear();
                btnVitaCreateNoIntroPSV.Enabled = false;

                var progress = new Progress<ProgressReport>(p =>
                {
                    if (p.Value > 0)
                    {
                        //progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                        //progressBar1.Value = (int)p.Value;
                    }
                    if (p.HasMessage)
                        txtLog.AppendText(p.Message + "\r\n");
                });
                await Task.Run(() => ConvertToNoIntro(ofd.FileName, progress));

                btnVitaCreateNoIntroPSV.Enabled = true;
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
                progress.Report(new ProgressReport { Message = $"ERROR: The NoNpDrm rif is missing. It should be called {Path.GetFileName(noNpDrmRifPath)} and be in the same directory as the PSV." });
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
            if (ogRif.ContentID != noNpDrmRif.ContentID)
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The PSV is of {ogRif.ContentID.Trim('\0')} but the NoNpDrm RIF provided is for {noNpDrmRif.ContentID.Trim('\0')}." });
                return;
            }

            // ALL SET!

            // Writing
            var size = (double)br.BaseStream.Length;
            using var bw = new BinaryWriterX(File.Create(newPsvPath));

            // Reset our PSV reader back to the beginning.
            br.BaseStream.Position = 0;
            progress.Report(new ProgressReport { Message = $"Saving No-Intro PSV...", Value = 0 });

            // PSV Header
            var newHeader = new PsvHeader
            {
                Flags = PsvFlags.FLAG_NOINTRO,
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
            progress.Report(new ProgressReport { Value = bw.BaseStream.Position / size * 1000 });

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
                    progress.Report(new ProgressReport { Value = bw.BaseStream.Position / size * 1000 });
            }

            // Write the license.
            progress.Report(new ProgressReport { Message = "Injecting NoNpDrm license..." });
            bw.Write(nndBr.ReadAllBytes());
            progress.Report(new ProgressReport { Message = "Copying data...", Value = bw.BaseStream.Position / size * 1000 });

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
                    progress.Report(new ProgressReport { Value = bw.BaseStream.Position / size * 1000 });
            }

            bw.Close();
            progress.Report(new ProgressReport { Message = $"No-Intro PSV created successfully!", Value = 1000 });
        }

        private void btnBrowseVitaDumps_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog()
            {
                SelectedPath = Properties.Settings.Default.VitaDumpDirectory
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtVitaSyncPath.Text = ofd.SelectedPath;
                Properties.Settings.Default.VitaDumpDirectory = ofd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private async void btnTestTask_Click(object sender, EventArgs e)
        {
            await RunProcess(new TestProcess());
        }

        /// <summary>
        /// Adds a new process to the list and begins execution.
        /// </summary>
        /// <param name="process"></param>
        private async Task RunProcess(Process process)
        {
            // Can this process be added?
            if (_processes.Any(p => p.ExclusivityGroup == process.ExclusivityGroup))
            {
                return; // No, this process is already running.
            } // Yes.

            // Register this process.
            _processes.Add(process);

            // Create a new Process monitor for our process.
            var pm = new ProcessMonitor(process);
            flpProcesses.Controls.Add(pm);

            // Add a handler for updating the master log.
            pm.ProgressChanged += (sender, e) =>
            {
                if (e.HasMessage)
                    txtLog.AppendText(e.Message + (e.NewLine ? "\r\n" : string.Empty));
            };

            // Add a handler for removing the control when it is closed.
            pm.Close += (sender, e) => { flpProcesses.Controls.Remove(pm); };

            // Run the process!
            var result = await pm.Run();

            if (result)
                flpProcesses.Controls.Remove(pm);

            // Unregister the process regardless of how it ended.
            _processes.Remove(process);
        }
    }
}
