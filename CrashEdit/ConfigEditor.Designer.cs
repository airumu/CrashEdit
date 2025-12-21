using AltUI.Controls;
using CrashEdit.CE.Properties;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
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
            TableLayoutPanel tableLayoutPanel4;
            lblFontName = new Label();
            lblFontSize = new Label();
            dpdFont = new DarkComboBox();
            numFontSize = new DarkNumericUpDown();
            chkViewerShowHelp = new CheckBox();
            chkFont2DEnable = new CheckBox();
            chkFont3DEnable = new CheckBox();
            chkCollisionDisplay = new CheckBox();
            chkNormalDisplay = new CheckBox();
            fraAnimGrid = new DarkGroupBox();
            numAnimGrid = new DarkNumericUpDown();
            lblAnimGrid = new Label();
            chkAnimGrid = new CheckBox();
            fraClearCol = new DarkGroupBox();
            picClearCol = new PictureBox();
            fraFont = new DarkGroupBox();
            chkPatchNSDSavesNSF = new CheckBox();
            chkDeleteInvalidEntries = new CheckBox();
            dpdLang = new DarkComboBox();
            numH = new DarkNumericUpDown();
            lblWH = new Label();
            numW = new DarkNumericUpDown();
            cmdReset = new DarkButton();
            fraSize = new DarkGroupBox();
            cdlClearCol = new ColorDialog();
            fraNodeShadeAmt = new DarkGroupBox();
            lblNodeShadeAmt = new Label();
            sldNodeShadeAmt = new MetroSetTrackBar();
            tbcSettings = new MetroSetTabControl();
            tbpGeneral = new TabPage();
            fraHexView = new DarkGroupBox();
            dpdHexView = new DarkComboBox();
            chkApplyMica = new CheckBox();
            cmdHelp = new DarkButton();
            fraLang = new DarkGroupBox();
            chkAllowMultiopenNSF = new CheckBox();
            fraRecentNSF = new DarkGroupBox();
            lstRecentNSF = new ListBox();
            cmdClearRecentFiles = new DarkButton();
            tbp3D = new TabPage();
            fraCollisionNode = new DarkGroupBox();
            chkUseNeighborZoneTransparency = new CheckBox();
            chkAnimTexShow0 = new CheckBox();
            tbpDebugDisplay = new TabPage();
            chkShowRenderingErrors = new CheckBox();
            darkGroupBox3 = new DarkGroupBox();
            chkViewCamera = new CheckBox();
            chkViewCameraAngle = new CheckBox();
            darkGroupBox2 = new DarkGroupBox();
            chkViewZoneBox = new CheckBox();
            chkViewZoneName = new CheckBox();
            darkGroupBox1 = new DarkGroupBox();
            chkShowEntityParams = new CheckBox();
            tbpPatchNSD = new TabPage();
            tbpExtra = new TabPage();
            chkIgnoreDuplicatedEntryError = new CheckBox();
            fraMiscDebug = new DarkGroupBox();
            chkOutputCopyTextureResult = new CheckBox();
            chkOutputCLUTInfo = new CheckBox();
            chkOutputModelTextureInfo = new CheckBox();
            chkEnableLegacyEntityBox = new CheckBox();
            fraExtra = new DarkGroupBox();
            chkEnableCustomCrates = new CheckBox();
            chkLagacyPatchNSD = new CheckBox();
            chkEnableC2TT = new CheckBox();
            fraUICtrls = new DarkGroupBox();
            chkShowRebuild = new CheckBox();
            chkShowUndockButton = new CheckBox();
            chkShowRefresh = new CheckBox();
            chkSplitViewerPanels = new CheckBox();
            chkLiteralCollisionTypes = new CheckBox();
            chkPatchGOOLC3toC2 = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).BeginInit();
            fraAnimGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAnimGrid).BeginInit();
            fraClearCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picClearCol).BeginInit();
            fraFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numW).BeginInit();
            fraSize.SuspendLayout();
            fraNodeShadeAmt.SuspendLayout();
            tbcSettings.SuspendLayout();
            tbpGeneral.SuspendLayout();
            fraHexView.SuspendLayout();
            fraLang.SuspendLayout();
            fraRecentNSF.SuspendLayout();
            tbp3D.SuspendLayout();
            fraCollisionNode.SuspendLayout();
            tbpDebugDisplay.SuspendLayout();
            darkGroupBox3.SuspendLayout();
            darkGroupBox2.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            tbpPatchNSD.SuspendLayout();
            tbpExtra.SuspendLayout();
            fraMiscDebug.SuspendLayout();
            fraExtra.SuspendLayout();
            fraUICtrls.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(lblFontName, 0, 0);
            tableLayoutPanel4.Controls.Add(lblFontSize, 0, 1);
            tableLayoutPanel4.Controls.Add(dpdFont, 1, 0);
            tableLayoutPanel4.Controls.Add(numFontSize, 1, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(4, 19);
            tableLayoutPanel4.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(282, 59);
            tableLayoutPanel4.TabIndex = 5;
            // 
            // lblFontName
            // 
            lblFontName.AutoSize = true;
            lblFontName.Dock = DockStyle.Fill;
            lblFontName.Location = new Point(4, 0);
            lblFontName.Margin = new Padding(4, 0, 4, 0);
            lblFontName.Name = "lblFontName";
            lblFontName.Size = new Size(54, 30);
            lblFontName.TabIndex = 3;
            lblFontName.Text = "Font";
            lblFontName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblFontSize
            // 
            lblFontSize.AutoSize = true;
            lblFontSize.Dock = DockStyle.Fill;
            lblFontSize.Location = new Point(4, 30);
            lblFontSize.Margin = new Padding(4, 0, 4, 0);
            lblFontSize.Name = "lblFontSize";
            lblFontSize.Size = new Size(54, 29);
            lblFontSize.TabIndex = 4;
            lblFontSize.Text = "Font Size";
            lblFontSize.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dpdFont
            // 
            dpdFont.CausesValidation = false;
            dpdFont.DrawMode = DrawMode.OwnerDrawFixed;
            dpdFont.FormattingEnabled = true;
            dpdFont.Location = new Point(66, 3);
            dpdFont.Margin = new Padding(4, 3, 4, 3);
            dpdFont.Name = "dpdFont";
            dpdFont.Size = new Size(212, 24);
            dpdFont.TabIndex = 1;
            // 
            // numFontSize
            // 
            numFontSize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            numFontSize.Location = new Point(66, 33);
            numFontSize.Margin = new Padding(4, 3, 4, 3);
            numFontSize.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numFontSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numFontSize.Name = "numFontSize";
            numFontSize.Size = new Size(63, 23);
            numFontSize.TabIndex = 3;
            numFontSize.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numFontSize.ValueChanged += numFontSize_ValueChanged;
            // 
            // chkViewerShowHelp
            // 
            chkViewerShowHelp.AutoSize = true;
            chkViewerShowHelp.BackColor = Color.Transparent;
            chkViewerShowHelp.Checked = true;
            chkViewerShowHelp.CheckState = CheckState.Checked;
            chkViewerShowHelp.Location = new Point(7, 56);
            chkViewerShowHelp.Margin = new Padding(4, 3, 4, 3);
            chkViewerShowHelp.Name = "chkViewerShowHelp";
            chkViewerShowHelp.Size = new Size(160, 19);
            chkViewerShowHelp.TabIndex = 7;
            chkViewerShowHelp.Text = "Show help text by default";
            chkViewerShowHelp.UseVisualStyleBackColor = false;
            chkViewerShowHelp.CheckedChanged += chkViewerShowHelp_CheckedChanged;
            // 
            // chkFont2DEnable
            // 
            chkFont2DEnable.AutoSize = true;
            chkFont2DEnable.BackColor = Color.Transparent;
            chkFont2DEnable.Location = new Point(7, 81);
            chkFont2DEnable.Margin = new Padding(4, 3, 4, 3);
            chkFont2DEnable.Name = "chkFont2DEnable";
            chkFont2DEnable.Size = new Size(136, 19);
            chkFont2DEnable.TabIndex = 6;
            chkFont2DEnable.Text = "Show debug console";
            chkFont2DEnable.UseVisualStyleBackColor = false;
            chkFont2DEnable.CheckedChanged += chkFont2DEnable_CheckedChanged;
            // 
            // chkFont3DEnable
            // 
            chkFont3DEnable.AutoSize = true;
            chkFont3DEnable.Checked = true;
            chkFont3DEnable.CheckState = CheckState.Checked;
            chkFont3DEnable.Location = new Point(7, 22);
            chkFont3DEnable.Margin = new Padding(4, 3, 4, 3);
            chkFont3DEnable.Name = "chkFont3DEnable";
            chkFont3DEnable.Size = new Size(126, 19);
            chkFont3DEnable.TabIndex = 5;
            chkFont3DEnable.Text = "Show entity names";
            chkFont3DEnable.UseVisualStyleBackColor = true;
            chkFont3DEnable.CheckedChanged += chkFont3DEnable_CheckedChanged;
            // 
            // chkCollisionDisplay
            // 
            chkCollisionDisplay.AutoSize = true;
            chkCollisionDisplay.BackColor = Color.Transparent;
            chkCollisionDisplay.Location = new Point(7, 31);
            chkCollisionDisplay.Margin = new Padding(4, 3, 4, 3);
            chkCollisionDisplay.Name = "chkCollisionDisplay";
            chkCollisionDisplay.Size = new Size(192, 19);
            chkCollisionDisplay.TabIndex = 2;
            chkCollisionDisplay.Text = "Show collision boxes by default";
            chkCollisionDisplay.UseVisualStyleBackColor = false;
            chkCollisionDisplay.CheckedChanged += chkCollisionDisplay_CheckedChanged;
            // 
            // chkNormalDisplay
            // 
            chkNormalDisplay.AutoSize = true;
            chkNormalDisplay.BackColor = Color.Transparent;
            chkNormalDisplay.Location = new Point(7, 6);
            chkNormalDisplay.Margin = new Padding(4, 3, 4, 3);
            chkNormalDisplay.Name = "chkNormalDisplay";
            chkNormalDisplay.Size = new Size(226, 19);
            chkNormalDisplay.TabIndex = 0;
            chkNormalDisplay.Text = "(Crash 1) Show normals in animations";
            chkNormalDisplay.UseVisualStyleBackColor = false;
            chkNormalDisplay.CheckedChanged += chkNormalDisplay_CheckedChanged;
            // 
            // fraAnimGrid
            // 
            fraAnimGrid.AutoSize = true;
            fraAnimGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraAnimGrid.BackColor = Color.Transparent;
            fraAnimGrid.Controls.Add(numAnimGrid);
            fraAnimGrid.Controls.Add(lblAnimGrid);
            fraAnimGrid.Controls.Add(chkAnimGrid);
            fraAnimGrid.Location = new Point(7, 6);
            fraAnimGrid.Margin = new Padding(4, 3, 4, 3);
            fraAnimGrid.Name = "fraAnimGrid";
            fraAnimGrid.Padding = new Padding(4, 3, 4, 3);
            fraAnimGrid.Size = new Size(160, 93);
            fraAnimGrid.TabIndex = 6;
            fraAnimGrid.TabStop = false;
            fraAnimGrid.Text = "Grid";
            // 
            // numAnimGrid
            // 
            numAnimGrid.Location = new Point(66, 48);
            numAnimGrid.Margin = new Padding(4, 3, 4, 3);
            numAnimGrid.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numAnimGrid.Name = "numAnimGrid";
            numAnimGrid.Size = new Size(86, 23);
            numAnimGrid.TabIndex = 2;
            numAnimGrid.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numAnimGrid.ValueChanged += numAnimGrid_ValueChanged;
            // 
            // lblAnimGrid
            // 
            lblAnimGrid.AutoSize = true;
            lblAnimGrid.Location = new Point(8, 50);
            lblAnimGrid.Margin = new Padding(4, 0, 4, 0);
            lblAnimGrid.Name = "lblAnimGrid";
            lblAnimGrid.Size = new Size(27, 15);
            lblAnimGrid.TabIndex = 1;
            lblAnimGrid.Text = "Size";
            // 
            // chkAnimGrid
            // 
            chkAnimGrid.AutoSize = true;
            chkAnimGrid.Location = new Point(8, 22);
            chkAnimGrid.Margin = new Padding(4, 3, 4, 3);
            chkAnimGrid.Name = "chkAnimGrid";
            chkAnimGrid.Size = new Size(68, 19);
            chkAnimGrid.TabIndex = 0;
            chkAnimGrid.Text = "Enabled";
            chkAnimGrid.UseVisualStyleBackColor = true;
            chkAnimGrid.CheckedChanged += chkAnimGrid_CheckedChanged;
            // 
            // fraClearCol
            // 
            fraClearCol.AutoSize = true;
            fraClearCol.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraClearCol.BackColor = Color.Transparent;
            fraClearCol.Controls.Add(picClearCol);
            fraClearCol.Location = new Point(175, 6);
            fraClearCol.Margin = new Padding(4, 3, 4, 3);
            fraClearCol.Name = "fraClearCol";
            fraClearCol.Padding = new Padding(4, 3, 4, 3);
            fraClearCol.Size = new Size(85, 93);
            fraClearCol.TabIndex = 4;
            fraClearCol.TabStop = false;
            fraClearCol.Text = "Clear Color";
            // 
            // picClearCol
            // 
            picClearCol.BorderStyle = BorderStyle.FixedSingle;
            picClearCol.Location = new Point(7, 22);
            picClearCol.Margin = new Padding(4, 3, 4, 3);
            picClearCol.Name = "picClearCol";
            picClearCol.Size = new Size(70, 49);
            picClearCol.TabIndex = 0;
            picClearCol.TabStop = false;
            picClearCol.Click += pictureBox1_Click;
            // 
            // fraFont
            // 
            fraFont.AutoSize = true;
            fraFont.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraFont.BackColor = Color.Transparent;
            fraFont.Controls.Add(tableLayoutPanel4);
            fraFont.Location = new Point(7, 105);
            fraFont.Margin = new Padding(4, 3, 4, 3);
            fraFont.Name = "fraFont";
            fraFont.Padding = new Padding(4, 3, 4, 13);
            fraFont.Size = new Size(290, 91);
            fraFont.TabIndex = 8;
            fraFont.TabStop = false;
            fraFont.Text = "3D Text";
            // 
            // chkPatchNSDSavesNSF
            // 
            chkPatchNSDSavesNSF.AutoSize = true;
            chkPatchNSDSavesNSF.Checked = true;
            chkPatchNSDSavesNSF.CheckState = CheckState.Checked;
            chkPatchNSDSavesNSF.Location = new Point(7, 31);
            chkPatchNSDSavesNSF.Margin = new Padding(4, 3, 4, 3);
            chkPatchNSDSavesNSF.Name = "chkPatchNSDSavesNSF";
            chkPatchNSDSavesNSF.Size = new Size(177, 19);
            chkPatchNSDSavesNSF.TabIndex = 7;
            chkPatchNSDSavesNSF.Text = "Save NSF after NSD patching";
            chkPatchNSDSavesNSF.UseVisualStyleBackColor = true;
            chkPatchNSDSavesNSF.CheckedChanged += chkPatchNSDSavesNSF_CheckedChanged;
            // 
            // chkDeleteInvalidEntries
            // 
            chkDeleteInvalidEntries.AutoSize = true;
            chkDeleteInvalidEntries.Checked = true;
            chkDeleteInvalidEntries.CheckState = CheckState.Checked;
            chkDeleteInvalidEntries.Location = new Point(7, 6);
            chkDeleteInvalidEntries.Margin = new Padding(4, 3, 4, 3);
            chkDeleteInvalidEntries.Name = "chkDeleteInvalidEntries";
            chkDeleteInvalidEntries.Size = new Size(245, 19);
            chkDeleteInvalidEntries.TabIndex = 5;
            chkDeleteInvalidEntries.Text = "Delete non-existent entries from load lists";
            chkDeleteInvalidEntries.UseVisualStyleBackColor = true;
            chkDeleteInvalidEntries.CheckedChanged += chkDeleteInvalidEntries_CheckedChanged;
            // 
            // dpdLang
            // 
            dpdLang.CausesValidation = false;
            dpdLang.DrawMode = DrawMode.OwnerDrawFixed;
            dpdLang.FormattingEnabled = true;
            dpdLang.Location = new Point(7, 22);
            dpdLang.Margin = new Padding(4, 3, 4, 3);
            dpdLang.MaximumSize = new Size(154, 0);
            dpdLang.Name = "dpdLang";
            dpdLang.Size = new Size(154, 24);
            dpdLang.TabIndex = 0;
            // 
            // numH
            // 
            numH.Location = new Point(88, 22);
            numH.Margin = new Padding(4, 3, 4, 3);
            numH.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            numH.MaximumSize = new Size(88, 0);
            numH.Minimum = new decimal(new int[] { 480, 0, 0, 0 });
            numH.Name = "numH";
            numH.Size = new Size(51, 23);
            numH.TabIndex = 1;
            numH.Value = new decimal(new int[] { 480, 0, 0, 0 });
            numH.ValueChanged += numH_ValueChanged;
            // 
            // lblWH
            // 
            lblWH.AutoSize = true;
            lblWH.Font = new Font("Microsoft Sans Serif", 10F);
            lblWH.Location = new Point(67, 24);
            lblWH.Margin = new Padding(4, 0, 4, 0);
            lblWH.Name = "lblWH";
            lblWH.Size = new Size(14, 17);
            lblWH.TabIndex = 2;
            lblWH.Text = "x";
            lblWH.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numW
            // 
            numW.Location = new Point(8, 22);
            numW.Margin = new Padding(4, 3, 4, 3);
            numW.Maximum = new decimal(new int[] { 8192, 0, 0, 0 });
            numW.MaximumSize = new Size(88, 0);
            numW.Minimum = new decimal(new int[] { 640, 0, 0, 0 });
            numW.Name = "numW";
            numW.Size = new Size(51, 23);
            numW.TabIndex = 0;
            numW.Value = new decimal(new int[] { 640, 0, 0, 0 });
            numW.ValueChanged += numW_ValueChanged;
            // 
            // cmdReset
            // 
            cmdReset.BorderColour = Color.Empty;
            cmdReset.CustomColour = false;
            cmdReset.FlatBottom = false;
            cmdReset.FlatTop = false;
            cmdReset.Location = new Point(7, 382);
            cmdReset.Margin = new Padding(4, 3, 4, 3);
            cmdReset.Name = "cmdReset";
            cmdReset.Padding = new Padding(5);
            cmdReset.Size = new Size(98, 28);
            cmdReset.TabIndex = 1;
            cmdReset.Text = "Reset Settings";
            cmdReset.Click += cmdReset_Click;
            // 
            // fraSize
            // 
            fraSize.AutoSize = true;
            fraSize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSize.BackColor = Color.Transparent;
            fraSize.Controls.Add(numW);
            fraSize.Controls.Add(numH);
            fraSize.Controls.Add(lblWH);
            fraSize.Location = new Point(7, 485);
            fraSize.Margin = new Padding(4, 3, 4, 3);
            fraSize.Name = "fraSize";
            fraSize.Padding = new Padding(4, 3, 4, 3);
            fraSize.Size = new Size(147, 67);
            fraSize.TabIndex = 1;
            fraSize.TabStop = false;
            fraSize.Text = "Default Window Size";
            fraSize.Visible = false;
            // 
            // cdlClearCol
            // 
            cdlClearCol.AnyColor = true;
            cdlClearCol.FullOpen = true;
            cdlClearCol.SolidColorOnly = true;
            // 
            // fraNodeShadeAmt
            // 
            fraNodeShadeAmt.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraNodeShadeAmt.BackColor = Color.Transparent;
            fraNodeShadeAmt.Controls.Add(lblNodeShadeAmt);
            fraNodeShadeAmt.Controls.Add(sldNodeShadeAmt);
            fraNodeShadeAmt.Location = new Point(7, 47);
            fraNodeShadeAmt.Margin = new Padding(4, 3, 4, 3);
            fraNodeShadeAmt.Name = "fraNodeShadeAmt";
            fraNodeShadeAmt.Padding = new Padding(4, 3, 4, 3);
            fraNodeShadeAmt.Size = new Size(276, 61);
            fraNodeShadeAmt.TabIndex = 10;
            fraNodeShadeAmt.TabStop = false;
            fraNodeShadeAmt.Text = "Shade Amount";
            // 
            // lblNodeShadeAmt
            // 
            lblNodeShadeAmt.Dock = DockStyle.Top;
            lblNodeShadeAmt.Font = new Font("Microsoft Sans Serif", 10F);
            lblNodeShadeAmt.Location = new Point(4, 35);
            lblNodeShadeAmt.Name = "lblNodeShadeAmt";
            lblNodeShadeAmt.Size = new Size(268, 23);
            lblNodeShadeAmt.TabIndex = 11;
            lblNodeShadeAmt.Text = "100%";
            lblNodeShadeAmt.TextAlign = ContentAlignment.TopCenter;
            // 
            // sldNodeShadeAmt
            // 
            sldNodeShadeAmt.BackColor = Color.Transparent;
            sldNodeShadeAmt.BackgroundColor = Color.FromArgb(205, 205, 205);
            sldNodeShadeAmt.DisabledBackColor = Color.FromArgb(235, 235, 235);
            sldNodeShadeAmt.DisabledBorderColor = Color.Empty;
            sldNodeShadeAmt.DisabledHandlerColor = Color.FromArgb(196, 196, 196);
            sldNodeShadeAmt.DisabledValueColor = Color.FromArgb(205, 205, 205);
            sldNodeShadeAmt.Dock = DockStyle.Top;
            sldNodeShadeAmt.HandlerColor = Color.FromArgb(180, 180, 180);
            sldNodeShadeAmt.IsDerivedStyle = true;
            sldNodeShadeAmt.Location = new Point(4, 19);
            sldNodeShadeAmt.Margin = new Padding(4, 3, 4, 3);
            sldNodeShadeAmt.Maximum = 100;
            sldNodeShadeAmt.Minimum = 0;
            sldNodeShadeAmt.Name = "sldNodeShadeAmt";
            sldNodeShadeAmt.Size = new Size(268, 16);
            sldNodeShadeAmt.Style = MetroSet_UI.Enums.Style.Light;
            sldNodeShadeAmt.StyleManager = null;
            sldNodeShadeAmt.TabIndex = 0;
            sldNodeShadeAmt.ThemeAuthor = "Narwin";
            sldNodeShadeAmt.ThemeName = "MetroLite";
            sldNodeShadeAmt.TickFrequency = 5;
            sldNodeShadeAmt.Value = 20;
            sldNodeShadeAmt.ValueColor = Color.FromArgb(65, 177, 225);
            sldNodeShadeAmt.ValueChanged += sldNodeShadeAmt_ValueChangedl;
            // 
            // tbcSettings
            // 
            tbcSettings.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tbcSettings.AnimateTime = 200;
            tbcSettings.BackgroundColor = Color.FromArgb(31, 31, 32);
            tbcSettings.Controls.Add(tbpGeneral);
            tbcSettings.Controls.Add(tbp3D);
            tbcSettings.Controls.Add(tbpDebugDisplay);
            tbcSettings.Controls.Add(tbpPatchNSD);
            tbcSettings.Controls.Add(tbpExtra);
            tbcSettings.Dock = DockStyle.Fill;
            tbcSettings.IsDerivedStyle = false;
            tbcSettings.ItemSize = new Size(100, 28);
            tbcSettings.Location = new Point(4, 3);
            tbcSettings.Name = "tbcSettings";
            tbcSettings.SelectedIndex = 0;
            tbcSettings.SelectedTextColor = Color.White;
            tbcSettings.Size = new Size(792, 594);
            tbcSettings.SizeMode = TabSizeMode.Fixed;
            tbcSettings.Speed = 100;
            tbcSettings.Style = MetroSet_UI.Enums.Style.Dark;
            tbcSettings.StyleManager = null;
            tbcSettings.TabIndex = 14;
            tbcSettings.ThemeAuthor = "Narwin";
            tbcSettings.ThemeName = "MetroDark";
            tbcSettings.UnselectedTextColor = Color.Gray;
            tbcSettings.UseAnimation = false;
            // 
            // tbpGeneral
            // 
            tbpGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tbpGeneral.Controls.Add(fraHexView);
            tbpGeneral.Controls.Add(chkApplyMica);
            tbpGeneral.Controls.Add(cmdHelp);
            tbpGeneral.Controls.Add(fraLang);
            tbpGeneral.Controls.Add(cmdReset);
            tbpGeneral.Controls.Add(fraSize);
            tbpGeneral.Controls.Add(chkAllowMultiopenNSF);
            tbpGeneral.Controls.Add(fraRecentNSF);
            tbpGeneral.Controls.Add(cmdClearRecentFiles);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Padding = new Padding(3);
            tbpGeneral.Size = new Size(784, 558);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            // 
            // fraHexView
            // 
            fraHexView.Controls.Add(dpdHexView);
            fraHexView.Location = new Point(185, 6);
            fraHexView.Name = "fraHexView";
            fraHexView.Size = new Size(134, 64);
            fraHexView.TabIndex = 5;
            fraHexView.TabStop = false;
            fraHexView.Text = "HexView Cell Size";
            // 
            // dpdHexView
            // 
            dpdHexView.DrawMode = DrawMode.OwnerDrawVariable;
            dpdHexView.FormattingEnabled = true;
            dpdHexView.Location = new Point(6, 22);
            dpdHexView.Name = "dpdHexView";
            dpdHexView.Size = new Size(121, 24);
            dpdHexView.TabIndex = 6;
            // 
            // chkApplyMica
            // 
            chkApplyMica.AutoSize = true;
            chkApplyMica.BackColor = Color.Transparent;
            chkApplyMica.Location = new Point(7, 76);
            chkApplyMica.Name = "chkApplyMica";
            chkApplyMica.Size = new Size(86, 19);
            chkApplyMica.TabIndex = 4;
            chkApplyMica.Text = "Apply Mica";
            chkApplyMica.UseVisualStyleBackColor = false;
            chkApplyMica.CheckedChanged += chkApplyMica_CheckedChanged;
            chkApplyMica.Click += chkApplyMica_Click;
            // 
            // cmdHelp
            // 
            cmdHelp.BorderColour = Color.Empty;
            cmdHelp.CustomColour = false;
            cmdHelp.FlatBottom = false;
            cmdHelp.FlatTop = false;
            cmdHelp.Location = new Point(7, 348);
            cmdHelp.Margin = new Padding(4, 3, 4, 3);
            cmdHelp.Name = "cmdHelp";
            cmdHelp.Padding = new Padding(5);
            cmdHelp.Size = new Size(98, 28);
            cmdHelp.TabIndex = 3;
            cmdHelp.Text = "Show Help";
            cmdHelp.Click += cmdHelp_Click;
            // 
            // fraLang
            // 
            fraLang.BackColor = Color.Transparent;
            fraLang.Controls.Add(dpdLang);
            fraLang.Location = new Point(7, 6);
            fraLang.Name = "fraLang";
            fraLang.Size = new Size(172, 64);
            fraLang.TabIndex = 2;
            fraLang.TabStop = false;
            fraLang.Text = "Language (requires restart)";
            // 
            // chkAllowMultiopenNSF
            // 
            chkAllowMultiopenNSF.AutoSize = true;
            chkAllowMultiopenNSF.Location = new Point(7, 101);
            chkAllowMultiopenNSF.Name = "chkAllowMultiopenNSF";
            chkAllowMultiopenNSF.Size = new Size(257, 19);
            chkAllowMultiopenNSF.TabIndex = 30;
            chkAllowMultiopenNSF.Text = "Allow opening the same NSF multiple times";
            chkAllowMultiopenNSF.UseVisualStyleBackColor = true;
            chkAllowMultiopenNSF.CheckedChanged += chkAllowMultiopenNSF_CheckedChanged;
            // 
            // fraRecentNSF
            // 
            fraRecentNSF.BackColor = Color.Transparent;
            fraRecentNSF.Controls.Add(lstRecentNSF);
            fraRecentNSF.Location = new Point(7, 126);
            fraRecentNSF.Name = "fraRecentNSF";
            fraRecentNSF.Size = new Size(717, 190);
            fraRecentNSF.TabIndex = 31;
            fraRecentNSF.TabStop = false;
            fraRecentNSF.Text = "Recent files";
            // 
            // lstRecentNSF
            // 
            lstRecentNSF.BackColor = Color.FromArgb(31, 31, 32);
            lstRecentNSF.BorderStyle = BorderStyle.None;
            lstRecentNSF.Cursor = Cursors.Hand;
            lstRecentNSF.Dock = DockStyle.Fill;
            lstRecentNSF.DrawMode = DrawMode.OwnerDrawFixed;
            lstRecentNSF.Location = new Point(3, 19);
            lstRecentNSF.Name = "lstRecentNSF";
            lstRecentNSF.Size = new Size(711, 168);
            lstRecentNSF.TabIndex = 0;
            lstRecentNSF.Click += lstRecentNSF_Click;
            lstRecentNSF.DrawItem += lstRecentNSF_DrawItem;
            // 
            // cmdClearRecentFiles
            // 
            cmdClearRecentFiles.BorderColour = Color.Empty;
            cmdClearRecentFiles.CustomColour = false;
            cmdClearRecentFiles.FlatBottom = false;
            cmdClearRecentFiles.FlatTop = false;
            cmdClearRecentFiles.Location = new Point(700, 322);
            cmdClearRecentFiles.Name = "cmdClearRecentFiles";
            cmdClearRecentFiles.Padding = new Padding(5);
            cmdClearRecentFiles.Size = new Size(24, 24);
            cmdClearRecentFiles.TabIndex = 31;
            cmdClearRecentFiles.Image = new Bitmap(Embeds.Bitmaps["Erase"], new Size(16, 16));
            cmdClearRecentFiles.Click += cmdClearRecentFiles_Click;
            // 
            // tbp3D
            // 
            tbp3D.BackColor = Color.FromArgb(31, 31, 32);
            tbp3D.Controls.Add(fraCollisionNode);
            tbp3D.Controls.Add(fraFont);
            tbp3D.Controls.Add(fraClearCol);
            tbp3D.Controls.Add(fraAnimGrid);
            tbp3D.Location = new Point(4, 32);
            tbp3D.Name = "tbp3D";
            tbp3D.Padding = new Padding(3);
            tbp3D.Size = new Size(784, 558);
            tbp3D.TabIndex = 1;
            tbp3D.Text = "3D Viewer";
            // 
            // fraCollisionNode
            // 
            fraCollisionNode.AutoSize = true;
            fraCollisionNode.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraCollisionNode.BackColor = Color.Transparent;
            fraCollisionNode.Controls.Add(fraNodeShadeAmt);
            fraCollisionNode.Controls.Add(chkUseNeighborZoneTransparency);            
            fraCollisionNode.Location = new Point(7, 202);
            fraCollisionNode.Name = "fraCollisionNode";
            fraCollisionNode.Size = new Size(290, 130);
            fraCollisionNode.TabIndex = 11;
            fraCollisionNode.TabStop = false;
            fraCollisionNode.Text = "Collision Node";
            // 
            // chkUseNeighborZoneTransparency
            // 
            chkUseNeighborZoneTransparency.AutoSize = true;
            chkUseNeighborZoneTransparency.Checked = true;
            chkUseNeighborZoneTransparency.CheckState = CheckState.Checked;
            chkUseNeighborZoneTransparency.Location = new Point(7, 22);
            chkUseNeighborZoneTransparency.Margin = new Padding(4, 3, 4, 3);
            chkUseNeighborZoneTransparency.Name = "chkUseNeighborZoneTransparency";
            chkUseNeighborZoneTransparency.Size = new Size(218, 19);
            chkUseNeighborZoneTransparency.TabIndex = 0;
            chkUseNeighborZoneTransparency.Text = "Use transparency for neighbor zones";
            chkUseNeighborZoneTransparency.UseVisualStyleBackColor = true;
            chkUseNeighborZoneTransparency.CheckedChanged += chkUseNeighborZoneTransparency_CheckedChanged;            
            // 
            // tbpDebugDisplay
            // 
            tbpDebugDisplay.BackColor = Color.FromArgb(31, 31, 32);
            tbpDebugDisplay.Controls.Add(chkShowRenderingErrors);
            tbpDebugDisplay.Controls.Add(darkGroupBox3);
            tbpDebugDisplay.Controls.Add(darkGroupBox2);
            tbpDebugDisplay.Controls.Add(darkGroupBox1);
            tbpDebugDisplay.Controls.Add(chkViewerShowHelp);
            tbpDebugDisplay.Controls.Add(chkFont2DEnable);
            tbpDebugDisplay.Controls.Add(chkNormalDisplay);
            tbpDebugDisplay.Controls.Add(chkCollisionDisplay);
            tbpDebugDisplay.Location = new Point(4, 32);
            tbpDebugDisplay.Name = "tbpDebugDisplay";
            tbpDebugDisplay.Padding = new Padding(3);
            tbpDebugDisplay.Size = new Size(784, 558);
            tbpDebugDisplay.TabIndex = 3;
            tbpDebugDisplay.Text = "Debug Displays";
            // 
            // chkShowRenderingErrors
            // 
            chkShowRenderingErrors.AutoSize = true;
            chkShowRenderingErrors.Checked = true;
            chkShowRenderingErrors.CheckState = CheckState.Checked;
            chkShowRenderingErrors.Location = new Point(7, 106);
            chkShowRenderingErrors.Name = "chkShowRenderingErrors";
            chkShowRenderingErrors.Size = new Size(142, 19);
            chkShowRenderingErrors.TabIndex = 17;
            chkShowRenderingErrors.Text = "Show rendering errors";
            chkShowRenderingErrors.UseVisualStyleBackColor = true;
            chkShowRenderingErrors.CheckedChanged += chkShowRenderingErrors_CheckedChanged;
            // 
            // darkGroupBox3
            // 
            darkGroupBox3.BackColor = Color.Transparent;
            darkGroupBox3.Controls.Add(chkViewCamera);
            darkGroupBox3.Controls.Add(chkViewCameraAngle);
            darkGroupBox3.Location = new Point(3, 287);
            darkGroupBox3.Name = "darkGroupBox3";
            darkGroupBox3.Size = new Size(200, 72);
            darkGroupBox3.TabIndex = 16;
            darkGroupBox3.TabStop = false;
            darkGroupBox3.Text = "Cameras";
            // 
            // chkViewCamera
            // 
            chkViewCamera.AutoSize = true;
            chkViewCamera.Checked = true;
            chkViewCamera.CheckState = CheckState.Checked;
            chkViewCamera.Location = new Point(6, 22);
            chkViewCamera.Name = "chkViewCamera";
            chkViewCamera.Size = new Size(138, 19);
            chkViewCamera.TabIndex = 11;
            chkViewCamera.Text = "Show camera entities";
            chkViewCamera.UseVisualStyleBackColor = true;
            chkViewCamera.CheckedChanged += chkViewCamera_CheckedChanged;
            // 
            // chkViewCameraAngle
            // 
            chkViewCameraAngle.AutoSize = true;
            chkViewCameraAngle.Checked = true;
            chkViewCameraAngle.CheckState = CheckState.Checked;
            chkViewCameraAngle.Location = new Point(6, 47);
            chkViewCameraAngle.Name = "chkViewCameraAngle";
            chkViewCameraAngle.Size = new Size(167, 19);
            chkViewCameraAngle.TabIndex = 10;
            chkViewCameraAngle.Text = "Show camera entity angles";
            chkViewCameraAngle.UseVisualStyleBackColor = true;
            chkViewCameraAngle.CheckedChanged += chkViewCameraAngle_CheckedChanged;
            // 
            // darkGroupBox2
            // 
            darkGroupBox2.BackColor = Color.Transparent;
            darkGroupBox2.Controls.Add(chkViewZoneBox);
            darkGroupBox2.Controls.Add(chkViewZoneName);
            darkGroupBox2.Location = new Point(3, 209);
            darkGroupBox2.Name = "darkGroupBox2";
            darkGroupBox2.Size = new Size(200, 72);
            darkGroupBox2.TabIndex = 15;
            darkGroupBox2.TabStop = false;
            darkGroupBox2.Text = "Zones";
            // 
            // chkViewZoneBox
            // 
            chkViewZoneBox.AutoSize = true;
            chkViewZoneBox.Checked = true;
            chkViewZoneBox.CheckState = CheckState.Checked;
            chkViewZoneBox.Location = new Point(7, 47);
            chkViewZoneBox.Name = "chkViewZoneBox";
            chkViewZoneBox.Size = new Size(145, 19);
            chkViewZoneBox.TabIndex = 8;
            chkViewZoneBox.Text = "Show zone boundaries";
            chkViewZoneBox.UseVisualStyleBackColor = true;
            chkViewZoneBox.CheckedChanged += chkViewZoneBox_CheckedChanged;
            // 
            // chkViewZoneName
            // 
            chkViewZoneName.AutoSize = true;
            chkViewZoneName.Checked = true;
            chkViewZoneName.CheckState = CheckState.Checked;
            chkViewZoneName.Location = new Point(7, 22);
            chkViewZoneName.Name = "chkViewZoneName";
            chkViewZoneName.Size = new Size(121, 19);
            chkViewZoneName.TabIndex = 9;
            chkViewZoneName.Text = "Show zone names";
            chkViewZoneName.UseVisualStyleBackColor = true;
            chkViewZoneName.CheckedChanged += chkViewZoneName_CheckedChanged;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.BackColor = Color.Transparent;
            darkGroupBox1.Controls.Add(chkFont3DEnable);
            darkGroupBox1.Controls.Add(chkShowEntityParams);
            darkGroupBox1.Location = new Point(3, 131);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(200, 72);
            darkGroupBox1.TabIndex = 14;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Entities";
            // 
            // chkShowEntityParams
            // 
            chkShowEntityParams.AutoSize = true;
            chkShowEntityParams.Checked = true;
            chkShowEntityParams.CheckState = CheckState.Checked;
            chkShowEntityParams.Location = new Point(7, 47);
            chkShowEntityParams.Margin = new Padding(4, 3, 4, 3);
            chkShowEntityParams.Name = "chkShowEntityParams";
            chkShowEntityParams.Size = new Size(150, 19);
            chkShowEntityParams.TabIndex = 13;
            chkShowEntityParams.Text = "Show entity parameters";
            chkShowEntityParams.UseVisualStyleBackColor = true;
            chkShowEntityParams.CheckedChanged += chkShowEntityParams_CheckedChanged;
            // 
            // tbpPatchNSD
            // 
            tbpPatchNSD.BackColor = Color.FromArgb(31, 31, 32);
            tbpPatchNSD.Controls.Add(chkDeleteInvalidEntries);
            tbpPatchNSD.Controls.Add(chkPatchNSDSavesNSF);
            tbpPatchNSD.Location = new Point(4, 32);
            tbpPatchNSD.Name = "tbpPatchNSD";
            tbpPatchNSD.Padding = new Padding(3);
            tbpPatchNSD.Size = new Size(784, 558);
            tbpPatchNSD.TabIndex = 2;
            tbpPatchNSD.Text = "Patch NSD";
            // 
            // tbpExtra
            // 
            tbpExtra.BackColor = Color.FromArgb(31, 31, 32);
            tbpExtra.Controls.Add(chkIgnoreDuplicatedEntryError);
            tbpExtra.Controls.Add(fraMiscDebug);
            tbpExtra.Controls.Add(chkEnableLegacyEntityBox);
            tbpExtra.Controls.Add(fraExtra);
            tbpExtra.Controls.Add(fraUICtrls);
            tbpExtra.Controls.Add(chkSplitViewerPanels);
            tbpExtra.Controls.Add(chkLiteralCollisionTypes);
            tbpExtra.Controls.Add(chkPatchGOOLC3toC2);
            tbpExtra.Controls.Add(chkAnimTexShow0);
            tbpExtra.Location = new Point(4, 32);
            tbpExtra.Name = "tbpExtra";
            tbpExtra.Padding = new Padding(3);
            tbpExtra.Size = new Size(784, 558);
            tbpExtra.TabIndex = 2;
            tbpExtra.Text = "Extra";
            // 
            // chkIgnoreDuplicatedEntryError
            // 
            chkIgnoreDuplicatedEntryError.AutoSize = true;
            chkIgnoreDuplicatedEntryError.Location = new Point(6, 31);
            chkIgnoreDuplicatedEntryError.Name = "chkIgnoreDuplicatedEntryError";
            chkIgnoreDuplicatedEntryError.Size = new Size(282, 19);
            chkIgnoreDuplicatedEntryError.TabIndex = 21;
            chkIgnoreDuplicatedEntryError.Text = "Ignore errors when processing duplicated entries";
            chkIgnoreDuplicatedEntryError.UseVisualStyleBackColor = true;
            chkIgnoreDuplicatedEntryError.CheckedChanged += chkIgnoreDuplicatedEntryError_CheckedChanged;
            // 
            // fraMiscDebug
            // 
            fraMiscDebug.BackColor = Color.Transparent;
            fraMiscDebug.Controls.Add(chkOutputCopyTextureResult);
            fraMiscDebug.Controls.Add(chkOutputCLUTInfo);
            fraMiscDebug.Controls.Add(chkOutputModelTextureInfo);
            fraMiscDebug.Location = new Point(0, 157);
            fraMiscDebug.Name = "fraMiscDebug";
            fraMiscDebug.Size = new Size(417, 100);
            fraMiscDebug.TabIndex = 20;
            fraMiscDebug.TabStop = false;
            fraMiscDebug.Text = "Debug";
            // 
            // chkOutputCopyTextureResult
            // 
            chkOutputCopyTextureResult.AutoSize = true;
            chkOutputCopyTextureResult.Location = new Point(6, 22);
            chkOutputCopyTextureResult.Margin = new Padding(4, 3, 4, 3);
            chkOutputCopyTextureResult.Name = "chkOutputCopyTextureResult";
            chkOutputCopyTextureResult.Size = new Size(260, 19);
            chkOutputCopyTextureResult.TabIndex = 19;
            chkOutputCopyTextureResult.Text = "Output copying texture result to the console";
            chkOutputCopyTextureResult.UseVisualStyleBackColor = true;
            chkOutputCopyTextureResult.CheckedChanged += chkOutputCopyTextureResult_CheckedChanged;
            // 
            // chkOutputCLUTInfo
            // 
            chkOutputCLUTInfo.AutoSize = true;
            chkOutputCLUTInfo.Location = new Point(6, 72);
            chkOutputCLUTInfo.Margin = new Padding(4, 3, 4, 3);
            chkOutputCLUTInfo.Name = "chkOutputCLUTInfo";
            chkOutputCLUTInfo.Size = new Size(239, 19);
            chkOutputCLUTInfo.TabIndex = 19;
            chkOutputCLUTInfo.Text = "Output CLUT information to the console";
            chkOutputCLUTInfo.UseVisualStyleBackColor = true;
            chkOutputCLUTInfo.CheckedChanged += chkOutputCLUTInfo_CheckedChanged;
            // 
            // chkOutputModelTextureInfo
            // 
            chkOutputModelTextureInfo.AutoSize = true;
            chkOutputModelTextureInfo.Location = new Point(6, 47);
            chkOutputModelTextureInfo.Margin = new Padding(4, 3, 4, 3);
            chkOutputModelTextureInfo.Name = "chkOutputModelTextureInfo";
            chkOutputModelTextureInfo.Size = new Size(248, 19);
            chkOutputModelTextureInfo.TabIndex = 19;
            chkOutputModelTextureInfo.Text = "Output texture information to the console";
            chkOutputModelTextureInfo.UseVisualStyleBackColor = true;
            chkOutputModelTextureInfo.CheckedChanged += chkOutputModelTextureInfo_CheckedChanged;
            // 
            // chkEnableLegacyEntityBox
            // 
            chkEnableLegacyEntityBox.AutoSize = true;
            chkEnableLegacyEntityBox.Location = new Point(6, 106);
            chkEnableLegacyEntityBox.Margin = new Padding(4, 3, 4, 3);
            chkEnableLegacyEntityBox.Name = "chkEnableLegacyEntityBox";
            chkEnableLegacyEntityBox.Size = new Size(165, 19);
            chkEnableLegacyEntityBox.TabIndex = 19;
            chkEnableLegacyEntityBox.Text = "Enable legacy entity editor";
            chkEnableLegacyEntityBox.UseVisualStyleBackColor = true;
            chkEnableLegacyEntityBox.CheckedChanged += chkEnableLegacyEntityBox_CheckedChanged;
            // 
            // chkAnimTexShow0
            //
            chkAnimTexShow0.AutoSize = true;
            chkAnimTexShow0.Location = new Point(6, 131);
            chkAnimTexShow0.Margin = new Padding(4, 3, 4, 3);
            chkAnimTexShow0.Name = "chkAnimTexShow0";
            chkAnimTexShow0.Size = new Size(165, 19);
            chkAnimTexShow0.TabIndex = 20;
            chkAnimTexShow0.Text = "Animated textures display Offset 0 textures";
            chkAnimTexShow0.UseVisualStyleBackColor = true;
            chkAnimTexShow0.CheckedChanged += chkAnimTexShow0_CheckedChanged;
            // 
            // fraExtra
            // 
            fraExtra.BackColor = Color.Transparent;
            fraExtra.Controls.Add(chkEnableCustomCrates);
            fraExtra.Controls.Add(chkLagacyPatchNSD);
            fraExtra.Controls.Add(chkEnableC2TT);
            fraExtra.Location = new Point(0, 262);
            fraExtra.Name = "fraExtra";
            fraExtra.Size = new Size(417, 100);
            fraExtra.TabIndex = 18;
            fraExtra.TabStop = false;
            fraExtra.Text = "CrashEdit-tweaked features";
            // 
            // chkEnableCustomCrates
            // 
            chkEnableCustomCrates.AutoSize = true;
            chkEnableCustomCrates.Location = new Point(6, 22);
            chkEnableCustomCrates.Name = "chkEnableCustomCrates";
            chkEnableCustomCrates.Size = new Size(138, 19);
            chkEnableCustomCrates.TabIndex = 15;
            chkEnableCustomCrates.Text = "Enable custom crates";
            chkEnableCustomCrates.UseVisualStyleBackColor = true;
            chkEnableCustomCrates.CheckedChanged += chkShowCustomCrates_CheckedChanged;
            // 
            // chkLagacyPatchNSD
            // 
            chkLagacyPatchNSD.AutoSize = true;
            chkLagacyPatchNSD.ForeColor = Color.Gold;
            chkLagacyPatchNSD.Location = new Point(6, 72);
            chkLagacyPatchNSD.Margin = new Padding(4, 3, 4, 3);
            chkLagacyPatchNSD.Name = "chkLagacyPatchNSD";
            chkLagacyPatchNSD.Size = new Size(288, 19);
            chkLagacyPatchNSD.TabIndex = 9;
            chkLagacyPatchNSD.Text = "Use legacy NSD patching from CrashEdit v0.2.49.0";
            chkLagacyPatchNSD.UseVisualStyleBackColor = true;
            chkLagacyPatchNSD.CheckedChanged += chkOldPatchNSD_CheckedChanged;
            // 
            // chkEnableC2TT
            // 
            chkEnableC2TT.AutoSize = true;
            chkEnableC2TT.Location = new Point(6, 47);
            chkEnableC2TT.Name = "chkEnableC2TT";
            chkEnableC2TT.Size = new Size(187, 19);
            chkEnableC2TT.TabIndex = 16;
            chkEnableC2TT.Text = "Enable Crash 2 time trial editor";
            chkEnableC2TT.UseVisualStyleBackColor = true;
            chkEnableC2TT.CheckedChanged += chkEnableC2TT_CheckedChanged;
            // 
            // fraUICtrls
            // 
            fraUICtrls.BackColor = Color.Transparent;
            fraUICtrls.Controls.Add(chkShowRebuild);
            fraUICtrls.Controls.Add(chkShowUndockButton);
            fraUICtrls.Controls.Add(chkShowRefresh);
            fraUICtrls.Location = new Point(0, 370);
            fraUICtrls.Name = "fraUICtrls";
            fraUICtrls.Size = new Size(417, 100);
            fraUICtrls.TabIndex = 25;
            fraUICtrls.TabStop = false;
            fraUICtrls.Text = "Toolbar UI customization";
            // 
            // chkShowRebuild
            // 
            chkShowRebuild.AutoSize = true;
            chkShowRebuild.Location = new Point(6, 72);
            chkShowRebuild.Name = "chkShowRebuild";
            chkShowRebuild.Size = new Size(248, 19);
            chkShowRebuild.TabIndex = 24;
            chkShowRebuild.Text = "Show Rebuild (c2export) button in toolbar";
            chkShowRebuild.UseVisualStyleBackColor = true;
            chkShowRebuild.CheckedChanged += chkEnableC2Rebuild_CheckedChanged;
            // 
            // chkShowUndockButton
            // 
            chkShowUndockButton.AutoSize = true;
            chkShowUndockButton.Location = new Point(6, 22);
            chkShowUndockButton.Name = "chkShowUndockButton";
            chkShowUndockButton.Size = new Size(192, 19);
            chkShowUndockButton.TabIndex = 22;
            chkShowUndockButton.Text = "Show Undock button in toolbar";
            chkShowUndockButton.UseVisualStyleBackColor = true;
            chkShowUndockButton.CheckedChanged += chkShowUndockButton_CheckedChanged;
            // 
            // chkShowRefresh
            // 
            chkShowRefresh.AutoSize = true;
            chkShowRefresh.Location = new Point(6, 47);
            chkShowRefresh.Name = "chkShowRefresh";
            chkShowRefresh.Size = new Size(187, 19);
            chkShowRefresh.TabIndex = 23;
            chkShowRefresh.Text = "Show Reload button in toolbar";
            chkShowRefresh.UseVisualStyleBackColor = true;
            chkShowRefresh.CheckedChanged += chkShowRefresh_CheckedChanged;
            // 
            // chkSplitViewerPanels
            // 
            chkSplitViewerPanels.AutoSize = true;
            chkSplitViewerPanels.Location = new Point(6, 81);
            chkSplitViewerPanels.Margin = new Padding(4, 3, 4, 3);
            chkSplitViewerPanels.Name = "chkSplitViewerPanels";
            chkSplitViewerPanels.Size = new Size(180, 19);
            chkSplitViewerPanels.TabIndex = 17;
            chkSplitViewerPanels.Text = "Split animation viewer panels";
            chkSplitViewerPanels.UseVisualStyleBackColor = true;
            chkSplitViewerPanels.CheckedChanged += chkSplitViewerPanels_CheckedChanged;
            // 
            // chkLiteralCollisionTypes
            // 
            chkLiteralCollisionTypes.AutoSize = true;
            chkLiteralCollisionTypes.Location = new Point(6, 56);
            chkLiteralCollisionTypes.Name = "chkLiteralCollisionTypes";
            chkLiteralCollisionTypes.Size = new Size(245, 19);
            chkLiteralCollisionTypes.TabIndex = 14;
            chkLiteralCollisionTypes.Text = "Show collision node types as literal values";
            chkLiteralCollisionTypes.UseVisualStyleBackColor = true;
            chkLiteralCollisionTypes.CheckedChanged += chkDetailedCollision_CheckedChanged;
            // 
            // chkPatchGOOLC3toC2
            // 
            chkPatchGOOLC3toC2.AutoSize = true;
            chkPatchGOOLC3toC2.Location = new Point(6, 6);
            chkPatchGOOLC3toC2.Margin = new Padding(4, 3, 4, 3);
            chkPatchGOOLC3toC2.Name = "chkPatchGOOLC3toC2";
            chkPatchGOOLC3toC2.Size = new Size(338, 19);
            chkPatchGOOLC3toC2.TabIndex = 8;
            chkPatchGOOLC3toC2.Text = "(Crash 2) Patch GOOL frame groups imported from Crash 3";
            chkPatchGOOLC3toC2.UseVisualStyleBackColor = true;
            chkPatchGOOLC3toC2.CheckedChanged += chkPatchGOOLC3toC2_CheckedChanged;
            chkPatchGOOLC3toC2.Click += chkPatchGOOLC3toC2_Click;
            // 
            // ConfigEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(tbcSettings);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ConfigEditor";
            Padding = new Padding(4, 3, 4, 3);
            Size = new Size(800, 600);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).EndInit();
            fraAnimGrid.ResumeLayout(false);
            fraAnimGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAnimGrid).EndInit();
            fraClearCol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picClearCol).EndInit();
            fraFont.ResumeLayout(false);
            fraFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numH).EndInit();
            ((System.ComponentModel.ISupportInitialize)numW).EndInit();
            fraSize.ResumeLayout(false);
            fraSize.PerformLayout();
            fraNodeShadeAmt.ResumeLayout(false);
            tbcSettings.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            tbpGeneral.PerformLayout();
            fraHexView.ResumeLayout(false);
            fraLang.ResumeLayout(false);
            fraRecentNSF.ResumeLayout(false);
            tbp3D.ResumeLayout(false);
            tbp3D.PerformLayout();
            fraCollisionNode.ResumeLayout(false);
            fraCollisionNode.PerformLayout();
            tbpDebugDisplay.ResumeLayout(false);
            tbpDebugDisplay.PerformLayout();
            darkGroupBox3.ResumeLayout(false);
            darkGroupBox3.PerformLayout();
            darkGroupBox2.ResumeLayout(false);
            darkGroupBox2.PerformLayout();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            tbpPatchNSD.ResumeLayout(false);
            tbpPatchNSD.PerformLayout();
            tbpExtra.ResumeLayout(false);
            tbpExtra.PerformLayout();
            fraMiscDebug.ResumeLayout(false);
            fraMiscDebug.PerformLayout();
            fraExtra.ResumeLayout(false);
            fraExtra.PerformLayout();
            fraUICtrls.ResumeLayout(false);
            fraUICtrls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DarkButton cmdReset;
        private DarkGroupBox fraSize;
        private Label lblWH;
        private DarkNumericUpDown numH;
        private DarkNumericUpDown numW;
        private ColorDialog cdlClearCol;
        private DarkGroupBox fraClearCol;
        private PictureBox picClearCol;
        private DarkGroupBox fraAnimGrid;
        private DarkNumericUpDown numAnimGrid;
        private Label lblAnimGrid;
        private CheckBox chkAnimGrid;
        private DarkComboBox dpdFont;
        private DarkGroupBox fraFont;
        private DarkNumericUpDown numFontSize;
        private Label lblFontSize;
        private Label lblFontName;
        private DarkGroupBox fraNodeShadeAmt;
        private MetroSetTrackBar sldNodeShadeAmt;
        private CheckBox chkViewerShowHelp;
        private CheckBox chkFont2DEnable;
        private CheckBox chkFont3DEnable;
        private CheckBox chkCollisionDisplay;
        private CheckBox chkNormalDisplay;
        private CheckBox chkPatchNSDSavesNSF;
        private CheckBox chkDeleteInvalidEntries;
        private DarkComboBox dpdLang;
        private Label lblNodeShadeAmt;
        private MetroSetTabControl tbcSettings;
        private TabPage tbpGeneral;
        private TabPage tbp3D;
        private TabPage tbpPatchNSD;
        private TabPage tbpDebugDisplay;
        private TabPage tbpExtra;
        private CheckBox chkViewZoneBox;
        private CheckBox chkViewCameraAngle;
        private CheckBox chkViewZoneName;
        private CheckBox chkViewCamera;
        private DarkGroupBox fraLang;
        private CheckBox chkShowEntityParams;
        private CheckBox chkLagacyPatchNSD;
        private CheckBox chkLiteralCollisionTypes;
        private CheckBox chkEnableCustomCrates;
        private CheckBox chkEnableC2TT;
        private CheckBox chkShowRebuild;
        private CheckBox chkShowUndockButton;
        private CheckBox chkShowRefresh;
        private CheckBox chkPatchGOOLC3toC2;
        private CheckBox chkSplitViewerPanels;
        private DarkGroupBox fraExtra;
        private DarkGroupBox fraUICtrls;
        private DarkButton cmdHelp;
        private DarkGroupBox darkGroupBox2;
        private DarkGroupBox darkGroupBox1;
        private DarkGroupBox darkGroupBox3;
        private CheckBox chkEnableLegacyEntityBox;
        private CheckBox chkOutputCopyTextureResult;
        private CheckBox chkOutputModelTextureInfo;
        private DarkGroupBox fraMiscDebug;
        private CheckBox chkApplyMica;
        private CheckBox chkOutputCLUTInfo;
        private CheckBox chkIgnoreDuplicatedEntryError;
        private DarkComboBox dpdHexView;
        private DarkGroupBox fraHexView;
        private CheckBox chkShowRenderingErrors;
        private System.Windows.Forms.ListBox lstRecentNSF;
        private DarkGroupBox fraRecentNSF;
        private CheckBox chkAllowMultiopenNSF;
        private DarkButton cmdClearRecentFiles;
        private CheckBox chkUseNeighborZoneTransparency;
        private DarkGroupBox fraCollisionNode;
        private CheckBox chkAnimTexShow0;
    }
}
