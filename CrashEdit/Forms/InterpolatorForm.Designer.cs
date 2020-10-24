namespace CrashEdit
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
            this.cmdCancel = new DarkUI.Controls.DarkButton();
            this.cmdOK = new DarkUI.Controls.DarkButton();
            this.dpdFunc = new DarkUI.Controls.DarkComboBox();
            this.lblX = new DarkUI.Controls.DarkLabel();
            this.lblY = new DarkUI.Controls.DarkLabel();
            this.lblZ = new DarkUI.Controls.DarkLabel();
            this.numX = new DarkUI.Controls.DarkNumericUpDown();
            this.numY = new DarkUI.Controls.DarkNumericUpDown();
            this.numZ = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAverage = new DarkUI.Controls.DarkLabel();
            this.fraFunction = new System.Windows.Forms.GroupBox();
            this.fraPosition = new System.Windows.Forms.GroupBox();
            this.lblPosition = new DarkUI.Controls.DarkLabel();
            this.cmdNext = new DarkUI.Controls.DarkButton();
            this.cmdPrev = new DarkUI.Controls.DarkButton();
            this.cmdLast = new DarkUI.Controls.DarkButton();
            this.cmdFirst = new DarkUI.Controls.DarkButton();
            this.fraBound = new System.Windows.Forms.GroupBox();
            this.numEnd = new DarkUI.Controls.DarkNumericUpDown();
            this.numStart = new DarkUI.Controls.DarkNumericUpDown();
            this.fraAmount = new System.Windows.Forms.GroupBox();
            this.numAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.fraTension = new System.Windows.Forms.GroupBox();
            this.numTension = new DarkUI.Controls.DarkNumericUpDown();
            this.fraOrder = new System.Windows.Forms.GroupBox();
            this.numOrder = new DarkUI.Controls.DarkNumericUpDown();
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
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(226, 306);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdCancel.Size = new System.Drawing.Size(87, 26);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(132, 306);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdOK.Size = new System.Drawing.Size(87, 26);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "Interpolate!";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // dpdFunc
            // 
            this.dpdFunc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.dpdFunc.FormattingEnabled = true;
            this.dpdFunc.Location = new System.Drawing.Point(7, 22);
            this.dpdFunc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dpdFunc.Name = "dpdFunc";
            this.dpdFunc.Size = new System.Drawing.Size(140, 22);
            this.dpdFunc.TabIndex = 5;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblX.Location = new System.Drawing.Point(7, 40);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 15);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblY.Location = new System.Drawing.Point(7, 70);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 15);
            this.lblY.TabIndex = 1;
            this.lblY.Text = "Y";
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblZ.Location = new System.Drawing.Point(7, 100);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 15);
            this.lblZ.TabIndex = 2;
            this.lblZ.Text = "Z";
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(30, 38);
            this.numX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numX.Size = new System.Drawing.Size(100, 21);
            this.numX.TabIndex = 3;
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(30, 68);
            this.numY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numY.Size = new System.Drawing.Size(100, 21);
            this.numY.TabIndex = 4;
            // 
            // numZ
            // 
            this.numZ.Location = new System.Drawing.Point(30, 98);
            this.numZ.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numZ.Size = new System.Drawing.Size(100, 21);
            this.numZ.TabIndex = 5;
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblAverage.Location = new System.Drawing.Point(14, 282);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(144, 15);
            this.lblAverage.TabIndex = 11;
            this.lblAverage.Text = "Average Point Distance: -";
            // 
            // fraFunction
            // 
            this.fraFunction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fraFunction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraFunction.Controls.Add(this.dpdFunc);
            this.fraFunction.ForeColor = System.Drawing.SystemColors.Control;
            this.fraFunction.Location = new System.Drawing.Point(159, 69);
            this.fraFunction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraFunction.Name = "fraFunction";
            this.fraFunction.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraFunction.Size = new System.Drawing.Size(155, 60);
            this.fraFunction.TabIndex = 0;
            this.fraFunction.TabStop = false;
            this.fraFunction.Text = "Progress Function";
            // 
            // fraPosition
            // 
            this.fraPosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fraPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
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
            this.fraPosition.ForeColor = System.Drawing.SystemColors.Control;
            this.fraPosition.Location = new System.Drawing.Point(14, 14);
            this.fraPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraPosition.Name = "fraPosition";
            this.fraPosition.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraPosition.Size = new System.Drawing.Size(138, 210);
            this.fraPosition.TabIndex = 6;
            this.fraPosition.TabStop = false;
            this.fraPosition.Text = "Positions";
            // 
            // lblPosition
            // 
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblPosition.Location = new System.Drawing.Point(7, 19);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(124, 16);
            this.lblPosition.TabIndex = 6;
            this.lblPosition.Text = "?? / ??";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(72, 129);
            this.cmdNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdNext.Size = new System.Drawing.Size(58, 26);
            this.cmdNext.TabIndex = 0;
            this.cmdNext.Text = "Next";
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.Location = new System.Drawing.Point(7, 129);
            this.cmdPrev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdPrev.Size = new System.Drawing.Size(58, 26);
            this.cmdPrev.TabIndex = 1;
            this.cmdPrev.Text = "Prev";
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrev_Click);
            // 
            // cmdLast
            // 
            this.cmdLast.Location = new System.Drawing.Point(72, 176);
            this.cmdLast.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdLast.Name = "cmdLast";
            this.cmdLast.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdLast.Size = new System.Drawing.Size(58, 26);
            this.cmdLast.TabIndex = 3;
            this.cmdLast.Text = "Last";
            this.cmdLast.Click += new System.EventHandler(this.cmdLast_Click);
            // 
            // cmdFirst
            // 
            this.cmdFirst.Location = new System.Drawing.Point(7, 176);
            this.cmdFirst.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdFirst.Name = "cmdFirst";
            this.cmdFirst.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdFirst.Size = new System.Drawing.Size(58, 26);
            this.cmdFirst.TabIndex = 2;
            this.cmdFirst.Text = "First";
            this.cmdFirst.Click += new System.EventHandler(this.cmdFirst_Click);
            // 
            // fraBound
            // 
            this.fraBound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraBound.Controls.Add(this.numEnd);
            this.fraBound.Controls.Add(this.numStart);
            this.fraBound.ForeColor = System.Drawing.SystemColors.Control;
            this.fraBound.Location = new System.Drawing.Point(159, 14);
            this.fraBound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraBound.Name = "fraBound";
            this.fraBound.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraBound.Size = new System.Drawing.Size(155, 49);
            this.fraBound.TabIndex = 12;
            this.fraBound.TabStop = false;
            this.fraBound.Text = "Start/End Position";
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(80, 18);
            this.numEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numEnd.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(70, 21);
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
            this.numStart.Location = new System.Drawing.Point(3, 18);
            this.numStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(70, 21);
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
            this.fraAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraAmount.Controls.Add(this.numAmount);
            this.fraAmount.ForeColor = System.Drawing.SystemColors.Control;
            this.fraAmount.Location = new System.Drawing.Point(14, 231);
            this.fraAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraAmount.Name = "fraAmount";
            this.fraAmount.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraAmount.Size = new System.Drawing.Size(85, 49);
            this.fraAmount.TabIndex = 13;
            this.fraAmount.TabStop = false;
            this.fraAmount.Text = "Amount";
            // 
            // numAmount
            // 
            this.numAmount.Location = new System.Drawing.Point(7, 18);
            this.numAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numAmount.Size = new System.Drawing.Size(71, 21);
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
            this.fraTension.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraTension.Controls.Add(this.numTension);
            this.fraTension.ForeColor = System.Drawing.SystemColors.Control;
            this.fraTension.Location = new System.Drawing.Point(159, 136);
            this.fraTension.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraTension.Name = "fraTension";
            this.fraTension.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraTension.Size = new System.Drawing.Size(155, 52);
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
            this.numTension.Location = new System.Drawing.Point(7, 21);
            this.numTension.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numTension.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numTension.Name = "numTension";
            this.numTension.Size = new System.Drawing.Size(141, 21);
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
            this.fraOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraOrder.Controls.Add(this.numOrder);
            this.fraOrder.ForeColor = System.Drawing.SystemColors.Control;
            this.fraOrder.Location = new System.Drawing.Point(159, 196);
            this.fraOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraOrder.Name = "fraOrder";
            this.fraOrder.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraOrder.Size = new System.Drawing.Size(155, 52);
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
            this.numOrder.Location = new System.Drawing.Point(7, 21);
            this.numOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numOrder.Size = new System.Drawing.Size(141, 21);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(328, 344);
            this.Controls.Add(this.fraOrder);
            this.Controls.Add(this.fraTension);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.fraAmount);
            this.Controls.Add(this.fraBound);
            this.Controls.Add(this.fraFunction);
            this.Controls.Add(this.fraPosition);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Font = new System.Drawing.Font("Yu Gothic UI ", 9F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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

        private DarkUI.Controls.DarkButton cmdCancel;
        private DarkUI.Controls.DarkButton cmdOK;
        private DarkUI.Controls.DarkComboBox dpdFunc;
        private DarkUI.Controls.DarkLabel lblX;
        private DarkUI.Controls.DarkLabel lblY;
        private DarkUI.Controls.DarkLabel lblZ;
        private DarkUI.Controls.DarkNumericUpDown numX;
        private DarkUI.Controls.DarkNumericUpDown numY;
        private DarkUI.Controls.DarkNumericUpDown numZ;
        private DarkUI.Controls.DarkLabel lblAverage;
        private System.Windows.Forms.GroupBox fraFunction;
        private System.Windows.Forms.GroupBox fraPosition;
        private DarkUI.Controls.DarkButton cmdLast;
        private DarkUI.Controls.DarkButton cmdFirst;
        private DarkUI.Controls.DarkButton cmdPrev;
        private DarkUI.Controls.DarkButton cmdNext;
        private DarkUI.Controls.DarkLabel lblPosition;
        private System.Windows.Forms.GroupBox fraBound;
        private DarkUI.Controls.DarkNumericUpDown numEnd;
        private DarkUI.Controls.DarkNumericUpDown numStart;
        private System.Windows.Forms.GroupBox fraAmount;
        private DarkUI.Controls.DarkNumericUpDown numAmount;
        private System.Windows.Forms.GroupBox fraTension;
        private DarkUI.Controls.DarkNumericUpDown numTension;
        private System.Windows.Forms.GroupBox fraOrder;
        private DarkUI.Controls.DarkNumericUpDown numOrder;
    }
}