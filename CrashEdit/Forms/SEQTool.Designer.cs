namespace CrashEdit.CE.Forms
{
    partial class SEQTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEQTool));
            toolStrip = new ToolStrip();
            tsbOpen = new ToolStripButton();
            tsbSave = new ToolStripButton();
            dgvEvents = new DataGridView();
            numLoopStart = new AltUI.Controls.DarkNumericUpDown();
            numLoopEnd = new AltUI.Controls.DarkNumericUpDown();
            fraLoopStart = new AltUI.Controls.DarkGroupBox();
            dgvLoopStart = new DataGridView();
            fraLoopEnd = new AltUI.Controls.DarkGroupBox();
            dgvLoopEnd = new DataGridView();
            progressBar = new ProgressBar();
            pnControls = new Panel();
            cmdImport = new AltUI.Controls.DarkButton();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLoopStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLoopEnd).BeginInit();
            fraLoopStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoopStart).BeginInit();
            fraLoopEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoopEnd).BeginInit();
            pnControls.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { tsbOpen, tsbSave });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(602, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip";
            // 
            // tsbOpen
            // 
            tsbOpen.Image = (Image)resources.GetObject("tsbOpen.Image");
            tsbOpen.ImageTransparentColor = Color.Magenta;
            tsbOpen.Name = "tsbOpen";
            tsbOpen.Size = new Size(56, 22);
            tsbOpen.Text = "Open";
            tsbOpen.Click += tsbOpen_Click;
            // 
            // tsbSave
            // 
            tsbSave.Image = (Image)resources.GetObject("tsbSave.Image");
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(51, 22);
            tsbSave.Text = "Save";
            tsbSave.Click += tsbSave_Click;
            // 
            // dgvEvents
            // 
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AllowUserToResizeColumns = false;
            dgvEvents.AllowUserToResizeRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEvents.ColumnHeadersHeight = 24;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEvents.Dock = DockStyle.Left;
            dgvEvents.Location = new Point(0, 25);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.RowHeadersWidth = 24;
            dgvEvents.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvEvents.ShowCellToolTips = false;
            dgvEvents.Size = new Size(482, 425);
            dgvEvents.TabIndex = 1;
            dgvEvents.CellFormatting += dgvEvents_CellFormatting;
            // 
            // numLoopStart
            // 
            numLoopStart.Location = new Point(6, 22);
            numLoopStart.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numLoopStart.Name = "numLoopStart";
            numLoopStart.Size = new Size(86, 23);
            numLoopStart.TabIndex = 3;
            numLoopStart.ValueChanged += numLoopStart_ValueChanged;
            // 
            // numLoopEnd
            // 
            numLoopEnd.Location = new Point(6, 22);
            numLoopEnd.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numLoopEnd.Name = "numLoopEnd";
            numLoopEnd.Size = new Size(86, 23);
            numLoopEnd.TabIndex = 3;
            numLoopEnd.ValueChanged += numLoopEnd_ValueChanged;
            // 
            // fraLoopStart
            // 
            fraLoopStart.BackColor = Color.Transparent;
            fraLoopStart.Controls.Add(dgvLoopStart);
            fraLoopStart.Controls.Add(numLoopStart);
            fraLoopStart.Location = new Point(6, 3);
            fraLoopStart.Name = "fraLoopStart";
            fraLoopStart.Size = new Size(100, 140);
            fraLoopStart.TabIndex = 4;
            fraLoopStart.TabStop = false;
            fraLoopStart.Text = "Loop Start";
            // 
            // dgvLoopStart
            // 
            dgvLoopStart.AllowUserToAddRows = false;
            dgvLoopStart.AllowUserToDeleteRows = false;
            dgvLoopStart.AllowUserToResizeColumns = false;
            dgvLoopStart.AllowUserToResizeRows = false;
            dgvLoopStart.ColumnHeadersHeight = 20;
            dgvLoopStart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLoopStart.ColumnHeadersVisible = false;
            dgvLoopStart.Location = new Point(6, 51);
            dgvLoopStart.Name = "dgvLoopStart";
            dgvLoopStart.RowHeadersVisible = false;
            dgvLoopStart.RowHeadersWidth = 20;
            dgvLoopStart.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvLoopStart.ShowCellToolTips = false;
            dgvLoopStart.Size = new Size(86, 78);
            dgvLoopStart.TabIndex = 6;
            dgvLoopStart.CellBeginEdit += dgvLoop_CellBeginEdit;
            dgvLoopStart.CellEndEdit += dgvLoop_CellEndEdit;
            dgvLoopStart.CellValidating += dgvLoop_CellValidating;
            // 
            // fraLoopEnd
            // 
            fraLoopEnd.BackColor = Color.Transparent;
            fraLoopEnd.Controls.Add(dgvLoopEnd);
            fraLoopEnd.Controls.Add(numLoopEnd);
            fraLoopEnd.Location = new Point(6, 149);
            fraLoopEnd.Name = "fraLoopEnd";
            fraLoopEnd.Size = new Size(100, 140);
            fraLoopEnd.TabIndex = 4;
            fraLoopEnd.TabStop = false;
            fraLoopEnd.Text = "Loop End";
            // 
            // dgvLoopEnd
            // 
            dgvLoopEnd.AllowUserToAddRows = false;
            dgvLoopEnd.AllowUserToDeleteRows = false;
            dgvLoopEnd.AllowUserToResizeColumns = false;
            dgvLoopEnd.AllowUserToResizeRows = false;
            dgvLoopEnd.ColumnHeadersHeight = 20;
            dgvLoopEnd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLoopEnd.ColumnHeadersVisible = false;
            dgvLoopEnd.Location = new Point(6, 51);
            dgvLoopEnd.Name = "dgvLoopEnd";
            dgvLoopEnd.RowHeadersVisible = false;
            dgvLoopEnd.RowHeadersWidth = 20;
            dgvLoopEnd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvLoopEnd.ShowCellToolTips = false;
            dgvLoopEnd.Size = new Size(86, 78);
            dgvLoopEnd.TabIndex = 6;
            dgvLoopEnd.CellBeginEdit += dgvLoop_CellBeginEdit;
            dgvLoopEnd.CellEndEdit += dgvLoop_CellEndEdit;
            dgvLoopEnd.CellValidating += dgvLoop_CellValidating;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 390);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(86, 23);
            progressBar.TabIndex = 5;
            progressBar.Visible = false;
            // 
            // pnControls
            // 
            pnControls.AutoSize = true;
            pnControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnControls.BackColor = Color.Transparent;
            pnControls.Controls.Add(cmdImport);
            pnControls.Controls.Add(fraLoopStart);
            pnControls.Controls.Add(progressBar);
            pnControls.Controls.Add(fraLoopEnd);
            pnControls.Dock = DockStyle.Left;
            pnControls.Enabled = false;
            pnControls.Location = new Point(482, 25);
            pnControls.Name = "pnControls";
            pnControls.Size = new Size(109, 425);
            pnControls.TabIndex = 6;
            // 
            // cmdImport
            // 
            cmdImport.BorderColour = Color.Empty;
            cmdImport.CustomColour = false;
            cmdImport.FlatBottom = false;
            cmdImport.FlatTop = false;
            cmdImport.Location = new Point(12, 314);
            cmdImport.Name = "cmdImport";
            cmdImport.Padding = new Padding(5);
            cmdImport.Size = new Size(86, 47);
            cmdImport.TabIndex = 6;
            cmdImport.Text = "Import";
            cmdImport.Click += cmdImport_Click;
            // 
            // SEQTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            ClientSize = new Size(602, 450);
            Controls.Add(pnControls);
            Controls.Add(dgvEvents);
            Controls.Add(toolStrip);
            CornerStyle = CornerPreference.Default;
            Name = "SEQTool";
            Text = "SEQ Tool";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLoopStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLoopEnd).EndInit();
            fraLoopStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLoopStart).EndInit();
            fraLoopEnd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLoopEnd).EndInit();
            pnControls.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton tsbOpen;
        private ToolStripButton tsbSave;
        private DataGridView dgvEvents;
        private AltUI.Controls.DarkNumericUpDown numLoopStart;
        private AltUI.Controls.DarkNumericUpDown numLoopEnd;
        private AltUI.Controls.DarkGroupBox fraLoopStart;
        private AltUI.Controls.DarkGroupBox fraLoopEnd;
        private ProgressBar progressBar;
        private Panel pnControls;
        private DataGridView dgvLoopStart;
        private DataGridView dgvLoopEnd;
        private AltUI.Controls.DarkButton cmdImport;
    }
}