using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using System.Media;

namespace CrashEdit.CE
{
    public sealed class SoundBox : UserControl
    {
        private SampleSet samples;

        private SoundPlayer spPlayer;

        private SoundEntry soundentry;
        private SpeechEntry speechentry;

        private bool isSpeech;

        private ToolStrip tsToolbar;
        private ToolStripButton tbbImport;
        private ToolStripButton tbbExport;
        private TableLayoutPanel pnOptions;
        private DarkButton cmdPlay;
        private DarkButton cmdExport;
        private MetroSetTrackBar trkSampleRate;
        private Label lblSampleRate;
        private DarkNumericUpDown numSampleRate;

        private void UpdateSampleRate()
        {
            int smpe = (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
            double smpe2 = trkSampleRate.Value / 256.0;
            cmdPlay.Text = string.Format("Play ({0}Hz)", smpe);
            cmdExport.Text = string.Format("Export ({0}Hz)", smpe);
            lblSampleRate.Text = string.Format("Sample Rate: {0:0.000}", smpe2);
        }

        public SoundBox(SampleSet samples, string title)
        {
            this.samples = samples;
            isSpeech = title.Contains("Speech");
            DoubleBuffered = true;

            spPlayer = new SoundPlayer();

            tbbImport = new ToolStripButton();
            tbbImport.Text = "Import";
            tbbImport.Click += new EventHandler(tbbImport_Click);

            tbbExport = new ToolStripButton();
            tbbExport.Text = "Export";
            tbbExport.Click += new EventHandler(tbbExport_Click);

            tsToolbar = new ToolStrip();
            tsToolbar.Dock = DockStyle.Top;
            tsToolbar.Items.Add(tbbImport);
            tsToolbar.Items.Add(tbbExport);

            trkSampleRate = new MetroSetTrackBar
            {
                Minimum = 0,
                Maximum = 16 * 256,
                TickFrequency = 16,
                Value = isSpeech ? 2048 : 1024,
                Dock = DockStyle.Fill,
                Style = MetroSet_UI.Enums.Style.Dark
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
            pnOptions.BackColor = Color.FromArgb(31, 31, 32);
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

        public SoundBox(SoundEntry entry) : this(entry.Samples, entry.Title)
        {
            soundentry = entry;
        }

        public SoundBox(SpeechEntry entry) : this(entry.Samples, entry.Title)
        {
            speechentry = entry;
        }

        void tbbImport_Click(object sender, EventArgs e)
        {
            // Todo refresh sound chunk
            byte[] data = FileUtil.OpenFile(FileFilters.VAG + "|" + FileFilters.Any);
            if (data == null) return;
            if (data.Length < 48)
            {
                DarkMessageBox.ShowError("Invalid VAG length.", "Import VAG");
                return;
            }

            // Check if the first 16 bytes are all 0.
            if (!data.Take(16).All(b => b == 0))
            {
                // If they are not all 0, treat the first 48 bytes as a header and remove them.
                data = data.Skip(48).ToArray();
            }
            samples = SampleSet.Load(data);
            if (isSpeech)
                speechentry.Samples = samples;
            else
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
