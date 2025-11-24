using System.Windows.Forms;
using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class T21EntryBox
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
            lblInfo = new DarkLabel();
            picImage = new PictureBox();
            cmbPalette = new DarkComboBox();
            cmbImage = new DarkComboBox();
            lblPalette = new DarkLabel();
            lblImage = new DarkLabel();
            panel1 = new Panel();
            chkSync = new DarkCheckBox();
            cmdReplaceImage = new DarkButton();
            cmdReplacePalette = new DarkButton();
            lblUnknown = new Label();
            fraPicture = new DarkGroupBox();
            panel2 = new Panel();
            numW = new DarkNumericUpDown();
            numH = new DarkNumericUpDown();
            lblX = new DarkLabel();
            fraInfo = new DarkGroupBox();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            panel1.SuspendLayout();
            fraPicture.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numH).BeginInit();
            fraInfo.SuspendLayout();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(6, 19);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(139, 30);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Palettes: {Palettes.Count}\r\nImages: {Images.Count}";
            // 
            // picImage
            // 
            picImage.BackColor = Color.Transparent;
            picImage.Cursor = Cursors.Hand;
            picImage.Location = new Point(6, 112);
            picImage.Name = "picImage";
            picImage.Size = new Size(100, 100);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 1;
            picImage.TabStop = false;
            // 
            // cmbPalette
            // 
            cmbPalette.DrawMode = DrawMode.OwnerDrawVariable;
            cmbPalette.FormattingEnabled = true;
            cmbPalette.Location = new Point(3, 21);
            cmbPalette.Name = "cmbPalette";
            cmbPalette.Size = new Size(75, 24);
            cmbPalette.TabIndex = 2;
            cmbPalette.SelectedIndexChanged += cmbPalette_SelectedIndexChanged;
            // 
            // cmbImage
            // 
            cmbImage.DrawMode = DrawMode.OwnerDrawVariable;
            cmbImage.FormattingEnabled = true;
            cmbImage.Location = new Point(141, 21);
            cmbImage.Name = "cmbImage";
            cmbImage.Size = new Size(75, 24);
            cmbImage.TabIndex = 2;
            cmbImage.SelectedIndexChanged += cmbImage_SelectedIndexChanged;
            // 
            // lblPalette
            // 
            lblPalette.AutoSize = true;
            lblPalette.BackColor = Color.Transparent;
            lblPalette.Location = new Point(3, 3);
            lblPalette.Name = "lblPalette";
            lblPalette.Size = new Size(70, 15);
            lblPalette.TabIndex = 0;
            lblPalette.Text = "Palette Data";
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.BackColor = Color.Transparent;
            lblImage.Location = new Point(141, 3);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(67, 15);
            lblImage.TabIndex = 0;
            lblImage.Text = "Image Data";
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(chkSync);
            panel1.Controls.Add(cmdReplaceImage);
            panel1.Controls.Add(cmdReplacePalette);
            panel1.Controls.Add(lblPalette);
            panel1.Controls.Add(cmbImage);
            panel1.Controls.Add(cmbPalette);
            panel1.Controls.Add(lblImage);
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(219, 77);
            panel1.TabIndex = 3;
            // 
            // chkSync
            // 
            chkSync.AutoSize = true;
            chkSync.Checked = true;
            chkSync.CheckState = CheckState.Checked;
            chkSync.Location = new Point(84, 23);
            chkSync.Name = "chkSync";
            chkSync.Offset = 1;
            chkSync.Size = new Size(51, 19);
            chkSync.TabIndex = 6;
            chkSync.Text = "Sync";
            // 
            // cmdReplaceImage
            // 
            cmdReplaceImage.BorderColour = Color.Empty;
            cmdReplaceImage.CustomColour = false;
            cmdReplaceImage.FlatBottom = false;
            cmdReplaceImage.FlatTop = false;
            cmdReplaceImage.Location = new Point(141, 51);
            cmdReplaceImage.Name = "cmdReplaceImage";
            cmdReplaceImage.Padding = new Padding(5);
            cmdReplaceImage.Size = new Size(75, 23);
            cmdReplaceImage.TabIndex = 3;
            cmdReplaceImage.Text = "Replace";
            cmdReplaceImage.Click += cmdReplaceImage_Click;
            // 
            // cmdReplacePalette
            // 
            cmdReplacePalette.BorderColour = Color.Empty;
            cmdReplacePalette.CustomColour = false;
            cmdReplacePalette.FlatBottom = false;
            cmdReplacePalette.FlatTop = false;
            cmdReplacePalette.Location = new Point(3, 51);
            cmdReplacePalette.Name = "cmdReplacePalette";
            cmdReplacePalette.Padding = new Padding(5);
            cmdReplacePalette.Size = new Size(75, 23);
            cmdReplacePalette.TabIndex = 3;
            cmdReplacePalette.Text = "Replace";
            cmdReplacePalette.Click += cmdReplacePalette_Click;
            // 
            // lblUnknown
            // 
            lblUnknown.AutoSize = true;
            lblUnknown.ForeColor = Color.Red;
            lblUnknown.Location = new Point(115, 128);
            lblUnknown.Name = "lblUnknown";
            lblUnknown.Size = new Size(111, 15);
            lblUnknown.TabIndex = 7;
            lblUnknown.Text = "Unknown structure!";
            lblUnknown.Visible = false;
            // 
            // fraPicture
            // 
            fraPicture.AutoSize = true;
            fraPicture.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPicture.BackColor = Color.Transparent;
            fraPicture.Controls.Add(lblUnknown);
            fraPicture.Controls.Add(panel2);
            fraPicture.Controls.Add(panel1);
            fraPicture.Controls.Add(picImage);
            fraPicture.Location = new Point(3, 3);
            fraPicture.Name = "fraPicture";
            fraPicture.Size = new Size(256, 234);
            fraPicture.TabIndex = 4;
            fraPicture.TabStop = false;
            fraPicture.Text = "Image";
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.Controls.Add(numW);
            panel2.Controls.Add(numH);
            panel2.Controls.Add(lblX);
            panel2.Location = new Point(112, 146);
            panel2.Name = "panel2";
            panel2.Size = new Size(138, 29);
            panel2.TabIndex = 5;
            // 
            // numW
            // 
            numW.Location = new Point(3, 3);
            numW.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            numW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numW.Name = "numW";
            numW.Size = new Size(53, 23);
            numW.TabIndex = 4;
            numW.Value = new decimal(new int[] { 64, 0, 0, 0 });
            numW.ValueChanged += numW_ValueChanged;
            // 
            // numH
            // 
            numH.Location = new Point(82, 3);
            numH.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            numH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numH.Name = "numH";
            numH.Size = new Size(53, 23);
            numH.TabIndex = 4;
            numH.Value = new decimal(new int[] { 48, 0, 0, 0 });
            numH.ValueChanged += numH_ValueChanged;
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.Location = new Point(62, 7);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 0;
            lblX.Text = "X";
            // 
            // fraInfo
            // 
            fraInfo.AutoSize = true;
            fraInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraInfo.BackColor = Color.Transparent;
            fraInfo.Controls.Add(lblInfo);
            fraInfo.Location = new Point(3, 243);
            fraInfo.Name = "fraInfo";
            fraInfo.Size = new Size(151, 68);
            fraInfo.TabIndex = 5;
            fraInfo.TabStop = false;
            fraInfo.Text = "Info";
            // 
            // T21EntryBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(fraInfo);
            Controls.Add(fraPicture);
            Name = "T21EntryBox";
            Size = new Size(400, 400);
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            fraPicture.ResumeLayout(false);
            fraPicture.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numW).EndInit();
            ((System.ComponentModel.ISupportInitialize)numH).EndInit();
            fraInfo.ResumeLayout(false);
            fraInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkLabel lblInfo;
        private PictureBox picImage;
        private DarkComboBox cmbPalette;
        private DarkComboBox cmbImage;
        private DarkLabel lblPalette;
        private DarkLabel lblImage;
        private Panel panel1;
        private DarkGroupBox fraPicture;
        private DarkGroupBox fraInfo;
        private DarkButton cmdReplaceImage;
        private DarkButton cmdReplacePalette;
        private DarkCheckBox chkSync;
        private DarkNumericUpDown numH;
        private DarkNumericUpDown numW;
        private DarkLabel lblX;
        private Panel panel2;
        private Label lblUnknown;
    }
}
