using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class OldModelBox
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            tabOldModel = new MetroSet_UI.Controls.MetroSetTabControl();
            tbpGeneral = new TabPage();
            panel1 = new Panel();
            fraScales = new DarkGroupBox();
            chkScalesShowAsHex = new CheckBox();
            numScaleZ = new DarkNumericUpDown();
            lblScaleZ = new Label();
            numScaleY = new DarkNumericUpDown();
            lblScaleY = new Label();
            numScaleX = new DarkNumericUpDown();
            lblScaleX = new Label();
            lblModelInfo = new Label();
            tbpPolygons = new TabPage();
            dgvPolygons = new DataGridView();
            tbpTextures = new TabPage();
            pnPicture = new Panel();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            pnTextureControls = new Panel();
            chkMaxValueFlag = new CheckBox();
            fraSwitches = new DarkGroupBox();
            tglSimpleMode = new MetroSet_UI.Controls.MetroSetSwitch();
            fraReplaceTexture = new DarkGroupBox();
            cmdReplaceTexture = new DarkButton();
            chkReplaceCLUT = new CheckBox();
            chkBGRA = new CheckBox();
            numRowIndex = new DarkNumericUpDown();
            cmdLoadTexture = new DarkButton();
            fraReplace = new DarkGroupBox();
            label1 = new Label();
            numReplaceTo = new DarkNumericUpDown();
            numReplace = new DarkNumericUpDown();
            cmdReplace = new DarkButton();
            fraTPage = new DarkGroupBox();
            rbtReloadTPage = new MetroSet_UI.Controls.MetroSetRadioButton();
            dpdTPage = new DarkComboBox();
            trkPictureSize = new MetroSet_UI.Controls.MetroSetTrackBar();
            dgvTextures = new DataGridView();
            tabOldModel.SuspendLayout();
            tbpGeneral.SuspendLayout();
            panel1.SuspendLayout();
            fraScales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleX).BeginInit();
            tbpPolygons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPolygons).BeginInit();
            tbpTextures.SuspendLayout();
            pnPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            pnTextureControls.SuspendLayout();
            fraSwitches.SuspendLayout();
            fraReplaceTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRowIndex).BeginInit();
            fraReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numReplaceTo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReplace).BeginInit();
            fraTPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTextures).BeginInit();
            SuspendLayout();
            // 
            // tabOldModel
            // 
            tabOldModel.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabOldModel.AnimateTime = 200;
            tabOldModel.BackgroundColor = Color.FromArgb(30, 30, 30);
            tabOldModel.Controls.Add(tbpGeneral);
            tabOldModel.Controls.Add(tbpPolygons);
            tabOldModel.Controls.Add(tbpTextures);
            tabOldModel.Dock = DockStyle.Fill;
            tabOldModel.IsDerivedStyle = true;
            tabOldModel.ItemSize = new Size(100, 28);
            tabOldModel.Location = new Point(0, 0);
            tabOldModel.Multiline = true;
            tabOldModel.Name = "tabOldModel";
            tabOldModel.SelectedIndex = 0;
            tabOldModel.SelectedTextColor = Color.White;
            tabOldModel.Size = new Size(1024, 800);
            tabOldModel.SizeMode = TabSizeMode.Fixed;
            tabOldModel.Speed = 100;
            tabOldModel.Style = MetroSet_UI.Enums.Style.Dark;
            tabOldModel.StyleManager = null;
            tabOldModel.TabIndex = 0;
            tabOldModel.ThemeAuthor = "Narwin";
            tabOldModel.ThemeName = "MetroDark";
            tabOldModel.UnselectedTextColor = Color.Gray;
            tabOldModel.UseAnimation = false;
            // 
            // tbpGeneral
            // 
            tbpGeneral.AutoScroll = true;
            tbpGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tbpGeneral.Controls.Add(panel1);
            tbpGeneral.Controls.Add(lblModelInfo);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Size = new Size(1016, 764);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            // 
            // panel1
            // 
            panel1.Controls.Add(fraScales);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(386, 134);
            panel1.TabIndex = 2;
            // 
            // fraScales
            // 
            fraScales.BackColor = Color.Transparent;
            fraScales.Controls.Add(chkScalesShowAsHex);
            fraScales.Controls.Add(numScaleZ);
            fraScales.Controls.Add(lblScaleZ);
            fraScales.Controls.Add(numScaleY);
            fraScales.Controls.Add(lblScaleY);
            fraScales.Controls.Add(numScaleX);
            fraScales.Controls.Add(lblScaleX);
            fraScales.Dock = DockStyle.Left;
            fraScales.Location = new Point(0, 0);
            fraScales.Name = "fraScales";
            fraScales.Size = new Size(190, 134);
            fraScales.TabIndex = 2;
            fraScales.TabStop = false;
            fraScales.Text = "Scales";
            // 
            // chkScalesShowAsHex
            // 
            chkScalesShowAsHex.AutoSize = true;
            chkScalesShowAsHex.Checked = true;
            chkScalesShowAsHex.CheckState = CheckState.Checked;
            chkScalesShowAsHex.Location = new Point(56, 104);
            chkScalesShowAsHex.Name = "chkScalesShowAsHex";
            chkScalesShowAsHex.Size = new Size(47, 19);
            chkScalesShowAsHex.TabIndex = 2;
            chkScalesShowAsHex.Text = "Hex";
            chkScalesShowAsHex.UseVisualStyleBackColor = true;
            chkScalesShowAsHex.CheckedChanged += chkScalesShowAsHex_CheckedChanged;
            // 
            // numScaleZ
            // 
            numScaleZ.Hexadecimal = true;
            numScaleZ.Location = new Point(56, 75);
            numScaleZ.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numScaleZ.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numScaleZ.Name = "numScaleZ";
            numScaleZ.Size = new Size(122, 23);
            numScaleZ.TabIndex = 1;
            numScaleZ.ValueChanged += numScaleZ_ValueChanged;
            // 
            // lblScaleZ
            // 
            lblScaleZ.AutoSize = true;
            lblScaleZ.BackColor = Color.Transparent;
            lblScaleZ.Location = new Point(6, 77);
            lblScaleZ.Name = "lblScaleZ";
            lblScaleZ.Size = new Size(44, 15);
            lblScaleZ.TabIndex = 0;
            lblScaleZ.Text = "Scale Z";
            // 
            // numScaleY
            // 
            numScaleY.Hexadecimal = true;
            numScaleY.Location = new Point(56, 46);
            numScaleY.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numScaleY.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numScaleY.Name = "numScaleY";
            numScaleY.Size = new Size(122, 23);
            numScaleY.TabIndex = 1;
            numScaleY.ValueChanged += numScaleY_ValueChanged;
            // 
            // lblScaleY
            // 
            lblScaleY.AutoSize = true;
            lblScaleY.BackColor = Color.Transparent;
            lblScaleY.Location = new Point(6, 48);
            lblScaleY.Name = "lblScaleY";
            lblScaleY.Size = new Size(44, 15);
            lblScaleY.TabIndex = 0;
            lblScaleY.Text = "Scale Y";
            // 
            // numScaleX
            // 
            numScaleX.Hexadecimal = true;
            numScaleX.Location = new Point(56, 17);
            numScaleX.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numScaleX.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numScaleX.Name = "numScaleX";
            numScaleX.Size = new Size(122, 23);
            numScaleX.TabIndex = 1;
            numScaleX.ValueChanged += numScaleX_ValueChanged;
            // 
            // lblScaleX
            // 
            lblScaleX.AutoSize = true;
            lblScaleX.BackColor = Color.Transparent;
            lblScaleX.Location = new Point(6, 19);
            lblScaleX.Name = "lblScaleX";
            lblScaleX.Size = new Size(44, 15);
            lblScaleX.TabIndex = 0;
            lblScaleX.Text = "Scale X";
            // 
            // lblModelInfo
            // 
            lblModelInfo.AutoSize = true;
            lblModelInfo.BackColor = Color.Transparent;
            lblModelInfo.Location = new Point(9, 140);
            lblModelInfo.Name = "lblModelInfo";
            lblModelInfo.Size = new Size(184, 45);
            lblModelInfo.TabIndex = 0;
            lblModelInfo.Text = "Polygon count: {0}\r\nVertex count: {1}\r\nCompression ratio: {2:P1} ({3}/{4})";
            lblModelInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbpPolygons
            // 
            tbpPolygons.AutoScroll = true;
            tbpPolygons.BackColor = Color.FromArgb(31, 31, 32);
            tbpPolygons.Controls.Add(dgvPolygons);
            tbpPolygons.Location = new Point(4, 32);
            tbpPolygons.Name = "tbpPolygons";
            tbpPolygons.Size = new Size(1016, 764);
            tbpPolygons.TabIndex = 1;
            tbpPolygons.Text = "Polygons";
            tbpPolygons.Enter += tbpPolygons_Enter;
            // 
            // dgvPolygons
            // 
            dgvPolygons.AllowUserToAddRows = false;
            dgvPolygons.AllowUserToResizeColumns = false;
            dgvPolygons.AllowUserToResizeRows = false;
            dgvPolygons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPolygons.ColumnHeadersHeight = 24;
            dgvPolygons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPolygons.Location = new Point(3, 3);
            dgvPolygons.Name = "dgvPolygons";
            dgvPolygons.RowHeadersWidth = 24;
            dgvPolygons.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPolygons.ShowCellToolTips = false;
            dgvPolygons.Size = new Size(597, 425);
            dgvPolygons.TabIndex = 0;
            dgvPolygons.CellValueChanged += dgvPolygons_CellValueChanged;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Controls.Add(pnPicture);
            tbpTextures.Controls.Add(panel2);
            tbpTextures.Location = new Point(4, 32);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(1016, 764);
            tbpTextures.TabIndex = 3;
            tbpTextures.Text = "Textures";
            tbpTextures.Enter += tbpTextures_Enter;
            // 
            // pnPicture
            // 
            pnPicture.Controls.Add(pictureBox1);
            pnPicture.Dock = DockStyle.Top;
            pnPicture.Location = new Point(0, 378);
            pnPicture.Name = "pnPicture";
            pnPicture.Size = new Size(1016, 152);
            pnPicture.TabIndex = 14;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(4, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1024, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(pnTextureControls);
            panel2.Controls.Add(fraTPage);
            panel2.Controls.Add(trkPictureSize);
            panel2.Controls.Add(dgvTextures);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1016, 378);
            panel2.TabIndex = 13;
            // 
            // pnTextureControls
            // 
            pnTextureControls.Controls.Add(chkMaxValueFlag);
            pnTextureControls.Controls.Add(fraSwitches);
            pnTextureControls.Controls.Add(fraReplaceTexture);
            pnTextureControls.Controls.Add(numRowIndex);
            pnTextureControls.Controls.Add(cmdLoadTexture);
            pnTextureControls.Controls.Add(fraReplace);
            pnTextureControls.Location = new Point(703, 0);
            pnTextureControls.Name = "pnTextureControls";
            pnTextureControls.Size = new Size(214, 372);
            pnTextureControls.TabIndex = 13;
            pnTextureControls.Visible = false;
            // 
            // chkMaxValueFlag
            // 
            chkMaxValueFlag.AutoSize = true;
            chkMaxValueFlag.Enabled = false;
            chkMaxValueFlag.Location = new Point(3, 61);
            chkMaxValueFlag.Name = "chkMaxValueFlag";
            chkMaxValueFlag.Size = new Size(83, 19);
            chkMaxValueFlag.TabIndex = 11;
            chkMaxValueFlag.Text = "RegionEnd";
            chkMaxValueFlag.UseVisualStyleBackColor = true;
            // 
            // fraSwitches
            // 
            fraSwitches.BackColor = Color.Transparent;
            fraSwitches.Controls.Add(tglSimpleMode);
            fraSwitches.Enabled = false;
            fraSwitches.Location = new Point(3, 3);
            fraSwitches.Name = "fraSwitches";
            fraSwitches.Size = new Size(107, 52);
            fraSwitches.TabIndex = 7;
            fraSwitches.TabStop = false;
            fraSwitches.Text = "Simple View";
            // 
            // tglSimpleMode
            // 
            tglSimpleMode.BackColor = Color.Transparent;
            tglSimpleMode.BackgroundColor = Color.Empty;
            tglSimpleMode.BorderColor = Color.FromArgb(155, 155, 155);
            tglSimpleMode.CheckColor = Color.FromArgb(65, 177, 225);
            tglSimpleMode.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            tglSimpleMode.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            tglSimpleMode.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            tglSimpleMode.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            tglSimpleMode.IsDerivedStyle = true;
            tglSimpleMode.Location = new Point(16, 22);
            tglSimpleMode.Name = "tglSimpleMode";
            tglSimpleMode.Size = new Size(58, 22);
            tglSimpleMode.Style = MetroSet_UI.Enums.Style.Dark;
            tglSimpleMode.StyleManager = null;
            tglSimpleMode.Switched = false;
            tglSimpleMode.SymbolColor = Color.FromArgb(92, 92, 92);
            tglSimpleMode.TabIndex = 2;
            tglSimpleMode.Text = "Toggle simple mode";
            tglSimpleMode.ThemeAuthor = "Narwin";
            tglSimpleMode.ThemeName = "MetroDark";
            tglSimpleMode.UnCheckColor = Color.FromArgb(155, 155, 155);
            // 
            // fraReplaceTexture
            // 
            fraReplaceTexture.BackColor = Color.Transparent;
            fraReplaceTexture.Controls.Add(cmdReplaceTexture);
            fraReplaceTexture.Controls.Add(chkReplaceCLUT);
            fraReplaceTexture.Controls.Add(chkBGRA);
            fraReplaceTexture.Enabled = false;
            fraReplaceTexture.Location = new Point(3, 219);
            fraReplaceTexture.Name = "fraReplaceTexture";
            fraReplaceTexture.Size = new Size(121, 109);
            fraReplaceTexture.TabIndex = 9;
            fraReplaceTexture.TabStop = false;
            fraReplaceTexture.Text = "Replace Texture";
            // 
            // cmdReplaceTexture
            // 
            cmdReplaceTexture.BorderColour = Color.Empty;
            cmdReplaceTexture.CustomColour = false;
            cmdReplaceTexture.FlatBottom = false;
            cmdReplaceTexture.FlatTop = false;
            cmdReplaceTexture.Location = new Point(16, 22);
            cmdReplaceTexture.Name = "cmdReplaceTexture";
            cmdReplaceTexture.Padding = new Padding(5);
            cmdReplaceTexture.Size = new Size(75, 23);
            cmdReplaceTexture.TabIndex = 5;
            cmdReplaceTexture.Text = "Browse...";
            // 
            // chkReplaceCLUT
            // 
            chkReplaceCLUT.AutoSize = true;
            chkReplaceCLUT.BackColor = Color.Transparent;
            chkReplaceCLUT.Checked = true;
            chkReplaceCLUT.CheckState = CheckState.Checked;
            chkReplaceCLUT.Location = new Point(6, 51);
            chkReplaceCLUT.Name = "chkReplaceCLUT";
            chkReplaceCLUT.Size = new Size(98, 19);
            chkReplaceCLUT.TabIndex = 8;
            chkReplaceCLUT.Text = "Replace CLUT";
            chkReplaceCLUT.UseVisualStyleBackColor = false;
            // 
            // chkBGRA
            // 
            chkBGRA.AutoSize = true;
            chkBGRA.BackColor = Color.Transparent;
            chkBGRA.Checked = true;
            chkBGRA.CheckState = CheckState.Checked;
            chkBGRA.Location = new Point(6, 76);
            chkBGRA.Name = "chkBGRA";
            chkBGRA.Size = new Size(95, 19);
            chkBGRA.TabIndex = 8;
            chkBGRA.Text = "BGRA format";
            chkBGRA.UseVisualStyleBackColor = false;
            // 
            // numRowIndex
            // 
            numRowIndex.Location = new Point(116, 3);
            numRowIndex.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numRowIndex.Name = "numRowIndex";
            numRowIndex.Size = new Size(95, 23);
            numRowIndex.TabIndex = 6;
            numRowIndex.Visible = false;
            // 
            // cmdLoadTexture
            // 
            cmdLoadTexture.BackColor = Color.Transparent;
            cmdLoadTexture.BorderColour = Color.Empty;
            cmdLoadTexture.CustomColour = false;
            cmdLoadTexture.FlatBottom = false;
            cmdLoadTexture.FlatTop = false;
            cmdLoadTexture.Location = new Point(116, 32);
            cmdLoadTexture.Name = "cmdLoadTexture";
            cmdLoadTexture.Padding = new Padding(5);
            cmdLoadTexture.Size = new Size(75, 23);
            cmdLoadTexture.TabIndex = 10;
            cmdLoadTexture.Text = "Load";
            cmdLoadTexture.Visible = false;
            // 
            // fraReplace
            // 
            fraReplace.BackColor = Color.Transparent;
            fraReplace.Controls.Add(label1);
            fraReplace.Controls.Add(numReplaceTo);
            fraReplace.Controls.Add(numReplace);
            fraReplace.Controls.Add(cmdReplace);
            fraReplace.Enabled = false;
            fraReplace.Location = new Point(3, 86);
            fraReplace.Name = "fraReplace";
            fraReplace.Size = new Size(107, 127);
            fraReplace.TabIndex = 6;
            fraReplace.TabStop = false;
            fraReplace.Text = "Replace Values";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 48);
            label1.Name = "label1";
            label1.Size = new Size(13, 15);
            label1.TabIndex = 7;
            label1.Text = "↓";
            // 
            // numReplaceTo
            // 
            numReplaceTo.Location = new Point(6, 66);
            numReplaceTo.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numReplaceTo.Name = "numReplaceTo";
            numReplaceTo.Size = new Size(95, 23);
            numReplaceTo.TabIndex = 6;
            // 
            // numReplace
            // 
            numReplace.Enabled = false;
            numReplace.InterceptArrowKeys = false;
            numReplace.Location = new Point(6, 22);
            numReplace.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numReplace.Name = "numReplace";
            numReplace.ReadOnly = true;
            numReplace.Size = new Size(95, 23);
            numReplace.TabIndex = 6;
            // 
            // cmdReplace
            // 
            cmdReplace.BorderColour = Color.Empty;
            cmdReplace.CustomColour = false;
            cmdReplace.FlatBottom = false;
            cmdReplace.FlatTop = false;
            cmdReplace.Location = new Point(16, 95);
            cmdReplace.Name = "cmdReplace";
            cmdReplace.Padding = new Padding(5);
            cmdReplace.Size = new Size(75, 23);
            cmdReplace.TabIndex = 5;
            cmdReplace.Text = "Replace";
            // 
            // fraTPage
            // 
            fraTPage.Controls.Add(rbtReloadTPage);
            fraTPage.Controls.Add(dpdTPage);
            fraTPage.Enabled = false;
            fraTPage.Location = new Point(3, 3);
            fraTPage.Name = "fraTPage";
            fraTPage.Size = new Size(132, 77);
            fraTPage.TabIndex = 0;
            fraTPage.TabStop = false;
            fraTPage.Text = "Texture Pages";
            // 
            // rbtReloadTPage
            // 
            rbtReloadTPage.BackgroundColor = Color.FromArgb(30, 30, 30);
            rbtReloadTPage.BorderColor = Color.FromArgb(155, 155, 155);
            rbtReloadTPage.Checked = true;
            rbtReloadTPage.CheckSignColor = Color.FromArgb(65, 177, 225);
            rbtReloadTPage.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            rbtReloadTPage.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rbtReloadTPage.Enabled = false;
            rbtReloadTPage.Font = new Font("Microsoft Sans Serif", 10F);
            rbtReloadTPage.Group = 0;
            rbtReloadTPage.IsDerivedStyle = true;
            rbtReloadTPage.Location = new Point(107, 52);
            rbtReloadTPage.Name = "rbtReloadTPage";
            rbtReloadTPage.Size = new Size(19, 17);
            rbtReloadTPage.Style = MetroSet_UI.Enums.Style.Dark;
            rbtReloadTPage.StyleManager = null;
            rbtReloadTPage.TabIndex = 10;
            rbtReloadTPage.ThemeAuthor = "Narwin";
            rbtReloadTPage.ThemeName = "MetroDark";
            rbtReloadTPage.Click += rbtReloadTPage_Click;
            // 
            // dpdTPage
            // 
            dpdTPage.DrawMode = DrawMode.OwnerDrawVariable;
            dpdTPage.Enabled = false;
            dpdTPage.Location = new Point(6, 22);
            dpdTPage.MaxLength = 5;
            dpdTPage.Name = "dpdTPage";
            dpdTPage.Size = new Size(120, 24);
            dpdTPage.TabIndex = 2;
            dpdTPage.SelectedIndexChanged += dpdTPage_SelectedIndexChanged;
            // 
            // trkPictureSize
            // 
            trkPictureSize.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkPictureSize.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkPictureSize.DisabledBorderColor = Color.Empty;
            trkPictureSize.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkPictureSize.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkPictureSize.HandlerColor = Color.FromArgb(143, 143, 143);
            trkPictureSize.IsDerivedStyle = true;
            trkPictureSize.Location = new Point(3, 359);
            trkPictureSize.Maximum = 100;
            trkPictureSize.Minimum = 80;
            trkPictureSize.Name = "trkPictureSize";
            trkPictureSize.Size = new Size(75, 16);
            trkPictureSize.Style = MetroSet_UI.Enums.Style.Dark;
            trkPictureSize.StyleManager = null;
            trkPictureSize.TabIndex = 12;
            trkPictureSize.Text = "metroSetTrackBar1";
            trkPictureSize.ThemeAuthor = "Narwin";
            trkPictureSize.ThemeName = "MetroDark";
            trkPictureSize.TickFrequency = 2;
            trkPictureSize.Value = 100;
            trkPictureSize.ValueColor = Color.FromArgb(65, 177, 225);
            trkPictureSize.Visible = false;
            trkPictureSize.ValueChanged += trkPictureSize_ValueChanged;
            // 
            // dgvTextures
            // 
            dgvTextures.AllowUserToAddRows = false;
            dgvTextures.AllowUserToResizeColumns = false;
            dgvTextures.AllowUserToResizeRows = false;
            dgvTextures.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTextures.ColumnHeadersHeight = 24;
            dgvTextures.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTextures.Location = new Point(141, 3);
            dgvTextures.Name = "dgvTextures";
            dgvTextures.RowHeadersWidth = 24;
            dgvTextures.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTextures.ScrollBars = ScrollBars.Vertical;
            dgvTextures.ShowCellToolTips = false;
            dgvTextures.Size = new Size(556, 372);
            dgvTextures.TabIndex = 0;
            dgvTextures.CellBeginEdit += dgvTextures_CellBeginEdit;
            dgvTextures.CellValidating += dgvTextures_CellValidating;
            dgvTextures.CellValueChanged += dgvTextures_CellValueChanged;
            dgvTextures.EditingControlShowing += dgvTextures_EditingControlShowing;
            dgvTextures.SelectionChanged += dgvTextures_SelectionChanged;
            // 
            // OldModelBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(tabOldModel);
            Name = "OldModelBox";
            Size = new Size(1024, 800);
            tabOldModel.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            tbpGeneral.PerformLayout();
            panel1.ResumeLayout(false);
            fraScales.ResumeLayout(false);
            fraScales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleX).EndInit();
            tbpPolygons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPolygons).EndInit();
            tbpTextures.ResumeLayout(false);
            tbpTextures.PerformLayout();
            pnPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            pnTextureControls.ResumeLayout(false);
            pnTextureControls.PerformLayout();
            fraSwitches.ResumeLayout(false);
            fraReplaceTexture.ResumeLayout(false);
            fraReplaceTexture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRowIndex).EndInit();
            fraReplace.ResumeLayout(false);
            fraReplace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numReplaceTo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReplace).EndInit();
            fraTPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTextures).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetTabControl tabOldModel;
        private TabPage tbpGeneral;
        private TabPage tbpPolygons;
        private Panel panel1;
        private Label lblModelInfo;
        private DarkGroupBox fraScales;
        private CheckBox chkScalesShowAsHex;
        private DarkNumericUpDown numScaleZ;
        private Label lblScaleZ;
        private DarkNumericUpDown numScaleY;
        private Label lblScaleY;
        private DarkNumericUpDown numScaleX;
        private Label lblScaleX;
        private DataGridView dgvPolygons;
        private TabPage tbpTextures;
        private Panel pnPicture;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Panel pnTextureControls;
        private CheckBox chkMaxValueFlag;
        private DarkGroupBox fraSwitches;
        private MetroSet_UI.Controls.MetroSetSwitch tglSimpleMode;
        private DarkGroupBox fraReplaceTexture;
        private DarkButton cmdReplaceTexture;
        private CheckBox chkReplaceCLUT;
        private CheckBox chkBGRA;
        private DarkNumericUpDown numRowIndex;
        private DarkButton cmdLoadTexture;
        private DarkGroupBox fraReplace;
        private Label label1;
        private DarkNumericUpDown numReplaceTo;
        private DarkNumericUpDown numReplace;
        private DarkButton cmdReplace;
        private DarkGroupBox fraTPage;
        private DarkComboBox dpdTPage;
        private MetroSet_UI.Controls.MetroSetTrackBar trkPictureSize;
        private DataGridView dgvTextures;
        private MetroSet_UI.Controls.MetroSetRadioButton rbtReloadTPage;
    }
}
