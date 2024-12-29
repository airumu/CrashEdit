using AltUI.Controls;
using CrashEdit.Crash;
using System.Media;

namespace CrashEdit.CE
{
    public sealed class SoundBox : UserControl
    {
        private SampleSet samples;

        private SoundPlayer spPlayer;

        private SoundEntry soundentry;

        private ToolStrip tsToolbar;
        private ToolStripButton tbbImport;
        private ToolStripButton tbbExport;
        private TableLayoutPanel pnOptions;
        private DarkButton cmdPlay;
        private DarkButton cmdExport;
        private TrackBar trkSampleRate;
        private Label lblSampleRate;
        private DarkNumericUpDown numSampleRate;

        private void UpdateSampleRate()
        {
            int smpe = (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
            cmdPlay.Text = string.Format("Play ({0}Hz)", smpe);
            cmdExport.Text = string.Format("Export ({0}Hz)", smpe);
            lblSampleRate.Text = string.Format("Sample Rate: {0:0.000}", trkSampleRate.Value / 256.0);
        }

        public SoundBox(SampleSet samples)
        {
            this.samples = samples;

            spPlayer = new SoundPlayer();

            tbbImport = new ToolStripButton();
            tbbImport.Text = "Import";
            /*            tbbImport.ForeColor = SystemColors.ControlText;
                        tbbImport.BackColor = SystemColors.Window;*/
            tbbImport.Click += new EventHandler(tbbImport_Click);

            tbbExport = new ToolStripButton();
            tbbExport.Text = "Export";
/*            tbbExport.ForeColor = SystemColors.ControlText;
            tbbExport.BackColor = SystemColors.Window;*/
            tbbExport.Click += new EventHandler(tbbExport_Click);

            tsToolbar = new ToolStrip();
            tsToolbar.Dock = DockStyle.Top;
            tsToolbar.Items.Add(tbbImport);
            tsToolbar.Items.Add(tbbExport);

            trkSampleRate = new TrackBar()
            {
                Minimum = 0,
                Maximum = 16 * 256,
                TickFrequency = 128,
                Value = 1024,
                Dock = DockStyle.Fill
            };
            trkSampleRate.ValueChanged += (object sender, EventArgs e) =>
            {
                numSampleRate.Value = (int)trkSampleRate.Value;
                UpdateSampleRate();
            };

            numSampleRate = new DarkNumericUpDown()
            {
                Minimum = 0,
                Maximum = 16 * 256,
                Value = 1024,
                Hexadecimal = true,
                Dock = DockStyle.Fill
            };
            numSampleRate.ValueChanged += (object sender, EventArgs e) =>
            {
                trkSampleRate.Value = (int)numSampleRate.Value;
                UpdateSampleRate();
            };

            int smp = (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
            cmdPlay = new DarkButton()
            {
                Dock = DockStyle.Fill,
                //Style = MetroSet_UI.Enums.Style.Dark,
                Text = string.Format("Play ({0}Hz)", smp)
            };
            /*            cmdPlay.ForeColor = SystemColors.ControlText;
                        cmdPlay.BackColor = SystemColors.Window;*/
            cmdPlay.Click += new EventHandler(cmdPlay_Click);

            cmdExport = new DarkButton()
            {
                Dock = DockStyle.Fill,
                //Style = MetroSet_UI.Enums.Style.Dark,
                Text = string.Format("Export ({0}Hz)", smp)
            };
            /*            cmdExport.ForeColor = SystemColors.ControlText;
                        cmdExport.BackColor = SystemColors.Window;*/
            cmdExport.Click += new EventHandler(cmdExport_Click);

            lblSampleRate = new Label()
            {
                ForeColor = SystemColors.ControlText,
                BackColor = Color.Transparent,
                Text = string.Format("Sample Rate: {0:0.000}", trkSampleRate.Value / 256.0),
                TextAlign = ContentAlignment.TopRight,
                Dock = DockStyle.Fill
            };

            pnOptions = new TableLayoutPanel();
            pnOptions.Dock = DockStyle.Fill;
            pnOptions.ColumnCount = 2;
            pnOptions.RowCount = 4;
            pnOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            pnOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            pnOptions.Controls.Add(cmdPlay, 0, 0);
            pnOptions.Controls.Add(cmdExport, 1, 0);
            pnOptions.Controls.Add(trkSampleRate, 1, 1);
            pnOptions.Controls.Add(lblSampleRate, 0, 1);
            pnOptions.Controls.Add(numSampleRate, 1, 2);

            Controls.Add(pnOptions);
            Controls.Add(tsToolbar);
        }

        void cmdPlay_Click(object sender, EventArgs e)
        {
            Play((int)(trkSampleRate.Value / 256.0 * (11025 / 4.0)));
        }

        void cmdExport_Click(object sender, EventArgs e)
        {
            ExportWave((int)(trkSampleRate.Value / 256.0 * (11025 / 4.0)));
        }

        public SoundBox(SoundEntry entry)
            : this(entry.Samples)
        {
            soundentry = entry;
        }

        public SoundBox(SpeechEntry entry) : this(entry.Samples)
        {
        }

        void tbbImport_Click(object sender, EventArgs e)
        {
            // Todo refresh sound chunk
            byte[] data = FileUtil.OpenFile(FileFilters.VAG + "|" + FileFilters.Any);
            if (data == null) return;

            bool hasHeader = false;
            for (int i = 12; i <= 15; i++)
            {
                if (data[i] != 0)
                    hasHeader = true;
            }
            if (hasHeader)
            {
                byte[] result = new byte[data.Length - 48];
                Array.Copy(data, 48, result, 0, data.Length - 48);
                data = result;
            }
            samples = SampleSet.Load(data);
            soundentry.Samples = samples;
        }

        void tbbExport_Click(object sender, EventArgs e)
        {
            FileUtil.SaveFile(samples.Save(), FileFilters.Any);
        }

        private void Play(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(), samplerate).Save();
            spPlayer.Stop();
            spPlayer.Stream = new MemoryStream(wave);
            spPlayer.Play();
        }

        private void ExportWave(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(), samplerate).Save();
            FileUtil.SaveFile(wave, FileFilters.Wave, FileFilters.Any);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                spPlayer.Stop();
                spPlayer.Dispose();
            }
        }
    }
}
