using AltUI.Controls;
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
            cmdRemoveZone = new AltUI.Controls.DarkButton();
            cmdAppendZone = new AltUI.Controls.DarkButton();
            fraWorlds = new AltUI.Controls.DarkGroupBox();
            dgvWorlds = new DataGridView();
            cmdRemoveWorld = new AltUI.Controls.DarkButton();
            cmdAppendWorld = new AltUI.Controls.DarkButton();
            fraMusic = new AltUI.Controls.DarkGroupBox();
            txtMusic = new AltUI.Controls.DarkTextBox();
            lblEIDError = new Label();
            fraSpecialLoadList = new AltUI.Controls.DarkGroupBox();
            pictureBox1 = new PictureBox();
            lblEIDErrorSP = new Label();
            cmdRemoveSP = new AltUI.Controls.DarkButton();
            txtSPLoadList = new AltUI.Controls.DarkTextBox();
            lbSPLoadList = new AltUI.Controls.DarkListBox();
            cmdAppendSP = new AltUI.Controls.DarkButton();
            fraZoneFlags = new AltUI.Controls.DarkGroupBox();
            txtZoneFlags = new AltUI.Controls.DarkTextBox();
            fraDrawGenFlag = new AltUI.Controls.DarkGroupBox();
            chkDrawGenFlag = new AltUI.Controls.DarkCheckBox();
            fraTransLoadOverride = new DarkGroupBox();
            chkTransLoadOverride = new DarkCheckBox();
            cmbTransLoadOverride = new DarkComboBox();
            pnHeader = new Panel();
            pnMisc = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvZones).BeginInit();
            fraZones.SuspendLayout();
            fraWorlds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWorlds).BeginInit();
            fraMusic.SuspendLayout();
            fraSpecialLoadList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            fraZoneFlags.SuspendLayout();
            pnHeader.SuspendLayout();
            pnMisc.SuspendLayout();
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
            fraZones.Controls.Add(cmdRemoveZone);
            fraZones.Controls.Add(cmdAppendZone);
            fraZones.Location = new Point(3, 3);
            fraZones.Name = "fraZones";
            fraZones.Size = new Size(216, 284);
            fraZones.TabIndex = 1;
            fraZones.TabStop = false;
            fraZones.Text = "Zones";
            // 
            // cmdRemoveZone
            // 
            cmdRemoveZone.BorderColour = Color.Empty;
            cmdRemoveZone.CustomColour = false;
            cmdRemoveZone.FlatBottom = false;
            cmdRemoveZone.FlatTop = false;
            cmdRemoveZone.Location = new Point(92, 248);
            cmdRemoveZone.Name = "cmdRemoveZone";
            cmdRemoveZone.Padding = new Padding(1, 5, 1, 5);
            cmdRemoveZone.Size = new Size(80, 28);
            cmdRemoveZone.TabIndex = 2;
            cmdRemoveZone.Text = "Remove Last";
            cmdRemoveZone.Click += cmdRemoveZone_Click;
            // 
            // cmdAppendZone
            // 
            cmdAppendZone.BorderColour = Color.Empty;
            cmdAppendZone.CustomColour = false;
            cmdAppendZone.FlatBottom = false;
            cmdAppendZone.FlatTop = false;
            cmdAppendZone.Location = new Point(6, 248);
            cmdAppendZone.Name = "cmdAppendZone";
            cmdAppendZone.Padding = new Padding(5);
            cmdAppendZone.Size = new Size(80, 28);
            cmdAppendZone.TabIndex = 2;
            cmdAppendZone.Text = "Append";
            cmdAppendZone.Click += cmdAppendZone_Click;
            // 
            // fraWorlds
            // 
            fraWorlds.Controls.Add(dgvWorlds);
            fraWorlds.Controls.Add(cmdRemoveWorld);
            fraWorlds.Controls.Add(cmdAppendWorld);
            fraWorlds.Location = new Point(225, 3);
            fraWorlds.Name = "fraWorlds";
            fraWorlds.Size = new Size(178, 284);
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
            dgvWorlds.CellValidating += dgvWorlds_CellValidating;
            dgvWorlds.CellValueChanged += dgvWorlds_CellValueChanged;
            // 
            // cmdRemoveWorld
            // 
            cmdRemoveWorld.BorderColour = Color.Empty;
            cmdRemoveWorld.CustomColour = false;
            cmdRemoveWorld.FlatBottom = false;
            cmdRemoveWorld.FlatTop = false;
            cmdRemoveWorld.Location = new Point(92, 248);
            cmdRemoveWorld.Name = "cmdRemoveWorld";
            cmdRemoveWorld.Padding = new Padding(1, 5, 1, 5);
            cmdRemoveWorld.Size = new Size(80, 28);
            cmdRemoveWorld.TabIndex = 2;
            cmdRemoveWorld.Text = "Remove Last";
            cmdRemoveWorld.Click += cmdRemoveWorld_Click;
            // 
            // cmdAppendWorld
            // 
            cmdAppendWorld.BorderColour = Color.Empty;
            cmdAppendWorld.CustomColour = false;
            cmdAppendWorld.FlatBottom = false;
            cmdAppendWorld.FlatTop = false;
            cmdAppendWorld.Location = new Point(6, 248);
            cmdAppendWorld.Name = "cmdAppendWorld";
            cmdAppendWorld.Padding = new Padding(5);
            cmdAppendWorld.Size = new Size(80, 28);
            cmdAppendWorld.TabIndex = 2;
            cmdAppendWorld.Text = "Append";
            cmdAppendWorld.Click += cmdAppendWorld_Click;
            // 
            // fraMusic
            // 
            fraMusic.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraMusic.Controls.Add(txtMusic);
            fraMusic.Location = new Point(3, 3);
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
            lblEIDError.Location = new Point(9, 63);
            lblEIDError.Name = "lblEIDError";
            lblEIDError.Size = new Size(66, 15);
            lblEIDError.TabIndex = 1;
            lblEIDError.Text = "EID ERROR!";
            lblEIDError.Visible = false;
            // 
            // fraSpecialLoadList
            // 
            fraSpecialLoadList.Controls.Add(pictureBox1);
            fraSpecialLoadList.Controls.Add(lblEIDErrorSP);
            fraSpecialLoadList.Controls.Add(cmdRemoveSP);
            fraSpecialLoadList.Controls.Add(txtSPLoadList);
            fraSpecialLoadList.Controls.Add(lbSPLoadList);
            fraSpecialLoadList.Controls.Add(cmdAppendSP);
            fraSpecialLoadList.Location = new Point(225, 293);
            fraSpecialLoadList.Name = "fraSpecialLoadList";
            fraSpecialLoadList.Size = new Size(156, 296);
            fraSpecialLoadList.TabIndex = 4;
            fraSpecialLoadList.TabStop = false;
            fraSpecialLoadList.Text = "Special Load List";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(106, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(16, 16);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
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
            cmdRemoveSP.Size = new Size(68, 28);
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
            txtSPLoadList.KeyDown += txtSPLoadList_KeyDown;
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
            lbSPLoadList.DoubleClick += lbSPLoadList_DoubleClick;
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
            cmdAppendSP.Size = new Size(68, 28);
            cmdAppendSP.TabIndex = 2;
            cmdAppendSP.Text = "Append";
            cmdAppendSP.Click += cmdAppendSP_Click;
            // 
            // fraZoneFlags
            // 
            fraZoneFlags.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraZoneFlags.Controls.Add(txtZoneFlags);
            fraZoneFlags.Location = new Point(93, 3);
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
            // fraDrawGenFlag
            //
            fraDrawGenFlag.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraDrawGenFlag.Location = new Point(3, 60);
            fraDrawGenFlag.Name = "fraDrawGenFlag";
            fraDrawGenFlag.Size = new Size(206, 52);
            fraDrawGenFlag.TabIndex = 3;
            fraDrawGenFlag.TabStop = false;
            fraDrawGenFlag.Text = "Draw list gen (c2export)";
            //
            // chkDrawGenFlag
            //
            chkDrawGenFlag.Location = new Point(10, 22);
            chkDrawGenFlag.Text = "Skip for this zone";
            chkDrawGenFlag.AutoSize = true;
            chkDrawGenFlag.Checked = false;
            chkDrawGenFlag.CheckedChanged += chkDrawGen_Changed;
            fraDrawGenFlag.Controls.Add(chkDrawGenFlag);
            //
            // fraTransLoadOverride
            //
            fraTransLoadOverride.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTransLoadOverride.Location = new Point(3, 120);
            fraTransLoadOverride.Name = "fraTransLoadOverride";
            fraTransLoadOverride.Size = new Size(206, 52);
            fraTransLoadOverride.TabIndex = 4;
            fraTransLoadOverride.TabStop = false;
            fraTransLoadOverride.Text = "Trans load override (c2export)";
            //
            // chkTransLoadOverride
            // 
            chkTransLoadOverride.Location = new Point(10, 22);
            chkTransLoadOverride.Text = "Enable";
            chkTransLoadOverride.AutoSize = true;
            chkTransLoadOverride.Checked = false;
            chkTransLoadOverride.CheckedChanged += chkTransLoadOverride_Changed;
            fraTransLoadOverride.Controls.Add(chkTransLoadOverride);
            //
            // cmbTransLoadOverride
            //
            cmbTransLoadOverride.Location = new Point(75, 20);
            cmbTransLoadOverride.Size = new Size(125, 20);
            cmbTransLoadOverride.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTransLoadOverride.Items.AddRange([
                "[0] none",
                "[1] textures",
                "[2] normal entries",
                "[3] all",
                ]);
            fraTransLoadOverride.Controls.Add(cmbTransLoadOverride);
            // 
            // pnHeader
            // 
            pnHeader.BackColor = Color.Transparent;
            pnHeader.Controls.Add(pnMisc);
            pnHeader.Controls.Add(fraZones);
            pnHeader.Controls.Add(fraWorlds);
            pnHeader.Controls.Add(fraSpecialLoadList);
            pnHeader.Dock = DockStyle.Fill;
            pnHeader.Location = new Point(0, 0);
            pnHeader.Name = "pnHeader";
            pnHeader.Size = new Size(573, 652);
            pnHeader.TabIndex = 5;
            // 
            // pnMisc
            // 
            pnMisc.Controls.Add(fraMusic);
            pnMisc.Controls.Add(fraZoneFlags);
            pnMisc.Controls.Add(fraDrawGenFlag);
            pnMisc.Controls.Add(fraTransLoadOverride);
            pnMisc.Controls.Add(lblEIDError);
            pnMisc.Location = new Point(3, 293);
            pnMisc.Name = "pnMisc";
            pnMisc.Size = new Size(216, 296);
            pnMisc.TabIndex = 5;
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            fraZoneFlags.ResumeLayout(false);
            fraZoneFlags.PerformLayout();
            pnHeader.ResumeLayout(false);
            pnMisc.ResumeLayout(false);
            pnMisc.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvZones;
        private AltUI.Controls.DarkGroupBox fraZones;
        private AltUI.Controls.DarkGroupBox fraWorlds;
        private DataGridView dgvWorlds;
        private AltUI.Controls.DarkGroupBox fraMusic;
        private AltUI.Controls.DarkTextBox txtMusic;
        private AltUI.Controls.DarkGroupBox fraDrawGenFlag;
        private AltUI.Controls.DarkCheckBox chkDrawGenFlag;
        private DarkGroupBox fraTransLoadOverride;
        private DarkCheckBox chkTransLoadOverride;
        private DarkComboBox cmbTransLoadOverride;
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
        private Panel pnMisc;
        private AltUI.Controls.DarkButton cmdRemoveZone;
        private AltUI.Controls.DarkButton cmdAppendZone;
        private AltUI.Controls.DarkButton cmdRemoveWorld;
        private AltUI.Controls.DarkButton cmdAppendWorld;
        private PictureBox pictureBox1;
    }
}
