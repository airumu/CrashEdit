using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class GOOLFrameGroupBox
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblTPageError = new Label();
            dgvTexture = new DataGridView();
            trkPictureSize = new MetroSet_UI.Controls.MetroSetTrackBar();
            dgvFrameGroup = new DataGridView();
            dpdTPages = new DarkComboBox();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTexture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFrameGroup).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(lblTPageError);
            panel1.Controls.Add(dgvTexture);
            panel1.Controls.Add(trkPictureSize);
            panel1.Controls.Add(dgvFrameGroup);
            panel1.Controls.Add(dpdTPages);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 440);
            panel1.TabIndex = 0;
            // 
            // lblTPageError
            // 
            lblTPageError.AutoSize = true;
            lblTPageError.ForeColor = Color.Red;
            lblTPageError.Location = new Point(171, 415);
            lblTPageError.Name = "lblTPageError";
            lblTPageError.Size = new Size(153, 15);
            lblTPageError.TabIndex = 3;
            lblTPageError.Text = "Texture page does not exist!";
            lblTPageError.Visible = false;
            // 
            // dgvTexture
            // 
            dgvTexture.AllowUserToAddRows = false;
            dgvTexture.AllowUserToResizeColumns = false;
            dgvTexture.AllowUserToResizeRows = false;
            dgvTexture.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTexture.ColumnHeadersHeight = 24;
            dgvTexture.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTexture.Location = new Point(330, 3);
            dgvTexture.Name = "dgvTexture";
            dgvTexture.RowHeadersWidth = 24;
            dgvTexture.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTexture.Size = new Size(443, 403);
            dgvTexture.TabIndex = 0;
            dgvTexture.Visible = false;
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
            trkPictureSize.Location = new Point(3, 414);
            trkPictureSize.Maximum = 100;
            trkPictureSize.Minimum = 80;
            trkPictureSize.Name = "trkPictureSize";
            trkPictureSize.Size = new Size(75, 16);
            trkPictureSize.Style = MetroSet_UI.Enums.Style.Dark;
            trkPictureSize.StyleManager = null;
            trkPictureSize.TabIndex = 1;
            trkPictureSize.Text = "metroSetTrackBar1";
            trkPictureSize.ThemeAuthor = "Narwin";
            trkPictureSize.ThemeName = "MetroDark";
            trkPictureSize.TickFrequency = 2;
            trkPictureSize.Value = 100;
            trkPictureSize.ValueColor = Color.FromArgb(65, 177, 225);
            trkPictureSize.ValueChanged += trkPictureSize_ValueChanged;
            // 
            // dgvFrameGroup
            // 
            dgvFrameGroup.AllowUserToAddRows = false;
            dgvFrameGroup.AllowUserToResizeColumns = false;
            dgvFrameGroup.AllowUserToResizeRows = false;
            dgvFrameGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvFrameGroup.ColumnHeadersHeight = 24;
            dgvFrameGroup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFrameGroup.Location = new Point(3, 3);
            dgvFrameGroup.Name = "dgvFrameGroup";
            dgvFrameGroup.RowHeadersWidth = 24;
            dgvFrameGroup.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvFrameGroup.Size = new Size(314, 403);
            dgvFrameGroup.TabIndex = 0;
            // 
            // dpdTPages
            // 
            dpdTPages.DrawMode = DrawMode.OwnerDrawVariable;
            dpdTPages.FormattingEnabled = true;
            dpdTPages.Location = new Point(84, 412);
            dpdTPages.Name = "dpdTPages";
            dpdTPages.Size = new Size(81, 24);
            dpdTPages.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 440);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 150);
            panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1024, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // GOOLFrameGroupBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "GOOLFrameGroupBox";
            Size = new Size(800, 800);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTexture).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFrameGroup).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvFrameGroup;
        private Panel panel2;
        private DataGridView dgvTexture;
        private PictureBox pictureBox1;
        private MetroSet_UI.Controls.MetroSetTrackBar trkPictureSize;
        private DarkComboBox dpdTPages;
        private Label lblTPageError;
    }
}
