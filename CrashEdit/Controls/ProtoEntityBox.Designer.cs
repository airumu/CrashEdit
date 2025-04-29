using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
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
            numType = new DarkNumericUpDown();
            fraType = new DarkGroupBox();
            lblCodeName = new Label();
            fraSubtype = new DarkGroupBox();
            numSubtype = new DarkNumericUpDown();
            fraPosition = new DarkGroupBox();
            lblZ = new Label();
            lblY = new Label();
            lblX = new Label();
            numZ = new DarkNumericUpDown();
            numY = new DarkNumericUpDown();
            numX = new DarkNumericUpDown();
            fraID = new DarkGroupBox();
            numID = new DarkNumericUpDown();
            tbcTabs = new MetroSetTabControl();
            tabGeneral = new TabPage();
            fraSettings = new DarkGroupBox();
            lblModeC = new Label();
            numModeC = new DarkNumericUpDown();
            lblModeB = new Label();
            lblModeA = new Label();
            lblFlags = new Label();
            numModeB = new DarkNumericUpDown();
            numModeA = new DarkNumericUpDown();
            numFlags = new DarkNumericUpDown();
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
            fraSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numModeC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numModeB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numModeA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFlags).BeginInit();
            SuspendLayout();
            // 
            // numType
            // 
            numType.Location = new Point(7, 25);
            numType.Margin = new Padding(4, 3, 4, 3);
            numType.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numType.Name = "numType";
            numType.Size = new Size(140, 23);
            numType.TabIndex = 1;
            numType.ValueChanged += numType_ValueChanged;
            // 
            // fraType
            // 
            fraType.Controls.Add(lblCodeName);
            fraType.Controls.Add(numType);
            fraType.Location = new Point(153, 3);
            fraType.Margin = new Padding(4, 3, 4, 3);
            fraType.Name = "fraType";
            fraType.Padding = new Padding(4, 3, 4, 3);
            fraType.Size = new Size(154, 77);
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
            lblCodeName.Text = "CodeC";
            lblCodeName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fraSubtype
            // 
            fraSubtype.Controls.Add(numSubtype);
            fraSubtype.Location = new Point(153, 88);
            fraSubtype.Margin = new Padding(4, 3, 4, 3);
            fraSubtype.Name = "fraSubtype";
            fraSubtype.Padding = new Padding(4, 3, 4, 3);
            fraSubtype.Size = new Size(154, 53);
            fraSubtype.TabIndex = 5;
            fraSubtype.TabStop = false;
            fraSubtype.Text = "Subtype";
            // 
            // numSubtype
            // 
            numSubtype.Location = new Point(7, 23);
            numSubtype.Margin = new Padding(4, 3, 4, 3);
            numSubtype.Name = "numSubtype";
            numSubtype.Size = new Size(140, 23);
            numSubtype.TabIndex = 1;
            numSubtype.ValueChanged += numSubtype_ValueChanged;
            // 
            // fraPosition
            // 
            fraPosition.Controls.Add(lblZ);
            fraPosition.Controls.Add(lblY);
            fraPosition.Controls.Add(lblX);
            fraPosition.Controls.Add(numZ);
            fraPosition.Controls.Add(numY);
            fraPosition.Controls.Add(numX);
            fraPosition.Location = new Point(4, 3);
            fraPosition.Margin = new Padding(4, 3, 4, 3);
            fraPosition.Name = "fraPosition";
            fraPosition.Padding = new Padding(4, 3, 4, 3);
            fraPosition.Size = new Size(142, 115);
            fraPosition.TabIndex = 1;
            fraPosition.TabStop = false;
            fraPosition.Text = "Start Position";
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.BackColor = Color.Transparent;
            lblZ.Location = new Point(7, 83);
            lblZ.Margin = new Padding(4, 0, 4, 0);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(14, 15);
            lblZ.TabIndex = 5;
            lblZ.Text = "Z";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.BackColor = Color.Transparent;
            lblY.Location = new Point(7, 53);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 4;
            lblY.Text = "Y";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.Location = new Point(7, 23);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 3;
            lblX.Text = "X";
            // 
            // numZ
            // 
            numZ.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numZ.Location = new Point(30, 81);
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
            numY.Location = new Point(30, 51);
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
            numX.Location = new Point(30, 21);
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
            fraID.Location = new Point(4, 126);
            fraID.Margin = new Padding(4, 3, 4, 3);
            fraID.Name = "fraID";
            fraID.Padding = new Padding(4, 3, 4, 3);
            fraID.Size = new Size(142, 57);
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
            numID.Size = new Size(124, 23);
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
            tbcTabs.Size = new Size(464, 524);
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
            tabGeneral.Controls.Add(fraSettings);
            tabGeneral.Controls.Add(fraType);
            tabGeneral.Controls.Add(fraSubtype);
            tabGeneral.Controls.Add(fraPosition);
            tabGeneral.Controls.Add(fraID);
            tabGeneral.Location = new Point(4, 32);
            tabGeneral.Margin = new Padding(4, 3, 4, 3);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Size = new Size(456, 488);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // fraSettings
            // 
            fraSettings.Controls.Add(lblModeC);
            fraSettings.Controls.Add(numModeC);
            fraSettings.Controls.Add(lblModeB);
            fraSettings.Controls.Add(lblModeA);
            fraSettings.Controls.Add(lblFlags);
            fraSettings.Controls.Add(numModeB);
            fraSettings.Controls.Add(numModeA);
            fraSettings.Controls.Add(numFlags);
            fraSettings.Location = new Point(4, 189);
            fraSettings.Margin = new Padding(4, 3, 4, 3);
            fraSettings.Name = "fraSettings";
            fraSettings.Padding = new Padding(4, 3, 4, 3);
            fraSettings.Size = new Size(187, 153);
            fraSettings.TabIndex = 8;
            fraSettings.TabStop = false;
            fraSettings.Text = "Special Settings";
            // 
            // lblModeC
            // 
            lblModeC.AutoSize = true;
            lblModeC.BackColor = Color.Transparent;
            lblModeC.Location = new Point(7, 114);
            lblModeC.Margin = new Padding(4, 0, 4, 0);
            lblModeC.Name = "lblModeC";
            lblModeC.Size = new Size(49, 15);
            lblModeC.TabIndex = 7;
            lblModeC.Text = "Mode C";
            // 
            // numModeC
            // 
            numModeC.Location = new Point(72, 112);
            numModeC.Margin = new Padding(4, 3, 4, 3);
            numModeC.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numModeC.Name = "numModeC";
            numModeC.Size = new Size(100, 23);
            numModeC.TabIndex = 6;
            numModeC.ValueChanged += numD_ValueChanged;
            // 
            // lblModeB
            // 
            lblModeB.AutoSize = true;
            lblModeB.BackColor = Color.Transparent;
            lblModeB.Location = new Point(7, 84);
            lblModeB.Margin = new Padding(4, 0, 4, 0);
            lblModeB.Name = "lblModeB";
            lblModeB.Size = new Size(48, 15);
            lblModeB.TabIndex = 5;
            lblModeB.Text = "Mode B";
            // 
            // lblModeA
            // 
            lblModeA.AutoSize = true;
            lblModeA.BackColor = Color.Transparent;
            lblModeA.Location = new Point(7, 54);
            lblModeA.Margin = new Padding(4, 0, 4, 0);
            lblModeA.Name = "lblModeA";
            lblModeA.Size = new Size(49, 15);
            lblModeA.TabIndex = 4;
            lblModeA.Text = "Mode A";
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
            // numModeB
            // 
            numModeB.Location = new Point(72, 82);
            numModeB.Margin = new Padding(4, 3, 4, 3);
            numModeB.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numModeB.Name = "numModeB";
            numModeB.Size = new Size(100, 23);
            numModeB.TabIndex = 4;
            numModeB.ValueChanged += numC_ValueChanged;
            // 
            // numModeA
            // 
            numModeA.Location = new Point(72, 52);
            numModeA.Margin = new Padding(4, 3, 4, 3);
            numModeA.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numModeA.Name = "numModeA";
            numModeA.Size = new Size(100, 23);
            numModeA.TabIndex = 3;
            numModeA.ValueChanged += numB_ValueChanged;
            // 
            // numFlags
            // 
            numFlags.Location = new Point(72, 22);
            numFlags.Margin = new Padding(4, 3, 4, 3);
            numFlags.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numFlags.Name = "numFlags";
            numFlags.Size = new Size(100, 23);
            numFlags.TabIndex = 2;
            numFlags.ValueChanged += numA_ValueChanged;
            // 
            // ProtoEntityBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbcTabs);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ProtoEntityBox";
            Size = new Size(464, 524);
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
            fraSettings.ResumeLayout(false);
            fraSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numModeC).EndInit();
            ((System.ComponentModel.ISupportInitialize)numModeB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numModeA).EndInit();
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
        private DarkGroupBox fraID;
        private DarkNumericUpDown numID;
        private MetroSetTabControl tbcTabs;
        private TabPage tabGeneral;
        private DarkGroupBox fraSettings;
        private Label lblModeC;
        private DarkNumericUpDown numModeC;
        private Label lblModeB;
        private Label lblModeA;
        private Label lblFlags;
        private DarkNumericUpDown numModeB;
        private DarkNumericUpDown numModeA;
        private DarkNumericUpDown numFlags;
        private Label lblCodeName;
    }
}
