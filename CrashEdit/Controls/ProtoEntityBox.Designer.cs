namespace CrashEdit
{
    partial class ProtoEntityBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numType = new DarkUI.Controls.DarkNumericUpDown();
            this.fraType = new System.Windows.Forms.GroupBox();
            this.lblCodeName = new DarkUI.Controls.DarkLabel();
            this.fraSubtype = new System.Windows.Forms.GroupBox();
            this.numSubtype = new DarkUI.Controls.DarkNumericUpDown();
            this.fraPosition = new System.Windows.Forms.GroupBox();
            this.lblZ = new DarkUI.Controls.DarkLabel();
            this.lblY = new DarkUI.Controls.DarkLabel();
            this.lblX = new DarkUI.Controls.DarkLabel();
            this.numZ = new DarkUI.Controls.DarkNumericUpDown();
            this.numY = new DarkUI.Controls.DarkNumericUpDown();
            this.numX = new DarkUI.Controls.DarkNumericUpDown();
            this.fraID = new System.Windows.Forms.GroupBox();
            this.numID = new DarkUI.Controls.DarkNumericUpDown();
            this.tbcTabs = new MetroFramework.Controls.MetroTabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.fraSettings = new System.Windows.Forms.GroupBox();
            this.lblModeC = new DarkUI.Controls.DarkLabel();
            this.numModeC = new DarkUI.Controls.DarkNumericUpDown();
            this.lblModeB = new DarkUI.Controls.DarkLabel();
            this.lblModeA = new DarkUI.Controls.DarkLabel();
            this.lblFlags = new DarkUI.Controls.DarkLabel();
            this.numModeB = new DarkUI.Controls.DarkNumericUpDown();
            this.numModeA = new DarkUI.Controls.DarkNumericUpDown();
            this.numFlags = new DarkUI.Controls.DarkNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numType)).BeginInit();
            this.fraType.SuspendLayout();
            this.fraSubtype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSubtype)).BeginInit();
            this.fraPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            this.fraID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numID)).BeginInit();
            this.tbcTabs.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.fraSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModeC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModeB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModeA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlags)).BeginInit();
            this.SuspendLayout();
            // 
            // numType
            // 
            this.numType.Location = new System.Drawing.Point(6, 20);
            this.numType.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numType.Name = "numType";
            this.numType.Size = new System.Drawing.Size(120, 21);
            this.numType.TabIndex = 1;
            this.numType.ValueChanged += new System.EventHandler(this.numType_ValueChanged);
            // 
            // fraType
            // 
            this.fraType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraType.Controls.Add(this.lblCodeName);
            this.fraType.Controls.Add(this.numType);
            this.fraType.ForeColor = System.Drawing.SystemColors.Window;
            this.fraType.Location = new System.Drawing.Point(131, 3);
            this.fraType.Name = "fraType";
            this.fraType.Size = new System.Drawing.Size(132, 62);
            this.fraType.TabIndex = 4;
            this.fraType.TabStop = false;
            this.fraType.Text = "Type";
            // 
            // lblCodeName
            // 
            this.lblCodeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeName.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.lblCodeName.Location = new System.Drawing.Point(2, 42);
            this.lblCodeName.Name = "lblCodeName";
            this.lblCodeName.Size = new System.Drawing.Size(120, 18);
            this.lblCodeName.TabIndex = 9;
            this.lblCodeName.Text = "CodeC";
            this.lblCodeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fraSubtype
            // 
            this.fraSubtype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSubtype.Controls.Add(this.numSubtype);
            this.fraSubtype.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSubtype.Location = new System.Drawing.Point(131, 70);
            this.fraSubtype.Name = "fraSubtype";
            this.fraSubtype.Size = new System.Drawing.Size(132, 47);
            this.fraSubtype.TabIndex = 5;
            this.fraSubtype.TabStop = false;
            this.fraSubtype.Text = "Subtype";
            // 
            // numSubtype
            // 
            this.numSubtype.Location = new System.Drawing.Point(6, 18);
            this.numSubtype.Name = "numSubtype";
            this.numSubtype.Size = new System.Drawing.Size(120, 21);
            this.numSubtype.TabIndex = 1;
            this.numSubtype.ValueChanged += new System.EventHandler(this.numSubtype_ValueChanged);
            // 
            // fraPosition
            // 
            this.fraPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraPosition.Controls.Add(this.lblZ);
            this.fraPosition.Controls.Add(this.lblY);
            this.fraPosition.Controls.Add(this.lblX);
            this.fraPosition.Controls.Add(this.numZ);
            this.fraPosition.Controls.Add(this.numY);
            this.fraPosition.Controls.Add(this.numX);
            this.fraPosition.ForeColor = System.Drawing.SystemColors.Window;
            this.fraPosition.Location = new System.Drawing.Point(3, 3);
            this.fraPosition.Name = "fraPosition";
            this.fraPosition.Size = new System.Drawing.Size(122, 92);
            this.fraPosition.TabIndex = 1;
            this.fraPosition.TabStop = false;
            this.fraPosition.Text = "Start Position";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblZ.Location = new System.Drawing.Point(6, 66);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 15);
            this.lblZ.TabIndex = 5;
            this.lblZ.Text = "Z";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblY.Location = new System.Drawing.Point(6, 42);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 15);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblX.Location = new System.Drawing.Point(6, 18);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 15);
            this.lblX.TabIndex = 3;
            this.lblX.Text = "X";
            // 
            // numZ
            // 
            this.numZ.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numZ.Location = new System.Drawing.Point(26, 65);
            this.numZ.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numZ.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numZ.Name = "numZ";
            this.numZ.Size = new System.Drawing.Size(86, 21);
            this.numZ.TabIndex = 4;
            this.numZ.ValueChanged += new System.EventHandler(this.numZ_ValueChanged);
            // 
            // numY
            // 
            this.numY.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numY.Location = new System.Drawing.Point(26, 41);
            this.numY.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numY.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(86, 21);
            this.numY.TabIndex = 3;
            this.numY.ValueChanged += new System.EventHandler(this.numY_ValueChanged);
            // 
            // numX
            // 
            this.numX.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numX.Location = new System.Drawing.Point(26, 17);
            this.numX.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numX.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(86, 21);
            this.numX.TabIndex = 2;
            this.numX.ValueChanged += new System.EventHandler(this.numX_ValueChanged);
            // 
            // fraID
            // 
            this.fraID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraID.Controls.Add(this.numID);
            this.fraID.ForeColor = System.Drawing.SystemColors.Window;
            this.fraID.Location = new System.Drawing.Point(3, 101);
            this.fraID.Name = "fraID";
            this.fraID.Size = new System.Drawing.Size(122, 45);
            this.fraID.TabIndex = 3;
            this.fraID.TabStop = false;
            this.fraID.Text = "ID";
            // 
            // numID
            // 
            this.numID.Location = new System.Drawing.Point(6, 18);
            this.numID.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numID.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numID.Name = "numID";
            this.numID.Size = new System.Drawing.Size(106, 21);
            this.numID.TabIndex = 1;
            this.numID.ValueChanged += new System.EventHandler(this.numID_ValueChanged);
            // 
            // tbcTabs
            // 
            this.tbcTabs.Controls.Add(this.tabGeneral);
            this.tbcTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcTabs.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.tbcTabs.Location = new System.Drawing.Point(0, 0);
            this.tbcTabs.Name = "tbcTabs";
            this.tbcTabs.SelectedIndex = 0;
            this.tbcTabs.Size = new System.Drawing.Size(398, 419);
            this.tbcTabs.Style = MetroFramework.MetroColorStyle.Teal;
            this.tbcTabs.TabIndex = 7;
            this.tbcTabs.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tbcTabs.UseSelectable = true;
            // 
            // tabGeneral
            // 
            this.tabGeneral.AutoScroll = true;
            this.tabGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabGeneral.Controls.Add(this.fraSettings);
            this.tabGeneral.Controls.Add(this.fraType);
            this.tabGeneral.Controls.Add(this.fraSubtype);
            this.tabGeneral.Controls.Add(this.fraPosition);
            this.tabGeneral.Controls.Add(this.fraID);
            this.tabGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabGeneral.ForeColor = System.Drawing.SystemColors.Window;
            this.tabGeneral.Location = new System.Drawing.Point(4, 38);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(390, 377);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // fraSettings
            // 
            this.fraSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSettings.Controls.Add(this.lblModeC);
            this.fraSettings.Controls.Add(this.numModeC);
            this.fraSettings.Controls.Add(this.lblModeB);
            this.fraSettings.Controls.Add(this.lblModeA);
            this.fraSettings.Controls.Add(this.lblFlags);
            this.fraSettings.Controls.Add(this.numModeB);
            this.fraSettings.Controls.Add(this.numModeA);
            this.fraSettings.Controls.Add(this.numFlags);
            this.fraSettings.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSettings.Location = new System.Drawing.Point(3, 151);
            this.fraSettings.Name = "fraSettings";
            this.fraSettings.Size = new System.Drawing.Size(160, 123);
            this.fraSettings.TabIndex = 8;
            this.fraSettings.TabStop = false;
            this.fraSettings.Text = "Special Settings";
            // 
            // lblModeC
            // 
            this.lblModeC.AutoSize = true;
            this.lblModeC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblModeC.Location = new System.Drawing.Point(6, 91);
            this.lblModeC.Name = "lblModeC";
            this.lblModeC.Size = new System.Drawing.Size(49, 15);
            this.lblModeC.TabIndex = 7;
            this.lblModeC.Text = "Mode C";
            // 
            // numModeC
            // 
            this.numModeC.Location = new System.Drawing.Point(62, 90);
            this.numModeC.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numModeC.Name = "numModeC";
            this.numModeC.Size = new System.Drawing.Size(86, 21);
            this.numModeC.TabIndex = 6;
            this.numModeC.ValueChanged += new System.EventHandler(this.numD_ValueChanged);
            // 
            // lblModeB
            // 
            this.lblModeB.AutoSize = true;
            this.lblModeB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblModeB.Location = new System.Drawing.Point(6, 67);
            this.lblModeB.Name = "lblModeB";
            this.lblModeB.Size = new System.Drawing.Size(48, 15);
            this.lblModeB.TabIndex = 5;
            this.lblModeB.Text = "Mode B";
            // 
            // lblModeA
            // 
            this.lblModeA.AutoSize = true;
            this.lblModeA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblModeA.Location = new System.Drawing.Point(6, 43);
            this.lblModeA.Name = "lblModeA";
            this.lblModeA.Size = new System.Drawing.Size(46, 15);
            this.lblModeA.TabIndex = 4;
            this.lblModeA.Text = "Mode A";
            // 
            // lblFlags
            // 
            this.lblFlags.AutoSize = true;
            this.lblFlags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblFlags.Location = new System.Drawing.Point(6, 19);
            this.lblFlags.Name = "lblFlags";
            this.lblFlags.Size = new System.Drawing.Size(38, 15);
            this.lblFlags.TabIndex = 3;
            this.lblFlags.Text = "Flags";
            // 
            // numModeB
            // 
            this.numModeB.Location = new System.Drawing.Point(62, 66);
            this.numModeB.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numModeB.Name = "numModeB";
            this.numModeB.Size = new System.Drawing.Size(86, 21);
            this.numModeB.TabIndex = 4;
            this.numModeB.ValueChanged += new System.EventHandler(this.numC_ValueChanged);
            // 
            // numModeA
            // 
            this.numModeA.Location = new System.Drawing.Point(62, 42);
            this.numModeA.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numModeA.Name = "numModeA";
            this.numModeA.Size = new System.Drawing.Size(86, 21);
            this.numModeA.TabIndex = 3;
            this.numModeA.ValueChanged += new System.EventHandler(this.numB_ValueChanged);
            // 
            // numFlags
            // 
            this.numFlags.Location = new System.Drawing.Point(62, 18);
            this.numFlags.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numFlags.Name = "numFlags";
            this.numFlags.Size = new System.Drawing.Size(86, 21);
            this.numFlags.TabIndex = 2;
            this.numFlags.ValueChanged += new System.EventHandler(this.numA_ValueChanged);
            // 
            // ProtoEntityBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcTabs);
            this.Name = "ProtoEntityBox";
            this.Size = new System.Drawing.Size(398, 419);
            ((System.ComponentModel.ISupportInitialize)(this.numType)).EndInit();
            this.fraType.ResumeLayout(false);
            this.fraSubtype.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSubtype)).EndInit();
            this.fraPosition.ResumeLayout(false);
            this.fraPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            this.fraID.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numID)).EndInit();
            this.tbcTabs.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.fraSettings.ResumeLayout(false);
            this.fraSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModeC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModeB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModeA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkNumericUpDown numType;
        private System.Windows.Forms.GroupBox fraType;
        private System.Windows.Forms.GroupBox fraSubtype;
        private DarkUI.Controls.DarkNumericUpDown numSubtype;
        private System.Windows.Forms.GroupBox fraPosition;
        private DarkUI.Controls.DarkLabel lblZ;
        private DarkUI.Controls.DarkLabel lblY;
        private DarkUI.Controls.DarkLabel lblX;
        private DarkUI.Controls.DarkNumericUpDown numZ;
        private DarkUI.Controls.DarkNumericUpDown numY;
        private DarkUI.Controls.DarkNumericUpDown numX;
        private System.Windows.Forms.GroupBox fraID;
        private DarkUI.Controls.DarkNumericUpDown numID;
        private MetroFramework.Controls.MetroTabControl tbcTabs;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox fraSettings;
        private DarkUI.Controls.DarkLabel lblModeC;
        private DarkUI.Controls.DarkNumericUpDown numModeC;
        private DarkUI.Controls.DarkLabel lblModeB;
        private DarkUI.Controls.DarkLabel lblModeA;
        private DarkUI.Controls.DarkLabel lblFlags;
        private DarkUI.Controls.DarkNumericUpDown numModeB;
        private DarkUI.Controls.DarkNumericUpDown numModeA;
        private DarkUI.Controls.DarkNumericUpDown numFlags;
        private DarkUI.Controls.DarkLabel lblCodeName;
    }
}
