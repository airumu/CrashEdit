using System.ComponentModel;
using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class MidiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnPiano = new Panel();
            piano = new M.PianoBox();
            fraMidi = new DarkGroupBox();
            cmdRefresh = new DarkButton();
            cmbMidiOut = new DarkComboBox();
            lbMidiOut = new Label();
            cmbMidiIn = new DarkComboBox();
            lbMlidiIn = new Label();
            lbChannel = new Label();
            numChannel = new DarkNumericUpDown();
            lbProgram = new Label();
            cmbProgram = new DarkComboBox();
            lbNote = new Label();
            lbOctave = new Label();
            pnControls = new Panel();
            cmdReload = new DarkButton();
            pnPiano.SuspendLayout();
            fraMidi.SuspendLayout();
            ((ISupportInitialize)numChannel).BeginInit();
            pnControls.SuspendLayout();
            SuspendLayout();
            // 
            // pnPiano
            // 
            pnPiano.Controls.Add(piano);
            pnPiano.Location = new Point(0, 30);
            pnPiano.Margin = new Padding(4, 3, 4, 3);
            pnPiano.Name = "pnPiano";
            pnPiano.Size = new Size(1080, 88);
            pnPiano.TabIndex = 1;
            // 
            // piano
            // 
            piano.BlackKeyColor = Color.Black;
            piano.BorderColor = Color.Black;
            piano.Dock = DockStyle.Fill;
            piano.HotKeys = new Keys[]
    {
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.None,
    Keys.Q,
    Keys.D2,
    Keys.W,
    Keys.D3,
    Keys.E,
    Keys.R,
    Keys.D5,
    Keys.T,
    Keys.D6,
    Keys.Y,
    Keys.D7,
    Keys.U,
    Keys.Z,
    Keys.S,
    Keys.X,
    Keys.D,
    Keys.C,
    Keys.V,
    Keys.G,
    Keys.B,
    Keys.H,
    Keys.N,
    Keys.J,
    Keys.M
    };
            piano.Location = new Point(0, 0);
            piano.Margin = new Padding(4, 3, 4, 3);
            piano.Name = "piano";
            piano.NoteHighlightColor = Color.SkyBlue;
            piano.Octaves = 11;
            piano.Size = new Size(1080, 88);
            piano.TabIndex = 1;
            piano.Text = "Piano";
            piano.WhiteKeyColor = Color.White;
            piano.PianoKeyDown += piano_PianoKeyDown;
            piano.PianoKeyUp += piano_PianoKeyUp;
            piano.KeyDown += piano_KeyDown;
            // 
            // fraMidi
            // 
            fraMidi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            fraMidi.BackColor = Color.Transparent;
            fraMidi.Controls.Add(cmdRefresh);
            fraMidi.Controls.Add(cmbMidiOut);
            fraMidi.Controls.Add(lbMidiOut);
            fraMidi.Controls.Add(cmbMidiIn);
            fraMidi.Controls.Add(lbMlidiIn);
            fraMidi.Location = new Point(853, 2);
            fraMidi.Margin = new Padding(4, 3, 4, 3);
            fraMidi.Name = "fraMidi";
            fraMidi.Padding = new Padding(4, 3, 4, 3);
            fraMidi.Size = new Size(184, 112);
            fraMidi.TabIndex = 2;
            fraMidi.TabStop = false;
            fraMidi.Text = "MIDI";
            fraMidi.Visible = false;
            // 
            // cmdRefresh
            // 
            cmdRefresh.BorderColour = Color.Empty;
            cmdRefresh.CustomColour = false;
            cmdRefresh.FlatBottom = false;
            cmdRefresh.FlatTop = false;
            cmdRefresh.Location = new Point(89, 77);
            cmdRefresh.Margin = new Padding(4, 3, 4, 3);
            cmdRefresh.Name = "cmdRefresh";
            cmdRefresh.Padding = new Padding(6);
            cmdRefresh.Size = new Size(88, 27);
            cmdRefresh.TabIndex = 4;
            cmdRefresh.Text = "Refresh";
            // 
            // cmbMidiOut
            // 
            cmbMidiOut.DrawMode = DrawMode.OwnerDrawVariable;
            cmbMidiOut.FormattingEnabled = true;
            cmbMidiOut.Location = new Point(35, 48);
            cmbMidiOut.Margin = new Padding(4, 3, 4, 3);
            cmbMidiOut.Name = "cmbMidiOut";
            cmbMidiOut.Size = new Size(140, 24);
            cmbMidiOut.TabIndex = 3;
            // 
            // lbMidiOut
            // 
            lbMidiOut.AutoSize = true;
            lbMidiOut.Location = new Point(8, 52);
            lbMidiOut.Margin = new Padding(4, 0, 4, 0);
            lbMidiOut.Name = "lbMidiOut";
            lbMidiOut.Size = new Size(27, 15);
            lbMidiOut.TabIndex = 2;
            lbMidiOut.Text = "Out";
            // 
            // cmbMidiIn
            // 
            cmbMidiIn.DrawMode = DrawMode.OwnerDrawVariable;
            cmbMidiIn.FormattingEnabled = true;
            cmbMidiIn.Location = new Point(35, 20);
            cmbMidiIn.Margin = new Padding(4, 3, 4, 3);
            cmbMidiIn.Name = "cmbMidiIn";
            cmbMidiIn.Size = new Size(140, 24);
            cmbMidiIn.TabIndex = 1;
            // 
            // lbMlidiIn
            // 
            lbMlidiIn.AutoSize = true;
            lbMlidiIn.Location = new Point(8, 23);
            lbMlidiIn.Margin = new Padding(4, 0, 4, 0);
            lbMlidiIn.Name = "lbMlidiIn";
            lbMlidiIn.Size = new Size(17, 15);
            lbMlidiIn.TabIndex = 0;
            lbMlidiIn.Text = "In";
            // 
            // lbChannel
            // 
            lbChannel.AutoSize = true;
            lbChannel.BackColor = Color.Transparent;
            lbChannel.Location = new Point(943, 7);
            lbChannel.Margin = new Padding(4, 0, 4, 0);
            lbChannel.Name = "lbChannel";
            lbChannel.Size = new Size(51, 15);
            lbChannel.TabIndex = 3;
            lbChannel.Text = "Channel";
            lbChannel.Visible = false;
            // 
            // numChannel
            // 
            numChannel.Location = new Point(1002, 3);
            numChannel.Margin = new Padding(4, 3, 4, 3);
            numChannel.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numChannel.Name = "numChannel";
            numChannel.Size = new Size(40, 23);
            numChannel.TabIndex = 4;
            numChannel.Visible = false;
            // 
            // lbProgram
            // 
            lbProgram.AutoSize = true;
            lbProgram.BackColor = Color.Transparent;
            lbProgram.Location = new Point(4, 7);
            lbProgram.Margin = new Padding(4, 0, 4, 0);
            lbProgram.Name = "lbProgram";
            lbProgram.Size = new Size(53, 15);
            lbProgram.TabIndex = 3;
            lbProgram.Text = "Program";
            // 
            // cmbProgram
            // 
            cmbProgram.DrawMode = DrawMode.OwnerDrawVariable;
            cmbProgram.FormattingEnabled = true;
            cmbProgram.Location = new Point(64, 3);
            cmbProgram.MaxDropDownItems = 16;
            cmbProgram.Name = "cmbProgram";
            cmbProgram.Size = new Size(121, 24);
            cmbProgram.TabIndex = 5;
            cmbProgram.SelectedIndexChanged += cmbProgram_SelectedIndexChanged;
            // 
            // lbNote
            // 
            lbNote.AutoSize = true;
            lbNote.BackColor = Color.Transparent;
            lbNote.Font = new Font("Segoe UI", 9F);
            lbNote.Location = new Point(192, 7);
            lbNote.Margin = new Padding(4, 0, 4, 0);
            lbNote.Name = "lbNote";
            lbNote.Size = new Size(76, 15);
            lbNote.TabIndex = 3;
            lbNote.Text = "Note: 60 (C5)";
            // 
            // lbOctave
            // 
            lbOctave.AutoSize = true;
            lbOctave.BackColor = Color.Transparent;
            lbOctave.Location = new Point(276, 7);
            lbOctave.Margin = new Padding(4, 0, 4, 0);
            lbOctave.Name = "lbOctave";
            lbOctave.Size = new Size(56, 15);
            lbOctave.TabIndex = 3;
            lbOctave.Text = "Octave: 5";
            // 
            // pnControls
            // 
            pnControls.AutoSize = true;
            pnControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnControls.Controls.Add(cmdReload);
            pnControls.Controls.Add(lbChannel);
            pnControls.Controls.Add(lbOctave);
            pnControls.Controls.Add(lbNote);
            pnControls.Controls.Add(numChannel);
            pnControls.Controls.Add(lbProgram);
            pnControls.Controls.Add(cmbProgram);
            pnControls.Dock = DockStyle.Top;
            pnControls.Location = new Point(0, 0);
            pnControls.Name = "pnControls";
            pnControls.Size = new Size(1046, 30);
            pnControls.TabIndex = 6;
            // 
            // cmdReload
            // 
            cmdReload.BorderColour = Color.Empty;
            cmdReload.CustomColour = false;
            cmdReload.FlatBottom = false;
            cmdReload.FlatTop = false;
            cmdReload.Location = new Point(339, 3);
            cmdReload.Name = "cmdReload";
            cmdReload.Padding = new Padding(5);
            cmdReload.Size = new Size(75, 23);
            cmdReload.TabIndex = 6;
            cmdReload.Text = "Reload";
            cmdReload.Click += cmdReload_Click;
            // 
            // MidiForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1046, 117);
            Controls.Add(pnPiano);
            Controls.Add(pnControls);
            Controls.Add(fraMidi);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MidiForm";
            Icon = Embeds.GetIcon("SpeakerWhite");
            TransparencyKey = Color.FromArgb(31, 31, 32);
            HelpButtonClicked += MidiForm_HelpButtonClicked;
            pnPiano.ResumeLayout(false);
            fraMidi.ResumeLayout(false);
            fraMidi.PerformLayout();
            ((ISupportInitialize)numChannel).EndInit();
            pnControls.ResumeLayout(false);
            pnControls.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Panel pnPiano;
        private M.PianoBox piano;
        private DarkGroupBox fraMidi;
        private DarkButton cmdRefresh;
        private DarkComboBox cmbMidiOut;
        private Label lbMidiOut;
        private DarkComboBox cmbMidiIn;
        private Label lbMlidiIn;
        private Label lbChannel;
        private DarkNumericUpDown numChannel;
        private Label lbProgram;
        private DarkComboBox cmbProgram;
        private Label lbNote;
        private Label lbOctave;
        private Panel pnControls;
        private DarkButton cmdReload;
    }
}

