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
            lstColor = new ListView();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            tbpTextures = new TabPage();
            tbcModel.SuspendLayout();
            tbpInfo.SuspendLayout();
            fraTexture.SuspendLayout();
            tbpColors.SuspendLayout();
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
            tbcModel.Size = new Size(529, 573);
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
            tbpInfo.Size = new Size(521, 517);
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
            tbpColors.Controls.Add(lstColor);
            tbpColors.Controls.Add(colorEditor);
            tbpColors.Controls.Add(colorWheel);
            tbpColors.Location = new Point(4, 52);
            tbpColors.Name = "tbpColors";
            tbpColors.Size = new Size(521, 517);
            tbpColors.TabIndex = 1;
            tbpColors.Text = "Colors";
            // 
            // lstColor
            // 
            lstColor.BackColor = Color.FromArgb(26, 26, 28);
            lstColor.BorderStyle = BorderStyle.FixedSingle;
            lstColor.ForeColor = Color.FromArgb(213, 213, 213);
            lstColor.Location = new Point(3, 3);
            lstColor.Name = "lstColor";
            lstColor.Size = new Size(212, 212);
            lstColor.TabIndex = 0;
            lstColor.SelectedIndexChanged += lstColor_SelectedIndexChanged;
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Location = new Point(222, 3);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.Size = new Size(228, 327);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // colorWheel
            // 
            colorWheel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            colorWheel.Color = Color.FromArgb(65, 105, 225);
            colorWheel.Location = new Point(80, 221);
            colorWheel.Name = "colorWheel";
            colorWheel.Size = new Size(135, 139);
            colorWheel.TabIndex = 0;
            colorWheel.ColorChanged += colorWheel_ColorChanged;
            // 
            // tbpTextures
            // 
            tbpTextures.BackColor = Color.FromArgb(31, 31, 32);
            tbpTextures.Location = new Point(4, 52);
            tbpTextures.Name = "tbpTextures";
            tbpTextures.Size = new Size(521, 517);
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
            Size = new Size(529, 573);
            tbcModel.ResumeLayout(false);
            tbpInfo.ResumeLayout(false);
            fraTexture.ResumeLayout(false);
            tbpColors.ResumeLayout(false);
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
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel;
    }
}
