using AltUI.Controls;

namespace CrashEdit.CE
{
    public partial class NewEntryForm
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
            fraType = new DarkGroupBox();
            numType = new DarkNumericUpDown();
            dpdType = new DarkComboBox();
            fraName = new DarkGroupBox();
            lblEIDErr = new Label();
            txtEID = new DarkTextBox();
            cmdOK = new DarkButton();
            cmdCancel = new DarkButton();
            fraType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numType).BeginInit();
            fraName.SuspendLayout();
            SuspendLayout();
            // 
            // fraType
            // 
            fraType.AutoSize = true;
            fraType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraType.Controls.Add(numType);
            fraType.Controls.Add(dpdType);
            fraType.Location = new Point(211, 14);
            fraType.Margin = new Padding(4, 3, 4, 3);
            fraType.Name = "fraType";
            fraType.Padding = new Padding(4, 3, 4, 3);
            fraType.Size = new Size(231, 68);
            fraType.TabIndex = 1;
            fraType.TabStop = false;
            fraType.Text = "Entry Type";
            // 
            // numType
            // 
            numType.Enabled = false;
            numType.Location = new Point(154, 23);
            numType.Margin = new Padding(4, 3, 4, 3);
            numType.Maximum = new decimal(new int[] { 22, 0, 0, 0 });
            numType.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numType.Name = "numType";
            numType.Size = new Size(69, 23);
            numType.TabIndex = 1;
            numType.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dpdType
            // 
            dpdType.DrawMode = DrawMode.OwnerDrawVariable;
            dpdType.FormattingEnabled = true;
            dpdType.Location = new Point(7, 22);
            dpdType.Margin = new Padding(4, 3, 4, 3);
            dpdType.Name = "dpdType";
            dpdType.Size = new Size(139, 24);
            dpdType.TabIndex = 0;
            dpdType.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // fraName
            // 
            fraName.Controls.Add(lblEIDErr);
            fraName.Controls.Add(txtEID);
            fraName.Location = new Point(14, 14);
            fraName.Margin = new Padding(4, 3, 4, 3);
            fraName.Name = "fraName";
            fraName.Padding = new Padding(4, 3, 4, 3);
            fraName.Size = new Size(190, 82);
            fraName.TabIndex = 2;
            fraName.TabStop = false;
            fraName.Text = "Entry Name";
            // 
            // lblEIDErr
            // 
            lblEIDErr.AutoSize = true;
            lblEIDErr.ForeColor = Color.Red;
            lblEIDErr.Location = new Point(7, 53);
            lblEIDErr.Margin = new Padding(4, 0, 4, 0);
            lblEIDErr.Name = "lblEIDErr";
            lblEIDErr.Size = new Size(159, 15);
            lblEIDErr.TabIndex = 6;
            lblEIDErr.Text = "VERY LONG EID ERROR OMG";
            lblEIDErr.Visible = false;
            // 
            // txtEID
            // 
            txtEID.BackColor = Color.FromArgb(26, 26, 28);
            txtEID.BorderStyle = BorderStyle.FixedSingle;
            txtEID.ForeColor = Color.FromArgb(213, 213, 213);
            txtEID.Location = new Point(8, 23);
            txtEID.Margin = new Padding(4, 3, 4, 3);
            txtEID.MaxLength = 5;
            txtEID.Name = "txtEID";
            txtEID.Size = new Size(58, 23);
            txtEID.TabIndex = 0;
            txtEID.Text = "NONE!";
            txtEID.TextChanged += txtEID_TextChanged;
            // 
            // cmdOK
            // 
            cmdOK.BorderColour = Color.Empty;
            cmdOK.CustomColour = false;
            cmdOK.Enabled = false;
            cmdOK.FlatBottom = false;
            cmdOK.FlatTop = false;
            cmdOK.Location = new Point(260, 89);
            cmdOK.Margin = new Padding(4, 3, 4, 3);
            cmdOK.Name = "cmdOK";
            cmdOK.Padding = new Padding(6, 6, 6, 6);
            cmdOK.Size = new Size(88, 27);
            cmdOK.TabIndex = 4;
            cmdOK.Text = "OK";
            cmdOK.Click += cmdOK_Click;
            // 
            // cmdCancel
            // 
            cmdCancel.BorderColour = Color.Empty;
            cmdCancel.CustomColour = false;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.FlatBottom = false;
            cmdCancel.FlatTop = false;
            cmdCancel.Location = new Point(355, 89);
            cmdCancel.Margin = new Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Padding = new Padding(6, 6, 6, 6);
            cmdCancel.Size = new Size(88, 27);
            cmdCancel.TabIndex = 3;
            cmdCancel.Text = "Cancel";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // NewEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(456, 127);
            Controls.Add(cmdOK);
            Controls.Add(cmdCancel);
            Controls.Add(fraName);
            Controls.Add(fraType);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewEntryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "New Entry";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numType).EndInit();
            fraName.ResumeLayout(false);
            fraName.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DarkGroupBox fraType;
        private DarkNumericUpDown numType;
        private DarkGroupBox fraName;
        private DarkTextBox txtEID;
        private Label lblEIDErr;
        private DarkButton cmdOK;
        private DarkButton cmdCancel;
        private DarkComboBox dpdType;
    }
}