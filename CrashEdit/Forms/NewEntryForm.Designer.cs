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
            this.fraType.Location = new System.Drawing.Point(211, 14);
            this.fraType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraType.Name = "fraType";
            this.fraType.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraType.Size = new System.Drawing.Size(229, 66);
            this.fraType.TabIndex = 1;
            this.fraType.TabStop = false;
            this.fraType.Text = "Entry Type";
            // 
            // numType
            // 
            this.numType.Enabled = false;
            this.numType.Location = new System.Drawing.Point(154, 22);
            this.numType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numType.Size = new System.Drawing.Size(69, 21);
            this.numType.TabIndex = 1;
            this.numType.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dpdType
            // 
            this.dpdType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.dpdType.FormattingEnabled = true;
            this.dpdType.Location = new System.Drawing.Point(7, 22);
            this.dpdType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dpdType.Name = "dpdType";
            this.dpdType.Size = new System.Drawing.Size(139, 22);
            this.dpdType.TabIndex = 0;
            this.dpdType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // fraName
            // 
            this.fraName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraName.Controls.Add(this.lblEIDErr);
            this.fraName.Controls.Add(this.txtEID);
            this.fraName.ForeColor = System.Drawing.SystemColors.Control;
            this.fraName.Location = new System.Drawing.Point(14, 14);
            this.fraName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraName.Name = "fraName";
            this.fraName.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraName.Size = new System.Drawing.Size(190, 82);
            this.fraName.TabIndex = 2;
            this.fraName.TabStop = false;
            this.fraName.Text = "Entry Name";
            // 
            // lblEIDErr
            // 
            this.lblEIDErr.AutoSize = true;
            this.lblEIDErr.ForeColor = System.Drawing.Color.Red;
            this.lblEIDErr.Location = new System.Drawing.Point(7, 52);
            this.lblEIDErr.Name = "lblEIDErr";
            this.lblEIDErr.Size = new System.Drawing.Size(175, 15);
            this.lblEIDErr.TabIndex = 6;
            this.lblEIDErr.Text = "VERY LONG EID ERROR OMG";
            this.lblEIDErr.Visible = false;
            // 
            // txtEID
            // 
            this.txtEID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.txtEID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtEID.Location = new System.Drawing.Point(8, 22);
            this.txtEID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEID.MaxLength = 5;
            this.txtEID.Name = "txtEID";
            this.txtEID.Size = new System.Drawing.Size(58, 21);
            this.txtEID.TabIndex = 0;
            this.txtEID.Text = "NONE!";
            this.txtEID.TextChanged += new System.EventHandler(this.txtEID_TextChanged);
            // 
            // cmdOK
            // 
            this.cmdOK.Enabled = false;
            this.cmdOK.Location = new System.Drawing.Point(260, 89);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdOK.Size = new System.Drawing.Size(87, 26);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(355, 89);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdCancel.Size = new System.Drawing.Size(87, 26);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // NewEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(456, 128);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.fraName);
            this.Controls.Add(this.fraType);
            this.Font = new System.Drawing.Font("Yu Gothic UI ", 9F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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