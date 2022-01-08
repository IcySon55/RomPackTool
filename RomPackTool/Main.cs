using FluentFTP;
using Komponent.IO;
using RomPackTool.Models;
using RomPackTool.RMP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomPackTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;
            textBox2.Text = Properties.Settings.Default.VitaIP;
            textBox3.Text = Properties.Settings.Default.VitaDumpDirectory;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.VitaIP = textBox2.Text.Trim();
            Properties.Settings.Default.Save();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox3.Text.Trim()))
            {
                textBox1.AppendText($"The selected dump directory doesn't exist.");
                return;
            }
            else
            {
                Properties.Settings.Default.VitaDumpDirectory = textBox3.Text.Trim();
                Properties.Settings.Default.Save();
            }

            textBox1.Clear();
            textBox1.AppendText($"Connecting to your Vita at {textBox2.Text.Trim()}:1337...\r\n");

            var client = new FtpClient($"ftp://{textBox2.Text.Trim()}", 1337, new NetworkCredential(string.Empty, string.Empty))
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
            rmp.Files = files.ToList<RomPackFileEntry>();

            // Add the license file to the list.
            rmp.Files.Add(new RomPackFtpFileEntry
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

        private async Task<IEnumerable<RomPackFtpFileEntry>> ProcessDirectory(FtpClient client, string path, IProgress<string> progress)
        {
            // Create our list of files
            var files = new List<RomPackFtpFileEntry>();

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
                        files.Add(new RomPackFtpFileEntry
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

                var progress = new Progress<ProgressReport>(p =>
                {
                    if (p.HasMessage)
                        textBox1.AppendText(p.Message + "\r\n");
                });
                await Task.Run(() => ConvertToNoIntro(ofd.FileName, progress));

                progressBar1.Maximum = 100;
                progressBar1.Value = 100;
                progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
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

            if (br.BaseStream.Length - PsvHeader.HeaderSize != psv.ImageSize || psv.Flags.HasFlag(PsvFlags.FLAG_TRIMMED | PsvFlags.FLAG_COMPRESSED))
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
            var noNpDrmRif = nndBr.ReadType<VitaRIF>();
            nndBr.BaseStream.Position = 0;
            progress.Report(new ProgressReport { Message = $"Loaded {Path.GetFileName(noNpDrmRifPath)}..." });

            // Locating the license
            // Find FF FF 00 01 00 01 04 02 00 00 00 00 00 00 00 00
            var rifHeader = new byte[] { 0xFF, 0xFF, 0x00, 0x01, 0x00, 0x01, 0x04, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            progress.Report(new ProgressReport { Message = $"Locating the license offset..." });
            var sector = 0x10;
            var offset = 0;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                br.BaseStream.Position = sector * 0x200;
                if (br.ReadBytes(0x10).SequenceEqual(rifHeader))
                    offset = sector * 0x200;
                sector++;
            }

            if (offset <= 0)
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The license could not be found in the PSV file." });
                return;
            }
            progress.Report(new ProgressReport { Message = $"License offset found!" });

            br.BaseStream.Position = offset;
            var ogRif = br.ReadType<VitaRIF>();

            // Final sanity check.
            if (ogRif.TitleID != noNpDrmRif.TitleID)
            {
                progress.Report(new ProgressReport { Message = $"ERROR: The PSV is of {ogRif.TitleID.Trim('\0')} but the NoNpDrm RIF provided is for {noNpDrmRif.TitleID.Trim('\0')}." });
                return;
            }

            // ALL SET!

            // Writing
            using var bw = new BinaryWriterX(File.Create(newPsvPath));
            progress.Report(new ProgressReport { Message = $"Saving No-Intro PSV..." });

            // Reset our PSV reader back to the beginning.
            br.BaseStream.Position = 0;

            // PSV Header
            bw.Write(br.ReadBytes(0xC));

            // Nullify 0xC for 0x54.
            for (var i = 0; i < 0x54; i++)
                bw.Write((byte)0x0);

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy structure until 0x1E00.
            bw.Write(br.ReadBytes(0x1E00 - (int)br.BaseStream.Position));

            // Nullify 0x1E00 for 0x260.
            for (var i = 0; i < 0x260; i++)
                bw.Write((byte)0x0);

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy structure until we reach the license.
            // Copying in blocks of 0x40 might also work.
            var copyLength = offset - br.BaseStream.Position;
            for (var i = 0; i < copyLength; i += 0x20)
                bw.Write(br.ReadBytes(0x20));

            // Write the license.
            bw.Write(nndBr.ReadAllBytes());

            // Resync
            br.BaseStream.Position = bw.BaseStream.Position;

            // Copy the rest of the game data in blocks of 512 bytes (0x200).
            copyLength = br.BaseStream.Length - br.BaseStream.Position;
            for (var i = 0; i < copyLength; i += 0x200)
                bw.Write(br.ReadBytes(0x200));

            bw.Close();

            progress.Report(new ProgressReport { Message = $"No-Intro PSV created successfully!" });
        }

        private void btnBrowseVitaDumps_Click(object sender, EventArgs e)
        {
            var ofd = new FolderBrowserDialog()
            {
                SelectedPath = Properties.Settings.Default.VitaDumpDirectory
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = ofd.SelectedPath;
                Properties.Settings.Default.VitaDumpDirectory = ofd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
