using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    partial class SoundBox
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
                waveOut.Stop();
                waveOut.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundBox));
            tsToolbar = new ToolStrip();
            tbbImport = new ToolStripButton();
            tbbExport = new ToolStripButton();
            cmdPlay = new DarkButton();
            cmdExport = new DarkButton();
            trkSampleRate = new MetroSetTrackBar();
            lblSampleRate = new Label();
            chkLoop = new CheckBox();
            numSampleRate = new DarkNumericUpDown();
            panel1 = new WaveformPanel();
            panel2 = new WaveformPanel();
            numSelStart = new DarkNumericUpDown();
            numSelSize = new DarkNumericUpDown();
            cmdSetLoop = new DarkButton();
            cmdClearLoop = new DarkButton();
            fraWaveform = new DarkGroupBox();
            darkGroupBox1 = new DarkGroupBox();
            tsToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSelStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSelSize).BeginInit();
            fraWaveform.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tsToolbar
            // 
            tsToolbar.AutoSize = false;
            tsToolbar.BackColor = Color.FromArgb(31, 31, 32);
            tsToolbar.ForeColor = Color.FromArgb(213, 213, 213);
            tsToolbar.Items.AddRange(new ToolStripItem[] { tbbImport, tbbExport });
            tsToolbar.Location = new Point(0, 0);
            tsToolbar.Name = "tsToolbar";
            tsToolbar.Padding = new Padding(5, 0, 1, 0);
            tsToolbar.Size = new Size(771, 28);
            tsToolbar.TabIndex = 0;
            tsToolbar.Text = "ToolStrip1";
            // 
            // tbbImport
            // 
            tbbImport.BackColor = Color.FromArgb(31, 31, 32);
            tbbImport.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tbbImport.ForeColor = Color.FromArgb(213, 213, 213);
            tbbImport.Image = (Image)resources.GetObject("tbbImport.Image");
            tbbImport.ImageTransparentColor = Color.Magenta;
            tbbImport.Name = "tbbImport";
            tbbImport.Size = new Size(47, 25);
            tbbImport.Text = "Import";
            tbbImport.Click += tbbImport_Click;
            // 
            // tbbExport
            // 
            tbbExport.BackColor = Color.FromArgb(31, 31, 32);
            tbbExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tbbExport.ForeColor = Color.FromArgb(213, 213, 213);
            tbbExport.Image = (Image)resources.GetObject("tbbExport.Image");
            tbbExport.ImageTransparentColor = Color.Magenta;
            tbbExport.Name = "tbbExport";
            tbbExport.Size = new Size(45, 25);
            tbbExport.Text = "Export";
            tbbExport.Click += tbbExport_Click;
            // 
            // cmdPlay
            // 
            cmdPlay.BorderColour = Color.Empty;
            cmdPlay.CustomColour = false;
            cmdPlay.FlatBottom = false;
            cmdPlay.FlatTop = false;
            cmdPlay.Location = new Point(3, 31);
            cmdPlay.Name = "cmdPlay";
            cmdPlay.Padding = new Padding(5);
            cmdPlay.Size = new Size(280, 280);
            cmdPlay.TabIndex = 1;
            cmdPlay.Text = "Play ({0}Hz)";
            cmdPlay.Click += cmdPlay_Click;
            cmdPlay.Leave += cmdPlay_Leave;
            // 
            // cmdExport
            // 
            cmdExport.BorderColour = Color.Empty;
            cmdExport.CustomColour = false;
            cmdExport.FlatBottom = false;
            cmdExport.FlatTop = false;
            cmdExport.Location = new Point(289, 31);
            cmdExport.Name = "cmdExport";
            cmdExport.Padding = new Padding(5);
            cmdExport.Size = new Size(280, 280);
            cmdExport.TabIndex = 1;
            cmdExport.Text = "Export ({0}Hz)";
            cmdExport.Click += cmdExport_Click;
            // 
            // trkSampleRate
            // 
            trkSampleRate.BackgroundColor = Color.FromArgb(90, 90, 90);
            trkSampleRate.DisabledBackColor = Color.FromArgb(80, 80, 80);
            trkSampleRate.DisabledBorderColor = Color.Empty;
            trkSampleRate.DisabledHandlerColor = Color.FromArgb(90, 90, 90);
            trkSampleRate.DisabledValueColor = Color.FromArgb(109, 109, 109);
            trkSampleRate.HandlerColor = Color.FromArgb(143, 143, 143);
            trkSampleRate.IsDerivedStyle = true;
            trkSampleRate.Location = new Point(291, 3);
            trkSampleRate.Maximum = 4096;
            trkSampleRate.Minimum = 1;
            trkSampleRate.Name = "trkSampleRate";
            trkSampleRate.Size = new Size(272, 16);
            trkSampleRate.Style = MetroSet_UI.Enums.Style.Dark;
            trkSampleRate.StyleManager = null;
            trkSampleRate.TabIndex = 2;
            trkSampleRate.Text = "metroSetTrackBar1";
            trkSampleRate.ThemeAuthor = "Narwin";
            trkSampleRate.ThemeName = "MetroDark";
            trkSampleRate.TickFrequency = 16;
            trkSampleRate.Value = 0;
            trkSampleRate.ValueColor = Color.FromArgb(65, 177, 225);
            trkSampleRate.ValueChanged += trkSampleRate_ValueChanged;
            // 
            // lblSampleRate
            // 
            lblSampleRate.AutoSize = true;
            lblSampleRate.BackColor = Color.Transparent;
            lblSampleRate.Location = new Point(158, 4);
            lblSampleRate.Name = "lblSampleRate";
            lblSampleRate.Size = new Size(127, 15);
            lblSampleRate.TabIndex = 3;
            lblSampleRate.Text = "Sample Rate: {0:0.000}\"";
            // 
            // chkLoop
            // 
            chkLoop.AutoSize = true;
            chkLoop.BackColor = Color.Transparent;
            chkLoop.Checked = true;
            chkLoop.CheckState = CheckState.Checked;
            chkLoop.Location = new Point(3, 3);
            chkLoop.Name = "chkLoop";
            chkLoop.Size = new Size(53, 19);
            chkLoop.TabIndex = 4;
            chkLoop.Text = "Loop";
            chkLoop.UseVisualStyleBackColor = false;
            // 
            // numSampleRate
            // 
            numSampleRate.Hexadecimal = true;
            numSampleRate.Location = new Point(291, 36);
            numSampleRate.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            numSampleRate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSampleRate.Name = "numSampleRate";
            numSampleRate.Size = new Size(272, 23);
            numSampleRate.TabIndex = 5;
            numSampleRate.Value = new decimal(new int[] { 1024, 0, 0, 0 });
            numSampleRate.ValueChanged += numSampleRate_ValueChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(chkLoop);
            panel1.Controls.Add(numSampleRate);
            panel1.Controls.Add(trkSampleRate);
            panel1.Controls.Add(lblSampleRate);
            panel1.Location = new Point(3, 317);
            panel1.Name = "panel1";
            panel1.Size = new Size(566, 87);
            panel1.TabIndex = 6;
            panel1.TabStop = true;
            // 
            // panel2
            // 
            panel2.Location = new Point(6, 22);
            panel2.Name = "panel2";
            panel2.Size = new Size(554, 121);
            panel2.TabIndex = 7;
            panel2.TabStop = true;
            panel2.KeyDown += panel2_KeyDown;
            panel2.Paint += panel2_Paint;
            panel2.Leave += panel2_Leave;
            panel2.PreviewKeyDown += panel2_PreviewKeyDown;
            // 
            // numSelStart
            // 
            numSelStart.Location = new Point(6, 149);
            numSelStart.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numSelStart.Name = "numSelStart";
            numSelStart.Size = new Size(74, 23);
            numSelStart.TabIndex = 0;
            numSelStart.ValueChanged += numSelStart_ValueChanged;
            // 
            // numSelSize
            // 
            numSelSize.Location = new Point(6, 182);
            numSelSize.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            numSelSize.Name = "numSelSize";
            numSelSize.Size = new Size(74, 23);
            numSelSize.TabIndex = 8;
            numSelSize.Visible = false;
            // 
            // cmdSetLoop
            // 
            cmdSetLoop.BorderColour = Color.Empty;
            cmdSetLoop.CustomColour = false;
            cmdSetLoop.FlatBottom = false;
            cmdSetLoop.FlatTop = false;
            cmdSetLoop.Location = new Point(6, 22);
            cmdSetLoop.Name = "cmdSetLoop";
            cmdSetLoop.Padding = new Padding(5);
            cmdSetLoop.Size = new Size(52, 34);
            cmdSetLoop.TabIndex = 9;
            cmdSetLoop.Text = "Set";
            cmdSetLoop.Click += cmdSetLoop_Click;
            // 
            // cmdClearLoop
            // 
            cmdClearLoop.BorderColour = Color.Empty;
            cmdClearLoop.CustomColour = false;
            cmdClearLoop.FlatBottom = false;
            cmdClearLoop.FlatTop = false;
            cmdClearLoop.Location = new Point(64, 22);
            cmdClearLoop.Name = "cmdClearLoop";
            cmdClearLoop.Padding = new Padding(5);
            cmdClearLoop.Size = new Size(52, 34);
            cmdClearLoop.TabIndex = 9;
            cmdClearLoop.Text = "Clear";
            cmdClearLoop.Click += cmdClearLoop_Click;
            // 
            // fraWaveform
            // 
            fraWaveform.BackColor = Color.Transparent;
            fraWaveform.Controls.Add(darkGroupBox1);
            fraWaveform.Controls.Add(panel2);
            fraWaveform.Controls.Add(numSelStart);
            fraWaveform.Controls.Add(numSelSize);
            fraWaveform.Location = new Point(3, 410);
            fraWaveform.Name = "fraWaveform";
            fraWaveform.Size = new Size(566, 232);
            fraWaveform.TabIndex = 10;
            fraWaveform.TabStop = false;
            fraWaveform.Text = "Waveform";
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.Controls.Add(cmdSetLoop);
            darkGroupBox1.Controls.Add(cmdClearLoop);
            darkGroupBox1.Location = new Point(86, 149);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(122, 64);
            darkGroupBox1.TabIndex = 10;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Loops";
            // 
            // SoundBox
            // 
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(fraWaveform);
            Controls.Add(panel1);
            Controls.Add(cmdExport);
            Controls.Add(cmdPlay);
            Controls.Add(tsToolbar);
            Name = "SoundBox";
            Size = new Size(771, 744);
            tsToolbar.ResumeLayout(false);
            tsToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSampleRate).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSelStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSelSize).EndInit();
            fraWaveform.ResumeLayout(false);
            darkGroupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private ToolStrip tsToolbar;
        private ToolStripButton tbbImport;
        private ToolStripButton tbbExport;
        private DarkButton cmdPlay;
        private DarkButton cmdExport;
        private MetroSetTrackBar trkSampleRate;
        private Label lblSampleRate;
        private CheckBox chkLoop;
        private DarkNumericUpDown numSampleRate;
        private WaveformPanel panel1;
        private WaveformPanel panel2;
        private DarkNumericUpDown numSelStart;
        private DarkNumericUpDown numSelSize;
        private DarkButton cmdSetLoop;
        private DarkButton cmdClearLoop;
        private DarkGroupBox fraWaveform;
        private DarkGroupBox darkGroupBox1;
    }

    public class WaveformPanel : Panel
    {
        public bool isDragging = false;
        public int dragStartX = 0;
        public int dragEndX = 0;

        public WaveformPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();
            TabStop = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            Capture = true;
            isDragging = true;
            dragStartX = Math.Clamp(e.X, 0, this.Width - 1);
            dragEndX = dragStartX;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDragging)
            {
                dragEndX = Math.Clamp(e.X, 0, this.Width - 1);

                // Range selection is disabled for now
                dragStartX = dragEndX;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Capture = false;
            isDragging = false;
            dragEndX = Math.Clamp(e.X, 0, this.Width - 1);
            Invalidate();
        }
    }
}