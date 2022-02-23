using RomPackTool.Core;
using RomPackTool.Core.Processing;
using RomPackTool.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomPackTool.WinForms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessMonitor : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private Process _process;

        /// <summary>
        /// 
        /// </summary>
        private CancellationTokenSource _source = new();

        /// <summary>
        /// 
        /// </summary>
        public List<string> MessageLog = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        public ProcessMonitor(Process process)
        {
            InitializeComponent();
            _process = process;
            tslName.Text = _process.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CloseEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 
        /// </summary>
        public event CloseEventHandler Close;

        /// <summary>
        /// Redirects progress reports coming from the running process to the parent UI.
        /// </summary>
        /// <typeparam name="ProcessReport"></typeparam>
        /// <param name="sender"></param>
        /// <param name="report"></param>
        public delegate void EventHandler<ProcessReport>(object sender, ProcessReport report);

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ProgressReport> ProgressChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Run()
        {
            // Add a handler to the internal progress tracker.
            var progress = _process.Progress as Progress<ProgressReport>;

            progress.ProgressChanged += (sender, progress) =>
            {
                // Update progress.
                progressBar1.Maximum = (int)(progress.MaxValue > 0 ? progress.MaxValue : progressBar1.Maximum);

                if (progress.Value > 0)
                    progressBar1.Value = (int)progress.Value;

                // Update file.
                if (progress is FileReport file)
                {
                    lblFile.Text = $"{file.FileName}";

                    var lines = new List<string>();
                    if (file.TotalFiles > 0)
                        lines.Add($"{file.CurrentFile} of {file.TotalFiles}");
                    if (file.FileSize > 0)
                        lines.Add($"{file.FileSize.ToSizeString()}");

                    lblFile2.Text = string.Join("\r\n", lines);
                }
                else
                {
                    // Update the message.
                    if (progress.HasMessage)
                        lblMessage.Text = progress.Message;

                    // Report the progress to the parent UI.
                    ProgressChanged?.Invoke(this, progress);
                }
            };

            // Update status
            lblStatus.Text = "Running...";
            lblStatus.ForeColor = Color.Green;

            // Run the process.
            var result = await Task.Run(() => _process.Run(_source.Token));

            // Handle the result.
            switch (_process.State)
            {
                case ProcessState.Cancelled:
                    tsbCancel.Image = Resources.close;
                    tsbCancel.Text = "Close";
                    progressBar1.SetState(ProgressBarStyle.Pause);
                    lblStatus.ForeColor = Color.Orange;
                    break;
                case ProcessState.None: // Something went wrong here, fall into Error.
                case ProcessState.Running: // Something went wrong here, fall into Error.
                case ProcessState.Error:
                    tsbCancel.Image = Resources.close;
                    tsbCancel.Text = "Close";
                    progressBar1.SetState(ProgressBarStyle.Error);
                    lblStatus.ForeColor = Color.Red;
                    break;
            }
            lblStatus.Text = _process.State.ToString();
            progressBar1.Update();

            // Allows the progress bar to complete its animation
            await Task.Delay(1000);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (_process.State == ProcessState.Running && !_source.IsCancellationRequested)
            {
                // Tell the process that you want to cancel.
                _source.Cancel();
            }
            else if (_process.State != ProcessState.Completed)
            {
                // If the process has successfully cancelled, then close the monitor.
                tsbCancel.Enabled = false;
                Close?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}