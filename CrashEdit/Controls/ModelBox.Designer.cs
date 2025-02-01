using System.Data.Common;
using System.Drawing.Design;
using System.Windows.Forms;
using AltUI.ColorPicker;
using AltUI.Controls;
using Cyotek.Windows.Forms;
using MetroSet_UI.Controls;

namespace CrashEdit.CE.Controls
{
    partial class ModelBox
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
            tabModel = new MetroSetTabControl();
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
            fraOffsets = new DarkGroupBox();
            chkOffsetsShowAsHex = new CheckBox();
            numOffsetZ = new DarkNumericUpDown();
            lblOffsetZ = new Label();
            numOffsetY = new DarkNumericUpDown();
            lblOffsetY = new Label();
            numOffsetX = new DarkNumericUpDown();
            lblOffsetX = new Label();
            lblModelInfo = new Label();
            tbpPolygons = new TabPage();
            label3 = new Label();
            label2 = new Label();
            btnConvert = new DarkButton();
            dgvStructs = new DataGridView();
            dgvPolygons = new DataGridView();
            tbpColors = new TabPage();
            lblColorIndex = new Label();
            fraGlobalControl = new DarkGroupBox();
            cmdCancel = new DarkButton();
            cmdApply = new DarkButton();
            pnGlobalControl = new Panel();
            colorEditorGlobal = new Cyotek.Windows.Forms.ColorEditor();
            tglGlobalControl = new MetroSetSwitch();
            pnSliders = new Panel();
            fraColorSlider = new DarkGroupBox();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            lstColor = new DoubleBufferedListView();
            tbpTextures = new TabPage();
            pnPicture = new Panel();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            pnTextureControls = new Panel();
            chkMaxValueFlag = new CheckBox();
            fraSwitches = new DarkGroupBox();
            tglSimpleMode = new MetroSetSwitch();
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
            rbtReloadTPage = new MetroSetRadioButton();
            dpdTPage = new DarkComboBox();
            cmdRemoveTPage = new DarkButton();
            cmdAppendTPage = new DarkButton();
            lstTPages = new DoubleBufferedListView();
            trkPictureSize = new MetroSetTrackBar();
            dgvTextures = new DataGridView();
            tbpExtendedTextures = new TabPage();
            panel3 = new Panel();
            dgvExtendedTextures = new DataGridView();
            tbpPositions = new TabPage();
            dgvPositions = new DataGridView();
            tabModel.SuspendLayout();
            tbpGeneral.SuspendLayout();
            panel1.SuspendLayout();
            fraScales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleX).BeginInit();
            fraOffsets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numOffsetZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOffsetY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numOffsetX).BeginInit();
            tbpPolygons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStructs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPolygons).BeginInit();
            tbpColors.SuspendLayout();
            fraGlobalControl.SuspendLayout();
            pnGlobalControl.SuspendLayout();
            pnSliders.SuspendLayout();
            fraColorSlider.SuspendLayout();
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
            tbpExtendedTextures.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExtendedTextures).BeginInit();
            tbpPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPositions).BeginInit();
            SuspendLayout();
            // 
            // tabModel
            // 
            tabModel.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabModel.AnimateTime = 200;
            tabModel.BackgroundColor = Color.FromArgb(31, 31, 32);
            tabModel.Controls.Add(tbpGeneral);
            tabModel.Controls.Add(tbpPolygons);
            tabModel.Controls.Add(tbpColors);
            tabModel.Controls.Add(tbpTextures);
            tabModel.Controls.Add(tbpExtendedTextures);
            tabModel.Controls.Add(tbpPositions);
            tabModel.Dock = DockStyle.Fill;
            tabModel.IsDerivedStyle = false;
            tabModel.ItemSize = new Size(100, 28);
            tabModel.Location = new Point(0, 0);
            tabModel.Multiline = true;
            tabModel.Name = "tabModel";
            tabModel.SelectedIndex = 0;
            tabModel.SelectedTextColor = Color.White;
            tabModel.Size = new Size(1040, 800);
            tabModel.SizeMode = TabSizeMode.Fixed;
            tabModel.Speed = 100;
            tabModel.Style = MetroSet_UI.Enums.Style.Dark;
            tabModel.StyleManager = null;
            tabModel.TabIndex = 0;
            tabModel.ThemeAuthor = "Narwin";
            tabModel.ThemeName = "MetroDark";
            tabModel.UnselectedTextColor = Color.Gray;
            tabModel.UseAnimation = false;
            // 
            // tbpGeneral
            // 
            tbpGeneral.AutoScroll = true;
            tbpGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tbpGeneral.Controls.Add(panel1);
            tbpGeneral.Controls.Add(lblModelInfo);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Size = new Size(1032, 764);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            // 
            // panel1
            // 
            panel1.Controls.Add(fraScales);
            panel1.Controls.Add(fraOffsets);
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
            fraScales.Location = new Point(190, 0);
            fraScales.Name = "fraScales";
            fraScales.Size = new Size(190, 134);
            fraScales.TabIndex = 1;
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
            chkScalesShowAsHex.CheckedChanged += chkScalesAsHex_CheckedChanged;
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
            // fraOffsets
            // 
            fraOffsets.BackColor = Color.Transparent;
            fraOffsets.Controls.Add(chkOffsetsShowAsHex);
            fraOffsets.Controls.Add(numOffsetZ);
            fraOffsets.Controls.Add(lblOffsetZ);
            fraOffsets.Controls.Add(numOffsetY);
            fraOffsets.Controls.Add(lblOffsetY);
            fraOffsets.Controls.Add(numOffsetX);
            fraOffsets.Controls.Add(lblOffsetX);
            fraOffsets.Dock = DockStyle.Left;
            fraOffsets.Location = new Point(0, 0);
            fraOffsets.Name = "fraOffsets";
            fraOffsets.Size = new Size(190, 134);
            fraOffsets.TabIndex = 1;
            fraOffsets.TabStop = false;
            fraOffsets.Text = "Offsets";
            // 
            // chkOffsetsShowAsHex
            // 
            chkOffsetsShowAsHex.AutoSize = true;
            chkOffsetsShowAsHex.Checked = true;
            chkOffsetsShowAsHex.CheckState = CheckState.Checked;
            chkOffsetsShowAsHex.Location = new Point(56, 104);
            chkOffsetsShowAsHex.Name = "chkOffsetsShowAsHex";
            chkOffsetsShowAsHex.Size = new Size(47, 19);
            chkOffsetsShowAsHex.TabIndex = 2;
            chkOffsetsShowAsHex.Text = "Hex";
            chkOffsetsShowAsHex.UseVisualStyleBackColor = true;
            chkOffsetsShowAsHex.CheckedChanged += chkOffsetsAsHex_CheckedChanged;
            // 
            // numOffsetZ
            // 
            numOffsetZ.Hexadecimal = true;
            numOffsetZ.Location = new Point(56, 75);
            numOffsetZ.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numOffsetZ.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numOffsetZ.Name = "numOffsetZ";
            numOffsetZ.Size = new Size(122, 23);
            numOffsetZ.TabIndex = 1;
            numOffsetZ.ValueChanged += numOffsetZ_ValueChanged;
            // 
            // lblOffsetZ
            // 
            lblOffsetZ.AutoSize = true;
            lblOffsetZ.BackColor = Color.Transparent;
            lblOffsetZ.Location = new Point(6, 77);
            lblOffsetZ.Name = "lblOffsetZ";
            lblOffsetZ.Size = new Size(49, 15);
            lblOffsetZ.TabIndex = 0;
            lblOffsetZ.Text = "Offset Z";
            // 
            // numOffsetY
            // 
            numOffsetY.Hexadecimal = true;
            numOffsetY.Location = new Point(56, 46);
            numOffsetY.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numOffsetY.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numOffsetY.Name = "numOffsetY";
            numOffsetY.Size = new Size(122, 23);
            numOffsetY.TabIndex = 1;
            numOffsetY.ValueChanged += numOffsetY_ValueChanged;
            // 
            // lblOffsetY
            // 
            lblOffsetY.AutoSize = true;
            lblOffsetY.BackColor = Color.Transparent;
            lblOffsetY.Location = new Point(6, 48);
            lblOffsetY.Name = "lblOffsetY";
            lblOffsetY.Size = new Size(49, 15);
            lblOffsetY.TabIndex = 0;
            lblOffsetY.Text = "Offset Y";
            // 
            // numOffsetX
            // 
            numOffsetX.Hexadecimal = true;
            numOffsetX.Location = new Point(56, 17);
            numOffsetX.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numOffsetX.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numOffsetX.Name = "numOffsetX";
            numOffsetX.Size = new Size(122, 23);
            numOffsetX.TabIndex = 1;
            numOffsetX.ValueChanged += numOffsetX_ValueChanged;
            // 
            // lblOffsetX
            // 
            lblOffsetX.AutoSize = true;
            lblOffsetX.BackColor = Color.Transparent;
            lblOffsetX.Location = new Point(6, 19);
            lblOffsetX.Name = "lblOffsetX";
            lblOffsetX.Size = new Size(49, 15);
            lblOffsetX.TabIndex = 0;
            lblOffsetX.Text = "Offset X";
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
            tbpPolygons.BackColor = Color.FromArgb(31, 31, 32);
            tbpPolygons.Controls.Add(label3);
            tbpPolygons.Controls.Add(label2);
            tbpPolygons.Controls.Add(btnConvert);
            tbpPolygons.Controls.Add(dgvStructs);
            tbpPolygons.Controls.Add(dgvPolygons);
            tbpPolygons.Location = new Point(4, 32);
            tbpPolygons.Name = "tbpPolygons";
            tbpPolygons.Size = new Size(1032, 764);
            tbpPolygons.TabIndex = 1;
            tbpPolygons.Text = "Polygons";
            tbpPolygons.Enter += tbpPolygons_Enter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 387);
            label3.Name = "label3";
            label3.Size = new Size(184, 15);
            label3.TabIndex = 2;
            label3.Text = "Transformed Triangles (read-only)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 3);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 2;
            label2.Text = "Model Structs";
            // 
            // btnConvert
            // 
            btnConvert.BorderColour = Color.Empty;
            btnConvert.CustomColour = false;
            btnConvert.FlatBottom = false;
            btnConvert.FlatTop = false;
            btnConvert.Location = new Point(664, 3);
            btnConvert.Name = "btnConvert";
            btnConvert.Padding = new Padding(5);
            btnConvert.Size = new Size(122, 23);
            btnConvert.TabIndex = 1;
            btnConvert.Text = "Convert";
            btnConvert.Click += btnConvert_Click;
            // 
            // dgvStructs
            // 
            dgvStructs.AllowUserToAddRows = false;
            dgvStructs.AllowUserToResizeColumns = false;
            dgvStructs.AllowUserToResizeRows = false;
            dgvStructs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStructs.ColumnHeadersHeight = 24;
            dgvStructs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStructs.Location = new Point(3, 21);
            dgvStructs.Name = "dgvStructs";
            dgvStructs.RowHeadersWidth = 24;
            dgvStructs.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvStructs.Size = new Size(642, 347);
            dgvStructs.TabIndex = 0;
            dgvStructs.CellParsing += dgv_CellParsing;
            dgvStructs.CellValueChanged += dgvStructs_CellValueChanged;
            // 
            // dgvPolygons
            // 
            dgvPolygons.AllowUserToAddRows = false;
            dgvPolygons.AllowUserToResizeColumns = false;
            dgvPolygons.AllowUserToResizeRows = false;
            dgvPolygons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPolygons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPolygons.Location = new Point(3, 405);
            dgvPolygons.Name = "dgvPolygons";
            dgvPolygons.RowHeadersWidth = 24;
            dgvPolygons.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPolygons.Size = new Size(642, 347);
            dgvPolygons.TabIndex = 0;
            dgvPolygons.CellParsing += dgv_CellParsing;
            dgvPolygons.CellValueChanged += dgvPolygons_CellValueChanged;
            dgvPolygons.KeyDown += dgvPolygons_KeyDown;
            // 
            // tbpColors
            // 
            tbpColors.BackColor = Color.FromArgb(31, 31, 32);
            tbpColors.Controls.Add(lblColorIndex);
            tbpColors.Controls.Add(fraGlobalControl);
            tbpColors.Controls.Add(pnSliders);
            tbpColors.Controls.Add(lstColor);
            tbpColors.Location = new Point(4, 32);
            tbpColors.Name = "tbpColors";
            tbpColors.Size = new Size(1032, 764);
            tbpColors.TabIndex = 1;
            tbpColors.Text = "Colors";
            tbpColors.Enter += tbpColors_Enter;
            tbpColors.Leave += tbpColors_Leave;
            // 
            // lblColorIndex
            // 
            lblColorIndex.AutoSize = true;
            lblColorIndex.BackColor = Color.Transparent;
            lblColorIndex.Location = new Point(304, 375);
            lblColorIndex.Name = "lblColorIndex";
            lblColorIndex.Size = new Size(47, 15);
            lblColorIndex.TabIndex = 8;
            lblColorIndex.Text = "Index: -";
            // 
            // fraGlobalControl
            // 
            fraGlobalControl.BackColor = Color.Transparent;
            fraGlobalControl.Controls.Add(cmdCancel);
            fraGlobalControl.Controls.Add(cmdApply);
            fraGlobalControl.Controls.Add(pnGlobalControl);
            fraGlobalControl.Controls.Add(tglGlobalControl);
            fraGlobalControl.Location = new Point(304, 218);
            fraGlobalControl.Name = "fraGlobalControl";
            fraGlobalControl.Size = new Size(293, 154);
            fraGlobalControl.TabIndex = 7;
            fraGlobalControl.TabStop = false;
            fraGlobalControl.Text = "Global Controller";
            // 
            // cmdCancel
            // 
            cmdCancel.BorderColour = Color.Empty;
            cmdCancel.CustomColour = false;
            cmdCancel.Enabled = false;
            cmdCancel.FlatBottom = false;
            cmdCancel.FlatTop = false;
            cmdCancel.Location = new Point(153, 21);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Padding = new Padding(5);
            cmdCancel.Size = new Size(75, 23);
            cmdCancel.TabIndex = 5;
            cmdCancel.Text = "Cancel";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // cmdApply
            // 
            cmdApply.BorderColour = Color.Empty;
            cmdApply.CustomColour = false;
            cmdApply.Enabled = false;
            cmdApply.FlatBottom = false;
            cmdApply.FlatTop = false;
            cmdApply.Location = new Point(73, 21);
            cmdApply.Name = "cmdApply";
            cmdApply.Padding = new Padding(5);
            cmdApply.Size = new Size(75, 23);
            cmdApply.TabIndex = 5;
            cmdApply.Text = "Apply";
            cmdApply.Click += cmdApply_Click;
            // 
            // pnGlobalControl
            // 
            pnGlobalControl.Controls.Add(colorEditorGlobal);
            pnGlobalControl.Enabled = false;
            pnGlobalControl.Location = new Point(0, 50);
            pnGlobalControl.Name = "pnGlobalControl";
            pnGlobalControl.Size = new Size(293, 100);
            pnGlobalControl.TabIndex = 4;
            // 
            // colorEditorGlobal
            // 
            colorEditorGlobal.AutoSize = true;
            colorEditorGlobal.Color = Color.FromArgb(0, 0, 0);
            colorEditorGlobal.Location = new Point(4, 3);
            colorEditorGlobal.Margin = new Padding(4, 3, 4, 3);
            colorEditorGlobal.Name = "colorEditorGlobal";
            colorEditorGlobal.Padding = new Padding(9);
            colorEditorGlobal.ShowAlphaChannel = false;
            colorEditorGlobal.ShowColorSpaceLabels = false;
            colorEditorGlobal.ShowHex = false;
            colorEditorGlobal.ShowRgb = false;
            colorEditorGlobal.Size = new Size(284, 96);
            colorEditorGlobal.TabIndex = 0;
            colorEditorGlobal.ColorChanged += colorEditorGlobal_ColorChanged;
            // 
            // tglGlobalControl
            // 
            tglGlobalControl.BackColor = Color.Transparent;
            tglGlobalControl.BackgroundColor = Color.Empty;
            tglGlobalControl.BorderColor = Color.FromArgb(155, 155, 155);
            tglGlobalControl.CheckColor = Color.FromArgb(65, 177, 225);
            tglGlobalControl.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            tglGlobalControl.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            tglGlobalControl.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            tglGlobalControl.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            tglGlobalControl.IsDerivedStyle = true;
            tglGlobalControl.Location = new Point(9, 22);
            tglGlobalControl.Name = "tglGlobalControl";
            tglGlobalControl.Size = new Size(58, 22);
            tglGlobalControl.Style = MetroSet_UI.Enums.Style.Dark;
            tglGlobalControl.StyleManager = null;
            tglGlobalControl.Switched = false;
            tglGlobalControl.SymbolColor = Color.FromArgb(92, 92, 92);
            tglGlobalControl.TabIndex = 1;
            tglGlobalControl.Text = "metroSetSwitch1";
            tglGlobalControl.ThemeAuthor = "Narwin";
            tglGlobalControl.ThemeName = "MetroDark";
            tglGlobalControl.UnCheckColor = Color.FromArgb(155, 155, 155);
            tglGlobalControl.SwitchedChanged += tglGlobalControl_SwitchedChanged;
            // 
            // pnSliders
            // 
            pnSliders.Controls.Add(fraColorSlider);
            pnSliders.Controls.Add(colorWheel);
            pnSliders.Enabled = false;
            pnSliders.Location = new Point(301, 3);
            pnSliders.Name = "pnSliders";
            pnSliders.Size = new Size(486, 209);
            pnSliders.TabIndex = 3;
            // 
            // fraColorSlider
            // 
            fraColorSlider.Controls.Add(colorEditor);
            fraColorSlider.Location = new Point(3, 3);
            fraColorSlider.Name = "fraColorSlider";
            fraColorSlider.Size = new Size(293, 203);
            fraColorSlider.TabIndex = 3;
            fraColorSlider.TabStop = false;
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Location = new Point(6, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowColorSpaceLabels = false;
            colorEditor.Size = new Size(284, 197);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // colorWheel
            // 
            colorWheel.Alpha = 1D;
            colorWheel.Color = Color.FromArgb(255, 255, 255);
            colorWheel.Enabled = false;
            colorWheel.Location = new Point(302, 5);
            colorWheel.Name = "colorWheel";
            colorWheel.Size = new Size(178, 200);
            colorWheel.TabIndex = 0;
            colorWheel.Visible = false;
            colorWheel.ColorChanged += colorWheel_ColorChanged;
            // 
            // lstColor
            // 
            lstColor.BackColor = Color.FromArgb(26, 26, 28);
            lstColor.BorderStyle = BorderStyle.FixedSingle;
            lstColor.ForeColor = Color.FromArgb(213, 213, 213);
            lstColor.Location = new Point(3, 3);
            lstColor.Name = "lstColor";
            lstColor.OwnerDraw = true;
            lstColor.Size = new Size(292, 573);
            lstColor.TabIndex = 0;
            lstColor.UseCompatibleStateImageBehavior = false;
            lstColor.DrawItem += lstColor_DrawItem;
            lstColor.SelectedIndexChanged += lstColor_SelectedIndexChanged;
            lstColor.MouseDown += lstColor_MouseDown;
            lstColor.MouseUp += lstColor_MouseUp;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Controls.Add(pnPicture);
            tbpTextures.Controls.Add(panel2);
            tbpTextures.Location = new Point(4, 32);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(1032, 764);
            tbpTextures.TabIndex = 2;
            tbpTextures.Text = "Textures";
            tbpTextures.Enter += tbpTextures_Enter;
            // 
            // pnPicture
            // 
            pnPicture.Controls.Add(pictureBox1);
            pnPicture.Dock = DockStyle.Top;
            pnPicture.Location = new Point(0, 378);
            pnPicture.Name = "pnPicture";
            pnPicture.Size = new Size(1032, 152);
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
            panel2.Size = new Size(1032, 378);
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
            pnTextureControls.Location = new Point(671, 0);
            pnTextureControls.Name = "pnTextureControls";
            pnTextureControls.Size = new Size(214, 372);
            pnTextureControls.TabIndex = 13;
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
            chkMaxValueFlag.Click += chkMaxValueFlag_Click;
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
            tglSimpleMode.SwitchedChanged += tglSimpleMode_SwitchedChanged;
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
            cmdReplaceTexture.Click += cmdReplaceTexture_Click;
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
            chkReplaceCLUT.CheckedChanged += chkReplaceCLUT_CheckedChanged;
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
            chkBGRA.CheckedChanged += chkBGRA_CheckedChanged;
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
            cmdLoadTexture.Click += cmdLoadTexture_Click;
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
            numReplaceTo.Click += numReplaceTo_Click;
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
            cmdReplace.Click += cmdReplace_Click;
            // 
            // fraTPage
            // 
            fraTPage.Controls.Add(rbtReloadTPage);
            fraTPage.Controls.Add(dpdTPage);
            fraTPage.Controls.Add(cmdRemoveTPage);
            fraTPage.Controls.Add(cmdAppendTPage);
            fraTPage.Controls.Add(lstTPages);
            fraTPage.Location = new Point(3, 3);
            fraTPage.Name = "fraTPage";
            fraTPage.Size = new Size(132, 301);
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
            rbtReloadTPage.Location = new Point(107, 278);
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
            dpdTPage.Location = new Point(6, 208);
            dpdTPage.MaxLength = 5;
            dpdTPage.Name = "dpdTPage";
            dpdTPage.Size = new Size(120, 24);
            dpdTPage.TabIndex = 2;
            dpdTPage.SelectedIndexChanged += dpdTPage_SelectedIndexChanged;
            // 
            // cmdRemoveTPage
            // 
            cmdRemoveTPage.BorderColour = Color.Empty;
            cmdRemoveTPage.CustomColour = false;
            cmdRemoveTPage.Enabled = false;
            cmdRemoveTPage.FlatBottom = false;
            cmdRemoveTPage.FlatTop = false;
            cmdRemoveTPage.Location = new Point(28, 266);
            cmdRemoveTPage.Name = "cmdRemoveTPage";
            cmdRemoveTPage.Padding = new Padding(5);
            cmdRemoveTPage.Size = new Size(75, 23);
            cmdRemoveTPage.TabIndex = 1;
            cmdRemoveTPage.Text = "Remove";
            cmdRemoveTPage.Click += cmdRemoveTPage_Click;
            // 
            // cmdAppendTPage
            // 
            cmdAppendTPage.BorderColour = Color.Empty;
            cmdAppendTPage.CustomColour = false;
            cmdAppendTPage.Enabled = false;
            cmdAppendTPage.FlatBottom = false;
            cmdAppendTPage.FlatTop = false;
            cmdAppendTPage.Location = new Point(28, 237);
            cmdAppendTPage.Name = "cmdAppendTPage";
            cmdAppendTPage.Padding = new Padding(5);
            cmdAppendTPage.Size = new Size(75, 23);
            cmdAppendTPage.TabIndex = 1;
            cmdAppendTPage.Text = "Append";
            cmdAppendTPage.Click += cmdAppendTPage_Click;
            // 
            // lstTPages
            // 
            lstTPages.BorderStyle = BorderStyle.FixedSingle;
            lstTPages.FullRowSelect = true;
            lstTPages.Location = new Point(6, 22);
            lstTPages.Name = "lstTPages";
            lstTPages.Size = new Size(120, 180);
            lstTPages.TabIndex = 0;
            lstTPages.UseCompatibleStateImageBehavior = false;
            lstTPages.View = View.Details;
            lstTPages.ColumnWidthChanging += lstPages_ColumnWidthChangingHandler;
            lstTPages.SelectedIndexChanged += lstTPages_SelectedIndexChanged;
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
            dgvTextures.Size = new Size(524, 372);
            dgvTextures.TabIndex = 0;
            dgvTextures.CellEndEdit += dgvTextures_CellEndEdit;
            dgvTextures.CellParsing += dgv_CellParsing;
            dgvTextures.CellValidating += dgvTextures_CellValidating;
            dgvTextures.CellValueChanged += dgvTextures_CellValueChanged;
            dgvTextures.EditingControlShowing += dgvTextures_EditingControlShowing;
            dgvTextures.SelectionChanged += dgvTextures_SelectionChanged;
            // 
            // tbpExtendedTextures
            // 
            tbpExtendedTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpExtendedTextures.Controls.Add(panel3);
            tbpExtendedTextures.Location = new Point(4, 32);
            tbpExtendedTextures.Name = "tbpExtendedTextures";
            tbpExtendedTextures.Size = new Size(1032, 764);
            tbpExtendedTextures.TabIndex = 3;
            tbpExtendedTextures.Text = "Extended Textures";
            tbpExtendedTextures.Enter += tbpExtendedTextures_Enter;
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.Controls.Add(dgvExtendedTextures);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1032, 397);
            panel3.TabIndex = 1;
            // 
            // dgvExtendedTextures
            // 
            dgvExtendedTextures.AllowUserToAddRows = false;
            dgvExtendedTextures.AllowUserToResizeColumns = false;
            dgvExtendedTextures.AllowUserToResizeRows = false;
            dgvExtendedTextures.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvExtendedTextures.ColumnHeadersHeight = 24;
            dgvExtendedTextures.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvExtendedTextures.Location = new Point(3, 3);
            dgvExtendedTextures.Name = "dgvExtendedTextures";
            dgvExtendedTextures.RowHeadersWidth = 24;
            dgvExtendedTextures.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvExtendedTextures.ScrollBars = ScrollBars.Vertical;
            dgvExtendedTextures.Size = new Size(712, 391);
            dgvExtendedTextures.TabIndex = 0;
            dgvExtendedTextures.CellBeginEdit += dgvExtendedTextures_CellBeginEdit;
            dgvExtendedTextures.CellParsing += dgv_CellParsing;
            dgvExtendedTextures.CellValidating += dgvExtendedTextures_CellValidating;
            dgvExtendedTextures.CellValueChanged += dgvExtendedTextures_CellValueChanged;
            dgvExtendedTextures.EditingControlShowing += dgvExtendedTextures_EditingControlShowing;
            // 
            // tbpPositions
            // 
            tbpPositions.BackColor = Color.FromArgb(31, 31, 32);
            tbpPositions.Controls.Add(dgvPositions);
            tbpPositions.Location = new Point(4, 32);
            tbpPositions.Name = "tbpPositions";
            tbpPositions.Size = new Size(1032, 764);
            tbpPositions.TabIndex = 1;
            tbpPositions.Text = "Positions";
            tbpPositions.Enter += tbpPositions_Enter;
            // 
            // dgvPositions
            // 
            dgvPositions.AllowUserToAddRows = false;
            dgvPositions.AllowUserToResizeColumns = false;
            dgvPositions.AllowUserToResizeRows = false;
            dgvPositions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPositions.ColumnHeadersHeight = 24;
            dgvPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPositions.Location = new Point(3, 3);
            dgvPositions.Name = "dgvPositions";
            dgvPositions.RowHeadersWidth = 24;
            dgvPositions.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPositions.Size = new Size(377, 624);
            dgvPositions.TabIndex = 0;
            dgvPositions.CellBeginEdit += dgvPositions_CellBeginEdit;
            dgvPositions.CellParsing += dgv_CellParsing;
            dgvPositions.CellValidating += dgvPositions_CellValidating;
            dgvPositions.CellValueChanged += dgvPositions_CellValueChanged;
            dgvPositions.EditingControlShowing += dgvPositions_EditingControlShowing;
            // 
            // ModelBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabModel);
            Name = "ModelBox";
            Size = new Size(1040, 800);
            tabModel.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            tbpGeneral.PerformLayout();
            panel1.ResumeLayout(false);
            fraScales.ResumeLayout(false);
            fraScales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleX).EndInit();
            fraOffsets.ResumeLayout(false);
            fraOffsets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numOffsetZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOffsetY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numOffsetX).EndInit();
            tbpPolygons.ResumeLayout(false);
            tbpPolygons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStructs).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPolygons).EndInit();
            tbpColors.ResumeLayout(false);
            tbpColors.PerformLayout();
            fraGlobalControl.ResumeLayout(false);
            pnGlobalControl.ResumeLayout(false);
            pnGlobalControl.PerformLayout();
            pnSliders.ResumeLayout(false);
            fraColorSlider.ResumeLayout(false);
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
            tbpExtendedTextures.ResumeLayout(false);
            tbpExtendedTextures.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExtendedTextures).EndInit();
            tbpPositions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPositions).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetTabControl tabModel;
        private TabPage tbpGeneral;
        private TabPage tbpPolygons;
        private TabPage tbpPositions;
        private TabPage tbpColors;
        private TabPage tbpTextures;
        private TabPage tbpExtendedTextures;
        private AltUI.Controls.DarkGroupBox fraTPage;
        private DoubleBufferedListView lstTPages;
        private DoubleBufferedListView lstColor;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel;
        private MetroSetSwitch tglGlobalControl;
        private Panel pnSliders;
        private Panel pnGlobalControl;
        private AltUI.Controls.DarkButton cmdApply;
        private AltUI.Controls.DarkGroupBox fraGlobalControl;
        private AltUI.Controls.DarkButton cmdCancel;
        private DataGridView dgvTextures;
        private PictureBox pictureBox1;
        private MetroSetSwitch tglSimpleMode;
        private AltUI.Controls.DarkButton cmdReplace;
        private AltUI.Controls.DarkGroupBox fraReplace;
        private AltUI.Controls.DarkNumericUpDown numReplace;
        private AltUI.Controls.DarkGroupBox fraSwitches;
        private Label label1;
        private AltUI.Controls.DarkNumericUpDown numReplaceTo;
        private AltUI.Controls.DarkNumericUpDown numRowIndex;
        private CheckBox chkBGRA;
        private AltUI.Controls.DarkGroupBox fraReplaceTexture;
        private AltUI.Controls.DarkButton cmdReplaceTexture;
        private AltUI.Controls.DarkButton cmdRemoveTPage;
        private AltUI.Controls.DarkButton cmdAppendTPage;
        private DarkComboBox dpdTPage;
        private CheckBox chkReplaceCLUT;
        private Label lblModelInfo;
        private MetroSetRadioButton rbtReloadTPage;
        private DarkGroupBox fraColorSlider;
        private DarkGroupBox fraScales;
        private DarkNumericUpDown numScaleZ;
        private Label lblScaleZ;
        private DarkNumericUpDown numScaleY;
        private Label lblScaleY;
        private DarkNumericUpDown numScaleX;
        private Label lblScaleX;
        private CheckBox chkScalesShowAsHex;
        private DarkGroupBox fraOffsets;
        private CheckBox chkOffsetsShowAsHex;
        private DarkNumericUpDown numOffsetZ;
        private Label lblOffsetZ;
        private DarkNumericUpDown numOffsetY;
        private Label lblOffsetY;
        private DarkNumericUpDown numOffsetX;
        private Label lblOffsetX;
        private Panel panel1;
        private DarkButton cmdLoadTexture;
        private Cyotek.Windows.Forms.ColorEditor colorEditorGlobal;
        private Panel pnPicture;
        private MetroSetTrackBar trkPictureSize;
        private Panel panel2;
        private Panel pnTextureControls;
        private CheckBox chkMaxValueFlag;
        private DataGridView dgvExtendedTextures;
        private Panel panel3;
        private DataGridView dgvPolygons;
        private DataGridView dgvStructs;
        private DarkButton btnConvert;
        private Label label2;
        private Label label3;
        private DataGridView dgvPositions;
        private Label lblColorIndex;
    }
}
