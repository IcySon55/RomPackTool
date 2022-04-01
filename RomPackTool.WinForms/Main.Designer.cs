
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtVitaIp = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseVitaDumps = new System.Windows.Forms.Button();
            this.txtVitaSyncPath = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbsMain = new System.Windows.Forms.TabControl();
            this.tabVita = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvVita = new System.Windows.Forms.DataGridView();
            this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tspVita = new System.Windows.Forms.ToolStrip();
            this.btnVitaRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnVitaFtpToNoNpDrm = new System.Windows.Forms.ToolStripButton();
            this.tabPSV = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnVitaExtractNoNpDrm = new System.Windows.Forms.Button();
            this.btnVitaStripPSV = new System.Windows.Forms.Button();
            this.btnVitaCreateNoNpDrmPSV = new System.Windows.Forms.Button();
            this.tabRomPack = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuildRomPack = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.lblDivider = new System.Windows.Forms.Label();
            this.flpProcesses = new System.Windows.Forms.FlowLayoutPanel();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tbsMain.SuspendLayout();
            this.tabVita.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVita)).BeginInit();
            this.tspVita.SuspendLayout();
            this.tabPSV.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabRomPack.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(5, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(804, 647);
            this.txtLog.TabIndex = 2;
            // 
            // txtVitaIp
            // 
            this.txtVitaIp.Location = new System.Drawing.Point(87, 22);
            this.txtVitaIp.Name = "txtVitaIp";
            this.txtVitaIp.Size = new System.Drawing.Size(145, 23);
            this.txtVitaIp.TabIndex = 3;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(16, 25);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(65, 15);
            this.lblIP.TabIndex = 4;
            this.lblIP.Text = "IP Address:";
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBrowseVitaDumps);
            this.groupBox1.Controls.Add(this.txtVitaSyncPath);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.txtVitaIp);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 138);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sync Path:";
            // 
            // btnBrowseVitaDumps
            // 
            this.btnBrowseVitaDumps.Location = new System.Drawing.Point(449, 51);
            this.btnBrowseVitaDumps.Name = "btnBrowseVitaDumps";
            this.btnBrowseVitaDumps.Size = new System.Drawing.Size(85, 23);
            this.btnBrowseVitaDumps.TabIndex = 7;
            this.btnBrowseVitaDumps.Text = "Browse...";
            this.btnBrowseVitaDumps.UseVisualStyleBackColor = true;
            this.btnBrowseVitaDumps.Click += new System.EventHandler(this.btnBrowseVitaDumps_Click);
            // 
            // txtVitaSyncPath
            // 
            this.txtVitaSyncPath.Location = new System.Drawing.Point(87, 51);
            this.txtVitaSyncPath.Name = "txtVitaSyncPath";
            this.txtVitaSyncPath.Size = new System.Drawing.Size(356, 23);
            this.txtVitaSyncPath.TabIndex = 6;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.lblDivider);
            this.pnlMain.Controls.Add(this.flpProcesses);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1125, 695);
            this.pnlMain.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbsMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(832, 695);
            this.panel1.TabIndex = 3;
            // 
            // tbsMain
            // 
            this.tbsMain.Controls.Add(this.tabVita);
            this.tbsMain.Controls.Add(this.tabPSV);
            this.tbsMain.Controls.Add(this.tabRomPack);
            this.tbsMain.Controls.Add(this.tabLog);
            this.tbsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbsMain.Location = new System.Drawing.Point(5, 5);
            this.tbsMain.Name = "tbsMain";
            this.tbsMain.SelectedIndex = 0;
            this.tbsMain.Size = new System.Drawing.Size(822, 685);
            this.tbsMain.TabIndex = 1;
            // 
            // tabVita
            // 
            this.tabVita.Controls.Add(this.groupBox4);
            this.tabVita.Controls.Add(this.groupBox1);
            this.tabVita.Location = new System.Drawing.Point(4, 24);
            this.tabVita.Name = "tabVita";
            this.tabVita.Padding = new System.Windows.Forms.Padding(3);
            this.tabVita.Size = new System.Drawing.Size(814, 657);
            this.tabVita.TabIndex = 0;
            this.tabVita.Text = "Vita";
            this.tabVita.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dgvVita);
            this.groupBox4.Controls.Add(this.tspVita);
            this.groupBox4.Location = new System.Drawing.Point(6, 150);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(802, 501);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tools";
            // 
            // dgvVita
            // 
            this.dgvVita.AllowUserToAddRows = false;
            this.dgvVita.AllowUserToDeleteRows = false;
            this.dgvVita.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvVita.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvVita.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVita.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colSource,
            this.colTitleID,
            this.colName});
            this.dgvVita.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVita.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvVita.Location = new System.Drawing.Point(3, 44);
            this.dgvVita.MultiSelect = false;
            this.dgvVita.Name = "dgvVita";
            this.dgvVita.ReadOnly = true;
            this.dgvVita.RowHeadersVisible = false;
            this.dgvVita.RowTemplate.Height = 25;
            this.dgvVita.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVita.ShowEditingIcon = false;
            this.dgvVita.Size = new System.Drawing.Size(796, 454);
            this.dgvVita.TabIndex = 6;
            this.dgvVita.SelectionChanged += new System.EventHandler(this.dgvVita_SelectionChanged);
            // 
            // colIcon
            // 
            this.colIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colIcon.DataPropertyName = "Icon";
            this.colIcon.HeaderText = "Icon";
            this.colIcon.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colIcon.Name = "colIcon";
            this.colIcon.ReadOnly = true;
            this.colIcon.Width = 36;
            // 
            // colSource
            // 
            this.colSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSource.DataPropertyName = "Source";
            this.colSource.HeaderText = "Source";
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Width = 68;
            // 
            // colTitleID
            // 
            this.colTitleID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTitleID.DataPropertyName = "TitleID";
            this.colTitleID.HeaderText = "TitleID";
            this.colTitleID.Name = "colTitleID";
            this.colTitleID.ReadOnly = true;
            this.colTitleID.Width = 66;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 64;
            // 
            // tspVita
            // 
            this.tspVita.BackColor = System.Drawing.SystemColors.Window;
            this.tspVita.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspVita.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnVitaRefresh,
            this.btnVitaFtpToNoNpDrm});
            this.tspVita.Location = new System.Drawing.Point(3, 19);
            this.tspVita.Name = "tspVita";
            this.tspVita.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tspVita.Size = new System.Drawing.Size(796, 25);
            this.tspVita.TabIndex = 7;
            // 
            // btnVitaRefresh
            // 
            this.btnVitaRefresh.Image = global::RomPackTool.WinForms.Properties.Resources.refresh;
            this.btnVitaRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVitaRefresh.Name = "btnVitaRefresh";
            this.btnVitaRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnVitaRefresh.Text = "Refresh";
            this.btnVitaRefresh.ToolTipText = "Refresh Vita List";
            this.btnVitaRefresh.Click += new System.EventHandler(this.btnVitaRefresh_Click);
            // 
            // btnVitaFtpToNoNpDrm
            // 
            this.btnVitaFtpToNoNpDrm.Enabled = false;
            this.btnVitaFtpToNoNpDrm.Image = global::RomPackTool.WinForms.Properties.Resources.download;
            this.btnVitaFtpToNoNpDrm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVitaFtpToNoNpDrm.Name = "btnVitaFtpToNoNpDrm";
            this.btnVitaFtpToNoNpDrm.Size = new System.Drawing.Size(132, 22);
            this.btnVitaFtpToNoNpDrm.Text = "Dump to NoNpDrm";
            this.btnVitaFtpToNoNpDrm.Click += new System.EventHandler(this.btnVitaFtpToNoNpDrm_Click);
            // 
            // tabPSV
            // 
            this.tabPSV.Controls.Add(this.groupBox3);
            this.tabPSV.Location = new System.Drawing.Point(4, 24);
            this.tabPSV.Name = "tabPSV";
            this.tabPSV.Padding = new System.Windows.Forms.Padding(3);
            this.tabPSV.Size = new System.Drawing.Size(814, 657);
            this.tabPSV.TabIndex = 3;
            this.tabPSV.Text = "PSV";
            this.tabPSV.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnVitaExtractNoNpDrm);
            this.groupBox3.Controls.Add(this.btnVitaStripPSV);
            this.groupBox3.Controls.Add(this.btnVitaCreateNoNpDrmPSV);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(654, 262);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PSV Tools";
            // 
            // btnVitaExtractNoNpDrm
            // 
            this.btnVitaExtractNoNpDrm.Location = new System.Drawing.Point(11, 126);
            this.btnVitaExtractNoNpDrm.Name = "btnVitaExtractNoNpDrm";
            this.btnVitaExtractNoNpDrm.Size = new System.Drawing.Size(229, 46);
            this.btnVitaExtractNoNpDrm.TabIndex = 10;
            this.btnVitaExtractNoNpDrm.Text = "Extract NoNpDrm from PSV";
            this.btnVitaExtractNoNpDrm.UseVisualStyleBackColor = true;
            this.btnVitaExtractNoNpDrm.Click += new System.EventHandler(this.btnExtractNoNpDrm_Click);
            // 
            // btnVitaStripPSV
            // 
            this.btnVitaStripPSV.Enabled = false;
            this.btnVitaStripPSV.Location = new System.Drawing.Point(11, 22);
            this.btnVitaStripPSV.Name = "btnVitaStripPSV";
            this.btnVitaStripPSV.Size = new System.Drawing.Size(229, 46);
            this.btnVitaStripPSV.TabIndex = 9;
            this.btnVitaStripPSV.Text = "Strip PSV";
            this.btnVitaStripPSV.UseVisualStyleBackColor = true;
            this.btnVitaStripPSV.Click += new System.EventHandler(this.btnVitaStripPSV_Click);
            // 
            // btnVitaCreateNoNpDrmPSV
            // 
            this.btnVitaCreateNoNpDrmPSV.Location = new System.Drawing.Point(11, 74);
            this.btnVitaCreateNoNpDrmPSV.Name = "btnVitaCreateNoNpDrmPSV";
            this.btnVitaCreateNoNpDrmPSV.Size = new System.Drawing.Size(229, 46);
            this.btnVitaCreateNoNpDrmPSV.TabIndex = 7;
            this.btnVitaCreateNoNpDrmPSV.Text = "Create NoNpDrm PSV";
            this.btnVitaCreateNoNpDrmPSV.UseVisualStyleBackColor = true;
            this.btnVitaCreateNoNpDrmPSV.Click += new System.EventHandler(this.btnCreateNoNpDrmPSV_Click);
            // 
            // tabRomPack
            // 
            this.tabRomPack.Controls.Add(this.groupBox2);
            this.tabRomPack.Location = new System.Drawing.Point(4, 24);
            this.tabRomPack.Name = "tabRomPack";
            this.tabRomPack.Padding = new System.Windows.Forms.Padding(3);
            this.tabRomPack.Size = new System.Drawing.Size(814, 657);
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
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 24);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(5);
            this.tabLog.Size = new System.Drawing.Size(814, 657);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // lblDivider
            // 
            this.lblDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDivider.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDivider.Location = new System.Drawing.Point(832, 0);
            this.lblDivider.Name = "lblDivider";
            this.lblDivider.Size = new System.Drawing.Size(2, 695);
            this.lblDivider.TabIndex = 2;
            // 
            // flpProcesses
            // 
            this.flpProcesses.AutoScroll = true;
            this.flpProcesses.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpProcesses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProcesses.Location = new System.Drawing.Point(834, 0);
            this.flpProcesses.Name = "flpProcesses";
            this.flpProcesses.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.flpProcesses.Size = new System.Drawing.Size(291, 695);
            this.flpProcesses.TabIndex = 0;
            this.flpProcesses.WrapContents = false;
            // 
            // stsMain
            // 
            this.stsMain.Location = new System.Drawing.Point(0, 695);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(1125, 22);
            this.stsMain.TabIndex = 9;
            this.stsMain.Text = "statusStrip1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Dumps will be extracted to your Vita Sync Path";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 717);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.stsMain);
            this.Name = "Main";
            this.Text = "RomPackTool - Alpha 7";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tbsMain.ResumeLayout(false);
            this.tabVita.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVita)).EndInit();
            this.tspVita.ResumeLayout(false);
            this.tspVita.PerformLayout();
            this.tabPSV.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabRomPack.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtVitaIp;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuildRomPack;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnVitaCreateNoNpDrmPSV;
        private System.Windows.Forms.Button btnBrowseVitaDumps;
        private System.Windows.Forms.TextBox txtVitaSyncPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpProcesses;
        private System.Windows.Forms.TabControl tbsMain;
        private System.Windows.Forms.TabPage tabVita;
        private System.Windows.Forms.TabPage tabRomPack;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabPSV;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.Label lblDivider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvVita;
        private System.Windows.Forms.ToolStrip tspVita;
        private System.Windows.Forms.ToolStripButton btnVitaFtpToNoNpDrm;
        private System.Windows.Forms.ToolStripButton btnVitaRefresh;
        private System.Windows.Forms.Button btnVitaStripPSV;
        private System.Windows.Forms.DataGridViewImageColumn colIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.Button btnVitaExtractNoNpDrm;
        private System.Windows.Forms.Label label2;
    }
}

