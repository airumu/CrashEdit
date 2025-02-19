using System.Numerics;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
using MeltySynth;
using MetroSet_UI.Controls;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;

namespace CrashEdit.CE
{
    public sealed class MusicBox : UserControl
    {
        private MusicEntryController controller;
        private MusicEntry musicentry;
        private VAB vab;
        private SEQ seq;

        private TableLayoutPanel pnMain;
        private TableLayoutPanel pnSub1;
        private TableLayoutPanel pnSub2;
        private DoubleBufferedListView lstMusic;
        private DarkTextBox txtMusic;
        private Label lblEIDError;
        private Label? lblMasterVolume;
        private DarkNumericUpDown? numMasterVolume;
        private Label? lblMasterPan;
        private DarkNumericUpDown? numMasterPan;
        private Label txtSEQ;
        private DarkNumericUpDown numSEQ;
        private DarkButton cmdLoad;
        private DarkButton cmdPlay;
        private DarkButton cmdStop;
        private Label lbTimeInfo;
        private MetroSetTrackBar trkSeekBar;
        private Label lbSynthVolume;
        private DarkNumericUpDown numSynthVolumee;
        private Label lbSeqSpeed;
        private DarkNumericUpDown numSeqSpeed;

        private Timer timer;
        private int timerInterval;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private MidiSampleProvider? player;
        private WaveOut? waveOut;
        private MidiFile? midiFile;
        private TimeSpan midiLength;

        private bool isUserDragging;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public MusicBox(MusicEntryController controller)
        {
            this.controller = controller;
            musicentry = controller.MusicEntry;
            vab = controller.FindLinkedVAB();

            BackColor = Color.FromArgb(31, 31, 32);
            bool hasVH = musicentry.VH != null;
            bool hasSEQ = musicentry.Tracks.Count > 0;

            string basePath = "temp";
            string midiPath = Path.ChangeExtension(basePath, ".mid");
            string sf2Path = Path.ChangeExtension(basePath, ".sf2");
            string dlsPath = Path.ChangeExtension(basePath, ".dls");

            pnMain = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill
            };
            pnSub1 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 14,
                Dock = DockStyle.Fill
            };
            pnSub2 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 4,
                Dock = DockStyle.Fill
            };

            lstMusic = new DoubleBufferedListView()
            {
                BorderStyle = BorderStyle.FixedSingle,
                FullRowSelect = true,
                UseCompatibleStateImageBehavior = false,
                View = View.Details,
                Size = new Size(120, 200),
               
            };
            lstMusic.Click += lstMusic_Click;
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

            txtMusic = new DarkTextBox()
            {
                Enabled = false,
                MaxLength = 5,
                Width = 120
            };
            txtMusic.TextChanged += txtMusic_TextChanged;
            txtMusic.KeyDown += txtMusic_KeyDown;
            txtMusic.LostFocus += txtMusic_LostFocus;

            lblEIDError = new Label()
            {
                AutoSize = true,
                ForeColor = Color.Red
            };

            if (hasVH)
            {
                lblMasterVolume = new Label()
                {
                    Text = "Master Volume"
                };
                numMasterVolume = new DarkNumericUpDown()
                {
                    Minimum = 0,
                    Maximum = 255,
                    Value = musicentry.VH.Volume
                };
                numMasterVolume.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Volume = (byte)numMasterVolume.Value;
                };

                lblMasterPan = new Label()
                {
                    Text = "Master Pan"
                };
                numMasterPan = new DarkNumericUpDown()
                {
                    Minimum = 0,
                    Maximum = 127,
                    Value = musicentry.VH.Panning
                };
                numMasterPan.ValueChanged += (sender, e) =>
                {
                    musicentry.VH.Panning = (byte)numMasterPan.Value;
                };

                pnSub2.Controls.Add(lblMasterVolume, 0, 0);
                pnSub2.Controls.Add(numMasterVolume, 0, 1);
                pnSub2.Controls.Add(lblMasterPan, 0, 2);
                pnSub2.Controls.Add(numMasterPan, 0, 3);
            }

            txtSEQ = new Label()
            {
                Enabled = hasSEQ,
                Text = "Tracks"
            };

            numSEQ = new DarkNumericUpDown()
            {
                Enabled = hasSEQ,
                Minimum = 0,
                Maximum = hasSEQ ? musicentry.Tracks.Count - 1 : 0
            };
            numSEQ.ValueChanged += (sender, e) =>
            {
                StopPlayer(true);
            };

            trkSeekBar = new MetroSetTrackBar()
            {
                Enabled = false,
                Minimum = 0,
                Maximum = 0,
                TickFrequency = 64,
                Dock = DockStyle.Fill,
                Style = MetroSet_UI.Enums.Style.Dark
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

            timerInterval = 1000;
            timer = new Timer()
            {
                Interval = timerInterval
            };
            timer.Tick += (sender, e) =>
            {
                if (!isUserDragging)
                {
                    trkSeekBar.Value = player.sequencer.MessageIndex / 4;
                    timer.Interval = timerInterval;
                }
            };

            lbTimeInfo = new Label();
            ResetTimeInfo(false, true);

            cmdLoad = new DarkButton()
            {
                Text = "Load VAB"
            };
            cmdLoad.Click += (sender, e) =>
            {
                StopPlayer(false);
                LoadSF2(sf2Path);
                LoadDLS(dlsPath);
            };

            cmdPlay = new DarkButton()
            {
                Enabled = hasSEQ,
                Text = "Play"
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
                numSynthVolumee.Enabled =
                numSeqSpeed.Enabled = true;
                int bpm = (int)Math.Round(60000000.0 / seq.Tempo * (double)numSeqSpeed.Value);
                lbSeqSpeed.Text = $"Speed ({bpm} BPM)";
                trkSeekBar.Maximum = Convert.ToInt32(midiFile.Messages.Length / 4);
                timer.Start();
                ResetTimeInfo(true, false);
            };

            cmdStop = new DarkButton()
            {
                Enabled = hasSEQ,
                Text = "Stop"
            };
            cmdStop.Click += (sender, e) =>
            {
                StopPlayer(false);
            };

            lbSynthVolume = new Label()
            {
                Text = "Volume"
            };

            numSynthVolumee = new DarkNumericUpDown()
            {
                Enabled = false,
                DecimalPlaces = 1,
                Minimum = 0.0M,
                Maximum = 2.0M,
                Value = 1.0M,
                Increment = 0.1M
            };
            numSynthVolumee.ValueChanged += (sender, e) =>
            {
                // The default MasterVolume is 0.5F.
                player.synthesizer.MasterVolume = (float)(numSynthVolumee.Value / 2);
            };
            numSynthVolumee.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            lbSeqSpeed = new Label()
            {
                Text = "Speed"
            };

            numSeqSpeed = new DarkNumericUpDown()
            {
                Enabled = false,
                DecimalPlaces = 2,
                Minimum = 0.5M,
                Maximum = 2.0M,
                Value = 1.0M,
                Increment = 0.05M
            };
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

            pnSub1.Controls.Add(lstMusic, 0, 0);
            pnSub1.Controls.Add(txtMusic, 0, 1);
            pnSub1.Controls.Add(lblEIDError, 0, 2);
            pnSub1.Controls.Add(txtSEQ, 0, 3);
            pnSub1.Controls.Add(numSEQ, 0, 4);
            pnSub1.Controls.Add(cmdLoad, 0, 5);
            pnSub1.Controls.Add(cmdPlay, 0, 6);
            pnSub1.Controls.Add(cmdStop, 0, 7);
            pnSub1.Controls.Add(lbTimeInfo, 0, 8);
            pnSub1.Controls.Add(trkSeekBar, 0, 9);
            pnSub1.Controls.Add(lbSynthVolume, 0, 10);
            pnSub1.Controls.Add(numSynthVolumee, 0, 11);
            pnSub1.Controls.Add(lbSeqSpeed, 0, 12);
            pnSub1.Controls.Add(numSeqSpeed, 0, 13);

            pnMain.Controls.Add(pnSub1);
            pnMain.Controls.Add(pnSub2);
            Controls.Add(pnMain);

            Leave += (sender, e) =>
            {
                StopPlayer(false);
            };
        }

        private void UpdateMessageIndex()
        {
            player.sequencer.MessageIndex = Math.Min(trkSeekBar.Value * 4, trkSeekBar.Maximum * 4 - 1);
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
            if (lblEIDError.Text != string.Empty) return;

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
            lblEIDError.Text = Entry.CheckEIDErrors(txtMusic.Text, true);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
