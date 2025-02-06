
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
            cmbMode = new AltUI.Controls.DarkComboBox();
            cmdLoad = new AltUI.Controls.DarkButton();
            cmdProcess = new AltUI.Controls.DarkButton();
            chkShowFilePath = new CheckBox();
            chkSetModelEID = new CheckBox();
            cmdClear = new AltUI.Controls.DarkButton();
            cmbType = new AltUI.Controls.DarkComboBox();
            lblWarning = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAnim).BeginInit();
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
            dgvAnim.Location = new Point(12, 70);
            dgvAnim.Name = "dgvAnim";
            dgvAnim.RowHeadersWidth = 24;
            dgvAnim.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvAnim.Size = new Size(469, 421);
            dgvAnim.TabIndex = 0;
            dgvAnim.CellBeginEdit += dgvAnim_CellBeginEdit;
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
            cmdProcess.Location = new Point(406, 12);
            cmdProcess.Name = "cmdProcess";
            cmdProcess.Padding = new Padding(5);
            cmdProcess.Size = new Size(75, 23);
            cmdProcess.TabIndex = 3;
            cmdProcess.Text = "Process";
            cmdProcess.Click += cmdProcess_Click;
            // 
            // chkShowFilePath
            // 
            chkShowFilePath.AutoSize = true;
            chkShowFilePath.Location = new Point(400, 44);
            chkShowFilePath.Name = "chkShowFilePath";
            chkShowFilePath.Size = new Size(103, 19);
            chkShowFilePath.TabIndex = 4;
            chkShowFilePath.Text = "Show File Path";
            chkShowFilePath.UseVisualStyleBackColor = true;
            chkShowFilePath.Visible = false;
            chkShowFilePath.CheckedChanged += chkShowFilePath_CheckedChanged;
            // 
            // chkSetModelEID
            // 
            chkSetModelEID.AutoSize = true;
            chkSetModelEID.Location = new Point(220, 44);
            chkSetModelEID.Name = "chkSetModelEID";
            chkSetModelEID.Size = new Size(174, 19);
            chkSetModelEID.TabIndex = 4;
            chkSetModelEID.Text = "Set model EID automatically";
            chkSetModelEID.UseVisualStyleBackColor = true;
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
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.BackColor = Color.Transparent;
            lblWarning.ForeColor = Color.Red;
            lblWarning.Location = new Point(220, 9);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(126, 30);
            lblWarning.TabIndex = 6;
            lblWarning.Text = "Model conversions are\r\nexperimental!";
            // 
            // EntryConverterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 503);
            Controls.Add(lblWarning);
            Controls.Add(cmdClear);
            Controls.Add(chkSetModelEID);
            Controls.Add(chkShowFilePath);
            Controls.Add(cmdProcess);
            Controls.Add(cmdLoad);
            Controls.Add(cmbType);
            Controls.Add(cmbMode);
            Controls.Add(dgvAnim);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EntryConverterForm";
            Text = "Entry Converter";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            ((System.ComponentModel.ISupportInitialize)dgvAnim).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private DataGridView dgvAnim;
        private AltUI.Controls.DarkComboBox cmbMode;
        private AltUI.Controls.DarkButton cmdLoad;
        private AltUI.Controls.DarkButton cmdProcess;
        private CheckBox chkShowFilePath;
        private CheckBox chkSetModelEID;
        private AltUI.Controls.DarkButton cmdClear;
        private AltUI.Controls.DarkComboBox cmbType;
        private Label lblWarning;
    }
}