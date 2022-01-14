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
        /// <param name="process"></param>
        public ProcessMonitor(Process process)
        {
            InitializeComponent();
            _process = process;
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
