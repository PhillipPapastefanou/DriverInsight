namespace GridlistCFConverter
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxConverter = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.textBoxConLat = new System.Windows.Forms.TextBox();
            this.textBoxConLon = new System.Windows.Forms.TextBox();
            this.groupBoxFileInfo = new System.Windows.Forms.GroupBox();
            this.textBoxResolution = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxReduced = new System.Windows.Forms.CheckBox();
            this.textBoxLat = new System.Windows.Forms.TextBox();
            this.textBoxLon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDriverInput = new System.Windows.Forms.GroupBox();
            this.labelFilename = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialogDriver = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.groupBoxConverter.SuspendLayout();
            this.groupBoxFileInfo.SuspendLayout();
            this.groupBoxDriverInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBoxConverter);
            this.panel1.Controls.Add(this.groupBoxFileInfo);
            this.panel1.Controls.Add(this.groupBoxDriverInput);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(782, 869);
            this.panel1.TabIndex = 0;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.BackgroundImage = global::GridlistCFConverter.Properties.Resources.WorldMap;
            this.pictureBoxDisplay.InitialImage = null;
            this.pictureBoxDisplay.Location = new System.Drawing.Point(12, 36);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(720, 360);
            this.pictureBoxDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDisplay.TabIndex = 6;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseClick);
            this.pictureBoxDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Myriad Pro", 20.2F);
            this.label7.Location = new System.Drawing.Point(144, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(378, 41);
            this.label7.TabIndex = 5;
            this.label7.Text = "LPJ-GUESS gridlist creator";
            // 
            // groupBoxConverter
            // 
            this.groupBoxConverter.Controls.Add(this.label5);
            this.groupBoxConverter.Controls.Add(this.label4);
            this.groupBoxConverter.Controls.Add(this.textBoxResult);
            this.groupBoxConverter.Controls.Add(this.buttonConvert);
            this.groupBoxConverter.Controls.Add(this.textBoxConLat);
            this.groupBoxConverter.Controls.Add(this.textBoxConLon);
            this.groupBoxConverter.Font = new System.Drawing.Font("Myriad Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxConverter.Location = new System.Drawing.Point(358, 209);
            this.groupBoxConverter.Name = "groupBoxConverter";
            this.groupBoxConverter.Size = new System.Drawing.Size(410, 213);
            this.groupBoxConverter.TabIndex = 4;
            this.groupBoxConverter.TabStop = false;
            this.groupBoxConverter.Text = "Convert Coordinate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Latitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Longitude:";
            // 
            // textBoxResult
            // 
            this.textBoxResult.Font = new System.Drawing.Font("Myriad Pro", 16F);
            this.textBoxResult.Location = new System.Drawing.Point(17, 158);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(185, 39);
            this.textBoxResult.TabIndex = 3;
            this.textBoxResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxResult_MouseClick);
            // 
            // buttonConvert
            // 
            this.buttonConvert.BackColor = System.Drawing.Color.RoyalBlue;
            this.buttonConvert.Font = new System.Drawing.Font("Myriad Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConvert.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonConvert.Location = new System.Drawing.Point(238, 40);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(144, 88);
            this.buttonConvert.TabIndex = 2;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = false;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // textBoxConLat
            // 
            this.textBoxConLat.Location = new System.Drawing.Point(128, 97);
            this.textBoxConLat.Name = "textBoxConLat";
            this.textBoxConLat.Size = new System.Drawing.Size(74, 31);
            this.textBoxConLat.TabIndex = 1;
            // 
            // textBoxConLon
            // 
            this.textBoxConLon.Location = new System.Drawing.Point(128, 46);
            this.textBoxConLon.Name = "textBoxConLon";
            this.textBoxConLon.Size = new System.Drawing.Size(74, 31);
            this.textBoxConLon.TabIndex = 0;
            // 
            // groupBoxFileInfo
            // 
            this.groupBoxFileInfo.Controls.Add(this.textBoxResolution);
            this.groupBoxFileInfo.Controls.Add(this.label6);
            this.groupBoxFileInfo.Controls.Add(this.label3);
            this.groupBoxFileInfo.Controls.Add(this.checkBoxReduced);
            this.groupBoxFileInfo.Controls.Add(this.textBoxLat);
            this.groupBoxFileInfo.Controls.Add(this.textBoxLon);
            this.groupBoxFileInfo.Controls.Add(this.label2);
            this.groupBoxFileInfo.Controls.Add(this.label1);
            this.groupBoxFileInfo.Font = new System.Drawing.Font("Myriad Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFileInfo.Location = new System.Drawing.Point(20, 209);
            this.groupBoxFileInfo.Name = "groupBoxFileInfo";
            this.groupBoxFileInfo.Size = new System.Drawing.Size(307, 213);
            this.groupBoxFileInfo.TabIndex = 3;
            this.groupBoxFileInfo.TabStop = false;
            this.groupBoxFileInfo.Text = "File Info";
            // 
            // textBoxResolution
            // 
            this.textBoxResolution.Location = new System.Drawing.Point(131, 166);
            this.textBoxResolution.Name = "textBoxResolution";
            this.textBoxResolution.ReadOnly = true;
            this.textBoxResolution.Size = new System.Drawing.Size(100, 31);
            this.textBoxResolution.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 24);
            this.label6.TabIndex = 6;
            this.label6.Text = "Resolution:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Orthogonal:";
            // 
            // checkBoxReduced
            // 
            this.checkBoxReduced.AutoSize = true;
            this.checkBoxReduced.Cursor = System.Windows.Forms.Cursors.Cross;
            this.checkBoxReduced.Enabled = false;
            this.checkBoxReduced.Location = new System.Drawing.Point(140, 31);
            this.checkBoxReduced.Name = "checkBoxReduced";
            this.checkBoxReduced.Size = new System.Drawing.Size(36, 28);
            this.checkBoxReduced.TabIndex = 4;
            this.checkBoxReduced.Text = " ";
            this.checkBoxReduced.UseVisualStyleBackColor = true;
            this.checkBoxReduced.CheckedChanged += new System.EventHandler(this.checkBoxReduced_CheckedChanged);
            // 
            // textBoxLat
            // 
            this.textBoxLat.Location = new System.Drawing.Point(131, 121);
            this.textBoxLat.Name = "textBoxLat";
            this.textBoxLat.ReadOnly = true;
            this.textBoxLat.Size = new System.Drawing.Size(153, 31);
            this.textBoxLat.TabIndex = 3;
            // 
            // textBoxLon
            // 
            this.textBoxLon.Location = new System.Drawing.Point(131, 77);
            this.textBoxLon.Name = "textBoxLon";
            this.textBoxLon.ReadOnly = true;
            this.textBoxLon.Size = new System.Drawing.Size(153, 31);
            this.textBoxLon.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Longitude:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBoxDriverInput
            // 
            this.groupBoxDriverInput.Controls.Add(this.labelFilename);
            this.groupBoxDriverInput.Controls.Add(this.textBox1);
            this.groupBoxDriverInput.Controls.Add(this.buttonOpenFile);
            this.groupBoxDriverInput.Font = new System.Drawing.Font("Myriad Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDriverInput.Location = new System.Drawing.Point(20, 96);
            this.groupBoxDriverInput.Name = "groupBoxDriverInput";
            this.groupBoxDriverInput.Size = new System.Drawing.Size(748, 86);
            this.groupBoxDriverInput.TabIndex = 2;
            this.groupBoxDriverInput.TabStop = false;
            this.groupBoxDriverInput.Text = "Driver input file";
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.Location = new System.Drawing.Point(106, 43);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(43, 24);
            this.labelFilename.TabIndex = 2;
            this.labelFilename.Text = "File:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(155, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(586, 31);
            this.textBox1.TabIndex = 1;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(13, 35);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 33);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Open";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.White;
            this.buttonClose.BackgroundImage = global::GridlistCFConverter.Properties.Resources._15107_illustration_of_a_red_close_button_pv;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(733, 15);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GridlistCFConverter.Properties.Resources.TUM_Logo_blau_rgb_p;
            this.pictureBox1.Location = new System.Drawing.Point(20, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialogDriver
            // 
            this.openFileDialogDriver.FileName = "openFileDialogDriver";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxDisplay);
            this.groupBox1.Font = new System.Drawing.Font("Myriad Pro", 12F);
            this.groupBox1.Location = new System.Drawing.Point(20, 443);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 412);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "World map";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(784, 871);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Coordinate Converter";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.groupBoxConverter.ResumeLayout(false);
            this.groupBoxConverter.PerformLayout();
            this.groupBoxFileInfo.ResumeLayout(false);
            this.groupBoxFileInfo.PerformLayout();
            this.groupBoxDriverInput.ResumeLayout(false);
            this.groupBoxDriverInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxDriverInput;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogDriver;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBoxFileInfo;
        private System.Windows.Forms.TextBox textBoxLat;
        private System.Windows.Forms.TextBox textBoxLon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBoxConverter;
        private System.Windows.Forms.TextBox textBoxConLat;
        private System.Windows.Forms.TextBox textBoxConLon;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxReduced;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxResolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

