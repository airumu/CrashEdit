using AltUI.ColorPicker;
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
            tbcModel = new MetroSetTabControl();
            tbpInfo = new TabPage();
            fraTexture = new AltUI.Controls.DarkGroupBox();
            listView1 = new ListView();
            tbpColors = new TabPage();
            cmdApply = new AltUI.Controls.DarkButton();
            pnEditMode = new Panel();
            saturationColorSlider = new Cyotek.Windows.Forms.SaturationColorSlider();
            hueColorSlider = new Cyotek.Windows.Forms.HueColorSlider();
            lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            pnSliders = new Panel();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            picPreview = new PictureBox();
            colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            lstColor = new ListView();
            lstCopy = new ListView();
            swtEditEntire = new MetroSetSwitch();
            tbpTextures = new TabPage();
            tbcModel.SuspendLayout();
            tbpInfo.SuspendLayout();
            fraTexture.SuspendLayout();
            tbpColors.SuspendLayout();
            pnEditMode.SuspendLayout();
            pnSliders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            SuspendLayout();
            // 
            // tbcModel
            // 
            tbcModel.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tbcModel.AnimateTime = 200;
            tbcModel.BackgroundColor = Color.FromArgb(30, 30, 30);
            tbcModel.Controls.Add(tbpInfo);
            tbcModel.Controls.Add(tbpColors);
            tbcModel.Controls.Add(tbpTextures);
            tbcModel.Dock = DockStyle.Fill;
            tbcModel.IsDerivedStyle = true;
            tbcModel.ItemSize = new Size(100, 48);
            tbcModel.Location = new Point(0, 0);
            tbcModel.Multiline = true;
            tbcModel.Name = "tbcModel";
            tbcModel.SelectedIndex = 1;
            tbcModel.SelectedTextColor = Color.White;
            tbcModel.Size = new Size(800, 800);
            tbcModel.SizeMode = TabSizeMode.Fixed;
            tbcModel.Speed = 100;
            tbcModel.Style = MetroSet_UI.Enums.Style.Dark;
            tbcModel.StyleManager = null;
            tbcModel.TabIndex = 0;
            tbcModel.ThemeAuthor = "Narwin";
            tbcModel.ThemeName = "MetroDark";
            tbcModel.UnselectedTextColor = Color.Gray;
            tbcModel.UseAnimation = false;
            tbcModel.Enter += tbpColors_Enter;
            // 
            // tbpInfo
            // 
            tbpInfo.BackColor = Color.FromArgb(31, 31, 32);
            tbpInfo.Controls.Add(fraTexture);
            tbpInfo.Location = new Point(4, 52);
            tbpInfo.Name = "tbpInfo";
            tbpInfo.Size = new Size(792, 744);
            tbpInfo.TabIndex = 0;
            tbpInfo.Text = "Info";
            // 
            // fraTexture
            // 
            fraTexture.Controls.Add(listView1);
            fraTexture.Location = new Point(3, 3);
            fraTexture.Name = "fraTexture";
            fraTexture.Size = new Size(313, 226);
            fraTexture.TabIndex = 0;
            fraTexture.TabStop = false;
            fraTexture.Text = "TPages";
            // 
            // listView1
            // 
            listView1.FullRowSelect = true;
            listView1.Location = new Point(6, 22);
            listView1.Name = "listView1";
            listView1.Size = new Size(120, 200);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.Click += listView1_Click;
            // 
            // tbpColors
            // 
            tbpColors.BackColor = Color.FromArgb(31, 31, 32);
            tbpColors.Controls.Add(cmdApply);
            tbpColors.Controls.Add(pnEditMode);
            tbpColors.Controls.Add(pnSliders);
            tbpColors.Controls.Add(lstColor);
            tbpColors.Controls.Add(lstCopy);
            tbpColors.Controls.Add(swtEditEntire);
            tbpColors.Location = new Point(4, 52);
            tbpColors.Name = "tbpColors";
            tbpColors.Size = new Size(792, 744);
            tbpColors.TabIndex = 1;
            tbpColors.Text = "Colors";
            tbpColors.Click += tbpColors_Click;
            // 
            // cmdApply
            // 
            cmdApply.BorderColour = Color.Empty;
            cmdApply.CustomColour = false;
            cmdApply.Enabled = false;
            cmdApply.FlatBottom = false;
            cmdApply.FlatTop = false;
            cmdApply.Location = new Point(70, 426);
            cmdApply.Name = "cmdApply";
            cmdApply.Padding = new Padding(5);
            cmdApply.Size = new Size(75, 23);
            cmdApply.TabIndex = 5;
            cmdApply.Text = "Apply";
            cmdApply.Click += cmdApply_Click;
            // 
            // pnEditMode
            // 
            pnEditMode.Controls.Add(saturationColorSlider);
            pnEditMode.Controls.Add(hueColorSlider);
            pnEditMode.Controls.Add(lightnessColorSlider);
            pnEditMode.Enabled = false;
            pnEditMode.Location = new Point(3, 455);
            pnEditMode.Name = "pnEditMode";
            pnEditMode.Size = new Size(239, 254);
            pnEditMode.TabIndex = 4;
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
            // pnSliders
            // 
            pnSliders.Controls.Add(colorEditor);
            pnSliders.Controls.Add(picPreview);
            pnSliders.Controls.Add(colorWheel);
            pnSliders.Enabled = false;
            pnSliders.Location = new Point(248, 3);
            pnSliders.Name = "pnSliders";
            pnSliders.Size = new Size(306, 592);
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
            colorEditor.Size = new Size(232, 202);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // picPreview
            // 
            picPreview.Location = new Point(4, 211);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(100, 50);
            picPreview.TabIndex = 2;
            picPreview.TabStop = false;
            // 
            // colorWheel
            // 
            colorWheel.Color = Color.FromArgb(255, 255, 255);
            colorWheel.Location = new Point(58, 267);
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
            // lstCopy
            // 
            lstCopy.Location = new Point(3, 3);
            lstCopy.Name = "lstCopy";
            lstCopy.Size = new Size(121, 97);
            lstCopy.TabIndex = 6;
            lstCopy.UseCompatibleStateImageBehavior = false;
            lstCopy.Visible = false;
            // 
            // swtEditEntire
            // 
            swtEditEntire.BackColor = Color.Transparent;
            swtEditEntire.BackgroundColor = Color.Empty;
            swtEditEntire.BorderColor = Color.FromArgb(155, 155, 155);
            swtEditEntire.CheckColor = Color.FromArgb(65, 177, 225);
            swtEditEntire.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            swtEditEntire.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            swtEditEntire.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            swtEditEntire.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            swtEditEntire.IsDerivedStyle = true;
            swtEditEntire.Location = new Point(6, 427);
            swtEditEntire.Name = "swtEditEntire";
            swtEditEntire.Size = new Size(58, 22);
            swtEditEntire.Style = MetroSet_UI.Enums.Style.Dark;
            swtEditEntire.StyleManager = null;
            swtEditEntire.Switched = false;
            swtEditEntire.SymbolColor = Color.FromArgb(92, 92, 92);
            swtEditEntire.TabIndex = 1;
            swtEditEntire.Text = "metroSetSwitch1";
            swtEditEntire.ThemeAuthor = "Narwin";
            swtEditEntire.ThemeName = "MetroDark";
            swtEditEntire.UnCheckColor = Color.FromArgb(155, 155, 155);
            swtEditEntire.SwitchedChanged += swtEditEntire_SwitchedChanged;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Location = new Point(4, 52);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(792, 744);
            tbpTextures.TabIndex = 2;
            tbpTextures.Text = "Textures";
            tbpTextures.Enter += tbpTextures_Enter;
            // 
            // ModelBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbcModel);
            Name = "ModelBox";
            Size = new Size(800, 800);
            tbcModel.ResumeLayout(false);
            tbpInfo.ResumeLayout(false);
            fraTexture.ResumeLayout(false);
            tbpColors.ResumeLayout(false);
            pnEditMode.ResumeLayout(false);
            pnSliders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroSet_UI.Controls.MetroSetTabControl tbcModel;
        private TabPage tbpInfo;
        private TabPage tbpColors;
        private TabPage tbpTextures;
        private AltUI.Controls.DarkGroupBox fraTexture;
        private ListView listView1;
        private ListView lstColor;
        private ListView lstCopy;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel;
        private MetroSetSwitch swtEditEntire;
        private Cyotek.Windows.Forms.HueColorSlider hueColorSlider;
        private Cyotek.Windows.Forms.SaturationColorSlider saturationColorSlider;
        private Cyotek.Windows.Forms.LightnessColorSlider lightnessColorSlider;
        private PictureBox picPreview;
        private Panel pnSliders;
        private Panel pnEditMode;
        private AltUI.Controls.DarkButton cmdApply;
    }
}
