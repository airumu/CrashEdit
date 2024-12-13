using AltUI.Controls;

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
            this.cmdCancel = new DarkButton();
            this.cmdOK = new DarkButton();
            this.dpdFunc = new DarkComboBox();
            this.lblX = new Label();
            this.lblY = new Label();
            this.lblZ = new Label();
            this.numX = new DarkNumericUpDown();
            this.numY = new DarkNumericUpDown();
            this.numZ = new DarkNumericUpDown();
            this.lblAverage = new Label();
            this.fraFunction = new DarkGroupBox();
            this.fraPosition = new DarkGroupBox();
            this.lblPosition = new Label();
            this.cmdNext = new DarkButton();
            this.cmdPrev = new DarkButton();
            this.cmdLast = new DarkButton();
            this.cmdFirst = new DarkButton();
            this.fraBound = new DarkGroupBox();
            this.numEnd = new DarkNumericUpDown();
            this.numStart = new DarkNumericUpDown();
            this.fraAmount = new DarkGroupBox();
            this.numAmount = new DarkNumericUpDown();
            this.fraTension = new DarkGroupBox();
            this.numTension = new DarkNumericUpDown();
            this.fraOrder = new DarkGroupBox();
            this.numOrder = new DarkNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            this.fraFunction.SuspendLayout();
            this.fraPosition.SuspendLayout();
            this.fraBound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            this.fraAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.fraTension.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTension)).BeginInit();
            this.fraOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | AnchorStyles.Right)));
            this.cmdCancel.DialogResult = DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(194, 265);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(113, 265);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Interpolate!";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // dpdFunc
            // 
            this.dpdFunc.DropDownStyle = ComboBoxStyle.DropDownList;
            this.dpdFunc.FormattingEnabled = true;
            this.dpdFunc.Location = new System.Drawing.Point(6, 19);
            this.dpdFunc.Name = "dpdFunc";
            this.dpdFunc.Size = new System.Drawing.Size(121, 21);
            this.dpdFunc.TabIndex = 5;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(6, 35);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(6, 61);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 1;
            this.lblY.Text = "Y";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.Location = new System.Drawing.Point(6, 87);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 13);
            this.lblZ.TabIndex = 2;
            this.lblZ.Text = "Z";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(26, 33);
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
            this.numX.ReadOnly = true;
            this.numX.Size = new System.Drawing.Size(86, 20);
            this.numX.TabIndex = 3;
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(26, 59);
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
            this.numY.ReadOnly = true;
            this.numY.Size = new System.Drawing.Size(86, 20);
            this.numY.TabIndex = 4;
            // 
            // numZ
            // 
            this.numZ.Location = new System.Drawing.Point(26, 85);
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
            this.numZ.ReadOnly = true;
            this.numZ.Size = new System.Drawing.Size(86, 20);
            this.numZ.TabIndex = 5;
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Location = new System.Drawing.Point(12, 245);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(128, 13);
            this.lblAverage.TabIndex = 11;
            this.lblAverage.Text = "Average Point Distance: -";
            // 
            // fraFunction
            // 
            this.fraFunction.AutoSize = true;
            this.fraFunction.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fraFunction.Controls.Add(this.dpdFunc);
            this.fraFunction.Location = new System.Drawing.Point(136, 60);
            this.fraFunction.Name = "fraFunction";
            this.fraFunction.Size = new System.Drawing.Size(133, 59);
            this.fraFunction.TabIndex = 0;
            this.fraFunction.TabStop = false;
            this.fraFunction.Text = "Progress Function";
            // 
            // fraPosition
            // 
            this.fraPosition.AutoSize = true;
            this.fraPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.fraPosition.Controls.Add(this.lblPosition);
            this.fraPosition.Controls.Add(this.cmdNext);
            this.fraPosition.Controls.Add(this.lblX);
            this.fraPosition.Controls.Add(this.cmdPrev);
            this.fraPosition.Controls.Add(this.lblY);
            this.fraPosition.Controls.Add(this.cmdLast);
            this.fraPosition.Controls.Add(this.lblZ);
            this.fraPosition.Controls.Add(this.cmdFirst);
            this.fraPosition.Controls.Add(this.numX);
            this.fraPosition.Controls.Add(this.numZ);
            this.fraPosition.Controls.Add(this.numY);
            this.fraPosition.Location = new System.Drawing.Point(12, 12);
            this.fraPosition.Name = "fraPosition";
            this.fraPosition.Size = new System.Drawing.Size(118, 182);
            this.fraPosition.TabIndex = 6;
            this.fraPosition.TabStop = false;
            this.fraPosition.Text = "Positions";
            // 
            // lblPosition
            // 
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Location = new System.Drawing.Point(6, 16);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(106, 14);
            this.lblPosition.TabIndex = 6;
            this.lblPosition.Text = "?? / ??";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(62, 111);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(50, 23);
            this.cmdNext.TabIndex = 0;
            this.cmdNext.Text = "Next";
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.Location = new System.Drawing.Point(6, 111);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(50, 23);
            this.cmdPrev.TabIndex = 1;
            this.cmdPrev.Text = "Prev";
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrev_Click);
            // 
            // cmdLast
            // 
            this.cmdLast.Location = new System.Drawing.Point(62, 140);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Size = new System.Drawing.Size(50, 23);
            this.cmdLast.TabIndex = 3;
            this.cmdLast.Text = "Last";
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdFirst
            // 
            this.cmdFirst.Location = new System.Drawing.Point(6, 140);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Size = new System.Drawing.Size(50, 23);
            this.cmdFirst.TabIndex = 2;
            this.cmdFirst.Text = "First";
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // fraBound
            // 
            this.fraBound.Controls.Add(this.numEnd);
            this.fraBound.Controls.Add(this.numStart);
            this.fraBound.Location = new System.Drawing.Point(136, 12);
            this.fraBound.Name = "fraBound";
            this.fraBound.Size = new System.Drawing.Size(133, 42);
            this.fraBound.TabIndex = 12;
            this.fraBound.TabStop = false;
            this.fraBound.Text = "Start/End Position";
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(69, 16);
            this.numEnd.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(60, 20);
            this.numEnd.TabIndex = 1;
            this.numEnd.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numEnd.ValueChanged += new System.EventHandler(this.numEnd_ValueChanged);
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(3, 16);
            this.numStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(60, 20);
            this.numStart.TabIndex = 0;
            this.numStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStart.ValueChanged += new System.EventHandler(this.numStart_ValueChanged);
            // 
            // fraAmount
            // 
            this.fraAmount.Controls.Add(this.numAmount);
            this.fraAmount.Location = new System.Drawing.Point(12, 200);
            this.fraAmount.Name = "fraAmount";
            this.fraAmount.Size = new System.Drawing.Size(73, 42);
            this.fraAmount.TabIndex = 13;
            this.fraAmount.TabStop = false;
            this.fraAmount.Text = "Amount";
            // 
            // numAmount
            // 
            this.numAmount.Location = new System.Drawing.Point(3, 16);
            this.numAmount.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(60, 20);
            this.numAmount.TabIndex = 0;
            this.numAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);
            // 
            // fraTension
            // 
            this.fraTension.Controls.Add(this.numTension);
            this.fraTension.Location = new System.Drawing.Point(136, 125);
            this.fraTension.Name = "fraTension";
            this.fraTension.Size = new System.Drawing.Size(133, 45);
            this.fraTension.TabIndex = 14;
            this.fraTension.TabStop = false;
            this.fraTension.Text = "Tension";
            // 
            // numTension
            // 
            this.numTension.DecimalPlaces = 2;
            this.numTension.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numTension.Location = new System.Drawing.Point(6, 19);
            this.numTension.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numTension.Name = "numTension";
            this.numTension.Size = new System.Drawing.Size(121, 20);
            this.numTension.TabIndex = 0;
            this.numTension.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTension.ValueChanged += new System.EventHandler(this.numTension_ValueChanged);
            // 
            // fraOrder
            // 
            this.fraOrder.Controls.Add(this.numOrder);
            this.fraOrder.Location = new System.Drawing.Point(136, 176);
            this.fraOrder.Name = "fraOrder";
            this.fraOrder.Size = new System.Drawing.Size(133, 45);
            this.fraOrder.TabIndex = 15;
            this.fraOrder.TabStop = false;
            this.fraOrder.Text = "Order";
            // 
            // numOrder
            // 
            this.numOrder.DecimalPlaces = 3;
            this.numOrder.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numOrder.Location = new System.Drawing.Point(6, 19);
            this.numOrder.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numOrder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOrder.Name = "numOrder";
            this.numOrder.Size = new System.Drawing.Size(121, 20);
            this.numOrder.TabIndex = 0;
            this.numOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOrder.ValueChanged += new System.EventHandler(this.numOrder_ValueChanged);
            // 
            // InterpolatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(281, 298);
            this.Controls.Add(this.fraOrder);
            this.Controls.Add(this.fraTension);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.fraAmount);
            this.Controls.Add(this.fraBound);
            this.Controls.Add(this.fraFunction);
            this.Controls.Add(this.fraPosition);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "InterpolatorForm";
            this.Text = "Interpolate Path";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            this.fraFunction.ResumeLayout(false);
            this.fraPosition.ResumeLayout(false);
            this.fraPosition.PerformLayout();
            this.fraBound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            this.fraAmount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.fraTension.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTension)).EndInit();
            this.fraOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}