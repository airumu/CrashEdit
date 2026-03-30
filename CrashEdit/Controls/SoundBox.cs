using AltUI.Forms;
using CrashEdit.Crash;
using NAudio.Wave;
using System.Runtime.InteropServices;
using System.Text;

namespace CrashEdit.CE
{
    public partial class SoundBox : UserControl
    {
        private readonly WaveOutEvent waveOut;
        private SampleSet samples;
        private byte[] pcm;

        private readonly SoundEntry? soundentry = null;
        private readonly SpeechEntry? speechentry = null;

        private readonly bool isSpeech;

        private int Samplerate
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

            waveOut = new WaveOutEvent();
            SoundInit();

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

        private void SoundInit()
        {
            LoadPcm(out SampleSet sampleset, out byte[] pcm);
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
            cmdPlay.Text = string.Format("Play ({0}Hz)", Samplerate);
            cmdExport.Text = string.Format("Export ({0}Hz)", Samplerate);
            lblSampleRate.Text = string.Format("Sample Rate: {0:0.000}", smpe2);
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            ExportWave(Samplerate);
        }

        private void tbbImport_Click(object sender, EventArgs e)
        {
            // TODO: Refresh sound chunk

            byte[]? data = null;

            using OpenFileDialog ofd = new();
            ofd.Filter = FileFilters.SupportedAudio + "|" + FileFilters.Wave + "|" + FileFilters.VAG;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    data = ToVAG(ofd.FileName);
                }
                else
                {
                    data = File.ReadAllBytes(ofd.FileName);
                }
            }

            if (data == null) return;
            if (data.Length < 48)
            {
                DarkMessageBox.ShowError("Invalid VAG length.", "Import VAG");
                return;
            }

            // Check if the first 16 bytes are all 0
            if (!data.Take(16).All(b => b == 0))
            {
                // If they are not all 0, treat the first 48 bytes as a header and remove them
                data = data.Skip(48).ToArray();
            }
            samples = SampleSet.Load(data);

            if (isSpeech)
                speechentry.Samples = samples;
            else
                soundentry.Samples = samples;

            SoundInit();
        }

        private static byte[] ToVAG(string path)
        {
            using Stream inStream = File.OpenRead(path);
            using var wavReader = new WaveFileReader(inStream);

            double duration = wavReader.SampleCount / (double)wavReader.WaveFormat.SampleRate;

            bool debug = false;
            if (debug)
            {
                Console.WriteLine($"SAMPLERATE: {wavReader.WaveFormat.SampleRate}");
                Console.WriteLine($"CHANNELS: {wavReader.WaveFormat.Channels}");
                Console.WriteLine($"DURATION: {duration}");
                Console.WriteLine($"BIT DEPTH: {wavReader.WaveFormat.BitsPerSample}");
            }

            float[] sampleData = new float[wavReader.SampleCount * wavReader.WaveFormat.Channels];
            wavReader.ToSampleProvider().Read(sampleData, 0, sampleData.Length);

            short[] sampleData16 = new short[sampleData.Length];
            for (int i = 0; i < sampleData.Length; i++)
            {
                sampleData16[i] = (short)(sampleData[i] * short.MaxValue);
            }

            using MemoryStream stream = new();
            using BinaryWriter writer = new(stream);
            using VAGConv vagConv = new(wavReader.WaveFormat.SampleRate, wavReader.WaveFormat.Channels, sampleData16, writer);
            vagConv.WriteHeader();
            vagConv.Finish();

            writer.Flush();
            //string outPath = Path.ChangeExtension(path, "vag");
            //File.WriteAllBytes(outPath, stream.ToArray());
            return stream.ToArray();
        }

        /// <summary>
        /// Generate and save a VAG audio file from the current sample data.
        /// </summary>
        private void tbbExport_Click(object sender, EventArgs e)
        {
            // create PCM data
            byte[] sampleData = samples.Save();
            // add 16 byte header
            byte[] pcm = new byte[sampleData.Length + 16]; 
            sampleData.CopyTo(pcm, 16);

            // create VAG header
            using MemoryStream stream = new();
            using BinaryWriter writer = new(stream);
            using VAGConv vagConv = new(11025, 1, [], writer);
            vagConv.WriteHeader();
            vagConv.WriteSampleLength((uint)sampleData.Length);
            writer.Flush();
            byte[] header = stream.ToArray();

            byte[] result = new byte[header.Length + pcm.Length];
            header.CopyTo(result, 0);
            pcm.CopyTo(result, header.Length);

            FileUtil.SaveFile(result, FileFilters.VAG + "|" + FileFilters.Any);
        }

        private void LoadPcm(out SampleSet sampleset, out byte[] pcmdata)
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
            waveOut.Stop();
            MemoryStream ms = new(pcm);
            WaveFormat format = new(Samplerate, 16, 1); // 16bit mono
            RawSourceWaveStream reader = new(ms, format);

            if (chkLoop.Checked)
            {
                waveOut.Init(new LoopStream(reader)
                {
                    LoopStart = samples.LoopStart,
                    LoopEnd = samples.LoopEnd
                });
            }
            else
            {
                waveOut.Init(reader);
            }
            waveOut.Play();
        }

        private void ExportWave(int samplerate)
        {
            byte[] wave = WaveConv.ToWave(samples.ToPCM(), samplerate).Save();
            FileUtil.SaveFile(wave, FileFilters.Wave, FileFilters.Any);
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

        private void cmdPlay_Leave(object sender, EventArgs e)
        {
            waveOut.Stop();
        }
    }

    public class LoopStream(WaveStream sourceStream) : WaveStream
    {
        private readonly WaveStream source = sourceStream;
        public long LoopStart { get; set; } = 0;
        public long LoopEnd { get; set; } = sourceStream.Length;

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
