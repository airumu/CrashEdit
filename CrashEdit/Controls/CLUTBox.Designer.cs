using System.Windows.Forms;
using AltUI.Controls;

namespace CrashEdit.CE.Controls
{
    partial class CLUTBox
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
            dgvCLUT = new DataGridView();
            cmdLoadCLUT = new DarkButton();
            numLoadClut = new DarkNumericUpDown();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            fraGlobalControl = new DarkGroupBox();
            cmdCancel = new DarkButton();
            cmdApply = new DarkButton();
            pnGlobalControl = new Panel();
            fraSTPbit = new DarkGroupBox();
            cmdSetSTPbit = new DarkButton();
            cmdRemoveSTPbit = new DarkButton();
            fraGlobalSlider = new DarkGroupBox();
            colorEditorGlobal = new Cyotek.Windows.Forms.ColorEditor();
            rdiModeSelectedCells = new MetroSet_UI.Controls.MetroSetRadioButton();
            fraCLUT = new DarkGroupBox();
            lblCLUTTo = new Label();
            lblCLUTFrom = new Label();
            lblClutX = new Label();
            numClutX2 = new DarkNumericUpDown();
            numClutY2 = new DarkNumericUpDown();
            label1 = new Label();
            label2 = new Label();
            numClutY1 = new DarkNumericUpDown();
            lblClutY = new Label();
            numClutX1 = new DarkNumericUpDown();
            rdiModeCLUT = new MetroSet_UI.Controls.MetroSetRadioButton();
            tglGlobalControl = new MetroSet_UI.Controls.MetroSetSwitch();
            fraSlider = new DarkGroupBox();
            chkSTPbit = new CheckBox();
            fraCount = new DarkGroupBox();
            chkHighlightSTPbit = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dgvCLUT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).BeginInit();
            fraGlobalControl.SuspendLayout();
            pnGlobalControl.SuspendLayout();
            fraSTPbit.SuspendLayout();
            fraGlobalSlider.SuspendLayout();
            fraCLUT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numClutX2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutX1).BeginInit();
            fraSlider.SuspendLayout();
            fraCount.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCLUT
            // 
            dgvCLUT.AllowUserToAddRows = false;
            dgvCLUT.AllowUserToDeleteRows = false;
            dgvCLUT.AllowUserToResizeColumns = false;
            dgvCLUT.AllowUserToResizeRows = false;
            dgvCLUT.ColumnHeadersHeight = 24;
            dgvCLUT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCLUT.Location = new Point(0, 0);
            dgvCLUT.Name = "dgvCLUT";
            dgvCLUT.ReadOnly = true;
            dgvCLUT.RowHeadersWidth = 24;
            dgvCLUT.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvCLUT.ScrollBars = ScrollBars.Vertical;
            dgvCLUT.Size = new Size(516, 624);
            dgvCLUT.TabIndex = 0;
            dgvCLUT.CellPainting += dgvCLUT_CellPainting;
            dgvCLUT.SelectionChanged += dgvCLUT_SelectionChanged;
            dgvCLUT.KeyDown += dgvCLUT_KeyDown;
            // 
            // cmdLoadCLUT
            // 
            cmdLoadCLUT.BorderColour = Color.Empty;
            cmdLoadCLUT.CustomColour = false;
            cmdLoadCLUT.FlatBottom = false;
            cmdLoadCLUT.FlatTop = false;
            cmdLoadCLUT.Location = new Point(6, 51);
            cmdLoadCLUT.Name = "cmdLoadCLUT";
            cmdLoadCLUT.Padding = new Padding(5);
            cmdLoadCLUT.Size = new Size(75, 23);
            cmdLoadCLUT.TabIndex = 1;
            cmdLoadCLUT.Text = "Load";
            cmdLoadCLUT.Click += cmdLoadCLUT_Click;
            // 
            // numLoadClut
            // 
            numLoadClut.Location = new Point(6, 22);
            numLoadClut.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            numLoadClut.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numLoadClut.Name = "numLoadClut";
            numLoadClut.Size = new Size(75, 23);
            numLoadClut.TabIndex = 2;
            numLoadClut.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // colorEditor
            // 
            colorEditor.AutoSize = true;
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Enabled = false;
            colorEditor.Location = new Point(3, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowColorSpaceLabels = false;
            colorEditor.Size = new Size(284, 200);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // fraGlobalControl
            // 
            fraGlobalControl.BackColor = Color.Transparent;
            fraGlobalControl.Controls.Add(cmdCancel);
            fraGlobalControl.Controls.Add(cmdApply);
            fraGlobalControl.Controls.Add(pnGlobalControl);
            fraGlobalControl.Controls.Add(tglGlobalControl);
            fraGlobalControl.Enabled = false;
            fraGlobalControl.Location = new Point(522, 314);
            fraGlobalControl.Name = "fraGlobalControl";
            fraGlobalControl.Size = new Size(297, 332);
            fraGlobalControl.TabIndex = 8;
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
            pnGlobalControl.Controls.Add(fraSTPbit);
            pnGlobalControl.Controls.Add(fraGlobalSlider);
            pnGlobalControl.Controls.Add(rdiModeSelectedCells);
            pnGlobalControl.Controls.Add(fraCLUT);
            pnGlobalControl.Controls.Add(rdiModeCLUT);
            pnGlobalControl.Enabled = false;
            pnGlobalControl.Location = new Point(6, 50);
            pnGlobalControl.Name = "pnGlobalControl";
            pnGlobalControl.Size = new Size(288, 280);
            pnGlobalControl.TabIndex = 4;
            // 
            // fraSTPbit
            // 
            fraSTPbit.Controls.Add(cmdSetSTPbit);
            fraSTPbit.Controls.Add(cmdRemoveSTPbit);
            fraSTPbit.Location = new Point(3, 222);
            fraSTPbit.Name = "fraSTPbit";
            fraSTPbit.Size = new Size(157, 54);
            fraSTPbit.TabIndex = 14;
            fraSTPbit.TabStop = false;
            fraSTPbit.Text = "STP Bit";
            // 
            // cmdSetSTPbit
            // 
            cmdSetSTPbit.BorderColour = Color.Empty;
            cmdSetSTPbit.CustomColour = false;
            cmdSetSTPbit.FlatBottom = false;
            cmdSetSTPbit.FlatTop = false;
            cmdSetSTPbit.Location = new Point(6, 22);
            cmdSetSTPbit.Name = "cmdSetSTPbit";
            cmdSetSTPbit.Padding = new Padding(5);
            cmdSetSTPbit.Size = new Size(71, 23);
            cmdSetSTPbit.TabIndex = 12;
            cmdSetSTPbit.Text = "Set";
            cmdSetSTPbit.Click += cmdSetSTPbit_Click;
            // 
            // cmdRemoveSTPbit
            // 
            cmdRemoveSTPbit.BorderColour = Color.Empty;
            cmdRemoveSTPbit.CustomColour = false;
            cmdRemoveSTPbit.FlatBottom = false;
            cmdRemoveSTPbit.FlatTop = false;
            cmdRemoveSTPbit.Location = new Point(80, 22);
            cmdRemoveSTPbit.Name = "cmdRemoveSTPbit";
            cmdRemoveSTPbit.Padding = new Padding(5);
            cmdRemoveSTPbit.Size = new Size(71, 23);
            cmdRemoveSTPbit.TabIndex = 12;
            cmdRemoveSTPbit.Text = "Remove";
            cmdRemoveSTPbit.Click += cmdRemoveSTPbit_Click;
            // 
            // fraGlobalSlider
            // 
            fraGlobalSlider.Controls.Add(colorEditorGlobal);
            fraGlobalSlider.Location = new Point(3, 112);
            fraGlobalSlider.Name = "fraGlobalSlider";
            fraGlobalSlider.Size = new Size(282, 104);
            fraGlobalSlider.TabIndex = 13;
            fraGlobalSlider.TabStop = false;
            // 
            // colorEditorGlobal
            // 
            colorEditorGlobal.AutoSize = true;
            colorEditorGlobal.Color = Color.FromArgb(0, 0, 0);
            colorEditorGlobal.Location = new Point(0, 4);
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
            // rdiModeSelectedCells
            // 
            rdiModeSelectedCells.BackgroundColor = Color.FromArgb(30, 30, 30);
            rdiModeSelectedCells.BorderColor = Color.FromArgb(155, 155, 155);
            rdiModeSelectedCells.Checked = false;
            rdiModeSelectedCells.CheckSignColor = Color.FromArgb(65, 177, 225);
            rdiModeSelectedCells.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            rdiModeSelectedCells.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rdiModeSelectedCells.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rdiModeSelectedCells.Group = 0;
            rdiModeSelectedCells.IsDerivedStyle = true;
            rdiModeSelectedCells.Location = new Point(63, 3);
            rdiModeSelectedCells.Name = "rdiModeSelectedCells";
            rdiModeSelectedCells.Size = new Size(97, 17);
            rdiModeSelectedCells.Style = MetroSet_UI.Enums.Style.Dark;
            rdiModeSelectedCells.StyleManager = null;
            rdiModeSelectedCells.TabIndex = 6;
            rdiModeSelectedCells.Text = "Selected Cells";
            rdiModeSelectedCells.ThemeAuthor = "Narwin";
            rdiModeSelectedCells.ThemeName = "MetroDark";
            rdiModeSelectedCells.Click += rdiModeSelectedCells_Click;
            // 
            // fraCLUT
            // 
            fraCLUT.Controls.Add(lblCLUTTo);
            fraCLUT.Controls.Add(lblCLUTFrom);
            fraCLUT.Controls.Add(lblClutX);
            fraCLUT.Controls.Add(numClutX2);
            fraCLUT.Controls.Add(numClutY2);
            fraCLUT.Controls.Add(numClutX1);
            fraCLUT.Controls.Add(label1);
            fraCLUT.Controls.Add(label2);
            fraCLUT.Controls.Add(numClutY1);
            fraCLUT.Controls.Add(lblClutY);
            fraCLUT.Location = new Point(3, 26);
            fraCLUT.Name = "fraCLUT";
            fraCLUT.Size = new Size(208, 80);
            fraCLUT.TabIndex = 10;
            fraCLUT.TabStop = false;
            // 
            // lblCLUTTo
            // 
            lblCLUTTo.AutoSize = true;
            lblCLUTTo.Location = new Point(19, 51);
            lblCLUTTo.Name = "lblCLUTTo";
            lblCLUTTo.Size = new Size(19, 15);
            lblCLUTTo.TabIndex = 3;
            lblCLUTTo.Text = "To";
            // 
            // lblCLUTFrom
            // 
            lblCLUTFrom.AutoSize = true;
            lblCLUTFrom.Location = new Point(3, 22);
            lblCLUTFrom.Name = "lblCLUTFrom";
            lblCLUTFrom.Size = new Size(35, 15);
            lblCLUTFrom.TabIndex = 3;
            lblCLUTFrom.Text = "From";
            // 
            // lblClutX
            // 
            lblClutX.AutoSize = true;
            lblClutX.Location = new Point(148, 2);
            lblClutX.Name = "lblClutX";
            lblClutX.Size = new Size(45, 15);
            lblClutX.TabIndex = 3;
            lblClutX.Text = "CLUT X";
            // 
            // numClutX2
            // 
            numClutX2.Location = new Point(138, 49);
            numClutX2.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numClutX2.Name = "numClutX2";
            numClutX2.Size = new Size(64, 23);
            numClutX2.TabIndex = 2;
            numClutX2.ValueChanged += numClutX2_ValueChanged;
            // 
            // numClutY2
            // 
            numClutY2.Location = new Point(44, 49);
            numClutY2.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numClutY2.Name = "numClutY2";
            numClutY2.Size = new Size(64, 23);
            numClutY2.TabIndex = 2;
            numClutY2.ValueChanged += numClutY2_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(116, 22);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 3;
            label1.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(116, 51);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 3;
            label2.Text = "-";
            // 
            // numClutY1
            // 
            numClutY1.Location = new Point(44, 20);
            numClutY1.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numClutY1.Name = "numClutY1";
            numClutY1.Size = new Size(64, 23);
            numClutY1.TabIndex = 2;
            numClutY1.ValueChanged += numClutY1_ValueChanged;
            // 
            // lblClutY
            // 
            lblClutY.AutoSize = true;
            lblClutY.Location = new Point(52, 2);
            lblClutY.Name = "lblClutY";
            lblClutY.Size = new Size(45, 15);
            lblClutY.TabIndex = 3;
            lblClutY.Text = "CLUT Y";
            // 
            // numClutX1
            // 
            numClutX1.Location = new Point(138, 20);
            numClutX1.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numClutX1.Name = "numClutX1";
            numClutX1.Size = new Size(64, 23);
            numClutX1.TabIndex = 2;
            numClutX1.ValueChanged += numClutX1_ValueChanged;
            // 
            // rdiModeCLUT
            // 
            rdiModeCLUT.BackgroundColor = Color.FromArgb(30, 30, 30);
            rdiModeCLUT.BorderColor = Color.FromArgb(155, 155, 155);
            rdiModeCLUT.Checked = true;
            rdiModeCLUT.CheckSignColor = Color.FromArgb(65, 177, 225);
            rdiModeCLUT.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            rdiModeCLUT.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rdiModeCLUT.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rdiModeCLUT.Group = 0;
            rdiModeCLUT.IsDerivedStyle = true;
            rdiModeCLUT.Location = new Point(3, 3);
            rdiModeCLUT.Name = "rdiModeCLUT";
            rdiModeCLUT.Size = new Size(58, 17);
            rdiModeCLUT.Style = MetroSet_UI.Enums.Style.Dark;
            rdiModeCLUT.StyleManager = null;
            rdiModeCLUT.TabIndex = 6;
            rdiModeCLUT.Text = "CLUT";
            rdiModeCLUT.ThemeAuthor = "Narwin";
            rdiModeCLUT.ThemeName = "MetroDark";
            rdiModeCLUT.Click += rdiModeCLUT_Click;
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
            // fraSlider
            // 
            fraSlider.Controls.Add(chkSTPbit);
            fraSlider.Controls.Add(colorEditor);
            fraSlider.Enabled = false;
            fraSlider.Location = new Point(522, 89);
            fraSlider.Name = "fraSlider";
            fraSlider.Size = new Size(297, 219);
            fraSlider.TabIndex = 9;
            fraSlider.TabStop = false;
            // 
            // chkSTPbit
            // 
            chkSTPbit.AutoSize = true;
            chkSTPbit.Location = new Point(9, 194);
            chkSTPbit.Name = "chkSTPbit";
            chkSTPbit.Size = new Size(62, 19);
            chkSTPbit.TabIndex = 11;
            chkSTPbit.Text = "STP Bit";
            chkSTPbit.UseVisualStyleBackColor = true;
            chkSTPbit.Click += chkSTPbit_Click;
            // 
            // fraCount
            // 
            fraCount.BackColor = Color.Transparent;
            fraCount.Controls.Add(numLoadClut);
            fraCount.Controls.Add(cmdLoadCLUT);
            fraCount.Location = new Point(522, 3);
            fraCount.Name = "fraCount";
            fraCount.Size = new Size(92, 80);
            fraCount.TabIndex = 10;
            fraCount.TabStop = false;
            fraCount.Text = "Count";
            // 
            // chkHighlightSTPbit
            // 
            chkHighlightSTPbit.AutoSize = true;
            chkHighlightSTPbit.Location = new Point(620, 3);
            chkHighlightSTPbit.Name = "chkHighlightSTPbit";
            chkHighlightSTPbit.Size = new Size(172, 19);
            chkHighlightSTPbit.TabIndex = 11;
            chkHighlightSTPbit.Text = "Highlight cells with STP bits";
            chkHighlightSTPbit.UseVisualStyleBackColor = true;
            chkHighlightSTPbit.CheckedChanged += chkHighlightSTPbit_CheckedChanged;
            // 
            // CLUTBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(chkHighlightSTPbit);
            Controls.Add(fraCount);
            Controls.Add(fraSlider);
            Controls.Add(fraGlobalControl);
            Controls.Add(dgvCLUT);
            Name = "CLUTBox";
            Size = new Size(1000, 1000);
            Leave += CLUTBox_Leave;
            ((System.ComponentModel.ISupportInitialize)dgvCLUT).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).EndInit();
            fraGlobalControl.ResumeLayout(false);
            pnGlobalControl.ResumeLayout(false);
            fraSTPbit.ResumeLayout(false);
            fraGlobalSlider.ResumeLayout(false);
            fraGlobalSlider.PerformLayout();
            fraCLUT.ResumeLayout(false);
            fraCLUT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numClutX2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutX1).EndInit();
            fraSlider.ResumeLayout(false);
            fraSlider.PerformLayout();
            fraCount.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCLUT;
        private AltUI.Controls.DarkButton cmdLoadCLUT;
        private AltUI.Controls.DarkNumericUpDown numLoadClut;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private AltUI.Controls.DarkGroupBox fraGlobalControl;
        private AltUI.Controls.DarkButton cmdCancel;
        private Label lblClutX;
        private AltUI.Controls.DarkButton cmdApply;
        private Panel pnGlobalControl;
        private MetroSet_UI.Controls.MetroSetSwitch tglGlobalControl;
        private AltUI.Controls.DarkNumericUpDown numClutY2;
        private AltUI.Controls.DarkNumericUpDown numClutX2;
        private AltUI.Controls.DarkNumericUpDown numClutY1;
        private AltUI.Controls.DarkNumericUpDown numClutX1;
        private Label lblClutY;
        private Label label2;
        private Label label1;
        private AltUI.Controls.DarkGroupBox fraSlider;
        private MetroSet_UI.Controls.MetroSetRadioButton rdiModeCLUT;
        private MetroSet_UI.Controls.MetroSetRadioButton rdiModeSelectedCells;
        private DarkGroupBox fraCLUT;
        private AltUI.Controls.DarkGroupBox fraCount;
        private Label lblCLUTTo;
        private Label lblCLUTFrom;
        private Cyotek.Windows.Forms.ColorEditor colorEditorGlobal;
        private CheckBox chkSTPbit;
        private AltUI.Controls.DarkButton cmdRemoveSTPbit;
        private AltUI.Controls.DarkButton cmdSetSTPbit;
        private AltUI.Controls.DarkGroupBox fraSTPbit;
        private AltUI.Controls.DarkGroupBox fraGlobalSlider;
        private CheckBox chkHighlightSTPbit;
    }
}
