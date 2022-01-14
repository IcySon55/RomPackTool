
namespace RomPackTool.WinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtVitaIp = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseVitaDumps = new System.Windows.Forms.Button();
            this.txtVitaDumpPath = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExtractNoNpDrm = new System.Windows.Forms.Button();
            this.btnCreateNoIntroPSV = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuildRomPack = new System.Windows.Forms.Button();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.flpProcesses = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnTestTask = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(425, 156);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(316, 21);
            this.progressBar1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(425, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(316, 127);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.txtVitaIp.Location = new System.Drawing.Point(136, 22);
            this.txtVitaIp.Name = "textBox2";
            this.txtVitaIp.Size = new System.Drawing.Size(149, 23);
            this.txtVitaIp.TabIndex = 3;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(87, 25);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(43, 15);
            this.lblIP.TabIndex = 4;
            this.lblIP.Text = "Vita IP:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(198, 46);
            this.button2.TabIndex = 5;
            this.button2.Text = "Dump Cart to NoNpDrm\r\n";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(87, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(198, 46);
            this.button3.TabIndex = 6;
            this.button3.Text = "Extract RomPack Contents...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBrowseVitaDumps);
            this.groupBox1.Controls.Add(this.txtVitaDumpPath);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.txtVitaIp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 138);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PlayStation Vita/TV";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dump Path:";
            // 
            // btnBrowseVitaDumps
            // 
            this.btnBrowseVitaDumps.Location = new System.Drawing.Point(291, 103);
            this.btnBrowseVitaDumps.Name = "btnBrowseVitaDumps";
            this.btnBrowseVitaDumps.Size = new System.Drawing.Size(85, 23);
            this.btnBrowseVitaDumps.TabIndex = 7;
            this.btnBrowseVitaDumps.Text = "Browse...";
            this.btnBrowseVitaDumps.UseVisualStyleBackColor = true;
            this.btnBrowseVitaDumps.Click += new System.EventHandler(this.btnBrowseVitaDumps_Click);
            // 
            // textBox3
            // 
            this.txtVitaDumpPath.Location = new System.Drawing.Point(87, 103);
            this.txtVitaDumpPath.Name = "textBox3";
            this.txtVitaDumpPath.Size = new System.Drawing.Size(198, 23);
            this.txtVitaDumpPath.TabIndex = 6;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.statusStrip1);
            this.pnlMain.Controls.Add(this.splMain);
            this.pnlMain.Controls.Add(this.groupBox3);
            this.pnlMain.Controls.Add(this.groupBox2);
            this.pnlMain.Controls.Add(this.textBox1);
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Controls.Add(this.progressBar1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(977, 547);
            this.pnlMain.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExtractNoNpDrm);
            this.groupBox3.Controls.Add(this.btnCreateNoIntroPSV);
            this.groupBox3.Location = new System.Drawing.Point(12, 356);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 82);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PSV Tools";
            // 
            // btnExtractNoNpDrm
            // 
            this.btnExtractNoNpDrm.Location = new System.Drawing.Point(194, 22);
            this.btnExtractNoNpDrm.Name = "btnExtractNoNpDrm";
            this.btnExtractNoNpDrm.Size = new System.Drawing.Size(182, 46);
            this.btnExtractNoNpDrm.TabIndex = 8;
            this.btnExtractNoNpDrm.Text = "Extract NoNpDrm from PSV";
            this.btnExtractNoNpDrm.UseVisualStyleBackColor = true;
            this.btnExtractNoNpDrm.Click += new System.EventHandler(this.btnExtractNoNpDrm_Click);
            // 
            // btnCreateNoIntroPSV
            // 
            this.btnCreateNoIntroPSV.Location = new System.Drawing.Point(6, 22);
            this.btnCreateNoIntroPSV.Name = "btnCreateNoIntroPSV";
            this.btnCreateNoIntroPSV.Size = new System.Drawing.Size(182, 46);
            this.btnCreateNoIntroPSV.TabIndex = 7;
            this.btnCreateNoIntroPSV.Text = "Create No-Intro PSV";
            this.btnCreateNoIntroPSV.UseVisualStyleBackColor = true;
            this.btnCreateNoIntroPSV.Click += new System.EventHandler(this.btnCreateNoIntroPSV_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBuildRomPack);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 135);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RomPack Tools";
            // 
            // btnBuildRomPack
            // 
            this.btnBuildRomPack.Enabled = false;
            this.btnBuildRomPack.Location = new System.Drawing.Point(87, 22);
            this.btnBuildRomPack.Name = "btnBuildRomPack";
            this.btnBuildRomPack.Size = new System.Drawing.Size(198, 46);
            this.btnBuildRomPack.TabIndex = 7;
            this.btnBuildRomPack.Text = "Build RomPack from Directory...";
            this.btnBuildRomPack.UseVisualStyleBackColor = true;
            this.btnBuildRomPack.Click += new System.EventHandler(this.btnBuildRomPack_Click);
            // 
            // splMain
            // 
            this.splMain.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(414, 215);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.btnTestTask);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.flpProcesses);
            this.splMain.Panel2MinSize = 200;
            this.splMain.Size = new System.Drawing.Size(542, 276);
            this.splMain.SplitterDistance = 329;
            this.splMain.TabIndex = 10;
            // 
            // flpProcesses
            // 
            this.flpProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProcesses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProcesses.Location = new System.Drawing.Point(0, 0);
            this.flpProcesses.Name = "flpProcesses";
            this.flpProcesses.Size = new System.Drawing.Size(209, 276);
            this.flpProcesses.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(977, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnTestTask
            // 
            this.btnTestTask.Location = new System.Drawing.Point(38, 41);
            this.btnTestTask.Name = "btnTestTask";
            this.btnTestTask.Size = new System.Drawing.Size(198, 46);
            this.btnTestTask.TabIndex = 0;
            this.btnTestTask.Text = "Test Button";
            this.btnTestTask.UseVisualStyleBackColor = true;
            this.btnTestTask.Click += new System.EventHandler(this.btnTestTask_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 547);
            this.Controls.Add(this.pnlMain);
            this.Name = "Main";
            this.Text = "RomPackTool - Alpha 6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtVitaIp;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuildRomPack;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateNoIntroPSV;
        private System.Windows.Forms.Button btnBrowseVitaDumps;
        private System.Windows.Forms.TextBox txtVitaDumpPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExtractNoNpDrm;
        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.FlowLayoutPanel flpProcesses;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnTestTask;
    }
}

