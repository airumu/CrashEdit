using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class FrameBox
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
            fraVertice = new DarkGroupBox();
            fraNearbyVertices = new DarkGroupBox();
            lbDistance = new DarkLabel();
            lstNearbyVertices = new DarkListBox();
            chkEditNearbyVertices = new CheckBox();
            numDistance = new DarkNumericUpDown();
            pnVertexControls = new Panel();
            cmdAppendVertice = new DarkButton();
            cmdRemoveVertice = new DarkButton();
            lblSPVertex = new Label();
            cmdNext10Vertice = new DarkButton();
            cmdPrevious10Vertice = new DarkButton();
            cmdLastVertice = new DarkButton();
            cmdFirstVertice = new DarkButton();
            lblVerticeIndex = new Label();
            cmdNextVertice = new DarkButton();
            cmdPreviousVertice = new DarkButton();
            lblZ = new Label();
            lblY = new Label();
            lblX = new Label();
            numZ = new DarkNumericUpDown();
            numY = new DarkNumericUpDown();
            numX = new DarkNumericUpDown();
            cmdInsertVertice = new DarkButton();
            fraGG = new DarkGroupBox();
            lblZG = new Label();
            lblYG = new Label();
            lblXG = new Label();
            numZG = new DarkNumericUpDown();
            numYG = new DarkNumericUpDown();
            numXG = new DarkNumericUpDown();
            fraG2 = new DarkGroupBox();
            lblZ2 = new Label();
            lblY2 = new Label();
            lblX2 = new Label();
            numZ2 = new DarkNumericUpDown();
            numY2 = new DarkNumericUpDown();
            numX2 = new DarkNumericUpDown();
            fraG1 = new DarkGroupBox();
            lblZ1 = new Label();
            lblY1 = new Label();
            lblX1 = new Label();
            numZ1 = new DarkNumericUpDown();
            numY1 = new DarkNumericUpDown();
            numX1 = new DarkNumericUpDown();
            fraOffset = new DarkGroupBox();
            lblZOffset = new Label();
            lblYOffset = new Label();
            lblXOffset = new Label();
            numZOffset = new DarkNumericUpDown();
            numYOffset = new DarkNumericUpDown();
            numXOffset = new DarkNumericUpDown();
            cmdPreviousCollision = new DarkButton();
            cmdNextCollision = new DarkButton();
            lblCollisionIndex = new Label();
            cmdAppendCollision = new DarkButton();
            fraCollision = new DarkGroupBox();
            cmdRemoveCollision = new DarkButton();
            cmdInsertCollision = new DarkButton();
            numHeader = new DarkNumericUpDown();
            groupBox1 = new DarkGroupBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            groupBox2 = new DarkGroupBox();
            numSPVertex = new DarkNumericUpDown();
            darkGroupBox1 = new DarkGroupBox();
            txtModel = new DarkTextBox();
            lblEIDError = new Label();
            chkSyncFrames = new CheckBox();
            cmdCopyCollision = new DarkButton();
            fraCopy = new DarkGroupBox();
            cmdCopyOffset = new DarkButton();
            pnFrameBox = new Panel();
            fraMisc = new DarkGroupBox();
            cmdRotateZ = new DarkButton();
            cmdRotateY = new DarkButton();
            cmdRotateX = new DarkButton();
            cmdMisc = new CheckBox();
            fraVertice.SuspendLayout();
            fraNearbyVertices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDistance).BeginInit();
            pnVertexControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            fraGG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numXG).BeginInit();
            fraG2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX2).BeginInit();
            fraG1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX1).BeginInit();
            fraOffset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZOffset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYOffset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numXOffset).BeginInit();
            fraCollision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numHeader).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSPVertex).BeginInit();
            darkGroupBox1.SuspendLayout();
            fraCopy.SuspendLayout();
            pnFrameBox.SuspendLayout();
            fraMisc.SuspendLayout();
            SuspendLayout();
            // 
            // fraVertice
            // 
            fraVertice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraVertice.BackColor = Color.Transparent;
            fraVertice.Controls.Add(fraNearbyVertices);
            fraVertice.Controls.Add(pnVertexControls);
            fraVertice.Controls.Add(lblSPVertex);
            fraVertice.Controls.Add(cmdNext10Vertice);
            fraVertice.Controls.Add(cmdPrevious10Vertice);
            fraVertice.Controls.Add(cmdLastVertice);
            fraVertice.Controls.Add(cmdFirstVertice);
            fraVertice.Controls.Add(lblVerticeIndex);
            fraVertice.Controls.Add(cmdNextVertice);
            fraVertice.Controls.Add(cmdPreviousVertice);
            fraVertice.Controls.Add(lblZ);
            fraVertice.Controls.Add(lblY);
            fraVertice.Controls.Add(lblX);
            fraVertice.Controls.Add(numZ);
            fraVertice.Controls.Add(numY);
            fraVertice.Controls.Add(numX);
            fraVertice.Font = new Font("Segoe UI", 9F);
            fraVertice.ForeColor = Color.Silver;
            fraVertice.Location = new Point(3, 4);
            fraVertice.Margin = new Padding(3, 4, 3, 4);
            fraVertice.Name = "fraVertice";
            fraVertice.Padding = new Padding(3, 4, 3, 4);
            fraVertice.Size = new Size(612, 175);
            fraVertice.TabIndex = 1;
            fraVertice.TabStop = false;
            fraVertice.Text = "Vertice(s)";
            // 
            // fraNearbyVertices
            // 
            fraNearbyVertices.Controls.Add(lbDistance);
            fraNearbyVertices.Controls.Add(lstNearbyVertices);
            fraNearbyVertices.Controls.Add(chkEditNearbyVertices);
            fraNearbyVertices.Controls.Add(numDistance);
            fraNearbyVertices.Location = new Point(302, 19);
            fraNearbyVertices.Name = "fraNearbyVertices";
            fraNearbyVertices.Size = new Size(200, 149);
            fraNearbyVertices.TabIndex = 23;
            fraNearbyVertices.TabStop = false;
            fraNearbyVertices.Text = "Edit Nearby Vertices";
            // 
            // lbDistance
            // 
            lbDistance.AutoSize = true;
            lbDistance.Location = new Point(98, 84);
            lbDistance.Name = "lbDistance";
            lbDistance.Size = new Size(59, 15);
            lbDistance.TabIndex = 23;
            lbDistance.Text = "Threshold";
            // 
            // lstNearbyVertices
            // 
            lstNearbyVertices.BackColor = Color.FromArgb(26, 26, 28);
            lstNearbyVertices.BorderStyle = BorderStyle.FixedSingle;
            lstNearbyVertices.ForeColor = Color.FromArgb(213, 213, 213);
            lstNearbyVertices.FormattingEnabled = true;
            lstNearbyVertices.Location = new Point(6, 22);
            lstNearbyVertices.Name = "lstNearbyVertices";
            lstNearbyVertices.Size = new Size(86, 122);
            lstNearbyVertices.TabIndex = 22;
            lstNearbyVertices.SelectedIndexChanged += lstNearbyVertices_SelectedIndexChanged;
            // 
            // chkEditNearbyVertices
            // 
            chkEditNearbyVertices.AutoSize = true;
            chkEditNearbyVertices.BackColor = Color.Transparent;
            chkEditNearbyVertices.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkEditNearbyVertices.ForeColor = SystemColors.ControlText;
            chkEditNearbyVertices.Location = new Point(98, 21);
            chkEditNearbyVertices.Name = "chkEditNearbyVertices";
            chkEditNearbyVertices.Size = new Size(68, 19);
            chkEditNearbyVertices.TabIndex = 18;
            chkEditNearbyVertices.Text = "Enabled";
            chkEditNearbyVertices.UseVisualStyleBackColor = false;
            // 
            // numDistance
            // 
            numDistance.Location = new Point(98, 103);
            numDistance.Margin = new Padding(3, 4, 3, 4);
            numDistance.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            numDistance.Name = "numDistance";
            numDistance.Size = new Size(85, 23);
            numDistance.TabIndex = 2;
            numDistance.ValueChanged += numDistance_ValueChanged;
            // 
            // pnVertexControls
            // 
            pnVertexControls.Controls.Add(cmdAppendVertice);
            pnVertexControls.Controls.Add(cmdRemoveVertice);
            pnVertexControls.Location = new Point(514, 19);
            pnVertexControls.Name = "pnVertexControls";
            pnVertexControls.Size = new Size(92, 102);
            pnVertexControls.TabIndex = 21;
            // 
            // cmdAppendVertice
            // 
            cmdAppendVertice.BorderColour = Color.Empty;
            cmdAppendVertice.CustomColour = false;
            cmdAppendVertice.FlatBottom = false;
            cmdAppendVertice.FlatTop = false;
            cmdAppendVertice.Font = new Font("Segoe UI", 9F);
            cmdAppendVertice.ForeColor = SystemColors.ControlText;
            cmdAppendVertice.Location = new Point(3, 4);
            cmdAppendVertice.Margin = new Padding(3, 4, 3, 4);
            cmdAppendVertice.Name = "cmdAppendVertice";
            cmdAppendVertice.Padding = new Padding(5);
            cmdAppendVertice.Size = new Size(86, 26);
            cmdAppendVertice.TabIndex = 5;
            cmdAppendVertice.Text = "Append";
            cmdAppendVertice.Click += cmdAppendVertice_Click;
            // 
            // cmdRemoveVertice
            // 
            cmdRemoveVertice.BorderColour = Color.Empty;
            cmdRemoveVertice.CustomColour = false;
            cmdRemoveVertice.FlatBottom = false;
            cmdRemoveVertice.FlatTop = false;
            cmdRemoveVertice.Font = new Font("Segoe UI", 9F);
            cmdRemoveVertice.ForeColor = SystemColors.ControlText;
            cmdRemoveVertice.Location = new Point(4, 37);
            cmdRemoveVertice.Margin = new Padding(3, 4, 3, 4);
            cmdRemoveVertice.Name = "cmdRemoveVertice";
            cmdRemoveVertice.Padding = new Padding(5);
            cmdRemoveVertice.Size = new Size(86, 26);
            cmdRemoveVertice.TabIndex = 7;
            cmdRemoveVertice.Text = "Remove";
            cmdRemoveVertice.Click += cmdRemoveVertice_Click;
            // 
            // lblSPVertex
            // 
            lblSPVertex.AutoSize = true;
            lblSPVertex.BackColor = Color.Transparent;
            lblSPVertex.ForeColor = Color.MediumTurquoise;
            lblSPVertex.Location = new Point(23, 48);
            lblSPVertex.Name = "lblSPVertex";
            lblSPVertex.Size = new Size(79, 15);
            lblSPVertex.TabIndex = 12;
            lblSPVertex.Text = "Special Vertex";
            // 
            // cmdNext10Vertice
            // 
            cmdNext10Vertice.BorderColour = Color.Empty;
            cmdNext10Vertice.CustomColour = false;
            cmdNext10Vertice.FlatBottom = false;
            cmdNext10Vertice.FlatTop = false;
            cmdNext10Vertice.ForeColor = SystemColors.ControlText;
            cmdNext10Vertice.Location = new Point(216, 56);
            cmdNext10Vertice.Margin = new Padding(3, 4, 3, 4);
            cmdNext10Vertice.Name = "cmdNext10Vertice";
            cmdNext10Vertice.Padding = new Padding(5);
            cmdNext10Vertice.Size = new Size(80, 26);
            cmdNext10Vertice.TabIndex = 11;
            cmdNext10Vertice.Text = "Next 10";
            cmdNext10Vertice.Click += CmdNext10Vertice_Click;
            // 
            // cmdPrevious10Vertice
            // 
            cmdPrevious10Vertice.BorderColour = Color.Empty;
            cmdPrevious10Vertice.CustomColour = false;
            cmdPrevious10Vertice.FlatBottom = false;
            cmdPrevious10Vertice.FlatTop = false;
            cmdPrevious10Vertice.ForeColor = SystemColors.ControlText;
            cmdPrevious10Vertice.Location = new Point(130, 56);
            cmdPrevious10Vertice.Margin = new Padding(3, 4, 3, 4);
            cmdPrevious10Vertice.Name = "cmdPrevious10Vertice";
            cmdPrevious10Vertice.Padding = new Padding(5);
            cmdPrevious10Vertice.Size = new Size(80, 26);
            cmdPrevious10Vertice.TabIndex = 10;
            cmdPrevious10Vertice.Text = "Previous 10";
            cmdPrevious10Vertice.Click += CmdPrevious10Vertice_Click;
            // 
            // cmdLastVertice
            // 
            cmdLastVertice.BorderColour = Color.Empty;
            cmdLastVertice.CustomColour = false;
            cmdLastVertice.FlatBottom = false;
            cmdLastVertice.FlatTop = false;
            cmdLastVertice.ForeColor = SystemColors.ControlText;
            cmdLastVertice.Location = new Point(216, 122);
            cmdLastVertice.Margin = new Padding(3, 4, 3, 4);
            cmdLastVertice.Name = "cmdLastVertice";
            cmdLastVertice.Padding = new Padding(5);
            cmdLastVertice.Size = new Size(79, 26);
            cmdLastVertice.TabIndex = 9;
            cmdLastVertice.Text = "Last";
            cmdLastVertice.Click += cmdLastVertice_Click;
            // 
            // cmdFirstVertice
            // 
            cmdFirstVertice.BorderColour = Color.Empty;
            cmdFirstVertice.CustomColour = false;
            cmdFirstVertice.FlatBottom = false;
            cmdFirstVertice.FlatTop = false;
            cmdFirstVertice.ForeColor = SystemColors.ControlText;
            cmdFirstVertice.Location = new Point(130, 122);
            cmdFirstVertice.Margin = new Padding(3, 4, 3, 4);
            cmdFirstVertice.Name = "cmdFirstVertice";
            cmdFirstVertice.Padding = new Padding(5);
            cmdFirstVertice.Size = new Size(80, 26);
            cmdFirstVertice.TabIndex = 8;
            cmdFirstVertice.Text = "First";
            cmdFirstVertice.Click += cmdFirstVertice_Click;
            // 
            // lblVerticeIndex
            // 
            lblVerticeIndex.BackColor = Color.Transparent;
            lblVerticeIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVerticeIndex.ForeColor = SystemColors.ControlText;
            lblVerticeIndex.Location = new Point(27, 22);
            lblVerticeIndex.Name = "lblVerticeIndex";
            lblVerticeIndex.Size = new Size(70, 26);
            lblVerticeIndex.TabIndex = 5;
            lblVerticeIndex.Text = "?? / ??";
            lblVerticeIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdNextVertice
            // 
            cmdNextVertice.BorderColour = Color.Empty;
            cmdNextVertice.CustomColour = false;
            cmdNextVertice.FlatBottom = false;
            cmdNextVertice.FlatTop = false;
            cmdNextVertice.ForeColor = SystemColors.ControlText;
            cmdNextVertice.Location = new Point(216, 22);
            cmdNextVertice.Margin = new Padding(3, 4, 3, 4);
            cmdNextVertice.Name = "cmdNextVertice";
            cmdNextVertice.Padding = new Padding(5);
            cmdNextVertice.Size = new Size(80, 26);
            cmdNextVertice.TabIndex = 1;
            cmdNextVertice.Text = "Next";
            cmdNextVertice.Click += cmdNextVertice_Click;
            // 
            // cmdPreviousVertice
            // 
            cmdPreviousVertice.BorderColour = Color.Empty;
            cmdPreviousVertice.CustomColour = false;
            cmdPreviousVertice.FlatBottom = false;
            cmdPreviousVertice.FlatTop = false;
            cmdPreviousVertice.ForeColor = SystemColors.ControlText;
            cmdPreviousVertice.Location = new Point(130, 22);
            cmdPreviousVertice.Margin = new Padding(3, 4, 3, 4);
            cmdPreviousVertice.Name = "cmdPreviousVertice";
            cmdPreviousVertice.Padding = new Padding(5);
            cmdPreviousVertice.Size = new Size(80, 26);
            cmdPreviousVertice.TabIndex = 0;
            cmdPreviousVertice.Text = "Previous";
            cmdPreviousVertice.Click += cmdPreviousVertice_Click;
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.BackColor = Color.Transparent;
            lblZ.ForeColor = SystemColors.ControlText;
            lblZ.Location = new Point(7, 130);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(14, 15);
            lblZ.TabIndex = 5;
            lblZ.Text = "Z";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.BackColor = Color.Transparent;
            lblY.ForeColor = SystemColors.ControlText;
            lblY.Location = new Point(7, 100);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 4;
            lblY.Text = "Y";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.ForeColor = SystemColors.ControlText;
            lblX.Location = new Point(7, 70);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 3;
            lblX.Text = "X";
            // 
            // numZ
            // 
            numZ.Increment = new decimal(new int[] { 8, 0, 0, 0 });
            numZ.Location = new Point(30, 128);
            numZ.Margin = new Padding(3, 4, 3, 4);
            numZ.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numZ.Name = "numZ";
            numZ.Size = new Size(85, 23);
            numZ.TabIndex = 4;
            numZ.ValueChanged += numZ_ValueChanged;
            // 
            // numY
            // 
            numY.Increment = new decimal(new int[] { 8, 0, 0, 0 });
            numY.Location = new Point(30, 98);
            numY.Margin = new Padding(3, 4, 3, 4);
            numY.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numY.Name = "numY";
            numY.Size = new Size(85, 23);
            numY.TabIndex = 3;
            numY.ValueChanged += numY_ValueChanged;
            // 
            // numX
            // 
            numX.Increment = new decimal(new int[] { 8, 0, 0, 0 });
            numX.Location = new Point(30, 68);
            numX.Margin = new Padding(3, 4, 3, 4);
            numX.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numX.Name = "numX";
            numX.Size = new Size(85, 23);
            numX.TabIndex = 2;
            numX.ValueChanged += numX_ValueChanged;
            // 
            // cmdInsertVertice
            // 
            cmdInsertVertice.BorderColour = Color.Empty;
            cmdInsertVertice.CustomColour = false;
            cmdInsertVertice.FlatBottom = false;
            cmdInsertVertice.FlatTop = false;
            cmdInsertVertice.Font = new Font("Segoe UI", 9F);
            cmdInsertVertice.ForeColor = SystemColors.ControlText;
            cmdInsertVertice.Location = new Point(621, 4);
            cmdInsertVertice.Margin = new Padding(3, 4, 3, 4);
            cmdInsertVertice.Name = "cmdInsertVertice";
            cmdInsertVertice.Padding = new Padding(5);
            cmdInsertVertice.Size = new Size(86, 26);
            cmdInsertVertice.TabIndex = 6;
            cmdInsertVertice.Text = "Insert";
            cmdInsertVertice.Visible = false;
            cmdInsertVertice.Click += cmdInsertVertice_Click;
            // 
            // fraGG
            // 
            fraGG.BackColor = Color.FromArgb(32, 32, 40);
            fraGG.Controls.Add(lblZG);
            fraGG.Controls.Add(lblYG);
            fraGG.Controls.Add(lblXG);
            fraGG.Controls.Add(numZG);
            fraGG.Controls.Add(numYG);
            fraGG.Controls.Add(numXG);
            fraGG.ForeColor = Color.Gainsboro;
            fraGG.Location = new Point(6, 52);
            fraGG.Margin = new Padding(3, 4, 3, 4);
            fraGG.Name = "fraGG";
            fraGG.Padding = new Padding(3, 4, 3, 4);
            fraGG.Size = new Size(145, 112);
            fraGG.TabIndex = 11;
            fraGG.TabStop = false;
            fraGG.Text = "Offset";
            // 
            // lblZG
            // 
            lblZG.AutoSize = true;
            lblZG.BackColor = Color.Transparent;
            lblZG.ForeColor = SystemColors.ControlText;
            lblZG.Location = new Point(7, 84);
            lblZG.Name = "lblZG";
            lblZG.Size = new Size(14, 15);
            lblZG.TabIndex = 5;
            lblZG.Text = "Z";
            // 
            // lblYG
            // 
            lblYG.AutoSize = true;
            lblYG.BackColor = Color.Transparent;
            lblYG.ForeColor = SystemColors.ControlText;
            lblYG.Location = new Point(7, 54);
            lblYG.Name = "lblYG";
            lblYG.Size = new Size(14, 15);
            lblYG.TabIndex = 4;
            lblYG.Text = "Y";
            // 
            // lblXG
            // 
            lblXG.AutoSize = true;
            lblXG.BackColor = Color.Transparent;
            lblXG.ForeColor = SystemColors.ControlText;
            lblXG.Location = new Point(7, 24);
            lblXG.Name = "lblXG";
            lblXG.Size = new Size(14, 15);
            lblXG.TabIndex = 3;
            lblXG.Text = "X";
            // 
            // numZG
            // 
            numZG.Location = new Point(27, 82);
            numZG.Margin = new Padding(3, 4, 3, 4);
            numZG.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numZG.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numZG.Name = "numZG";
            numZG.Size = new Size(111, 23);
            numZG.TabIndex = 4;
            numZG.ValueChanged += numZGlobal_ValueChanged;
            // 
            // numYG
            // 
            numYG.Location = new Point(27, 52);
            numYG.Margin = new Padding(3, 4, 3, 4);
            numYG.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numYG.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numYG.Name = "numYG";
            numYG.Size = new Size(111, 23);
            numYG.TabIndex = 3;
            numYG.ValueChanged += numYGlobal_ValueChanged;
            // 
            // numXG
            // 
            numXG.Location = new Point(27, 22);
            numXG.Margin = new Padding(3, 4, 3, 4);
            numXG.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numXG.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numXG.Name = "numXG";
            numXG.Size = new Size(111, 23);
            numXG.TabIndex = 2;
            numXG.ValueChanged += numXGlobal_ValueChanged;
            // 
            // fraG2
            // 
            fraG2.BackColor = Color.FromArgb(32, 32, 40);
            fraG2.Controls.Add(lblZ2);
            fraG2.Controls.Add(lblY2);
            fraG2.Controls.Add(lblX2);
            fraG2.Controls.Add(numZ2);
            fraG2.Controls.Add(numY2);
            fraG2.Controls.Add(numX2);
            fraG2.ForeColor = Color.Gainsboro;
            fraG2.Location = new Point(157, 172);
            fraG2.Margin = new Padding(3, 4, 3, 4);
            fraG2.Name = "fraG2";
            fraG2.Padding = new Padding(3, 4, 3, 4);
            fraG2.Size = new Size(145, 112);
            fraG2.TabIndex = 10;
            fraG2.TabStop = false;
            fraG2.Text = "Point 2";
            // 
            // lblZ2
            // 
            lblZ2.AutoSize = true;
            lblZ2.BackColor = Color.Transparent;
            lblZ2.ForeColor = SystemColors.ControlText;
            lblZ2.Location = new Point(7, 84);
            lblZ2.Name = "lblZ2";
            lblZ2.Size = new Size(14, 15);
            lblZ2.TabIndex = 5;
            lblZ2.Text = "Z";
            // 
            // lblY2
            // 
            lblY2.AutoSize = true;
            lblY2.BackColor = Color.Transparent;
            lblY2.ForeColor = SystemColors.ControlText;
            lblY2.Location = new Point(7, 54);
            lblY2.Name = "lblY2";
            lblY2.Size = new Size(14, 15);
            lblY2.TabIndex = 4;
            lblY2.Text = "Y";
            // 
            // lblX2
            // 
            lblX2.AutoSize = true;
            lblX2.BackColor = Color.Transparent;
            lblX2.ForeColor = SystemColors.ControlText;
            lblX2.Location = new Point(7, 24);
            lblX2.Name = "lblX2";
            lblX2.Size = new Size(14, 15);
            lblX2.TabIndex = 3;
            lblX2.Text = "X";
            // 
            // numZ2
            // 
            numZ2.Location = new Point(27, 82);
            numZ2.Margin = new Padding(3, 4, 3, 4);
            numZ2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numZ2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numZ2.Name = "numZ2";
            numZ2.Size = new Size(111, 23);
            numZ2.TabIndex = 4;
            numZ2.ValueChanged += numZ2_ValueChanged;
            // 
            // numY2
            // 
            numY2.Location = new Point(27, 52);
            numY2.Margin = new Padding(3, 4, 3, 4);
            numY2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numY2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numY2.Name = "numY2";
            numY2.Size = new Size(111, 23);
            numY2.TabIndex = 3;
            numY2.ValueChanged += numY2_ValueChanged;
            // 
            // numX2
            // 
            numX2.Location = new Point(27, 22);
            numX2.Margin = new Padding(3, 4, 3, 4);
            numX2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numX2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numX2.Name = "numX2";
            numX2.Size = new Size(111, 23);
            numX2.TabIndex = 2;
            numX2.ValueChanged += numX2_ValueChanged;
            // 
            // fraG1
            // 
            fraG1.BackColor = Color.FromArgb(32, 32, 40);
            fraG1.Controls.Add(lblZ1);
            fraG1.Controls.Add(lblY1);
            fraG1.Controls.Add(lblX1);
            fraG1.Controls.Add(numZ1);
            fraG1.Controls.Add(numY1);
            fraG1.Controls.Add(numX1);
            fraG1.ForeColor = Color.Gainsboro;
            fraG1.Location = new Point(6, 172);
            fraG1.Margin = new Padding(3, 4, 3, 4);
            fraG1.Name = "fraG1";
            fraG1.Padding = new Padding(3, 4, 3, 4);
            fraG1.Size = new Size(145, 112);
            fraG1.TabIndex = 9;
            fraG1.TabStop = false;
            fraG1.Text = "Point 1";
            // 
            // lblZ1
            // 
            lblZ1.AutoSize = true;
            lblZ1.BackColor = Color.Transparent;
            lblZ1.ForeColor = SystemColors.ControlText;
            lblZ1.Location = new Point(7, 84);
            lblZ1.Name = "lblZ1";
            lblZ1.Size = new Size(14, 15);
            lblZ1.TabIndex = 5;
            lblZ1.Text = "Z";
            // 
            // lblY1
            // 
            lblY1.AutoSize = true;
            lblY1.BackColor = Color.Transparent;
            lblY1.ForeColor = SystemColors.ControlText;
            lblY1.Location = new Point(7, 54);
            lblY1.Name = "lblY1";
            lblY1.Size = new Size(14, 15);
            lblY1.TabIndex = 4;
            lblY1.Text = "Y";
            // 
            // lblX1
            // 
            lblX1.AutoSize = true;
            lblX1.BackColor = Color.Transparent;
            lblX1.ForeColor = SystemColors.ControlText;
            lblX1.Location = new Point(7, 24);
            lblX1.Name = "lblX1";
            lblX1.Size = new Size(14, 15);
            lblX1.TabIndex = 3;
            lblX1.Text = "X";
            // 
            // numZ1
            // 
            numZ1.Location = new Point(27, 82);
            numZ1.Margin = new Padding(3, 4, 3, 4);
            numZ1.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numZ1.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numZ1.Name = "numZ1";
            numZ1.Size = new Size(111, 23);
            numZ1.TabIndex = 4;
            numZ1.ValueChanged += numZ1_ValueChanged;
            // 
            // numY1
            // 
            numY1.Location = new Point(27, 52);
            numY1.Margin = new Padding(3, 4, 3, 4);
            numY1.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numY1.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numY1.Name = "numY1";
            numY1.Size = new Size(111, 23);
            numY1.TabIndex = 3;
            numY1.ValueChanged += numY1_ValueChanged;
            // 
            // numX1
            // 
            numX1.Location = new Point(27, 22);
            numX1.Margin = new Padding(3, 4, 3, 4);
            numX1.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numX1.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numX1.Name = "numX1";
            numX1.Size = new Size(111, 23);
            numX1.TabIndex = 2;
            numX1.ValueChanged += numX1_ValueChanged;
            // 
            // fraOffset
            // 
            fraOffset.BackColor = Color.Transparent;
            fraOffset.Controls.Add(lblZOffset);
            fraOffset.Controls.Add(lblYOffset);
            fraOffset.Controls.Add(lblXOffset);
            fraOffset.Controls.Add(numZOffset);
            fraOffset.Controls.Add(numYOffset);
            fraOffset.Controls.Add(numXOffset);
            fraOffset.Font = new Font("Segoe UI", 9F);
            fraOffset.ForeColor = Color.Silver;
            fraOffset.Location = new Point(317, 186);
            fraOffset.Margin = new Padding(3, 4, 3, 4);
            fraOffset.Name = "fraOffset";
            fraOffset.Padding = new Padding(3, 4, 3, 4);
            fraOffset.Size = new Size(145, 112);
            fraOffset.TabIndex = 8;
            fraOffset.TabStop = false;
            fraOffset.Text = "Offset";
            // 
            // lblZOffset
            // 
            lblZOffset.AutoSize = true;
            lblZOffset.BackColor = Color.Transparent;
            lblZOffset.ForeColor = SystemColors.ControlText;
            lblZOffset.Location = new Point(7, 84);
            lblZOffset.Name = "lblZOffset";
            lblZOffset.Size = new Size(14, 15);
            lblZOffset.TabIndex = 5;
            lblZOffset.Text = "Z";
            // 
            // lblYOffset
            // 
            lblYOffset.AutoSize = true;
            lblYOffset.BackColor = Color.Transparent;
            lblYOffset.ForeColor = SystemColors.ControlText;
            lblYOffset.Location = new Point(7, 54);
            lblYOffset.Name = "lblYOffset";
            lblYOffset.Size = new Size(14, 15);
            lblYOffset.TabIndex = 4;
            lblYOffset.Text = "Y";
            // 
            // lblXOffset
            // 
            lblXOffset.AutoSize = true;
            lblXOffset.BackColor = Color.Transparent;
            lblXOffset.ForeColor = SystemColors.ControlText;
            lblXOffset.Location = new Point(7, 24);
            lblXOffset.Name = "lblXOffset";
            lblXOffset.Size = new Size(14, 15);
            lblXOffset.TabIndex = 3;
            lblXOffset.Text = "X";
            // 
            // numZOffset
            // 
            numZOffset.Location = new Point(27, 82);
            numZOffset.Margin = new Padding(3, 4, 3, 4);
            numZOffset.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numZOffset.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numZOffset.Name = "numZOffset";
            numZOffset.Size = new Size(111, 23);
            numZOffset.TabIndex = 4;
            numZOffset.ValueChanged += numZOffset_ValueChanged;
            // 
            // numYOffset
            // 
            numYOffset.Location = new Point(27, 52);
            numYOffset.Margin = new Padding(3, 4, 3, 4);
            numYOffset.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numYOffset.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numYOffset.Name = "numYOffset";
            numYOffset.Size = new Size(111, 23);
            numYOffset.TabIndex = 3;
            numYOffset.ValueChanged += numYOffset_ValueChanged;
            // 
            // numXOffset
            // 
            numXOffset.Location = new Point(27, 22);
            numXOffset.Margin = new Padding(3, 4, 3, 4);
            numXOffset.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numXOffset.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numXOffset.Name = "numXOffset";
            numXOffset.Size = new Size(111, 23);
            numXOffset.TabIndex = 2;
            numXOffset.ValueChanged += numXOffset_ValueChanged;
            // 
            // cmdPreviousCollision
            // 
            cmdPreviousCollision.BorderColour = Color.Empty;
            cmdPreviousCollision.CustomColour = false;
            cmdPreviousCollision.FlatBottom = false;
            cmdPreviousCollision.FlatTop = false;
            cmdPreviousCollision.ForeColor = SystemColors.ControlText;
            cmdPreviousCollision.Location = new Point(160, 22);
            cmdPreviousCollision.Margin = new Padding(3, 4, 3, 4);
            cmdPreviousCollision.Name = "cmdPreviousCollision";
            cmdPreviousCollision.Padding = new Padding(5);
            cmdPreviousCollision.Size = new Size(68, 26);
            cmdPreviousCollision.TabIndex = 8;
            cmdPreviousCollision.Text = "Previous";
            cmdPreviousCollision.Click += cmdPreviousCollision_Click;
            // 
            // cmdNextCollision
            // 
            cmdNextCollision.BorderColour = Color.Empty;
            cmdNextCollision.CustomColour = false;
            cmdNextCollision.FlatBottom = false;
            cmdNextCollision.FlatTop = false;
            cmdNextCollision.ForeColor = SystemColors.ControlText;
            cmdNextCollision.Location = new Point(234, 22);
            cmdNextCollision.Margin = new Padding(3, 4, 3, 4);
            cmdNextCollision.Name = "cmdNextCollision";
            cmdNextCollision.Padding = new Padding(5);
            cmdNextCollision.Size = new Size(68, 26);
            cmdNextCollision.TabIndex = 8;
            cmdNextCollision.Text = "Next";
            cmdNextCollision.Click += cmdNextCollision_Click;
            // 
            // lblCollisionIndex
            // 
            lblCollisionIndex.BackColor = Color.Transparent;
            lblCollisionIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCollisionIndex.ForeColor = SystemColors.ControlText;
            lblCollisionIndex.Location = new Point(45, 22);
            lblCollisionIndex.Name = "lblCollisionIndex";
            lblCollisionIndex.Size = new Size(70, 26);
            lblCollisionIndex.TabIndex = 8;
            lblCollisionIndex.Text = "?? / ??";
            lblCollisionIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdAppendCollision
            // 
            cmdAppendCollision.BorderColour = Color.Empty;
            cmdAppendCollision.CustomColour = false;
            cmdAppendCollision.FlatBottom = false;
            cmdAppendCollision.FlatTop = false;
            cmdAppendCollision.ForeColor = SystemColors.ControlText;
            cmdAppendCollision.Location = new Point(184, 62);
            cmdAppendCollision.Margin = new Padding(3, 4, 3, 4);
            cmdAppendCollision.Name = "cmdAppendCollision";
            cmdAppendCollision.Padding = new Padding(5);
            cmdAppendCollision.Size = new Size(87, 26);
            cmdAppendCollision.TabIndex = 12;
            cmdAppendCollision.Text = "Append";
            cmdAppendCollision.Click += cmdAppendCollision_Click;
            // 
            // fraCollision
            // 
            fraCollision.BackColor = Color.Transparent;
            fraCollision.Controls.Add(cmdRemoveCollision);
            fraCollision.Controls.Add(cmdInsertCollision);
            fraCollision.Controls.Add(lblCollisionIndex);
            fraCollision.Controls.Add(fraGG);
            fraCollision.Controls.Add(fraG2);
            fraCollision.Controls.Add(fraG1);
            fraCollision.Controls.Add(cmdAppendCollision);
            fraCollision.Controls.Add(cmdPreviousCollision);
            fraCollision.Controls.Add(cmdNextCollision);
            fraCollision.Font = new Font("Segoe UI", 9F);
            fraCollision.ForeColor = Color.Silver;
            fraCollision.Location = new Point(3, 186);
            fraCollision.Name = "fraCollision";
            fraCollision.Size = new Size(308, 290);
            fraCollision.TabIndex = 13;
            fraCollision.TabStop = false;
            fraCollision.Text = "Collision(s)";
            // 
            // cmdRemoveCollision
            // 
            cmdRemoveCollision.BorderColour = Color.Empty;
            cmdRemoveCollision.CustomColour = false;
            cmdRemoveCollision.FlatBottom = false;
            cmdRemoveCollision.FlatTop = false;
            cmdRemoveCollision.ForeColor = SystemColors.ControlText;
            cmdRemoveCollision.Location = new Point(184, 130);
            cmdRemoveCollision.Margin = new Padding(3, 4, 3, 4);
            cmdRemoveCollision.Name = "cmdRemoveCollision";
            cmdRemoveCollision.Padding = new Padding(5);
            cmdRemoveCollision.Size = new Size(87, 26);
            cmdRemoveCollision.TabIndex = 14;
            cmdRemoveCollision.Text = "Remove";
            cmdRemoveCollision.Click += cmdRemoveCollision_Click;
            // 
            // cmdInsertCollision
            // 
            cmdInsertCollision.BorderColour = Color.Empty;
            cmdInsertCollision.CustomColour = false;
            cmdInsertCollision.FlatBottom = false;
            cmdInsertCollision.FlatTop = false;
            cmdInsertCollision.ForeColor = SystemColors.ControlText;
            cmdInsertCollision.Location = new Point(184, 96);
            cmdInsertCollision.Margin = new Padding(3, 4, 3, 4);
            cmdInsertCollision.Name = "cmdInsertCollision";
            cmdInsertCollision.Padding = new Padding(5);
            cmdInsertCollision.Size = new Size(87, 26);
            cmdInsertCollision.TabIndex = 13;
            cmdInsertCollision.Text = "Insert";
            cmdInsertCollision.Click += cmdInsertCollision_Click;
            // 
            // numHeader
            // 
            numHeader.Enabled = false;
            numHeader.InterceptArrowKeys = false;
            numHeader.Location = new Point(27, 17);
            numHeader.Margin = new Padding(3, 4, 3, 4);
            numHeader.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numHeader.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numHeader.Name = "numHeader";
            numHeader.ReadOnly = true;
            numHeader.Size = new Size(111, 23);
            numHeader.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(numHeader);
            groupBox1.Font = new Font("Segoe UI", 9F);
            groupBox1.ForeColor = Color.Silver;
            groupBox1.Location = new Point(317, 306);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(145, 48);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Header Size";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(numSPVertex);
            groupBox2.Font = new Font("Segoe UI", 9F);
            groupBox2.ForeColor = Color.DeepSkyBlue;
            groupBox2.Location = new Point(317, 360);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(145, 48);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Special Vertex Count";
            // 
            // numSPVertex
            // 
            numSPVertex.Enabled = false;
            numSPVertex.InterceptArrowKeys = false;
            numSPVertex.Location = new Point(27, 17);
            numSPVertex.Margin = new Padding(3, 4, 3, 4);
            numSPVertex.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numSPVertex.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numSPVertex.Name = "numSPVertex";
            numSPVertex.ReadOnly = true;
            numSPVertex.Size = new Size(111, 23);
            numSPVertex.TabIndex = 6;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.BackColor = Color.Transparent;
            darkGroupBox1.Controls.Add(txtModel);
            darkGroupBox1.Font = new Font("Segoe UI", 9F);
            darkGroupBox1.ForeColor = SystemColors.ControlText;
            darkGroupBox1.Location = new Point(317, 414);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(145, 48);
            darkGroupBox1.TabIndex = 16;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Model EID";
            // 
            // txtModel
            // 
            txtModel.BackColor = Color.FromArgb(26, 26, 28);
            txtModel.BorderStyle = BorderStyle.FixedSingle;
            txtModel.ForeColor = Color.FromArgb(213, 213, 213);
            txtModel.Location = new Point(27, 19);
            txtModel.MaxLength = 5;
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(102, 23);
            txtModel.TabIndex = 0;
            txtModel.TextChanged += txtModel_TextChanged;
            // 
            // lblEIDError
            // 
            lblEIDError.AutoSize = true;
            lblEIDError.ForeColor = Color.Red;
            lblEIDError.Location = new Point(317, 465);
            lblEIDError.Name = "lblEIDError";
            lblEIDError.Size = new Size(74, 15);
            lblEIDError.TabIndex = 17;
            lblEIDError.Text = "EIDERROR!";
            // 
            // chkSyncFrames
            // 
            chkSyncFrames.AutoSize = true;
            chkSyncFrames.BackColor = Color.Transparent;
            chkSyncFrames.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkSyncFrames.ForeColor = SystemColors.ControlText;
            chkSyncFrames.Location = new Point(468, 274);
            chkSyncFrames.Name = "chkSyncFrames";
            chkSyncFrames.Size = new Size(92, 19);
            chkSyncFrames.TabIndex = 18;
            chkSyncFrames.Text = "Sync Frames";
            chkSyncFrames.UseVisualStyleBackColor = false;
            chkSyncFrames.CheckedChanged += chkSyncFrames_CheckedChanged;
            // 
            // cmdCopyCollision
            // 
            cmdCopyCollision.BorderColour = Color.Empty;
            cmdCopyCollision.CustomColour = false;
            cmdCopyCollision.FlatBottom = false;
            cmdCopyCollision.FlatTop = false;
            cmdCopyCollision.Location = new Point(6, 22);
            cmdCopyCollision.Name = "cmdCopyCollision";
            cmdCopyCollision.Padding = new Padding(5);
            cmdCopyCollision.Size = new Size(75, 23);
            cmdCopyCollision.TabIndex = 19;
            cmdCopyCollision.Text = "Collision";
            cmdCopyCollision.Click += cmdCopyCollision_Click;
            // 
            // fraCopy
            // 
            fraCopy.BackColor = Color.Transparent;
            fraCopy.Controls.Add(cmdCopyOffset);
            fraCopy.Controls.Add(cmdCopyCollision);
            fraCopy.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fraCopy.Location = new Point(468, 186);
            fraCopy.Name = "fraCopy";
            fraCopy.Size = new Size(147, 82);
            fraCopy.TabIndex = 20;
            fraCopy.TabStop = false;
            fraCopy.Text = "Copy to other frames";
            // 
            // cmdCopyOffset
            // 
            cmdCopyOffset.BorderColour = Color.Empty;
            cmdCopyOffset.CustomColour = false;
            cmdCopyOffset.FlatBottom = false;
            cmdCopyOffset.FlatTop = false;
            cmdCopyOffset.Location = new Point(6, 51);
            cmdCopyOffset.Name = "cmdCopyOffset";
            cmdCopyOffset.Padding = new Padding(5);
            cmdCopyOffset.Size = new Size(75, 23);
            cmdCopyOffset.TabIndex = 19;
            cmdCopyOffset.Text = "Offset";
            cmdCopyOffset.Click += cmdCopyOffset_Click;
            // 
            // pnFrameBox
            // 
            pnFrameBox.AutoScroll = true;
            pnFrameBox.BackColor = Color.FromArgb(31, 31, 32);
            pnFrameBox.Controls.Add(cmdMisc);
            pnFrameBox.Controls.Add(fraMisc);
            pnFrameBox.Controls.Add(fraCopy);
            pnFrameBox.Controls.Add(cmdInsertVertice);
            pnFrameBox.Controls.Add(chkSyncFrames);
            pnFrameBox.Controls.Add(lblEIDError);
            pnFrameBox.Controls.Add(darkGroupBox1);
            pnFrameBox.Controls.Add(groupBox2);
            pnFrameBox.Controls.Add(groupBox1);
            pnFrameBox.Controls.Add(fraCollision);
            pnFrameBox.Controls.Add(fraOffset);
            pnFrameBox.Controls.Add(fraVertice);
            pnFrameBox.Dock = DockStyle.Fill;
            pnFrameBox.Location = new Point(0, 0);
            pnFrameBox.Name = "pnFrameBox";
            pnFrameBox.Size = new Size(710, 524);
            pnFrameBox.TabIndex = 21;
            // 
            // fraMisc
            // 
            fraMisc.BackColor = Color.Transparent;
            fraMisc.Controls.Add(cmdRotateZ);
            fraMisc.Controls.Add(cmdRotateY);
            fraMisc.Controls.Add(cmdRotateX);
            fraMisc.Location = new Point(474, 360);
            fraMisc.Name = "fraMisc";
            fraMisc.Size = new Size(141, 116);
            fraMisc.TabIndex = 21;
            fraMisc.TabStop = false;
            fraMisc.Text = "Misc";
            fraMisc.Visible = false;
            // 
            // cmdRotateZ
            // 
            cmdRotateZ.BorderColour = Color.Empty;
            cmdRotateZ.CustomColour = false;
            cmdRotateZ.FlatBottom = false;
            cmdRotateZ.FlatTop = false;
            cmdRotateZ.Location = new Point(6, 84);
            cmdRotateZ.Name = "cmdRotateZ";
            cmdRotateZ.Padding = new Padding(5);
            cmdRotateZ.Size = new Size(126, 26);
            cmdRotateZ.TabIndex = 0;
            cmdRotateZ.Text = "Rotate Z 90deg";
            cmdRotateZ.Click += cmdRotateZ_Click;
            // 
            // cmdRotateY
            // 
            cmdRotateY.BorderColour = Color.Empty;
            cmdRotateY.CustomColour = false;
            cmdRotateY.FlatBottom = false;
            cmdRotateY.FlatTop = false;
            cmdRotateY.Location = new Point(6, 52);
            cmdRotateY.Name = "cmdRotateY";
            cmdRotateY.Padding = new Padding(5);
            cmdRotateY.Size = new Size(126, 26);
            cmdRotateY.TabIndex = 0;
            cmdRotateY.Text = "Rotate Y 90deg";
            cmdRotateY.Click += cmdRotateY_Click;
            // 
            // cmdRotateX
            // 
            cmdRotateX.BorderColour = Color.Empty;
            cmdRotateX.CustomColour = false;
            cmdRotateX.FlatBottom = false;
            cmdRotateX.FlatTop = false;
            cmdRotateX.Location = new Point(6, 20);
            cmdRotateX.Name = "cmdRotateX";
            cmdRotateX.Padding = new Padding(5);
            cmdRotateX.Size = new Size(126, 26);
            cmdRotateX.TabIndex = 0;
            cmdRotateX.Text = "Rotate X 90deg";
            cmdRotateX.Click += cmdRotateX_Click;
            // 
            // cmdMisc
            // 
            cmdMisc.AutoSize = true;
            cmdMisc.Location = new Point(474, 335);
            cmdMisc.Name = "cmdMisc";
            cmdMisc.Size = new Size(52, 19);
            cmdMisc.TabIndex = 22;
            cmdMisc.Text = "Misc";
            cmdMisc.UseVisualStyleBackColor = true;
            cmdMisc.CheckedChanged += cmdMisc_CheckedChanged;
            // 
            // FrameBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(pnFrameBox);
            Font = new Font("Microsoft Sans Serif", 9F);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrameBox";
            Size = new Size(710, 524);
            fraVertice.ResumeLayout(false);
            fraVertice.PerformLayout();
            fraNearbyVertices.ResumeLayout(false);
            fraNearbyVertices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDistance).EndInit();
            pnVertexControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            fraGG.ResumeLayout(false);
            fraGG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZG).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYG).EndInit();
            ((System.ComponentModel.ISupportInitialize)numXG).EndInit();
            fraG2.ResumeLayout(false);
            fraG2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZ2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX2).EndInit();
            fraG1.ResumeLayout(false);
            fraG1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZ1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX1).EndInit();
            fraOffset.ResumeLayout(false);
            fraOffset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZOffset).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYOffset).EndInit();
            ((System.ComponentModel.ISupportInitialize)numXOffset).EndInit();
            fraCollision.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numHeader).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numSPVertex).EndInit();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            fraCopy.ResumeLayout(false);
            pnFrameBox.ResumeLayout(false);
            pnFrameBox.PerformLayout();
            fraMisc.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DarkGroupBox fraVertice;
        private Label lblZ;
        private Label lblY;
        private Label lblX;
        private DarkNumericUpDown numZ;
        private DarkNumericUpDown numY;
        private DarkNumericUpDown numX;
        private DarkButton cmdInsertVertice;
        private DarkButton cmdRemoveVertice;
        private DarkButton cmdAppendVertice;
        private DarkButton cmdNextVertice;
        private DarkButton cmdPreviousVertice;
        private Label lblVerticeIndex;
        private DarkGroupBox fraOffset;
        private Label lblZOffset;
        private Label lblYOffset;
        private Label lblXOffset;
        private DarkNumericUpDown numZOffset;
        private DarkNumericUpDown numYOffset;
        private DarkNumericUpDown numXOffset;
        private DarkGroupBox fraG2;
        private Label lblZ2;
        private Label lblY2;
        private Label lblX2;
        private DarkNumericUpDown numZ2;
        private DarkNumericUpDown numY2;
        private DarkNumericUpDown numX2;
        private DarkGroupBox fraG1;
        private Label lblZ1;
        private Label lblY1;
        private Label lblX1;
        private DarkNumericUpDown numZ1;
        private DarkNumericUpDown numY1;
        private DarkNumericUpDown numX1;
        private DarkGroupBox fraGG;
        private Label lblZG;
        private Label lblYG;
        private Label lblXG;
        private DarkNumericUpDown numZG;
        private DarkNumericUpDown numYG;
        private DarkNumericUpDown numXG;
        private DarkButton cmdPreviousCollision;
        private DarkButton cmdNextCollision;
        private Label lblCollisionIndex;
        private DarkButton cmdAppendCollision;
        private DarkGroupBox fraCollision;
        private DarkButton cmdRemoveCollision;
        private DarkButton cmdInsertCollision;
        private DarkNumericUpDown numHeader;
        private DarkGroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DarkGroupBox groupBox2;
        private DarkNumericUpDown numSPVertex;
        private DarkButton cmdLastVertice;
        private DarkButton cmdFirstVertice;
        private DarkButton cmdNext10Vertice;
        private DarkButton cmdPrevious10Vertice;
        private Label lblSPVertex;
        private DarkGroupBox darkGroupBox1;
        private DarkTextBox txtModel;
        private Label lblEIDError;
        private CheckBox chkSyncFrames;
        private DarkButton cmdCopyCollision;
        private DarkGroupBox fraCopy;
        private DarkButton cmdCopyOffset;
        private Panel pnFrameBox;
        private Panel pnVertexControls;
        private DarkListBox lstNearbyVertices;
        private CheckBox chkEditNearbyVertices;
        private DarkGroupBox fraNearbyVertices;
        private DarkNumericUpDown numDistance;
        private DarkLabel lbDistance;
        private DarkGroupBox fraMisc;
        private DarkButton cmdRotateX;
        private DarkButton cmdRotateY;
        private DarkButton cmdRotateZ;
        private CheckBox cmdMisc;
    }
}