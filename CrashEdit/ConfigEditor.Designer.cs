namespace CrashEdit
{
    partial class ConfigEditor
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
            this.fraLang = new System.Windows.Forms.GroupBox();
            this.dpdLang = new DarkUI.Controls.DarkComboBox();
            this.fraSize = new System.Windows.Forms.GroupBox();
            this.lblH = new System.Windows.Forms.Label();
            this.lblW = new System.Windows.Forms.Label();
            this.numH = new DarkUI.Controls.DarkNumericUpDown();
            this.numW = new DarkUI.Controls.DarkNumericUpDown();
            this.cdlClearCol = new System.Windows.Forms.ColorDialog();
            this.fraClearCol = new System.Windows.Forms.GroupBox();
            this.picClearCol = new System.Windows.Forms.PictureBox();
            this.cmdClearCol = new DarkUI.Controls.DarkButton();
            this.fraAnimGrid = new System.Windows.Forms.GroupBox();
            this.numAnimGrid = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAnimGrid = new System.Windows.Forms.Label();
            this.chkAnimGrid = new DarkUI.Controls.DarkCheckBox();
            this.chkOldPatchNSD = new DarkUI.Controls.DarkCheckBox();
            this.chkPatchNSDSavesNSF = new DarkUI.Controls.DarkCheckBox();
            this.chkDeleteInvalidEntries = new DarkUI.Controls.DarkCheckBox();
            this.chkUseAnimLinks = new DarkUI.Controls.DarkCheckBox();
            this.chkCollisionDisplay = new DarkUI.Controls.DarkCheckBox();
            this.chkNormalDisplay = new DarkUI.Controls.DarkCheckBox();
            this.cmdReset = new DarkUI.Controls.DarkButton();
            this.fraLang.SuspendLayout();
            this.fraSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW)).BeginInit();
            this.fraClearCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClearCol)).BeginInit();
            this.fraAnimGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // fraLang
            // 
            this.fraLang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraLang.Controls.Add(this.dpdLang);
            this.fraLang.ForeColor = System.Drawing.SystemColors.Window;
            this.fraLang.Location = new System.Drawing.Point(3, 3);
            this.fraLang.Name = "fraLang";
            this.fraLang.Size = new System.Drawing.Size(187, 45);
            this.fraLang.TabIndex = 1;
            this.fraLang.TabStop = false;
            this.fraLang.Text = "Language (requires restart)";
            // 
            // dpdLang
            // 
            this.dpdLang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.dpdLang.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.dpdLang.FormattingEnabled = true;
            this.dpdLang.Location = new System.Drawing.Point(6, 18);
            this.dpdLang.Name = "dpdLang";
            this.dpdLang.Size = new System.Drawing.Size(175, 20);
            this.dpdLang.TabIndex = 0;
            // 
            // fraSize
            // 
            this.fraSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSize.Controls.Add(this.lblH);
            this.fraSize.Controls.Add(this.lblW);
            this.fraSize.Controls.Add(this.numH);
            this.fraSize.Controls.Add(this.numW);
            this.fraSize.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSize.Location = new System.Drawing.Point(3, 54);
            this.fraSize.Name = "fraSize";
            this.fraSize.Size = new System.Drawing.Size(131, 68);
            this.fraSize.TabIndex = 1;
            this.fraSize.TabStop = false;
            this.fraSize.Text = "Default Window Size";
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.Location = new System.Drawing.Point(6, 43);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(38, 12);
            this.lblH.TabIndex = 3;
            this.lblH.Text = "Height";
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(6, 19);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(33, 12);
            this.lblW.TabIndex = 2;
            this.lblW.Text = "Width";
            // 
            // numH
            // 
            this.numH.Location = new System.Drawing.Point(50, 42);
            this.numH.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.numH.Minimum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.numH.Name = "numH";
            this.numH.Size = new System.Drawing.Size(75, 19);
            this.numH.TabIndex = 1;
            this.numH.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.numH.ValueChanged += new System.EventHandler(this.numH_ValueChanged);
            // 
            // numW
            // 
            this.numW.Location = new System.Drawing.Point(50, 18);
            this.numW.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numW.Minimum = new decimal(new int[] {
            640,
            0,
            0,
            0});
            this.numW.Name = "numW";
            this.numW.Size = new System.Drawing.Size(75, 19);
            this.numW.TabIndex = 0;
            this.numW.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            this.numW.ValueChanged += new System.EventHandler(this.numW_ValueChanged);
            // 
            // cdlClearCol
            // 
            this.cdlClearCol.AnyColor = true;
            this.cdlClearCol.FullOpen = true;
            this.cdlClearCol.SolidColorOnly = true;
            // 
            // fraClearCol
            // 
            this.fraClearCol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraClearCol.Controls.Add(this.picClearCol);
            this.fraClearCol.Controls.Add(this.cmdClearCol);
            this.fraClearCol.ForeColor = System.Drawing.SystemColors.Window;
            this.fraClearCol.Location = new System.Drawing.Point(140, 54);
            this.fraClearCol.Name = "fraClearCol";
            this.fraClearCol.Size = new System.Drawing.Size(128, 68);
            this.fraClearCol.TabIndex = 4;
            this.fraClearCol.TabStop = false;
            this.fraClearCol.Text = "Background Color";
            // 
            // picClearCol
            // 
            this.picClearCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picClearCol.Location = new System.Drawing.Point(6, 18);
            this.picClearCol.Name = "picClearCol";
            this.picClearCol.Size = new System.Drawing.Size(60, 45);
            this.picClearCol.TabIndex = 0;
            this.picClearCol.TabStop = false;
            this.picClearCol.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cmdClearCol
            // 
            this.cmdClearCol.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdClearCol.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmdClearCol.Location = new System.Drawing.Point(73, 39);
            this.cmdClearCol.Name = "cmdClearCol";
            this.cmdClearCol.Padding = new System.Windows.Forms.Padding(5);
            this.cmdClearCol.Size = new System.Drawing.Size(49, 24);
            this.cmdClearCol.TabIndex = 7;
            this.cmdClearCol.Text = "Reset";
            this.cmdClearCol.Click += new System.EventHandler(this.cmdClearCol_Click);
            // 
            // fraAnimGrid
            // 
            this.fraAnimGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraAnimGrid.Controls.Add(this.numAnimGrid);
            this.fraAnimGrid.Controls.Add(this.lblAnimGrid);
            this.fraAnimGrid.Controls.Add(this.chkAnimGrid);
            this.fraAnimGrid.ForeColor = System.Drawing.SystemColors.Window;
            this.fraAnimGrid.Location = new System.Drawing.Point(275, 54);
            this.fraAnimGrid.Name = "fraAnimGrid";
            this.fraAnimGrid.Size = new System.Drawing.Size(184, 68);
            this.fraAnimGrid.TabIndex = 6;
            this.fraAnimGrid.TabStop = false;
            this.fraAnimGrid.Text = "Animation Viewer World Grid";
            // 
            // numAnimGrid
            // 
            this.numAnimGrid.Location = new System.Drawing.Point(56, 39);
            this.numAnimGrid.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numAnimGrid.Name = "numAnimGrid";
            this.numAnimGrid.Size = new System.Drawing.Size(80, 19);
            this.numAnimGrid.TabIndex = 2;
            this.numAnimGrid.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numAnimGrid.ValueChanged += new System.EventHandler(this.numAnimGrid_ValueChanged);
            // 
            // lblAnimGrid
            // 
            this.lblAnimGrid.AutoSize = true;
            this.lblAnimGrid.Location = new System.Drawing.Point(7, 42);
            this.lblAnimGrid.Name = "lblAnimGrid";
            this.lblAnimGrid.Size = new System.Drawing.Size(44, 12);
            this.lblAnimGrid.TabIndex = 1;
            this.lblAnimGrid.Text = "Amount";
            // 
            // chkAnimGrid
            // 
            this.chkAnimGrid.AutoSize = true;
            this.chkAnimGrid.Location = new System.Drawing.Point(6, 18);
            this.chkAnimGrid.Name = "chkAnimGrid";
            this.chkAnimGrid.Size = new System.Drawing.Size(64, 16);
            this.chkAnimGrid.TabIndex = 0;
            this.chkAnimGrid.Text = "Enabled";
            this.chkAnimGrid.CheckedChanged += new System.EventHandler(this.chkAnimGrid_CheckedChanged);
            // 
            // chkOldPatchNSD
            // 
            this.chkOldPatchNSD.AutoSize = true;
            this.chkOldPatchNSD.Checked = true;
            this.chkOldPatchNSD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOldPatchNSD.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.chkOldPatchNSD.Location = new System.Drawing.Point(4, 192);
            this.chkOldPatchNSD.Name = "chkOldPatchNSD";
            this.chkOldPatchNSD.Size = new System.Drawing.Size(332, 16);
            this.chkOldPatchNSD.TabIndex = 9;
            this.chkOldPatchNSD.Text = "(Patch NSD) Use old NSD patching from CrashEdit v0.2.49.0";
            this.chkOldPatchNSD.CheckedChanged += new System.EventHandler(this.ChkOldPatchNSD_CheckedChanged);
            // 
            // chkPatchNSDSavesNSF
            // 
            this.chkPatchNSDSavesNSF.AutoSize = true;
            this.chkPatchNSDSavesNSF.Checked = true;
            this.chkPatchNSDSavesNSF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPatchNSDSavesNSF.Location = new System.Drawing.Point(3, 213);
            this.chkPatchNSDSavesNSF.Name = "chkPatchNSDSavesNSF";
            this.chkPatchNSDSavesNSF.Size = new System.Drawing.Size(285, 16);
            this.chkPatchNSDSavesNSF.TabIndex = 8;
            this.chkPatchNSDSavesNSF.Text = "(Patch NSD) Always save NSF after NSD patching";
            this.chkPatchNSDSavesNSF.CheckedChanged += new System.EventHandler(this.chkPatchNSDSavesNSF_CheckedChanged);
            // 
            // chkDeleteInvalidEntries
            // 
            this.chkDeleteInvalidEntries.AutoSize = true;
            this.chkDeleteInvalidEntries.Checked = true;
            this.chkDeleteInvalidEntries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteInvalidEntries.Location = new System.Drawing.Point(4, 171);
            this.chkDeleteInvalidEntries.Name = "chkDeleteInvalidEntries";
            this.chkDeleteInvalidEntries.Size = new System.Drawing.Size(311, 16);
            this.chkDeleteInvalidEntries.TabIndex = 5;
            this.chkDeleteInvalidEntries.Text = "(Patch NSD) Delete non-existent entries from load lists";
            this.chkDeleteInvalidEntries.CheckedChanged += new System.EventHandler(this.chkDeleteInvalidEntries_CheckedChanged);
            // 
            // chkUseAnimLinks
            // 
            this.chkUseAnimLinks.AutoSize = true;
            this.chkUseAnimLinks.Checked = true;
            this.chkUseAnimLinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseAnimLinks.Location = new System.Drawing.Point(3, 234);
            this.chkUseAnimLinks.Name = "chkUseAnimLinks";
            this.chkUseAnimLinks.Size = new System.Drawing.Size(253, 16);
            this.chkUseAnimLinks.TabIndex = 3;
            this.chkUseAnimLinks.Text = "(Crash 3) Used saved animation-model links";
            this.chkUseAnimLinks.CheckedChanged += new System.EventHandler(this.chkUseAnimLinks_CheckedChanged);
            // 
            // chkCollisionDisplay
            // 
            this.chkCollisionDisplay.AutoSize = true;
            this.chkCollisionDisplay.Checked = true;
            this.chkCollisionDisplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCollisionDisplay.Location = new System.Drawing.Point(4, 150);
            this.chkCollisionDisplay.Name = "chkCollisionDisplay";
            this.chkCollisionDisplay.Size = new System.Drawing.Size(196, 16);
            this.chkCollisionDisplay.TabIndex = 2;
            this.chkCollisionDisplay.Text = "Display frame collision by default";
            this.chkCollisionDisplay.CheckedChanged += new System.EventHandler(this.chkCollisionDisplay_CheckedChanged);
            // 
            // chkNormalDisplay
            // 
            this.chkNormalDisplay.AutoSize = true;
            this.chkNormalDisplay.Checked = true;
            this.chkNormalDisplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNormalDisplay.Location = new System.Drawing.Point(4, 128);
            this.chkNormalDisplay.Name = "chkNormalDisplay";
            this.chkNormalDisplay.Size = new System.Drawing.Size(106, 16);
            this.chkNormalDisplay.TabIndex = 0;
            this.chkNormalDisplay.Text = "Display normals";
            this.chkNormalDisplay.CheckedChanged += new System.EventHandler(this.chkNormalDisplay_CheckedChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(3, 256);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Padding = new System.Windows.Forms.Padding(5);
            this.cmdReset.Size = new System.Drawing.Size(100, 21);
            this.cmdReset.TabIndex = 1;
            this.cmdReset.Text = "Reset Settings";
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.chkOldPatchNSD);
            this.Controls.Add(this.chkPatchNSDSavesNSF);
            this.Controls.Add(this.fraAnimGrid);
            this.Controls.Add(this.chkDeleteInvalidEntries);
            this.Controls.Add(this.chkUseAnimLinks);
            this.Controls.Add(this.chkCollisionDisplay);
            this.Controls.Add(this.chkNormalDisplay);
            this.Controls.Add(this.fraSize);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.fraLang);
            this.Controls.Add(this.fraClearCol);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "ConfigEditor";
            this.Size = new System.Drawing.Size(474, 287);
            this.fraLang.ResumeLayout(false);
            this.fraSize.ResumeLayout(false);
            this.fraSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW)).EndInit();
            this.fraClearCol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClearCol)).EndInit();
            this.fraAnimGrid.ResumeLayout(false);
            this.fraAnimGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkComboBox dpdLang;
        private System.Windows.Forms.GroupBox fraLang;
        private DarkUI.Controls.DarkButton cmdReset;
        private System.Windows.Forms.GroupBox fraSize;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label lblW;
        private DarkUI.Controls.DarkNumericUpDown numH;
        private DarkUI.Controls.DarkNumericUpDown numW;
        private DarkUI.Controls.DarkCheckBox chkNormalDisplay;
        private DarkUI.Controls.DarkCheckBox chkCollisionDisplay;
        private DarkUI.Controls.DarkCheckBox chkUseAnimLinks;
        private System.Windows.Forms.ColorDialog cdlClearCol;
        private System.Windows.Forms.GroupBox fraClearCol;
        private System.Windows.Forms.PictureBox picClearCol;
        private DarkUI.Controls.DarkCheckBox chkDeleteInvalidEntries;
        private System.Windows.Forms.GroupBox fraAnimGrid;
        private DarkUI.Controls.DarkNumericUpDown numAnimGrid;
        private System.Windows.Forms.Label lblAnimGrid;
        private DarkUI.Controls.DarkCheckBox chkAnimGrid;
        private DarkUI.Controls.DarkButton cmdClearCol;
        private DarkUI.Controls.DarkCheckBox chkPatchNSDSavesNSF;
        private DarkUI.Controls.DarkCheckBox chkOldPatchNSD;
    }
}
