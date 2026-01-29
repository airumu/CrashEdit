using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class ModelConverterForm
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
            cmdOpen = new DarkButton();
            cmdConvert = new DarkButton();
            fraSettings = new DarkGroupBox();
            darkGroupBox2 = new DarkGroupBox();
            lblModelEID = new Label();
            lblAnimEID = new Label();
            lblTpage = new Label();
            txtModelEID = new DarkTextBox();
            txtAnimEID = new DarkTextBox();
            txtTpageEID = new DarkTextBox();
            cmdSetExportPath = new DarkButton();
            label2 = new Label();
            lblExportPath = new Label();
            cmdSaveSettings = new DarkButton();
            fraScaleFactor = new DarkGroupBox();
            lblScaleFX = new Label();
            numScaleFZ = new DarkNumericUpDown();
            numScaleFY = new DarkNumericUpDown();
            numScaleFX = new DarkNumericUpDown();
            lblScaleFY = new Label();
            lblScaleFZ = new Label();
            darkGroupBox1 = new DarkGroupBox();
            chkHex = new CheckBox();
            lblScaleX = new Label();
            numScaleX = new DarkNumericUpDown();
            numScaleY = new DarkNumericUpDown();
            numScaleZ = new DarkNumericUpDown();
            lblScaleY = new Label();
            lblScaleZ = new Label();
            lblPath = new Label();
            label1 = new Label();
            chkDebug = new CheckBox();
            fraSettings.SuspendLayout();
            darkGroupBox2.SuspendLayout();
            fraScaleFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleFZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFX).BeginInit();
            darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).BeginInit();
            SuspendLayout();
            // 
            // cmdOpen
            // 
            cmdOpen.BorderColour = Color.Empty;
            cmdOpen.CustomColour = false;
            cmdOpen.FlatBottom = false;
            cmdOpen.FlatTop = false;
            cmdOpen.Location = new Point(12, 12);
            cmdOpen.Name = "cmdOpen";
            cmdOpen.Padding = new Padding(5);
            cmdOpen.Size = new Size(75, 28);
            cmdOpen.TabIndex = 0;
            cmdOpen.Text = "Open";
            cmdOpen.Click += cmdOpen_Click;
            // 
            // cmdConvert
            // 
            cmdConvert.BorderColour = Color.Empty;
            cmdConvert.CustomColour = false;
            cmdConvert.Enabled = false;
            cmdConvert.FlatBottom = false;
            cmdConvert.FlatTop = false;
            cmdConvert.Location = new Point(12, 348);
            cmdConvert.Name = "cmdConvert";
            cmdConvert.Padding = new Padding(5);
            cmdConvert.Size = new Size(414, 40);
            cmdConvert.TabIndex = 0;
            cmdConvert.Text = "Convert";
            cmdConvert.Click += cmdConvert_Click;
            // 
            // fraSettings
            // 
            fraSettings.BackColor = Color.Transparent;
            fraSettings.Controls.Add(darkGroupBox2);
            fraSettings.Controls.Add(cmdSetExportPath);
            fraSettings.Controls.Add(label2);
            fraSettings.Controls.Add(lblExportPath);
            fraSettings.Controls.Add(cmdSaveSettings);
            fraSettings.Controls.Add(fraScaleFactor);
            fraSettings.Controls.Add(darkGroupBox1);
            fraSettings.Enabled = false;
            fraSettings.Location = new Point(12, 61);
            fraSettings.Name = "fraSettings";
            fraSettings.Size = new Size(414, 256);
            fraSettings.TabIndex = 2;
            fraSettings.TabStop = false;
            fraSettings.Text = "Settings";
            // 
            // darkGroupBox2
            // 
            darkGroupBox2.Controls.Add(lblModelEID);
            darkGroupBox2.Controls.Add(lblAnimEID);
            darkGroupBox2.Controls.Add(lblTpage);
            darkGroupBox2.Controls.Add(txtModelEID);
            darkGroupBox2.Controls.Add(txtAnimEID);
            darkGroupBox2.Controls.Add(txtTpageEID);
            darkGroupBox2.Location = new Point(6, 16);
            darkGroupBox2.Name = "darkGroupBox2";
            darkGroupBox2.Size = new Size(142, 132);
            darkGroupBox2.TabIndex = 9;
            darkGroupBox2.TabStop = false;
            darkGroupBox2.Text = "EIDs";
            // 
            // lblModelEID
            // 
            lblModelEID.AutoSize = true;
            lblModelEID.BackColor = Color.Transparent;
            lblModelEID.Location = new Point(6, 27);
            lblModelEID.Name = "lblModelEID";
            lblModelEID.Size = new Size(41, 15);
            lblModelEID.TabIndex = 3;
            lblModelEID.Text = "Model";
            // 
            // lblAnimEID
            // 
            lblAnimEID.AutoSize = true;
            lblAnimEID.BackColor = Color.Transparent;
            lblAnimEID.Location = new Point(6, 56);
            lblAnimEID.Name = "lblAnimEID";
            lblAnimEID.Size = new Size(63, 15);
            lblAnimEID.TabIndex = 3;
            lblAnimEID.Text = "Animation";
            // 
            // lblTpage
            // 
            lblTpage.AutoSize = true;
            lblTpage.BackColor = Color.Transparent;
            lblTpage.Location = new Point(6, 86);
            lblTpage.Name = "lblTpage";
            lblTpage.Size = new Size(38, 15);
            lblTpage.TabIndex = 3;
            lblTpage.Text = "Tpage";
            // 
            // txtModelEID
            // 
            txtModelEID.BackColor = Color.FromArgb(26, 26, 28);
            txtModelEID.BorderStyle = BorderStyle.FixedSingle;
            txtModelEID.ForeColor = Color.FromArgb(213, 213, 213);
            txtModelEID.Location = new Point(82, 24);
            txtModelEID.MaxLength = 5;
            txtModelEID.Name = "txtModelEID";
            txtModelEID.Size = new Size(48, 23);
            txtModelEID.TabIndex = 4;
            txtModelEID.Text = "0000G";
            txtModelEID.Validating += EID_Validating;
            // 
            // txtAnimEID
            // 
            txtAnimEID.BackColor = Color.FromArgb(26, 26, 28);
            txtAnimEID.BorderStyle = BorderStyle.FixedSingle;
            txtAnimEID.ForeColor = Color.FromArgb(213, 213, 213);
            txtAnimEID.Location = new Point(82, 54);
            txtAnimEID.MaxLength = 5;
            txtAnimEID.Name = "txtAnimEID";
            txtAnimEID.Size = new Size(48, 23);
            txtAnimEID.TabIndex = 4;
            txtAnimEID.Text = "0000V";
            txtAnimEID.Validating += EID_Validating;
            // 
            // txtTpageEID
            // 
            txtTpageEID.BackColor = Color.FromArgb(26, 26, 28);
            txtTpageEID.BorderStyle = BorderStyle.FixedSingle;
            txtTpageEID.ForeColor = Color.FromArgb(213, 213, 213);
            txtTpageEID.Location = new Point(82, 83);
            txtTpageEID.MaxLength = 5;
            txtTpageEID.Name = "txtTpageEID";
            txtTpageEID.Size = new Size(48, 23);
            txtTpageEID.TabIndex = 4;
            txtTpageEID.Text = "Z000T";
            txtTpageEID.Validating += EID_Validating;
            // 
            // cmdSetExportPath
            // 
            cmdSetExportPath.BorderColour = Color.Empty;
            cmdSetExportPath.CustomColour = false;
            cmdSetExportPath.FlatBottom = false;
            cmdSetExportPath.FlatTop = false;
            cmdSetExportPath.Location = new Point(80, 152);
            cmdSetExportPath.Name = "cmdSetExportPath";
            cmdSetExportPath.Padding = new Padding(5);
            cmdSetExportPath.Size = new Size(24, 24);
            cmdSetExportPath.TabIndex = 8;
            cmdSetExportPath.Click += cmdSetExportPath_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 156);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Export Path:";
            // 
            // lblExportPath
            // 
            lblExportPath.BackColor = Color.Transparent;
            lblExportPath.Location = new Point(6, 178);
            lblExportPath.Name = "lblExportPath";
            lblExportPath.Size = new Size(401, 30);
            lblExportPath.TabIndex = 3;
            lblExportPath.Text = "PATH";
            // 
            // cmdSaveSettings
            // 
            cmdSaveSettings.BorderColour = Color.Empty;
            cmdSaveSettings.CustomColour = false;
            cmdSaveSettings.FlatBottom = false;
            cmdSaveSettings.FlatTop = false;
            cmdSaveSettings.Location = new Point(311, 220);
            cmdSaveSettings.Name = "cmdSaveSettings";
            cmdSaveSettings.Padding = new Padding(5);
            cmdSaveSettings.Size = new Size(96, 28);
            cmdSaveSettings.TabIndex = 7;
            cmdSaveSettings.Text = "Save Settings";
            cmdSaveSettings.Click += cmdSaveSettings_Click;
            // 
            // fraScaleFactor
            // 
            fraScaleFactor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraScaleFactor.Controls.Add(lblScaleFX);
            fraScaleFactor.Controls.Add(numScaleFZ);
            fraScaleFactor.Controls.Add(numScaleFY);
            fraScaleFactor.Controls.Add(numScaleFX);
            fraScaleFactor.Controls.Add(lblScaleFY);
            fraScaleFactor.Controls.Add(lblScaleFZ);
            fraScaleFactor.Location = new Point(154, 16);
            fraScaleFactor.Name = "fraScaleFactor";
            fraScaleFactor.Size = new Size(115, 132);
            fraScaleFactor.TabIndex = 6;
            fraScaleFactor.TabStop = false;
            fraScaleFactor.Text = "Scale Factors";
            // 
            // lblScaleFX
            // 
            lblScaleFX.AutoSize = true;
            lblScaleFX.BackColor = Color.Transparent;
            lblScaleFX.Location = new Point(10, 26);
            lblScaleFX.Name = "lblScaleFX";
            lblScaleFX.Size = new Size(14, 15);
            lblScaleFX.TabIndex = 3;
            lblScaleFX.Text = "X";
            // 
            // numScaleFZ
            // 
            numScaleFZ.DecimalPlaces = 1;
            numScaleFZ.Location = new Point(42, 80);
            numScaleFZ.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFZ.Name = "numScaleFZ";
            numScaleFZ.Size = new Size(64, 23);
            numScaleFZ.TabIndex = 0;
            numScaleFZ.Value = new decimal(new int[] { 127, 0, 0, 0 });
            // 
            // numScaleFY
            // 
            numScaleFY.DecimalPlaces = 1;
            numScaleFY.Location = new Point(42, 51);
            numScaleFY.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFY.Name = "numScaleFY";
            numScaleFY.Size = new Size(64, 23);
            numScaleFY.TabIndex = 0;
            numScaleFY.Value = new decimal(new int[] { 127, 0, 0, 0 });
            // 
            // numScaleFX
            // 
            numScaleFX.DecimalPlaces = 1;
            numScaleFX.Location = new Point(42, 22);
            numScaleFX.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFX.Name = "numScaleFX";
            numScaleFX.Size = new Size(64, 23);
            numScaleFX.TabIndex = 0;
            numScaleFX.Value = new decimal(new int[] { 127, 0, 0, 0 });
            // 
            // lblScaleFY
            // 
            lblScaleFY.AutoSize = true;
            lblScaleFY.BackColor = Color.Transparent;
            lblScaleFY.Location = new Point(10, 55);
            lblScaleFY.Name = "lblScaleFY";
            lblScaleFY.Size = new Size(14, 15);
            lblScaleFY.TabIndex = 3;
            lblScaleFY.Text = "Y";
            // 
            // lblScaleFZ
            // 
            lblScaleFZ.AutoSize = true;
            lblScaleFZ.BackColor = Color.Transparent;
            lblScaleFZ.Location = new Point(10, 84);
            lblScaleFZ.Name = "lblScaleFZ";
            lblScaleFZ.Size = new Size(14, 15);
            lblScaleFZ.TabIndex = 3;
            lblScaleFZ.Text = "Z";
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox1.Controls.Add(chkHex);
            darkGroupBox1.Controls.Add(lblScaleX);
            darkGroupBox1.Controls.Add(numScaleX);
            darkGroupBox1.Controls.Add(numScaleY);
            darkGroupBox1.Controls.Add(numScaleZ);
            darkGroupBox1.Controls.Add(lblScaleY);
            darkGroupBox1.Controls.Add(lblScaleZ);
            darkGroupBox1.Location = new Point(275, 16);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(132, 132);
            darkGroupBox1.TabIndex = 6;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Model Scale";
            // 
            // chkHex
            // 
            chkHex.AutoSize = true;
            chkHex.Checked = true;
            chkHex.CheckState = CheckState.Checked;
            chkHex.Location = new Point(62, 109);
            chkHex.Name = "chkHex";
            chkHex.Size = new Size(47, 19);
            chkHex.TabIndex = 5;
            chkHex.Text = "Hex";
            chkHex.UseVisualStyleBackColor = true;
            chkHex.CheckedChanged += chkHex_CheckedChanged;
            // 
            // lblScaleX
            // 
            lblScaleX.AutoSize = true;
            lblScaleX.BackColor = Color.Transparent;
            lblScaleX.Location = new Point(12, 26);
            lblScaleX.Name = "lblScaleX";
            lblScaleX.Size = new Size(44, 15);
            lblScaleX.TabIndex = 3;
            lblScaleX.Text = "Scale X";
            // 
            // numScaleX
            // 
            numScaleX.Hexadecimal = true;
            numScaleX.Location = new Point(62, 22);
            numScaleX.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleX.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleX.Name = "numScaleX";
            numScaleX.Size = new Size(64, 23);
            numScaleX.TabIndex = 0;
            numScaleX.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            // 
            // numScaleY
            // 
            numScaleY.Hexadecimal = true;
            numScaleY.Location = new Point(62, 51);
            numScaleY.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleY.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleY.Name = "numScaleY";
            numScaleY.Size = new Size(64, 23);
            numScaleY.TabIndex = 0;
            numScaleY.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            // 
            // numScaleZ
            // 
            numScaleZ.Hexadecimal = true;
            numScaleZ.Location = new Point(62, 80);
            numScaleZ.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleZ.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleZ.Name = "numScaleZ";
            numScaleZ.Size = new Size(64, 23);
            numScaleZ.TabIndex = 0;
            numScaleZ.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            // 
            // lblScaleY
            // 
            lblScaleY.AutoSize = true;
            lblScaleY.BackColor = Color.Transparent;
            lblScaleY.Location = new Point(12, 55);
            lblScaleY.Name = "lblScaleY";
            lblScaleY.Size = new Size(44, 15);
            lblScaleY.TabIndex = 3;
            lblScaleY.Text = "Scale Y";
            // 
            // lblScaleZ
            // 
            lblScaleZ.AutoSize = true;
            lblScaleZ.BackColor = Color.Transparent;
            lblScaleZ.Location = new Point(12, 84);
            lblScaleZ.Name = "lblScaleZ";
            lblScaleZ.Size = new Size(44, 15);
            lblScaleZ.TabIndex = 3;
            lblScaleZ.Text = "Scale Z";
            // 
            // lblPath
            // 
            lblPath.BackColor = Color.Transparent;
            lblPath.Location = new Point(93, 28);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(326, 30);
            lblPath.TabIndex = 3;
            lblPath.Text = "PATH";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(93, 8);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 3;
            label1.Text = "Raw Model File:";
            // 
            // chkDebug
            // 
            chkDebug.AutoSize = true;
            chkDebug.Enabled = false;
            chkDebug.Location = new Point(18, 323);
            chkDebug.Name = "chkDebug";
            chkDebug.Size = new Size(107, 19);
            chkDebug.TabIndex = 5;
            chkDebug.Text = "Log debug info";
            chkDebug.UseVisualStyleBackColor = true;
            chkDebug.CheckedChanged += chkDebug_CheckedChanged;
            // 
            // ModelConverterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 402);
            Controls.Add(chkDebug);
            Controls.Add(label1);
            Controls.Add(lblPath);
            Controls.Add(cmdConvert);
            Controls.Add(fraSettings);
            Controls.Add(cmdOpen);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ModelConverterForm";
            Text = "Model Converter";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraSettings.ResumeLayout(false);
            fraSettings.PerformLayout();
            darkGroupBox2.ResumeLayout(false);
            darkGroupBox2.PerformLayout();
            fraScaleFactor.ResumeLayout(false);
            fraScaleFactor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleFZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFX).EndInit();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkButton cmdOpen;
        private DarkButton cmdConvert;
        private DarkGroupBox fraSettings;
        private Label lblPath;
        private Label lblScaleZ;
        private Label lblModelEID;
        private DarkTextBox txtAnimEID;
        private DarkTextBox txtModelEID;
        private Label lblAnimEID;
        private DarkTextBox txtTpageEID;
        private Label lblTpage;
        private Label lblScaleX;
        private DarkNumericUpDown numScaleX;
        private CheckBox chkHex;
        private Label lblScaleY;
        private DarkNumericUpDown numScaleZ;
        private DarkNumericUpDown numScaleY;
        private DarkGroupBox darkGroupBox1;
        private DarkButton cmdSaveSettings;
        private Label label1;
        private Label label2;
        private Label lblExportPath;
        private DarkButton cmdSetExportPath;
        private CheckBox chkDebug;
        private DarkGroupBox fraScaleFactor;
        private Label lblScaleFX;
        private DarkNumericUpDown numScaleFX;
        private Label lblScaleFY;
        private Label lblScaleFZ;
        private DarkNumericUpDown numScaleFZ;
        private DarkNumericUpDown numScaleFY;
        private DarkGroupBox darkGroupBox2;
    }
}