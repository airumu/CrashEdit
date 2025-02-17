using AltUI.Controls;
using CrashEdit.Crash;
using MeltySynth;
using NAudio.Wave;

namespace CrashEdit.CE
{
    public sealed class MusicBox : UserControl
    {
        private MusicEntryController controller;
        private MusicEntry musicentry;

        private TableLayoutPanel pnMain;
        private TableLayoutPanel pnSub1;
        private TableLayoutPanel pnSub2;
        private DoubleBufferedListView lstMusic;
        private DarkTextBox txtMusic;
        private Label lblEIDError;
        private Label lblMasterVolume;
        private DarkNumericUpDown numMasterVolume;
        private Label lblMasterPan;
        private DarkNumericUpDown numMasterPan;
        private DarkButton cmdLoad;
        private DarkButton cmdPlay;
        private DarkButton cmdStop;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private MidiSampleProvider player;

        public MusicBox(MusicEntryController controller)
        {
            this.controller = controller;
            musicentry = controller.MusicEntry;

            BackColor = Color.FromArgb(31, 31, 32);

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

            if (musicentry.VH != null)
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
            }

            cmdLoad = new DarkButton()
            {
                Text = "Load"
            };
            cmdLoad.Click += (sender, e) =>
            {
                VAB vab = controller.FindLinkedVAB();
                string outputPath = "test.dls";
                byte[] dls = vab.ToDLS().Save();
                File.WriteAllBytes(outputPath, dls);

                //VAB vab = controller.FindLinkedVAB();
                //string outputPath = "test.sf2";
                //byte[] sf2 = SF2Conv.ToSF2(vab, true);
                //File.WriteAllBytes(outputPath, sf2);
                return;
            };

            cmdPlay = new DarkButton()
            {
                Text = "Play"
            };
            cmdPlay.Click += (sender, e) =>
            {
                if (musicentry.Tracks.Count == 0) return;

                SEQ seq = musicentry.Tracks[0];
                byte[] midiData = seq.ToMIDI();
                string tempFile = Path.GetTempFileName();
                string midiPath = Path.ChangeExtension(tempFile, ".mid");
                File.WriteAllBytes(midiPath, midiData);
                Console.WriteLine("MIDI File Path: " + midiPath);
                Console.WriteLine("Now playing: " + musicentry.EName);

                string sfPath = "test.sf2";

                //string sfPath = "";
                //using (OpenFileDialog dialog = new OpenFileDialog())
                //{
                //    dialog.Filter = FileFilters.SF2;
                //    if (dialog.ShowDialog(this) == DialogResult.OK)
                //    {
                //        sfPath = dialog.FileName;
                //    }
                //    else return;
                //}

                //string midipath = "";
                //using (OpenFileDialog dialog = new OpenFileDialog())
                //{
                //    dialog.Filter = FileFilters.MIDI;
                //    if (dialog.ShowDialog(this) == DialogResult.OK)
                //    {
                //        midipath = dialog.FileName;
                //    }
                //    else return;
                //}

                player = new MidiSampleProvider(sfPath);

                using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(player);
                    waveOut.Play();

                    // Load the MIDI file.
                    var midiFile = new MidiFile(midiPath);

                    // Play the MIDI file.
                    player.Play(midiFile, true);

                    // Wait until any key is pressed.
                    Console.ReadKey();
                }

            };

            cmdStop = new DarkButton()
            {
                Text = "Stop"
            };
            cmdStop.Click += (sender, e) =>
            {
                if (player != null)
                {
                    player.Stop();
                }
            };


            pnMain = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill
            };
            pnSub1 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 6,
                Dock = DockStyle.Fill
            };
            pnSub2 = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 4,
                Dock = DockStyle.Fill
            };
            pnSub1.Controls.Add(lstMusic, 0, 0);
            pnSub1.Controls.Add(txtMusic, 0, 1);
            pnSub1.Controls.Add(lblEIDError, 0, 2);
            pnSub1.Controls.Add(cmdLoad, 0, 3);
            pnSub1.Controls.Add(cmdPlay, 0, 4);
            pnSub1.Controls.Add(cmdStop, 0, 5);
            if (musicentry.VH != null)
            {
                pnSub2.Controls.Add(lblMasterVolume, 0, 0);
                pnSub2.Controls.Add(numMasterVolume, 0, 1);
                pnSub2.Controls.Add(lblMasterPan, 0, 2);
                pnSub2.Controls.Add(numMasterPan, 0, 3);
            }
            pnMain.Controls.Add(pnSub1);
            pnMain.Controls.Add(pnSub2);
            Controls.Add(pnMain);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class MidiSampleProvider : ISampleProvider
    {
        private static WaveFormat format = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

        private Synthesizer synthesizer;
        private MidiFileSequencer sequencer;

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
