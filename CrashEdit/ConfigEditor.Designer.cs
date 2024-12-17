using AltUI.Controls;
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
            sldNodeShadeAmt = new TrackBar();
            tbcSettings = new MetroSetTabControl();
            tbpGeneral = new TabPage();
            fraLang = new DarkGroupBox();
            tbp3D = new TabPage();
            tbpDebugDisplay = new TabPage();
            chkShowEntityParams = new CheckBox();
            chkDisableVisual = new CheckBox();
            chkViewCamera = new CheckBox();
            chkViewCameraAngle = new CheckBox();
            chkViewZoneName = new CheckBox();
            chkViewZoneBox = new CheckBox();
            tbpPatchNSD = new TabPage();
            tbpMisc = new TabPage();
            chkEnableC2TT = new CheckBox();
            chkShowCustomCrates = new CheckBox();
            chkLiteralCollisionTypes = new CheckBox();
            chkPatchGOOLC3toC2 = new CheckBox();
            chkOldPatchNSD = new CheckBox();
            chkSplitViewerPanels = new CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)sldNodeShadeAmt).BeginInit();
            tbcSettings.SuspendLayout();
            tbpGeneral.SuspendLayout();
            fraLang.SuspendLayout();
            tbp3D.SuspendLayout();
            tbpDebugDisplay.SuspendLayout();
            tbpPatchNSD.SuspendLayout();
            tbpMisc.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(lblFontName, 0, 0);
            tableLayoutPanel4.Controls.Add(lblFontSize, 0, 1);
            tableLayoutPanel4.Controls.Add(dpdFont, 1, 0);
            tableLayoutPanel4.Controls.Add(numFontSize, 1, 1);
            tableLayoutPanel4.Location = new Point(7, 18);
            tableLayoutPanel4.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(191, 59);
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
            dpdFont.Size = new Size(121, 24);
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
            chkViewerShowHelp.Checked = true;
            chkViewerShowHelp.CheckState = CheckState.Checked;
            chkViewerShowHelp.Location = new Point(7, 131);
            chkViewerShowHelp.Margin = new Padding(4, 3, 4, 3);
            chkViewerShowHelp.Name = "chkViewerShowHelp";
            chkViewerShowHelp.Size = new Size(160, 19);
            chkViewerShowHelp.TabIndex = 7;
            chkViewerShowHelp.Text = "Show help text by default";
            chkViewerShowHelp.UseVisualStyleBackColor = true;
            chkViewerShowHelp.CheckedChanged += chkViewerShowHelp_CheckedChanged;
            // 
            // chkFont2DEnable
            // 
            chkFont2DEnable.AutoSize = true;
            chkFont2DEnable.Checked = true;
            chkFont2DEnable.CheckState = CheckState.Checked;
            chkFont2DEnable.Location = new Point(7, 106);
            chkFont2DEnable.Margin = new Padding(4, 3, 4, 3);
            chkFont2DEnable.Name = "chkFont2DEnable";
            chkFont2DEnable.Size = new Size(136, 19);
            chkFont2DEnable.TabIndex = 6;
            chkFont2DEnable.Text = "Show debug console";
            chkFont2DEnable.UseVisualStyleBackColor = true;
            chkFont2DEnable.CheckedChanged += chkFont2DEnable_CheckedChanged;
            // 
            // chkFont3DEnable
            // 
            chkFont3DEnable.AutoSize = true;
            chkFont3DEnable.Checked = true;
            chkFont3DEnable.CheckState = CheckState.Checked;
            chkFont3DEnable.Location = new Point(7, 56);
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
            chkCollisionDisplay.Checked = true;
            chkCollisionDisplay.CheckState = CheckState.Checked;
            chkCollisionDisplay.Location = new Point(7, 31);
            chkCollisionDisplay.Margin = new Padding(4, 3, 4, 3);
            chkCollisionDisplay.Name = "chkCollisionDisplay";
            chkCollisionDisplay.Size = new Size(192, 19);
            chkCollisionDisplay.TabIndex = 2;
            chkCollisionDisplay.Text = "Show collision boxes by default";
            chkCollisionDisplay.UseVisualStyleBackColor = true;
            chkCollisionDisplay.CheckedChanged += chkCollisionDisplay_CheckedChanged;
            // 
            // chkNormalDisplay
            // 
            chkNormalDisplay.AutoSize = true;
            chkNormalDisplay.Checked = true;
            chkNormalDisplay.CheckState = CheckState.Checked;
            chkNormalDisplay.Location = new Point(7, 6);
            chkNormalDisplay.Margin = new Padding(4, 3, 4, 3);
            chkNormalDisplay.Name = "chkNormalDisplay";
            chkNormalDisplay.Size = new Size(226, 19);
            chkNormalDisplay.TabIndex = 0;
            chkNormalDisplay.Text = "(Crash 1) Show normals in animations";
            chkNormalDisplay.UseVisualStyleBackColor = true;
            chkNormalDisplay.CheckedChanged += chkNormalDisplay_CheckedChanged;
            // 
            // fraAnimGrid
            // 
            fraAnimGrid.AutoSize = true;
            fraAnimGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
            numAnimGrid.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
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
            fraFont.Controls.Add(tableLayoutPanel4);
            fraFont.Location = new Point(7, 105);
            fraFont.Margin = new Padding(4, 3, 4, 3);
            fraFont.Name = "fraFont";
            fraFont.Padding = new Padding(4, 3, 4, 3);
            fraFont.Size = new Size(206, 79);
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
            cmdReset.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdReset.Location = new Point(7, 149);
            cmdReset.Margin = new Padding(4, 3, 4, 3);
            cmdReset.Name = "cmdReset";
            cmdReset.Padding = new Padding(5);
            cmdReset.Size = new Size(90, 25);
            cmdReset.TabIndex = 1;
            cmdReset.Text = "Reset Settings";
            cmdReset.Click += cmdReset_Click;
            // 
            // fraSize
            // 
            fraSize.AutoSize = true;
            fraSize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSize.Controls.Add(numW);
            fraSize.Controls.Add(numH);
            fraSize.Controls.Add(lblWH);
            fraSize.Location = new Point(7, 76);
            fraSize.Margin = new Padding(4, 3, 4, 3);
            fraSize.Name = "fraSize";
            fraSize.Padding = new Padding(4, 3, 4, 3);
            fraSize.Size = new Size(147, 67);
            fraSize.TabIndex = 1;
            fraSize.TabStop = false;
            fraSize.Text = "Default Window Size";
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
            fraNodeShadeAmt.Controls.Add(lblNodeShadeAmt);
            fraNodeShadeAmt.Controls.Add(sldNodeShadeAmt);
            fraNodeShadeAmt.Location = new Point(7, 203);
            fraNodeShadeAmt.Margin = new Padding(4, 3, 4, 3);
            fraNodeShadeAmt.Name = "fraNodeShadeAmt";
            fraNodeShadeAmt.Padding = new Padding(4, 3, 4, 3);
            fraNodeShadeAmt.Size = new Size(276, 90);
            fraNodeShadeAmt.TabIndex = 10;
            fraNodeShadeAmt.TabStop = false;
            fraNodeShadeAmt.Text = "Collision Node Shade Amount";
            // 
            // lblNodeShadeAmt
            // 
            lblNodeShadeAmt.Dock = DockStyle.Top;
            lblNodeShadeAmt.Font = new Font("Microsoft Sans Serif", 10F);
            lblNodeShadeAmt.Location = new Point(4, 64);
            lblNodeShadeAmt.Name = "lblNodeShadeAmt";
            lblNodeShadeAmt.Size = new Size(268, 23);
            lblNodeShadeAmt.TabIndex = 11;
            lblNodeShadeAmt.Text = "100%";
            lblNodeShadeAmt.TextAlign = ContentAlignment.TopCenter;
            // 
            // sldNodeShadeAmt
            // 
            sldNodeShadeAmt.BackColor = Color.FromArgb(30, 30, 30);
            sldNodeShadeAmt.Dock = DockStyle.Top;
            sldNodeShadeAmt.LargeChange = 10;
            sldNodeShadeAmt.Location = new Point(4, 19);
            sldNodeShadeAmt.Margin = new Padding(4, 3, 4, 3);
            sldNodeShadeAmt.Maximum = 100;
            sldNodeShadeAmt.Name = "sldNodeShadeAmt";
            sldNodeShadeAmt.Size = new Size(268, 45);
            sldNodeShadeAmt.TabIndex = 0;
            sldNodeShadeAmt.TickFrequency = 5;
            sldNodeShadeAmt.Value = 20;
            sldNodeShadeAmt.Scroll += sldNodeShadeAmt_Scroll;
            // 
            // tbcSettings
            // 
            tbcSettings.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tbcSettings.AnimateTime = 200;
            tbcSettings.BackgroundColor = Color.FromArgb(30, 30, 30);
            tbcSettings.Controls.Add(tbpGeneral);
            tbcSettings.Controls.Add(tbp3D);
            tbcSettings.Controls.Add(tbpDebugDisplay);
            tbcSettings.Controls.Add(tbpPatchNSD);
            tbcSettings.Controls.Add(tbpMisc);
            tbcSettings.Dock = DockStyle.Fill;
            tbcSettings.IsDerivedStyle = true;
            tbcSettings.ItemSize = new Size(100, 28);
            tbcSettings.Location = new Point(4, 3);
            tbcSettings.Name = "tbcSettings";
            tbcSettings.SelectedIndex = 0;
            tbcSettings.SelectedTextColor = Color.White;
            tbcSettings.Size = new Size(425, 417);
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
            tbpGeneral.BackColor = Color.FromArgb(30, 30, 30);
            tbpGeneral.Controls.Add(fraLang);
            tbpGeneral.Controls.Add(cmdReset);
            tbpGeneral.Controls.Add(fraSize);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Padding = new Padding(3);
            tbpGeneral.Size = new Size(417, 381);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            // 
            // fraLang
            // 
            fraLang.Controls.Add(dpdLang);
            fraLang.Location = new Point(7, 6);
            fraLang.Name = "fraLang";
            fraLang.Size = new Size(172, 64);
            fraLang.TabIndex = 2;
            fraLang.TabStop = false;
            fraLang.Text = "Language (requires restart)";
            // 
            // tbp3D
            // 
            tbp3D.BackColor = Color.FromArgb(30, 30, 30);
            tbp3D.Controls.Add(fraNodeShadeAmt);
            tbp3D.Controls.Add(fraFont);
            tbp3D.Controls.Add(fraClearCol);
            tbp3D.Controls.Add(fraAnimGrid);
            tbp3D.Location = new Point(4, 32);
            tbp3D.Name = "tbp3D";
            tbp3D.Padding = new Padding(3);
            tbp3D.Size = new Size(417, 381);
            tbp3D.TabIndex = 1;
            tbp3D.Text = "3D Viewer";
            // 
            // tbpDebugDisplay
            // 
            tbpDebugDisplay.BackColor = Color.FromArgb(30, 30, 30);
            tbpDebugDisplay.Controls.Add(chkShowEntityParams);
            tbpDebugDisplay.Controls.Add(chkDisableVisual);
            tbpDebugDisplay.Controls.Add(chkViewCamera);
            tbpDebugDisplay.Controls.Add(chkViewCameraAngle);
            tbpDebugDisplay.Controls.Add(chkViewZoneName);
            tbpDebugDisplay.Controls.Add(chkViewZoneBox);
            tbpDebugDisplay.Controls.Add(chkViewerShowHelp);
            tbpDebugDisplay.Controls.Add(chkFont2DEnable);
            tbpDebugDisplay.Controls.Add(chkNormalDisplay);
            tbpDebugDisplay.Controls.Add(chkCollisionDisplay);
            tbpDebugDisplay.Controls.Add(chkFont3DEnable);
            tbpDebugDisplay.Location = new Point(4, 32);
            tbpDebugDisplay.Name = "tbpDebugDisplay";
            tbpDebugDisplay.Padding = new Padding(3);
            tbpDebugDisplay.Size = new Size(417, 381);
            tbpDebugDisplay.TabIndex = 3;
            tbpDebugDisplay.Text = "Debug Displays";
            // 
            // chkShowEntityParams
            // 
            chkShowEntityParams.AutoSize = true;
            chkShowEntityParams.Checked = true;
            chkShowEntityParams.CheckState = CheckState.Checked;
            chkShowEntityParams.Location = new Point(7, 81);
            chkShowEntityParams.Margin = new Padding(4, 3, 4, 3);
            chkShowEntityParams.Name = "chkShowEntityParams";
            chkShowEntityParams.Size = new Size(150, 19);
            chkShowEntityParams.TabIndex = 13;
            chkShowEntityParams.Text = "Show entity parameters";
            chkShowEntityParams.UseVisualStyleBackColor = true;
            chkShowEntityParams.CheckedChanged += chkShowEntityParams_CheckedChanged;
            // 
            // chkDisableVisual
            // 
            chkDisableVisual.AutoSize = true;
            chkDisableVisual.Location = new Point(7, 256);
            chkDisableVisual.Name = "chkDisableVisual";
            chkDisableVisual.Size = new Size(154, 19);
            chkDisableVisual.TabIndex = 12;
            chkDisableVisual.Text = "Disable 3D entity display";
            chkDisableVisual.UseVisualStyleBackColor = true;
            chkDisableVisual.CheckedChanged += chkDisableVisual_CheckedChanged;
            // 
            // chkViewCamera
            // 
            chkViewCamera.AutoSize = true;
            chkViewCamera.Checked = true;
            chkViewCamera.CheckState = CheckState.Checked;
            chkViewCamera.Location = new Point(7, 206);
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
            chkViewCameraAngle.Location = new Point(7, 231);
            chkViewCameraAngle.Name = "chkViewCameraAngle";
            chkViewCameraAngle.Size = new Size(167, 19);
            chkViewCameraAngle.TabIndex = 10;
            chkViewCameraAngle.Text = "Show camera entity angles";
            chkViewCameraAngle.UseVisualStyleBackColor = true;
            chkViewCameraAngle.CheckedChanged += chkViewCameraAngle_CheckedChanged;
            // 
            // chkViewZoneName
            // 
            chkViewZoneName.AutoSize = true;
            chkViewZoneName.Checked = true;
            chkViewZoneName.CheckState = CheckState.Checked;
            chkViewZoneName.Location = new Point(7, 181);
            chkViewZoneName.Name = "chkViewZoneName";
            chkViewZoneName.Size = new Size(121, 19);
            chkViewZoneName.TabIndex = 9;
            chkViewZoneName.Text = "Show zone names";
            chkViewZoneName.UseVisualStyleBackColor = true;
            chkViewZoneName.CheckedChanged += chkViewZoneName_CheckedChanged;
            // 
            // chkViewZoneBox
            // 
            chkViewZoneBox.AutoSize = true;
            chkViewZoneBox.Checked = true;
            chkViewZoneBox.CheckState = CheckState.Checked;
            chkViewZoneBox.Location = new Point(7, 156);
            chkViewZoneBox.Name = "chkViewZoneBox";
            chkViewZoneBox.Size = new Size(145, 19);
            chkViewZoneBox.TabIndex = 8;
            chkViewZoneBox.Text = "Show zone boundaries";
            chkViewZoneBox.UseVisualStyleBackColor = true;
            chkViewZoneBox.CheckedChanged += chkViewZoneBox_CheckedChanged;
            // 
            // tbpPatchNSD
            // 
            tbpPatchNSD.BackColor = Color.FromArgb(30, 30, 30);
            tbpPatchNSD.Controls.Add(chkDeleteInvalidEntries);
            tbpPatchNSD.Controls.Add(chkPatchNSDSavesNSF);
            tbpPatchNSD.Location = new Point(4, 32);
            tbpPatchNSD.Name = "tbpPatchNSD";
            tbpPatchNSD.Padding = new Padding(3);
            tbpPatchNSD.Size = new Size(417, 381);
            tbpPatchNSD.TabIndex = 2;
            tbpPatchNSD.Text = "Patch NSD";
            // 
            // tbpMisc
            // 
            tbpMisc.BackColor = Color.FromArgb(30, 30, 30);
            tbpMisc.Controls.Add(chkSplitViewerPanels);
            tbpMisc.Controls.Add(chkEnableC2TT);
            tbpMisc.Controls.Add(chkShowCustomCrates);
            tbpMisc.Controls.Add(chkLiteralCollisionTypes);
            tbpMisc.Controls.Add(chkPatchGOOLC3toC2);
            tbpMisc.Controls.Add(chkOldPatchNSD);
            tbpMisc.Location = new Point(4, 32);
            tbpMisc.Name = "tbpMisc";
            tbpMisc.Padding = new Padding(3);
            tbpMisc.Size = new Size(417, 381);
            tbpMisc.TabIndex = 2;
            tbpMisc.Text = "Misc";
            // 
            // chkEnableC2TT
            // 
            chkEnableC2TT.AutoSize = true;
            chkEnableC2TT.Location = new Point(7, 81);
            chkEnableC2TT.Name = "chkEnableC2TT";
            chkEnableC2TT.Size = new Size(187, 19);
            chkEnableC2TT.TabIndex = 16;
            chkEnableC2TT.Text = "Enable Crash 2 time trial editor";
            chkEnableC2TT.UseVisualStyleBackColor = true;
            chkEnableC2TT.CheckedChanged += chkEnableC2TT_CheckedChanged;
            // 
            // chkShowCustomCrates
            // 
            chkShowCustomCrates.AutoSize = true;
            chkShowCustomCrates.Location = new Point(7, 56);
            chkShowCustomCrates.Name = "chkShowCustomCrates";
            chkShowCustomCrates.Size = new Size(132, 19);
            chkShowCustomCrates.TabIndex = 15;
            chkShowCustomCrates.Text = "Show custom crates";
            chkShowCustomCrates.UseVisualStyleBackColor = true;
            chkShowCustomCrates.CheckedChanged += chkShowCustomCrates_CheckedChanged;
            // 
            // chkLiteralCollisionTypes
            // 
            chkLiteralCollisionTypes.AutoSize = true;
            chkLiteralCollisionTypes.Location = new Point(7, 31);
            chkLiteralCollisionTypes.Name = "chkLiteralCollisionTypes";
            chkLiteralCollisionTypes.Size = new Size(193, 19);
            chkLiteralCollisionTypes.TabIndex = 14;
            chkLiteralCollisionTypes.Text = "Show literal zone collision types";
            chkLiteralCollisionTypes.UseVisualStyleBackColor = true;
            chkLiteralCollisionTypes.CheckedChanged += chkDetailedCollision_CheckedChanged;
            // 
            // chkPatchGOOLC3toC2
            // 
            chkPatchGOOLC3toC2.AutoSize = true;
            chkPatchGOOLC3toC2.Location = new Point(7, 6);
            chkPatchGOOLC3toC2.Margin = new Padding(4, 3, 4, 3);
            chkPatchGOOLC3toC2.Name = "chkPatchGOOLC3toC2";
            chkPatchGOOLC3toC2.Size = new Size(421, 19);
            chkPatchGOOLC3toC2.TabIndex = 8;
            chkPatchGOOLC3toC2.Text = "(Crash 2 ) Patch GOOLs frame groups ported from Crash 3 (requires restart)";
            chkPatchGOOLC3toC2.UseVisualStyleBackColor = true;
            chkPatchGOOLC3toC2.CheckedChanged += chkPatchGOOLC3toC2_CheckedChanged;
            chkPatchGOOLC3toC2.Click += chkPatchGOOLC3toC2_Click;
            // 
            // chkOldPatchNSD
            // 
            chkOldPatchNSD.AutoSize = true;
            chkOldPatchNSD.Location = new Point(7, 106);
            chkOldPatchNSD.Margin = new Padding(4, 3, 4, 3);
            chkOldPatchNSD.Name = "chkOldPatchNSD";
            chkOldPatchNSD.Size = new Size(271, 19);
            chkOldPatchNSD.TabIndex = 9;
            chkOldPatchNSD.Text = "Use old NSD patching from CrashEdit v0.2.49.0";
            chkOldPatchNSD.UseVisualStyleBackColor = true;
            chkOldPatchNSD.CheckedChanged += chkOldPatchNSD_CheckedChanged;
            // 
            // chkSplitViewerPanels
            // 
            chkSplitViewerPanels.AutoSize = true;
            chkSplitViewerPanels.Location = new Point(7, 131);
            chkSplitViewerPanels.Margin = new Padding(4, 3, 4, 3);
            chkSplitViewerPanels.Name = "chkSplitViewerPanels";
            chkSplitViewerPanels.Size = new Size(123, 19);
            chkSplitViewerPanels.TabIndex = 17;
            chkSplitViewerPanels.Text = "Split viewer panels";
            chkSplitViewerPanels.UseVisualStyleBackColor = true;
            chkSplitViewerPanels.CheckedChanged += chkSplitViewerPanels_CheckedChanged;
            // 
            // ConfigEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(30, 30, 30);
            Controls.Add(tbcSettings);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ConfigEditor";
            Padding = new Padding(4, 3, 4, 3);
            Size = new Size(433, 423);
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
            fraNodeShadeAmt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sldNodeShadeAmt).EndInit();
            tbcSettings.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            tbpGeneral.PerformLayout();
            fraLang.ResumeLayout(false);
            tbp3D.ResumeLayout(false);
            tbp3D.PerformLayout();
            tbpDebugDisplay.ResumeLayout(false);
            tbpDebugDisplay.PerformLayout();
            tbpPatchNSD.ResumeLayout(false);
            tbpPatchNSD.PerformLayout();
            tbpMisc.ResumeLayout(false);
            tbpMisc.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DarkButton cmdReset;
        private DarkGroupBox fraSize;
        private Label lblWH;
        private DarkNumericUpDown numH;
        private DarkNumericUpDown numW;
        private System.Windows.Forms.ColorDialog cdlClearCol;
        private DarkGroupBox fraClearCol;
        private System.Windows.Forms.PictureBox picClearCol;
        private DarkGroupBox fraAnimGrid;
        private DarkNumericUpDown numAnimGrid;
        private Label lblAnimGrid;
        private System.Windows.Forms.CheckBox chkAnimGrid;
        private DarkComboBox dpdFont;
        private DarkGroupBox fraFont;
        private DarkNumericUpDown numFontSize;
        private Label lblFontSize;
        private Label lblFontName;
        private DarkGroupBox fraNodeShadeAmt;
        private System.Windows.Forms.TrackBar sldNodeShadeAmt;
        private System.Windows.Forms.CheckBox chkViewerShowHelp;
        private System.Windows.Forms.CheckBox chkFont2DEnable;
        private System.Windows.Forms.CheckBox chkFont3DEnable;
        private System.Windows.Forms.CheckBox chkCollisionDisplay;
        private System.Windows.Forms.CheckBox chkNormalDisplay;
        private System.Windows.Forms.CheckBox chkPatchNSDSavesNSF;
        private System.Windows.Forms.CheckBox chkDeleteInvalidEntries;
        private DarkComboBox dpdLang;
        private Label lblNodeShadeAmt;
        private MetroSetTabControl tbcSettings;
        private TabPage tbpGeneral;
        private TabPage tbp3D;
        private TabPage tbpPatchNSD;
        private TabPage tbpDebugDisplay;
        private TabPage tbpMisc;
        private CheckBox chkViewZoneBox;
        private CheckBox chkViewCameraAngle;
        private CheckBox chkViewZoneName;
        private CheckBox chkViewCamera;
        private CheckBox chkDisableVisual;
        private DarkGroupBox fraLang;
        private CheckBox chkShowEntityParams;
        private CheckBox chkOldPatchNSD;
        private CheckBox chkLiteralCollisionTypes;
        private CheckBox chkShowCustomCrates;
        private CheckBox chkEnableC2TT;
        private CheckBox chkPatchGOOLC3toC2;
        private CheckBox chkSplitViewerPanels;
    }
}
