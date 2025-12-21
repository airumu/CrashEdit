using System.Windows.Forms;
using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
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
            toolStripSeparator3 = new ToolStripSeparator();
            tbbSave = new ToolStripButton();
            tbbSaveAs = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            tbbClose = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tbdExport = new ToolStripDropDownButton();
            tbbExportSF2 = new ToolStripMenuItem();
            tbbExportDLS = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            tbbExportSettings = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            tblSaved = new ToolStripLabel();
            dgvHeader = new DataGridView();
            fraVABHeader = new DarkGroupBox();
            trkMasterPan = new MetroSetTrackBar();
            trkMasterVolume = new MetroSetTrackBar();
            cmdPreviewVAB = new DarkButton();
            cmdViewVAG = new DarkButton();
            fraVABPrograms = new DarkGroupBox();
            fraProgramCommands = new DarkGroupBox();
            cmdAppendProgram = new DarkButton();
            cmdDeleteProgram = new DarkButton();
            cmdInsertProgram = new DarkButton();
            fraProgramControls = new DarkGroupBox();
            trkProgramVolume = new MetroSetTrackBar();
            lbProgramPan = new Label();
            trkProgramPan = new MetroSetTrackBar();
            lbProgramVolume = new Label();
            dgvPrograms = new DataGridView();
            fraTones = new DarkGroupBox();
            pnToneControls1 = new Panel();
            lbMode = new Label();
            lbTone = new Label();
            lbPriority = new Label();
            numMode = new DarkNumericUpDown();
            numPriority = new DarkNumericUpDown();
            cmdDeleteTone = new DarkButton();
            pnToneControls2 = new Panel();
            fraPlay = new DarkGroupBox();
            chkAutoPlay = new CheckBox();
            lbNote = new Label();
            numNote = new DarkNumericUpDown();
            cmdPlayTone = new DarkButton();
            cmdADSR = new DarkButton();
            numVAG = new DarkNumericUpDown();
            lbVAG = new Label();
            cmdInsertTone = new DarkButton();
            tblToneControls = new TableLayoutPanel();
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
            fraProgramCommands.SuspendLayout();
            fraProgramControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).BeginInit();
            fraTones.SuspendLayout();
            pnToneControls1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPriority).BeginInit();
            pnToneControls2.SuspendLayout();
            fraPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVAG).BeginInit();
            tblToneControls.SuspendLayout();
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
            toolStrip.Items.AddRange(new ToolStripItem[] { tbbOpen, toolStripSeparator3, tbbSave, tbbSaveAs, toolStripSeparator4, tbbClose, toolStripSeparator1, tbdExport, toolStripSeparator2, tblSaved });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1168, 25);
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
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
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
            // tbbSaveAs
            // 
            tbbSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbbSaveAs.Image = (Image)resources.GetObject("tbbSaveAs.Image");
            tbbSaveAs.ImageTransparentColor = Color.Magenta;
            tbbSaveAs.Name = "tbbSaveAs";
            tbbSaveAs.Size = new Size(23, 22);
            tbbSaveAs.Text = "SaveAs";
            tbbSaveAs.Click += tbbSaveAs_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tbdExport
            // 
            tbdExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tbdExport.DropDownItems.AddRange(new ToolStripItem[] { tbbExportSF2, tbbExportDLS, toolStripMenuItem1, tbbExportSettings });
            tbdExport.Image = (Image)resources.GetObject("tbdExport.Image");
            tbdExport.ImageTransparentColor = Color.Magenta;
            tbdExport.Name = "tbdExport";
            tbdExport.Size = new Size(29, 22);
            tbdExport.Text = "Export";
            // 
            // tbbExportSF2
            // 
            tbbExportSF2.Name = "tbbExportSF2";
            tbbExportSF2.Size = new Size(153, 22);
            tbbExportSF2.Text = "Export as SF2";
            tbbExportSF2.Click += tbbExportSF2_Click;
            // 
            // tbbExportDLS
            // 
            tbbExportDLS.Name = "tbbExportDLS";
            tbbExportDLS.Size = new Size(153, 22);
            tbbExportDLS.Text = "Export as DLS";
            tbbExportDLS.Click += tbbExportDLS_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(150, 6);
            // 
            // tbbExportSettings
            // 
            tbbExportSettings.Name = "tbbExportSettings";
            tbbExportSettings.Size = new Size(153, 22);
            tbbExportSettings.Text = "Export Settings";
            tbbExportSettings.Click += exportOptionsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // tblSaved
            // 
            tblSaved.ForeColor = Color.MediumSpringGreen;
            tblSaved.Name = "tblSaved";
            tblSaved.Size = new Size(65, 22);
            tblSaved.Text = "VAB Saved!";
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
            dgvHeader.ReadOnly = true;
            dgvHeader.RowHeadersVisible = false;
            dgvHeader.RowHeadersWidth = 24;
            dgvHeader.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvHeader.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvHeader.ShowCellToolTips = false;
            dgvHeader.Size = new Size(446, 60);
            dgvHeader.TabIndex = 1;
            // 
            // fraVABHeader
            // 
            fraVABHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraVABHeader.BackColor = Color.Transparent;
            fraVABHeader.Controls.Add(trkMasterPan);
            fraVABHeader.Controls.Add(trkMasterVolume);
            fraVABHeader.Controls.Add(dgvHeader);
            fraVABHeader.Controls.Add(cmdPreviewVAB);
            fraVABHeader.Controls.Add(cmdViewVAG);
            fraVABHeader.Location = new Point(12, 30);
            fraVABHeader.Name = "fraVABHeader";
            fraVABHeader.Size = new Size(456, 124);
            fraVABHeader.TabIndex = 3;
            fraVABHeader.TabStop = false;
            fraVABHeader.Text = "VAB File Settings";
            // 
            // trkMasterPan
            // 
            trkMasterPan.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkMasterPan.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkMasterPan.DisabledBorderColor = Color.Empty;
            trkMasterPan.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkMasterPan.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkMasterPan.HandlerColor = Color.FromArgb(143, 143, 143);
            trkMasterPan.IsDerivedStyle = true;
            trkMasterPan.Location = new Point(388, 88);
            trkMasterPan.Maximum = 127;
            trkMasterPan.Minimum = 0;
            trkMasterPan.Name = "trkMasterPan";
            trkMasterPan.Size = new Size(59, 16);
            trkMasterPan.Style = MetroSet_UI.Enums.Style.Dark;
            trkMasterPan.StyleManager = null;
            trkMasterPan.TabIndex = 2;
            trkMasterPan.Text = "ProgramVolume";
            trkMasterPan.ThemeAuthor = "Narwin";
            trkMasterPan.ThemeName = "MetroDark";
            trkMasterPan.TickFrequency = 1;
            trkMasterPan.Value = 0;
            trkMasterPan.ValueColor = Color.FromArgb(65, 177, 225);
            trkMasterPan.ValueChanged += trkMasterPan_ValueChanged;
            trkMasterPan.MouseWheel += trkScrollHandlerFunction;
            // 
            // trkMasterVolume
            // 
            trkMasterVolume.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkMasterVolume.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkMasterVolume.DisabledBorderColor = Color.Empty;
            trkMasterVolume.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkMasterVolume.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkMasterVolume.HandlerColor = Color.FromArgb(143, 143, 143);
            trkMasterVolume.IsDerivedStyle = true;
            trkMasterVolume.Location = new Point(334, 88);
            trkMasterVolume.Maximum = 255;
            trkMasterVolume.Minimum = 0;
            trkMasterVolume.Name = "trkMasterVolume";
            trkMasterVolume.Size = new Size(59, 16);
            trkMasterVolume.Style = MetroSet_UI.Enums.Style.Dark;
            trkMasterVolume.StyleManager = null;
            trkMasterVolume.TabIndex = 2;
            trkMasterVolume.Text = "ProgramVolume";
            trkMasterVolume.ThemeAuthor = "Narwin";
            trkMasterVolume.ThemeName = "MetroDark";
            trkMasterVolume.TickFrequency = 1;
            trkMasterVolume.Value = 0;
            trkMasterVolume.ValueColor = Color.SpringGreen;
            trkMasterVolume.ValueChanged += trkMasterVolume_ValueChanged;
            trkMasterVolume.MouseWheel += trkScrollHandlerFunction;
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
            fraVABPrograms.Controls.Add(fraProgramCommands);
            fraVABPrograms.Controls.Add(fraProgramControls);
            fraVABPrograms.Controls.Add(dgvPrograms);
            fraVABPrograms.Location = new Point(12, 160);
            fraVABPrograms.Name = "fraVABPrograms";
            fraVABPrograms.Size = new Size(456, 488);
            fraVABPrograms.TabIndex = 3;
            fraVABPrograms.TabStop = false;
            fraVABPrograms.Text = "Programs";
            // 
            // fraProgramCommands
            // 
            fraProgramCommands.AutoSize = true;
            fraProgramCommands.Controls.Add(cmdAppendProgram);
            fraProgramCommands.Controls.Add(cmdDeleteProgram);
            fraProgramCommands.Controls.Add(cmdInsertProgram);
            fraProgramCommands.Location = new Point(302, 157);
            fraProgramCommands.Name = "fraProgramCommands";
            fraProgramCommands.Size = new Size(142, 140);
            fraProgramCommands.TabIndex = 7;
            fraProgramCommands.TabStop = false;
            // 
            // cmdAppendProgram
            // 
            cmdAppendProgram.BorderColour = Color.Empty;
            cmdAppendProgram.CustomColour = false;
            cmdAppendProgram.FlatBottom = false;
            cmdAppendProgram.FlatTop = false;
            cmdAppendProgram.Location = new Point(6, 22);
            cmdAppendProgram.Name = "cmdAppendProgram";
            cmdAppendProgram.Padding = new Padding(5);
            cmdAppendProgram.Size = new Size(75, 28);
            cmdAppendProgram.TabIndex = 5;
            cmdAppendProgram.Text = "Append";
            cmdAppendProgram.Click += cmdAppendProgram_Click;
            // 
            // cmdDeleteProgram
            // 
            cmdDeleteProgram.BorderColour = Color.Empty;
            cmdDeleteProgram.CustomColour = false;
            cmdDeleteProgram.FlatBottom = false;
            cmdDeleteProgram.FlatTop = false;
            cmdDeleteProgram.Location = new Point(6, 90);
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
            cmdInsertProgram.Location = new Point(6, 56);
            cmdInsertProgram.Name = "cmdInsertProgram";
            cmdInsertProgram.Padding = new Padding(5);
            cmdInsertProgram.Size = new Size(75, 28);
            cmdInsertProgram.TabIndex = 5;
            cmdInsertProgram.Text = "Insert";
            cmdInsertProgram.Click += cmdInsertProgram_Click;
            // 
            // fraProgramControls
            // 
            fraProgramControls.AutoSize = true;
            fraProgramControls.Controls.Add(trkProgramVolume);
            fraProgramControls.Controls.Add(lbProgramPan);
            fraProgramControls.Controls.Add(trkProgramPan);
            fraProgramControls.Controls.Add(lbProgramVolume);
            fraProgramControls.Location = new Point(302, 22);
            fraProgramControls.Name = "fraProgramControls";
            fraProgramControls.Size = new Size(142, 129);
            fraProgramControls.TabIndex = 6;
            fraProgramControls.TabStop = false;
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
            trkProgramVolume.Location = new Point(6, 38);
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
            trkProgramVolume.MouseWheel += trkScrollHandlerFunction;
            // 
            // lbProgramPan
            // 
            lbProgramPan.AutoSize = true;
            lbProgramPan.BackColor = Color.Transparent;
            lbProgramPan.Location = new Point(6, 73);
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
            trkProgramPan.Location = new Point(6, 91);
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
            trkProgramPan.MouseWheel += trkScrollHandlerFunction;
            // 
            // lbProgramVolume
            // 
            lbProgramVolume.AutoSize = true;
            lbProgramVolume.BackColor = Color.Transparent;
            lbProgramVolume.Location = new Point(6, 20);
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
            dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrograms.ShowCellToolTips = false;
            dgvPrograms.Size = new Size(290, 456);
            dgvPrograms.TabIndex = 1;
            dgvPrograms.CellBeginEdit += dgvPrograms_CellBeginEdit;
            dgvPrograms.CellValidating += dgvPrograms_CellValidating;
            dgvPrograms.CellValueChanged += dgvPrograms_CellValueChanged;
            dgvPrograms.SelectionChanged += dgvVABPrograms_SelectionChanged;
            // 
            // fraTones
            // 
            fraTones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTones.BackColor = Color.Transparent;
            fraTones.Controls.Add(pnToneControls1);
            fraTones.Controls.Add(cmdDeleteTone);
            fraTones.Controls.Add(pnToneControls2);
            fraTones.Controls.Add(cmdInsertTone);
            fraTones.Controls.Add(tblToneControls);
            fraTones.Controls.Add(cmdAppendTone);
            fraTones.Controls.Add(dgvTones);
            fraTones.Location = new Point(476, 30);
            fraTones.Name = "fraTones";
            fraTones.Size = new Size(682, 618);
            fraTones.TabIndex = 3;
            fraTones.TabStop = false;
            fraTones.Text = "Tone Attributes Table";
            // 
            // pnToneControls1
            // 
            pnToneControls1.Controls.Add(lbMode);
            pnToneControls1.Controls.Add(lbTone);
            pnToneControls1.Controls.Add(lbPriority);
            pnToneControls1.Controls.Add(numMode);
            pnToneControls1.Controls.Add(numPriority);
            pnToneControls1.Location = new Point(7, 22);
            pnToneControls1.Name = "pnToneControls1";
            pnToneControls1.Size = new Size(57, 142);
            pnToneControls1.TabIndex = 8;
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
            lbTone.Location = new Point(3, 123);
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
            numMode.Location = new Point(3, 72);
            numMode.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numMode.Name = "numMode";
            numMode.Size = new Size(44, 23);
            numMode.TabIndex = 4;
            numMode.ValueChanged += numMode_ValueChanged;
            // 
            // numPriority
            // 
            numPriority.Location = new Point(3, 22);
            numPriority.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
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
            cmdDeleteTone.Location = new Point(168, 584);
            cmdDeleteTone.Name = "cmdDeleteTone";
            cmdDeleteTone.Padding = new Padding(5);
            cmdDeleteTone.Size = new Size(75, 28);
            cmdDeleteTone.TabIndex = 5;
            cmdDeleteTone.Text = "Delete";
            cmdDeleteTone.Click += cmdDeleteTone_Click;
            // 
            // pnToneControls2
            // 
            pnToneControls2.Controls.Add(fraPlay);
            pnToneControls2.Controls.Add(cmdADSR);
            pnToneControls2.Controls.Add(numVAG);
            pnToneControls2.Controls.Add(lbVAG);
            pnToneControls2.Location = new Point(549, 22);
            pnToneControls2.Name = "pnToneControls2";
            pnToneControls2.Size = new Size(127, 142);
            pnToneControls2.TabIndex = 7;
            // 
            // fraPlay
            // 
            fraPlay.Controls.Add(chkAutoPlay);
            fraPlay.Controls.Add(lbNote);
            fraPlay.Controls.Add(numNote);
            fraPlay.Controls.Add(cmdPlayTone);
            fraPlay.Location = new Point(3, 54);
            fraPlay.Name = "fraPlay";
            fraPlay.Size = new Size(121, 85);
            fraPlay.TabIndex = 7;
            fraPlay.TabStop = false;
            // 
            // chkAutoPlay
            // 
            chkAutoPlay.AutoSize = true;
            chkAutoPlay.Location = new Point(68, 58);
            chkAutoPlay.Name = "chkAutoPlay";
            chkAutoPlay.Size = new Size(52, 19);
            chkAutoPlay.TabIndex = 6;
            chkAutoPlay.Text = "Auto";
            chkAutoPlay.UseVisualStyleBackColor = true;
            // 
            // lbNote
            // 
            lbNote.AutoSize = true;
            lbNote.BackColor = Color.Transparent;
            lbNote.Location = new Point(6, 4);
            lbNote.Margin = new Padding(3);
            lbNote.Name = "lbNote";
            lbNote.Size = new Size(33, 15);
            lbNote.TabIndex = 3;
            lbNote.Text = "Note";
            // 
            // numNote
            // 
            numNote.Location = new Point(5, 25);
            numNote.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            numNote.Name = "numNote";
            numNote.Size = new Size(44, 23);
            numNote.TabIndex = 4;
            numNote.Value = new decimal(new int[] { 60, 0, 0, 0 });
            numNote.ValueChanged += numNote_ValueChanged;
            // 
            // cmdPlayTone
            // 
            cmdPlayTone.BorderColour = Color.Empty;
            cmdPlayTone.CustomColour = false;
            cmdPlayTone.FlatBottom = false;
            cmdPlayTone.FlatTop = false;
            cmdPlayTone.Location = new Point(5, 54);
            cmdPlayTone.Name = "cmdPlayTone";
            cmdPlayTone.Padding = new Padding(5);
            cmdPlayTone.Size = new Size(60, 26);
            cmdPlayTone.TabIndex = 5;
            cmdPlayTone.Text = "Play";
            cmdPlayTone.Click += cmdPlayTone_Click;
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
            // numVAG
            // 
            numVAG.Location = new Point(5, 22);
            numVAG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numVAG.Name = "numVAG";
            numVAG.Size = new Size(44, 23);
            numVAG.TabIndex = 4;
            numVAG.ValueChanged += numVAG_ValueChanged;
            // 
            // lbVAG
            // 
            lbVAG.AutoSize = true;
            lbVAG.BackColor = Color.Transparent;
            lbVAG.Location = new Point(5, 3);
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
            cmdInsertTone.Location = new Point(87, 584);
            cmdInsertTone.Name = "cmdInsertTone";
            cmdInsertTone.Padding = new Padding(5);
            cmdInsertTone.Size = new Size(75, 28);
            cmdInsertTone.TabIndex = 5;
            cmdInsertTone.Text = "Insert";
            cmdInsertTone.Click += cmdInsertTone_Click;
            // 
            // tblToneControls
            // 
            tblToneControls.ColumnCount = 8;
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.49875F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.4987507F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5024967F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.501874F));
            tblToneControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.501874F));
            tblToneControls.Controls.Add(lbVolume, 0, 0);
            tblToneControls.Controls.Add(trkVolume, 0, 1);
            tblToneControls.Controls.Add(lbPan, 1, 0);
            tblToneControls.Controls.Add(trkPan, 1, 1);
            tblToneControls.Controls.Add(lbCenter, 2, 0);
            tblToneControls.Controls.Add(trkCenter, 2, 1);
            tblToneControls.Controls.Add(lbPitch, 3, 0);
            tblToneControls.Controls.Add(trkPitch, 3, 1);
            tblToneControls.Controls.Add(lbMinNote, 4, 0);
            tblToneControls.Controls.Add(trkMinNote, 4, 1);
            tblToneControls.Controls.Add(lbMaxNote, 5, 0);
            tblToneControls.Controls.Add(trkMaxNote, 5, 1);
            tblToneControls.Controls.Add(lbPBmin, 6, 0);
            tblToneControls.Controls.Add(trkPBmin, 6, 1);
            tblToneControls.Controls.Add(lbPBmax, 7, 0);
            tblToneControls.Controls.Add(trkPBmax, 7, 1);
            tblToneControls.Location = new Point(67, 22);
            tblToneControls.Name = "tblToneControls";
            tblToneControls.RowCount = 2;
            tblToneControls.RowStyles.Add(new RowStyle(SizeType.Percent, 22.5352116F));
            tblToneControls.RowStyles.Add(new RowStyle(SizeType.Percent, 77.46479F));
            tblToneControls.Size = new Size(479, 142);
            tblToneControls.TabIndex = 6;
            // 
            // lbVolume
            // 
            lbVolume.AutoSize = true;
            lbVolume.BackColor = Color.Transparent;
            lbVolume.Dock = DockStyle.Fill;
            lbVolume.Font = new Font("Arial", 9F);
            lbVolume.ForeColor = SystemColors.MenuText;
            lbVolume.Location = new Point(1, 1);
            lbVolume.Margin = new Padding(1);
            lbVolume.Name = "lbVolume";
            lbVolume.Size = new Size(57, 30);
            lbVolume.TabIndex = 3;
            lbVolume.Text = "Volume\r\n(100%)";
            // 
            // trkVolume
            // 
            trkVolume.BackColor = Color.FromArgb(31, 31, 32);
            trkVolume.Dock = DockStyle.Fill;
            trkVolume.LargeChange = 1;
            trkVolume.Location = new Point(1, 33);
            trkVolume.Margin = new Padding(1);
            trkVolume.Maximum = 255;
            trkVolume.Name = "trkVolume";
            trkVolume.Orientation = Orientation.Vertical;
            trkVolume.Size = new Size(57, 108);
            trkVolume.TabIndex = 5;
            trkVolume.Tag = "Volume";
            trkVolume.TickFrequency = 16;
            trkVolume.ValueChanged += tonesTrackBar_ValueChanged;
            trkVolume.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbPan
            // 
            lbPan.AutoSize = true;
            lbPan.BackColor = Color.Transparent;
            lbPan.Dock = DockStyle.Fill;
            lbPan.Font = new Font("Arial", 9F);
            lbPan.ForeColor = SystemColors.MenuText;
            lbPan.Location = new Point(60, 1);
            lbPan.Margin = new Padding(1);
            lbPan.Name = "lbPan";
            lbPan.Size = new Size(57, 30);
            lbPan.TabIndex = 3;
            lbPan.Text = "Pan\r\n(0.00)";
            // 
            // trkPan
            // 
            trkPan.BackColor = Color.FromArgb(31, 31, 32);
            trkPan.Dock = DockStyle.Fill;
            trkPan.LargeChange = 1;
            trkPan.Location = new Point(60, 33);
            trkPan.Margin = new Padding(1);
            trkPan.Maximum = 127;
            trkPan.Name = "trkPan";
            trkPan.Orientation = Orientation.Vertical;
            trkPan.Size = new Size(57, 108);
            trkPan.TabIndex = 5;
            trkPan.Tag = "Pan";
            trkPan.TickFrequency = 31;
            trkPan.ValueChanged += tonesTrackBar_ValueChanged;
            trkPan.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbCenter
            // 
            lbCenter.AutoSize = true;
            lbCenter.BackColor = Color.Transparent;
            lbCenter.Dock = DockStyle.Fill;
            lbCenter.Font = new Font("Arial", 9F);
            lbCenter.ForeColor = SystemColors.MenuText;
            lbCenter.Location = new Point(119, 1);
            lbCenter.Margin = new Padding(1);
            lbCenter.Name = "lbCenter";
            lbCenter.Size = new Size(57, 30);
            lbCenter.TabIndex = 3;
            lbCenter.Text = "Center\r\n(C0)";
            // 
            // trkCenter
            // 
            trkCenter.BackColor = Color.FromArgb(31, 31, 32);
            trkCenter.Dock = DockStyle.Fill;
            trkCenter.LargeChange = 1;
            trkCenter.Location = new Point(119, 33);
            trkCenter.Margin = new Padding(1);
            trkCenter.Maximum = 127;
            trkCenter.Name = "trkCenter";
            trkCenter.Orientation = Orientation.Vertical;
            trkCenter.Size = new Size(57, 108);
            trkCenter.TabIndex = 5;
            trkCenter.Tag = "Center";
            trkCenter.TickFrequency = 8;
            trkCenter.ValueChanged += tonesTrackBar_ValueChanged;
            trkCenter.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbPitch
            // 
            lbPitch.AutoSize = true;
            lbPitch.BackColor = Color.Transparent;
            lbPitch.Dock = DockStyle.Fill;
            lbPitch.Font = new Font("Arial", 9F);
            lbPitch.ForeColor = SystemColors.MenuText;
            lbPitch.Location = new Point(178, 1);
            lbPitch.Margin = new Padding(1);
            lbPitch.Name = "lbPitch";
            lbPitch.Size = new Size(57, 30);
            lbPitch.TabIndex = 3;
            lbPitch.Text = "Pitch\r\n(0)";
            // 
            // trkPitch
            // 
            trkPitch.BackColor = Color.FromArgb(31, 31, 32);
            trkPitch.Dock = DockStyle.Fill;
            trkPitch.LargeChange = 1;
            trkPitch.Location = new Point(178, 33);
            trkPitch.Margin = new Padding(1);
            trkPitch.Maximum = 99;
            trkPitch.Name = "trkPitch";
            trkPitch.Orientation = Orientation.Vertical;
            trkPitch.Size = new Size(57, 108);
            trkPitch.TabIndex = 5;
            trkPitch.Tag = "Pitch";
            trkPitch.TickFrequency = 10;
            trkPitch.ValueChanged += tonesTrackBar_ValueChanged;
            trkPitch.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbMinNote
            // 
            lbMinNote.AutoSize = true;
            lbMinNote.BackColor = Color.Transparent;
            lbMinNote.Dock = DockStyle.Fill;
            lbMinNote.Font = new Font("Arial", 9F);
            lbMinNote.ForeColor = SystemColors.MenuText;
            lbMinNote.Location = new Point(237, 1);
            lbMinNote.Margin = new Padding(1);
            lbMinNote.Name = "lbMinNote";
            lbMinNote.Size = new Size(57, 30);
            lbMinNote.TabIndex = 3;
            lbMinNote.Text = "MinNote\r\n(0)";
            // 
            // trkMinNote
            // 
            trkMinNote.BackColor = Color.FromArgb(31, 31, 32);
            trkMinNote.Dock = DockStyle.Fill;
            trkMinNote.LargeChange = 1;
            trkMinNote.Location = new Point(237, 33);
            trkMinNote.Margin = new Padding(1);
            trkMinNote.Maximum = 127;
            trkMinNote.Name = "trkMinNote";
            trkMinNote.Orientation = Orientation.Vertical;
            trkMinNote.Size = new Size(57, 108);
            trkMinNote.TabIndex = 5;
            trkMinNote.Tag = "MinNote";
            trkMinNote.TickFrequency = 8;
            trkMinNote.ValueChanged += tonesTrackBar_ValueChanged;
            trkMinNote.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbMaxNote
            // 
            lbMaxNote.AutoSize = true;
            lbMaxNote.BackColor = Color.Transparent;
            lbMaxNote.Dock = DockStyle.Fill;
            lbMaxNote.Font = new Font("Arial", 9F);
            lbMaxNote.ForeColor = SystemColors.MenuText;
            lbMaxNote.Location = new Point(296, 1);
            lbMaxNote.Margin = new Padding(1);
            lbMaxNote.Name = "lbMaxNote";
            lbMaxNote.Size = new Size(57, 30);
            lbMaxNote.TabIndex = 3;
            lbMaxNote.Text = "MaxNote\r\n(0)";
            // 
            // trkMaxNote
            // 
            trkMaxNote.BackColor = Color.FromArgb(31, 31, 32);
            trkMaxNote.Dock = DockStyle.Fill;
            trkMaxNote.LargeChange = 1;
            trkMaxNote.Location = new Point(296, 33);
            trkMaxNote.Margin = new Padding(1);
            trkMaxNote.Maximum = 127;
            trkMaxNote.Name = "trkMaxNote";
            trkMaxNote.Orientation = Orientation.Vertical;
            trkMaxNote.Size = new Size(57, 108);
            trkMaxNote.TabIndex = 5;
            trkMaxNote.Tag = "MaxNote";
            trkMaxNote.TickFrequency = 8;
            trkMaxNote.ValueChanged += tonesTrackBar_ValueChanged;
            trkMaxNote.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbPBmin
            // 
            lbPBmin.AutoSize = true;
            lbPBmin.BackColor = Color.Transparent;
            lbPBmin.Dock = DockStyle.Fill;
            lbPBmin.Font = new Font("Arial", 9F);
            lbPBmin.ForeColor = SystemColors.MenuText;
            lbPBmin.Location = new Point(355, 1);
            lbPBmin.Margin = new Padding(1);
            lbPBmin.Name = "lbPBmin";
            lbPBmin.Size = new Size(57, 30);
            lbPBmin.TabIndex = 3;
            lbPBmin.Text = "PBmin\r\n(0)";
            // 
            // trkPBmin
            // 
            trkPBmin.BackColor = Color.FromArgb(31, 31, 32);
            trkPBmin.Dock = DockStyle.Fill;
            trkPBmin.LargeChange = 1;
            trkPBmin.Location = new Point(355, 33);
            trkPBmin.Margin = new Padding(1);
            trkPBmin.Maximum = 127;
            trkPBmin.Name = "trkPBmin";
            trkPBmin.Orientation = Orientation.Vertical;
            trkPBmin.Size = new Size(57, 108);
            trkPBmin.TabIndex = 5;
            trkPBmin.Tag = "PBmin";
            trkPBmin.TickFrequency = 8;
            trkPBmin.ValueChanged += tonesTrackBar_ValueChanged;
            trkPBmin.MouseWheel += trkScrollHandlerFunction2;
            // 
            // lbPBmax
            // 
            lbPBmax.AutoSize = true;
            lbPBmax.BackColor = Color.Transparent;
            lbPBmax.Dock = DockStyle.Fill;
            lbPBmax.Font = new Font("Arial", 9F);
            lbPBmax.ForeColor = SystemColors.MenuText;
            lbPBmax.Location = new Point(414, 1);
            lbPBmax.Margin = new Padding(1);
            lbPBmax.Name = "lbPBmax";
            lbPBmax.Size = new Size(64, 30);
            lbPBmax.TabIndex = 3;
            lbPBmax.Text = "PBmax\r\n(0)";
            // 
            // trkPBmax
            // 
            trkPBmax.BackColor = Color.FromArgb(31, 31, 32);
            trkPBmax.Dock = DockStyle.Fill;
            trkPBmax.LargeChange = 1;
            trkPBmax.Location = new Point(414, 33);
            trkPBmax.Margin = new Padding(1);
            trkPBmax.Maximum = 127;
            trkPBmax.Name = "trkPBmax";
            trkPBmax.Orientation = Orientation.Vertical;
            trkPBmax.Size = new Size(64, 108);
            trkPBmax.TabIndex = 5;
            trkPBmax.Tag = "PBmax";
            trkPBmax.TickFrequency = 8;
            trkPBmax.ValueChanged += tonesTrackBar_ValueChanged;
            trkPBmax.MouseWheel += trkScrollHandlerFunction2;
            // 
            // cmdAppendTone
            // 
            cmdAppendTone.BorderColour = Color.Empty;
            cmdAppendTone.CustomColour = false;
            cmdAppendTone.FlatBottom = false;
            cmdAppendTone.FlatTop = false;
            cmdAppendTone.Location = new Point(6, 584);
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
            dgvTones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTones.ShowCellToolTips = false;
            dgvTones.Size = new Size(591, 408);
            dgvTones.TabIndex = 1;
            dgvTones.CellBeginEdit += dgvTones_CellBeginEdit;
            dgvTones.CellValidating += dgvTones_CellValidating;
            dgvTones.CellValueChanged += dgvTones_CellValueChanged;
            dgvTones.SelectionChanged += dgvTones_SelectionChanged;
            // 
            // VABTool
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1168, 659);
            Controls.Add(fraVABPrograms);
            Controls.Add(fraTones);
            Controls.Add(fraVABHeader);
            Controls.Add(toolStrip);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "VABTool";
            Text = "VAB Tool";
            Icon = Embeds.GetIcon("MusicNoteBlue");
            TransparencyKey = Color.FromArgb(31, 31, 32);
            FormClosing += VABTool_FormClosing;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            fraVABHeader.ResumeLayout(false);
            fraVABPrograms.ResumeLayout(false);
            fraVABPrograms.PerformLayout();
            fraProgramCommands.ResumeLayout(false);
            fraProgramControls.ResumeLayout(false);
            fraProgramControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).EndInit();
            fraTones.ResumeLayout(false);
            pnToneControls1.ResumeLayout(false);
            pnToneControls1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPriority).EndInit();
            pnToneControls2.ResumeLayout(false);
            pnToneControls2.PerformLayout();
            fraPlay.ResumeLayout(false);
            fraPlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNote).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVAG).EndInit();
            tblToneControls.ResumeLayout(false);
            tblToneControls.PerformLayout();
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
        private TableLayoutPanel tblToneControls;
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
        private Panel pnToneControls2;
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
        private Panel pnToneControls1;
        private Label lbPriority;
        private DarkNumericUpDown numPriority;
        private Label lbMode;
        private DarkNumericUpDown numMode;
        private Label lbTone;
        private DarkButton cmdViewVAG;
        private ToolStripButton tbbClose;
        private DarkButton cmdPreviewVAB;
        private DarkButton cmdADSR;
        private DarkGroupBox fraProgramControls;
        private DarkGroupBox fraProgramCommands;
        private DarkGroupBox fraPlay;
        private MetroSetTrackBar trkMasterVolume;
        private MetroSetTrackBar trkMasterPan;
        private ToolStripButton tbbSaveAs;
        private ToolStripLabel tblSaved;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton tbdExport;
        private ToolStripMenuItem tbbExportSF2;
        private ToolStripMenuItem tbbExportDLS;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem tbbExportSettings;
    }
}