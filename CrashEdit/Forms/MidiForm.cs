using System.ComponentModel;
using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.Crash;
using M;
using NAudio.Wave;

namespace CrashEdit.CE
{
    public partial class MidiForm : DarkForm
    {
        private VABTool vabTool;
        private VAB vab;

        private int octave;

        private string sf2Path;

        private MidiSampleProvider? player;
        private WaveOutEvent? waveOut;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public MidiForm(VABTool vabTool)
        {
            this.vabTool = vabTool;
            InitializeComponent();
            Text = "Preview Keyboard " + vabTool.titleText;

            string basePath = Path.Combine("tmp", "tmp");
            Directory.CreateDirectory(Path.GetDirectoryName(basePath) ?? "");
            sf2Path = Path.ChangeExtension(basePath, ".sf2");
            GetVAB();

            CreatePlayer();
            octave = 5;
            lbOctave.Text = $"Octave: {octave}";
            UpdateProgramList();
            SetHotkeys();
        }

        private void CreatePlayer()
        {
            // Load the SF2 file and create the player.
            player = new MidiSampleProvider(sf2Path);

            // Create WaveOut.
            waveOut = new WaveOutEvent();
            waveOut.Init(player);
            waveOut.Play();
        }

        private void StopPlayer()
        {
            if (player != null)
            {
                player.Stop();
                player = null;
                waveOut?.Stop();
                waveOut?.Dispose();
                waveOut = null;
            }
        }

        private void GetVAB()
        {
            vab = VAB.Join(vabTool.vh, vabTool.vb);
            byte[] sf2 = SF2Conv.ToSF2(vab);
            File.WriteAllBytes(sf2Path, sf2);
        }

        private void UpdateProgramList()
        {
            dirty.Push(true);
            var oldItem = cmbProgram.SelectedItem;

            // Load the SF2 file.
            var sf2 = new MeltySynth.SoundFont(sf2Path);
            // Populate the program list.
            cmbProgram.Items.Clear();
            for (int i = 0; i < sf2.Instruments.Count; i++)
            {
                cmbProgram.Items.Add(sf2.Instruments[i].Name);
            }

            if (cmbProgram.Items.Contains(oldItem))
            {
                cmbProgram.SelectedItem = oldItem;
            }
            else if (cmbProgram.Items.Count > 0)
            {
                cmbProgram.SelectedIndex = 0;
            }
            dirty.Pop();
        }

        private void cmdReload_Click(object sender, EventArgs e)
        {
            GetVAB();
            UpdateProgramList();
            StopPlayer();
            CreatePlayer();
            ChangeInstrument();
        }

        private void piano_PianoKeyDown(object sender, PianoKeyEventArgs args)
        {
            waveOut.Volume = 1.0f;
            player.synthesizer.NoteOn(0, (byte)args.Key, 127);

            int oldOctave = octave;
            octave = args.Key / 12;
            if (oldOctave != octave)
            {
                lbOctave.Text = $"Octave: {octave}";
                SetHotkeys();
            }
            lbNote.Text = $"Note: {args.Key.ToString()} ({GetNoteName(args.Key)}{octave})";
        }

        private void piano_PianoKeyUp(object sender, PianoKeyEventArgs args)
        {
            player.synthesizer.NoteOff(0, (byte)args.Key);
        }

        private void ChangeInstrument()
        {
            string selectedText = cmbProgram.SelectedItem?.ToString() ?? string.Empty;
            if (Dirty || string.IsNullOrEmpty(selectedText)) return;

            // Get the program number from the instrument name and change the instrument.
            int program = Convert.ToInt32(Regex.Replace(selectedText, @"\D", ""));
            player.synthesizer.ProcessMidiMessage(0, 0xC0, program, 0);
        }

        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeInstrument();
            piano.Focus();
        }

        private void piano_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                case Keys.Q:
                    if (octave > 0)
                    {
                        --octave;
                        lbOctave.Text = $"Octave: {octave}";
                        SetHotkeys();
                    }
                    return;
                case Keys.NumPad2:
                case Keys.W:
                    if (octave < 10)
                    {
                        ++octave;
                        lbOctave.Text = $"Octave: {octave}";
                        SetHotkeys();
                    }
                    return;
            }
        }

        private void SetHotkeys()
        {
            int octaveOffset = octave * 12;
            Keys[] nullkeys = new Keys[octaveOffset];
            for (int i = 0; i < octaveOffset; ++i)
            {
                nullkeys[i] = Keys.None;
            }

            Keys[] keys =
            [
                Keys.Z, Keys.S, Keys.X, Keys.D, Keys.C, Keys.V, Keys.G, Keys.B, Keys.H, Keys.N, Keys.J, Keys.M
            ];

            Keys[] combinedKeys = new Keys[nullkeys.Length + keys.Length];
            nullkeys.CopyTo(combinedKeys, 0);
            keys.CopyTo(combinedKeys, nullkeys.Length);

            piano.HotKeys = combinedKeys;
        }

        public static string GetNoteName(int key)
        {
            switch (key % 12)
            {
                case 0:
                    return "C";
                case 1:
                    return "C#";
                case 2:
                    return "D";
                case 3:
                    return "D#";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "F#";
                case 7:
                    return "G";
                case 8:
                    return "G#";
                case 9:
                    return "A";
                case 10:
                    return "A#";
                case 11:
                    return "B";
                default:
                    return string.Empty;
            }
        }

        private void MidiForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DarkMessageBox.ShowInformation("Use the keyboard to play notes.\nPress numpad 1 and numpad 2 to change the octave.", "MIDIForm");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            StopPlayer();
        }

    }
}