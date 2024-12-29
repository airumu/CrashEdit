using System.Windows.Forms;

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
            grdCLUT = new DataGridView();
            cmdLoadCLUT = new AltUI.Controls.DarkButton();
            numLoadClut = new AltUI.Controls.DarkNumericUpDown();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            fraGlobalControl = new AltUI.Controls.DarkGroupBox();
            cmdCancel = new AltUI.Controls.DarkButton();
            cmdApply = new AltUI.Controls.DarkButton();
            pnGlobalControl = new Panel();
            rdiModeSelectedCells = new MetroSet_UI.Controls.MetroSetRadioButton();
            pnCLUT = new Panel();
            lblCLUTTo = new Label();
            lblCLUTFrom = new Label();
            lblClutX = new Label();
            numClutX2 = new AltUI.Controls.DarkNumericUpDown();
            numClutY2 = new AltUI.Controls.DarkNumericUpDown();
            label1 = new Label();
            label2 = new Label();
            numClutY1 = new AltUI.Controls.DarkNumericUpDown();
            lblClutY = new Label();
            numClutX1 = new AltUI.Controls.DarkNumericUpDown();
            saturationColorSlider = new Cyotek.Windows.Forms.SaturationColorSlider();
            hueColorSlider = new Cyotek.Windows.Forms.HueColorSlider();
            lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            rdiModeCLUT = new MetroSet_UI.Controls.MetroSetRadioButton();
            tglGlobalControl = new MetroSet_UI.Controls.MetroSetSwitch();
            fraSlider = new AltUI.Controls.DarkGroupBox();
            pnSliders = new Panel();
            fraCount = new AltUI.Controls.DarkGroupBox();
            ((System.ComponentModel.ISupportInitialize)grdCLUT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).BeginInit();
            fraGlobalControl.SuspendLayout();
            pnGlobalControl.SuspendLayout();
            pnCLUT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numClutX2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numClutX1).BeginInit();
            fraSlider.SuspendLayout();
            pnSliders.SuspendLayout();
            fraCount.SuspendLayout();
            SuspendLayout();
            // 
            // grdCLUT
            // 
            grdCLUT.AllowUserToAddRows = false;
            grdCLUT.AllowUserToDeleteRows = false;
            grdCLUT.AllowUserToResizeColumns = false;
            grdCLUT.AllowUserToResizeRows = false;
            grdCLUT.ColumnHeadersHeight = 24;
            grdCLUT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grdCLUT.Location = new Point(0, 0);
            grdCLUT.Name = "grdCLUT";
            grdCLUT.ReadOnly = true;
            grdCLUT.RowHeadersWidth = 24;
            grdCLUT.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            grdCLUT.ScrollBars = ScrollBars.Vertical;
            grdCLUT.Size = new Size(538, 600);
            grdCLUT.TabIndex = 0;
            grdCLUT.CellPainting += grdCLUT_CellPainting;
            grdCLUT.SelectionChanged += grdCLUT_SelectionChanged;
            grdCLUT.KeyDown += grdCLUT_KeyDown;
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
            numLoadClut.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numLoadClut.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numLoadClut.Name = "numLoadClut";
            numLoadClut.Size = new Size(75, 23);
            numLoadClut.TabIndex = 2;
            numLoadClut.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Enabled = false;
            colorEditor.Location = new Point(1, 1);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowColorSpaceLabels = false;
            colorEditor.Size = new Size(284, 195);
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
            fraGlobalControl.Location = new Point(544, 295);
            fraGlobalControl.Name = "fraGlobalControl";
            fraGlobalControl.Size = new Size(239, 282);
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
            pnGlobalControl.Controls.Add(rdiModeSelectedCells);
            pnGlobalControl.Controls.Add(pnCLUT);
            pnGlobalControl.Controls.Add(saturationColorSlider);
            pnGlobalControl.Controls.Add(hueColorSlider);
            pnGlobalControl.Controls.Add(lightnessColorSlider);
            pnGlobalControl.Controls.Add(rdiModeCLUT);
            pnGlobalControl.Enabled = false;
            pnGlobalControl.Location = new Point(6, 50);
            pnGlobalControl.Name = "pnGlobalControl";
            pnGlobalControl.Size = new Size(227, 223);
            pnGlobalControl.TabIndex = 4;
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
            rdiModeSelectedCells.Text = "Selected cells";
            rdiModeSelectedCells.ThemeAuthor = "Narwin";
            rdiModeSelectedCells.ThemeName = "MetroDark";
            rdiModeSelectedCells.Click += rdiModeSelectedCells_Click;
            // 
            // pnCLUT
            // 
            pnCLUT.Controls.Add(lblCLUTTo);
            pnCLUT.Controls.Add(lblCLUTFrom);
            pnCLUT.Controls.Add(lblClutX);
            pnCLUT.Controls.Add(numClutX2);
            pnCLUT.Controls.Add(numClutY2);
            pnCLUT.Controls.Add(label1);
            pnCLUT.Controls.Add(label2);
            pnCLUT.Controls.Add(numClutY1);
            pnCLUT.Controls.Add(lblClutY);
            pnCLUT.Controls.Add(numClutX1);
            pnCLUT.Location = new Point(3, 26);
            pnCLUT.Name = "pnCLUT";
            pnCLUT.Size = new Size(221, 89);
            pnCLUT.TabIndex = 10;
            // 
            // lblCLUTTo
            // 
            lblCLUTTo.AutoSize = true;
            lblCLUTTo.Location = new Point(35, 53);
            lblCLUTTo.Name = "lblCLUTTo";
            lblCLUTTo.Size = new Size(19, 15);
            lblCLUTTo.TabIndex = 3;
            lblCLUTTo.Text = "To";
            // 
            // lblCLUTFrom
            // 
            lblCLUTFrom.AutoSize = true;
            lblCLUTFrom.Location = new Point(19, 24);
            lblCLUTFrom.Name = "lblCLUTFrom";
            lblCLUTFrom.Size = new Size(35, 15);
            lblCLUTFrom.TabIndex = 3;
            lblCLUTFrom.Text = "From";
            // 
            // lblClutX
            // 
            lblClutX.AutoSize = true;
            lblClutX.Location = new Point(68, 4);
            lblClutX.Name = "lblClutX";
            lblClutX.Size = new Size(45, 15);
            lblClutX.TabIndex = 3;
            lblClutX.Text = "CLUT X";
            // 
            // numClutX2
            // 
            numClutX2.Location = new Point(60, 51);
            numClutX2.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numClutX2.Name = "numClutX2";
            numClutX2.Size = new Size(64, 23);
            numClutX2.TabIndex = 2;
            numClutX2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numClutX2.ValueChanged += numClutX2_ValueChanged;
            // 
            // numClutY2
            // 
            numClutY2.Location = new Point(154, 51);
            numClutY2.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numClutY2.Name = "numClutY2";
            numClutY2.Size = new Size(64, 23);
            numClutY2.TabIndex = 2;
            numClutY2.ValueChanged += numClutY2_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(132, 24);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 3;
            label1.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(132, 53);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 3;
            label2.Text = "-";
            // 
            // numClutY1
            // 
            numClutY1.Location = new Point(154, 22);
            numClutY1.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numClutY1.Name = "numClutY1";
            numClutY1.Size = new Size(64, 23);
            numClutY1.TabIndex = 2;
            numClutY1.ValueChanged += numClutY1_ValueChanged;
            // 
            // lblClutY
            // 
            lblClutY.AutoSize = true;
            lblClutY.Location = new Point(164, 4);
            lblClutY.Name = "lblClutY";
            lblClutY.Size = new Size(45, 15);
            lblClutY.TabIndex = 3;
            lblClutY.Text = "CLUT Y";
            // 
            // numClutX1
            // 
            numClutX1.Location = new Point(60, 22);
            numClutX1.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numClutX1.Name = "numClutX1";
            numClutX1.Size = new Size(64, 23);
            numClutX1.TabIndex = 2;
            numClutX1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numClutX1.ValueChanged += numClutX1_ValueChanged;
            // 
            // saturationColorSlider
            // 
            saturationColorSlider.Location = new Point(3, 157);
            saturationColorSlider.Name = "saturationColorSlider";
            saturationColorSlider.Size = new Size(219, 29);
            saturationColorSlider.TabIndex = 0;
            saturationColorSlider.Value = 50F;
            saturationColorSlider.ValueChanged += saturationColorSlider_ValueChanged;
            // 
            // hueColorSlider
            // 
            hueColorSlider.Location = new Point(3, 120);
            hueColorSlider.Name = "hueColorSlider";
            hueColorSlider.Size = new Size(219, 29);
            hueColorSlider.TabIndex = 0;
            hueColorSlider.Value = 180F;
            hueColorSlider.ValueChanged += hueColorSlider_ValueChanged;
            // 
            // lightnessColorSlider
            // 
            lightnessColorSlider.Color = Color.FromArgb(127, 127, 127);
            lightnessColorSlider.Location = new Point(3, 193);
            lightnessColorSlider.Name = "lightnessColorSlider";
            lightnessColorSlider.Size = new Size(219, 29);
            lightnessColorSlider.TabIndex = 0;
            lightnessColorSlider.Value = 50F;
            lightnessColorSlider.ValueChanged += lightnessColorSlider_ValueChanged;
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
            fraSlider.Controls.Add(pnSliders);
            fraSlider.Enabled = false;
            fraSlider.Location = new Point(544, 85);
            fraSlider.Name = "fraSlider";
            fraSlider.Size = new Size(297, 206);
            fraSlider.TabIndex = 9;
            fraSlider.TabStop = false;
            // 
            // pnSliders
            // 
            pnSliders.Controls.Add(colorEditor);
            pnSliders.Location = new Point(3, 3);
            pnSliders.Name = "pnSliders";
            pnSliders.Size = new Size(291, 197);
            pnSliders.TabIndex = 1;
            // 
            // fraCount
            // 
            fraCount.BackColor = Color.Transparent;
            fraCount.Controls.Add(numLoadClut);
            fraCount.Controls.Add(cmdLoadCLUT);
            fraCount.Location = new Point(544, 3);
            fraCount.Name = "fraCount";
            fraCount.Size = new Size(92, 80);
            fraCount.TabIndex = 10;
            fraCount.TabStop = false;
            fraCount.Text = "Count";
            // 
            // CLUTBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(fraCount);
            Controls.Add(fraSlider);
            Controls.Add(fraGlobalControl);
            Controls.Add(grdCLUT);
            Name = "CLUTBox";
            Size = new Size(1000, 1000);
            ((System.ComponentModel.ISupportInitialize)grdCLUT).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).EndInit();
            fraGlobalControl.ResumeLayout(false);
            pnGlobalControl.ResumeLayout(false);
            pnCLUT.ResumeLayout(false);
            pnCLUT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numClutX2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numClutX1).EndInit();
            fraSlider.ResumeLayout(false);
            pnSliders.ResumeLayout(false);
            fraCount.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grdCLUT;
        private AltUI.Controls.DarkButton cmdLoadCLUT;
        private AltUI.Controls.DarkNumericUpDown numLoadClut;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private AltUI.Controls.DarkGroupBox fraGlobalControl;
        private AltUI.Controls.DarkButton cmdCancel;
        private Label lblClutX;
        private AltUI.Controls.DarkButton cmdApply;
        private Panel pnGlobalControl;
        private Cyotek.Windows.Forms.SaturationColorSlider saturationColorSlider;
        private Cyotek.Windows.Forms.HueColorSlider hueColorSlider;
        private Cyotek.Windows.Forms.LightnessColorSlider lightnessColorSlider;
        private MetroSet_UI.Controls.MetroSetSwitch tglGlobalControl;
        private AltUI.Controls.DarkNumericUpDown numClutY2;
        private AltUI.Controls.DarkNumericUpDown numClutX2;
        private AltUI.Controls.DarkNumericUpDown numClutY1;
        private AltUI.Controls.DarkNumericUpDown numClutX1;
        private Label lblClutY;
        private Label label2;
        private Label label1;
        private AltUI.Controls.DarkGroupBox fraSlider;
        private Panel pnSliders;
        private MetroSet_UI.Controls.MetroSetRadioButton rdiModeCLUT;
        private MetroSet_UI.Controls.MetroSetRadioButton rdiModeSelectedCells;
        private Panel pnCLUT;
        private AltUI.Controls.DarkGroupBox fraCount;
        private Label lblCLUTTo;
        private Label lblCLUTFrom;
    }
}
