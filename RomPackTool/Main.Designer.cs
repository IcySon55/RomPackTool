
namespace RomPackTool
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseVitaDumps = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateNoIntroPSV = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuildRomPack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(400, 444);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(480, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(400, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(480, 426);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(136, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(149, 23);
            this.textBox2.TabIndex = 3;
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
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 138);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PlayStation Vita/TV";
            // 
            // btnBrowseVitaDumps
            // 
            this.btnBrowseVitaDumps.Location = new System.Drawing.Point(291, 102);
            this.btnBrowseVitaDumps.Name = "btnBrowseVitaDumps";
            this.btnBrowseVitaDumps.Size = new System.Drawing.Size(85, 23);
            this.btnBrowseVitaDumps.TabIndex = 7;
            this.btnBrowseVitaDumps.Text = "Browse...";
            this.btnBrowseVitaDumps.UseVisualStyleBackColor = true;
            this.btnBrowseVitaDumps.Click += new System.EventHandler(this.btnBrowseVitaDumps_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(87, 103);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(198, 23);
            this.textBox3.TabIndex = 6;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupBox3);
            this.pnlMain.Controls.Add(this.groupBox2);
            this.pnlMain.Controls.Add(this.textBox1);
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Controls.Add(this.progressBar1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(892, 479);
            this.pnlMain.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreateNoIntroPSV);
            this.groupBox3.Location = new System.Drawing.Point(12, 356);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 82);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PSV Tools";
            // 
            // btnCreateNoIntroPSV
            // 
            this.btnCreateNoIntroPSV.Location = new System.Drawing.Point(87, 22);
            this.btnCreateNoIntroPSV.Name = "btnCreateNoIntroPSV";
            this.btnCreateNoIntroPSV.Size = new System.Drawing.Size(198, 46);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dump Path:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 479);
            this.Controls.Add(this.pnlMain);
            this.Name = "Main";
            this.Text = "RomPackTool - Alpha 4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
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
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
    }
}

