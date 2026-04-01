using AltUI.Forms;
using CrashEdit.Crash;
using NAudio.Wave;

namespace CrashEdit.CE
{
    public partial class SoundBox : UserControl
    {
        private readonly WaveOutEvent waveOut;
        private SampleSet samples;
        private byte[] pcm;
        private readonly List<float> waveform = [];

        private readonly SoundEntry? soundentry = null;
        private readonly SpeechEntry? speechentry = null;

        private readonly bool isSpeech;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

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

            CreateWaveform();

            numSampleRate.MouseWheel += ScrollHandlerFunction;
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

        private void UpdateSamples()
        {
            if (isSpeech)
                speechentry.Samples = samples;
            else
                soundentry.Samples = samples;

            SoundInit();
            CreateWaveform();
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

            // check if the first 16 bytes are all 0
            // if not, assume it's a VAG file with a header, and we need to skip the first 48 bytes
            if (!data.Take(16).All(b => b == 0))
            {
                data = data.Skip(48).ToArray();
            }
            samples = SampleSet.Load(data);

            UpdateSamples();
        }

        private static byte[] ToVAG(string path)
        {
            using Stream inStream = File.OpenRead(path);
            using var wavReader = new WaveFileReader(inStream);

            double duration = wavReader.SampleCount / (double)wavReader.WaveFormat.SampleRate;

            {
                Console.WriteLine($"File: {path}");
                Console.WriteLine($"SampleRate: {wavReader.WaveFormat.SampleRate}");
                Console.WriteLine($"Channels: {wavReader.WaveFormat.Channels}");
                Console.WriteLine($"Duration: {duration}");
                Console.WriteLine($"Bit Depth: {wavReader.WaveFormat.BitsPerSample}");
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

        private void Stop()
        {
            waveOut.Stop();
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

        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
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

        private void ResetFlags()
        {
            for (int i = 1; i < samples.SampleLines.Count - 2; i++)
            {
                samples.SampleLines[i].Flags = SampleLineFlags.None;
            }
            samples.SampleLines[samples.SampleLines.Count - 2].Flags = SampleLineFlags.StopEnvelope;
        }

        private void ClampSelection()
        {
            numSelStart.Value = Math.Clamp(numSelStart.Value, 0, waveform.Count - (56 * 2));
        }

        private void cmdSetLoop_Click(object sender, EventArgs e)
        {
            ResetFlags();

            ClampSelection();

            int start = ((int)numSelStart.Value * 2 / 56) + 1;
            int end = samples.SampleLines.Count - 2;
            samples.SampleLines[start].Flags = SampleLineFlags.LoopStartAlt;
            samples.SampleLines[end].Flags = SampleLineFlags.LoopEnd;
            for (int i = start + 1; i < end; i++)
            {
                samples.SampleLines[i].Flags = SampleLineFlags.NoForceStopEnvelope;
            }

            UpdateSamples();

            chkLoop.Checked =
            chkLoop.Enabled = true;
            panel2.Invalidate();
        }

        private void cmdClearLoop_Click(object sender, EventArgs e)
        {
            ResetFlags();

            UpdateSamples();

            chkLoop.Checked =
            chkLoop.Enabled = false;
            panel2.Invalidate();
        }

        private void numSelStart_ValueChanged(object sender, EventArgs e)
        {
            if (waveform.Count == 0) return;
            if (Dirty || panel2.isDragging) return;

            dirty.Push(true);

            ClampSelection();

            double pixelsPerSample = (double)panel2.Width / waveform.Count;
            int x = (int)((double)numSelStart.Value * pixelsPerSample);
            panel2.dragStartX = x;
            panel2.dragEndX = x;
            panel2.Invalidate();

            dirty.Pop();
        }

        private void panel2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.IsInputKey = true;
            }
        }

        private void panel2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                cmdPlay.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.S)
            {
                Stop();
                cmdSetLoop.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                Stop();
                cmdClearLoop.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (waveform.Count == 0) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = panel2.Width;
            int height = panel2.Height;

            double samplesPerPixel = (double)waveform.Count / width;
            double pixelsPerSample = (double)width / waveform.Count;

            using Pen pen = new(Color.FromArgb(0xFF, 0x10, 0x30, 0x80));
            using Pen penAlt = new(Color.FromArgb(0xFF, 0x00, 0x80, 0x80));

            // change logic to handle both cases: when there are more samples than pixels, and when there are fewer samples than pixels
            if (waveform.Count >= width)
            {
                Point? prevTop = null;
                Point? prevBottom = null;

                for (int x = 0; x < width; x++)
                {
                    int start = (int)(x * samplesPerPixel);
                    int end = (int)((x + 1) * samplesPerPixel);
                    end = Math.Min(end, waveform.Count);

                    float min = 1f, max = -1f;

                    for (int i = start; i < end; i++)
                    {
                        float s = waveform[i];
                        if (s < min) min = s;
                        if (s > max) max = s;
                    }

                    int y1 = (int)((min + 1) * height / 2);
                    int y2 = (int)((max + 1) * height / 2);

                    int loopStartPixel = (int)(samples.LoopStart / 2 * pixelsPerSample);

                    var currentPen = (x > loopStartPixel && chkLoop.Checked) ? penAlt : pen;

                    g.DrawLine(currentPen, x, y1, x, y2);

                    if (prevTop.HasValue)
                        g.DrawLine(currentPen, prevTop.Value, new Point(x, y1));

                    if (prevBottom.HasValue)
                        g.DrawLine(currentPen, prevBottom.Value, new Point(x, y2));

                    prevTop = new Point(x, y1);
                    prevBottom = new Point(x, y2);
                }
            }
            else
            {
                for (int i = 0; i < waveform.Count - 1; i++)
                {
                    int x1 = (int)(i * pixelsPerSample);
                    int x2 = (int)((i + 1) * pixelsPerSample);

                    int y1 = (int)((waveform[i] + 1) * height / 2);
                    int y2 = (int)((waveform[i + 1] + 1) * height / 2);

                    int loopStartPixel = (int)(samples.LoopStart / 2 * pixelsPerSample);

                    var currentPen = (x1 > loopStartPixel - 40 && chkLoop.Checked) ? penAlt : pen;

                    g.DrawLine(currentPen, x1, y1, x2, y2);
                }
            }

            int dragX1 = Math.Clamp(Math.Min(panel2.dragStartX, panel2.dragEndX), 0, width);
            int dragX2 = Math.Clamp(Math.Max(panel2.dragStartX, panel2.dragEndX), 0, width);

            int startSample = (int)(dragX1 * samplesPerPixel);
            int endSample = (int)((dragX2 + 1) * samplesPerPixel);
            startSample = Math.Clamp(startSample, 0, waveform.Count);
            endSample = Math.Clamp(endSample, 0, waveform.Count);

            int drawX1 = (int)(startSample * pixelsPerSample);
            int drawX2 = (int)(endSample * pixelsPerSample);
            drawX1 = Math.Clamp(drawX1, 0, width);
            drawX2 = Math.Clamp(drawX2, 0, width);

            int left = Math.Min(drawX1, drawX2);
            int right = Math.Max(drawX1, drawX2);

            if (right == left)
                right = left + 1;

            // draw selection
            using SolidBrush brush = new(Color.FromArgb(40, Color.LightBlue));
            g.FillRectangle(brush, left, 0, right - left, height);

            // draw selection borders
            using Pen pen2 = new(Color.DeepSkyBlue, 2);
            g.DrawLine(pen2, left, 0, left, height);
            g.DrawLine(pen2, right, 0, right, height);

            if (!Dirty && panel2.isDragging)
            {
                dirty.Push(true);
                numSelStart.Value = startSample;
                numSelSize.Value = endSample - startSample;
                dirty.Pop();
            }
        }

        private void CreateWaveform()
        {
            MemoryStream ms = new(pcm);
            WaveFormat format = new(Samplerate, 16, 1); // 16bit mono
            using RawSourceWaveStream reader = new(ms, format);

            byte[] buffer = new byte[reader.WaveFormat.SampleRate * 2]; // 2 bytes per sample
            int read;

            waveform.Clear();

            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < read; i += 2)
                {
                    short sample = BitConverter.ToInt16(buffer, i);
                    float normalized = sample / 32768f;
                    waveform.Add(normalized);
                }
            }
            
            panel2.Invalidate();
        }

        private void cmdPlay_Leave(object sender, EventArgs e)
        {
            Stop();
        }

        private void panel2_Leave(object sender, EventArgs e)
        {
            Stop();
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
