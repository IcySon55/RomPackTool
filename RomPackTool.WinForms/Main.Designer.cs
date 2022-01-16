
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
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.tbsMain = new System.Windows.Forms.TabControl();
            this.tabVita = new System.Windows.Forms.TabPage();
            this.btnTestTask = new System.Windows.Forms.Button();
            this.tabPSV = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExtractNoNpDrm = new System.Windows.Forms.Button();
            this.btnCreateNoIntroPSV = new System.Windows.Forms.Button();
            this.tabRomPack = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuildRomPack = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.flpProcesses = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.tbsMain.SuspendLayout();
            this.tabVita.SuspendLayout();
            this.tabPSV.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabRomPack.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(239, 100);
            this.textBox1.TabIndex = 2;
            // 
            // txtVitaIp
            // 
            this.txtVitaIp.Location = new System.Drawing.Point(136, 22);
            this.txtVitaIp.Name = "txtVitaIp";
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
            this.groupBox1.Location = new System.Drawing.Point(6, 16);
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
            // txtVitaDumpPath
            // 
            this.txtVitaDumpPath.Location = new System.Drawing.Point(87, 103);
            this.txtVitaDumpPath.Name = "txtVitaDumpPath";
            this.txtVitaDumpPath.Size = new System.Drawing.Size(198, 23);
            this.txtVitaDumpPath.TabIndex = 6;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(977, 525);
            this.pnlMain.TabIndex = 8;
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(3, 3);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.tbsMain);
            this.splMain.Panel1.Controls.Add(this.progressBar1);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.flpProcesses);
            this.splMain.Panel2MinSize = 200;
            this.splMain.Size = new System.Drawing.Size(971, 519);
            this.splMain.SplitterDistance = 744;
            this.splMain.TabIndex = 10;
            // 
            // tbsMain
            // 
            this.tbsMain.Controls.Add(this.tabVita);
            this.tbsMain.Controls.Add(this.tabPSV);
            this.tbsMain.Controls.Add(this.tabRomPack);
            this.tbsMain.Controls.Add(this.tabLog);
            this.tbsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbsMain.Location = new System.Drawing.Point(0, 0);
            this.tbsMain.Name = "tbsMain";
            this.tbsMain.SelectedIndex = 0;
            this.tbsMain.Size = new System.Drawing.Size(744, 498);
            this.tbsMain.TabIndex = 1;
            // 
            // tabVita
            // 
            this.tabVita.Controls.Add(this.btnTestTask);
            this.tabVita.Controls.Add(this.groupBox1);
            this.tabVita.Location = new System.Drawing.Point(4, 24);
            this.tabVita.Name = "tabVita";
            this.tabVita.Padding = new System.Windows.Forms.Padding(3);
            this.tabVita.Size = new System.Drawing.Size(736, 470);
            this.tabVita.TabIndex = 0;
            this.tabVita.Text = "Vita";
            this.tabVita.UseVisualStyleBackColor = true;
            // 
            // btnTestTask
            // 
            this.btnTestTask.Location = new System.Drawing.Point(122, 227);
            this.btnTestTask.Name = "btnTestTask";
            this.btnTestTask.Size = new System.Drawing.Size(198, 46);
            this.btnTestTask.TabIndex = 0;
            this.btnTestTask.Text = "Test Button";
            this.btnTestTask.UseVisualStyleBackColor = true;
            this.btnTestTask.Click += new System.EventHandler(this.btnTestTask_Click);
            // 
            // tabPSV
            // 
            this.tabPSV.Controls.Add(this.groupBox3);
            this.tabPSV.Location = new System.Drawing.Point(4, 24);
            this.tabPSV.Name = "tabPSV";
            this.tabPSV.Padding = new System.Windows.Forms.Padding(3);
            this.tabPSV.Size = new System.Drawing.Size(736, 470);
            this.tabPSV.TabIndex = 3;
            this.tabPSV.Text = "PSV";
            this.tabPSV.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExtractNoNpDrm);
            this.groupBox3.Controls.Add(this.btnCreateNoIntroPSV);
            this.groupBox3.Location = new System.Drawing.Point(6, 14);
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
            // tabRomPack
            // 
            this.tabRomPack.Controls.Add(this.groupBox2);
            this.tabRomPack.Location = new System.Drawing.Point(4, 24);
            this.tabRomPack.Name = "tabRomPack";
            this.tabRomPack.Padding = new System.Windows.Forms.Padding(3);
            this.tabRomPack.Size = new System.Drawing.Size(736, 470);
            this.tabRomPack.TabIndex = 1;
            this.tabRomPack.Text = "RomPack";
            this.tabRomPack.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBuildRomPack);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
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
            // tabLog
            // 
            this.tabLog.Controls.Add(this.textBox1);
            this.tabLog.Location = new System.Drawing.Point(4, 24);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(736, 470);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 498);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(744, 21);
            this.progressBar1.TabIndex = 1;
            // 
            // flpProcesses
            // 
            this.flpProcesses.AutoScroll = true;
            this.flpProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProcesses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProcesses.Location = new System.Drawing.Point(0, 0);
            this.flpProcesses.Name = "flpProcesses";
            this.flpProcesses.Size = new System.Drawing.Size(223, 519);
            this.flpProcesses.TabIndex = 0;
            this.flpProcesses.WrapContents = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(977, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 547);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Main";
            this.Text = "RomPackTool - Alpha 6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.tbsMain.ResumeLayout(false);
            this.tabVita.ResumeLayout(false);
            this.tabPSV.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabRomPack.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button btnTestTask;
        private System.Windows.Forms.TabControl tbsMain;
        private System.Windows.Forms.TabPage tabVita;
        private System.Windows.Forms.TabPage tabRomPack;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabPage tabPSV;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

