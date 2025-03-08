using System.Windows.Forms;
using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class NSDBox
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
            fraID = new DarkGroupBox();
            txtID = new DarkTextBox();
            fraSpawns = new DarkGroupBox();
            cmdDelete = new DarkButton();
            cmdAppend = new DarkButton();
            cmdPaste = new DarkButton();
            cmdCopy = new DarkButton();
            cmdGetSpawn = new DarkButton();
            dgvSpawns = new DataGridView();
            fraEntityCount = new DarkGroupBox();
            lblEntityCount = new Label();
            rbtReload = new MetroSet_UI.Controls.MetroSetRadioButton();
            fraID.SuspendLayout();
            fraSpawns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSpawns).BeginInit();
            fraEntityCount.SuspendLayout();
            SuspendLayout();
            // 
            // fraID
            // 
            fraID.AutoSize = true;
            fraID.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraID.BackColor = Color.Transparent;
            fraID.Controls.Add(txtID);
            fraID.Location = new Point(3, 3);
            fraID.Name = "fraID";
            fraID.Size = new Size(76, 67);
            fraID.TabIndex = 0;
            fraID.TabStop = false;
            fraID.Text = "Level ID";
            // 
            // txtID
            // 
            txtID.BackColor = Color.FromArgb(26, 26, 28);
            txtID.BorderStyle = BorderStyle.FixedSingle;
            txtID.ForeColor = Color.FromArgb(213, 213, 213);
            txtID.Location = new Point(6, 22);
            txtID.MaxLength = 2;
            txtID.Name = "txtID";
            txtID.Size = new Size(64, 23);
            txtID.TabIndex = 2;
            txtID.TextChanged += txtID_TextChanged;
            txtID.KeyDown += txtID_KeyDown;
            txtID.LostFocus += txtID_LostFocus;
            // 
            // fraSpawns
            // 
            fraSpawns.BackColor = Color.Transparent;
            fraSpawns.Controls.Add(cmdDelete);
            fraSpawns.Controls.Add(cmdAppend);
            fraSpawns.Controls.Add(cmdPaste);
            fraSpawns.Controls.Add(cmdCopy);
            fraSpawns.Controls.Add(cmdGetSpawn);
            fraSpawns.Controls.Add(dgvSpawns);
            fraSpawns.Location = new Point(3, 76);
            fraSpawns.Name = "fraSpawns";
            fraSpawns.Size = new Size(523, 453);
            fraSpawns.TabIndex = 1;
            fraSpawns.TabStop = false;
            fraSpawns.Text = "Spawn Point(s)";
            // 
            // cmdDelete
            // 
            cmdDelete.BorderColour = Color.Empty;
            cmdDelete.CustomColour = false;
            cmdDelete.FlatBottom = false;
            cmdDelete.FlatTop = false;
            cmdDelete.Location = new Point(440, 204);
            cmdDelete.Name = "cmdDelete";
            cmdDelete.Padding = new Padding(5);
            cmdDelete.Size = new Size(75, 28);
            cmdDelete.TabIndex = 3;
            cmdDelete.Text = "Delete";
            cmdDelete.Click += cmdDelete_Click;
            // 
            // cmdAppend
            // 
            cmdAppend.BorderColour = Color.Empty;
            cmdAppend.CustomColour = false;
            cmdAppend.FlatBottom = false;
            cmdAppend.FlatTop = false;
            cmdAppend.Location = new Point(440, 170);
            cmdAppend.Name = "cmdAppend";
            cmdAppend.Padding = new Padding(5);
            cmdAppend.Size = new Size(75, 28);
            cmdAppend.TabIndex = 2;
            cmdAppend.Text = "Append";
            cmdAppend.Click += cmdAppend_Click;
            // 
            // cmdPaste
            // 
            cmdPaste.BorderColour = Color.Empty;
            cmdPaste.CustomColour = false;
            cmdPaste.FlatBottom = false;
            cmdPaste.FlatTop = false;
            cmdPaste.Location = new Point(440, 112);
            cmdPaste.Name = "cmdPaste";
            cmdPaste.Padding = new Padding(5);
            cmdPaste.Size = new Size(75, 28);
            cmdPaste.TabIndex = 3;
            cmdPaste.Text = "Paste";
            cmdPaste.Click += cmdPaste_Click;
            // 
            // cmdCopy
            // 
            cmdCopy.BorderColour = Color.Empty;
            cmdCopy.CustomColour = false;
            cmdCopy.FlatBottom = false;
            cmdCopy.FlatTop = false;
            cmdCopy.Location = new Point(440, 78);
            cmdCopy.Name = "cmdCopy";
            cmdCopy.Padding = new Padding(5);
            cmdCopy.Size = new Size(75, 28);
            cmdCopy.TabIndex = 2;
            cmdCopy.Text = "Copy";
            cmdCopy.Click += cmdCopy_Click;
            // 
            // cmdGetSpawn
            // 
            cmdGetSpawn.BorderColour = Color.Empty;
            cmdGetSpawn.CustomColour = false;
            cmdGetSpawn.FlatBottom = false;
            cmdGetSpawn.FlatTop = false;
            cmdGetSpawn.Location = new Point(440, 22);
            cmdGetSpawn.Name = "cmdGetSpawn";
            cmdGetSpawn.Padding = new Padding(5);
            cmdGetSpawn.Size = new Size(75, 28);
            cmdGetSpawn.TabIndex = 1;
            cmdGetSpawn.Text = "Get Spawn";
            cmdGetSpawn.Click += cmdGetSpawn_Click;
            // 
            // dgvSpawns
            // 
            dgvSpawns.AllowDrop = true;
            dgvSpawns.AllowUserToAddRows = false;
            dgvSpawns.AllowUserToResizeColumns = false;
            dgvSpawns.AllowUserToResizeRows = false;
            dgvSpawns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSpawns.ColumnHeadersHeight = 24;
            dgvSpawns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSpawns.Location = new Point(6, 22);
            dgvSpawns.MultiSelect = false;
            dgvSpawns.Name = "dgvSpawns";
            dgvSpawns.RowHeadersWidth = 24;
            dgvSpawns.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvSpawns.ScrollBars = ScrollBars.Vertical;
            dgvSpawns.ShowCellToolTips = false;
            dgvSpawns.Size = new Size(428, 424);
            dgvSpawns.TabIndex = 0;
            dgvSpawns.CellValidating += dgvSpawns_CellValidating;
            dgvSpawns.CellValueChanged += dgvSpawns_CellValueChanged;
            dgvSpawns.DragDrop += dgvSpawns_DragDrop;
            dgvSpawns.DragOver += dgvSpawns_DragOver;
            dgvSpawns.Paint += dgvSpawns_Paint;
            dgvSpawns.KeyDown += dgvSpawns_KeyDown;
            dgvSpawns.MouseDown += dgvSpawns_MouseDown;
            dgvSpawns.MouseMove += dgvSpawns_MouseMove;
            // 
            // fraEntityCount
            // 
            fraEntityCount.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraEntityCount.BackColor = Color.Transparent;
            fraEntityCount.Controls.Add(lblEntityCount);
            fraEntityCount.Location = new Point(85, 3);
            fraEntityCount.Name = "fraEntityCount";
            fraEntityCount.Size = new Size(88, 67);
            fraEntityCount.TabIndex = 0;
            fraEntityCount.TabStop = false;
            fraEntityCount.Text = "EntityCount";
            // 
            // lblEntityCount
            // 
            lblEntityCount.AutoSize = true;
            lblEntityCount.Location = new Point(6, 24);
            lblEntityCount.Name = "lblEntityCount";
            lblEntityCount.Size = new Size(48, 15);
            lblEntityCount.TabIndex = 2;
            lblEntityCount.Text = "{Count}";
            // 
            // rbtReload
            // 
            rbtReload.BackgroundColor = Color.FromArgb(30, 30, 30);
            rbtReload.BorderColor = Color.FromArgb(155, 155, 155);
            rbtReload.Checked = true;
            rbtReload.CheckSignColor = Color.FromArgb(65, 177, 225);
            rbtReload.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            rbtReload.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rbtReload.Font = new Font("Microsoft Sans Serif", 10F);
            rbtReload.Group = 0;
            rbtReload.IsDerivedStyle = true;
            rbtReload.Location = new Point(507, 3);
            rbtReload.Name = "rbtReload";
            rbtReload.Size = new Size(19, 17);
            rbtReload.Style = MetroSet_UI.Enums.Style.Dark;
            rbtReload.StyleManager = null;
            rbtReload.TabIndex = 2;
            rbtReload.ThemeAuthor = "Narwin";
            rbtReload.ThemeName = "MetroDark";
            rbtReload.Click += rbtReload_Click;
            // 
            // NSDBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(rbtReload);
            Controls.Add(fraSpawns);
            Controls.Add(fraEntityCount);
            Controls.Add(fraID);
            Name = "NSDBox";
            Size = new Size(686, 664);
            fraID.ResumeLayout(false);
            fraID.PerformLayout();
            fraSpawns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSpawns).EndInit();
            fraEntityCount.ResumeLayout(false);
            fraEntityCount.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }




        #endregion

        private AltUI.Controls.DarkGroupBox fraID;
        private DarkTextBox txtID;
        private DarkGroupBox fraSpawns;
        private DataGridView dgvSpawns;
        private DarkButton cmdGetSpawn;
        private DarkButton cmdCopy;
        private DarkButton cmdPaste;
        private DarkGroupBox fraEntityCount;
        private Label lblEntityCount;
        private MetroSet_UI.Controls.MetroSetRadioButton rbtReload;
        private DarkButton cmdDelete;
        private DarkButton cmdAppend;
    }
}
