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
            groupBox5 = new GroupBox();
            C1numY = new NumericUpDown();
            C1numX = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            groupBox4 = new GroupBox();
            C1dpdH = new ComboBox();
            C1dpdW = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            groupBox3 = new GroupBox();
            C1numCY = new NumericUpDown();
            C1numCX = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            C1dpdBlend = new ComboBox();
            groupBox1 = new GroupBox();
            C1dpdColor = new ComboBox();
            tabC2 = new TabPage();
            groupBox6 = new GroupBox();
            C2numY = new NumericUpDown();
            C2numX = new NumericUpDown();
            label7 = new Label();
            label8 = new Label();
            groupBox7 = new GroupBox();
            C2numH = new NumericUpDown();
            label9 = new Label();
            C2numW = new NumericUpDown();
            label10 = new Label();
            groupBox8 = new GroupBox();
            C2numCY = new NumericUpDown();
            C2numCX = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            groupBox9 = new GroupBox();
            C2dpdBlend = new ComboBox();
            groupBox10 = new GroupBox();
            C2dpdColor = new ComboBox();
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
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numW).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)C2numCY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2numCX).BeginInit();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
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
            splitContainer1.Size = new Size(1195, 417);
            splitContainer1.SplitterDistance = 157;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
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
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.SelectedTextColor = Color.White;
            tabControl1.Size = new Size(1195, 255);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Speed = 100;
            tabControl1.Style = Style.Dark;
            tabControl1.StyleManager = null;
            tabControl1.TabIndex = 0;
            tabControl1.TabStyle = TabStyle.Style2;
            tabControl1.ThemeAuthor = "Narwin";
            tabControl1.ThemeName = "MetroDark";
            tabControl1.UnselectedTextColor = Color.Gray;
            tabControl1.UseAnimation = false;
            // 
            // tabC1
            // 
            tabC1.BackColor = SystemColors.Control;
            tabC1.Controls.Add(groupBox5);
            tabC1.Controls.Add(groupBox4);
            tabC1.Controls.Add(groupBox3);
            tabC1.Controls.Add(groupBox2);
            tabC1.Controls.Add(groupBox1);
            tabC1.ForeColor = SystemColors.ControlText;
            tabC1.Location = new Point(4, 32);
            tabC1.Margin = new Padding(4, 3, 4, 3);
            tabC1.Name = "tabC1";
            tabC1.Padding = new Padding(4, 3, 4, 3);
            tabC1.Size = new Size(1187, 219);
            tabC1.TabIndex = 0;
            tabC1.Text = "Crash 1";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(C1numY);
            groupBox5.Controls.Add(C1numX);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(label6);
            groupBox5.Location = new Point(9, 7);
            groupBox5.Margin = new Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4, 3, 4, 3);
            groupBox5.Size = new Size(107, 97);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Offset";
            // 
            // C1numY
            // 
            C1numY.Location = new Point(30, 52);
            C1numY.Margin = new Padding(4, 3, 4, 3);
            C1numY.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            C1numY.Name = "C1numY";
            C1numY.Size = new Size(70, 23);
            C1numY.TabIndex = 3;
            // 
            // C1numX
            // 
            C1numX.Location = new Point(30, 22);
            C1numX.Margin = new Padding(4, 3, 4, 3);
            C1numX.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C1numX.Name = "C1numX";
            C1numX.Size = new Size(70, 23);
            C1numX.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(7, 54);
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
            label6.Location = new Point(7, 24);
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
            groupBox4.Location = new Point(124, 7);
            groupBox4.Margin = new Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 3, 4, 3);
            groupBox4.Size = new Size(107, 97);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Size";
            // 
            // C1dpdH
            // 
            C1dpdH.DropDownStyle = ComboBoxStyle.DropDownList;
            C1dpdH.FormattingEnabled = true;
            C1dpdH.Items.AddRange(new object[] { "4", "8", "16", "32", "64" });
            C1dpdH.Location = new Point(35, 53);
            C1dpdH.Margin = new Padding(4, 3, 4, 3);
            C1dpdH.Name = "C1dpdH";
            C1dpdH.Size = new Size(65, 23);
            C1dpdH.TabIndex = 3;
            // 
            // C1dpdW
            // 
            C1dpdW.DropDownStyle = ComboBoxStyle.DropDownList;
            C1dpdW.FormattingEnabled = true;
            C1dpdW.Items.AddRange(new object[] { "4", "8", "16", "32", "64" });
            C1dpdW.Location = new Point(35, 22);
            C1dpdW.Margin = new Padding(4, 3, 4, 3);
            C1dpdW.Name = "C1dpdW";
            C1dpdW.Size = new Size(65, 23);
            C1dpdW.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(7, 57);
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
            label4.Location = new Point(7, 25);
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
            groupBox3.Location = new Point(238, 7);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(96, 97);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "CLUT";
            // 
            // C1numCY
            // 
            C1numCY.Location = new Point(30, 52);
            C1numCY.Margin = new Padding(4, 3, 4, 3);
            C1numCY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C1numCY.Name = "C1numCY";
            C1numCY.Size = new Size(58, 23);
            C1numCY.TabIndex = 3;
            // 
            // C1numCX
            // 
            C1numCX.Location = new Point(30, 22);
            C1numCX.Margin = new Padding(4, 3, 4, 3);
            C1numCX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            C1numCX.Name = "C1numCX";
            C1numCX.Size = new Size(58, 23);
            C1numCX.TabIndex = 2;
            C1numCX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 54);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 1;
            label2.Text = "Y";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 0;
            label1.Text = "X";
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(C1dpdBlend);
            groupBox2.Location = new Point(125, 111);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(144, 68);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Blend Mode";
            // 
            // C1dpdBlend
            // 
            C1dpdBlend.DropDownStyle = ComboBoxStyle.DropDownList;
            C1dpdBlend.FormattingEnabled = true;
            C1dpdBlend.Items.AddRange(new object[] { "0 (Transparency)", "1 (Additive)", "2 (Subtractive)", "3 (Solid)" });
            C1dpdBlend.Location = new Point(8, 23);
            C1dpdBlend.Margin = new Padding(4, 3, 4, 3);
            C1dpdBlend.Name = "C1dpdBlend";
            C1dpdBlend.Size = new Size(128, 23);
            C1dpdBlend.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(C1dpdColor);
            groupBox1.Location = new Point(9, 111);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(109, 68);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Color Mode";
            // 
            // C1dpdColor
            // 
            C1dpdColor.DropDownStyle = ComboBoxStyle.DropDownList;
            C1dpdColor.FormattingEnabled = true;
            C1dpdColor.Items.AddRange(new object[] { "0 (4bpp)", "1 (8bpp)", "2 (16bpp)" });
            C1dpdColor.Location = new Point(8, 23);
            C1dpdColor.Margin = new Padding(4, 3, 4, 3);
            C1dpdColor.Name = "C1dpdColor";
            C1dpdColor.Size = new Size(93, 23);
            C1dpdColor.TabIndex = 0;
            // 
            // tabC2
            // 
            tabC2.BackColor = SystemColors.Control;
            tabC2.Controls.Add(groupBox6);
            tabC2.Controls.Add(groupBox7);
            tabC2.Controls.Add(groupBox8);
            tabC2.Controls.Add(groupBox9);
            tabC2.Controls.Add(groupBox10);
            tabC2.ForeColor = SystemColors.ControlText;
            tabC2.Location = new Point(4, 32);
            tabC2.Margin = new Padding(4, 3, 4, 3);
            tabC2.Name = "tabC2";
            tabC2.Padding = new Padding(4, 3, 4, 3);
            tabC2.Size = new Size(1187, 219);
            tabC2.TabIndex = 1;
            tabC2.Text = "Crash 2";
            // 
            // groupBox6
            // 
            groupBox6.AutoSize = true;
            groupBox6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox6.Controls.Add(C2numY);
            groupBox6.Controls.Add(C2numX);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label8);
            groupBox6.Location = new Point(9, 7);
            groupBox6.Margin = new Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4, 3, 4, 3);
            groupBox6.Size = new Size(108, 97);
            groupBox6.TabIndex = 8;
            groupBox6.TabStop = false;
            groupBox6.Text = "Offset";
            // 
            // C2numY
            // 
            C2numY.Location = new Point(30, 52);
            C2numY.Margin = new Padding(4, 3, 4, 3);
            C2numY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numY.Name = "C2numY";
            C2numY.Size = new Size(70, 23);
            C2numY.TabIndex = 3;
            // 
            // C2numX
            // 
            C2numX.Location = new Point(30, 22);
            C2numX.Margin = new Padding(4, 3, 4, 3);
            C2numX.Maximum = new decimal(new int[] { 1023, 0, 0, 0 });
            C2numX.Name = "C2numX";
            C2numX.Size = new Size(70, 23);
            C2numX.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(7, 54);
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
            label8.Location = new Point(7, 24);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(14, 15);
            label8.TabIndex = 0;
            label8.Text = "X";
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(C2numH);
            groupBox7.Controls.Add(label9);
            groupBox7.Controls.Add(C2numW);
            groupBox7.Controls.Add(label10);
            groupBox7.Location = new Point(124, 7);
            groupBox7.Margin = new Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4, 3, 4, 3);
            groupBox7.Size = new Size(108, 97);
            groupBox7.TabIndex = 9;
            groupBox7.TabStop = false;
            groupBox7.Text = "Size";
            // 
            // C2numH
            // 
            C2numH.Location = new Point(30, 52);
            C2numH.Margin = new Padding(4, 3, 4, 3);
            C2numH.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            C2numH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numH.Name = "C2numH";
            C2numH.Size = new Size(70, 23);
            C2numH.TabIndex = 5;
            C2numH.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(7, 54);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(16, 15);
            label9.TabIndex = 1;
            label9.Text = "H";
            // 
            // C2numW
            // 
            C2numW.Location = new Point(30, 22);
            C2numW.Margin = new Padding(4, 3, 4, 3);
            C2numW.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            C2numW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            C2numW.Name = "C2numW";
            C2numW.Size = new Size(70, 23);
            C2numW.TabIndex = 4;
            C2numW.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ImeMode = ImeMode.NoControl;
            label10.Location = new Point(7, 24);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(18, 15);
            label10.TabIndex = 0;
            label10.Text = "W";
            // 
            // groupBox8
            // 
            groupBox8.AutoSize = true;
            groupBox8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox8.Controls.Add(C2numCY);
            groupBox8.Controls.Add(C2numCX);
            groupBox8.Controls.Add(label11);
            groupBox8.Controls.Add(label12);
            groupBox8.Location = new Point(238, 7);
            groupBox8.Margin = new Padding(4, 3, 4, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4, 3, 4, 3);
            groupBox8.Size = new Size(96, 97);
            groupBox8.TabIndex = 7;
            groupBox8.TabStop = false;
            groupBox8.Text = "CLUT";
            // 
            // C2numCY
            // 
            C2numCY.Location = new Point(30, 52);
            C2numCY.Margin = new Padding(4, 3, 4, 3);
            C2numCY.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            C2numCY.Name = "C2numCY";
            C2numCY.Size = new Size(58, 23);
            C2numCY.TabIndex = 3;
            // 
            // C2numCX
            // 
            C2numCX.Location = new Point(30, 22);
            C2numCX.Margin = new Padding(4, 3, 4, 3);
            C2numCX.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            C2numCX.Name = "C2numCX";
            C2numCX.Size = new Size(58, 23);
            C2numCX.TabIndex = 2;
            C2numCX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ImeMode = ImeMode.NoControl;
            label11.Location = new Point(7, 54);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(14, 15);
            label11.TabIndex = 1;
            label11.Text = "Y";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(7, 24);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(14, 15);
            label12.TabIndex = 0;
            label12.Text = "X";
            // 
            // groupBox9
            // 
            groupBox9.AutoSize = true;
            groupBox9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox9.Controls.Add(C2dpdBlend);
            groupBox9.Location = new Point(125, 111);
            groupBox9.Margin = new Padding(4, 3, 4, 3);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(4, 3, 4, 3);
            groupBox9.Size = new Size(144, 68);
            groupBox9.TabIndex = 6;
            groupBox9.TabStop = false;
            groupBox9.Text = "Blend Mode";
            // 
            // C2dpdBlend
            // 
            C2dpdBlend.DropDownStyle = ComboBoxStyle.DropDownList;
            C2dpdBlend.FormattingEnabled = true;
            C2dpdBlend.Items.AddRange(new object[] { "0 (Transparency)", "1 (Additive)", "2 (Subtractive)", "3 (Solid)" });
            C2dpdBlend.Location = new Point(8, 23);
            C2dpdBlend.Margin = new Padding(4, 3, 4, 3);
            C2dpdBlend.Name = "C2dpdBlend";
            C2dpdBlend.Size = new Size(128, 23);
            C2dpdBlend.TabIndex = 0;
            // 
            // groupBox10
            // 
            groupBox10.AutoSize = true;
            groupBox10.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox10.Controls.Add(C2dpdColor);
            groupBox10.Location = new Point(9, 111);
            groupBox10.Margin = new Padding(4, 3, 4, 3);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(4, 3, 4, 3);
            groupBox10.Size = new Size(109, 68);
            groupBox10.TabIndex = 5;
            groupBox10.TabStop = false;
            groupBox10.Text = "Color Mode";
            // 
            // C2dpdColor
            // 
            C2dpdColor.DropDownStyle = ComboBoxStyle.DropDownList;
            C2dpdColor.FormattingEnabled = true;
            C2dpdColor.Items.AddRange(new object[] { "0 (4bpp)", "1 (8bpp)", "2 (16bpp)" });
            C2dpdColor.Location = new Point(8, 23);
            C2dpdColor.Margin = new Padding(4, 3, 4, 3);
            C2dpdColor.Name = "C2dpdColor";
            C2dpdColor.Size = new Size(93, 23);
            C2dpdColor.TabIndex = 0;
            // 
            // TextureViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1195, 417);
            Controls.Add(splitContainer1);
            DoubleBuffered = true;
            Margin = new Padding(4, 3, 4, 3);
            Name = "TextureViewer";
            ShowIcon = false;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabC1.ResumeLayout(false);
            tabC1.PerformLayout();
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
            tabC2.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)C2numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2numX).EndInit();
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
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroSetTabControl tabControl1;
        private System.Windows.Forms.TabPage tabC1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox C1dpdBlend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox C1dpdColor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown C1numCY;
        private System.Windows.Forms.NumericUpDown C1numCX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox C1dpdH;
        private System.Windows.Forms.ComboBox C1dpdW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown C1numY;
        private System.Windows.Forms.NumericUpDown C1numX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabC2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown C2numY;
        private System.Windows.Forms.NumericUpDown C2numX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown C2numCY;
        private System.Windows.Forms.NumericUpDown C2numCX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox C2dpdBlend;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox C2dpdColor;
        private System.Windows.Forms.NumericUpDown C2numH;
        private System.Windows.Forms.NumericUpDown C2numW;
    }
}