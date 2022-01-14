
namespace RomPackTool.WinForms
{
    partial class ProcessMonitor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessMonitor));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tspCommon = new System.Windows.Forms.ToolStrip();
            this.tslName = new System.Windows.Forms.ToolStripLabel();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblFileLabel = new System.Windows.Forms.Label();
            this.tspCommon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(2, 98);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(194, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // tspCommon
            // 
            this.tspCommon.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspCommon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslName,
            this.tsbCancel});
            this.tspCommon.Location = new System.Drawing.Point(0, 0);
            this.tspCommon.Name = "tspCommon";
            this.tspCommon.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tspCommon.Size = new System.Drawing.Size(198, 25);
            this.tspCommon.TabIndex = 1;
            this.tspCommon.Text = "toolStrip1";
            // 
            // tslName
            // 
            this.tslName.Name = "tslName";
            this.tslName.Size = new System.Drawing.Size(82, 22);
            this.tslName.Text = "Process Name";
            // 
            // tsbCancel
            // 
            this.tsbCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 22);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lblStatusLabel);
            this.panel1.Controls.Add(this.lblFileLabel);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(198, 123);
            this.panel1.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(59, 76);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(134, 16);
            this.lblStatus.TabIndex = 3;
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.Location = new System.Drawing.Point(5, 76);
            this.lblStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(48, 16);
            this.lblStatusLabel.TabIndex = 2;
            this.lblStatusLabel.Text = "Status:";
            this.lblStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFileLabel
            // 
            this.lblFileLabel.Location = new System.Drawing.Point(5, 5);
            this.lblFileLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblFileLabel.Name = "lblFileLabel";
            this.lblFileLabel.Size = new System.Drawing.Size(48, 16);
            this.lblFileLabel.TabIndex = 1;
            this.lblFileLabel.Text = "File:";
            this.lblFileLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ProcessMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tspCommon);
            this.Name = "ProcessMonitor";
            this.Size = new System.Drawing.Size(198, 148);
            this.tspCommon.ResumeLayout(false);
            this.tspCommon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStrip tspCommon;
        private System.Windows.Forms.ToolStripLabel tslName;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFileLabel;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
    }
}
