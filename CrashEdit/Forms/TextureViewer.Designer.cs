using System.Windows.Forms;
using AltUI.Controls;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;

namespace CrashEdit.CE
{
    partial class TextureViewer
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
            splitContainer1 = new SplitContainer();
            pictureBox1 = new PictureBox();
            tabControl1 = new MetroSetTabControl();
            tabC1 = new TabPage();
            C1Size64 = new DarkButton();
            C1Size32 = new DarkButton();
            C1Size16 = new DarkButton();
            groupBox5 = new DarkGroupBox();
            C1numY = new DarkNumericUpDown();
            C1numX = new DarkNumericUpDown();
            label5 = new Label();
            label6 = new Label();
            groupBox4 = new DarkGroupBox();
            C1dpdH = new DarkComboBox();
            C1dpdW = new DarkComboBox();
            label3 = new Label();
            label4 = new Label();
            groupBox3 = new DarkGroupBox();
            C1numCY = new DarkNumericUpDown();
            C1numCX = new DarkNumericUpDown();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new DarkGroupBox();
            C1dpdBlend = new DarkComboBox();
            groupBox1 = new DarkGroupBox();
            C1dpdColor = new DarkComboBox();
            tabC2 = new TabPage();
            C2SizeMax = new DarkButton();
            C2Size64 = new DarkButton();
            C2Size32 = new DarkButton();
            C2Size16 = new DarkButton();
            groupBox6 = new DarkGroupBox();
            label14 = new Label();
            label13 = new Label();
            C2numY = new DarkNumericUpDown();
            C2numX = new DarkNumericUpDown();
            label7 = new Label();
            label8 = new Label();
            C2numY2 = new DarkNumericUpDown();
            C2numX2 = new DarkNumericUpDown();
            groupBox7 = new DarkGroupBox();
            C2numH = new DarkNumericUpDown();
            label9 = new Label();
            C2numW = new DarkNumericUpDown();
            label10 = new Label();
            groupBox8 = new DarkGroupBox();
            C2numCY = new DarkNumericUpDown();
            C2numCX = new DarkNumericUpDown();
            label11 = new Label();
            label12 = new Label();
            lblCLUT = new Label();
            groupBox9 = new DarkGroupBox();
            C2dpdBlend = new DarkComboBox();
            groupBox10 = new DarkGroupBox();
            C2dpdColor = new DarkComboBox();
            groupBox11 = new DarkGroupBox();
            C2numMoveX = new DarkNumericUpDown();
            C2btnMoveX1 = new DarkButton();
            C2btnMoveX2 = new DarkButton();
            groupBox12 = new DarkGroupBox();
            C2numMoveY = new DarkNumericUpDown();
            C2btnMoveY1 = new DarkButton();
            C2btnMoveY2 = new DarkButton();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl1.SuspendLayout();
            tabC1.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C1numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C1numX).BeginInit();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C1numCY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C1numCX).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabC2.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numX2).BeginInit();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numW).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numCY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numCX).BeginInit();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numMoveX).BeginInit();
            groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numMoveY).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(5, 4, 5, 4);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            splitContainer1.Panel1MinSize = 136;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(916, 359);
            splitContainer1.SplitterDistance = 136;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            splitContainer1.TabStop = false;
            splitContainer1.GotFocus += splitContainer1_GotFocus;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1195, 148);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.AnimateEasingType = EasingType.CubeOut;
            tabControl1.AnimateTime = 200;
            tabControl1.BackgroundColor = Color.FromArgb(30, 30, 30);
            tabControl1.Controls.Add(tabC1);
            tabControl1.Controls.Add(tabC2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.IsDerivedStyle = true;
            tabControl1.ItemSize = new Size(100, 28);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 1;
            tabControl1.SelectedTextColor = Color.White;
            tabControl1.Size = new Size(916, 218);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Speed = 100;
            tabControl1.Style = Style.Dark;
            tabControl1.StyleManager = null;
            tabControl1.TabIndex = 0;
            tabControl1.ThemeAuthor = "Narwin";
            tabControl1.ThemeName = "MetroDark";
            tabControl1.UnselectedTextColor = Color.Gray;
            tabControl1.UseAnimation = false;
            // 
            // tabC1
            // 
            tabC1.BackColor = Color.Transparent;
            tabC1.Controls.Add(C1Size64);
            tabC1.Controls.Add(C1Size32);
            tabC1.Controls.Add(C1Size16);
            tabC1.Controls.Add(groupBox5);
            tabC1.Controls.Add(groupBox4);
            tabC1.Controls.Add(groupBox3);
            tabC1.Controls.Add(groupBox2);
            tabC1.Controls.Add(groupBox1);
            tabC1.ForeColor = SystemColors.ControlText;
            tabC1.Location = new Point(4, 32);
            tabC1.Margin = new Padding(5, 4, 5, 4);
            tabC1.Name = "tabC1";
            tabC1.Padding = new Padding(5, 4, 5, 4);
            tabC1.Size = new Size(908, 182);
            tabC1.TabIndex = 0;
            tabC1.Text = "Crash 1";
            // 
            // C1Size64
            // 
            C1Size64.BorderColour = Color.Empty;
            C1Size64.CustomColour = false;
            C1Size64.FlatBottom = false;
            C1Size64.FlatTop = false;
            C1Size64.Location = new Point(355, 100);
            C1Size64.Margin = new Padding(4);
            C1Size64.Name = "C1Size64";
            C1Size64.Padding = new Padding(6);
            C1Size64.Size = new Size(82, 31);
            C1Size64.TabIndex = 13;
            C1Size64.Text = "64 × 64";
            C1Size64.Click += C1Size64_Click;
            // 
            // C1Size32
            // 
            C1Size32.BorderColour = Color.Empty;
            C1Size32.CustomColour = false;
            C1Size32.FlatBottom = false;
            C1Size32.FlatTop = false;
            C1Size32.Location = new Point(355, 59);
            C1Size32.Margin = new Padding(4);
            C1Size32.Name = "C1Size32";
            C1Size32.Padding = new Padding(6);
            C1Size32.Size = new Size(82, 31);
            C1Size32.TabIndex = 12;
            C1Size32.Text = "32 × 32";
            C1Size32.Click += C1Size32_Click;
            // 
            // C1Size16
            // 
            C1Size16.BorderColour = Color.Empty;
            C1Size16.CustomColour = false;
            C1Size16.FlatBottom = false;
            C1Size16.FlatTop = false;
            C1Size16.Location = new Point(355, 18);
            C1Size16.Margin = new Padding(4);
            C1Size16.Name = "C1Size16";
            C1Size16.Padding = new Padding(6);
            C1Size16.Size = new Size(82, 31);
            C1Size16.TabIndex = 11;
            C1Size16.Text = "16 × 16";
            C1Size16.Click += C1Size16_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(C1numY);
            groupBox5.Controls.Add(C1numX);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(label6);
            groupBox5.Location = new Point(9, 8);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(107, 98);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Offset";
            // 
            // C1numY
            // 
            C1numY.Location = new Point(30, 61);
            C1numY.Margin = new Padding(4);
            C1numY.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            C1numY.Name = "C1numY";
            C1numY.Size = new Size(70, 23);
            C1numY.TabIndex = 3;
            // 
            // C1numX
            // 
            C1numX.Location = new Point(30, 25);
            C1numX.Margin = new Padding(4);
            C1numX.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C1numX.Name = "C1numX";
            C1numX.Size = new Size(70, 23);
            C1numX.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(7, 64);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(14, 15);
            label5.TabIndex = 1;
            label5.Text = "Y";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(7, 28);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(14, 15);
            label6.TabIndex = 0;
            label6.Text = "X";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(C1dpdH);
            groupBox4.Controls.Add(C1dpdW);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(label4);
            groupBox4.Location = new Point(124, 8);
            groupBox4.Margin = new Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4);
            groupBox4.Size = new Size(107, 98);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Size";
            // 
            // C1dpdH
            // 
            C1dpdH.DrawMode = DrawMode.OwnerDrawVariable;
            C1dpdH.FormattingEnabled = true;
            C1dpdH.Items.AddRange(new object[] { "4", "8", "16", "32", "64" });
            C1dpdH.Location = new Point(35, 59);
            C1dpdH.Margin = new Padding(4);
            C1dpdH.Name = "C1dpdH";
            C1dpdH.Size = new Size(65, 24);
            C1dpdH.TabIndex = 3;
            // 
            // C1dpdW
            // 
            C1dpdW.DrawMode = DrawMode.OwnerDrawVariable;
            C1dpdW.FormattingEnabled = true;
            C1dpdW.Items.AddRange(new object[] { "4", "8", "16", "32", "64" });
            C1dpdW.Location = new Point(35, 22);
            C1dpdW.Margin = new Padding(4);
            C1dpdW.Name = "C1dpdW";
            C1dpdW.Size = new Size(65, 24);
            C1dpdW.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(7, 64);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(16, 15);
            label3.TabIndex = 1;
            label3.Text = "H";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(7, 28);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 0;
            label4.Text = "W";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(C1numCY);
            groupBox3.Controls.Add(C1numCX);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new Point(238, 8);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(107, 98);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CLUT";
            // 
            // C1numCY
            // 
            C1numCY.Location = new Point(30, 61);
            C1numCY.Margin = new Padding(4);
            C1numCY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C1numCY.Name = "C1numCY";
            C1numCY.Size = new Size(70, 23);
            C1numCY.TabIndex = 3;
            // 
            // C1numCX
            // 
            C1numCX.Location = new Point(30, 25);
            C1numCX.Margin = new Padding(4);
            C1numCX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            C1numCX.Name = "C1numCX";
            C1numCX.Size = new Size(70, 23);
            C1numCX.TabIndex = 2;
            C1numCX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 64);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 1;
            label2.Text = "Y";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 0;
            label1.Text = "X";
            // 
            // groupBox2
            // 
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(C1dpdBlend);
            groupBox2.Location = new Point(125, 111);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(133, 62);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Blend Mode";
            // 
            // C1dpdBlend
            // 
            C1dpdBlend.DrawMode = DrawMode.OwnerDrawVariable;
            C1dpdBlend.FormattingEnabled = true;
            C1dpdBlend.Items.AddRange(new object[] { "0 (Transparency)", "1 (Additive)", "2 (Subtractive)", "3 (Solid)" });
            C1dpdBlend.Location = new Point(8, 22);
            C1dpdBlend.Margin = new Padding(4);
            C1dpdBlend.Name = "C1dpdBlend";
            C1dpdBlend.Size = new Size(117, 24);
            C1dpdBlend.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(C1dpdColor);
            groupBox1.Location = new Point(9, 111);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(108, 62);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Color Mode";
            // 
            // C1dpdColor
            // 
            C1dpdColor.DrawMode = DrawMode.OwnerDrawVariable;
            C1dpdColor.FormattingEnabled = true;
            C1dpdColor.Items.AddRange(new object[] { "0 (4bpp)", "1 (8bpp)", "2 (16bpp)" });
            C1dpdColor.Location = new Point(8, 22);
            C1dpdColor.Margin = new Padding(4);
            C1dpdColor.Name = "C1dpdColor";
            C1dpdColor.Size = new Size(93, 24);
            C1dpdColor.TabIndex = 0;
            // 
            // tabC2
            // 
            tabC2.BackColor = Color.Transparent;
            tabC2.Controls.Add(C2SizeMax);
            tabC2.Controls.Add(C2Size64);
            tabC2.Controls.Add(C2Size32);
            tabC2.Controls.Add(C2Size16);
            tabC2.Controls.Add(groupBox6);
            tabC2.Controls.Add(groupBox7);
            tabC2.Controls.Add(groupBox8);
            tabC2.Controls.Add(groupBox9);
            tabC2.Controls.Add(groupBox10);
            tabC2.Controls.Add(groupBox11);
            tabC2.Controls.Add(groupBox12);
            tabC2.Location = new Point(4, 32);
            tabC2.Margin = new Padding(4);
            tabC2.Name = "tabC2";
            tabC2.Padding = new Padding(4);
            tabC2.Size = new Size(908, 182);
            tabC2.TabIndex = 1;
            tabC2.Text = "Crash 2";
            // 
            // C2SizeMax
            // 
            C2SizeMax.BorderColour = Color.Empty;
            C2SizeMax.CustomColour = false;
            C2SizeMax.FlatBottom = false;
            C2SizeMax.FlatTop = false;
            C2SizeMax.Location = new Point(451, 137);
            C2SizeMax.Margin = new Padding(4);
            C2SizeMax.Name = "C2SizeMax";
            C2SizeMax.Padding = new Padding(6);
            C2SizeMax.Size = new Size(82, 31);
            C2SizeMax.TabIndex = 0;
            C2SizeMax.Text = "Maximize";
            C2SizeMax.Click += C2SizeMax_Click;
            // 
            // C2Size64
            // 
            C2Size64.BorderColour = Color.Empty;
            C2Size64.CustomColour = false;
            C2Size64.FlatBottom = false;
            C2Size64.FlatTop = false;
            C2Size64.Location = new Point(451, 98);
            C2Size64.Margin = new Padding(4);
            C2Size64.Name = "C2Size64";
            C2Size64.Padding = new Padding(6);
            C2Size64.Size = new Size(82, 31);
            C2Size64.TabIndex = 12;
            C2Size64.Text = "64 × 64";
            C2Size64.Click += C2Size64_Click;
            // 
            // C2Size32
            // 
            C2Size32.BorderColour = Color.Empty;
            C2Size32.CustomColour = false;
            C2Size32.FlatBottom = false;
            C2Size32.FlatTop = false;
            C2Size32.Location = new Point(451, 59);
            C2Size32.Margin = new Padding(4);
            C2Size32.Name = "C2Size32";
            C2Size32.Padding = new Padding(6);
            C2Size32.Size = new Size(82, 31);
            C2Size32.TabIndex = 11;
            C2Size32.Text = "32 × 32";
            C2Size32.Click += C2Size32_Click;
            // 
            // C2Size16
            // 
            C2Size16.BorderColour = Color.Empty;
            C2Size16.CustomColour = false;
            C2Size16.FlatBottom = false;
            C2Size16.FlatTop = false;
            C2Size16.Location = new Point(451, 20);
            C2Size16.Margin = new Padding(4);
            C2Size16.Name = "C2Size16";
            C2Size16.Padding = new Padding(6);
            C2Size16.Size = new Size(82, 31);
            C2Size16.TabIndex = 10;
            C2Size16.Text = "16 × 16";
            C2Size16.Click += C2Size16_Click;
            // 
            // groupBox6
            // 
            groupBox6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox6.Controls.Add(label14);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(C2numY);
            groupBox6.Controls.Add(C2numX);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(C2numY2);
            groupBox6.Controls.Add(C2numX2);
            groupBox6.Location = new Point(9, 8);
            groupBox6.Margin = new Padding(4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4);
            groupBox6.Size = new Size(206, 98);
            groupBox6.TabIndex = 8;
            groupBox6.TabStop = false;
            groupBox6.Text = "Offset";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ImeMode = ImeMode.NoControl;
            label14.Location = new Point(108, 64);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(19, 15);
            label14.TabIndex = 5;
            label14.Text = "0x";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ImeMode = ImeMode.NoControl;
            label13.Location = new Point(108, 28);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(19, 15);
            label13.TabIndex = 4;
            label13.Text = "0x";
            // 
            // C2numY
            // 
            C2numY.Location = new Point(30, 61);
            C2numY.Margin = new Padding(4);
            C2numY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numY.Name = "C2numY";
            C2numY.Size = new Size(70, 23);
            C2numY.TabIndex = 3;
            // 
            // C2numX
            // 
            C2numX.Location = new Point(30, 25);
            C2numX.Margin = new Padding(4);
            C2numX.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numX.Name = "C2numX";
            C2numX.Size = new Size(70, 23);
            C2numX.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(7, 64);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(14, 15);
            label7.TabIndex = 1;
            label7.Text = "Y";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ImeMode = ImeMode.NoControl;
            label8.Location = new Point(7, 28);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(14, 15);
            label8.TabIndex = 0;
            label8.Text = "X";
            // 
            // C2numY2
            // 
            C2numY2.Hexadecimal = true;
            C2numY2.Location = new Point(128, 61);
            C2numY2.Margin = new Padding(4);
            C2numY2.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numY2.Name = "C2numY2";
            C2numY2.Size = new Size(70, 23);
            C2numY2.TabIndex = 3;
            // 
            // C2numX2
            // 
            C2numX2.Hexadecimal = true;
            C2numX2.Location = new Point(128, 25);
            C2numX2.Margin = new Padding(4);
            C2numX2.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numX2.Name = "C2numX2";
            C2numX2.Size = new Size(70, 23);
            C2numX2.TabIndex = 2;
            // 
            // groupBox7
            // 
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(C2numH);
            groupBox7.Controls.Add(label9);
            groupBox7.Controls.Add(C2numW);
            groupBox7.Controls.Add(label10);
            groupBox7.Location = new Point(222, 8);
            groupBox7.Margin = new Padding(4);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4);
            groupBox7.Size = new Size(107, 98);
            groupBox7.TabIndex = 9;
            groupBox7.TabStop = false;
            groupBox7.Text = "Size";
            // 
            // C2numH
            // 
            C2numH.Location = new Point(30, 61);
            C2numH.Margin = new Padding(4);
            C2numH.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            C2numH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numH.Name = "C2numH";
            C2numH.Size = new Size(70, 23);
            C2numH.TabIndex = 5;
            C2numH.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(7, 64);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(16, 15);
            label9.TabIndex = 1;
            label9.Text = "H";
            // 
            // C2numW
            // 
            C2numW.Location = new Point(30, 25);
            C2numW.Margin = new Padding(4);
            C2numW.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            C2numW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numW.Name = "C2numW";
            C2numW.Size = new Size(70, 23);
            C2numW.TabIndex = 4;
            C2numW.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ImeMode = ImeMode.NoControl;
            label10.Location = new Point(7, 28);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(18, 15);
            label10.TabIndex = 0;
            label10.Text = "W";
            // 
            // groupBox8
            // 
            groupBox8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox8.Controls.Add(C2numCY);
            groupBox8.Controls.Add(C2numCX);
            groupBox8.Controls.Add(label11);
            groupBox8.Controls.Add(label12);
            groupBox8.Controls.Add(lblCLUT);
            groupBox8.Font = new Font("Cascadia Code SemiLight", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox8.Location = new Point(336, 8);
            groupBox8.Margin = new Padding(4);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4);
            groupBox8.Size = new Size(107, 131);
            groupBox8.TabIndex = 7;
            groupBox8.TabStop = false;
            groupBox8.Text = "CLUT";
            // 
            // C2numCY
            // 
            C2numCY.Font = new Font("Segoe UI", 9F);
            C2numCY.Location = new Point(30, 61);
            C2numCY.Margin = new Padding(4);
            C2numCY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numCY.Name = "C2numCY";
            C2numCY.Size = new Size(70, 23);
            C2numCY.TabIndex = 3;
            // 
            // C2numCX
            // 
            C2numCX.Font = new Font("Segoe UI", 9F);
            C2numCX.Location = new Point(30, 25);
            C2numCX.Margin = new Padding(4);
            C2numCX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            C2numCX.Name = "C2numCX";
            C2numCX.Size = new Size(70, 23);
            C2numCX.TabIndex = 2;
            C2numCX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.ImeMode = ImeMode.NoControl;
            label11.Location = new Point(7, 64);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(14, 15);
            label11.TabIndex = 1;
            label11.Text = "Y";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(7, 28);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(14, 15);
            label12.TabIndex = 0;
            label12.Text = "X";
            // 
            // lblCLUT
            // 
            lblCLUT.ImeMode = ImeMode.NoControl;
            lblCLUT.Location = new Point(10, 88);
            lblCLUT.Margin = new Padding(4, 0, 4, 0);
            lblCLUT.Name = "lblCLUT";
            lblCLUT.RightToLeft = RightToLeft.No;
            lblCLUT.Size = new Size(90, 30);
            lblCLUT.TabIndex = 4;
            lblCLUT.Text = "clut_hex\r\nclut_offset";
            // 
            // groupBox9
            // 
            groupBox9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox9.Controls.Add(C2dpdBlend);
            groupBox9.Location = new Point(125, 111);
            groupBox9.Margin = new Padding(4);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(4);
            groupBox9.Size = new Size(133, 62);
            groupBox9.TabIndex = 6;
            groupBox9.TabStop = false;
            groupBox9.Text = "Blend Mode";
            // 
            // C2dpdBlend
            // 
            C2dpdBlend.DrawMode = DrawMode.OwnerDrawVariable;
            C2dpdBlend.FormattingEnabled = true;
            C2dpdBlend.Items.AddRange(new object[] { "0 (Transparency)", "1 (Additive)", "2 (Subtractive)", "3 (Solid)" });
            C2dpdBlend.Location = new Point(8, 22);
            C2dpdBlend.Margin = new Padding(4);
            C2dpdBlend.Name = "C2dpdBlend";
            C2dpdBlend.Size = new Size(117, 24);
            C2dpdBlend.TabIndex = 0;
            // 
            // groupBox10
            // 
            groupBox10.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox10.Controls.Add(C2dpdColor);
            groupBox10.Location = new Point(9, 111);
            groupBox10.Margin = new Padding(4);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(4);
            groupBox10.Size = new Size(108, 62);
            groupBox10.TabIndex = 5;
            groupBox10.TabStop = false;
            groupBox10.Text = "Color Mode";
            // 
            // C2dpdColor
            // 
            C2dpdColor.DrawMode = DrawMode.OwnerDrawVariable;
            C2dpdColor.FormattingEnabled = true;
            C2dpdColor.Items.AddRange(new object[] { "0 (4bpp)", "1 (8bpp)", "2 (16bpp)" });
            C2dpdColor.Location = new Point(8, 22);
            C2dpdColor.Margin = new Padding(4);
            C2dpdColor.Name = "C2dpdColor";
            C2dpdColor.Size = new Size(93, 24);
            C2dpdColor.TabIndex = 0;
            // 
            // groupBox11
            // 
            groupBox11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox11.Controls.Add(C2numMoveX);
            groupBox11.Controls.Add(C2btnMoveX1);
            groupBox11.Controls.Add(C2btnMoveX2);
            groupBox11.Location = new Point(541, 8);
            groupBox11.Margin = new Padding(4);
            groupBox11.Name = "groupBox11";
            groupBox11.Padding = new Padding(4);
            groupBox11.Size = new Size(103, 84);
            groupBox11.TabIndex = 10;
            groupBox11.TabStop = false;
            groupBox11.Text = "Move to X";
            // 
            // C2numMoveX
            // 
            C2numMoveX.Location = new Point(7, 21);
            C2numMoveX.Margin = new Padding(4);
            C2numMoveX.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numMoveX.Name = "C2numMoveX";
            C2numMoveX.Size = new Size(89, 23);
            C2numMoveX.TabIndex = 6;
            C2numMoveX.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // C2btnMoveX1
            // 
            C2btnMoveX1.BorderColour = Color.Empty;
            C2btnMoveX1.CustomColour = false;
            C2btnMoveX1.FlatBottom = false;
            C2btnMoveX1.FlatTop = false;
            C2btnMoveX1.Location = new Point(7, 49);
            C2btnMoveX1.Margin = new Padding(4);
            C2btnMoveX1.Name = "C2btnMoveX1";
            C2btnMoveX1.Padding = new Padding(6);
            C2btnMoveX1.Size = new Size(42, 28);
            C2btnMoveX1.TabIndex = 14;
            C2btnMoveX1.Text = "-";
            C2btnMoveX1.Click += C2btnMoveX1_Click;
            // 
            // C2btnMoveX2
            // 
            C2btnMoveX2.BorderColour = Color.Empty;
            C2btnMoveX2.CustomColour = false;
            C2btnMoveX2.FlatBottom = false;
            C2btnMoveX2.FlatTop = false;
            C2btnMoveX2.Location = new Point(54, 49);
            C2btnMoveX2.Margin = new Padding(4);
            C2btnMoveX2.Name = "C2btnMoveX2";
            C2btnMoveX2.Padding = new Padding(6);
            C2btnMoveX2.Size = new Size(42, 28);
            C2btnMoveX2.TabIndex = 13;
            C2btnMoveX2.Text = "+";
            C2btnMoveX2.Click += C2btnMoveX2_Click;
            // 
            // groupBox12
            // 
            groupBox12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox12.Controls.Add(C2numMoveY);
            groupBox12.Controls.Add(C2btnMoveY1);
            groupBox12.Controls.Add(C2btnMoveY2);
            groupBox12.Location = new Point(541, 93);
            groupBox12.Margin = new Padding(4);
            groupBox12.Name = "groupBox12";
            groupBox12.Padding = new Padding(4);
            groupBox12.Size = new Size(103, 84);
            groupBox12.TabIndex = 15;
            groupBox12.TabStop = false;
            groupBox12.Text = "Move to Y";
            // 
            // C2numMoveY
            // 
            C2numMoveY.Location = new Point(7, 21);
            C2numMoveY.Margin = new Padding(4);
            C2numMoveY.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numMoveY.Name = "C2numMoveY";
            C2numMoveY.Size = new Size(89, 23);
            C2numMoveY.TabIndex = 6;
            C2numMoveY.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // C2btnMoveY1
            // 
            C2btnMoveY1.BorderColour = Color.Empty;
            C2btnMoveY1.CustomColour = false;
            C2btnMoveY1.FlatBottom = false;
            C2btnMoveY1.FlatTop = false;
            C2btnMoveY1.Location = new Point(7, 49);
            C2btnMoveY1.Margin = new Padding(4);
            C2btnMoveY1.Name = "C2btnMoveY1";
            C2btnMoveY1.Padding = new Padding(6);
            C2btnMoveY1.Size = new Size(42, 28);
            C2btnMoveY1.TabIndex = 14;
            C2btnMoveY1.Text = "-";
            C2btnMoveY1.Click += C2btnMoveY1_Click;
            // 
            // C2btnMoveY2
            // 
            C2btnMoveY2.BorderColour = Color.Empty;
            C2btnMoveY2.CustomColour = false;
            C2btnMoveY2.FlatBottom = false;
            C2btnMoveY2.FlatTop = false;
            C2btnMoveY2.Location = new Point(54, 49);
            C2btnMoveY2.Margin = new Padding(4);
            C2btnMoveY2.Name = "C2btnMoveY2";
            C2btnMoveY2.Padding = new Padding(6);
            C2btnMoveY2.Size = new Size(42, 28);
            C2btnMoveY2.TabIndex = 13;
            C2btnMoveY2.Text = "+";
            C2btnMoveY2.Click += C2btnMoveY2_Click;
            // 
            // TextureViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(916, 359);
            Controls.Add(splitContainer1);
            CornerStyle = CornerPreference.Default;
            DoubleBuffered = true;
            Margin = new Padding(4);
            Name = "TextureViewer";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabC1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C1numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C1numX).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C1numCY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C1numCX).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tabC2.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numX).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numX2).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numH).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numW).EndInit();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numCY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numCX).EndInit();
            groupBox9.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)C2numMoveX).EndInit();
            groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)C2numMoveY).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox pictureBox1;
        private MetroSetTabControl tabControl1;
        private TabPage tabC1;
        private DarkGroupBox groupBox2;
        private DarkComboBox C1dpdBlend;
        private DarkGroupBox groupBox1;
        private DarkComboBox C1dpdColor;
        private DarkGroupBox groupBox3;
        private Label label2;
        private Label label1;
        private DarkNumericUpDown C1numCY;
        private DarkNumericUpDown C1numCX;
        private DarkGroupBox groupBox4;
        private DarkComboBox C1dpdH;
        private DarkComboBox C1dpdW;
        private Label label3;
        private Label label4;
        private DarkGroupBox groupBox5;
        private DarkNumericUpDown C1numY;
        private DarkNumericUpDown C1numX;
        private Label label5;
        private Label label6;
        private TabPage tabC2;
        private DarkGroupBox groupBox6;
        private DarkNumericUpDown C2numY;
        private DarkNumericUpDown C2numX;
        private Label label7;
        private Label label8;
        private DarkGroupBox groupBox7;
        private Label label9;
        private Label label10;
        private DarkGroupBox groupBox8;
        private DarkNumericUpDown C2numCY;
        private DarkNumericUpDown C2numCX;
        private Label label11;
        private Label label12;
        private DarkGroupBox groupBox9;
        private DarkComboBox C2dpdBlend;
        private DarkGroupBox groupBox10;
        private DarkComboBox C2dpdColor;
        private DarkNumericUpDown C2numH;
        private DarkNumericUpDown C2numW;
        private DarkButton C2Size16;
        private DarkButton C2Size32;
        private DarkButton C2Size64;
        private DarkButton C2SizeMax;
        private DarkButton C1Size16;
        private DarkButton C1Size32;
        private DarkButton C1Size64;
        private DarkNumericUpDown C2numY2;
        private DarkNumericUpDown C2numX2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblCLUT;
        private DarkButton C2btnMoveX2;
        private DarkButton C2btnMoveX1;
        private DarkGroupBox groupBox11;
        private DarkGroupBox groupBox12;
        private DarkButton C2btnMoveY1;
        private DarkButton C2btnMoveY2;
        private DarkNumericUpDown C2numMoveX;
        private DarkNumericUpDown C2numMoveY;
    }
}