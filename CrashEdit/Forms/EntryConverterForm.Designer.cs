using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class EntryConverterForm
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
            dgvAnim = new DataGridView();
            cmbMode = new DarkComboBox();
            cmdLoad = new DarkButton();
            cmdProcess = new DarkButton();
            chkSetModelEID = new CheckBox();
            cmdClear = new DarkButton();
            cmbType = new DarkComboBox();
            dgvModel = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvAnim).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvModel).BeginInit();
            SuspendLayout();
            // 
            // dgvAnim
            // 
            dgvAnim.AllowUserToAddRows = false;
            dgvAnim.AllowUserToResizeColumns = false;
            dgvAnim.AllowUserToResizeRows = false;
            dgvAnim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAnim.ColumnHeadersHeight = 20;
            dgvAnim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAnim.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvAnim.Location = new Point(12, 70);
            dgvAnim.Name = "dgvAnim";
            dgvAnim.RowHeadersWidth = 24;
            dgvAnim.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvAnim.ShowCellToolTips = false;
            dgvAnim.Size = new Size(332, 420);
            dgvAnim.TabIndex = 0;
            dgvAnim.CellBeginEdit += dgvAnim_CellBeginEdit;
            dgvAnim.CellValidating += EID_Validating;
            dgvAnim.RowsAdded += dgvAnim_RowsAdded;
            // 
            // cmbMode
            // 
            cmbMode.DrawMode = DrawMode.OwnerDrawVariable;
            cmbMode.FormattingEnabled = true;
            cmbMode.Location = new Point(12, 40);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(121, 24);
            cmbMode.TabIndex = 1;
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;
            // 
            // cmdLoad
            // 
            cmdLoad.BorderColour = Color.Empty;
            cmdLoad.CustomColour = false;
            cmdLoad.FlatBottom = false;
            cmdLoad.FlatTop = false;
            cmdLoad.Location = new Point(139, 12);
            cmdLoad.Name = "cmdLoad";
            cmdLoad.Padding = new Padding(5);
            cmdLoad.Size = new Size(75, 23);
            cmdLoad.TabIndex = 2;
            cmdLoad.Text = "Browse...";
            cmdLoad.Click += cmdLoad_Click;
            // 
            // cmdProcess
            // 
            cmdProcess.BorderColour = Color.Empty;
            cmdProcess.CustomColour = false;
            cmdProcess.Enabled = false;
            cmdProcess.FlatBottom = false;
            cmdProcess.FlatTop = false;
            cmdProcess.Location = new Point(408, 16);
            cmdProcess.Name = "cmdProcess";
            cmdProcess.Padding = new Padding(5);
            cmdProcess.Size = new Size(152, 40);
            cmdProcess.TabIndex = 3;
            cmdProcess.Text = "Process";
            cmdProcess.Click += cmdProcess_Click;
            // 
            // chkSetModelEID
            // 
            chkSetModelEID.AutoSize = true;
            chkSetModelEID.BackColor = Color.Transparent;
            chkSetModelEID.Checked = true;
            chkSetModelEID.CheckState = CheckState.Checked;
            chkSetModelEID.Location = new Point(220, 44);
            chkSetModelEID.Name = "chkSetModelEID";
            chkSetModelEID.Size = new Size(174, 19);
            chkSetModelEID.TabIndex = 4;
            chkSetModelEID.Text = "Set model EID automatically";
            chkSetModelEID.UseVisualStyleBackColor = false;
            // 
            // cmdClear
            // 
            cmdClear.BorderColour = Color.Empty;
            cmdClear.CustomColour = false;
            cmdClear.Enabled = false;
            cmdClear.FlatBottom = false;
            cmdClear.FlatTop = false;
            cmdClear.Location = new Point(139, 41);
            cmdClear.Name = "cmdClear";
            cmdClear.Padding = new Padding(5);
            cmdClear.Size = new Size(75, 23);
            cmdClear.TabIndex = 5;
            cmdClear.Text = "Clear";
            cmdClear.Click += cmdClear_Click;
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawVariable;
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(12, 11);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(121, 24);
            cmbType.TabIndex = 1;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // dgvModel
            // 
            dgvModel.AllowUserToAddRows = false;
            dgvModel.AllowUserToResizeColumns = false;
            dgvModel.AllowUserToResizeRows = false;
            dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvModel.ColumnHeadersHeight = 20;
            dgvModel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvModel.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvModel.Location = new Point(350, 70);
            dgvModel.Name = "dgvModel";
            dgvModel.RowHeadersWidth = 24;
            dgvModel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvModel.ShowCellToolTips = false;
            dgvModel.Size = new Size(234, 420);
            dgvModel.TabIndex = 0;
            dgvModel.CellBeginEdit += dgvModel_CellBeginEdit;
            dgvModel.CellValidating += EID_Validating;
            dgvModel.RowsAdded += dgvModel_RowsAdded;
            // 
            // EntryConverterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 505);
            Controls.Add(cmdClear);
            Controls.Add(chkSetModelEID);
            Controls.Add(cmdProcess);
            Controls.Add(cmdLoad);
            Controls.Add(cmbType);
            Controls.Add(cmbMode);
            Controls.Add(dgvModel);
            Controls.Add(dgvAnim);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EntryConverterForm";
            Text = "Entry Converter";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            ((System.ComponentModel.ISupportInitialize)dgvAnim).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvModel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private DataGridView dgvAnim;
        private DarkComboBox cmbMode;
        private DarkButton cmdLoad;
        private DarkButton cmdProcess;
        private CheckBox chkSetModelEID;
        private DarkButton cmdClear;
        private DarkComboBox cmbType;
        private DataGridView dgvModel;
    }
}