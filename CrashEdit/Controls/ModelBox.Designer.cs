using System.Windows.Forms;
using AltUI.ColorPicker;
using Cyotek.Windows.Forms;
using MetroSet_UI.Controls;

namespace CrashEdit.CE.Controls
{
    partial class ModelBox
    {
        public class DoubleBufferedListView : ListView
        {
            public DoubleBufferedListView()
            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
                this.UpdateStyles();
            }
        }

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
            tbpInfo = new TabPage();
            tbpColors = new TabPage();
            fraGlobalControl = new AltUI.Controls.DarkGroupBox();
            cmdCancel = new AltUI.Controls.DarkButton();
            cmdApply = new AltUI.Controls.DarkButton();
            pnGlobalControl = new Panel();
            saturationColorSlider = new Cyotek.Windows.Forms.SaturationColorSlider();
            hueColorSlider = new Cyotek.Windows.Forms.HueColorSlider();
            lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            tglGlobalControl = new MetroSetSwitch();
            pnSliders = new Panel();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            picPreview = new PictureBox();
            colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            lstColor = new DoubleBufferedListView();
            tbpTextures = new TabPage();
            fraSwitches = new AltUI.Controls.DarkGroupBox();
            lblSimpleMode = new Label();
            tglSimpleMode = new MetroSetSwitch();
            lblExpand = new Label();
            tglExpand = new MetroSetSwitch();
            fraReplace = new AltUI.Controls.DarkGroupBox();
            numReplace = new AltUI.Controls.DarkNumericUpDown();
            cmdReplace = new AltUI.Controls.DarkButton();
            pictureBox1 = new PictureBox();
            fraTexture = new AltUI.Controls.DarkGroupBox();
            lstPages = new DoubleBufferedListView();
            grdTextures = new DataGridView();
            tabModel.SuspendLayout();
            tbpColors.SuspendLayout();
            fraGlobalControl.SuspendLayout();
            pnGlobalControl.SuspendLayout();
            pnSliders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            tbpTextures.SuspendLayout();
            fraSwitches.SuspendLayout();
            fraReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numReplace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            fraTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdTextures).BeginInit();
            SuspendLayout();
            // 
            // tabModel
            // 
            tabModel.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabModel.AnimateTime = 200;
            tabModel.BackgroundColor = Color.FromArgb(30, 30, 30);
            tabModel.Controls.Add(tbpInfo);
            tabModel.Controls.Add(tbpColors);
            tabModel.Controls.Add(tbpTextures);
            tabModel.Dock = DockStyle.Fill;
            tabModel.IsDerivedStyle = true;
            tabModel.ItemSize = new Size(100, 28);
            tabModel.Location = new Point(0, 0);
            tabModel.Multiline = true;
            tabModel.Name = "tabModel";
            tabModel.SelectedIndex = 2;
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
            // tbpInfo
            // 
            tbpInfo.BackColor = Color.FromArgb(31, 31, 32);
            tbpInfo.Location = new Point(4, 32);
            tbpInfo.Name = "tbpInfo";
            tbpInfo.Size = new Size(1032, 764);
            tbpInfo.TabIndex = 0;
            tbpInfo.Text = "Info";
            // 
            // tbpColors
            // 
            tbpColors.BackColor = Color.FromArgb(31, 31, 32);
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
            // fraGlobalControl
            // 
            fraGlobalControl.BackColor = Color.Transparent;
            fraGlobalControl.Controls.Add(cmdCancel);
            fraGlobalControl.Controls.Add(cmdApply);
            fraGlobalControl.Controls.Add(pnGlobalControl);
            fraGlobalControl.Controls.Add(tglGlobalControl);
            fraGlobalControl.Location = new Point(3, 427);
            fraGlobalControl.Name = "fraGlobalControl";
            fraGlobalControl.Size = new Size(239, 167);
            fraGlobalControl.TabIndex = 7;
            fraGlobalControl.TabStop = false;
            fraGlobalControl.Text = "HLS Global Controller";
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
            pnGlobalControl.Controls.Add(saturationColorSlider);
            pnGlobalControl.Controls.Add(hueColorSlider);
            pnGlobalControl.Controls.Add(lightnessColorSlider);
            pnGlobalControl.Enabled = false;
            pnGlobalControl.Location = new Point(6, 50);
            pnGlobalControl.Name = "pnGlobalControl";
            pnGlobalControl.Size = new Size(227, 111);
            pnGlobalControl.TabIndex = 4;
            // 
            // saturationColorSlider
            // 
            saturationColorSlider.Location = new Point(3, 38);
            saturationColorSlider.Name = "saturationColorSlider";
            saturationColorSlider.Size = new Size(219, 29);
            saturationColorSlider.TabIndex = 0;
            saturationColorSlider.Value = 50F;
            saturationColorSlider.ValueChanged += saturationColorSlider_ValueChanged;
            // 
            // hueColorSlider
            // 
            hueColorSlider.Location = new Point(3, 3);
            hueColorSlider.Name = "hueColorSlider";
            hueColorSlider.Size = new Size(219, 29);
            hueColorSlider.TabIndex = 0;
            hueColorSlider.Value = 180F;
            hueColorSlider.ValueChanged += hueColorSlider_ValueChangedHandler;
            // 
            // lightnessColorSlider
            // 
            lightnessColorSlider.Color = Color.FromArgb(127, 127, 127);
            lightnessColorSlider.Location = new Point(3, 73);
            lightnessColorSlider.Name = "lightnessColorSlider";
            lightnessColorSlider.Size = new Size(219, 29);
            lightnessColorSlider.TabIndex = 0;
            lightnessColorSlider.Value = 50F;
            lightnessColorSlider.ValueChanged += lightnessColorSlider_ValueChanged;
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
            pnSliders.Controls.Add(colorEditor);
            pnSliders.Controls.Add(picPreview);
            pnSliders.Controls.Add(colorWheel);
            pnSliders.Enabled = false;
            pnSliders.Location = new Point(248, 3);
            pnSliders.Name = "pnSliders";
            pnSliders.Size = new Size(306, 591);
            pnSliders.TabIndex = 3;
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Location = new Point(4, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowColorSpaceLabels = false;
            colorEditor.Size = new Size(284, 202);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // picPreview
            // 
            picPreview.Location = new Point(188, 211);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(84, 50);
            picPreview.TabIndex = 2;
            picPreview.TabStop = false;
            // 
            // colorWheel
            // 
            colorWheel.Color = Color.FromArgb(255, 255, 255);
            colorWheel.Location = new Point(4, 195);
            colorWheel.Name = "colorWheel";
            colorWheel.Size = new Size(178, 200);
            colorWheel.TabIndex = 0;
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
            lstColor.Size = new Size(239, 418);
            lstColor.TabIndex = 0;
            lstColor.UseCompatibleStateImageBehavior = false;
            lstColor.DrawItem += lstColor_DrawItem;
            lstColor.DrawSubItem += lstColor_DrawSubItem;
            lstColor.SelectedIndexChanged += lstColor_SelectedIndexChanged;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Controls.Add(fraSwitches);
            tbpTextures.Controls.Add(fraReplace);
            tbpTextures.Controls.Add(pictureBox1);
            tbpTextures.Controls.Add(fraTexture);
            tbpTextures.Controls.Add(grdTextures);
            tbpTextures.Location = new Point(4, 32);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(1032, 764);
            tbpTextures.TabIndex = 2;
            tbpTextures.Text = "Textures";
            tbpTextures.Enter += tbpTextures_Enter;
            // 
            // fraSwitches
            // 
            fraSwitches.Controls.Add(lblSimpleMode);
            fraSwitches.Controls.Add(tglSimpleMode);
            fraSwitches.Controls.Add(lblExpand);
            fraSwitches.Controls.Add(tglExpand);
            fraSwitches.Enabled = false;
            fraSwitches.Location = new Point(740, 13);
            fraSwitches.Name = "fraSwitches";
            fraSwitches.Size = new Size(107, 106);
            fraSwitches.TabIndex = 7;
            fraSwitches.TabStop = false;
            // 
            // lblSimpleMode
            // 
            lblSimpleMode.AutoSize = true;
            lblSimpleMode.BackColor = Color.Transparent;
            lblSimpleMode.Location = new Point(6, 0);
            lblSimpleMode.Name = "lblSimpleMode";
            lblSimpleMode.Size = new Size(77, 15);
            lblSimpleMode.TabIndex = 3;
            lblSimpleMode.Text = "Simple mode";
            lblSimpleMode.Click += lblSimpleMode_Click;
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
            tglSimpleMode.Location = new Point(6, 22);
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
            // lblExpand
            // 
            lblExpand.AutoSize = true;
            lblExpand.BackColor = Color.Transparent;
            lblExpand.Location = new Point(6, 56);
            lblExpand.Name = "lblExpand";
            lblExpand.Size = new Size(46, 15);
            lblExpand.TabIndex = 3;
            lblExpand.Text = "Expand";
            // 
            // tglExpand
            // 
            tglExpand.BackColor = Color.Transparent;
            tglExpand.BackgroundColor = Color.Empty;
            tglExpand.BorderColor = Color.FromArgb(155, 155, 155);
            tglExpand.CheckColor = Color.FromArgb(65, 177, 225);
            tglExpand.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            tglExpand.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            tglExpand.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            tglExpand.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            tglExpand.IsDerivedStyle = true;
            tglExpand.Location = new Point(8, 78);
            tglExpand.Name = "tglExpand";
            tglExpand.Size = new Size(58, 22);
            tglExpand.Style = MetroSet_UI.Enums.Style.Dark;
            tglExpand.StyleManager = null;
            tglExpand.Switched = false;
            tglExpand.SymbolColor = Color.FromArgb(92, 92, 92);
            tglExpand.TabIndex = 2;
            tglExpand.Text = "Expand";
            tglExpand.ThemeAuthor = "Narwin";
            tglExpand.ThemeName = "MetroDark";
            tglExpand.UnCheckColor = Color.FromArgb(155, 155, 155);
            tglExpand.SwitchedChanged += tglExpand_SwitchedChanged;
            // 
            // fraReplace
            // 
            fraReplace.BackColor = Color.Transparent;
            fraReplace.Controls.Add(numReplace);
            fraReplace.Controls.Add(cmdReplace);
            fraReplace.Enabled = false;
            fraReplace.Location = new Point(740, 125);
            fraReplace.Name = "fraReplace";
            fraReplace.Size = new Size(107, 100);
            fraReplace.TabIndex = 6;
            fraReplace.TabStop = false;
            fraReplace.Text = "Replace Values";
            // 
            // numReplace
            // 
            numReplace.Location = new Point(6, 22);
            numReplace.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numReplace.Name = "numReplace";
            numReplace.Size = new Size(95, 23);
            numReplace.TabIndex = 6;
            // 
            // cmdReplace
            // 
            cmdReplace.BorderColour = Color.Empty;
            cmdReplace.CustomColour = false;
            cmdReplace.FlatBottom = false;
            cmdReplace.FlatTop = false;
            cmdReplace.Location = new Point(6, 51);
            cmdReplace.Name = "cmdReplace";
            cmdReplace.Padding = new Padding(5);
            cmdReplace.Size = new Size(75, 23);
            cmdReplace.TabIndex = 5;
            cmdReplace.Text = "BOOM";
            cmdReplace.Click += cmdReplace_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 279);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1024, 128);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // fraTexture
            // 
            fraTexture.Controls.Add(lstPages);
            fraTexture.Location = new Point(3, 3);
            fraTexture.Name = "fraTexture";
            fraTexture.Size = new Size(149, 253);
            fraTexture.TabIndex = 0;
            fraTexture.TabStop = false;
            fraTexture.Text = "Texture Pages";
            // 
            // lstPages
            // 
            lstPages.BorderStyle = BorderStyle.FixedSingle;
            lstPages.FullRowSelect = true;
            lstPages.Location = new Point(6, 22);
            lstPages.Name = "lstPages";
            lstPages.Size = new Size(120, 200);
            lstPages.TabIndex = 0;
            lstPages.UseCompatibleStateImageBehavior = false;
            lstPages.View = View.Details;
            lstPages.ColumnWidthChanging += lstPages_ColumnWidthChangingHandler;
            // 
            // grdTextures
            // 
            grdTextures.AllowUserToAddRows = false;
            grdTextures.AllowUserToResizeColumns = false;
            grdTextures.AllowUserToResizeRows = false;
            grdTextures.Location = new Point(158, 13);
            grdTextures.Name = "grdTextures";
            grdTextures.Size = new Size(576, 243);
            grdTextures.TabIndex = 0;
            grdTextures.CellEndEdit += grdTextures_CellEndEdit;
            grdTextures.CellValidating += grdTextures_CellValidating;
            grdTextures.CellValueChanged += grdTextures_CellValueChanged;
            grdTextures.SelectionChanged += grdTextures_SelectionChanged;
            // 
            // ModelBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabModel);
            Name = "ModelBox";
            Size = new Size(1040, 800);
            tabModel.ResumeLayout(false);
            tbpColors.ResumeLayout(false);
            fraGlobalControl.ResumeLayout(false);
            pnGlobalControl.ResumeLayout(false);
            pnSliders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            tbpTextures.ResumeLayout(false);
            fraSwitches.ResumeLayout(false);
            fraSwitches.PerformLayout();
            fraReplace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numReplace).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            fraTexture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grdTextures).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetTabControl tabModel;
        private TabPage tbpInfo;
        private TabPage tbpColors;
        private TabPage tbpTextures;
        private AltUI.Controls.DarkGroupBox fraTexture;
        private DoubleBufferedListView lstPages;
        private DoubleBufferedListView lstColor;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel;
        private MetroSetSwitch tglGlobalControl;
        private Cyotek.Windows.Forms.HueColorSlider hueColorSlider;
        private Cyotek.Windows.Forms.SaturationColorSlider saturationColorSlider;
        private Cyotek.Windows.Forms.LightnessColorSlider lightnessColorSlider;
        private PictureBox picPreview;
        private Panel pnSliders;
        private Panel pnGlobalControl;
        private AltUI.Controls.DarkButton cmdApply;
        private AltUI.Controls.DarkGroupBox fraGlobalControl;
        private AltUI.Controls.DarkButton cmdCancel;
        private DataGridView grdTextures;
        private PictureBox pictureBox1;
        private Label lblSimpleMode;
        private MetroSetSwitch tglSimpleMode;
        private Label lblExpand;
        private MetroSetSwitch tglExpand;
        private AltUI.Controls.DarkButton cmdReplace;
        private AltUI.Controls.DarkGroupBox fraReplace;
        private AltUI.Controls.DarkNumericUpDown numReplace;
        private AltUI.Controls.DarkGroupBox fraSwitches;
    }
}
