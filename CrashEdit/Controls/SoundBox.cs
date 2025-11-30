using AltUI.Forms;
using CrashEdit.Crash;
using NAudio.Wave;

namespace CrashEdit.CE
{
    public partial class SoundBox : UserControl
    {
        private WaveOutEvent spPlayer;

        private SampleSet samples;
        private SampleSet sampleset;
        private byte[] pcm;

        private SoundEntry? soundentry = null;
        private SpeechEntry? speechentry = null;

        private bool isSpeech;

        private int samplerate
        {
            get
            {
                return (int)(trkSampleRate.Value / 256.0 * (11025 / 4.0));
            }
        }

        public SoundBox(SampleSet samples, string title)
        {
            this.samples = samples;
            isSpeech = title.Contains("Speech");
            DoubleBuffered = true;

            InitializeComponent();

            spPlayer = new WaveOutEvent()
            {
                DesiredLatency = 80,  // 80ms
                NumberOfBuffers = 2
            };
            soundInit();

            int defaultRate = isSpeech ? 2048 : 1024;
            numSampleRate.Value = defaultRate;
            trkSampleRate.Value = defaultRate;
            UpdateSampleRate();

            numSampleRate.MouseWheel += ScrollHandlerFunction2;
        }

        public SoundBox(SoundEntry entry) : this(entry.Samples, entry.Title)
        {
            soundentry = entry;
        }

        public SoundBox(SpeechEntry entry) : this(entry.Samples, entry.Title)
        {
            speechentry = entry;
        }

        private void soundInit()
        {
            loadPcm(out SampleSet sampleset, out byte[] pcm);
            this.sampleset = sampleset;
            this.pcm = pcm;

            if (sampleset.LoopStart < 0)
            {
                sampleset.LoopStart = 0;
                chkLoop.Checked = false;
                chkLoop.Enabled = false;
            }
        }

        private void UpdateSampleRate()
        {
            double smpe2 = trkSampleRate.Value / 256.0;
            cmdPlay.Text = string.Format("Play ({0}Hz)", samplerate);
            cmdExport.Text = string.Format("Export ({0}Hz)", samplerate);
            lblSampleRate.Text = string.Format("Sample Rate: {0:0.000}", smpe2);
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            ExportWave(samplerate);
        }

        private void tbbImport_Click(object sender, EventArgs e)
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

        private void tbbExport_Click(object sender, EventArgs e)
        {
            FileUtil.SaveFile(samples.Save(), FileFilters.Any);
        }

        private void loadPcm(out SampleSet sampleset, out byte[] pcmdata)
        {
            List<byte> pcm = [];
            double s0 = 0.0;
            double s1 = 0.0;
            samples.LoopStart = -1;
            foreach (SampleLine sampleline in samples.SampleLines)
            {
                if (sampleline.Flags == SampleLineFlags.LoopStart || sampleline.Flags == SampleLineFlags.LoopStartAlt)
                {
                    samples.LoopStart = pcm.Count;
                }

                pcm.AddRange(sampleline.ToPCM(ref s0, ref s1));

                if (sampleline.Flags == SampleLineFlags.StopEnvelope)
                {
                    samples.LoopEnd = pcm.Count;
                    break;
                }
                if (sampleline.Flags == SampleLineFlags.LoopEnd)
                {
                    samples.LoopEnd = pcm.Count;
                    break;
                }
            }

            sampleset = samples;
            pcmdata = pcm.ToArray();
        }

        private void Play()
        {
            // byte[] pcm = WaveConv.ToWave(samples.ToPCM(), samplerate).Save();
            var ms = new MemoryStream(pcm);

            WaveFormat format = new(samplerate, 16, 1);  // 16bit mono
            RawSourceWaveStream reader = new(ms, format);

            LoopStream loop = new(reader)
            {
                LoopStart = sampleset.LoopStart,
                LoopEnd = sampleset.LoopEnd
            };

            spPlayer.Stop();
            // spPlayer.Stream = new MemoryStream(wave);
            if (chkLoop.Checked)
            {
                spPlayer.Init(loop);
            }
            else
            {
                spPlayer.Init(reader);
            }
            spPlayer.Play();
        }

        private void ExportWave(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(), samplerate).Save();
            FileUtil.SaveFile(wave, FileFilters.Wave, FileFilters.Any);
        }

        private void cmdPlay_Leave(object sender, EventArgs e)
        {
            spPlayer.Stop();
        }

        private void trkSampleRate_ValueChanged(object sender, EventArgs e)
        {
            numSampleRate.Value = (int)trkSampleRate.Value;
            UpdateSampleRate();
        }

        private void numSampleRate_ValueChanged(object sender, EventArgs e)
        {
            trkSampleRate.Value = (int)numSampleRate.Value;
            UpdateSampleRate();
        }

        private void ScrollHandlerFunction2(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null) handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue + 8 < numericUpDown.Maximum)
                    newValue += 8;

                else if (e.Delta < 0 && newValue - 8 >= numericUpDown.Minimum)
                    newValue -= 8;

                numericUpDown.Value = newValue;
            }
        }
    }

    public class LoopStream : WaveStream
    {
        private readonly WaveStream source;
        public long LoopStart { get; set; }
        public long LoopEnd { get; set; }

        public LoopStream(WaveStream sourceStream)
        {
            source = sourceStream;
            LoopStart = 0;
            LoopEnd = sourceStream.Length;
        }

        public override WaveFormat WaveFormat => source.WaveFormat;
        public override long Length => source.Length;

        public override long Position
        {
            get => source.Position;
            set => source.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = source.Read(buffer, offset, count);

            if (Position >= LoopEnd)
            {
                Position = LoopStart;
            }

            if (bytesRead < count)
            {
                Position = LoopStart;
                int additionalBytes = source.Read(
                    buffer, offset + bytesRead, count - bytesRead);
                bytesRead += additionalBytes;
            }

            return bytesRead;
        }
    }
}
