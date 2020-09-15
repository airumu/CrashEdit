namespace CrashEdit
{
    partial class OldEntityBox
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
            this.cmdInterpolate = new DarkUI.Controls.DarkButton();
            this.lblPositionIndex = new DarkUI.Controls.DarkLabel();
            this.cmdNextPosition = new DarkUI.Controls.DarkButton();
            this.cmdPreviousPosition = new DarkUI.Controls.DarkButton();
            this.cmdInsertPosition = new DarkUI.Controls.DarkButton();
            this.lblZ = new DarkUI.Controls.DarkLabel();
            this.cmdRemovePosition = new DarkUI.Controls.DarkButton();
            this.lblY = new DarkUI.Controls.DarkLabel();
            this.cmdAppendPosition = new DarkUI.Controls.DarkButton();
            this.lblX = new DarkUI.Controls.DarkLabel();
            this.numZ = new DarkUI.Controls.DarkNumericUpDown();
            this.numY = new DarkUI.Controls.DarkNumericUpDown();
            this.numX = new DarkUI.Controls.DarkNumericUpDown();
            this.fraID = new System.Windows.Forms.GroupBox();
            this.numID = new DarkUI.Controls.DarkNumericUpDown();
            this.tbcTabs = new MetroFramework.Controls.MetroTabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.fraSettings = new System.Windows.Forms.GroupBox();
            this.chkHexC = new DarkUI.Controls.DarkCheckBox();
            this.chkHexB = new DarkUI.Controls.DarkCheckBox();
            this.chkHexA = new DarkUI.Controls.DarkCheckBox();
            this.chkHexFlags = new DarkUI.Controls.DarkCheckBox();
            this.lblC = new DarkUI.Controls.DarkLabel();
            this.numC = new DarkUI.Controls.DarkNumericUpDown();
            this.lblB = new DarkUI.Controls.DarkLabel();
            this.lblA = new DarkUI.Controls.DarkLabel();
            this.lblFlags = new DarkUI.Controls.DarkLabel();
            this.numB = new DarkUI.Controls.DarkNumericUpDown();
            this.numA = new DarkUI.Controls.DarkNumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)(this.numC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
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
            this.numType.Size = new System.Drawing.Size(95, 21);
            this.numType.TabIndex = 1;
            this.numType.ValueChanged += new System.EventHandler(this.numType_ValueChanged);
            // 
            // fraType
            // 
            this.fraType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraType.Controls.Add(this.lblCodeName);
            this.fraType.Controls.Add(this.numType);
            this.fraType.ForeColor = System.Drawing.SystemColors.Window;
            this.fraType.Location = new System.Drawing.Point(217, 3);
            this.fraType.Name = "fraType";
            this.fraType.Size = new System.Drawing.Size(109, 62);
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
            this.lblCodeName.Size = new System.Drawing.Size(99, 18);
            this.lblCodeName.TabIndex = 9;
            this.lblCodeName.Text = "GOOLC";
            this.lblCodeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fraSubtype
            // 
            this.fraSubtype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSubtype.Controls.Add(this.numSubtype);
            this.fraSubtype.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSubtype.Location = new System.Drawing.Point(217, 66);
            this.fraSubtype.Name = "fraSubtype";
            this.fraSubtype.Size = new System.Drawing.Size(109, 45);
            this.fraSubtype.TabIndex = 5;
            this.fraSubtype.TabStop = false;
            this.fraSubtype.Text = "Subtype";
            // 
            // numSubtype
            // 
            this.numSubtype.Location = new System.Drawing.Point(6, 18);
            this.numSubtype.Name = "numSubtype";
            this.numSubtype.Size = new System.Drawing.Size(95, 21);
            this.numSubtype.TabIndex = 1;
            this.numSubtype.ValueChanged += new System.EventHandler(this.numSubtype_ValueChanged);
            // 
            // fraPosition
            // 
            this.fraPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraPosition.Controls.Add(this.cmdInterpolate);
            this.fraPosition.Controls.Add(this.lblPositionIndex);
            this.fraPosition.Controls.Add(this.cmdNextPosition);
            this.fraPosition.Controls.Add(this.cmdPreviousPosition);
            this.fraPosition.Controls.Add(this.cmdInsertPosition);
            this.fraPosition.Controls.Add(this.lblZ);
            this.fraPosition.Controls.Add(this.cmdRemovePosition);
            this.fraPosition.Controls.Add(this.lblY);
            this.fraPosition.Controls.Add(this.cmdAppendPosition);
            this.fraPosition.Controls.Add(this.lblX);
            this.fraPosition.Controls.Add(this.numZ);
            this.fraPosition.Controls.Add(this.numY);
            this.fraPosition.Controls.Add(this.numX);
            this.fraPosition.ForeColor = System.Drawing.SystemColors.Window;
            this.fraPosition.Location = new System.Drawing.Point(3, 3);
            this.fraPosition.Name = "fraPosition";
            this.fraPosition.Size = new System.Drawing.Size(208, 156);
            this.fraPosition.TabIndex = 1;
            this.fraPosition.TabStop = false;
            this.fraPosition.Text = "Position(s)";
            this.fraPosition.Enter += new System.EventHandler(this.FraPosition_Enter);
            // 
            // cmdInterpolate
            // 
            this.cmdInterpolate.Location = new System.Drawing.Point(9, 123);
            this.cmdInterpolate.Name = "cmdInterpolate";
            this.cmdInterpolate.Padding = new System.Windows.Forms.Padding(5);
            this.cmdInterpolate.Size = new System.Drawing.Size(75, 21);
            this.cmdInterpolate.TabIndex = 8;
            this.cmdInterpolate.Text = "Interpolate";
            this.cmdInterpolate.Click += new System.EventHandler(this.cmdInterpolate_Click);
            // 
            // lblPositionIndex
            // 
            this.lblPositionIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositionIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblPositionIndex.Location = new System.Drawing.Point(6, 18);
            this.lblPositionIndex.Name = "lblPositionIndex";
            this.lblPositionIndex.Size = new System.Drawing.Size(60, 21);
            this.lblPositionIndex.TabIndex = 5;
            this.lblPositionIndex.Text = "?? / ??";
            this.lblPositionIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdNextPosition
            // 
            this.cmdNextPosition.Location = new System.Drawing.Point(136, 18);
            this.cmdNextPosition.Name = "cmdNextPosition";
            this.cmdNextPosition.Padding = new System.Windows.Forms.Padding(5);
            this.cmdNextPosition.Size = new System.Drawing.Size(58, 21);
            this.cmdNextPosition.TabIndex = 1;
            this.cmdNextPosition.Text = "Next";
            this.cmdNextPosition.Click += new System.EventHandler(this.cmdNextPosition_Click);
            // 
            // cmdPreviousPosition
            // 
            this.cmdPreviousPosition.Location = new System.Drawing.Point(72, 18);
            this.cmdPreviousPosition.Name = "cmdPreviousPosition";
            this.cmdPreviousPosition.Padding = new System.Windows.Forms.Padding(5);
            this.cmdPreviousPosition.Size = new System.Drawing.Size(58, 21);
            this.cmdPreviousPosition.TabIndex = 0;
            this.cmdPreviousPosition.Text = "Previous";
            this.cmdPreviousPosition.Click += new System.EventHandler(this.cmdPreviousPosition_Click);
            // 
            // cmdInsertPosition
            // 
            this.cmdInsertPosition.Location = new System.Drawing.Point(126, 72);
            this.cmdInsertPosition.Name = "cmdInsertPosition";
            this.cmdInsertPosition.Padding = new System.Windows.Forms.Padding(5);
            this.cmdInsertPosition.Size = new System.Drawing.Size(75, 21);
            this.cmdInsertPosition.TabIndex = 6;
            this.cmdInsertPosition.Text = "Insert";
            this.cmdInsertPosition.Click += new System.EventHandler(this.cmdInsertPosition_Click);
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblZ.Location = new System.Drawing.Point(6, 98);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 15);
            this.lblZ.TabIndex = 5;
            this.lblZ.Text = "Z";
            // 
            // cmdRemovePosition
            // 
            this.cmdRemovePosition.Location = new System.Drawing.Point(126, 96);
            this.cmdRemovePosition.Name = "cmdRemovePosition";
            this.cmdRemovePosition.Padding = new System.Windows.Forms.Padding(5);
            this.cmdRemovePosition.Size = new System.Drawing.Size(75, 21);
            this.cmdRemovePosition.TabIndex = 7;
            this.cmdRemovePosition.Text = "Remove";
            this.cmdRemovePosition.Click += new System.EventHandler(this.cmdRemovePosition_Click);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblY.Location = new System.Drawing.Point(6, 74);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 15);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y";
            // 
            // cmdAppendPosition
            // 
            this.cmdAppendPosition.Location = new System.Drawing.Point(126, 48);
            this.cmdAppendPosition.Name = "cmdAppendPosition";
            this.cmdAppendPosition.Padding = new System.Windows.Forms.Padding(5);
            this.cmdAppendPosition.Size = new System.Drawing.Size(75, 21);
            this.cmdAppendPosition.TabIndex = 5;
            this.cmdAppendPosition.Text = "Append";
            this.cmdAppendPosition.Click += new System.EventHandler(this.cmdAppendPosition_Click);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblX.Location = new System.Drawing.Point(6, 50);
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
            this.numZ.Location = new System.Drawing.Point(26, 96);
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
            this.numY.Location = new System.Drawing.Point(26, 72);
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
            this.numX.Location = new System.Drawing.Point(26, 48);
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
            this.fraID.Location = new System.Drawing.Point(217, 114);
            this.fraID.Name = "fraID";
            this.fraID.Size = new System.Drawing.Size(109, 45);
            this.fraID.TabIndex = 3;
            this.fraID.TabStop = false;
            this.fraID.Text = "ID";
            this.fraID.Enter += new System.EventHandler(this.FraID_Enter);
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
            this.numID.Size = new System.Drawing.Size(95, 21);
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
            this.tabGeneral.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabGeneral.Location = new System.Drawing.Point(4, 38);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(390, 377);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // fraSettings
            // 
            this.fraSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.fraSettings.Controls.Add(this.chkHexC);
            this.fraSettings.Controls.Add(this.chkHexB);
            this.fraSettings.Controls.Add(this.chkHexA);
            this.fraSettings.Controls.Add(this.chkHexFlags);
            this.fraSettings.Controls.Add(this.lblC);
            this.fraSettings.Controls.Add(this.numC);
            this.fraSettings.Controls.Add(this.lblB);
            this.fraSettings.Controls.Add(this.lblA);
            this.fraSettings.Controls.Add(this.lblFlags);
            this.fraSettings.Controls.Add(this.numB);
            this.fraSettings.Controls.Add(this.numA);
            this.fraSettings.Controls.Add(this.numFlags);
            this.fraSettings.ForeColor = System.Drawing.SystemColors.Window;
            this.fraSettings.Location = new System.Drawing.Point(3, 163);
            this.fraSettings.Name = "fraSettings";
            this.fraSettings.Size = new System.Drawing.Size(208, 123);
            this.fraSettings.TabIndex = 8;
            this.fraSettings.TabStop = false;
            this.fraSettings.Text = "Settings";
            // 
            // chkHexC
            // 
            this.chkHexC.AutoSize = true;
            this.chkHexC.Location = new System.Drawing.Point(154, 90);
            this.chkHexC.Name = "chkHexC";
            this.chkHexC.Size = new System.Drawing.Size(47, 19);
            this.chkHexC.TabIndex = 11;
            this.chkHexC.Text = "Hex";
            this.chkHexC.CheckedChanged += new System.EventHandler(this.chkHexC_CheckedChanged);
            // 
            // chkHexB
            // 
            this.chkHexB.AutoSize = true;
            this.chkHexB.Location = new System.Drawing.Point(154, 66);
            this.chkHexB.Name = "chkHexB";
            this.chkHexB.Size = new System.Drawing.Size(47, 19);
            this.chkHexB.TabIndex = 10;
            this.chkHexB.Text = "Hex";
            this.chkHexB.CheckedChanged += new System.EventHandler(this.chkHexB_CheckedChanged);
            // 
            // chkHexA
            // 
            this.chkHexA.AutoSize = true;
            this.chkHexA.Location = new System.Drawing.Point(154, 42);
            this.chkHexA.Name = "chkHexA";
            this.chkHexA.Size = new System.Drawing.Size(47, 19);
            this.chkHexA.TabIndex = 9;
            this.chkHexA.Text = "Hex";
            this.chkHexA.CheckedChanged += new System.EventHandler(this.chkHexA_CheckedChanged);
            // 
            // chkHexFlags
            // 
            this.chkHexFlags.AutoSize = true;
            this.chkHexFlags.Checked = true;
            this.chkHexFlags.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHexFlags.Location = new System.Drawing.Point(154, 18);
            this.chkHexFlags.Name = "chkHexFlags";
            this.chkHexFlags.Size = new System.Drawing.Size(47, 19);
            this.chkHexFlags.TabIndex = 8;
            this.chkHexFlags.Text = "Hex";
            this.chkHexFlags.CheckedChanged += new System.EventHandler(this.chkHexUnknown_CheckedChanged);
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblC.Location = new System.Drawing.Point(6, 91);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(50, 15);
            this.lblC.TabIndex = 7;
            this.lblC.Text = "Vector Z";
            // 
            // numC
            // 
            this.numC.Location = new System.Drawing.Point(62, 90);
            this.numC.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numC.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numC.Name = "numC";
            this.numC.Size = new System.Drawing.Size(86, 21);
            this.numC.TabIndex = 6;
            this.numC.ValueChanged += new System.EventHandler(this.numC_ValueChanged);
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblB.Location = new System.Drawing.Point(6, 67);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(50, 15);
            this.lblB.TabIndex = 5;
            this.lblB.Text = "Vector Y";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblA.Location = new System.Drawing.Point(6, 43);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(50, 15);
            this.lblA.TabIndex = 4;
            this.lblA.Text = "Vector X";
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
            // numB
            // 
            this.numB.Location = new System.Drawing.Point(62, 66);
            this.numB.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numB.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(86, 21);
            this.numB.TabIndex = 4;
            this.numB.ValueChanged += new System.EventHandler(this.numB_ValueChanged);
            // 
            // numA
            // 
            this.numA.Location = new System.Drawing.Point(62, 42);
            this.numA.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numA.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(86, 21);
            this.numA.TabIndex = 3;
            this.numA.ValueChanged += new System.EventHandler(this.numA_ValueChanged);
            // 
            // numFlags
            // 
            this.numFlags.Hexadecimal = true;
            this.numFlags.Location = new System.Drawing.Point(62, 18);
            this.numFlags.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numFlags.Name = "numFlags";
            this.numFlags.Size = new System.Drawing.Size(86, 21);
            this.numFlags.TabIndex = 2;
            this.numFlags.ValueChanged += new System.EventHandler(this.numUnknown_ValueChanged);
            // 
            // OldEntityBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcTabs);
            this.Name = "OldEntityBox";
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
            ((System.ComponentModel.ISupportInitialize)(this.numC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
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
        private DarkUI.Controls.DarkButton cmdInsertPosition;
        private DarkUI.Controls.DarkButton cmdRemovePosition;
        private DarkUI.Controls.DarkButton cmdAppendPosition;
        private DarkUI.Controls.DarkButton cmdNextPosition;
        private DarkUI.Controls.DarkButton cmdPreviousPosition;
        private DarkUI.Controls.DarkLabel lblPositionIndex;
        private System.Windows.Forms.GroupBox fraID;
        private DarkUI.Controls.DarkNumericUpDown numID;
        private MetroFramework.Controls.MetroTabControl tbcTabs;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox fraSettings;
        private DarkUI.Controls.DarkLabel lblC;
        private DarkUI.Controls.DarkNumericUpDown numC;
        private DarkUI.Controls.DarkLabel lblB;
        private DarkUI.Controls.DarkLabel lblA;
        private DarkUI.Controls.DarkLabel lblFlags;
        private DarkUI.Controls.DarkNumericUpDown numB;
        private DarkUI.Controls.DarkNumericUpDown numA;
        private DarkUI.Controls.DarkNumericUpDown numFlags;
        private DarkUI.Controls.DarkLabel lblCodeName;
        private DarkUI.Controls.DarkCheckBox chkHexC;
        private DarkUI.Controls.DarkCheckBox chkHexB;
        private DarkUI.Controls.DarkCheckBox chkHexA;
        private DarkUI.Controls.DarkCheckBox chkHexFlags;
        private DarkUI.Controls.DarkButton cmdInterpolate;
    }
}
