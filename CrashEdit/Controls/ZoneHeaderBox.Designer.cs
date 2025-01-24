using CrashEdit.Crash.GOOLIns;

namespace CrashEdit.CE
{
    partial class ZoneHeaderBox
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
            dgvZones = new DataGridView();
            fraZones = new AltUI.Controls.DarkGroupBox();
            fraWorlds = new AltUI.Controls.DarkGroupBox();
            dgvWorlds = new DataGridView();
            fraMusic = new AltUI.Controls.DarkGroupBox();
            txtMusic = new AltUI.Controls.DarkTextBox();
            lblEIDError = new Label();
            fraSpecialLoadList = new AltUI.Controls.DarkGroupBox();
            lblEIDErrorSP = new Label();
            cmdRemoveSP = new AltUI.Controls.DarkButton();
            txtSPLoadList = new AltUI.Controls.DarkTextBox();
            lbSPLoadList = new AltUI.Controls.DarkListBox();
            cmdAppendSP = new AltUI.Controls.DarkButton();
            fraZoneFlags = new AltUI.Controls.DarkGroupBox();
            txtZoneFlags = new AltUI.Controls.DarkTextBox();
            pnHeader = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvZones).BeginInit();
            fraZones.SuspendLayout();
            fraWorlds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorlds).BeginInit();
            fraMusic.SuspendLayout();
            fraSpecialLoadList.SuspendLayout();
            fraZoneFlags.SuspendLayout();
            pnHeader.SuspendLayout();
            SuspendLayout();
            // 
            // dgvZones
            // 
            dgvZones.AllowUserToAddRows = false;
            dgvZones.AllowUserToResizeColumns = false;
            dgvZones.AllowUserToResizeRows = false;
            dgvZones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvZones.ColumnHeadersHeight = 20;
            dgvZones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvZones.Location = new Point(6, 22);
            dgvZones.Name = "dgvZones";
            dgvZones.RowHeadersWidth = 24;
            dgvZones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvZones.ScrollBars = ScrollBars.Vertical;
            dgvZones.Size = new Size(204, 220);
            dgvZones.TabIndex = 0;
            dgvZones.CellBeginEdit += dgv_CellBeginEdit;
            dgvZones.CellFormatting += dgvZones_CellFormatting;
            dgvZones.CellParsing += dgvZones_CellParsing;
            dgvZones.CellValidating += dgvZones_CellValidating;
            dgvZones.CellValueChanged += dgvZones_CellValueChanged;
            // 
            // fraZones
            // 
            fraZones.Controls.Add(dgvZones);
            fraZones.Location = new Point(3, 3);
            fraZones.Name = "fraZones";
            fraZones.Size = new Size(216, 248);
            fraZones.TabIndex = 1;
            fraZones.TabStop = false;
            fraZones.Text = "Zones";
            // 
            // fraWorlds
            // 
            fraWorlds.Controls.Add(dgvWorlds);
            fraWorlds.Location = new Point(225, 3);
            fraWorlds.Name = "fraWorlds";
            fraWorlds.Size = new Size(156, 248);
            fraWorlds.TabIndex = 2;
            fraWorlds.TabStop = false;
            fraWorlds.Text = "Worlds";
            // 
            // dgvWorlds
            // 
            dgvWorlds.AllowUserToAddRows = false;
            dgvWorlds.AllowUserToResizeColumns = false;
            dgvWorlds.AllowUserToResizeRows = false;
            dgvWorlds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvWorlds.ColumnHeadersHeight = 20;
            dgvWorlds.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvWorlds.Location = new Point(6, 22);
            dgvWorlds.Name = "dgvWorlds";
            dgvWorlds.RowHeadersWidth = 24;
            dgvWorlds.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvWorlds.ScrollBars = ScrollBars.Vertical;
            dgvWorlds.Size = new Size(144, 220);
            dgvWorlds.TabIndex = 0;
            dgvWorlds.CellBeginEdit += dgv_CellBeginEdit;
            dgvWorlds.CellValueChanged += dgvWorlds_CellValueChanged;
            // 
            // fraMusic
            // 
            fraMusic.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraMusic.Controls.Add(txtMusic);
            fraMusic.Location = new Point(3, 257);
            fraMusic.Name = "fraMusic";
            fraMusic.Size = new Size(84, 52);
            fraMusic.TabIndex = 3;
            fraMusic.TabStop = false;
            fraMusic.Text = "Music";
            // 
            // txtMusic
            // 
            txtMusic.BackColor = Color.FromArgb(26, 26, 28);
            txtMusic.BorderStyle = BorderStyle.FixedSingle;
            txtMusic.ForeColor = Color.FromArgb(213, 213, 213);
            txtMusic.Location = new Point(6, 22);
            txtMusic.MaxLength = 5;
            txtMusic.Name = "txtMusic";
            txtMusic.Size = new Size(72, 23);
            txtMusic.TabIndex = 0;
            txtMusic.TextChanged += txtMusic_TextChanged;
            // 
            // lblEIDError
            // 
            lblEIDError.AutoSize = true;
            lblEIDError.ForeColor = Color.Red;
            lblEIDError.Location = new Point(9, 317);
            lblEIDError.Name = "lblEIDError";
            lblEIDError.Size = new Size(66, 15);
            lblEIDError.TabIndex = 1;
            lblEIDError.Text = "EID ERROR!";
            lblEIDError.Visible = false;
            // 
            // fraSpecialLoadList
            // 
            fraSpecialLoadList.Controls.Add(lblEIDErrorSP);
            fraSpecialLoadList.Controls.Add(cmdRemoveSP);
            fraSpecialLoadList.Controls.Add(txtSPLoadList);
            fraSpecialLoadList.Controls.Add(lbSPLoadList);
            fraSpecialLoadList.Controls.Add(cmdAppendSP);
            fraSpecialLoadList.Location = new Point(225, 257);
            fraSpecialLoadList.Name = "fraSpecialLoadList";
            fraSpecialLoadList.Size = new Size(156, 291);
            fraSpecialLoadList.TabIndex = 4;
            fraSpecialLoadList.TabStop = false;
            fraSpecialLoadList.Text = "Special Load List";
            // 
            // lblEIDErrorSP
            // 
            lblEIDErrorSP.AutoSize = true;
            lblEIDErrorSP.ForeColor = Color.Red;
            lblEIDErrorSP.Location = new Point(0, 19);
            lblEIDErrorSP.Name = "lblEIDErrorSP";
            lblEIDErrorSP.Size = new Size(66, 15);
            lblEIDErrorSP.TabIndex = 1;
            lblEIDErrorSP.Text = "EID ERROR!";
            lblEIDErrorSP.Visible = false;
            // 
            // cmdRemoveSP
            // 
            cmdRemoveSP.BorderColour = Color.Empty;
            cmdRemoveSP.CustomColour = false;
            cmdRemoveSP.FlatBottom = false;
            cmdRemoveSP.FlatTop = false;
            cmdRemoveSP.Location = new Point(82, 263);
            cmdRemoveSP.Name = "cmdRemoveSP";
            cmdRemoveSP.Padding = new Padding(5);
            cmdRemoveSP.Size = new Size(68, 23);
            cmdRemoveSP.TabIndex = 2;
            cmdRemoveSP.Text = "Remove";
            cmdRemoveSP.Click += cmdRemoveSP_Click;
            // 
            // txtSPLoadList
            // 
            txtSPLoadList.BackColor = Color.FromArgb(26, 26, 28);
            txtSPLoadList.BorderStyle = BorderStyle.FixedSingle;
            txtSPLoadList.ForeColor = Color.FromArgb(213, 213, 213);
            txtSPLoadList.Location = new Point(6, 37);
            txtSPLoadList.MaxLength = 5;
            txtSPLoadList.Name = "txtSPLoadList";
            txtSPLoadList.Size = new Size(144, 23);
            txtSPLoadList.TabIndex = 1;
            txtSPLoadList.TextChanged += txtSPLoadList_TextChanged;
            // 
            // lbSPLoadList
            // 
            lbSPLoadList.BackColor = Color.FromArgb(26, 26, 28);
            lbSPLoadList.BorderStyle = BorderStyle.FixedSingle;
            lbSPLoadList.ForeColor = Color.FromArgb(213, 213, 213);
            lbSPLoadList.FormattingEnabled = true;
            lbSPLoadList.Location = new Point(6, 60);
            lbSPLoadList.Name = "lbSPLoadList";
            lbSPLoadList.Size = new Size(144, 197);
            lbSPLoadList.TabIndex = 0;
            lbSPLoadList.SelectedIndexChanged += lbSPLoadList_SelectedIndexChanged;
            lbSPLoadList.KeyDown += lbSPLoadList_KeyDown;
            // 
            // cmdAppendSP
            // 
            cmdAppendSP.BorderColour = Color.Empty;
            cmdAppendSP.CustomColour = false;
            cmdAppendSP.FlatBottom = false;
            cmdAppendSP.FlatTop = false;
            cmdAppendSP.Location = new Point(6, 263);
            cmdAppendSP.Name = "cmdAppendSP";
            cmdAppendSP.Padding = new Padding(5);
            cmdAppendSP.Size = new Size(68, 23);
            cmdAppendSP.TabIndex = 2;
            cmdAppendSP.Text = "Append";
            cmdAppendSP.Click += cmdAppendSP_Click;
            // 
            // fraZoneFlags
            // 
            fraZoneFlags.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraZoneFlags.Controls.Add(txtZoneFlags);
            fraZoneFlags.Location = new Point(93, 257);
            fraZoneFlags.Name = "fraZoneFlags";
            fraZoneFlags.Size = new Size(84, 52);
            fraZoneFlags.TabIndex = 3;
            fraZoneFlags.TabStop = false;
            fraZoneFlags.Text = "Zone Flags";
            // 
            // txtZoneFlags
            // 
            txtZoneFlags.BackColor = Color.FromArgb(26, 26, 28);
            txtZoneFlags.BorderStyle = BorderStyle.FixedSingle;
            txtZoneFlags.CharacterCasing = CharacterCasing.Upper;
            txtZoneFlags.ForeColor = Color.FromArgb(213, 213, 213);
            txtZoneFlags.Location = new Point(6, 22);
            txtZoneFlags.MaxLength = 8;
            txtZoneFlags.Name = "txtZoneFlags";
            txtZoneFlags.Size = new Size(72, 23);
            txtZoneFlags.TabIndex = 0;
            txtZoneFlags.TextChanged += txtZoneFlags_TextChanged;
            txtZoneFlags.KeyPress += txtZoneFlags_KeyPress;
            // 
            // pnHeader
            // 
            pnHeader.BackColor = Color.Transparent;
            pnHeader.Controls.Add(fraZones);
            pnHeader.Controls.Add(lblEIDError);
            pnHeader.Controls.Add(fraWorlds);
            pnHeader.Controls.Add(fraSpecialLoadList);
            pnHeader.Controls.Add(fraMusic);
            pnHeader.Controls.Add(fraZoneFlags);
            pnHeader.Dock = DockStyle.Fill;
            pnHeader.Location = new Point(0, 0);
            pnHeader.Name = "pnHeader";
            pnHeader.Size = new Size(573, 652);
            pnHeader.TabIndex = 5;
            // 
            // ZoneHeaderBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(pnHeader);
            Name = "ZoneHeaderBox";
            Size = new Size(573, 652);
            Leave += ZoneHeaderBox_Leave;
            ((System.ComponentModel.ISupportInitialize)dgvZones).EndInit();
            fraZones.ResumeLayout(false);
            fraWorlds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvWorlds).EndInit();
            fraMusic.ResumeLayout(false);
            fraMusic.PerformLayout();
            fraSpecialLoadList.ResumeLayout(false);
            fraSpecialLoadList.PerformLayout();
            fraZoneFlags.ResumeLayout(false);
            fraZoneFlags.PerformLayout();
            pnHeader.ResumeLayout(false);
            pnHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvZones;
        private AltUI.Controls.DarkGroupBox fraZones;
        private AltUI.Controls.DarkGroupBox fraWorlds;
        private DataGridView dgvWorlds;
        private AltUI.Controls.DarkGroupBox fraMusic;
        private AltUI.Controls.DarkTextBox txtMusic;
        private Label lblEIDError;
        private AltUI.Controls.DarkGroupBox fraSpecialLoadList;
        private AltUI.Controls.DarkButton cmdRemoveSP;
        private AltUI.Controls.DarkTextBox txtSPLoadList;
        private AltUI.Controls.DarkListBox lbSPLoadList;
        private AltUI.Controls.DarkButton cmdAppendSP;
        private Label lblEIDErrorSP;
        private AltUI.Controls.DarkGroupBox fraZoneFlags;
        private AltUI.Controls.DarkTextBox txtZoneFlags;
        private Panel pnHeader;
    }
}
