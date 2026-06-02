using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
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
            lblStruct = new Label();
            label3 = new Label();
            label2 = new Label();
            dgvStructs = new DataGridView();
            dgvPolygons = new DataGridView();
            tbpVertices = new TabPage();
            lblVertices = new DarkLabel();
            fraTempVertices = new DarkGroupBox();
            panel4 = new Panel();
            cmdClearTempVerts = new DarkButton();
            cmdRemoveTempVerts = new DarkButton();
            picTempVertsHint = new PictureBox();
            dgvTempVertices = new DataGridView();
            fraNearbyVertices = new DarkGroupBox();
            dgvNearbyVertices = new DataGridView();
            fraVertices = new DarkGroupBox();
            lblVertColor = new Label();
            lblVertFX = new Label();
            lblVertZ = new Label();
            lblVertY = new Label();
            lblVertX = new Label();
            lblVertexIndex = new Label();
            chkEditTempVertices = new CheckBox();
            chkEditNearbyVertices = new CheckBox();
            chkTempAddCoVerts = new CheckBox();
            numVertexIndex = new DarkNumericUpDown();
            inpVertexX = new DarkNumericUpDown();
            inpVertexY = new DarkNumericUpDown();
            inpVertexZ = new DarkNumericUpDown();
            inpVertexFX = new DarkNumericUpDown();
            inpVertexColor = new DarkNumericUpDown();
            tbpColors = new TabPage();
            lblColorIndex = new Label();
            fraGlobalControl = new DarkGroupBox();
            cmdCancel = new DarkButton();
            cmdApply = new DarkButton();
            pnGlobalControl = new Panel();
            pictureBox2 = new PictureBox();
            cmdClearSelection = new DarkButton();
            numLowestBrightness = new DarkNumericUpDown();
            colorEditorGlobal = new Cyotek.Windows.Forms.ColorEditor();
            chkLowestBrightness = new CheckBox();
            tglGlobalControl = new MetroSetSwitch();
            pnSliders = new Panel();
            fraColorSlider = new DarkGroupBox();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            dgvColor = new DataGridView();
            tbpTextures = new TabPage();
            pnTextureSelect = new Panel();
            lbTextureInfos = new DarkLabel();
            fraTextureGuides = new DarkGroupBox();
            groupBox6 = new DarkGroupBox();
            label14 = new Label();
            label13 = new Label();
            C2numY = new DarkNumericUpDown();
            C2numX = new DarkNumericUpDown();
            label7 = new Label();
            label8 = new Label();
            C2numY2 = new DarkNumericUpDown();
            C2numX2 = new DarkNumericUpDown();
            darkGroupBox1 = new DarkGroupBox();
            numSelectionSize = new DarkNumericUpDown();
            groupBox7 = new DarkGroupBox();
            C2numH = new DarkNumericUpDown();
            label9 = new Label();
            C2numW = new DarkNumericUpDown();
            label10 = new Label();
            chkEnableGuides = new CheckBox();
            pnPicture = new Panel();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            pnTextureControls = new Panel();
            cmdMoveTexture = new DarkButton();
            chkRegionEndFlag = new CheckBox();
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
            lblEIDError = new Label();
            rbtReloadTPage = new MetroSetRadioButton();
            dpdTPages = new DarkComboBox();
            cmdRemoveTPage = new DarkButton();
            cmdAppendTPage = new DarkButton();
            lstTPages = new DoubleBufferedListView();
            trkPictureSize = new MetroSetTrackBar();
            dgvTextures = new DataGridView();
            tbpExtendedTextures = new TabPage();
            fraExTexControls = new DarkGroupBox();
            lbExTextureInfos = new DarkLabel();
            cmdAppendExTex = new DarkButton();
            cmdRemoveExTex = new DarkButton();
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
            tbpVertices.SuspendLayout();
            fraTempVertices.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTempVertsHint).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTempVertices).BeginInit();
            fraNearbyVertices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNearbyVertices).BeginInit();
            fraVertices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numVertexIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexFX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexColor).BeginInit();
            tbpColors.SuspendLayout();
            fraGlobalControl.SuspendLayout();
            pnGlobalControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLowestBrightness).BeginInit();
            pnSliders.SuspendLayout();
            fraColorSlider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvColor).BeginInit();
            tbpTextures.SuspendLayout();
            pnTextureSelect.SuspendLayout();
            fraTextureGuides.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numX2).BeginInit();
            darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSelectionSize).BeginInit();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numW).BeginInit();
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
            fraExTexControls.SuspendLayout();
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
            tabModel.Controls.Add(tbpVertices);
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
            tabModel.Size = new Size(1040, 1040);
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
            tbpGeneral.Size = new Size(1032, 1004);
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
            tbpPolygons.AutoScroll = true;
            tbpPolygons.BackColor = Color.FromArgb(31, 31, 32);
            tbpPolygons.Controls.Add(lblStruct);
            tbpPolygons.Controls.Add(label3);
            tbpPolygons.Controls.Add(label2);
            tbpPolygons.Controls.Add(dgvStructs);
            tbpPolygons.Controls.Add(dgvPolygons);
            tbpPolygons.Location = new Point(4, 32);
            tbpPolygons.Name = "tbpPolygons";
            tbpPolygons.Size = new Size(1032, 1004);
            tbpPolygons.TabIndex = 1;
            tbpPolygons.Text = "Polygons";
            tbpPolygons.Enter += tbpPolygons_Enter;
            // 
            // lblStruct
            // 
            lblStruct.AutoSize = true;
            lblStruct.BackColor = Color.Transparent;
            lblStruct.Location = new Point(89, 3);
            lblStruct.Name = "lblStruct";
            lblStruct.Size = new Size(46, 15);
            lblStruct.TabIndex = 3;
            lblStruct.Text = "{Struct}";
            lblStruct.Visible = false;
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
            dgvStructs.ShowCellToolTips = false;
            dgvStructs.Size = new Size(642, 347);
            dgvStructs.TabIndex = 0;
            dgvStructs.CellBeginEdit += dgvStructs_CellBeginEdit;
            dgvStructs.CellFormatting += dgvStructs_CellFormatting;
            dgvStructs.CellParsing += dgv_CellParsing;
            dgvStructs.CellValidating += dgvStructs_CellValidating;
            dgvStructs.CellValueChanged += dgvStructs_CellValueChanged;
            dgvStructs.SelectionChanged += dgvStructs_SelectionChanged;
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
            dgvPolygons.ShowCellToolTips = false;
            dgvPolygons.Size = new Size(642, 347);
            dgvPolygons.TabIndex = 0;
            dgvPolygons.CellParsing += dgv_CellParsing;
            dgvPolygons.CellValidating += dgvPolygons_CellValidating;
            dgvPolygons.CellValueChanged += dgvPolygons_CellValueChanged;
            dgvPolygons.KeyDown += dgvPolygons_KeyDown;
            // 
            // tbpVertices
            // 
            tbpVertices.AutoScroll = true;
            tbpVertices.BackColor = Color.FromArgb(31, 31, 32);
            tbpVertices.Controls.Add(lblVertices);
            tbpVertices.Controls.Add(fraTempVertices);
            tbpVertices.Controls.Add(fraNearbyVertices);
            tbpVertices.Controls.Add(fraVertices);
            tbpVertices.Location = new Point(4, 32);
            tbpVertices.Name = "tbpVertices";
            tbpVertices.Size = new Size(1032, 1004);
            tbpVertices.TabIndex = 1;
            tbpVertices.Text = "Vertices";
            tbpVertices.Enter += tbpVertices_Enter;
            // 
            // lblVertices
            // 
            lblVertices.AutoSize = true;
            lblVertices.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVertices.Location = new Point(15, 12);
            lblVertices.Name = "lblVertices";
            lblVertices.Size = new Size(61, 17);
            lblVertices.TabIndex = 14;
            lblVertices.Text = "{Vertices}";
            // 
            // fraTempVertices
            // 
            fraTempVertices.AutoSize = true;
            fraTempVertices.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTempVertices.BackColor = Color.Transparent;
            fraTempVertices.Controls.Add(panel4);
            fraTempVertices.Controls.Add(picTempVertsHint);
            fraTempVertices.Controls.Add(dgvTempVertices);
            fraTempVertices.Location = new Point(220, 10);
            fraTempVertices.Name = "fraTempVertices";
            fraTempVertices.Size = new Size(312, 591);
            fraTempVertices.TabIndex = 13;
            fraTempVertices.TabStop = false;
            fraTempVertices.Text = "Multiselected vertices (0)";
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            panel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel4.Controls.Add(cmdClearTempVerts);
            panel4.Controls.Add(cmdRemoveTempVerts);
            panel4.Location = new Point(6, 500);
            panel4.Name = "panel4";
            panel4.Size = new Size(81, 69);
            panel4.TabIndex = 16;
            // 
            // cmdClearTempVerts
            // 
            cmdClearTempVerts.BorderColour = Color.Empty;
            cmdClearTempVerts.CustomColour = false;
            cmdClearTempVerts.Enabled = false;
            cmdClearTempVerts.FlatBottom = false;
            cmdClearTempVerts.FlatTop = false;
            cmdClearTempVerts.Location = new Point(3, 40);
            cmdClearTempVerts.Name = "cmdClearTempVerts";
            cmdClearTempVerts.Padding = new Padding(5);
            cmdClearTempVerts.Size = new Size(75, 26);
            cmdClearTempVerts.TabIndex = 15;
            cmdClearTempVerts.Text = "Clear";
            cmdClearTempVerts.Click += cmdClearTempVerts_Click;
            // 
            // cmdRemoveTempVerts
            // 
            cmdRemoveTempVerts.BorderColour = Color.Empty;
            cmdRemoveTempVerts.CustomColour = false;
            cmdRemoveTempVerts.Enabled = false;
            cmdRemoveTempVerts.FlatBottom = false;
            cmdRemoveTempVerts.FlatTop = false;
            cmdRemoveTempVerts.Location = new Point(3, 8);
            cmdRemoveTempVerts.Name = "cmdRemoveTempVerts";
            cmdRemoveTempVerts.Padding = new Padding(5);
            cmdRemoveTempVerts.Size = new Size(75, 26);
            cmdRemoveTempVerts.TabIndex = 15;
            cmdRemoveTempVerts.Text = "Remove";
            cmdRemoveTempVerts.Click += cmdRemoveTempVerts_Click;
            // 
            // picTempVertsHint
            // 
            picTempVertsHint.Location = new Point(180, 2);
            picTempVertsHint.Name = "picTempVertsHint";
            picTempVertsHint.Size = new Size(16, 16);
            picTempVertsHint.TabIndex = 15;
            picTempVertsHint.TabStop = false;
            // 
            // dgvTempVertices
            // 
            dgvTempVertices.AllowUserToAddRows = false;
            dgvTempVertices.AllowUserToResizeColumns = false;
            dgvTempVertices.AllowUserToResizeRows = false;
            dgvTempVertices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTempVertices.ColumnHeadersHeight = 24;
            dgvTempVertices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTempVertices.Location = new Point(6, 22);
            dgvTempVertices.MultiSelect = false;
            dgvTempVertices.Name = "dgvTempVertices";
            dgvTempVertices.ReadOnly = true;
            dgvTempVertices.RowHeadersWidth = 24;
            dgvTempVertices.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTempVertices.ScrollBars = ScrollBars.Vertical;
            dgvTempVertices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTempVertices.ShowCellToolTips = false;
            dgvTempVertices.Size = new Size(300, 470);
            dgvTempVertices.TabIndex = 12;
            dgvTempVertices.CellBeginEdit += dgvVertices_CellBeginEdit;
            dgvTempVertices.SelectionChanged += dgvTempVertices_SelectionChanged;
            // 
            // fraNearbyVertices
            // 
            fraNearbyVertices.AutoSize = true;
            fraNearbyVertices.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraNearbyVertices.BackColor = Color.Transparent;
            fraNearbyVertices.Controls.Add(dgvNearbyVertices);
            fraNearbyVertices.Location = new Point(10, 340);
            fraNearbyVertices.Name = "fraNearbyVertices";
            fraNearbyVertices.Size = new Size(192, 274);
            fraNearbyVertices.TabIndex = 13;
            fraNearbyVertices.TabStop = false;
            fraNearbyVertices.Text = "Co-Located Vertices";
            // 
            // dgvNearbyVertices
            // 
            dgvNearbyVertices.AllowUserToAddRows = false;
            dgvNearbyVertices.AllowUserToResizeColumns = false;
            dgvNearbyVertices.AllowUserToResizeRows = false;
            dgvNearbyVertices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvNearbyVertices.ColumnHeadersHeight = 24;
            dgvNearbyVertices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvNearbyVertices.Location = new Point(6, 22);
            dgvNearbyVertices.MultiSelect = false;
            dgvNearbyVertices.Name = "dgvNearbyVertices";
            dgvNearbyVertices.ReadOnly = true;
            dgvNearbyVertices.RowHeadersWidth = 24;
            dgvNearbyVertices.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvNearbyVertices.ScrollBars = ScrollBars.Vertical;
            dgvNearbyVertices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNearbyVertices.ShowCellToolTips = false;
            dgvNearbyVertices.Size = new Size(180, 230);
            dgvNearbyVertices.TabIndex = 12;
            dgvNearbyVertices.CellBeginEdit += dgvVertices_CellBeginEdit;
            dgvNearbyVertices.SelectionChanged += dgvNearbyVertices_SelectionChanged;
            // 
            // fraVertices
            // 
            fraVertices.AutoSize = true;
            fraVertices.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraVertices.BackColor = Color.Transparent;
            fraVertices.Controls.Add(lblVertColor);
            fraVertices.Controls.Add(lblVertFX);
            fraVertices.Controls.Add(lblVertZ);
            fraVertices.Controls.Add(lblVertY);
            fraVertices.Controls.Add(lblVertX);
            fraVertices.Controls.Add(lblVertexIndex);
            fraVertices.Controls.Add(chkEditTempVertices);
            fraVertices.Controls.Add(chkEditNearbyVertices);
            fraVertices.Controls.Add(chkTempAddCoVerts);
            fraVertices.Controls.Add(numVertexIndex);
            fraVertices.Controls.Add(inpVertexX);
            fraVertices.Controls.Add(inpVertexY);
            fraVertices.Controls.Add(inpVertexZ);
            fraVertices.Controls.Add(inpVertexFX);
            fraVertices.Controls.Add(inpVertexColor);
            fraVertices.Location = new Point(10, 18);
            fraVertices.Margin = new Padding(4, 3, 4, 0);
            fraVertices.Name = "fraVertices";
            fraVertices.Padding = new Padding(4, 3, 4, 0);
            fraVertices.Size = new Size(178, 318);
            fraVertices.TabIndex = 9;
            fraVertices.TabStop = false;
            // 
            // lblVertColor
            // 
            lblVertColor.AutoSize = true;
            lblVertColor.Location = new Point(10, 182);
            lblVertColor.Name = "lblVertColor";
            lblVertColor.Size = new Size(50, 15);
            lblVertColor.TabIndex = 14;
            lblVertColor.Text = "Color ID";
            // 
            // lblVertFX
            // 
            lblVertFX.AutoSize = true;
            lblVertFX.Location = new Point(10, 152);
            lblVertFX.Name = "lblVertFX";
            lblVertFX.Size = new Size(20, 15);
            lblVertFX.TabIndex = 14;
            lblVertFX.Text = "FX";
            // 
            // lblVertZ
            // 
            lblVertZ.AutoSize = true;
            lblVertZ.Location = new Point(10, 122);
            lblVertZ.Name = "lblVertZ";
            lblVertZ.Size = new Size(14, 15);
            lblVertZ.TabIndex = 14;
            lblVertZ.Text = "Z";
            // 
            // lblVertY
            // 
            lblVertY.AutoSize = true;
            lblVertY.Location = new Point(10, 92);
            lblVertY.Name = "lblVertY";
            lblVertY.Size = new Size(14, 15);
            lblVertY.TabIndex = 14;
            lblVertY.Text = "Y";
            // 
            // lblVertX
            // 
            lblVertX.AutoSize = true;
            lblVertX.Location = new Point(10, 62);
            lblVertX.Name = "lblVertX";
            lblVertX.Size = new Size(14, 15);
            lblVertX.TabIndex = 14;
            lblVertX.Text = "X";
            // 
            // lblVertexIndex
            // 
            lblVertexIndex.AutoSize = true;
            lblVertexIndex.Location = new Point(10, 22);
            lblVertexIndex.Name = "lblVertexIndex";
            lblVertexIndex.Size = new Size(36, 15);
            lblVertexIndex.TabIndex = 14;
            lblVertexIndex.Text = "Index";
            // 
            // chkEditTempVertices
            // 
            chkEditTempVertices.AutoSize = true;
            chkEditTempVertices.BackColor = Color.Transparent;
            chkEditTempVertices.Checked = true;
            chkEditTempVertices.CheckState = CheckState.Checked;
            chkEditTempVertices.Location = new Point(5, 243);
            chkEditTempVertices.Name = "chkEditTempVertices";
            chkEditTempVertices.Size = new Size(160, 19);
            chkEditTempVertices.TabIndex = 14;
            chkEditTempVertices.Text = "Affect Multiselected verts";
            chkEditTempVertices.UseVisualStyleBackColor = false;
            chkEditTempVertices.CheckedChanged += chkEditTempVertices_CheckedChanged;
            // 
            // chkEditNearbyVertices
            // 
            chkEditNearbyVertices.AutoSize = true;
            chkEditNearbyVertices.BackColor = Color.Transparent;
            chkEditNearbyVertices.Location = new Point(5, 218);
            chkEditNearbyVertices.Name = "chkEditNearbyVertices";
            chkEditNearbyVertices.Size = new Size(166, 19);
            chkEditNearbyVertices.TabIndex = 14;
            chkEditNearbyVertices.Text = "Affect Co-Located Vertices";
            chkEditNearbyVertices.UseVisualStyleBackColor = false;
            chkEditNearbyVertices.CheckedChanged += chkEditNearbyVertices_CheckedChanged;
            // 
            // chkTempAddCoVerts
            // 
            chkTempAddCoVerts.AutoSize = true;
            chkTempAddCoVerts.BackColor = Color.Transparent;
            chkTempAddCoVerts.Location = new Point(5, 280);
            chkTempAddCoVerts.Name = "chkTempAddCoVerts";
            chkTempAddCoVerts.Size = new Size(158, 19);
            chkTempAddCoVerts.TabIndex = 14;
            chkTempAddCoVerts.Text = "Add Co-Located to Multi";
            chkTempAddCoVerts.UseVisualStyleBackColor = false;
            chkTempAddCoVerts.CheckedChanged += ChkTempAddCoVerts_CheckedChanged;
            // 
            // numVertexIndex
            // 
            numVertexIndex.AutoSize = true;
            numVertexIndex.Location = new Point(80, 20);
            numVertexIndex.Name = "numVertexIndex";
            numVertexIndex.Size = new Size(65, 23);
            numVertexIndex.TabIndex = 4;
            numVertexIndex.ValueChanged += numVertexIndex_ValueChanged;
            numVertexIndex.MouseWheel += numVertexIndex_MouseWheel;
            // 
            // inpVertexX
            // 
            inpVertexX.Location = new Point(80, 60);
            inpVertexX.Name = "inpVertexX";
            inpVertexX.Size = new Size(65, 23);
            inpVertexX.TabIndex = 5;
            inpVertexX.ValueChanged += Vertex_ValueChanged;
            // 
            // inpVertexY
            // 
            inpVertexY.Location = new Point(80, 90);
            inpVertexY.Name = "inpVertexY";
            inpVertexY.Size = new Size(65, 23);
            inpVertexY.TabIndex = 6;
            inpVertexY.ValueChanged += Vertex_ValueChanged;
            // 
            // inpVertexZ
            // 
            inpVertexZ.Location = new Point(80, 120);
            inpVertexZ.Name = "inpVertexZ";
            inpVertexZ.Size = new Size(65, 23);
            inpVertexZ.TabIndex = 7;
            inpVertexZ.ValueChanged += Vertex_ValueChanged;
            // 
            // inpVertexFX
            // 
            inpVertexFX.Location = new Point(80, 150);
            inpVertexFX.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            inpVertexFX.Name = "inpVertexFX";
            inpVertexFX.Size = new Size(65, 23);
            inpVertexFX.TabIndex = 8;
            inpVertexFX.ValueChanged += VertexFX_ValueChanged;
            // 
            // inpVertexColor
            // 
            inpVertexColor.Location = new Point(80, 180);
            inpVertexColor.Name = "inpVertexColor";
            inpVertexColor.Size = new Size(65, 23);
            inpVertexColor.TabIndex = 9;
            inpVertexColor.ValueChanged += VertexColor_ValueChanged;
            // 
            // tbpColors
            // 
            tbpColors.BackColor = Color.FromArgb(31, 31, 32);
            tbpColors.Controls.Add(lblColorIndex);
            tbpColors.Controls.Add(fraGlobalControl);
            tbpColors.Controls.Add(pnSliders);
            tbpColors.Controls.Add(dgvColor);
            tbpColors.Location = new Point(4, 32);
            tbpColors.Name = "tbpColors";
            tbpColors.Size = new Size(1032, 1004);
            tbpColors.TabIndex = 1;
            tbpColors.Text = "Colors";
            tbpColors.Enter += tbpColors_Enter;
            // 
            // lblColorIndex
            // 
            lblColorIndex.AutoSize = true;
            lblColorIndex.BackColor = Color.Transparent;
            lblColorIndex.Location = new Point(304, 439);
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
            fraGlobalControl.Size = new Size(293, 214);
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
            pnGlobalControl.Controls.Add(pictureBox2);
            pnGlobalControl.Controls.Add(cmdClearSelection);
            pnGlobalControl.Controls.Add(numLowestBrightness);
            pnGlobalControl.Controls.Add(colorEditorGlobal);
            pnGlobalControl.Controls.Add(chkLowestBrightness);
            pnGlobalControl.Enabled = false;
            pnGlobalControl.Location = new Point(1, 50);
            pnGlobalControl.Name = "pnGlobalControl";
            pnGlobalControl.Size = new Size(292, 160);
            pnGlobalControl.TabIndex = 4;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(111, 134);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(16, 16);
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // cmdClearSelection
            // 
            cmdClearSelection.BorderColour = Color.Empty;
            cmdClearSelection.CustomColour = false;
            cmdClearSelection.FlatBottom = false;
            cmdClearSelection.FlatTop = false;
            cmdClearSelection.Location = new Point(10, 129);
            cmdClearSelection.Name = "cmdClearSelection";
            cmdClearSelection.Padding = new Padding(5);
            cmdClearSelection.Size = new Size(95, 26);
            cmdClearSelection.TabIndex = 8;
            cmdClearSelection.Text = "Clear Selection";
            cmdClearSelection.Click += cmdClearSelection_Click;
            // 
            // numLowestBrightness
            // 
            numLowestBrightness.DecimalPlaces = 2;
            numLowestBrightness.Enabled = false;
            numLowestBrightness.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numLowestBrightness.Location = new Point(230, 102);
            numLowestBrightness.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numLowestBrightness.Name = "numLowestBrightness";
            numLowestBrightness.Size = new Size(56, 23);
            numLowestBrightness.TabIndex = 7;
            // 
            // colorEditorGlobal
            // 
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
            // chkLowestBrightness
            // 
            chkLowestBrightness.AutoSize = true;
            chkLowestBrightness.Location = new Point(10, 104);
            chkLowestBrightness.Name = "chkLowestBrightness";
            chkLowestBrightness.Size = new Size(217, 19);
            chkLowestBrightness.TabIndex = 6;
            chkLowestBrightness.Text = "Ignore colors with brightness below:";
            chkLowestBrightness.UseVisualStyleBackColor = true;
            chkLowestBrightness.CheckedChanged += chkLowestBrightness_CheckedChanged;
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
            // dgvColor
            // 
            dgvColor.AllowUserToAddRows = false;
            dgvColor.AllowUserToResizeColumns = false;
            dgvColor.AllowUserToResizeRows = false;
            dgvColor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvColor.ColumnHeadersHeight = 24;
            dgvColor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvColor.Location = new Point(3, 3);
            dgvColor.MultiSelect = false;
            dgvColor.Name = "dgvColor";
            dgvColor.ReadOnly = true;
            dgvColor.RowHeadersWidth = 24;
            dgvColor.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvColor.ShowCellToolTips = false;
            dgvColor.Size = new Size(290, 573);
            dgvColor.TabIndex = 0;
            dgvColor.CellPainting += dgvColor_CellPainting;
            dgvColor.SelectionChanged += dgvColor_SelectedIndexChanged;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Controls.Add(pnTextureSelect);
            tbpTextures.Controls.Add(pnPicture);
            tbpTextures.Controls.Add(panel2);
            tbpTextures.Location = new Point(4, 32);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(1032, 1004);
            tbpTextures.TabIndex = 2;
            tbpTextures.Text = "Textures";
            tbpTextures.Enter += tbpTextures_Enter;
            // 
            // pnTextureSelect
            // 
            pnTextureSelect.Controls.Add(lbTextureInfos);
            pnTextureSelect.Controls.Add(fraTextureGuides);
            pnTextureSelect.Controls.Add(chkEnableGuides);
            pnTextureSelect.Location = new Point(4, 548);
            pnTextureSelect.Name = "pnTextureSelect";
            pnTextureSelect.Size = new Size(1024, 262);
            pnTextureSelect.TabIndex = 15;
            // 
            // lbTextureInfos
            // 
            lbTextureInfos.AutoSize = true;
            lbTextureInfos.Location = new Point(103, 5);
            lbTextureInfos.Name = "lbTextureInfos";
            lbTextureInfos.Size = new Size(50, 15);
            lbTextureInfos.TabIndex = 26;
            lbTextureInfos.Text = "Offset: -";
            // 
            // fraTextureGuides
            // 
            fraTextureGuides.AutoSize = true;
            fraTextureGuides.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTextureGuides.Controls.Add(groupBox6);
            fraTextureGuides.Controls.Add(darkGroupBox1);
            fraTextureGuides.Controls.Add(groupBox7);
            fraTextureGuides.Enabled = false;
            fraTextureGuides.Location = new Point(3, 25);
            fraTextureGuides.Name = "fraTextureGuides";
            fraTextureGuides.Size = new Size(450, 143);
            fraTextureGuides.TabIndex = 25;
            fraTextureGuides.TabStop = false;
            fraTextureGuides.Text = "Guide";
            fraTextureGuides.Visible = false;
            // 
            // groupBox6
            // 
            groupBox6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox6.Controls.Add(label14);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(C2numY);
            groupBox6.Controls.Add(C2numX);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(C2numY2);
            groupBox6.Controls.Add(C2numX2);
            groupBox6.Location = new Point(7, 22);
            groupBox6.Margin = new Padding(4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4);
            groupBox6.Size = new Size(206, 98);
            groupBox6.TabIndex = 20;
            groupBox6.TabStop = false;
            groupBox6.Text = "Offset";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ImeMode = ImeMode.NoControl;
            label14.Location = new Point(109, 65);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(19, 15);
            label14.TabIndex = 5;
            label14.Text = "0x";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ImeMode = ImeMode.NoControl;
            label13.Location = new Point(109, 29);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(19, 15);
            label13.TabIndex = 4;
            label13.Text = "0x";
            // 
            // C2numY
            // 
            C2numY.Location = new Point(30, 61);
            C2numY.Margin = new Padding(4);
            C2numY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numY.Name = "C2numY";
            C2numY.Size = new Size(70, 23);
            C2numY.TabIndex = 3;
            // 
            // C2numX
            // 
            C2numX.Location = new Point(30, 25);
            C2numX.Margin = new Padding(4);
            C2numX.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numX.Name = "C2numX";
            C2numX.Size = new Size(70, 23);
            C2numX.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(8, 65);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(14, 15);
            label7.TabIndex = 1;
            label7.Text = "Y";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ImeMode = ImeMode.NoControl;
            label8.Location = new Point(8, 29);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(14, 15);
            label8.TabIndex = 0;
            label8.Text = "X";
            // 
            // C2numY2
            // 
            C2numY2.Hexadecimal = true;
            C2numY2.Location = new Point(128, 61);
            C2numY2.Margin = new Padding(4);
            C2numY2.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numY2.Name = "C2numY2";
            C2numY2.Size = new Size(70, 23);
            C2numY2.TabIndex = 3;
            // 
            // C2numX2
            // 
            C2numX2.Hexadecimal = true;
            C2numX2.Location = new Point(128, 25);
            C2numX2.Margin = new Padding(4);
            C2numX2.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numX2.Name = "C2numX2";
            C2numX2.Size = new Size(70, 23);
            C2numX2.TabIndex = 2;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox1.Controls.Add(numSelectionSize);
            darkGroupBox1.Location = new Point(335, 22);
            darkGroupBox1.Margin = new Padding(4);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Padding = new Padding(4);
            darkGroupBox1.Size = new Size(108, 56);
            darkGroupBox1.TabIndex = 21;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Selection Size";
            // 
            // numSelectionSize
            // 
            numSelectionSize.Location = new Point(8, 25);
            numSelectionSize.Margin = new Padding(4);
            numSelectionSize.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numSelectionSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSelectionSize.Name = "numSelectionSize";
            numSelectionSize.Size = new Size(70, 23);
            numSelectionSize.TabIndex = 4;
            numSelectionSize.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // groupBox7
            // 
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(C2numH);
            groupBox7.Controls.Add(label9);
            groupBox7.Controls.Add(C2numW);
            groupBox7.Controls.Add(label10);
            groupBox7.Location = new Point(220, 22);
            groupBox7.Margin = new Padding(4);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4);
            groupBox7.Size = new Size(107, 98);
            groupBox7.TabIndex = 21;
            groupBox7.TabStop = false;
            groupBox7.Text = "Size";
            // 
            // C2numH
            // 
            C2numH.Location = new Point(30, 61);
            C2numH.Margin = new Padding(4);
            C2numH.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            C2numH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numH.Name = "C2numH";
            C2numH.Size = new Size(70, 23);
            C2numH.TabIndex = 5;
            C2numH.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(8, 65);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(16, 15);
            label9.TabIndex = 1;
            label9.Text = "H";
            // 
            // C2numW
            // 
            C2numW.Location = new Point(30, 25);
            C2numW.Margin = new Padding(4);
            C2numW.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            C2numW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numW.Name = "C2numW";
            C2numW.Size = new Size(70, 23);
            C2numW.TabIndex = 4;
            C2numW.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ImeMode = ImeMode.NoControl;
            label10.Location = new Point(8, 29);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(18, 15);
            label10.TabIndex = 0;
            label10.Text = "W";
            // 
            // chkEnableGuides
            // 
            chkEnableGuides.AutoSize = true;
            chkEnableGuides.BackColor = Color.Transparent;
            chkEnableGuides.Location = new Point(3, 3);
            chkEnableGuides.Name = "chkEnableGuides";
            chkEnableGuides.Size = new Size(94, 19);
            chkEnableGuides.TabIndex = 8;
            chkEnableGuides.Text = "Enable guide";
            chkEnableGuides.UseVisualStyleBackColor = false;
            chkEnableGuides.CheckedChanged += chkEnableGuides_CheckedChanged;
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
            pnTextureControls.Controls.Add(cmdMoveTexture);
            pnTextureControls.Controls.Add(chkRegionEndFlag);
            pnTextureControls.Controls.Add(fraSwitches);
            pnTextureControls.Controls.Add(fraReplaceTexture);
            pnTextureControls.Controls.Add(numRowIndex);
            pnTextureControls.Controls.Add(cmdLoadTexture);
            pnTextureControls.Controls.Add(fraReplace);
            pnTextureControls.Location = new Point(618, 0);
            pnTextureControls.Name = "pnTextureControls";
            pnTextureControls.Size = new Size(214, 372);
            pnTextureControls.TabIndex = 13;
            // 
            // cmdMoveTexture
            // 
            cmdMoveTexture.BorderColour = Color.Empty;
            cmdMoveTexture.CustomColour = false;
            cmdMoveTexture.FlatBottom = false;
            cmdMoveTexture.FlatTop = false;
            cmdMoveTexture.Location = new Point(19, 334);
            cmdMoveTexture.Name = "cmdMoveTexture";
            cmdMoveTexture.Padding = new Padding(5);
            cmdMoveTexture.Size = new Size(75, 23);
            cmdMoveTexture.TabIndex = 5;
            cmdMoveTexture.Text = "Move";
            cmdMoveTexture.Click += cmdMoveTexture_Click;
            // 
            // chkRegionEndFlag
            // 
            chkRegionEndFlag.AutoSize = true;
            chkRegionEndFlag.Enabled = false;
            chkRegionEndFlag.Location = new Point(3, 61);
            chkRegionEndFlag.Name = "chkRegionEndFlag";
            chkRegionEndFlag.Size = new Size(83, 19);
            chkRegionEndFlag.TabIndex = 11;
            chkRegionEndFlag.Text = "RegionEnd";
            chkRegionEndFlag.UseVisualStyleBackColor = true;
            chkRegionEndFlag.Click += chkRegionEndFlag_Click;
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
            fraTPage.Controls.Add(lblEIDError);
            fraTPage.Controls.Add(rbtReloadTPage);
            fraTPage.Controls.Add(dpdTPages);
            fraTPage.Controls.Add(cmdRemoveTPage);
            fraTPage.Controls.Add(cmdAppendTPage);
            fraTPage.Controls.Add(lstTPages);
            fraTPage.Location = new Point(3, 3);
            fraTPage.Name = "fraTPage";
            fraTPage.Size = new Size(122, 333);
            fraTPage.TabIndex = 0;
            fraTPage.TabStop = false;
            fraTPage.Text = "Texture Pages";
            // 
            // lblEIDError
            // 
            lblEIDError.AutoSize = true;
            lblEIDError.ForeColor = Color.Red;
            lblEIDError.Location = new Point(1, 298);
            lblEIDError.Name = "lblEIDError";
            lblEIDError.Size = new Size(83, 30);
            lblEIDError.TabIndex = 11;
            lblEIDError.Text = "Texture page\r\ndoes not exist!";
            lblEIDError.Visible = false;
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
            rbtReloadTPage.Location = new Point(97, 308);
            rbtReloadTPage.Name = "rbtReloadTPage";
            rbtReloadTPage.Size = new Size(19, 17);
            rbtReloadTPage.Style = MetroSet_UI.Enums.Style.Dark;
            rbtReloadTPage.StyleManager = null;
            rbtReloadTPage.TabIndex = 10;
            rbtReloadTPage.ThemeAuthor = "Narwin";
            rbtReloadTPage.ThemeName = "MetroDark";
            rbtReloadTPage.Click += rbtReloadTPage_Click;
            // 
            // dpdTPages
            // 
            dpdTPages.DrawMode = DrawMode.OwnerDrawVariable;
            dpdTPages.Enabled = false;
            dpdTPages.Location = new Point(6, 208);
            dpdTPages.MaxLength = 5;
            dpdTPages.Name = "dpdTPages";
            dpdTPages.Size = new Size(110, 24);
            dpdTPages.TabIndex = 2;
            dpdTPages.SelectedIndexChanged += dpdTPages_SelectedIndexChanged;
            // 
            // cmdRemoveTPage
            // 
            cmdRemoveTPage.BorderColour = Color.Empty;
            cmdRemoveTPage.CustomColour = false;
            cmdRemoveTPage.Enabled = false;
            cmdRemoveTPage.FlatBottom = false;
            cmdRemoveTPage.FlatTop = false;
            cmdRemoveTPage.Location = new Point(23, 266);
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
            cmdAppendTPage.Location = new Point(23, 237);
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
            lstTPages.Scrollable = false;
            lstTPages.Size = new Size(110, 180);
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
            trkPictureSize.Minimum = 70;
            trkPictureSize.Name = "trkPictureSize";
            trkPictureSize.Size = new Size(75, 16);
            trkPictureSize.Style = MetroSet_UI.Enums.Style.Dark;
            trkPictureSize.StyleManager = null;
            trkPictureSize.TabIndex = 12;
            trkPictureSize.Text = "metroSetTrackBar1";
            trkPictureSize.ThemeAuthor = "Narwin";
            trkPictureSize.ThemeName = "MetroDark";
            trkPictureSize.TickFrequency = 5;
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
            dgvTextures.Location = new Point(131, 3);
            dgvTextures.Name = "dgvTextures";
            dgvTextures.RowHeadersWidth = 24;
            dgvTextures.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTextures.ScrollBars = ScrollBars.Vertical;
            dgvTextures.ShowCellToolTips = false;
            dgvTextures.Size = new Size(481, 372);
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
            tbpExtendedTextures.AutoScroll = true;
            tbpExtendedTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpExtendedTextures.Controls.Add(fraExTexControls);
            tbpExtendedTextures.Controls.Add(panel3);
            tbpExtendedTextures.Location = new Point(4, 32);
            tbpExtendedTextures.Name = "tbpExtendedTextures";
            tbpExtendedTextures.Size = new Size(1032, 1004);
            tbpExtendedTextures.TabIndex = 3;
            tbpExtendedTextures.Text = "Extended Textures";
            tbpExtendedTextures.Enter += tbpExtendedTextures_Enter;
            // 
            // fraExTexControls
            // 
            fraExTexControls.BackColor = Color.Transparent;
            fraExTexControls.Controls.Add(lbExTextureInfos);
            fraExTexControls.Controls.Add(cmdAppendExTex);
            fraExTexControls.Controls.Add(cmdRemoveExTex);
            fraExTexControls.Location = new Point(3, 403);
            fraExTexControls.Name = "fraExTexControls";
            fraExTexControls.Size = new Size(203, 78);
            fraExTexControls.TabIndex = 3;
            fraExTexControls.TabStop = false;
            // 
            // lbExTextureInfos
            // 
            lbExTextureInfos.AutoSize = true;
            lbExTextureInfos.Location = new Point(87, 8);
            lbExTextureInfos.Name = "lbExTextureInfos";
            lbExTextureInfos.Size = new Size(51, 30);
            lbExTextureInfos.TabIndex = 3;
            lbExTextureInfos.Text = "Count: -\r\nOffset: -";
            // 
            // cmdAppendExTex
            // 
            cmdAppendExTex.BorderColour = Color.Empty;
            cmdAppendExTex.CustomColour = false;
            cmdAppendExTex.FlatBottom = false;
            cmdAppendExTex.FlatTop = false;
            cmdAppendExTex.Location = new Point(6, 8);
            cmdAppendExTex.Name = "cmdAppendExTex";
            cmdAppendExTex.Padding = new Padding(5);
            cmdAppendExTex.Size = new Size(75, 28);
            cmdAppendExTex.TabIndex = 2;
            cmdAppendExTex.Text = "Append";
            cmdAppendExTex.Click += cmdAppendExTex_Click;
            // 
            // cmdRemoveExTex
            // 
            cmdRemoveExTex.BorderColour = Color.Empty;
            cmdRemoveExTex.CustomColour = false;
            cmdRemoveExTex.FlatBottom = false;
            cmdRemoveExTex.FlatTop = false;
            cmdRemoveExTex.Location = new Point(6, 42);
            cmdRemoveExTex.Name = "cmdRemoveExTex";
            cmdRemoveExTex.Padding = new Padding(5);
            cmdRemoveExTex.Size = new Size(75, 28);
            cmdRemoveExTex.TabIndex = 2;
            cmdRemoveExTex.Text = "Remove";
            cmdRemoveExTex.Click += cmdRemoveExTex_Click;
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
            dgvExtendedTextures.ShowCellToolTips = false;
            dgvExtendedTextures.Size = new Size(712, 391);
            dgvExtendedTextures.TabIndex = 0;
            dgvExtendedTextures.CellBeginEdit += dgvExtendedTextures_CellBeginEdit;
            dgvExtendedTextures.CellParsing += dgv_CellParsing;
            dgvExtendedTextures.CellValidating += dgvExtendedTextures_CellValidating;
            dgvExtendedTextures.CellValueChanged += dgvExtendedTextures_CellValueChanged;
            dgvExtendedTextures.EditingControlShowing += dgvExtendedTextures_EditingControlShowing;
            dgvExtendedTextures.SelectionChanged += dgvExtendedTextures_SelectionChanged;
            // 
            // tbpPositions
            // 
            tbpPositions.AutoScroll = true;
            tbpPositions.BackColor = Color.FromArgb(31, 31, 32);
            tbpPositions.Controls.Add(dgvPositions);
            tbpPositions.Location = new Point(4, 32);
            tbpPositions.Name = "tbpPositions";
            tbpPositions.Size = new Size(1032, 1004);
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
            dgvPositions.ShowCellToolTips = false;
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
            Size = new Size(1040, 1040);
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
            tbpVertices.ResumeLayout(false);
            tbpVertices.PerformLayout();
            fraTempVertices.ResumeLayout(false);
            fraTempVertices.PerformLayout();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picTempVertsHint).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTempVertices).EndInit();
            fraNearbyVertices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNearbyVertices).EndInit();
            fraVertices.ResumeLayout(false);
            fraVertices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numVertexIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexX).EndInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexY).EndInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexFX).EndInit();
            ((System.ComponentModel.ISupportInitialize)inpVertexColor).EndInit();
            tbpColors.ResumeLayout(false);
            tbpColors.PerformLayout();
            fraGlobalControl.ResumeLayout(false);
            pnGlobalControl.ResumeLayout(false);
            pnGlobalControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLowestBrightness).EndInit();
            pnSliders.ResumeLayout(false);
            fraColorSlider.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvColor).EndInit();
            tbpTextures.ResumeLayout(false);
            tbpTextures.PerformLayout();
            pnTextureSelect.ResumeLayout(false);
            pnTextureSelect.PerformLayout();
            fraTextureGuides.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numX).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numX2).EndInit();
            darkGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSelectionSize).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numH).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numW).EndInit();
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
            fraTPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTextures).EndInit();
            tbpExtendedTextures.ResumeLayout(false);
            tbpExtendedTextures.PerformLayout();
            fraExTexControls.ResumeLayout(false);
            fraExTexControls.PerformLayout();
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
        private TabPage tbpVertices;
        private TabPage tbpPositions;
        private TabPage tbpColors;
        private TabPage tbpTextures;
        private TabPage tbpExtendedTextures;
        private AltUI.Controls.DarkGroupBox fraTPage;
        private DoubleBufferedListView lstTPages;
        private DataGridView dgvColor;
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
        private DarkComboBox dpdTPages;
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
        private CheckBox chkRegionEndFlag;
        private DataGridView dgvExtendedTextures;
        private Panel panel3;
        private DataGridView dgvPolygons;
        private DataGridView dgvStructs;
        private Label label2;
        private Label label3;
        private DataGridView dgvPositions;
        private Label lblColorIndex;
        private Label lblStruct;
        private DarkNumericUpDown numLowestBrightness;
        private CheckBox chkLowestBrightness;
        private DarkButton cmdClearSelection;
        private PictureBox pictureBox2;
        private DarkNumericUpDown numVertexIndex;
        private DarkNumericUpDown inpVertexX;
        private DarkNumericUpDown inpVertexY;
        private DarkNumericUpDown inpVertexZ;
        private DarkNumericUpDown inpVertexFX;
        private DarkNumericUpDown inpVertexColor;
        private DarkGroupBox fraVertices;
        private Panel pnTextureSelect;
        private DarkGroupBox groupBox6;
        private Label label14;
        private Label label13;
        private DarkNumericUpDown C2numY;
        private DarkNumericUpDown C2numX;
        private Label label7;
        private Label label8;
        private DarkNumericUpDown C2numY2;
        private DarkNumericUpDown C2numX2;
        private DarkGroupBox groupBox7;
        private DarkNumericUpDown C2numH;
        private Label label9;
        private DarkNumericUpDown C2numW;
        private Label label10;
        private DarkGroupBox fraTextureGuides;
        private CheckBox chkEnableGuides;
        private DarkGroupBox darkGroupBox1;
        private DarkNumericUpDown darkNumericUpDown1;
        private Label lblVertexIndex;
        private DarkNumericUpDown numSelectionSize;
        private Label label5;
        private Label lblEIDError;
        private DarkButton cmdAppendExTex;
        private DarkButton cmdRemoveExTex;
        private DarkGroupBox fraExTexControls;
        private DarkLabel lbExTextureInfos;
        private DarkLabel lbTextureInfos;
        private DataGridView dgvNearbyVertices;
        private DarkGroupBox fraNearbyVertices;
        private CheckBox chkEditNearbyVertices;
        private Label lblVertX;
        private Label lblVertColor;
        private Label lblVertFX;
        private Label lblVertZ;
        private Label lblVertY;
        private DarkLabel lblVertices;
        private DarkGroupBox fraTempVertices;
        private DataGridView dgvTempVertices;
        private DarkButton cmdClearTempVerts;
        private DarkButton cmdRemoveTempVerts;
        private CheckBox chkEditTempVertices;
        private CheckBox chkTempAddCoVerts;
        private PictureBox picTempVertsHint;
        private Panel panel4;
        private DarkButton cmdMoveTexture;
    }
}
