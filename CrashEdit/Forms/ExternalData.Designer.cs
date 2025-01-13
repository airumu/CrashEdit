using AltUI.Controls;
using SharpFont;

namespace CrashEdit.CE
{
    partial class ExternalData
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbGroups = new DarkComboBox();
            btnExecute = new DarkButton();
            cmdAppend = new DarkButton();
            fraEditor = new DarkGroupBox();
            cmdRename = new DarkButton();
            dgvGroups = new DataGridView();
            cmdRemove = new DarkButton();
            txtGroups = new DarkTextBox();
            chkShowEditor = new CheckBox();
            darkGroupBox1 = new DarkGroupBox();
            fraEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGroups).BeginInit();
            darkGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbGroups
            // 
            cmbGroups.DrawMode = DrawMode.OwnerDrawVariable;
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(6, 22);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(121, 24);
            cmbGroups.TabIndex = 1;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // btnExecute
            // 
            btnExecute.BorderColour = Color.Empty;
            btnExecute.CustomColour = false;
            btnExecute.FlatBottom = false;
            btnExecute.FlatTop = false;
            btnExecute.Location = new Point(133, 21);
            btnExecute.Name = "btnExecute";
            btnExecute.Padding = new Padding(5);
            btnExecute.Size = new Size(75, 23);
            btnExecute.TabIndex = 0;
            btnExecute.Text = "OK";
            btnExecute.Click += btnExecute_Click;
            // 
            // cmdAppend
            // 
            cmdAppend.BorderColour = Color.Empty;
            cmdAppend.CustomColour = false;
            cmdAppend.FlatBottom = false;
            cmdAppend.FlatTop = false;
            cmdAppend.Location = new Point(18, 35);
            cmdAppend.Name = "cmdAppend";
            cmdAppend.Padding = new Padding(5);
            cmdAppend.Size = new Size(75, 23);
            cmdAppend.TabIndex = 4;
            cmdAppend.Text = "Append";
            cmdAppend.Click += cmdAppend_Click;
            // 
            // fraEditor
            // 
            fraEditor.Controls.Add(cmdRename);
            fraEditor.Controls.Add(dgvGroups);
            fraEditor.Controls.Add(cmdRemove);
            fraEditor.Controls.Add(txtGroups);
            fraEditor.Controls.Add(cmdAppend);
            fraEditor.Location = new Point(3, 83);
            fraEditor.Name = "fraEditor";
            fraEditor.Size = new Size(256, 326);
            fraEditor.TabIndex = 5;
            fraEditor.TabStop = false;
            fraEditor.Visible = false;
            // 
            // cmdRename
            // 
            cmdRename.BorderColour = Color.Empty;
            cmdRename.CustomColour = false;
            cmdRename.FlatBottom = false;
            cmdRename.FlatTop = false;
            cmdRename.Location = new Point(18, 61);
            cmdRename.Name = "cmdRename";
            cmdRename.Padding = new Padding(5);
            cmdRename.Size = new Size(75, 23);
            cmdRename.TabIndex = 7;
            cmdRename.Text = "Rename";
            cmdRename.Click += cmdRename_Click;
            // 
            // dgvGroups
            // 
            dgvGroups.AllowUserToResizeColumns = false;
            dgvGroups.AllowUserToResizeRows = false;
            dgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGroups.Location = new Point(112, 6);
            dgvGroups.Name = "dgvGroups";
            dgvGroups.RowHeadersWidth = 24;
            dgvGroups.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvGroups.ScrollBars = ScrollBars.Vertical;
            dgvGroups.Size = new Size(140, 314);
            dgvGroups.TabIndex = 3;
            dgvGroups.CellBeginEdit += dgvGroups_CellBeginEdit;
            dgvGroups.EditingControlShowing += dgvGroups_EditingControlShowing;
            // 
            // cmdRemove
            // 
            cmdRemove.BorderColour = Color.Empty;
            cmdRemove.CustomColour = false;
            cmdRemove.FlatBottom = false;
            cmdRemove.FlatTop = false;
            cmdRemove.Location = new Point(18, 90);
            cmdRemove.Name = "cmdRemove";
            cmdRemove.Padding = new Padding(5);
            cmdRemove.Size = new Size(75, 23);
            cmdRemove.TabIndex = 6;
            cmdRemove.Text = "Remove";
            cmdRemove.Click += cmdRemove_Click;
            // 
            // txtGroups
            // 
            txtGroups.BackColor = Color.FromArgb(26, 26, 28);
            txtGroups.BorderStyle = BorderStyle.FixedSingle;
            txtGroups.ForeColor = Color.FromArgb(213, 213, 213);
            txtGroups.Location = new Point(6, 6);
            txtGroups.Name = "txtGroups";
            txtGroups.Size = new Size(100, 23);
            txtGroups.TabIndex = 5;
            // 
            // chkShowEditor
            // 
            chkShowEditor.AutoSize = true;
            chkShowEditor.Location = new Point(9, 52);
            chkShowEditor.Name = "chkShowEditor";
            chkShowEditor.Size = new Size(89, 19);
            chkShowEditor.TabIndex = 6;
            chkShowEditor.Text = "Show Editor";
            chkShowEditor.UseVisualStyleBackColor = true;
            chkShowEditor.CheckedChanged += chkShowEditor_CheckedChanged;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.Controls.Add(chkShowEditor);
            darkGroupBox1.Controls.Add(cmbGroups);
            darkGroupBox1.Controls.Add(btnExecute);
            darkGroupBox1.Location = new Point(3, 3);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(216, 74);
            darkGroupBox1.TabIndex = 8;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Use additional data";
            // 
            // ExternalData
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(267, 417);
            Controls.Add(darkGroupBox1);
            Controls.Add(fraEditor);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExternalData";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "ExternalData";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraEditor.ResumeLayout(false);
            fraEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private AltUI.Controls.DarkComboBox cmbGroups;
        private AltUI.Controls.DarkButton btnExecute;
        private AltUI.Controls.DarkButton cmdAppend;
        private DarkGroupBox fraEditor;
        private AltUI.Controls.DarkButton cmdRemove;
        private AltUI.Controls.DarkTextBox txtGroups;
        private AltUI.Controls.DarkButton cmdRename;
        private CheckBox chkShowEditor;
        private DataGridView dgvGroups;
        private DarkGroupBox darkGroupBox1;
    }
}