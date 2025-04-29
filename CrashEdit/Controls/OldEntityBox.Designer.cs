using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
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
            components = new System.ComponentModel.Container();
            numType = new DarkNumericUpDown();
            fraType = new DarkGroupBox();
            lblCodeName = new Label();
            fraSubtype = new DarkGroupBox();
            numSubtype = new DarkNumericUpDown();
            fraPosition = new DarkGroupBox();
            cmdInterpolate = new DarkButton();
            lblPositionIndex = new Label();
            cmdNextPosition = new DarkButton();
            cmdPreviousPosition = new DarkButton();
            cmdInsertPosition = new DarkButton();
            lblZ = new Label();
            cmdRemovePosition = new DarkButton();
            lblY = new Label();
            cmdAppendPosition = new DarkButton();
            lblX = new Label();
            numZ = new DarkNumericUpDown();
            numY = new DarkNumericUpDown();
            numX = new DarkNumericUpDown();
            fraID = new DarkGroupBox();
            numID = new DarkNumericUpDown();
            tbcTabs = new MetroSetTabControl();
            tabGeneral = new TabPage();
            fraSpawn = new DarkGroupBox();
            numSpawn = new DarkNumericUpDown();
            fraSettings = new DarkGroupBox();
            chkHexC = new CheckBox();
            chkHexB = new CheckBox();
            chkHexA = new CheckBox();
            chkHexFlags = new CheckBox();
            lblC = new Label();
            numC = new DarkNumericUpDown();
            lblB = new Label();
            lblA = new Label();
            lblFlags = new Label();
            numB = new DarkNumericUpDown();
            numA = new DarkNumericUpDown();
            numFlags = new DarkNumericUpDown();
            tipHover = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)numType).BeginInit();
            fraType.SuspendLayout();
            fraSubtype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSubtype).BeginInit();
            fraPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            fraID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numID).BeginInit();
            tbcTabs.SuspendLayout();
            tabGeneral.SuspendLayout();
            fraSpawn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSpawn).BeginInit();
            fraSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFlags).BeginInit();
            SuspendLayout();
            // 
            // numType
            // 
            numType.Location = new Point(7, 25);
            numType.Margin = new Padding(4, 3, 4, 3);
            numType.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numType.Name = "numType";
            numType.Size = new Size(90, 23);
            numType.TabIndex = 1;
            numType.ValueChanged += numType_ValueChanged;
            // 
            // fraType
            // 
            fraType.Controls.Add(lblCodeName);
            fraType.Controls.Add(numType);
            fraType.Location = new Point(244, 3);
            fraType.Margin = new Padding(4, 3, 4, 3);
            fraType.Name = "fraType";
            fraType.Padding = new Padding(4, 3, 4, 3);
            fraType.Size = new Size(104, 77);
            fraType.TabIndex = 4;
            fraType.TabStop = false;
            fraType.Text = "Type";
            // 
            // lblCodeName
            // 
            lblCodeName.BackColor = Color.Transparent;
            lblCodeName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodeName.Location = new Point(2, 52);
            lblCodeName.Margin = new Padding(4, 0, 4, 0);
            lblCodeName.Name = "lblCodeName";
            lblCodeName.Size = new Size(140, 22);
            lblCodeName.TabIndex = 9;
            lblCodeName.Text = "(Unknown)";
            lblCodeName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fraSubtype
            // 
            fraSubtype.Controls.Add(numSubtype);
            fraSubtype.Location = new Point(244, 88);
            fraSubtype.Margin = new Padding(4, 3, 4, 3);
            fraSubtype.Name = "fraSubtype";
            fraSubtype.Padding = new Padding(4, 3, 4, 3);
            fraSubtype.Size = new Size(104, 53);
            fraSubtype.TabIndex = 5;
            fraSubtype.TabStop = false;
            fraSubtype.Text = "Subtype";
            // 
            // numSubtype
            // 
            numSubtype.Location = new Point(7, 23);
            numSubtype.Margin = new Padding(4, 3, 4, 3);
            numSubtype.Name = "numSubtype";
            numSubtype.Size = new Size(90, 23);
            numSubtype.TabIndex = 1;
            numSubtype.ValueChanged += numSubtype_ValueChanged;
            // 
            // fraPosition
            // 
            fraPosition.AutoSize = true;
            fraPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPosition.Controls.Add(cmdInterpolate);
            fraPosition.Controls.Add(lblPositionIndex);
            fraPosition.Controls.Add(cmdNextPosition);
            fraPosition.Controls.Add(cmdPreviousPosition);
            fraPosition.Controls.Add(cmdInsertPosition);
            fraPosition.Controls.Add(lblZ);
            fraPosition.Controls.Add(cmdRemovePosition);
            fraPosition.Controls.Add(lblY);
            fraPosition.Controls.Add(cmdAppendPosition);
            fraPosition.Controls.Add(lblX);
            fraPosition.Controls.Add(numZ);
            fraPosition.Controls.Add(numY);
            fraPosition.Controls.Add(numX);
            fraPosition.Location = new Point(4, 3);
            fraPosition.Margin = new Padding(4, 3, 4, 3);
            fraPosition.Name = "fraPosition";
            fraPosition.Padding = new Padding(4, 3, 4, 3);
            fraPosition.Size = new Size(235, 199);
            fraPosition.TabIndex = 1;
            fraPosition.TabStop = false;
            fraPosition.Text = "Position(s)";
            // 
            // cmdInterpolate
            // 
            cmdInterpolate.BorderColour = Color.Empty;
            cmdInterpolate.CustomColour = false;
            cmdInterpolate.FlatBottom = false;
            cmdInterpolate.FlatTop = false;
            cmdInterpolate.Location = new Point(7, 150);
            cmdInterpolate.Margin = new Padding(4, 3, 4, 3);
            cmdInterpolate.Name = "cmdInterpolate";
            cmdInterpolate.Padding = new Padding(6);
            cmdInterpolate.Size = new Size(88, 27);
            cmdInterpolate.TabIndex = 8;
            cmdInterpolate.Text = "Interpolate";
            cmdInterpolate.Click += cmdInterpolate_Click;
            // 
            // lblPositionIndex
            // 
            lblPositionIndex.BackColor = Color.Transparent;
            lblPositionIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPositionIndex.Location = new Point(7, 22);
            lblPositionIndex.Margin = new Padding(4, 0, 4, 0);
            lblPositionIndex.Name = "lblPositionIndex";
            lblPositionIndex.Size = new Size(70, 27);
            lblPositionIndex.TabIndex = 5;
            lblPositionIndex.Text = "?? / ??";
            lblPositionIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdNextPosition
            // 
            cmdNextPosition.BorderColour = Color.Empty;
            cmdNextPosition.CustomColour = false;
            cmdNextPosition.FlatBottom = false;
            cmdNextPosition.FlatTop = false;
            cmdNextPosition.Location = new Point(159, 22);
            cmdNextPosition.Margin = new Padding(4, 3, 4, 3);
            cmdNextPosition.Name = "cmdNextPosition";
            cmdNextPosition.Padding = new Padding(6);
            cmdNextPosition.Size = new Size(68, 27);
            cmdNextPosition.TabIndex = 1;
            cmdNextPosition.Text = "Next";
            cmdNextPosition.Click += cmdNextPosition_Click;
            // 
            // cmdPreviousPosition
            // 
            cmdPreviousPosition.BorderColour = Color.Empty;
            cmdPreviousPosition.CustomColour = false;
            cmdPreviousPosition.FlatBottom = false;
            cmdPreviousPosition.FlatTop = false;
            cmdPreviousPosition.Location = new Point(84, 22);
            cmdPreviousPosition.Margin = new Padding(4, 3, 4, 3);
            cmdPreviousPosition.Name = "cmdPreviousPosition";
            cmdPreviousPosition.Padding = new Padding(6);
            cmdPreviousPosition.Size = new Size(68, 27);
            cmdPreviousPosition.TabIndex = 0;
            cmdPreviousPosition.Text = "Previous";
            cmdPreviousPosition.Click += cmdPreviousPosition_Click;
            // 
            // cmdInsertPosition
            // 
            cmdInsertPosition.BorderColour = Color.Empty;
            cmdInsertPosition.CustomColour = false;
            cmdInsertPosition.FlatBottom = false;
            cmdInsertPosition.FlatTop = false;
            cmdInsertPosition.Location = new Point(139, 87);
            cmdInsertPosition.Margin = new Padding(4, 3, 4, 3);
            cmdInsertPosition.Name = "cmdInsertPosition";
            cmdInsertPosition.Padding = new Padding(6);
            cmdInsertPosition.Size = new Size(88, 27);
            cmdInsertPosition.TabIndex = 6;
            cmdInsertPosition.Text = "Insert";
            cmdInsertPosition.Click += cmdInsertPosition_Click;
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.BackColor = Color.Transparent;
            lblZ.Location = new Point(7, 122);
            lblZ.Margin = new Padding(4, 0, 4, 0);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(14, 15);
            lblZ.TabIndex = 5;
            lblZ.Text = "Z";
            // 
            // cmdRemovePosition
            // 
            cmdRemovePosition.BorderColour = Color.Empty;
            cmdRemovePosition.CustomColour = false;
            cmdRemovePosition.FlatBottom = false;
            cmdRemovePosition.FlatTop = false;
            cmdRemovePosition.Location = new Point(139, 117);
            cmdRemovePosition.Margin = new Padding(4, 3, 4, 3);
            cmdRemovePosition.Name = "cmdRemovePosition";
            cmdRemovePosition.Padding = new Padding(6);
            cmdRemovePosition.Size = new Size(88, 27);
            cmdRemovePosition.TabIndex = 7;
            cmdRemovePosition.Text = "Remove";
            cmdRemovePosition.Click += cmdRemovePosition_Click;
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.BackColor = Color.Transparent;
            lblY.Location = new Point(7, 92);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 4;
            lblY.Text = "Y";
            // 
            // cmdAppendPosition
            // 
            cmdAppendPosition.BorderColour = Color.Empty;
            cmdAppendPosition.CustomColour = false;
            cmdAppendPosition.FlatBottom = false;
            cmdAppendPosition.FlatTop = false;
            cmdAppendPosition.Location = new Point(139, 57);
            cmdAppendPosition.Margin = new Padding(4, 3, 4, 3);
            cmdAppendPosition.Name = "cmdAppendPosition";
            cmdAppendPosition.Padding = new Padding(6);
            cmdAppendPosition.Size = new Size(88, 27);
            cmdAppendPosition.TabIndex = 5;
            cmdAppendPosition.Text = "Append";
            cmdAppendPosition.Click += cmdAppendPosition_Click;
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.Location = new Point(7, 62);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 3;
            lblX.Text = "X";
            // 
            // numZ
            // 
            numZ.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numZ.Location = new Point(30, 120);
            numZ.Margin = new Padding(4, 3, 4, 3);
            numZ.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numZ.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numZ.Name = "numZ";
            numZ.Size = new Size(100, 23);
            numZ.TabIndex = 4;
            numZ.ValueChanged += numZ_ValueChanged;
            // 
            // numY
            // 
            numY.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numY.Location = new Point(30, 90);
            numY.Margin = new Padding(4, 3, 4, 3);
            numY.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numY.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numY.Name = "numY";
            numY.Size = new Size(100, 23);
            numY.TabIndex = 3;
            numY.ValueChanged += numY_ValueChanged;
            // 
            // numX
            // 
            numX.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numX.Location = new Point(30, 60);
            numX.Margin = new Padding(4, 3, 4, 3);
            numX.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numX.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numX.Name = "numX";
            numX.Size = new Size(100, 23);
            numX.TabIndex = 2;
            numX.ValueChanged += numX_ValueChanged;
            // 
            // fraID
            // 
            fraID.Controls.Add(numID);
            fraID.Location = new Point(244, 148);
            fraID.Margin = new Padding(4, 3, 4, 3);
            fraID.Name = "fraID";
            fraID.Padding = new Padding(4, 3, 4, 3);
            fraID.Size = new Size(104, 57);
            fraID.TabIndex = 3;
            fraID.TabStop = false;
            fraID.Text = "ID";
            // 
            // numID
            // 
            numID.Location = new Point(7, 22);
            numID.Margin = new Padding(4, 3, 4, 3);
            numID.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numID.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numID.Name = "numID";
            numID.Size = new Size(90, 23);
            numID.TabIndex = 1;
            numID.ValueChanged += numID_ValueChanged;
            // 
            // tbcTabs
            // 
            tbcTabs.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tbcTabs.AnimateTime = 200;
            tbcTabs.BackgroundColor = Color.FromArgb(31, 31, 32);
            tbcTabs.Controls.Add(tabGeneral);
            tbcTabs.Dock = DockStyle.Fill;
            tbcTabs.IsDerivedStyle = false;
            tbcTabs.ItemSize = new Size(100, 28);
            tbcTabs.Location = new Point(0, 0);
            tbcTabs.Margin = new Padding(4, 3, 4, 3);
            tbcTabs.Name = "tbcTabs";
            tbcTabs.SelectedIndex = 0;
            tbcTabs.SelectedTextColor = Color.White;
            tbcTabs.Size = new Size(407, 427);
            tbcTabs.SizeMode = TabSizeMode.Fixed;
            tbcTabs.Speed = 100;
            tbcTabs.Style = MetroSet_UI.Enums.Style.Dark;
            tbcTabs.StyleManager = null;
            tbcTabs.TabIndex = 7;
            tbcTabs.ThemeAuthor = "Narwin";
            tbcTabs.ThemeName = "MetroDark";
            tbcTabs.UnselectedTextColor = Color.Gray;
            tbcTabs.UseAnimation = false;
            // 
            // tabGeneral
            // 
            tabGeneral.AutoScroll = true;
            tabGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tabGeneral.Controls.Add(fraSpawn);
            tabGeneral.Controls.Add(fraSettings);
            tabGeneral.Controls.Add(fraType);
            tabGeneral.Controls.Add(fraSubtype);
            tabGeneral.Controls.Add(fraPosition);
            tabGeneral.Controls.Add(fraID);
            tabGeneral.Location = new Point(4, 32);
            tabGeneral.Margin = new Padding(4, 3, 4, 3);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Size = new Size(399, 391);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // fraSpawn
            // 
            fraSpawn.Controls.Add(numSpawn);
            fraSpawn.Location = new Point(244, 211);
            fraSpawn.Margin = new Padding(4, 3, 4, 3);
            fraSpawn.Name = "fraSpawn";
            fraSpawn.Padding = new Padding(4, 3, 4, 3);
            fraSpawn.Size = new Size(104, 57);
            fraSpawn.TabIndex = 4;
            fraSpawn.TabStop = false;
            fraSpawn.Text = "Spawn [?]";
            tipHover.SetToolTip(fraSpawn, "Must be set to 3, or entity will not spawn!");
            // 
            // numSpawn
            // 
            numSpawn.Location = new Point(7, 22);
            numSpawn.Margin = new Padding(4, 3, 4, 3);
            numSpawn.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numSpawn.Name = "numSpawn";
            numSpawn.Size = new Size(90, 23);
            numSpawn.TabIndex = 12;
            numSpawn.ValueChanged += numSpawn_ValueChanged;
            // 
            // fraSettings
            // 
            fraSettings.Controls.Add(chkHexC);
            fraSettings.Controls.Add(chkHexB);
            fraSettings.Controls.Add(chkHexA);
            fraSettings.Controls.Add(chkHexFlags);
            fraSettings.Controls.Add(lblC);
            fraSettings.Controls.Add(numC);
            fraSettings.Controls.Add(lblB);
            fraSettings.Controls.Add(lblA);
            fraSettings.Controls.Add(lblFlags);
            fraSettings.Controls.Add(numB);
            fraSettings.Controls.Add(numA);
            fraSettings.Controls.Add(numFlags);
            fraSettings.Location = new Point(4, 209);
            fraSettings.Margin = new Padding(4, 3, 4, 3);
            fraSettings.Name = "fraSettings";
            fraSettings.Padding = new Padding(4, 3, 4, 3);
            fraSettings.Size = new Size(233, 153);
            fraSettings.TabIndex = 8;
            fraSettings.TabStop = false;
            fraSettings.Text = "Settings";
            // 
            // chkHexC
            // 
            chkHexC.AutoSize = true;
            chkHexC.BackColor = Color.Transparent;
            chkHexC.Location = new Point(180, 113);
            chkHexC.Margin = new Padding(4, 3, 4, 3);
            chkHexC.Name = "chkHexC";
            chkHexC.Size = new Size(47, 19);
            chkHexC.TabIndex = 11;
            chkHexC.Text = "Hex";
            chkHexC.UseVisualStyleBackColor = false;
            chkHexC.CheckedChanged += chkHexC_CheckedChanged;
            // 
            // chkHexB
            // 
            chkHexB.AutoSize = true;
            chkHexB.BackColor = Color.Transparent;
            chkHexB.Location = new Point(180, 83);
            chkHexB.Margin = new Padding(4, 3, 4, 3);
            chkHexB.Name = "chkHexB";
            chkHexB.Size = new Size(47, 19);
            chkHexB.TabIndex = 10;
            chkHexB.Text = "Hex";
            chkHexB.UseVisualStyleBackColor = false;
            chkHexB.CheckedChanged += chkHexB_CheckedChanged;
            // 
            // chkHexA
            // 
            chkHexA.AutoSize = true;
            chkHexA.BackColor = Color.Transparent;
            chkHexA.Location = new Point(180, 53);
            chkHexA.Margin = new Padding(4, 3, 4, 3);
            chkHexA.Name = "chkHexA";
            chkHexA.Size = new Size(47, 19);
            chkHexA.TabIndex = 9;
            chkHexA.Text = "Hex";
            chkHexA.UseVisualStyleBackColor = false;
            chkHexA.CheckedChanged += chkHexA_CheckedChanged;
            // 
            // chkHexFlags
            // 
            chkHexFlags.AutoSize = true;
            chkHexFlags.BackColor = Color.Transparent;
            chkHexFlags.Checked = true;
            chkHexFlags.CheckState = CheckState.Checked;
            chkHexFlags.Location = new Point(180, 23);
            chkHexFlags.Margin = new Padding(4, 3, 4, 3);
            chkHexFlags.Name = "chkHexFlags";
            chkHexFlags.Size = new Size(47, 19);
            chkHexFlags.TabIndex = 8;
            chkHexFlags.Text = "Hex";
            chkHexFlags.UseVisualStyleBackColor = false;
            chkHexFlags.CheckedChanged += chkHexUnknown_CheckedChanged;
            // 
            // lblC
            // 
            lblC.AutoSize = true;
            lblC.BackColor = Color.Transparent;
            lblC.Location = new Point(7, 114);
            lblC.Margin = new Padding(4, 0, 4, 0);
            lblC.Name = "lblC";
            lblC.Size = new Size(50, 15);
            lblC.TabIndex = 7;
            lblC.Text = "Vector Z";
            // 
            // numC
            // 
            numC.Location = new Point(72, 112);
            numC.Margin = new Padding(4, 3, 4, 3);
            numC.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numC.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numC.Name = "numC";
            numC.Size = new Size(100, 23);
            numC.TabIndex = 6;
            numC.ValueChanged += numC_ValueChanged;
            // 
            // lblB
            // 
            lblB.AutoSize = true;
            lblB.BackColor = Color.Transparent;
            lblB.Location = new Point(7, 84);
            lblB.Margin = new Padding(4, 0, 4, 0);
            lblB.Name = "lblB";
            lblB.Size = new Size(50, 15);
            lblB.TabIndex = 5;
            lblB.Text = "Vector Y";
            // 
            // lblA
            // 
            lblA.AutoSize = true;
            lblA.BackColor = Color.Transparent;
            lblA.Location = new Point(7, 54);
            lblA.Margin = new Padding(4, 0, 4, 0);
            lblA.Name = "lblA";
            lblA.Size = new Size(50, 15);
            lblA.TabIndex = 4;
            lblA.Text = "Vector X";
            // 
            // lblFlags
            // 
            lblFlags.AutoSize = true;
            lblFlags.BackColor = Color.Transparent;
            lblFlags.Location = new Point(7, 24);
            lblFlags.Margin = new Padding(4, 0, 4, 0);
            lblFlags.Name = "lblFlags";
            lblFlags.Size = new Size(34, 15);
            lblFlags.TabIndex = 3;
            lblFlags.Text = "Flags";
            // 
            // numB
            // 
            numB.Location = new Point(72, 82);
            numB.Margin = new Padding(4, 3, 4, 3);
            numB.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numB.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numB.Name = "numB";
            numB.Size = new Size(100, 23);
            numB.TabIndex = 4;
            numB.ValueChanged += numB_ValueChanged;
            // 
            // numA
            // 
            numA.Location = new Point(72, 52);
            numA.Margin = new Padding(4, 3, 4, 3);
            numA.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numA.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numA.Name = "numA";
            numA.Size = new Size(100, 23);
            numA.TabIndex = 3;
            numA.ValueChanged += numA_ValueChanged;
            // 
            // numFlags
            // 
            numFlags.Hexadecimal = true;
            numFlags.Location = new Point(72, 22);
            numFlags.Margin = new Padding(4, 3, 4, 3);
            numFlags.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numFlags.Name = "numFlags";
            numFlags.Size = new Size(100, 23);
            numFlags.TabIndex = 2;
            numFlags.ValueChanged += numUnknown_ValueChanged;
            // 
            // tipHover
            // 
            tipHover.AutomaticDelay = 250;
            // 
            // OldEntityBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbcTabs);
            Margin = new Padding(4, 3, 4, 3);
            Name = "OldEntityBox";
            Size = new Size(407, 427);
            ((System.ComponentModel.ISupportInitialize)numType).EndInit();
            fraType.ResumeLayout(false);
            fraSubtype.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSubtype).EndInit();
            fraPosition.ResumeLayout(false);
            fraPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            fraID.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numID).EndInit();
            tbcTabs.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            fraSpawn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSpawn).EndInit();
            fraSettings.ResumeLayout(false);
            fraSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numC).EndInit();
            ((System.ComponentModel.ISupportInitialize)numB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numA).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFlags).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DarkNumericUpDown numType;
        private DarkGroupBox fraType;
        private DarkGroupBox fraSubtype;
        private DarkNumericUpDown numSubtype;
        private DarkGroupBox fraPosition;
        private Label lblZ;
        private Label lblY;
        private Label lblX;
        private DarkNumericUpDown numZ;
        private DarkNumericUpDown numY;
        private DarkNumericUpDown numX;
        private DarkButton cmdInsertPosition;
        private DarkButton cmdRemovePosition;
        private DarkButton cmdAppendPosition;
        private DarkButton cmdNextPosition;
        private DarkButton cmdPreviousPosition;
        private Label lblPositionIndex;
        private DarkGroupBox fraID;
        private DarkNumericUpDown numID;
        private MetroSetTabControl tbcTabs;
        private TabPage tabGeneral;
        private DarkGroupBox fraSettings;
        private Label lblC;
        private DarkNumericUpDown numC;
        private Label lblB;
        private Label lblA;
        private Label lblFlags;
        private DarkNumericUpDown numB;
        private DarkNumericUpDown numA;
        private DarkNumericUpDown numFlags;
        private Label lblCodeName;
        private CheckBox chkHexC;
        private CheckBox chkHexB;
        private CheckBox chkHexA;
        private CheckBox chkHexFlags;
        private DarkButton cmdInterpolate;
        private DarkNumericUpDown numSpawn;
        private ToolTip tipHover;
        private DarkGroupBox fraSpawn;
    }
}
