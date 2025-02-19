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
        private MusicEntry musicentry;
        private VAB vab;
        private SEQ seq;

        private Timer timer;
        private int timerInterval;

        private readonly double sliderSteps = 4096;
        private double stepIncrement;
        private bool isUserDragging;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private MidiSampleProvider? player;
        private WaveOut? waveOut;
        private MidiFile? midiFile;
        private TimeSpan midiLength;

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
            string basePath = "temp";
            string midiPath = Path.ChangeExtension(basePath, ".mid");
            string sf2Path = Path.ChangeExtension(basePath, ".sf2");
            string dlsPath = Path.ChangeExtension(basePath, ".dls");

            lbEIDError.Visible = false;
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
                    trkSeekBar.Value = (int)Math.Round(player.sequencer.MessageIndex / stepIncrement);
                    timer.Interval = timerInterval;
                }
            };

            UpdatelstMusic();

            // Check if the music entry has a VH.
            if (musicentry.VH != null)
            {
                numMasterVolume.Value = musicentry.VH.Volume;
                numMasterVolume.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Volume = (byte)numMasterVolume.Value;
                };

                numMasterPan.Value = musicentry.VH.Panning;
                numMasterPan.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Panning = (byte)numMasterPan.Value;
                    lbMasterPan.Text = $"Master Pan ({ConvertPanByte(musicentry.VH.Panning):F1})";
                };
            }
            else
            {
                fraVH.Visible = false;
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

            numSEQ.ValueChanged += (sender, e) =>
            {
                StopPlayer(true);
            };

            trkSeekBar.MouseDown += (sender, e) => isUserDragging = true;
            trkSeekBar.MouseUp += (sender, e) =>
            {
                UpdateMessageIndex();
                SeekAndSyncTimer();
                isUserDragging = false;
            };
            trkSeekBar.MouseWheel += (sender, e) =>
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
            };
            trkSeekBar.ValueChanged += (s, e) =>
            {
                UpdateTimeInfo();
            };

            ResetTimeInfo(false, true);

            cmdLoad.Click += (sender, e) =>
            {
                StopPlayer(false);
                LoadSF2(sf2Path);
                //LoadDLS(dlsPath);
                Console.WriteLine("VAB loaded successfully.");
            };

            cmdPlay.Click += (sender, e) =>
            {
                if (musicentry.Tracks.Count == 0) return;
                StopPlayer(false);

                seq = musicentry.Tracks[(int)numSEQ.Value];
                byte[] midiData = seq.ToMIDI();
                File.WriteAllBytes(midiPath, midiData);

                //LoadSF2(sf2Path);
                if (!File.Exists(sf2Path))
                {
                    DarkMessageBox.ShowError("Failed to load the soundfont file.", "MusicBox");
                    return;
                }

                player = new MidiSampleProvider(sf2Path);
                waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
                waveOut.Init(player);
                waveOut.Play();

                // Load the MIDI file.
                midiFile = new MidiFile(midiPath, MidiFileLoopType.PSXSEQ);
                // Play the MIDI file.
                player.Play(midiFile, true);
                Console.WriteLine($"# Now playing: {musicentry.EName}, Tracks[{(int)numSEQ.Value}]");

                // Wait for the sequencer to load.
                player.sequencer.ProcessAllEvents();
                while (player.sequencer.Position.Ticks == 0) { }

                player.synthesizer.MasterVolume = (float)(numSynthVolumee.Value / 2);
                player.sequencer.Speed = (float)numSeqSpeed.Value;
                midiLength = midiFile.Length;

                lbTimeInfo.Enabled =
                trkSeekBar.Enabled =
                fraControls.Enabled = true;
                int tempo = seq.FakeTempo != 0 ? seq.FakeTempo : seq.Tempo;
                int bpm = (int)Math.Round(60000000.0 / tempo * (double)numSeqSpeed.Value);
                lbSeqSpeed.Text = $"Speed ({bpm} BPM)";
                trkSeekBar.Maximum = (int)sliderSteps;
                stepIncrement = midiFile.Messages.Length / sliderSteps;
                Console.WriteLine($"Midi messages: {midiFile.Messages.Length}, stepIncrement: {stepIncrement}");

                timer.Start();
                ResetTimeInfo(true, false);
            };

            cmdStop.Click += (sender, e) =>
            {
                StopPlayer(false);
            };

            numSynthVolumee.ValueChanged += (sender, e) =>
            {
                // The default MasterVolume is 0.5F.
                player.synthesizer.MasterVolume = (float)(numSynthVolumee.Value / 2);
            };
            numSynthVolumee.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            numSeqSpeed.ValueChanged += (sender, e) =>
            {
                decimal value = numSeqSpeed.Value;
                player.sequencer.Speed = (float)value;
                timerInterval = (int)Math.Round(1000 / value);
                timer.Interval = timerInterval;
                int bpm = (int)Math.Round(60000000.0 / seq.Tempo * (double)numSeqSpeed.Value);
                lbSeqSpeed.Text = $"Speed ({bpm} BPM)";
            };
            numSeqSpeed.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            Leave += (sender, e) =>
            {
                StopPlayer(false);
            };
        }

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
                waveOut.Stop();
                waveOut.Dispose();

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
            lbEIDError.Visible = true;
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
            }

            return count;
        }

        public WaveFormat WaveFormat => format;
    }

}
