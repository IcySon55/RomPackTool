
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tspCommon = new System.Windows.Forms.ToolStrip();
            this.tslName = new System.Windows.Forms.ToolStripLabel();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblFileLabel = new System.Windows.Forms.Label();
            this.tspCommon.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(2, 144);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(252, 21);
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
            this.tspCommon.Size = new System.Drawing.Size(256, 25);
            this.tspCommon.TabIndex = 1;
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
            this.tsbCancel.Image = global::RomPackTool.WinForms.Properties.Resources.cancel;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 22);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.Window;
            this.pnlMain.Controls.Add(this.lblFile2);
            this.pnlMain.Controls.Add(this.lblMessage);
            this.pnlMain.Controls.Add(this.lblFile);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.lblStatusLabel);
            this.pnlMain.Controls.Add(this.lblFileLabel);
            this.pnlMain.Controls.Add(this.progressBar1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(2);
            this.pnlMain.Size = new System.Drawing.Size(256, 167);
            this.pnlMain.TabIndex = 2;
            // 
            // lblFile2
            // 
            this.lblFile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFile2.Location = new System.Drawing.Point(64, 21);
            this.lblFile2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(187, 29);
            this.lblFile2.TabIndex = 7;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Location = new System.Drawing.Point(64, 56);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(187, 60);
            this.lblMessage.TabIndex = 6;
            // 
            // lblFile
            // 
            this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFile.Location = new System.Drawing.Point(64, 5);
            this.lblFile.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(187, 16);
            this.lblFile.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(64, 122);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(187, 16);
            this.lblStatus.TabIndex = 3;
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatusLabel.Location = new System.Drawing.Point(5, 122);
            this.lblStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(56, 16);
            this.lblStatusLabel.TabIndex = 2;
            this.lblStatusLabel.Text = "Status:";
            this.lblStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFileLabel
            // 
            this.lblFileLabel.Location = new System.Drawing.Point(5, 5);
            this.lblFileLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblFileLabel.Name = "lblFileLabel";
            this.lblFileLabel.Size = new System.Drawing.Size(56, 16);
            this.lblFileLabel.TabIndex = 1;
            this.lblFileLabel.Text = "File:";
            this.lblFileLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ProcessMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tspCommon);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.Name = "ProcessMonitor";
            this.Size = new System.Drawing.Size(256, 192);
            this.tspCommon.ResumeLayout(false);
            this.tspCommon.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStrip tspCommon;
        private System.Windows.Forms.ToolStripLabel tslName;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblFileLabel;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFile2;
    }
}
