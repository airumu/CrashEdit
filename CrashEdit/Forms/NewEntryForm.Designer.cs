namespace CrashEdit
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
            this.fraType = new System.Windows.Forms.GroupBox();
            this.numType = new DarkUI.Controls.DarkNumericUpDown();
            this.dpdType = new DarkUI.Controls.DarkComboBox();
            this.fraName = new System.Windows.Forms.GroupBox();
            this.lblEIDErr = new DarkUI.Controls.DarkLabel();
            this.txtEID = new DarkUI.Controls.DarkTextBox();
            this.cmdOK = new DarkUI.Controls.DarkButton();
            this.cmdCancel = new DarkUI.Controls.DarkButton();
            this.fraType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numType)).BeginInit();
            this.fraName.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraType
            // 
            this.fraType.AutoSize = true;
            this.fraType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fraType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraType.Controls.Add(this.numType);
            this.fraType.Controls.Add(this.dpdType);
            this.fraType.ForeColor = System.Drawing.SystemColors.Control;
            this.fraType.Location = new System.Drawing.Point(181, 11);
            this.fraType.Name = "fraType";
            this.fraType.Size = new System.Drawing.Size(197, 56);
            this.fraType.TabIndex = 1;
            this.fraType.TabStop = false;
            this.fraType.Text = "Entry Type";
            // 
            // numType
            // 
            this.numType.Enabled = false;
            this.numType.Location = new System.Drawing.Point(132, 18);
            this.numType.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.numType.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numType.Name = "numType";
            this.numType.Size = new System.Drawing.Size(59, 19);
            this.numType.TabIndex = 1;
            this.numType.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dpdType
            // 
            this.dpdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dpdType.FormattingEnabled = true;
            this.dpdType.Location = new System.Drawing.Point(6, 18);
            this.dpdType.Name = "dpdType";
            this.dpdType.Size = new System.Drawing.Size(120, 20);
            this.dpdType.TabIndex = 0;
            this.dpdType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // fraName
            // 
            this.fraName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraName.Controls.Add(this.lblEIDErr);
            this.fraName.Controls.Add(this.txtEID);
            this.fraName.ForeColor = System.Drawing.SystemColors.Control;
            this.fraName.Location = new System.Drawing.Point(12, 11);
            this.fraName.Name = "fraName";
            this.fraName.Size = new System.Drawing.Size(163, 66);
            this.fraName.TabIndex = 2;
            this.fraName.TabStop = false;
            this.fraName.Text = "Entry Name";
            // 
            // lblEIDErr
            // 
            this.lblEIDErr.AutoSize = true;
            this.lblEIDErr.ForeColor = System.Drawing.Color.Red;
            this.lblEIDErr.Location = new System.Drawing.Point(6, 42);
            this.lblEIDErr.Name = "lblEIDErr";
            this.lblEIDErr.Size = new System.Drawing.Size(163, 12);
            this.lblEIDErr.TabIndex = 6;
            this.lblEIDErr.Text = "VERY LONG EID ERROR OMG";
            this.lblEIDErr.Visible = false;
            // 
            // txtEID
            // 
            this.txtEID.Location = new System.Drawing.Point(7, 18);
            this.txtEID.MaxLength = 5;
            this.txtEID.Name = "txtEID";
            this.txtEID.Size = new System.Drawing.Size(50, 19);
            this.txtEID.TabIndex = 0;
            this.txtEID.Text = "NONE!";
            this.txtEID.TextChanged += new System.EventHandler(this.txtEID_TextChanged);
            // 
            // cmdOK
            // 
            this.cmdOK.Enabled = false;
            this.cmdOK.Location = new System.Drawing.Point(223, 71);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 21);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(304, 71);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 21);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // NewEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(391, 102);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.fraName);
            this.Controls.Add(this.fraType);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewEntryForm";
            this.Text = "New Entry";
            this.fraType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numType)).EndInit();
            this.fraName.ResumeLayout(false);
            this.fraName.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox fraType;
        private DarkUI.Controls.DarkNumericUpDown numType;
        private System.Windows.Forms.GroupBox fraName;
        private DarkUI.Controls.DarkTextBox txtEID;
        private DarkUI.Controls.DarkLabel lblEIDErr;
        private DarkUI.Controls.DarkButton cmdOK;
        private DarkUI.Controls.DarkButton cmdCancel;
        private DarkUI.Controls.DarkComboBox dpdType;
    }
}