using Crash;
using System;
using System.IO;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace CrashEdit
{
    public sealed class SoundBox : UserControl
    {
        private SampleSet samples;

        private SoundPlayer spPlayer;

        private DarkToolStrip tsToolbar;
        private ToolStripButton tbbExport;
        private TableLayoutPanel pnOptions;
        private DarkButton cmdPlay;
        private DarkButton cmdExport;
        private TrackBar trkSampleRate;
        private DarkLabel lblSampleRate;

        public SoundBox(SampleSet samples)
        {
            this.samples = samples;

            spPlayer = new SoundPlayer();

            tbbExport = new ToolStripButton();
            tbbExport.Text = "Export";
            tbbExport.Click += new EventHandler(tbbExport_Click);
            tbbExport.Size = new Size(48, 23);

            tsToolbar = new DarkToolStrip();
            tsToolbar.Dock = DockStyle.Top;
            tsToolbar.Items.Add(tbbExport);
            tsToolbar.BackColor = Color.FromArgb(30, 30, 30);
            tsToolbar.ForeColor = SystemColors.Control;

            trkSampleRate = new TrackBar()
            {
                Minimum = 0,
                Maximum = 16 * 256,
                TickFrequency = 128,
                Value = 1024,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
        };
            trkSampleRate.ValueChanged += (object sender, EventArgs e) => {
                int smpe = (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
                cmdPlay.Text = string.Format("Play ({0}Hz)", smpe);
                cmdExport.Text = string.Format("Export ({0}Hz)", smpe);
                lblSampleRate.Text = string.Format("Sample Rate: {0:0.000}", trkSampleRate.Value / 256.0);
            };

            int smp = (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
            cmdPlay = new DarkButton();
            cmdPlay.Dock = DockStyle.Fill;
            cmdPlay.Text = string.Format("Play ({0}Hz)", smp);
            cmdPlay.Click += new EventHandler(cmdPlay_Click);

            cmdExport = new DarkButton();
            cmdExport.Dock = DockStyle.Fill;
            cmdExport.Text = string.Format("Export ({0}Hz)", smp);
            cmdExport.Click += new EventHandler(cmdExport_Click);

            lblSampleRate = new DarkLabel()
            {
                Text = string.Format("Sample Rate: {0:0.000}", trkSampleRate.Value / 256.0),
                TextAlign = ContentAlignment.TopRight,
                Dock = DockStyle.Fill
            };

            pnOptions = new TableLayoutPanel();
            pnOptions.Dock = DockStyle.Fill;
            pnOptions.ColumnCount = 2;
            pnOptions.RowCount = 1;
            pnOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            pnOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent,50));
            pnOptions.RowStyles.Add(new RowStyle(SizeType.Percent,50));
            pnOptions.Controls.Add(cmdPlay,0,0);
            pnOptions.Controls.Add(cmdExport,1,0);
            pnOptions.Controls.Add(trkSampleRate,1,1);
            pnOptions.Controls.Add(lblSampleRate,0,1);
            pnOptions.BackColor = Color.FromArgb(30, 30, 30);
            pnOptions.ForeColor = SystemColors.Control;
            pnOptions.Font = new Font("Microsoft Sans Serif", 9F);

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
        }

        public SoundBox(SpeechEntry entry) : this(entry.Samples)
        {
        }

        void tbbExport_Click(object sender,EventArgs e)
        {
            FileUtil.SaveFile(samples.Save(),FileFilters.Any);
        }

        private void Play(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(),samplerate).Save();
            spPlayer.Stop();
            spPlayer.Stream = new MemoryStream(wave);
            spPlayer.Play();
        }

        private void ExportWave(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(),samplerate).Save();
            FileUtil.SaveFile(wave,FileFilters.Wave,FileFilters.Any);
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
