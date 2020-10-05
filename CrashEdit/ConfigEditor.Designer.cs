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
            this.dpdLang = new MetroFramework.Controls.MetroComboBox();
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
            this.chkAnimGrid = new MetroFramework.Controls.MetroCheckBox();
            this.chkOldPatchNSD = new MetroFramework.Controls.MetroCheckBox();
            this.chkPatchNSDSavesNSF = new MetroFramework.Controls.MetroCheckBox();
            this.chkDeleteInvalidEntries = new MetroFramework.Controls.MetroCheckBox();
            this.chkUseAnimLinks = new MetroFramework.Controls.MetroCheckBox();
            this.chkCollisionDisplay = new MetroFramework.Controls.MetroCheckBox();
            this.chkNormalDisplay = new MetroFramework.Controls.MetroCheckBox();
            this.cmdReset = new MetroFramework.Controls.MetroButton();
            this.chkDetailedCollision = new MetroFramework.Controls.MetroCheckBox();
            this.tglKeyBinds = new MetroFramework.Controls.MetroToggle();
            this.fraKeyBinds = new System.Windows.Forms.GroupBox();
            this.cmdHelp = new MetroFramework.Controls.MetroButton();
            this.fraLang.SuspendLayout();
            this.fraSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW)).BeginInit();
            this.fraClearCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClearCol)).BeginInit();
            this.fraAnimGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimGrid)).BeginInit();
            this.fraKeyBinds.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraLang
            // 
            this.fraLang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraLang.Controls.Add(this.dpdLang);
            this.fraLang.Font = new System.Drawing.Font("Arial", 9F);
            this.fraLang.ForeColor = System.Drawing.SystemColors.Window;
            this.fraLang.Location = new System.Drawing.Point(3, 3);
            this.fraLang.Name = "fraLang";
            this.fraLang.Size = new System.Drawing.Size(162, 49);
            this.fraLang.TabIndex = 1;
            this.fraLang.TabStop = false;
            this.fraLang.Text = "Language (requires restart)";
            // 
            // dpdLang
            // 
            this.dpdLang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.dpdLang.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.dpdLang.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.dpdLang.FormattingEnabled = true;
            this.dpdLang.IntegralHeight = false;
            this.dpdLang.ItemHeight = 19;
            this.dpdLang.Location = new System.Drawing.Point(6, 18);
            this.dpdLang.Name = "dpdLang";
            this.dpdLang.Size = new System.Drawing.Size(150, 25);
            this.dpdLang.Style = MetroFramework.MetroColorStyle.Blue;
            this.dpdLang.TabIndex = 0;
            this.dpdLang.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.dpdLang.UseCustomBackColor = true;
            this.dpdLang.UseSelectable = true;
            // 
            // fraSize
            // 
            this.fraSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSize.Controls.Add(this.lblH);
            this.fraSize.Controls.Add(this.lblW);
            this.fraSize.Controls.Add(this.numH);
            this.fraSize.Controls.Add(this.numW);
            this.fraSize.Font = new System.Drawing.Font("Arial", 9F);
            this.fraSize.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSize.Location = new System.Drawing.Point(3, 58);
            this.fraSize.Name = "fraSize";
            this.fraSize.Size = new System.Drawing.Size(135, 68);
            this.fraSize.TabIndex = 1;
            this.fraSize.TabStop = false;
            this.fraSize.Text = "Default Window Size";
            // 
            // lblH
            // 
            this.lblH.Location = new System.Drawing.Point(6, 45);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(44, 15);
            this.lblH.TabIndex = 3;
            this.lblH.Text = "Height";
            // 
            // lblW
            // 
            this.lblW.Location = new System.Drawing.Point(6, 20);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(38, 15);
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
            this.numH.Size = new System.Drawing.Size(75, 21);
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
            this.numW.Size = new System.Drawing.Size(75, 21);
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
            this.fraClearCol.Font = new System.Drawing.Font("Arial", 9F);
            this.fraClearCol.ForeColor = System.Drawing.SystemColors.Window;
            this.fraClearCol.Location = new System.Drawing.Point(144, 58);
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
            this.fraAnimGrid.Font = new System.Drawing.Font("Arial", 9F);
            this.fraAnimGrid.ForeColor = System.Drawing.SystemColors.Window;
            this.fraAnimGrid.Location = new System.Drawing.Point(279, 58);
            this.fraAnimGrid.Name = "fraAnimGrid";
            this.fraAnimGrid.Size = new System.Drawing.Size(156, 68);
            this.fraAnimGrid.TabIndex = 6;
            this.fraAnimGrid.TabStop = false;
            this.fraAnimGrid.Text = "Animation Viewer World Grid";
            // 
            // numAnimGrid
            // 
            this.numAnimGrid.Location = new System.Drawing.Point(62, 39);
            this.numAnimGrid.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numAnimGrid.Name = "numAnimGrid";
            this.numAnimGrid.Size = new System.Drawing.Size(88, 21);
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
            this.lblAnimGrid.Size = new System.Drawing.Size(49, 15);
            this.lblAnimGrid.TabIndex = 1;
            this.lblAnimGrid.Text = "Amount";
            // 
            // chkAnimGrid
            // 
            this.chkAnimGrid.AutoSize = true;
            this.chkAnimGrid.Checked = true;
            this.chkAnimGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAnimGrid.Location = new System.Drawing.Point(10, 20);
            this.chkAnimGrid.Name = "chkAnimGrid";
            this.chkAnimGrid.Size = new System.Drawing.Size(65, 15);
            this.chkAnimGrid.TabIndex = 0;
            this.chkAnimGrid.Text = "Enabled";
            this.chkAnimGrid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkAnimGrid.UseCustomBackColor = true;
            this.chkAnimGrid.UseSelectable = true;
            this.chkAnimGrid.CheckedChanged += new System.EventHandler(this.chkAnimGrid_CheckedChanged);
            // 
            // chkOldPatchNSD
            // 
            this.chkOldPatchNSD.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.chkOldPatchNSD.Location = new System.Drawing.Point(4, 259);
            this.chkOldPatchNSD.Name = "chkOldPatchNSD";
            this.chkOldPatchNSD.Size = new System.Drawing.Size(335, 15);
            this.chkOldPatchNSD.TabIndex = 9;
            this.chkOldPatchNSD.Text = "(Patch NSD) Use old NSD patching from CrashEdit v0.2.49.0";
            this.chkOldPatchNSD.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkOldPatchNSD.UseCustomBackColor = true;
            this.chkOldPatchNSD.UseCustomForeColor = true;
            this.chkOldPatchNSD.UseSelectable = true;
            this.chkOldPatchNSD.CheckedChanged += new System.EventHandler(this.chkOldPatchNSD_CheckedChanged);
            // 
            // chkPatchNSDSavesNSF
            // 
            this.chkPatchNSDSavesNSF.Checked = true;
            this.chkPatchNSDSavesNSF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPatchNSDSavesNSF.ForeColor = System.Drawing.SystemColors.Control;
            this.chkPatchNSDSavesNSF.Location = new System.Drawing.Point(4, 238);
            this.chkPatchNSDSavesNSF.Name = "chkPatchNSDSavesNSF";
            this.chkPatchNSDSavesNSF.Size = new System.Drawing.Size(280, 15);
            this.chkPatchNSDSavesNSF.TabIndex = 8;
            this.chkPatchNSDSavesNSF.Text = "(Patch NSD) Always save NSF after NSD patching";
            this.chkPatchNSDSavesNSF.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkPatchNSDSavesNSF.UseCustomBackColor = true;
            this.chkPatchNSDSavesNSF.UseSelectable = true;
            this.chkPatchNSDSavesNSF.CheckedChanged += new System.EventHandler(this.chkPatchNSDSavesNSF_CheckedChanged);
            // 
            // chkDeleteInvalidEntries
            // 
            this.chkDeleteInvalidEntries.Location = new System.Drawing.Point(4, 217);
            this.chkDeleteInvalidEntries.Name = "chkDeleteInvalidEntries";
            this.chkDeleteInvalidEntries.Size = new System.Drawing.Size(309, 15);
            this.chkDeleteInvalidEntries.TabIndex = 5;
            this.chkDeleteInvalidEntries.Text = "(Patch NSD) Delete non-existent entries from load lists";
            this.chkDeleteInvalidEntries.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkDeleteInvalidEntries.UseCustomBackColor = true;
            this.chkDeleteInvalidEntries.UseSelectable = true;
            this.chkDeleteInvalidEntries.CheckedChanged += new System.EventHandler(this.chkDeleteInvalidEntries_CheckedChanged);
            // 
            // chkUseAnimLinks
            // 
            this.chkUseAnimLinks.Checked = true;
            this.chkUseAnimLinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseAnimLinks.Location = new System.Drawing.Point(4, 196);
            this.chkUseAnimLinks.Name = "chkUseAnimLinks";
            this.chkUseAnimLinks.Size = new System.Drawing.Size(255, 15);
            this.chkUseAnimLinks.TabIndex = 3;
            this.chkUseAnimLinks.Text = "(Crash 3) Used saved animation-model links";
            this.chkUseAnimLinks.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkUseAnimLinks.UseCustomBackColor = true;
            this.chkUseAnimLinks.UseSelectable = true;
            this.chkUseAnimLinks.CheckedChanged += new System.EventHandler(this.chkUseAnimLinks_CheckedChanged);
            // 
            // chkCollisionDisplay
            // 
            this.chkCollisionDisplay.Location = new System.Drawing.Point(4, 154);
            this.chkCollisionDisplay.Name = "chkCollisionDisplay";
            this.chkCollisionDisplay.Size = new System.Drawing.Size(198, 15);
            this.chkCollisionDisplay.TabIndex = 2;
            this.chkCollisionDisplay.Text = "Display frame collision by default";
            this.chkCollisionDisplay.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkCollisionDisplay.UseCustomBackColor = true;
            this.chkCollisionDisplay.UseSelectable = true;
            this.chkCollisionDisplay.CheckedChanged += new System.EventHandler(this.chkCollisionDisplay_CheckedChanged);
            // 
            // chkNormalDisplay
            // 
            this.chkNormalDisplay.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkNormalDisplay.Location = new System.Drawing.Point(4, 132);
            this.chkNormalDisplay.Name = "chkNormalDisplay";
            this.chkNormalDisplay.Size = new System.Drawing.Size(107, 15);
            this.chkNormalDisplay.TabIndex = 0;
            this.chkNormalDisplay.Text = "Display normals";
            this.chkNormalDisplay.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkNormalDisplay.UseCustomBackColor = true;
            this.chkNormalDisplay.UseSelectable = true;
            this.chkNormalDisplay.CheckedChanged += new System.EventHandler(this.chkNormalDisplay_CheckedChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cmdReset.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.cmdReset.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.cmdReset.ForeColor = System.Drawing.Color.Cyan;
            this.cmdReset.Location = new System.Drawing.Point(12, 280);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(99, 23);
            this.cmdReset.TabIndex = 10;
            this.cmdReset.Text = "Reset Settings";
            this.cmdReset.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cmdReset.UseCustomBackColor = true;
            this.cmdReset.UseCustomForeColor = true;
            this.cmdReset.UseSelectable = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // chkDetailedCollision
            // 
            this.chkDetailedCollision.Checked = true;
            this.chkDetailedCollision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDetailedCollision.Location = new System.Drawing.Point(4, 175);
            this.chkDetailedCollision.Name = "chkDetailedCollision";
            this.chkDetailedCollision.Size = new System.Drawing.Size(186, 15);
            this.chkDetailedCollision.TabIndex = 11;
            this.chkDetailedCollision.Text = "Display detailed collision types";
            this.chkDetailedCollision.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkDetailedCollision.UseCustomBackColor = true;
            this.chkDetailedCollision.UseSelectable = true;
            this.chkDetailedCollision.CheckedChanged += new System.EventHandler(this.chkDetailedCollision_CheckedChanged);
            // 
            // tglKeyBinds
            // 
            this.tglKeyBinds.AutoSize = true;
            this.tglKeyBinds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.tglKeyBinds.Location = new System.Drawing.Point(15, 20);
            this.tglKeyBinds.Name = "tglKeyBinds";
            this.tglKeyBinds.Size = new System.Drawing.Size(80, 19);
            this.tglKeyBinds.Style = MetroFramework.MetroColorStyle.Blue;
            this.tglKeyBinds.TabIndex = 12;
            this.tglKeyBinds.Text = "Off";
            this.tglKeyBinds.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tglKeyBinds.UseCustomBackColor = true;
            this.tglKeyBinds.UseSelectable = true;
            this.tglKeyBinds.CheckedChanged += new System.EventHandler(this.TglKeyBinds_CheckedChanged);
            // 
            // fraKeyBinds
            // 
            this.fraKeyBinds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraKeyBinds.Controls.Add(this.tglKeyBinds);
            this.fraKeyBinds.Font = new System.Drawing.Font("Arial", 9F);
            this.fraKeyBinds.ForeColor = System.Drawing.SystemColors.Window;
            this.fraKeyBinds.Location = new System.Drawing.Point(171, 3);
            this.fraKeyBinds.Name = "fraKeyBinds";
            this.fraKeyBinds.Size = new System.Drawing.Size(117, 49);
            this.fraKeyBinds.TabIndex = 13;
            this.fraKeyBinds.TabStop = false;
            this.fraKeyBinds.Text = "Change Key Binding";
            // 
            // cmdHelp
            // 
            this.cmdHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.cmdHelp.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.cmdHelp.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.cmdHelp.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmdHelp.Location = new System.Drawing.Point(12, 309);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(50, 23);
            this.cmdHelp.Style = MetroFramework.MetroColorStyle.Blue;
            this.cmdHelp.TabIndex = 14;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cmdHelp.UseCustomBackColor = true;
            this.cmdHelp.UseCustomForeColor = true;
            this.cmdHelp.UseSelectable = true;
            this.cmdHelp.Click += new System.EventHandler(this.CmdHelp_Click);
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.fraKeyBinds);
            this.Controls.Add(this.chkDetailedCollision);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.chkOldPatchNSD);
            this.Controls.Add(this.chkPatchNSDSavesNSF);
            this.Controls.Add(this.fraAnimGrid);
            this.Controls.Add(this.chkDeleteInvalidEntries);
            this.Controls.Add(this.chkUseAnimLinks);
            this.Controls.Add(this.chkCollisionDisplay);
            this.Controls.Add(this.chkNormalDisplay);
            this.Controls.Add(this.fraSize);
            this.Controls.Add(this.fraLang);
            this.Controls.Add(this.fraClearCol);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "ConfigEditor";
            this.Size = new System.Drawing.Size(747, 560);
            this.fraLang.ResumeLayout(false);
            this.fraSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW)).EndInit();
            this.fraClearCol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClearCol)).EndInit();
            this.fraAnimGrid.ResumeLayout(false);
            this.fraAnimGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnimGrid)).EndInit();
            this.fraKeyBinds.ResumeLayout(false);
            this.fraKeyBinds.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox dpdLang;
        private System.Windows.Forms.GroupBox fraLang;
        private System.Windows.Forms.GroupBox fraSize;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label lblW;
        private DarkUI.Controls.DarkNumericUpDown numH;
        private DarkUI.Controls.DarkNumericUpDown numW;
        private MetroFramework.Controls.MetroCheckBox chkNormalDisplay;
        private MetroFramework.Controls.MetroCheckBox chkCollisionDisplay;
        private MetroFramework.Controls.MetroCheckBox chkUseAnimLinks;
        private System.Windows.Forms.ColorDialog cdlClearCol;
        private System.Windows.Forms.GroupBox fraClearCol;
        private System.Windows.Forms.PictureBox picClearCol;
        private MetroFramework.Controls.MetroCheckBox chkDeleteInvalidEntries;
        private System.Windows.Forms.GroupBox fraAnimGrid;
        private DarkUI.Controls.DarkNumericUpDown numAnimGrid;
        private System.Windows.Forms.Label lblAnimGrid;
        private MetroFramework.Controls.MetroCheckBox chkAnimGrid;
        private DarkUI.Controls.DarkButton cmdClearCol;
        private MetroFramework.Controls.MetroCheckBox chkPatchNSDSavesNSF;
        private MetroFramework.Controls.MetroCheckBox chkOldPatchNSD;
        private MetroFramework.Controls.MetroButton cmdReset;
        private MetroFramework.Controls.MetroCheckBox chkDetailedCollision;
        private System.Windows.Forms.GroupBox fraKeyBinds;
        private MetroFramework.Controls.MetroButton cmdHelp;
        private MetroFramework.Controls.MetroToggle tglKeyBinds;
    }
}
