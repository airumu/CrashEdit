using System.Windows.Forms;
using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE.Forms
{
    partial class VABTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VABTool));
            toolStrip = new ToolStrip();
            tbbOpen = new ToolStripButton();
            tbbSave = new ToolStripButton();
            tbbClose = new ToolStripButton();
            dgvHeader = new DataGridView();
            fraVABHeader = new DarkGroupBox();
            cmdPreviewVAB = new DarkButton();
            cmdViewVAG = new DarkButton();
            fraVABPrograms = new DarkGroupBox();
            cmdDeleteProgram = new DarkButton();
            cmdInsertProgram = new DarkButton();
            cmdAppendProgram = new DarkButton();
            pnProgramControls = new Panel();
            trkProgramVolume = new MetroSetTrackBar();
            lbProgramPan = new Label();
            trkProgramPan = new MetroSetTrackBar();
            lbProgramVolume = new Label();
            dgvPrograms = new DataGridView();
            fraTones = new DarkGroupBox();
            panel1 = new Panel();
            lbMode = new Label();
            lbTone = new Label();
            lbPriority = new Label();
            numMode = new DarkNumericUpDown();
            numPriority = new DarkNumericUpDown();
            cmdDeleteTone = new DarkButton();
            panel2 = new Panel();
            chkAutoPlay = new CheckBox();
            cmdADSR = new DarkButton();
            cmdPlayTone = new DarkButton();
            numNote = new DarkNumericUpDown();
            numVAG = new DarkNumericUpDown();
            lbNote = new Label();
            lbVAG = new Label();
            cmdInsertTone = new DarkButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbVolume = new Label();
            trkVolume = new TrackBar();
            lbPan = new Label();
            trkPan = new TrackBar();
            lbCenter = new Label();
            trkCenter = new TrackBar();
            lbPitch = new Label();
            trkPitch = new TrackBar();
            lbMinNote = new Label();
            trkMinNote = new TrackBar();
            lbMaxNote = new Label();
            trkMaxNote = new TrackBar();
            lbPBmin = new Label();
            trkPBmin = new TrackBar();
            lbPBmax = new Label();
            trkPBmax = new TrackBar();
            cmdAppendTone = new DarkButton();
            dgvTones = new DataGridView();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).BeginInit();
            fraVABHeader.SuspendLayout();
            fraVABPrograms.SuspendLayout();
            pnProgramControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).BeginInit();
            fraTones.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPriority).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVAG).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkPan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkCenter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkPitch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkMinNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkMaxNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkPBmin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkPBmax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTones).BeginInit();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { tbbOpen, tbbSave, tbbClose });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1152, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // tbbOpen
            // 
            tbbOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbbOpen.Image = (Image)resources.GetObject("tbbOpen.Image");
            tbbOpen.ImageTransparentColor = Color.Magenta;
            tbbOpen.Name = "tbbOpen";
            tbbOpen.Size = new Size(23, 22);
            tbbOpen.Text = "Open";
            tbbOpen.Click += tbbOpen_Click;
            // 
            // tbbSave
            // 
            tbbSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbbSave.Image = (Image)resources.GetObject("tbbSave.Image");
            tbbSave.ImageTransparentColor = Color.Magenta;
            tbbSave.Name = "tbbSave";
            tbbSave.Size = new Size(23, 22);
            tbbSave.Text = "Save";
            tbbSave.Click += tbbSave_Click;
            // 
            // tbbClose
            // 
            tbbClose.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbbClose.Image = (Image)resources.GetObject("tbbClose.Image");
            tbbClose.ImageTransparentColor = Color.Magenta;
            tbbClose.Name = "tbbClose";
            tbbClose.Size = new Size(23, 22);
            tbbClose.Text = "Close";
            tbbClose.Click += tbbClose_Click;
            // 
            // dgvHeader
            // 
            dgvHeader.AllowUserToAddRows = false;
            dgvHeader.AllowUserToResizeColumns = false;
            dgvHeader.AllowUserToResizeRows = false;
            dgvHeader.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvHeader.ColumnHeadersHeight = 24;
            dgvHeader.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHeader.Location = new Point(6, 22);
            dgvHeader.MultiSelect = false;
            dgvHeader.Name = "dgvHeader";
            dgvHeader.RowHeadersWidth = 24;
            dgvHeader.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvHeader.Size = new Size(470, 60);
            dgvHeader.TabIndex = 1;
            // 
            // fraVABHeader
            // 
            fraVABHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraVABHeader.BackColor = Color.Transparent;
            fraVABHeader.Controls.Add(dgvHeader);
            fraVABHeader.Controls.Add(cmdPreviewVAB);
            fraVABHeader.Controls.Add(cmdViewVAG);
            fraVABHeader.Location = new Point(12, 30);
            fraVABHeader.Name = "fraVABHeader";
            fraVABHeader.Size = new Size(480, 124);
            fraVABHeader.TabIndex = 3;
            fraVABHeader.TabStop = false;
            fraVABHeader.Text = "VAB File Settings";
            // 
            // cmdPreviewVAB
            // 
            cmdPreviewVAB.BorderColour = Color.Empty;
            cmdPreviewVAB.CustomColour = false;
            cmdPreviewVAB.FlatBottom = false;
            cmdPreviewVAB.FlatTop = false;
            cmdPreviewVAB.Location = new Point(101, 88);
            cmdPreviewVAB.Name = "cmdPreviewVAB";
            cmdPreviewVAB.Padding = new Padding(5);
            cmdPreviewVAB.Size = new Size(89, 28);
            cmdPreviewVAB.TabIndex = 5;
            cmdPreviewVAB.Text = "Preview VAB";
            cmdPreviewVAB.Click += cmdPreviewVAB_Click;
            // 
            // cmdViewVAG
            // 
            cmdViewVAG.BorderColour = Color.Empty;
            cmdViewVAG.CustomColour = false;
            cmdViewVAG.FlatBottom = false;
            cmdViewVAG.FlatTop = false;
            cmdViewVAG.Location = new Point(6, 88);
            cmdViewVAG.Name = "cmdViewVAG";
            cmdViewVAG.Padding = new Padding(5);
            cmdViewVAG.Size = new Size(89, 28);
            cmdViewVAG.TabIndex = 5;
            cmdViewVAG.Text = "View VAGs";
            cmdViewVAG.Click += cmdViewVAG_Click;
            // 
            // fraVABPrograms
            // 
            fraVABPrograms.BackColor = Color.Transparent;
            fraVABPrograms.Controls.Add(cmdDeleteProgram);
            fraVABPrograms.Controls.Add(cmdInsertProgram);
            fraVABPrograms.Controls.Add(cmdAppendProgram);
            fraVABPrograms.Controls.Add(pnProgramControls);
            fraVABPrograms.Controls.Add(dgvPrograms);
            fraVABPrograms.Location = new Point(12, 160);
            fraVABPrograms.Name = "fraVABPrograms";
            fraVABPrograms.Size = new Size(482, 404);
            fraVABPrograms.TabIndex = 3;
            fraVABPrograms.TabStop = false;
            fraVABPrograms.Text = "Programs";
            // 
            // cmdDeleteProgram
            // 
            cmdDeleteProgram.BorderColour = Color.Empty;
            cmdDeleteProgram.CustomColour = false;
            cmdDeleteProgram.FlatBottom = false;
            cmdDeleteProgram.FlatTop = false;
            cmdDeleteProgram.Location = new Point(350, 357);
            cmdDeleteProgram.Name = "cmdDeleteProgram";
            cmdDeleteProgram.Padding = new Padding(5);
            cmdDeleteProgram.Size = new Size(75, 28);
            cmdDeleteProgram.TabIndex = 5;
            cmdDeleteProgram.Text = "Delete";
            cmdDeleteProgram.Click += cmdDeleteProgram_Click;
            // 
            // cmdInsertProgram
            // 
            cmdInsertProgram.BorderColour = Color.Empty;
            cmdInsertProgram.CustomColour = false;
            cmdInsertProgram.FlatBottom = false;
            cmdInsertProgram.FlatTop = false;
            cmdInsertProgram.Location = new Point(350, 323);
            cmdInsertProgram.Name = "cmdInsertProgram";
            cmdInsertProgram.Padding = new Padding(5);
            cmdInsertProgram.Size = new Size(75, 28);
            cmdInsertProgram.TabIndex = 5;
            cmdInsertProgram.Text = "Insert";
            cmdInsertProgram.Click += cmdInsertProgram_Click;
            // 
            // cmdAppendProgram
            // 
            cmdAppendProgram.BorderColour = Color.Empty;
            cmdAppendProgram.CustomColour = false;
            cmdAppendProgram.FlatBottom = false;
            cmdAppendProgram.FlatTop = false;
            cmdAppendProgram.Location = new Point(350, 289);
            cmdAppendProgram.Name = "cmdAppendProgram";
            cmdAppendProgram.Padding = new Padding(5);
            cmdAppendProgram.Size = new Size(75, 28);
            cmdAppendProgram.TabIndex = 5;
            cmdAppendProgram.Text = "Append";
            cmdAppendProgram.Click += cmdAppendProgram_Click;
            // 
            // pnProgramControls
            // 
            pnProgramControls.AutoSize = true;
            pnProgramControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnProgramControls.Controls.Add(trkProgramVolume);
            pnProgramControls.Controls.Add(lbProgramPan);
            pnProgramControls.Controls.Add(trkProgramPan);
            pnProgramControls.Controls.Add(lbProgramVolume);
            pnProgramControls.Location = new Point(350, 22);
            pnProgramControls.Name = "pnProgramControls";
            pnProgramControls.Size = new Size(126, 94);
            pnProgramControls.TabIndex = 4;
            // 
            // trkProgramVolume
            // 
            trkProgramVolume.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkProgramVolume.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkProgramVolume.DisabledBorderColor = Color.Empty;
            trkProgramVolume.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkProgramVolume.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkProgramVolume.HandlerColor = Color.FromArgb(143, 143, 143);
            trkProgramVolume.IsDerivedStyle = true;
            trkProgramVolume.Location = new Point(3, 22);
            trkProgramVolume.Maximum = 255;
            trkProgramVolume.Minimum = 0;
            trkProgramVolume.Name = "trkProgramVolume";
            trkProgramVolume.Size = new Size(120, 16);
            trkProgramVolume.Style = MetroSet_UI.Enums.Style.Dark;
            trkProgramVolume.StyleManager = null;
            trkProgramVolume.TabIndex = 2;
            trkProgramVolume.Text = "ProgramVolume";
            trkProgramVolume.ThemeAuthor = "Narwin";
            trkProgramVolume.ThemeName = "MetroDark";
            trkProgramVolume.TickFrequency = 1;
            trkProgramVolume.Value = 0;
            trkProgramVolume.ValueColor = Color.SpringGreen;
            trkProgramVolume.ValueChanged += trkProgramVolume_ValueChanged;
            // 
            // lbProgramPan
            // 
            lbProgramPan.AutoSize = true;
            lbProgramPan.BackColor = Color.Transparent;
            lbProgramPan.Location = new Point(3, 57);
            lbProgramPan.Margin = new Padding(3);
            lbProgramPan.Name = "lbProgramPan";
            lbProgramPan.Size = new Size(27, 15);
            lbProgramPan.TabIndex = 3;
            lbProgramPan.Text = "Pan";
            // 
            // trkProgramPan
            // 
            trkProgramPan.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkProgramPan.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkProgramPan.DisabledBorderColor = Color.Empty;
            trkProgramPan.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkProgramPan.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkProgramPan.HandlerColor = Color.FromArgb(143, 143, 143);
            trkProgramPan.IsDerivedStyle = true;
            trkProgramPan.Location = new Point(3, 75);
            trkProgramPan.Maximum = 127;
            trkProgramPan.Minimum = 0;
            trkProgramPan.Name = "trkProgramPan";
            trkProgramPan.Size = new Size(120, 16);
            trkProgramPan.Style = MetroSet_UI.Enums.Style.Dark;
            trkProgramPan.StyleManager = null;
            trkProgramPan.TabIndex = 2;
            trkProgramPan.Text = "ProgramPan";
            trkProgramPan.ThemeAuthor = "Narwin";
            trkProgramPan.ThemeName = "MetroDark";
            trkProgramPan.TickFrequency = 1;
            trkProgramPan.Value = 0;
            trkProgramPan.ValueColor = Color.FromArgb(65, 177, 225);
            trkProgramPan.ValueChanged += trkProgramPan_ValueChanged;
            // 
            // lbProgramVolume
            // 
            lbProgramVolume.AutoSize = true;
            lbProgramVolume.BackColor = Color.Transparent;
            lbProgramVolume.Location = new Point(3, 4);
            lbProgramVolume.Margin = new Padding(3);
            lbProgramVolume.Name = "lbProgramVolume";
            lbProgramVolume.Size = new Size(47, 15);
            lbProgramVolume.TabIndex = 3;
            lbProgramVolume.Text = "Volume";
            // 
            // dgvPrograms
            // 
            dgvPrograms.AllowUserToAddRows = false;
            dgvPrograms.AllowUserToResizeColumns = false;
            dgvPrograms.AllowUserToResizeRows = false;
            dgvPrograms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPrograms.ColumnHeadersHeight = 24;
            dgvPrograms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPrograms.Location = new Point(6, 22);
            dgvPrograms.MultiSelect = false;
            dgvPrograms.Name = "dgvPrograms";
            dgvPrograms.RowHeadersWidth = 24;
            dgvPrograms.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPrograms.Size = new Size(314, 374);
            dgvPrograms.TabIndex = 1;
            dgvPrograms.SelectionChanged += dgvVABPrograms_SelectionChanged;
            // 
            // fraTones
            // 
            fraTones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTones.BackColor = Color.Transparent;
            fraTones.Controls.Add(panel1);
            fraTones.Controls.Add(cmdDeleteTone);
            fraTones.Controls.Add(panel2);
            fraTones.Controls.Add(cmdInsertTone);
            fraTones.Controls.Add(tableLayoutPanel1);
            fraTones.Controls.Add(cmdAppendTone);
            fraTones.Controls.Add(dgvTones);
            fraTones.Location = new Point(500, 30);
            fraTones.Name = "fraTones";
            fraTones.Size = new Size(646, 534);
            fraTones.TabIndex = 3;
            fraTones.TabStop = false;
            fraTones.Text = "Tone Attributes Table";
            // 
            // panel1
            // 
            panel1.Controls.Add(lbMode);
            panel1.Controls.Add(lbTone);
            panel1.Controls.Add(lbPriority);
            panel1.Controls.Add(numMode);
            panel1.Controls.Add(numPriority);
            panel1.Location = new Point(7, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(74, 142);
            panel1.TabIndex = 8;
            // 
            // lbMode
            // 
            lbMode.AutoSize = true;
            lbMode.BackColor = Color.Transparent;
            lbMode.Location = new Point(3, 53);
            lbMode.Margin = new Padding(3);
            lbMode.Name = "lbMode";
            lbMode.Size = new Size(38, 15);
            lbMode.TabIndex = 3;
            lbMode.Text = "Mode";
            // 
            // lbTone
            // 
            lbTone.AutoSize = true;
            lbTone.BackColor = Color.Transparent;
            lbTone.ForeColor = SystemColors.MenuHighlight;
            lbTone.Location = new Point(3, 117);
            lbTone.Margin = new Padding(3);
            lbTone.Name = "lbTone";
            lbTone.Size = new Size(49, 15);
            lbTone.TabIndex = 3;
            lbTone.Text = "Tone {x}";
            // 
            // lbPriority
            // 
            lbPriority.AutoSize = true;
            lbPriority.BackColor = Color.Transparent;
            lbPriority.Location = new Point(3, 3);
            lbPriority.Margin = new Padding(3);
            lbPriority.Name = "lbPriority";
            lbPriority.Size = new Size(45, 15);
            lbPriority.TabIndex = 3;
            lbPriority.Text = "Priority";
            // 
            // numMode
            // 
            numMode.Location = new Point(3, 74);
            numMode.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numMode.Name = "numMode";
            numMode.Size = new Size(44, 23);
            numMode.TabIndex = 4;
            numMode.ValueChanged += numMode_ValueChanged;
            // 
            // numPriority
            // 
            numPriority.Location = new Point(3, 24);
            numPriority.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numPriority.Name = "numPriority";
            numPriority.Size = new Size(44, 23);
            numPriority.TabIndex = 4;
            numPriority.ValueChanged += numPriority_ValueChanged;
            // 
            // cmdDeleteTone
            // 
            cmdDeleteTone.BorderColour = Color.Empty;
            cmdDeleteTone.CustomColour = false;
            cmdDeleteTone.FlatBottom = false;
            cmdDeleteTone.FlatTop = false;
            cmdDeleteTone.Location = new Point(168, 500);
            cmdDeleteTone.Name = "cmdDeleteTone";
            cmdDeleteTone.Padding = new Padding(5);
            cmdDeleteTone.Size = new Size(75, 28);
            cmdDeleteTone.TabIndex = 5;
            cmdDeleteTone.Text = "Delete";
            cmdDeleteTone.Click += cmdDeleteTone_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(chkAutoPlay);
            panel2.Controls.Add(cmdADSR);
            panel2.Controls.Add(cmdPlayTone);
            panel2.Controls.Add(numNote);
            panel2.Controls.Add(numVAG);
            panel2.Controls.Add(lbNote);
            panel2.Controls.Add(lbVAG);
            panel2.Location = new Point(513, 22);
            panel2.Name = "panel2";
            panel2.Size = new Size(127, 142);
            panel2.TabIndex = 7;
            // 
            // chkAutoPlay
            // 
            chkAutoPlay.AutoSize = true;
            chkAutoPlay.Location = new Point(69, 117);
            chkAutoPlay.Name = "chkAutoPlay";
            chkAutoPlay.Size = new Size(52, 19);
            chkAutoPlay.TabIndex = 6;
            chkAutoPlay.Text = "Auto";
            chkAutoPlay.UseVisualStyleBackColor = true;
            // 
            // cmdADSR
            // 
            cmdADSR.BorderColour = Color.Empty;
            cmdADSR.CustomColour = false;
            cmdADSR.FlatBottom = false;
            cmdADSR.FlatTop = false;
            cmdADSR.Location = new Point(61, 20);
            cmdADSR.Name = "cmdADSR";
            cmdADSR.Padding = new Padding(5);
            cmdADSR.Size = new Size(60, 26);
            cmdADSR.TabIndex = 5;
            cmdADSR.Text = "ADSR";
            cmdADSR.Click += cmdADSR_Click;
            // 
            // cmdPlayTone
            // 
            cmdPlayTone.BorderColour = Color.Empty;
            cmdPlayTone.CustomColour = false;
            cmdPlayTone.FlatBottom = false;
            cmdPlayTone.FlatTop = false;
            cmdPlayTone.Location = new Point(3, 112);
            cmdPlayTone.Name = "cmdPlayTone";
            cmdPlayTone.Padding = new Padding(5);
            cmdPlayTone.Size = new Size(60, 26);
            cmdPlayTone.TabIndex = 5;
            cmdPlayTone.Text = "Play";
            cmdPlayTone.Click += cmdPlayTone_Click;
            // 
            // numNote
            // 
            numNote.Location = new Point(3, 74);
            numNote.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            numNote.Name = "numNote";
            numNote.Size = new Size(44, 23);
            numNote.TabIndex = 4;
            numNote.Value = new decimal(new int[] { 60, 0, 0, 0 });
            numNote.ValueChanged += numNote_ValueChanged;
            // 
            // numVAG
            // 
            numVAG.Location = new Point(3, 24);
            numVAG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numVAG.Name = "numVAG";
            numVAG.Size = new Size(44, 23);
            numVAG.TabIndex = 4;
            numVAG.ValueChanged += numVAG_ValueChanged;
            // 
            // lbNote
            // 
            lbNote.AutoSize = true;
            lbNote.BackColor = Color.Transparent;
            lbNote.Location = new Point(3, 53);
            lbNote.Margin = new Padding(3);
            lbNote.Name = "lbNote";
            lbNote.Size = new Size(33, 15);
            lbNote.TabIndex = 3;
            lbNote.Text = "Note";
            // 
            // lbVAG
            // 
            lbVAG.AutoSize = true;
            lbVAG.BackColor = Color.Transparent;
            lbVAG.Location = new Point(3, 3);
            lbVAG.Margin = new Padding(3);
            lbVAG.Name = "lbVAG";
            lbVAG.Size = new Size(29, 15);
            lbVAG.TabIndex = 3;
            lbVAG.Text = "VAG";
            // 
            // cmdInsertTone
            // 
            cmdInsertTone.BorderColour = Color.Empty;
            cmdInsertTone.CustomColour = false;
            cmdInsertTone.FlatBottom = false;
            cmdInsertTone.FlatTop = false;
            cmdInsertTone.Location = new Point(87, 500);
            cmdInsertTone.Name = "cmdInsertTone";
            cmdInsertTone.Padding = new Padding(5);
            cmdInsertTone.Size = new Size(75, 28);
            cmdInsertTone.TabIndex = 5;
            cmdInsertTone.Text = "Insert";
            cmdInsertTone.Click += cmdInsertTone_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.49875F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5024967F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.501874F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.501874F));
            tableLayoutPanel1.Controls.Add(lbVolume, 0, 0);
            tableLayoutPanel1.Controls.Add(trkVolume, 0, 1);
            tableLayoutPanel1.Controls.Add(lbPan, 1, 0);
            tableLayoutPanel1.Controls.Add(trkPan, 1, 1);
            tableLayoutPanel1.Controls.Add(lbCenter, 2, 0);
            tableLayoutPanel1.Controls.Add(trkCenter, 2, 1);
            tableLayoutPanel1.Controls.Add(lbPitch, 3, 0);
            tableLayoutPanel1.Controls.Add(trkPitch, 3, 1);
            tableLayoutPanel1.Controls.Add(lbMinNote, 4, 0);
            tableLayoutPanel1.Controls.Add(trkMinNote, 4, 1);
            tableLayoutPanel1.Controls.Add(lbMaxNote, 5, 0);
            tableLayoutPanel1.Controls.Add(trkMaxNote, 5, 1);
            tableLayoutPanel1.Controls.Add(lbPBmin, 6, 0);
            tableLayoutPanel1.Controls.Add(trkPBmin, 6, 1);
            tableLayoutPanel1.Controls.Add(lbPBmax, 7, 0);
            tableLayoutPanel1.Controls.Add(trkPBmax, 7, 1);
            tableLayoutPanel1.Location = new Point(87, 22);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 17.2795429F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 82.72045F));
            tableLayoutPanel1.Size = new Size(420, 142);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // lbVolume
            // 
            lbVolume.AutoSize = true;
            lbVolume.BackColor = Color.Transparent;
            lbVolume.Dock = DockStyle.Fill;
            lbVolume.Font = new Font("Segoe UI", 8.25F);
            lbVolume.Location = new Point(3, 3);
            lbVolume.Margin = new Padding(3);
            lbVolume.Name = "lbVolume";
            lbVolume.Size = new Size(46, 18);
            lbVolume.TabIndex = 3;
            lbVolume.Text = "Volume";
            // 
            // trkVolume
            // 
            trkVolume.BackColor = Color.FromArgb(31, 31, 32);
            trkVolume.Dock = DockStyle.Fill;
            trkVolume.Location = new Point(3, 27);
            trkVolume.Maximum = 255;
            trkVolume.Name = "trkVolume";
            trkVolume.Orientation = Orientation.Vertical;
            trkVolume.Size = new Size(46, 112);
            trkVolume.TabIndex = 5;
            trkVolume.Tag = "Volume";
            trkVolume.TickFrequency = 8;
            trkVolume.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbPan
            // 
            lbPan.AutoSize = true;
            lbPan.BackColor = Color.Transparent;
            lbPan.Dock = DockStyle.Fill;
            lbPan.Font = new Font("Segoe UI", 8.25F);
            lbPan.Location = new Point(55, 3);
            lbPan.Margin = new Padding(3);
            lbPan.Name = "lbPan";
            lbPan.Size = new Size(46, 18);
            lbPan.TabIndex = 3;
            lbPan.Text = "Pan";
            // 
            // trkPan
            // 
            trkPan.BackColor = Color.FromArgb(31, 31, 32);
            trkPan.Dock = DockStyle.Fill;
            trkPan.Location = new Point(55, 27);
            trkPan.Maximum = 127;
            trkPan.Name = "trkPan";
            trkPan.Orientation = Orientation.Vertical;
            trkPan.Size = new Size(46, 112);
            trkPan.TabIndex = 5;
            trkPan.Tag = "Pan";
            trkPan.TickFrequency = 31;
            trkPan.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbCenter
            // 
            lbCenter.AutoSize = true;
            lbCenter.BackColor = Color.Transparent;
            lbCenter.Dock = DockStyle.Fill;
            lbCenter.Font = new Font("Segoe UI", 8.25F);
            lbCenter.Location = new Point(107, 3);
            lbCenter.Margin = new Padding(3);
            lbCenter.Name = "lbCenter";
            lbCenter.Size = new Size(46, 18);
            lbCenter.TabIndex = 3;
            lbCenter.Text = "Center";
            // 
            // trkCenter
            // 
            trkCenter.BackColor = Color.FromArgb(31, 31, 32);
            trkCenter.Dock = DockStyle.Fill;
            trkCenter.Location = new Point(107, 27);
            trkCenter.Maximum = 127;
            trkCenter.Name = "trkCenter";
            trkCenter.Orientation = Orientation.Vertical;
            trkCenter.Size = new Size(46, 112);
            trkCenter.TabIndex = 5;
            trkCenter.Tag = "Center";
            trkCenter.TickFrequency = 8;
            trkCenter.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbPitch
            // 
            lbPitch.AutoSize = true;
            lbPitch.BackColor = Color.Transparent;
            lbPitch.Dock = DockStyle.Fill;
            lbPitch.Font = new Font("Segoe UI", 8.25F);
            lbPitch.Location = new Point(159, 3);
            lbPitch.Margin = new Padding(3);
            lbPitch.Name = "lbPitch";
            lbPitch.Size = new Size(46, 18);
            lbPitch.TabIndex = 3;
            lbPitch.Text = "Pitch";
            // 
            // trkPitch
            // 
            trkPitch.BackColor = Color.FromArgb(31, 31, 32);
            trkPitch.Dock = DockStyle.Fill;
            trkPitch.Location = new Point(159, 27);
            trkPitch.Maximum = 99;
            trkPitch.Name = "trkPitch";
            trkPitch.Orientation = Orientation.Vertical;
            trkPitch.Size = new Size(46, 112);
            trkPitch.TabIndex = 5;
            trkPitch.Tag = "Pitch";
            trkPitch.TickFrequency = 10;
            trkPitch.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbMinNote
            // 
            lbMinNote.AutoSize = true;
            lbMinNote.BackColor = Color.Transparent;
            lbMinNote.Dock = DockStyle.Fill;
            lbMinNote.Font = new Font("Segoe UI", 8.25F);
            lbMinNote.Location = new Point(211, 3);
            lbMinNote.Margin = new Padding(3);
            lbMinNote.Name = "lbMinNote";
            lbMinNote.Size = new Size(46, 18);
            lbMinNote.TabIndex = 3;
            lbMinNote.Text = "MinNote";
            // 
            // trkMinNote
            // 
            trkMinNote.BackColor = Color.FromArgb(31, 31, 32);
            trkMinNote.Dock = DockStyle.Fill;
            trkMinNote.Location = new Point(211, 27);
            trkMinNote.Maximum = 127;
            trkMinNote.Name = "trkMinNote";
            trkMinNote.Orientation = Orientation.Vertical;
            trkMinNote.Size = new Size(46, 112);
            trkMinNote.TabIndex = 5;
            trkMinNote.Tag = "MinNote";
            trkMinNote.TickFrequency = 8;
            trkMinNote.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbMaxNote
            // 
            lbMaxNote.AutoSize = true;
            lbMaxNote.BackColor = Color.Transparent;
            lbMaxNote.Dock = DockStyle.Fill;
            lbMaxNote.Font = new Font("Segoe UI", 8.25F);
            lbMaxNote.Location = new Point(263, 3);
            lbMaxNote.Margin = new Padding(3);
            lbMaxNote.Name = "lbMaxNote";
            lbMaxNote.Size = new Size(46, 18);
            lbMaxNote.TabIndex = 3;
            lbMaxNote.Text = "MaxNote";
            // 
            // trkMaxNote
            // 
            trkMaxNote.BackColor = Color.FromArgb(31, 31, 32);
            trkMaxNote.Dock = DockStyle.Fill;
            trkMaxNote.Location = new Point(263, 27);
            trkMaxNote.Maximum = 127;
            trkMaxNote.Name = "trkMaxNote";
            trkMaxNote.Orientation = Orientation.Vertical;
            trkMaxNote.Size = new Size(46, 112);
            trkMaxNote.TabIndex = 5;
            trkMaxNote.Tag = "MaxNote";
            trkMaxNote.TickFrequency = 8;
            trkMaxNote.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbPBmin
            // 
            lbPBmin.AutoSize = true;
            lbPBmin.BackColor = Color.Transparent;
            lbPBmin.Dock = DockStyle.Fill;
            lbPBmin.Font = new Font("Segoe UI", 8.25F);
            lbPBmin.Location = new Point(315, 3);
            lbPBmin.Margin = new Padding(3);
            lbPBmin.Name = "lbPBmin";
            lbPBmin.Size = new Size(46, 18);
            lbPBmin.TabIndex = 3;
            lbPBmin.Text = "PBmin";
            // 
            // trkPBmin
            // 
            trkPBmin.BackColor = Color.FromArgb(31, 31, 32);
            trkPBmin.Dock = DockStyle.Fill;
            trkPBmin.Location = new Point(315, 27);
            trkPBmin.Maximum = 127;
            trkPBmin.Name = "trkPBmin";
            trkPBmin.Orientation = Orientation.Vertical;
            trkPBmin.Size = new Size(46, 112);
            trkPBmin.TabIndex = 5;
            trkPBmin.Tag = "PBmin";
            trkPBmin.TickFrequency = 8;
            trkPBmin.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // lbPBmax
            // 
            lbPBmax.AutoSize = true;
            lbPBmax.BackColor = Color.Transparent;
            lbPBmax.Dock = DockStyle.Fill;
            lbPBmax.Font = new Font("Segoe UI", 8.25F);
            lbPBmax.Location = new Point(367, 3);
            lbPBmax.Margin = new Padding(3);
            lbPBmax.Name = "lbPBmax";
            lbPBmax.Size = new Size(50, 18);
            lbPBmax.TabIndex = 3;
            lbPBmax.Text = "PBmax";
            // 
            // trkPBmax
            // 
            trkPBmax.BackColor = Color.FromArgb(31, 31, 32);
            trkPBmax.Dock = DockStyle.Fill;
            trkPBmax.Location = new Point(367, 27);
            trkPBmax.Maximum = 127;
            trkPBmax.Name = "trkPBmax";
            trkPBmax.Orientation = Orientation.Vertical;
            trkPBmax.Size = new Size(50, 112);
            trkPBmax.TabIndex = 5;
            trkPBmax.Tag = "PBmax";
            trkPBmax.TickFrequency = 8;
            trkPBmax.ValueChanged += tonesTrackBar_ValueChanged;
            // 
            // cmdAppendTone
            // 
            cmdAppendTone.BorderColour = Color.Empty;
            cmdAppendTone.CustomColour = false;
            cmdAppendTone.FlatBottom = false;
            cmdAppendTone.FlatTop = false;
            cmdAppendTone.Location = new Point(6, 500);
            cmdAppendTone.Name = "cmdAppendTone";
            cmdAppendTone.Padding = new Padding(5);
            cmdAppendTone.Size = new Size(75, 28);
            cmdAppendTone.TabIndex = 5;
            cmdAppendTone.Text = "Append";
            cmdAppendTone.Click += cmdAppendTone_Click;
            // 
            // dgvTones
            // 
            dgvTones.AllowUserToAddRows = false;
            dgvTones.AllowUserToResizeColumns = false;
            dgvTones.AllowUserToResizeRows = false;
            dgvTones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTones.ColumnHeadersHeight = 24;
            dgvTones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTones.Location = new Point(6, 170);
            dgvTones.MultiSelect = false;
            dgvTones.Name = "dgvTones";
            dgvTones.RowHeadersWidth = 24;
            dgvTones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvTones.Size = new Size(591, 324);
            dgvTones.TabIndex = 1;
            dgvTones.SelectionChanged += dgvTones_SelectionChanged;
            // 
            // VABTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1152, 571);
            Controls.Add(fraVABPrograms);
            Controls.Add(fraTones);
            Controls.Add(fraVABHeader);
            Controls.Add(toolStrip);
            CornerStyle = CornerPreference.Default;
            Name = "VABTool";
            Text = "VAB Tool";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            FormClosed += VABTool_FormClosed;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            fraVABHeader.ResumeLayout(false);
            fraVABPrograms.ResumeLayout(false);
            fraVABPrograms.PerformLayout();
            pnProgramControls.ResumeLayout(false);
            pnProgramControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).EndInit();
            fraTones.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPriority).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNote).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVAG).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkPan).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkCenter).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkPitch).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkMinNote).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkMaxNote).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkPBmin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkPBmax).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton tbbOpen;
        private ToolStripButton tbbSave;
        private DataGridView dgvHeader;
        private DarkGroupBox fraVABHeader;
        private DarkGroupBox fraVABPrograms;
        private DataGridView dgvPrograms;
        private MetroSet_UI.Controls.MetroSetTrackBar trkProgramVolume;
        private MetroSetTrackBar trkProgramPan;
        private Label lbProgramPan;
        private Label lbProgramVolume;
        private Panel pnProgramControls;
        private DarkGroupBox fraTones;
        private DataGridView dgvTones;
        private TrackBar trkVolume;
        private Label lbVolume;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbPan;
        private TrackBar trkPan;
        private Label lbCenter;
        private TrackBar trkCenter;
        private Label lbPitch;
        private TrackBar trkPitch;
        private Label lbMinNote;
        private TrackBar trkMinNote;
        private Label lbMaxNote;
        private TrackBar trkMaxNote;
        private Label lbPBmin;
        private TrackBar trkPBmin;
        private Label lbPBmax;
        private TrackBar trkPBmax;
        private Panel panel2;
        private Label lbVAG;
        private DarkNumericUpDown numVAG;
        private DarkNumericUpDown numNote;
        private DarkButton cmdPlayTone;
        private Label lbNote;
        private CheckBox chkAutoPlay;
        private DarkButton cmdDeleteProgram;
        private DarkButton cmdInsertProgram;
        private DarkButton cmdAppendProgram;
        private DarkButton cmdDeleteTone;
        private DarkButton cmdInsertTone;
        private DarkButton cmdAppendTone;
        private Panel panel1;
        private Label lbPriority;
        private DarkNumericUpDown numPriority;
        private Label lbMode;
        private DarkNumericUpDown numMode;
        private Label lbTone;
        private DarkButton cmdViewVAG;
        private ToolStripButton tbbClose;
        private DarkButton cmdPreviewVAB;
        private DarkButton cmdADSR;
    }
}