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
            pnTextureControls = new Panel();
            chkMaxValueFlag = new CheckBox();
            lblSimpleMode = new Label();
            tglSimpleMode = new MetroSet_UI.Controls.MetroSetSwitch();
            lblEIDError = new Label();
            trkPictureSize = new MetroSet_UI.Controls.MetroSetTrackBar();
            dpdTPages = new DarkComboBox();
            dgvTexture = new DataGridView();
            dgvFrameGroup = new DataGridView();
            pnPicture = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            pnTextureControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTexture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFrameGroup).BeginInit();
            pnPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(pnTextureControls);
            panel1.Controls.Add(dgvTexture);
            panel1.Controls.Add(dgvFrameGroup);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 440);
            panel1.TabIndex = 0;
            // 
            // pnTextureControls
            // 
            pnTextureControls.Controls.Add(chkMaxValueFlag);
            pnTextureControls.Controls.Add(lblSimpleMode);
            pnTextureControls.Controls.Add(tglSimpleMode);
            pnTextureControls.Controls.Add(lblEIDError);
            pnTextureControls.Controls.Add(trkPictureSize);
            pnTextureControls.Controls.Add(dpdTPages);
            pnTextureControls.Location = new Point(3, 408);
            pnTextureControls.Name = "pnTextureControls";
            pnTextureControls.Size = new Size(794, 32);
            pnTextureControls.TabIndex = 6;
            pnTextureControls.Visible = false;
            // 
            // chkMaxValueFlag
            // 
            chkMaxValueFlag.AutoSize = true;
            chkMaxValueFlag.Enabled = false;
            chkMaxValueFlag.Location = new Point(458, 5);
            chkMaxValueFlag.Name = "chkMaxValueFlag";
            chkMaxValueFlag.Size = new Size(83, 19);
            chkMaxValueFlag.TabIndex = 6;
            chkMaxValueFlag.Text = "RegionEnd";
            chkMaxValueFlag.UseVisualStyleBackColor = true;
            chkMaxValueFlag.Click += chkMaxValueFlag_Click;
            // 
            // lblSimpleMode
            // 
            lblSimpleMode.AutoSize = true;
            lblSimpleMode.Location = new Point(393, 6);
            lblSimpleMode.Name = "lblSimpleMode";
            lblSimpleMode.Size = new Size(59, 15);
            lblSimpleMode.TabIndex = 5;
            lblSimpleMode.Text = "Show UVs";
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
            tglSimpleMode.Location = new Point(329, 3);
            tglSimpleMode.Name = "tglSimpleMode";
            tglSimpleMode.Size = new Size(58, 22);
            tglSimpleMode.Style = MetroSet_UI.Enums.Style.Dark;
            tglSimpleMode.StyleManager = null;
            tglSimpleMode.Switched = false;
            tglSimpleMode.SymbolColor = Color.FromArgb(92, 92, 92);
            tglSimpleMode.TabIndex = 4;
            tglSimpleMode.Text = "metroSetSwitch1";
            tglSimpleMode.ThemeAuthor = "Narwin";
            tglSimpleMode.ThemeName = "MetroDark";
            tglSimpleMode.UnCheckColor = Color.FromArgb(155, 155, 155);
            tglSimpleMode.SwitchedChanged += tglSimpleMode_SwitchedChanged;
            // 
            // lblEIDError
            // 
            lblEIDError.AutoSize = true;
            lblEIDError.ForeColor = Color.Red;
            lblEIDError.Location = new Point(170, 6);
            lblEIDError.Name = "lblEIDError";
            lblEIDError.Size = new Size(153, 15);
            lblEIDError.TabIndex = 3;
            lblEIDError.Text = "Texture page does not exist!";
            lblEIDError.Visible = false;
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
            trkPictureSize.Location = new Point(3, 5);
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
            // dpdTPages
            // 
            dpdTPages.DrawMode = DrawMode.OwnerDrawVariable;
            dpdTPages.FormattingEnabled = true;
            dpdTPages.Location = new Point(84, 3);
            dpdTPages.Name = "dpdTPages";
            dpdTPages.Size = new Size(81, 24);
            dpdTPages.TabIndex = 2;
            dpdTPages.SelectedIndexChanged += dpdTPages_SelectedIndexChanged;
            // 
            // dgvTexture
            // 
            dgvTexture.AllowUserToAddRows = false;
            dgvTexture.AllowUserToResizeColumns = false;
            dgvTexture.AllowUserToResizeRows = false;
            dgvTexture.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTexture.ColumnHeadersHeight = 24;
            dgvTexture.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTexture.Location = new Point(341, 3);
            dgvTexture.Name = "dgvTexture";
            dgvTexture.RowHeadersWidth = 24;
            dgvTexture.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTexture.ShowCellToolTips = false;
            dgvTexture.Size = new Size(446, 400);
            dgvTexture.TabIndex = 0;
            dgvTexture.Visible = false;
            dgvTexture.CellBeginEdit += dgvTexture_CellBeginEdit;
            dgvTexture.CellValidating += dgvTexture_CellValidating;
            dgvTexture.CellValueChanged += dgvTexture_CellValueChanged;
            dgvTexture.EditingControlShowing += dgvTexture_EditingControlShowing;
            dgvTexture.SelectionChanged += dgvTexture_SelectionChanged;
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
            dgvFrameGroup.MultiSelect = false;
            dgvFrameGroup.Name = "dgvFrameGroup";
            dgvFrameGroup.RowHeadersWidth = 24;
            dgvFrameGroup.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvFrameGroup.ShowCellToolTips = false;
            dgvFrameGroup.Size = new Size(338, 400);
            dgvFrameGroup.TabIndex = 0;
            dgvFrameGroup.CellBeginEdit += dgvFrameGroup_CellBeginEdit;
            dgvFrameGroup.CellValidating += dgvFrameGroup_CellValidating;
            dgvFrameGroup.CellValueChanged += dgvFrameGroup_CellValueChanged;
            dgvFrameGroup.EditingControlShowing += dgvFrameGroup_EditingControlShowing;
            dgvFrameGroup.SelectionChanged += dgvFrameGroup_SelectionChanged;
            // 
            // pnPicture
            // 
            pnPicture.AutoScroll = true;
            pnPicture.BackColor = Color.Transparent;
            pnPicture.Controls.Add(pictureBox1);
            pnPicture.Dock = DockStyle.Top;
            pnPicture.Location = new Point(0, 440);
            pnPicture.Name = "pnPicture";
            pnPicture.Size = new Size(800, 150);
            pnPicture.TabIndex = 1;
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
            AutoScroll = true;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(pnPicture);
            Controls.Add(panel1);
            Name = "GOOLFrameGroupBox";
            Size = new Size(800, 800);
            panel1.ResumeLayout(false);
            pnTextureControls.ResumeLayout(false);
            pnTextureControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTexture).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFrameGroup).EndInit();
            pnPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvFrameGroup;
        private Panel pnPicture;
        private DataGridView dgvTexture;
        private PictureBox pictureBox1;
        private MetroSet_UI.Controls.MetroSetTrackBar trkPictureSize;
        private DarkComboBox dpdTPages;
        private Label lblEIDError;
        private MetroSet_UI.Controls.MetroSetSwitch tglSimpleMode;
        private Label lblSimpleMode;
        private Panel pnTextureControls;
        private CheckBox chkMaxValueFlag;
    }
}
