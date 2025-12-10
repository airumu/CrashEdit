using AltUI.Forms;
using CrashEdit.Crash;
using MeltySynth;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;

namespace CrashEdit.CE
{
    public partial class MusicBox : UserControl
    {
        private MusicEntryController controller;
        public MusicEntry musicentry;
        public VAB vab;
        private SEQ seq;

        private Timer timer;
        private int timerInterval;

        private readonly double sliderSteps = 4096;
        private double stepIncrement;
        private bool isUserDragging;
        private int seqTempo;

        private string midiPath;
        private string sf2Path;
        private string dlsPath;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private MidiSampleProvider? player;
        private WaveOut? waveOut;
        private MidiFile? midiFile;
        private TimeSpan midiLength;

        private VABTool? frmVABTool;

        public MusicBox(MusicEntryController controller)
        {
            this.controller = controller;
            musicentry = controller.MusicEntry;
            vab = controller.FindLinkedVAB();
            InitializeComponent();
            MainInit();
        }

        private void UpdatelstMusic()
        {
            lstMusic.Columns.Add("Item");
            lstMusic.Columns.Add("EID");
            var items = new (string Text, int EID)[]
            {
                ("VH", musicentry.VHEID),
                ("VB [0]", musicentry.VB0EID),
                ("VB [1]", musicentry.VB1EID),
                ("VB [2]", musicentry.VB2EID),
                ("VB [3]", musicentry.VB3EID),
                ("VB [4]", musicentry.VB4EID),
                ("VB [5]", musicentry.VB5EID),
                ("VB [6]", musicentry.VB6EID),
            };
            foreach (var (text, eid) in items)
            {
                var newItem = new ListViewItem(text);
                newItem.SubItems.Add(Entry.EIDToEName(eid));
                lstMusic.Items.Add(newItem);
            }
            foreach (ColumnHeader column in lstMusic.Columns)
            {
                column.Width = 60;
            }
        }

        private void MainInit()
        {
            string basePath = Path.Combine("tmp", "tmp");
            Directory.CreateDirectory(Path.GetDirectoryName(basePath) ?? "");
            midiPath = Path.ChangeExtension(basePath, ".mid");
            sf2Path = Path.ChangeExtension(basePath, ".sf2");
            dlsPath = Path.ChangeExtension(basePath, ".dls");

            lbEIDError.Text = "";
            txtMusic.Enabled =
            fraControls.Enabled =
            lbTimeInfo.Enabled =
            trkSeekBar.Enabled = false;

            timerInterval = 1000;
            timer = new Timer()
            {
                Interval = timerInterval
            };
            timer.Tick += (sender, e) =>
            {
                if (!isUserDragging)
                {
                    int oldValue = trkSeekBar.Value;
                    trkSeekBar.Value = (int)Math.Round(player.sequencer.MessageIndex / stepIncrement);
                    timer.Interval = timerInterval;

                    // loop
                    if (trkSeekBar.Value < oldValue)
                    {
                        SeekAndSyncTimer();
                    }
                }
            };

            UpdatelstMusic();
            ResetTimeInfo(false, true);

            // Check if the music entry has a VH.
            if (musicentry.VH == null)
            {
                cmdEditor.Visible = false;
            }

            // Check if the music entry has any SEQ.
            if (musicentry.Tracks.Count > 0)
            {
                numSEQ.Maximum = musicentry.Tracks.Count - 1;
            }
            else
            {
                fraPlayer.Enabled = false;
            }

            numSynthVolume.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numSeqSpeed.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
        }

        #region Music events

        private void UpdateEID()
        {
            if (lbEIDError.Text != string.Empty) return;

            string text = txtMusic.Text;
            lstMusic.SelectedItems[0].SubItems[1].Text = text;
            int idx = lstMusic.SelectedIndices[0];
            switch (idx)
            {
                case 0: musicentry.VHEID = Entry.ENameToEID(text); break;
                case 1: musicentry.VB0EID = Entry.ENameToEID(text); break;
                case 2: musicentry.VB1EID = Entry.ENameToEID(text); break;
                case 3: musicentry.VB2EID = Entry.ENameToEID(text); break;
                case 4: musicentry.VB3EID = Entry.ENameToEID(text); break;
                case 5: musicentry.VB4EID = Entry.ENameToEID(text); break;
                case 6: musicentry.VB5EID = Entry.ENameToEID(text); break;
                case 7: musicentry.VB6EID = Entry.ENameToEID(text); break;
            }
        }

        private void lstMusic_Click(object? sender, EventArgs e)
        {
            txtMusic.Enabled = true;
            txtMusic.Text = lstMusic.SelectedItems[0].SubItems[1].Text;
        }

        private void txtMusic_TextChanged(object? sender, EventArgs e)
        {
            lbEIDError.Text = Entry.CheckEIDErrors(txtMusic.Text, true);
        }

        private void txtMusic_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                UpdateEID();
        }

        private void txtMusic_LostFocus(object? sender, EventArgs e)
        {
            UpdateEID();
        }

        #endregion

        #region Player events

        private void numSEQ_ValueChanged(object sender, EventArgs e)
        {
            StopPlayer(true);
        }

        private void trkSeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            isUserDragging = true;
        }

        private void trkSeekBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMessageIndex();
            SeekAndSyncTimer();
            isUserDragging = false;
        }

        private void trkSeekBar_MouseWheel(object sender, MouseEventArgs e)
        {
            int step = 1;
            if (e.Delta > 0)
            {
                trkSeekBar.Value = Math.Min(trkSeekBar.Value + step, trkSeekBar.Maximum - step);
            }
            else if (e.Delta < 0)
            {
                trkSeekBar.Value = Math.Max(trkSeekBar.Value - step, trkSeekBar.Minimum);
            }
            UpdateMessageIndex();
            SeekAndSyncTimer();
        }

        private void trkSeekBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimeInfo();
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            StopPlayer(false);
            LoadSF2(sf2Path);
            //LoadDLS(dlsPath);
            Console.WriteLine("VAB loaded successfully.");
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            if (musicentry.Tracks.Count == 0) return;
            StopPlayer(false);

            if (!File.Exists(sf2Path))
            {
                DarkMessageBox.ShowError("Failed to load the soundfont file.", "MusicBox");
                return;
            }

            Console.WriteLine($"# Now playing: {musicentry.EName}, Tracks[{(int)numSEQ.Value}]");

            // Convert the SEQ to a MIDI.
            seq = musicentry.Tracks[(int)numSEQ.Value];
            byte[] midiData = seq.ToMIDI();
            File.WriteAllBytes(midiPath, midiData);

            // Load the SF2 file and create the player.
            player = new MidiSampleProvider(sf2Path);

            // Create WaveOut.
            waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            waveOut.Init(player);
            waveOut.Volume = 1.0f;
            waveOut.Play();

            // Load and play the MIDI file.
            midiFile = new MidiFile(midiPath, MidiFileLoopType.PSXSEQ);
            player.Play(midiFile, true);

            // Wait for the sequencer to load.
            player.sequencer.ProcessAllEvents();
            while (player.sequencer.Position.Ticks == 0) { }

            player.synthesizer.MasterVolume = (float)(numSynthVolume.Value / 2);
            player.sequencer.Speed = (float)numSeqSpeed.Value;
            midiLength = midiFile.Length;

            lbTimeInfo.Enabled =
            trkSeekBar.Enabled =
            fraControls.Enabled = true;
            seqTempo = seq.FakeTempo != 0 ? seq.FakeTempo : seq.Tempo;
            int bpm = (int)Math.Round(60000000.0 / seqTempo * (double)numSeqSpeed.Value);
            lbSeqSpeed.Text = $"Speed ({bpm} BPM)";
            trkSeekBar.Maximum = (int)sliderSteps;
            stepIncrement = midiFile.Messages.Length / sliderSteps;
            Console.WriteLine($"  MIDI messages: {midiFile.Messages.Length}, stepIncrement: {stepIncrement}");

            timer.Start();
            ResetTimeInfo(true, false);
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            StopPlayer(false);
        }

        private void numSynthVolumee_ValueChanged(object sender, EventArgs e)
        {
            // The default MasterVolume is 0.5F.
            player.synthesizer.MasterVolume = (float)(numSynthVolume.Value / 2);
        }

        private void numSeqSpeed_ValueChanged(object sender, EventArgs e)
        {
            decimal value = numSeqSpeed.Value;
            player.sequencer.Speed = (float)value;
            timerInterval = (int)Math.Round(1000 / value);
            timer.Interval = timerInterval;
            int bpm = (int)Math.Round(60000000.0 / seqTempo * (double)numSeqSpeed.Value);
            lbSeqSpeed.Text = $"Speed ({bpm} BPM)";
        }

        private void musicBox_Leave(object sender, EventArgs e)
        {
            StopPlayer(false);
        }

        #endregion

        private void UpdateMessageIndex()
        {
            player.sequencer.MessageIndex = Math.Min((int)Math.Round(trkSeekBar.Value * stepIncrement), (int)Math.Round(trkSeekBar.Maximum * stepIncrement) - 1);
        }

        private void SeekAndSyncTimer()
        {
            timer.Stop();
            UpdateTimeInfo();

            // Calculate the delay.
            int currentMs = player.sequencer.Position.Milliseconds;
            double speedRatio = (double)numSeqSpeed.Value;
            int delay = (int)Math.Round((1000 - currentMs) / speedRatio);
            if (delay < 1)
                delay = 1;

            timer.Interval = delay;
            timer.Start();
        }

        private void UpdateTimeInfo()
        {
            if (trkSeekBar.Enabled)
            {
                // Adjust seconds by rounding milliseconds.
                TimeSpan original = player.sequencer.Position;
                double roundedSeconds = Math.Round(original.TotalSeconds, MidpointRounding.AwayFromZero);
                TimeSpan rounded = TimeSpan.FromSeconds(roundedSeconds);
                lbTimeInfo.Text = $"{rounded.Minutes:D2}:{rounded.Seconds:D2} / {midiLength.Minutes:D2}:{midiLength.Seconds:D2}";
            }
        }

        private void ResetTimeInfo(bool enableLabel, bool resetMidi)
        {
            lbTimeInfo.Enabled = enableLabel;
            if (resetMidi)
            {
                lbTimeInfo.Text = "00:00 / 00:00";
            }
            else
            {
                lbTimeInfo.Text = $"00:00 / {midiLength.Minutes:D2}:{midiLength.Seconds:D2}";
            }
        }

        private void StopPlayer(bool resetMidi)
        {
            if (player != null)
            {
                player.Stop();
                player = null;
                waveOut?.Stop();
                waveOut?.Dispose();
                waveOut = null;

                fraControls.Enabled = false;
                trkSeekBar.Enabled = false;
                trkSeekBar.Value = 0;

                timer.Stop();
                ResetTimeInfo(false, resetMidi);
            }
        }

        private void LoadSF2(string sf2Path)
        {
            byte[] sf2 = SF2Conv.ToSF2(vab);
            File.WriteAllBytes(sf2Path, sf2);
        }

        private void LoadDLS(string dlsPath)
        {
            byte[] dls = vab.ToDLS().Save();
            File.WriteAllBytes(dlsPath, dls);
        }

        private void ScrollHandlerFunction(object? sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null)
                    handledArgs.Handled = true;

                decimal newValue = numericUpDown.Value;
                if (e.Delta > 0 && newValue < numericUpDown.Maximum)
                    newValue += numericUpDown.Increment;

                else if (e.Delta < 0 && newValue > numericUpDown.Minimum)
                    newValue -= numericUpDown.Increment;

                numericUpDown.Value = newValue;
            }
        }

        private double ConvertPanByte(short pan)
        {
            if (pan == 64) return 0;
            double normalized = (pan / 127.0) - 0.5;
            double result = normalized * 100;
            return Math.Round(result, 1);
        }

        private void cmdEditor_Click(object sender, EventArgs e)
        {
            if (frmVABTool == null || frmVABTool.IsDisposed)
                frmVABTool = new VABTool(this);

            if (!frmVABTool.Visible)
                frmVABTool.Show();
            else
                frmVABTool.Activate();
        }

        public void UpdateVAB(byte[] data, bool showdialog)
        {
            try
            {
                byte[] vab_data = data;
                vab = VAB.Load(vab_data);

                VH vh = VH.Load(vab_data);

                int vb_offset = 2592 + 32 * 16 * vh.Programs.Count;
                if ((vab_data.Length - vb_offset) % 16 != 0)
                {
                    ErrorManager.SignalIgnorableError("extra feature: VB size is invalid");
                }
                vh.VBSize = (vab_data.Length - vb_offset) / 16;
                var vb = new List<SampleLine>();
                byte[] line_data = new byte[16];
                for (int i = 0; i < vh.VBSize; i++)
                {
                    Array.Copy(vab_data, vb_offset + i * 16, line_data, 0, 16);
                    vb.Add(SampleLine.Load(line_data));
                }

                musicentry.VH = vh;
                ReplaceLinkedVB(vb);
            }
            catch (LoadAbortedException)
            {
            }
            if (showdialog)
                DarkMessageBox.ShowInformation("VAB updated successfully.", "MusicBox");
        }

        private void ReplaceLinkedVB(List<SampleLine> samples)
        {
            var vbEntries = new List<WavebankEntry>();
            foreach (WavebankEntry wavebank in controller.GetEntries<WavebankEntry>())
            {
                if (wavebank.EID == musicentry.VB0EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB1EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB2EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB3EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB4EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB5EID)
                    vbEntries.Add(wavebank);
                if (wavebank.EID == musicentry.VB6EID)
                    vbEntries.Add(wavebank);
            }

            foreach (var vbEntry in vbEntries)
            {
                vbEntry.Samples.SampleLines.Clear();
                if (samples.Count > 0)
                {
                    if (samples.Count <= WavebankEntry.MaxSampleLines)
                    {
                        vbEntry.Samples.SampleLines.AddRange(samples);
                        samples.Clear();
                    }
                    else
                    {
                        vbEntry.Samples.SampleLines.AddRange(samples.GetRange(0, WavebankEntry.MaxSampleLines));
                        samples.RemoveRange(0, WavebankEntry.MaxSampleLines);
                    }
                }
            }

            if (samples.Count > 0)
            {
                throw new GUIException("VB too large for the number of linked wavebank entries.\n\nThe imported data has been truncated.");
            }
        }

        private void KillForm()
        {
            if (frmVABTool != null)
                frmVABTool.Dispose();
        }

        //private Timer timer2;

        //private void StartWaveformTimer()
        //{
        //    timer2 = new Timer { Interval = 50 };
        //    timer2.Tick += (s, e) =>
        //    {
        //        if (player != null)
        //        {
        //            float[] buffer = new float[44100];
        //            int samplesRead = player.Read(buffer, 0, buffer.Length);

        //            DrawWaveform(buffer);
        //        }
        //    };
        //    timer2.Start();
        //}

        //private void DrawWaveform(float[] buffer)
        //{
        //    using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
        //    using (Graphics g = Graphics.FromImage(bmp))
        //    {
        //        g.Clear(Color.Black);
        //        Pen pen = new Pen(Color.Green, 1);

        //        int mid = pictureBox1.Height / 2;
        //        for (int i = 0; i < buffer.Length; i++)
        //        {
        //            int x = i * pictureBox1.Width / buffer.Length;
        //            int y = mid + (int)(buffer[i] * mid);
        //            g.DrawLine(pen, x, mid, x, y);
        //        }

        //        pictureBox1.Image?.Dispose();
        //        pictureBox1.Image = (Bitmap)bmp.Clone();
        //    }
        //}

    }

    public class MidiSampleProvider : ISampleProvider
    {
        private static WaveFormat format = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

        public Synthesizer synthesizer;
        public MidiFileSequencer sequencer;

        private object mutex;

        public MidiSampleProvider(string soundFontPath)
        {
            synthesizer = new Synthesizer(soundFontPath, format.SampleRate);
            sequencer = new MidiFileSequencer(synthesizer);

            mutex = new object();
        }

        public void Play(MidiFile midiFile, bool loop)
        {
            lock (mutex)
            {
                sequencer.Play(midiFile, loop);
            }
        }

        public void Stop()
        {
            lock (mutex)
            {
                sequencer.Stop();
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            lock (mutex)
            {
                sequencer.RenderInterleaved(buffer.AsSpan(offset, count));

                //Console.WriteLine($"Read {count} samples");
                //if (buffer[0] == 0 && buffer[1] == 0)
                //  Console.WriteLine("Warning: Buffer contains only silence!");
            }

            return count;
        }

        public WaveFormat WaveFormat => format;
    }

}
