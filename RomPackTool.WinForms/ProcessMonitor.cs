using RomPackTool.Core.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        private CancellationToken _token;

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

        public delegate void CloseEventHandler(object sender, EventArgs e);

        public event CloseEventHandler CloseEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Run()
        {
            var valid = await Task.Run(() => _process.Validate());
            if (!valid) return false;

            var progress = new Progress<ProgressReport>(p =>
            {
                progressBar1.Value = (int)p.Percentage;
                //if (p.HasMessage)
                //    textBox1.AppendText(p.Message + "\r\n");
            });

            var result = await Task.Run(() => _process.Run(progress, _source.Token));

            if (_source.IsCancellationRequested && !result)
            {
                lblStatus.Text = "Cancelled";
                progressBar1.SetState(ProgressBarStyle.Pause);
                progressBar1.Update();
                lblStatus.ForeColor = Color.DarkRed;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (!_source.IsCancellationRequested)
                _source.Cancel();
            else
            {
                tsbCancel.Enabled = false;
                CloseEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
