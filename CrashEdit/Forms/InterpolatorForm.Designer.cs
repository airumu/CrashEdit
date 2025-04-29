using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    public partial class InterpolatorForm
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
            cmdCancel = new DarkButton();
            cmdOK = new DarkButton();
            dpdFunc = new DarkComboBox();
            lblX = new Label();
            lblY = new Label();
            lblZ = new Label();
            numX = new DarkNumericUpDown();
            numY = new DarkNumericUpDown();
            numZ = new DarkNumericUpDown();
            lblAverage = new Label();
            fraFunction = new DarkGroupBox();
            fraPosition = new DarkGroupBox();
            lblPosition = new Label();
            cmdNext = new DarkButton();
            cmdPrev = new DarkButton();
            cmdLast = new DarkButton();
            cmdFirst = new DarkButton();
            fraBound = new DarkGroupBox();
            numEnd = new DarkNumericUpDown();
            numStart = new DarkNumericUpDown();
            fraAmount = new DarkGroupBox();
            numAmount = new DarkNumericUpDown();
            fraTension = new DarkGroupBox();
            numTension = new DarkNumericUpDown();
            fraOrder = new DarkGroupBox();
            numOrder = new DarkNumericUpDown();
            tabControl1 = new MetroSetTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            fraCenter = new DarkGroupBox();
            rdbCentral = new DarkRadioButton();
            rdbPosition0 = new DarkRadioButton();
            cmdOK2 = new DarkButton();
            fraDegrees = new DarkGroupBox();
            cmdDegZSub45 = new DarkButton();
            cmdDegYSub45 = new DarkButton();
            cmdDegXSub45 = new DarkButton();
            cmdDegZAdd45 = new DarkButton();
            cmdDegYAdd45 = new DarkButton();
            cmdDegXAdd45 = new DarkButton();
            numDegreeZ = new DarkNumericUpDown();
            numDegreeY = new DarkNumericUpDown();
            numDegreeX = new DarkNumericUpDown();
            lbDegreeY = new Label();
            lbDegreeX = new Label();
            lbDegreeZ = new Label();
            cmdCancel2 = new DarkButton();
            fraStartAngle = new DarkGroupBox();
            numStartAngle = new DarkNumericUpDown();
            cmdStartAngleAdd45 = new DarkButton();
            cmdStartAngleSub45 = new DarkButton();
            fraRadius = new DarkGroupBox();
            numRadius = new DarkNumericUpDown();
            fraAmount2 = new DarkGroupBox();
            numAmount2 = new DarkNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numZ).BeginInit();
            fraFunction.SuspendLayout();
            fraPosition.SuspendLayout();
            fraBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEnd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStart).BeginInit();
            fraAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            fraTension.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTension).BeginInit();
            fraOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numOrder).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            fraCenter.SuspendLayout();
            fraDegrees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDegreeZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDegreeY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDegreeX).BeginInit();
            fraStartAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStartAngle).BeginInit();
            fraRadius.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRadius).BeginInit();
            fraAmount2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount2).BeginInit();
            SuspendLayout();
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.BorderColour = Color.Empty;
            cmdCancel.CustomColour = false;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.FlatBottom = false;
            cmdCancel.FlatTop = false;
            cmdCancel.Location = new Point(247, 293);
            cmdCancel.Margin = new Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Padding = new Padding(6);
            cmdCancel.Size = new Size(88, 27);
            cmdCancel.TabIndex = 3;
            cmdCancel.Text = "Cancel";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // cmdOK
            // 
            cmdOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdOK.BorderColour = Color.Empty;
            cmdOK.CustomColour = false;
            cmdOK.FlatBottom = false;
            cmdOK.FlatTop = false;
            cmdOK.Location = new Point(152, 293);
            cmdOK.Margin = new Padding(4, 3, 4, 3);
            cmdOK.Name = "cmdOK";
            cmdOK.Padding = new Padding(6);
            cmdOK.Size = new Size(88, 27);
            cmdOK.TabIndex = 4;
            cmdOK.Text = "OK";
            cmdOK.Click += cmdOK_Click;
            // 
            // dpdFunc
            // 
            dpdFunc.DrawMode = DrawMode.OwnerDrawVariable;
            dpdFunc.FormattingEnabled = true;
            dpdFunc.Location = new Point(7, 22);
            dpdFunc.Margin = new Padding(4, 3, 4, 3);
            dpdFunc.Name = "dpdFunc";
            dpdFunc.Size = new Size(140, 24);
            dpdFunc.TabIndex = 5;
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.Location = new Point(8, 40);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 0;
            lblX.Text = "X";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.BackColor = Color.Transparent;
            lblY.Location = new Point(8, 70);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 1;
            lblY.Text = "Y";
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.BackColor = Color.Transparent;
            lblZ.Location = new Point(8, 100);
            lblZ.Margin = new Padding(4, 0, 4, 0);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(14, 15);
            lblZ.TabIndex = 2;
            lblZ.Text = "Z";
            // 
            // numX
            // 
            numX.Location = new Point(30, 38);
            numX.Margin = new Padding(4, 3, 4, 3);
            numX.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numX.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numX.Name = "numX";
            numX.ReadOnly = true;
            numX.Size = new Size(100, 23);
            numX.TabIndex = 3;
            // 
            // numY
            // 
            numY.Location = new Point(30, 68);
            numY.Margin = new Padding(4, 3, 4, 3);
            numY.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numY.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numY.Name = "numY";
            numY.ReadOnly = true;
            numY.Size = new Size(100, 23);
            numY.TabIndex = 4;
            // 
            // numZ
            // 
            numZ.Location = new Point(30, 98);
            numZ.Margin = new Padding(4, 3, 4, 3);
            numZ.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numZ.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numZ.Name = "numZ";
            numZ.ReadOnly = true;
            numZ.Size = new Size(100, 23);
            numZ.TabIndex = 5;
            // 
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.BackColor = Color.Transparent;
            lblAverage.Location = new Point(7, 275);
            lblAverage.Margin = new Padding(4, 0, 4, 0);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(140, 15);
            lblAverage.TabIndex = 11;
            lblAverage.Text = "Average Point Distance: -";
            // 
            // fraFunction
            // 
            fraFunction.AutoSize = true;
            fraFunction.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraFunction.Controls.Add(dpdFunc);
            fraFunction.Location = new Point(152, 61);
            fraFunction.Margin = new Padding(4, 3, 4, 3);
            fraFunction.Name = "fraFunction";
            fraFunction.Padding = new Padding(4, 3, 4, 3);
            fraFunction.Size = new Size(155, 68);
            fraFunction.TabIndex = 0;
            fraFunction.TabStop = false;
            fraFunction.Text = "Progress Function";
            // 
            // fraPosition
            // 
            fraPosition.AutoSize = true;
            fraPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPosition.Controls.Add(lblPosition);
            fraPosition.Controls.Add(cmdNext);
            fraPosition.Controls.Add(lblX);
            fraPosition.Controls.Add(cmdPrev);
            fraPosition.Controls.Add(lblY);
            fraPosition.Controls.Add(cmdLast);
            fraPosition.Controls.Add(lblZ);
            fraPosition.Controls.Add(cmdFirst);
            fraPosition.Controls.Add(numX);
            fraPosition.Controls.Add(numZ);
            fraPosition.Controls.Add(numY);
            fraPosition.Location = new Point(7, 6);
            fraPosition.Margin = new Padding(4, 3, 4, 3);
            fraPosition.Name = "fraPosition";
            fraPosition.Padding = new Padding(4, 3, 4, 3);
            fraPosition.Size = new Size(139, 211);
            fraPosition.TabIndex = 6;
            fraPosition.TabStop = false;
            fraPosition.Text = "Positions";
            // 
            // lblPosition
            // 
            lblPosition.BackColor = Color.Transparent;
            lblPosition.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPosition.Location = new Point(7, 18);
            lblPosition.Margin = new Padding(4, 0, 4, 0);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(124, 16);
            lblPosition.TabIndex = 6;
            lblPosition.Text = "?? / ??";
            lblPosition.TextAlign = ContentAlignment.TopCenter;
            // 
            // cmdNext
            // 
            cmdNext.BorderColour = Color.Empty;
            cmdNext.CustomColour = false;
            cmdNext.FlatBottom = false;
            cmdNext.FlatTop = false;
            cmdNext.Location = new Point(72, 128);
            cmdNext.Margin = new Padding(4, 3, 4, 3);
            cmdNext.Name = "cmdNext";
            cmdNext.Padding = new Padding(6);
            cmdNext.Size = new Size(58, 27);
            cmdNext.TabIndex = 0;
            cmdNext.Text = "Next";
            cmdNext.Click += cmdNext_Click;
            // 
            // cmdPrev
            // 
            cmdPrev.BorderColour = Color.Empty;
            cmdPrev.CustomColour = false;
            cmdPrev.FlatBottom = false;
            cmdPrev.FlatTop = false;
            cmdPrev.Location = new Point(7, 128);
            cmdPrev.Margin = new Padding(4, 3, 4, 3);
            cmdPrev.Name = "cmdPrev";
            cmdPrev.Padding = new Padding(6);
            cmdPrev.Size = new Size(58, 27);
            cmdPrev.TabIndex = 1;
            cmdPrev.Text = "Prev";
            cmdPrev.Click += cmdPrev_Click;
            // 
            // cmdLast
            // 
            cmdLast.BorderColour = Color.Empty;
            cmdLast.CustomColour = false;
            cmdLast.FlatBottom = false;
            cmdLast.FlatTop = false;
            cmdLast.Location = new Point(72, 162);
            cmdLast.Margin = new Padding(4, 3, 4, 3);
            cmdLast.Name = "cmdLast";
            cmdLast.Padding = new Padding(6);
            cmdLast.Size = new Size(58, 27);
            cmdLast.TabIndex = 3;
            cmdLast.Text = "Last";
            cmdLast.Click += cmdLast_Click;
            // 
            // cmdFirst
            // 
            cmdFirst.BorderColour = Color.Empty;
            cmdFirst.CustomColour = false;
            cmdFirst.FlatBottom = false;
            cmdFirst.FlatTop = false;
            cmdFirst.Location = new Point(7, 162);
            cmdFirst.Margin = new Padding(4, 3, 4, 3);
            cmdFirst.Name = "cmdFirst";
            cmdFirst.Padding = new Padding(6);
            cmdFirst.Size = new Size(58, 27);
            cmdFirst.TabIndex = 2;
            cmdFirst.Text = "First";
            cmdFirst.Click += cmdFirst_Click;
            // 
            // fraBound
            // 
            fraBound.Controls.Add(numEnd);
            fraBound.Controls.Add(numStart);
            fraBound.Location = new Point(152, 6);
            fraBound.Margin = new Padding(4, 3, 4, 3);
            fraBound.Name = "fraBound";
            fraBound.Padding = new Padding(4, 3, 4, 3);
            fraBound.Size = new Size(155, 48);
            fraBound.TabIndex = 12;
            fraBound.TabStop = false;
            fraBound.Text = "Start/End Position";
            // 
            // numEnd
            // 
            numEnd.Location = new Point(80, 18);
            numEnd.Margin = new Padding(4, 3, 4, 3);
            numEnd.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numEnd.Name = "numEnd";
            numEnd.Size = new Size(70, 23);
            numEnd.TabIndex = 1;
            numEnd.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numEnd.ValueChanged += numEnd_ValueChanged;
            // 
            // numStart
            // 
            numStart.Location = new Point(4, 18);
            numStart.Margin = new Padding(4, 3, 4, 3);
            numStart.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStart.Name = "numStart";
            numStart.Size = new Size(70, 23);
            numStart.TabIndex = 0;
            numStart.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numStart.ValueChanged += numStart_ValueChanged;
            // 
            // fraAmount
            // 
            fraAmount.Controls.Add(numAmount);
            fraAmount.Location = new Point(7, 223);
            fraAmount.Margin = new Padding(4, 3, 4, 3);
            fraAmount.Name = "fraAmount";
            fraAmount.Padding = new Padding(4, 3, 4, 3);
            fraAmount.Size = new Size(85, 48);
            fraAmount.TabIndex = 13;
            fraAmount.TabStop = false;
            fraAmount.Text = "Amount";
            // 
            // numAmount
            // 
            numAmount.Location = new Point(4, 18);
            numAmount.Margin = new Padding(4, 3, 4, 3);
            numAmount.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(70, 23);
            numAmount.TabIndex = 0;
            numAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numAmount.ValueChanged += numAmount_ValueChanged;
            // 
            // fraTension
            // 
            fraTension.Controls.Add(numTension);
            fraTension.Location = new Point(152, 136);
            fraTension.Margin = new Padding(4, 3, 4, 3);
            fraTension.Name = "fraTension";
            fraTension.Padding = new Padding(4, 3, 4, 3);
            fraTension.Size = new Size(155, 52);
            fraTension.TabIndex = 14;
            fraTension.TabStop = false;
            fraTension.Text = "Tension";
            // 
            // numTension
            // 
            numTension.DecimalPlaces = 2;
            numTension.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numTension.Location = new Point(7, 22);
            numTension.Margin = new Padding(4, 3, 4, 3);
            numTension.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numTension.Name = "numTension";
            numTension.Size = new Size(141, 23);
            numTension.TabIndex = 0;
            numTension.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numTension.ValueChanged += numTension_ValueChanged;
            // 
            // fraOrder
            // 
            fraOrder.Controls.Add(numOrder);
            fraOrder.Location = new Point(152, 195);
            fraOrder.Margin = new Padding(4, 3, 4, 3);
            fraOrder.Name = "fraOrder";
            fraOrder.Padding = new Padding(4, 3, 4, 3);
            fraOrder.Size = new Size(155, 52);
            fraOrder.TabIndex = 15;
            fraOrder.TabStop = false;
            fraOrder.Text = "Order";
            // 
            // numOrder
            // 
            numOrder.DecimalPlaces = 3;
            numOrder.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numOrder.Location = new Point(7, 22);
            numOrder.Margin = new Padding(4, 3, 4, 3);
            numOrder.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numOrder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numOrder.Name = "numOrder";
            numOrder.Size = new Size(141, 23);
            numOrder.TabIndex = 0;
            numOrder.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numOrder.ValueChanged += numOrder_ValueChanged;
            // 
            // tabControl1
            // 
            tabControl1.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabControl1.AnimateTime = 200;
            tabControl1.BackgroundColor = Color.FromArgb(30, 30, 30);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.IsDerivedStyle = true;
            tabControl1.ItemSize = new Size(100, 32);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.SelectedTextColor = Color.White;
            tabControl1.Size = new Size(347, 363);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Speed = 100;
            tabControl1.Style = MetroSet_UI.Enums.Style.Dark;
            tabControl1.StyleManager = null;
            tabControl1.TabIndex = 16;
            tabControl1.ThemeAuthor = "Narwin";
            tabControl1.ThemeName = "MetroDark";
            tabControl1.UnselectedTextColor = Color.Gray;
            tabControl1.UseAnimation = false;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(31, 31, 32);
            tabPage1.Controls.Add(fraPosition);
            tabPage1.Controls.Add(cmdOK);
            tabPage1.Controls.Add(fraOrder);
            tabPage1.Controls.Add(cmdCancel);
            tabPage1.Controls.Add(fraFunction);
            tabPage1.Controls.Add(fraTension);
            tabPage1.Controls.Add(fraBound);
            tabPage1.Controls.Add(lblAverage);
            tabPage1.Controls.Add(fraAmount);
            tabPage1.Location = new Point(4, 36);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(339, 323);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Interpolator";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(31, 31, 32);
            tabPage2.Controls.Add(fraCenter);
            tabPage2.Controls.Add(cmdOK2);
            tabPage2.Controls.Add(fraDegrees);
            tabPage2.Controls.Add(cmdCancel2);
            tabPage2.Controls.Add(fraStartAngle);
            tabPage2.Controls.Add(fraRadius);
            tabPage2.Controls.Add(fraAmount2);
            tabPage2.Location = new Point(4, 36);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(339, 323);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Builder";
            // 
            // fraCenter
            // 
            fraCenter.AutoSize = true;
            fraCenter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraCenter.BackColor = Color.Transparent;
            fraCenter.Controls.Add(rdbCentral);
            fraCenter.Controls.Add(rdbPosition0);
            fraCenter.Location = new Point(7, 64);
            fraCenter.MinimumSize = new Size(140, 0);
            fraCenter.Name = "fraCenter";
            fraCenter.Size = new Size(140, 85);
            fraCenter.TabIndex = 26;
            fraCenter.TabStop = false;
            fraCenter.Text = "Center";
            // 
            // rdbCentral
            // 
            rdbCentral.AutoSize = true;
            rdbCentral.Location = new Point(6, 44);
            rdbCentral.Name = "rdbCentral";
            rdbCentral.Size = new Size(71, 19);
            rdbCentral.TabIndex = 25;
            rdbCentral.TabStop = true;
            rdbCentral.Text = "Centroid";
            // 
            // rdbPosition0
            // 
            rdbPosition0.AutoSize = true;
            rdbPosition0.Checked = true;
            rdbPosition0.Location = new Point(6, 22);
            rdbPosition0.Name = "rdbPosition0";
            rdbPosition0.Size = new Size(77, 19);
            rdbPosition0.TabIndex = 25;
            rdbPosition0.TabStop = true;
            rdbPosition0.Text = "Position 0";
            // 
            // cmdOK2
            // 
            cmdOK2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdOK2.BorderColour = Color.Empty;
            cmdOK2.CustomColour = false;
            cmdOK2.FlatBottom = false;
            cmdOK2.FlatTop = false;
            cmdOK2.Location = new Point(152, 293);
            cmdOK2.Margin = new Padding(4, 3, 4, 3);
            cmdOK2.Name = "cmdOK2";
            cmdOK2.Padding = new Padding(6);
            cmdOK2.Size = new Size(88, 27);
            cmdOK2.TabIndex = 18;
            cmdOK2.Text = "OK";
            cmdOK2.Click += cmdOK2_Click;
            // 
            // fraDegrees
            // 
            fraDegrees.Controls.Add(cmdDegZSub45);
            fraDegrees.Controls.Add(cmdDegYSub45);
            fraDegrees.Controls.Add(cmdDegXSub45);
            fraDegrees.Controls.Add(cmdDegZAdd45);
            fraDegrees.Controls.Add(cmdDegYAdd45);
            fraDegrees.Controls.Add(cmdDegXAdd45);
            fraDegrees.Controls.Add(numDegreeZ);
            fraDegrees.Controls.Add(numDegreeY);
            fraDegrees.Controls.Add(numDegreeX);
            fraDegrees.Controls.Add(lbDegreeY);
            fraDegrees.Controls.Add(lbDegreeX);
            fraDegrees.Controls.Add(lbDegreeZ);
            fraDegrees.Location = new Point(152, 88);
            fraDegrees.Margin = new Padding(4, 3, 4, 3);
            fraDegrees.Name = "fraDegrees";
            fraDegrees.Padding = new Padding(4, 3, 4, 3);
            fraDegrees.Size = new Size(155, 183);
            fraDegrees.TabIndex = 24;
            fraDegrees.TabStop = false;
            fraDegrees.Text = "Degrees";
            // 
            // cmdDegZSub45
            // 
            cmdDegZSub45.BorderColour = Color.Empty;
            cmdDegZSub45.CustomColour = false;
            cmdDegZSub45.FlatBottom = false;
            cmdDegZSub45.FlatTop = false;
            cmdDegZSub45.Location = new Point(48, 151);
            cmdDegZSub45.Name = "cmdDegZSub45";
            cmdDegZSub45.Padding = new Padding(5);
            cmdDegZSub45.Size = new Size(36, 20);
            cmdDegZSub45.TabIndex = 3;
            cmdDegZSub45.Text = "-45";
            cmdDegZSub45.Click += cmdDegZSub45_Click;
            // 
            // cmdDegYSub45
            // 
            cmdDegYSub45.BorderColour = Color.Empty;
            cmdDegYSub45.CustomColour = false;
            cmdDegYSub45.FlatBottom = false;
            cmdDegYSub45.FlatTop = false;
            cmdDegYSub45.Location = new Point(48, 100);
            cmdDegYSub45.Name = "cmdDegYSub45";
            cmdDegYSub45.Padding = new Padding(5);
            cmdDegYSub45.Size = new Size(36, 20);
            cmdDegYSub45.TabIndex = 3;
            cmdDegYSub45.Text = "-45";
            cmdDegYSub45.Click += cmdDegYSub45_Click;
            // 
            // cmdDegXSub45
            // 
            cmdDegXSub45.BorderColour = Color.Empty;
            cmdDegXSub45.CustomColour = false;
            cmdDegXSub45.FlatBottom = false;
            cmdDegXSub45.FlatTop = false;
            cmdDegXSub45.Location = new Point(48, 49);
            cmdDegXSub45.Name = "cmdDegXSub45";
            cmdDegXSub45.Padding = new Padding(5);
            cmdDegXSub45.Size = new Size(36, 20);
            cmdDegXSub45.TabIndex = 3;
            cmdDegXSub45.Text = "-45";
            cmdDegXSub45.Click += cmdDegXSub45_Click;
            // 
            // cmdDegZAdd45
            // 
            cmdDegZAdd45.BorderColour = Color.Empty;
            cmdDegZAdd45.CustomColour = false;
            cmdDegZAdd45.FlatBottom = false;
            cmdDegZAdd45.FlatTop = false;
            cmdDegZAdd45.Location = new Point(86, 151);
            cmdDegZAdd45.Name = "cmdDegZAdd45";
            cmdDegZAdd45.Padding = new Padding(5);
            cmdDegZAdd45.Size = new Size(36, 20);
            cmdDegZAdd45.TabIndex = 3;
            cmdDegZAdd45.Text = "+45";
            cmdDegZAdd45.Click += cmdDegZAdd45_Click;
            // 
            // cmdDegYAdd45
            // 
            cmdDegYAdd45.BorderColour = Color.Empty;
            cmdDegYAdd45.CustomColour = false;
            cmdDegYAdd45.FlatBottom = false;
            cmdDegYAdd45.FlatTop = false;
            cmdDegYAdd45.Location = new Point(86, 100);
            cmdDegYAdd45.Name = "cmdDegYAdd45";
            cmdDegYAdd45.Padding = new Padding(5);
            cmdDegYAdd45.Size = new Size(36, 20);
            cmdDegYAdd45.TabIndex = 3;
            cmdDegYAdd45.Text = "+45";
            cmdDegYAdd45.Click += cmdDegYAdd45_Click;
            // 
            // cmdDegXAdd45
            // 
            cmdDegXAdd45.BorderColour = Color.Empty;
            cmdDegXAdd45.CustomColour = false;
            cmdDegXAdd45.FlatBottom = false;
            cmdDegXAdd45.FlatTop = false;
            cmdDegXAdd45.Location = new Point(86, 49);
            cmdDegXAdd45.Name = "cmdDegXAdd45";
            cmdDegXAdd45.Padding = new Padding(5);
            cmdDegXAdd45.Size = new Size(36, 20);
            cmdDegXAdd45.TabIndex = 3;
            cmdDegXAdd45.Text = "+45";
            cmdDegXAdd45.Click += cmdDegXAdd45_Click;
            // 
            // numDegreeZ
            // 
            numDegreeZ.Location = new Point(30, 124);
            numDegreeZ.Margin = new Padding(4, 3, 4, 3);
            numDegreeZ.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numDegreeZ.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numDegreeZ.Name = "numDegreeZ";
            numDegreeZ.Size = new Size(118, 23);
            numDegreeZ.TabIndex = 0;
            // 
            // numDegreeY
            // 
            numDegreeY.Location = new Point(30, 73);
            numDegreeY.Margin = new Padding(4, 3, 4, 3);
            numDegreeY.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numDegreeY.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numDegreeY.Name = "numDegreeY";
            numDegreeY.Size = new Size(118, 23);
            numDegreeY.TabIndex = 0;
            // 
            // numDegreeX
            // 
            numDegreeX.Location = new Point(30, 22);
            numDegreeX.Margin = new Padding(4, 3, 4, 3);
            numDegreeX.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numDegreeX.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numDegreeX.Name = "numDegreeX";
            numDegreeX.Size = new Size(118, 23);
            numDegreeX.TabIndex = 0;
            // 
            // lbDegreeY
            // 
            lbDegreeY.AutoSize = true;
            lbDegreeY.BackColor = Color.Transparent;
            lbDegreeY.Location = new Point(8, 76);
            lbDegreeY.Margin = new Padding(4, 0, 4, 0);
            lbDegreeY.Name = "lbDegreeY";
            lbDegreeY.Size = new Size(14, 15);
            lbDegreeY.TabIndex = 1;
            lbDegreeY.Text = "Y";
            // 
            // lbDegreeX
            // 
            lbDegreeX.AutoSize = true;
            lbDegreeX.BackColor = Color.Transparent;
            lbDegreeX.Location = new Point(8, 24);
            lbDegreeX.Margin = new Padding(4, 0, 4, 0);
            lbDegreeX.Name = "lbDegreeX";
            lbDegreeX.Size = new Size(14, 15);
            lbDegreeX.TabIndex = 0;
            lbDegreeX.Text = "X";
            // 
            // lbDegreeZ
            // 
            lbDegreeZ.AutoSize = true;
            lbDegreeZ.BackColor = Color.Transparent;
            lbDegreeZ.Location = new Point(8, 128);
            lbDegreeZ.Margin = new Padding(4, 0, 4, 0);
            lbDegreeZ.Name = "lbDegreeZ";
            lbDegreeZ.Size = new Size(14, 15);
            lbDegreeZ.TabIndex = 2;
            lbDegreeZ.Text = "Z";
            // 
            // cmdCancel2
            // 
            cmdCancel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel2.BorderColour = Color.Empty;
            cmdCancel2.CustomColour = false;
            cmdCancel2.DialogResult = DialogResult.Cancel;
            cmdCancel2.FlatBottom = false;
            cmdCancel2.FlatTop = false;
            cmdCancel2.Location = new Point(247, 293);
            cmdCancel2.Margin = new Padding(4, 3, 4, 3);
            cmdCancel2.Name = "cmdCancel2";
            cmdCancel2.Padding = new Padding(6);
            cmdCancel2.Size = new Size(88, 27);
            cmdCancel2.TabIndex = 17;
            cmdCancel2.Text = "Cancel";
            cmdCancel2.Click += cmdCancel_Click;
            // 
            // fraStartAngle
            // 
            fraStartAngle.Controls.Add(numStartAngle);
            fraStartAngle.Controls.Add(cmdStartAngleAdd45);
            fraStartAngle.Controls.Add(cmdStartAngleSub45);
            fraStartAngle.Location = new Point(152, 6);
            fraStartAngle.Margin = new Padding(4, 3, 4, 3);
            fraStartAngle.Name = "fraStartAngle";
            fraStartAngle.Padding = new Padding(4, 3, 4, 3);
            fraStartAngle.Size = new Size(155, 76);
            fraStartAngle.TabIndex = 23;
            fraStartAngle.TabStop = false;
            fraStartAngle.Text = "Start Angle";
            // 
            // numStartAngle
            // 
            numStartAngle.Location = new Point(7, 22);
            numStartAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numStartAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numStartAngle.Name = "numStartAngle";
            numStartAngle.Size = new Size(141, 23);
            numStartAngle.TabIndex = 25;
            // 
            // cmdStartAngleAdd45
            // 
            cmdStartAngleAdd45.BorderColour = Color.Empty;
            cmdStartAngleAdd45.CustomColour = false;
            cmdStartAngleAdd45.FlatBottom = false;
            cmdStartAngleAdd45.FlatTop = false;
            cmdStartAngleAdd45.Location = new Point(86, 49);
            cmdStartAngleAdd45.Name = "cmdStartAngleAdd45";
            cmdStartAngleAdd45.Padding = new Padding(5);
            cmdStartAngleAdd45.Size = new Size(36, 20);
            cmdStartAngleAdd45.TabIndex = 3;
            cmdStartAngleAdd45.Text = "+45";
            cmdStartAngleAdd45.Click += cmdStartAngleAdd45_Click;
            // 
            // cmdStartAngleSub45
            // 
            cmdStartAngleSub45.BorderColour = Color.Empty;
            cmdStartAngleSub45.CustomColour = false;
            cmdStartAngleSub45.FlatBottom = false;
            cmdStartAngleSub45.FlatTop = false;
            cmdStartAngleSub45.Location = new Point(48, 49);
            cmdStartAngleSub45.Name = "cmdStartAngleSub45";
            cmdStartAngleSub45.Padding = new Padding(5);
            cmdStartAngleSub45.Size = new Size(36, 20);
            cmdStartAngleSub45.TabIndex = 3;
            cmdStartAngleSub45.Text = "-45";
            cmdStartAngleSub45.Click += cmdStartAngleSub45_Click;
            // 
            // fraRadius
            // 
            fraRadius.Controls.Add(numRadius);
            fraRadius.Location = new Point(7, 6);
            fraRadius.Margin = new Padding(4, 3, 4, 3);
            fraRadius.Name = "fraRadius";
            fraRadius.Padding = new Padding(4, 3, 4, 3);
            fraRadius.Size = new Size(140, 52);
            fraRadius.TabIndex = 23;
            fraRadius.TabStop = false;
            fraRadius.Text = "Radius";
            // 
            // numRadius
            // 
            numRadius.Location = new Point(7, 22);
            numRadius.Margin = new Padding(4, 3, 4, 3);
            numRadius.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numRadius.Name = "numRadius";
            numRadius.Size = new Size(122, 23);
            numRadius.TabIndex = 0;
            numRadius.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // fraAmount2
            // 
            fraAmount2.Controls.Add(numAmount2);
            fraAmount2.Location = new Point(7, 223);
            fraAmount2.Margin = new Padding(4, 3, 4, 3);
            fraAmount2.Name = "fraAmount2";
            fraAmount2.Padding = new Padding(4, 3, 4, 3);
            fraAmount2.Size = new Size(85, 48);
            fraAmount2.TabIndex = 22;
            fraAmount2.TabStop = false;
            fraAmount2.Text = "Amount";
            // 
            // numAmount2
            // 
            numAmount2.Location = new Point(4, 18);
            numAmount2.Margin = new Padding(4, 3, 4, 3);
            numAmount2.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numAmount2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAmount2.Name = "numAmount2";
            numAmount2.Size = new Size(70, 23);
            numAmount2.TabIndex = 0;
            numAmount2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // InterpolatorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(31, 31, 32);
            ClientSize = new Size(347, 363);
            Controls.Add(tabControl1);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(363, 400);
            Name = "InterpolatorForm";
            Text = "Edit Path";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numZ).EndInit();
            fraFunction.ResumeLayout(false);
            fraPosition.ResumeLayout(false);
            fraPosition.PerformLayout();
            fraBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numEnd).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStart).EndInit();
            fraAmount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            fraTension.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numTension).EndInit();
            fraOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numOrder).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            fraCenter.ResumeLayout(false);
            fraCenter.PerformLayout();
            fraDegrees.ResumeLayout(false);
            fraDegrees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDegreeZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDegreeY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDegreeX).EndInit();
            fraStartAngle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numStartAngle).EndInit();
            fraRadius.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numRadius).EndInit();
            fraAmount2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numAmount2).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DarkButton cmdCancel;
        private DarkButton cmdOK;
        private DarkComboBox dpdFunc;
        private Label lblX;
        private Label lblY;
        private Label lblZ;
        private DarkNumericUpDown numX;
        private DarkNumericUpDown numY;
        private DarkNumericUpDown numZ;
        private Label lblAverage;
        private DarkGroupBox fraFunction;
        private DarkGroupBox fraPosition;
        private DarkButton cmdLast;
        private DarkButton cmdFirst;
        private DarkButton cmdPrev;
        private DarkButton cmdNext;
        private Label lblPosition;
        private DarkGroupBox fraBound;
        private DarkNumericUpDown numEnd;
        private DarkNumericUpDown numStart;
        private DarkGroupBox fraAmount;
        private DarkNumericUpDown numAmount;
        private DarkGroupBox fraTension;
        private DarkNumericUpDown numTension;
        private DarkGroupBox fraOrder;
        private DarkNumericUpDown numOrder;
        private MetroSetTabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DarkButton cmdOK2;
        private DarkButton cmdCancel2;
        private DarkGroupBox fraRadius;
        private DarkNumericUpDown numRadius;
        private DarkGroupBox fraAmount2;
        private DarkNumericUpDown numAmount2;
        private DarkGroupBox fraDegrees;
        private DarkNumericUpDown numDegreeX;
        private DarkNumericUpDown numDegreeZ;
        private DarkNumericUpDown numDegreeY;
        private Label lbDegreeY;
        private Label lbDegreeX;
        private Label lbDegreeZ;
        private DarkButton cmdDegXAdd45;
        private DarkButton cmdDegXSub45;
        private DarkButton cmdDegYSub45;
        private DarkButton cmdDegYAdd45;
        private DarkButton cmdDegZSub45;
        private DarkButton cmdDegZAdd45;
        private DarkNumericUpDown numStartAngle;
        private DarkGroupBox fraStartAngle;
        private DarkButton cmdStartAngleAdd45;
        private DarkButton cmdStartAngleSub45;
        private DarkRadioButton rdbPosition0;
        private DarkGroupBox fraCenter;
        private DarkRadioButton rdbCentral;
    }
}