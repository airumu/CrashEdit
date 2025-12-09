using System;
using System.ComponentModel;
using System.Data;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using NAudio.Midi;
using NAudio.Wave;
using Path = System.IO.Path;

namespace CrashEdit.CE.Forms
{
    public partial class SEQTool : DarkForm
    {
        private MidiFile? midi;
        private IList<MidiEvent> midiEvents;

        private string titleText;

        private readonly BindingList<MidiEventModel> eventList = [];

        private static readonly double pitchAdjustment = 8192;

        private TempoEvent midiTempo;

        private TimeSignatureInfo? timeSignatureInfo;
        private class TimeSignatureInfo
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }
        }

        private class MidiEventModel
        {
            public long Tick { get; set; }
            public int Channel { get; set; }
            public string EventType { get; set; }
            public string Data1 { get; set; }
            public string Data2 { get; set; }
        }

        static readonly Dictionary<int, string> CCNameByNumber = new()
        {
            { 0,  "Bank Select MSB" },
            { 1,  "Modulation" },
            { 2,  "Breath" },
            { 3,  "Undefined" },
            { 4,  "Foot Controller" },
            { 5,  "Portamento Time" },
            { 6,  "Data MSB" },
            { 7,  "Volume" },
            { 8,  "Balance" },
            { 9,  "Undefined" },
            { 10, "Pan" },
            { 11, "Expression" },
            { 12, "Effect Control 1" },
            { 13, "Effect Control 2" },
            { 14, "Undefined" },
            { 15, "Undefined" },

            // General Purpose Controllers
            { 16, "General Purpose 1" },
            { 17, "General Purpose 2" },
            { 18, "General Purpose 3" },
            { 19, "General Purpose 4" },

            // LSB for 0–31
            { 32, "Bank Select LSB" },
            { 33, "Modulation LSB" },
            { 34, "Breath LSB" },
            { 35, "Undefined LSB" },
            { 36, "Foot Controller LSB" },
            { 37, "Portamento Time LSB" },
            { 38, "Data LSB" },
            { 39, "Volume LSB" },
            { 40, "Balance LSB" },
            { 41, "Undefined LSB" },
            { 42, "Pan LSB" },
            { 43, "Expression LSB" },
            { 44, "Effect 1 LSB" },
            { 45, "Effect 2 LSB" },
            { 46, "Undefined LSB" },
            { 47, "Undefined LSB" },

            { 48, "General Purpose 1 LSB" },
            { 49, "General Purpose 2 LSB" },
            { 50, "General Purpose 3 LSB" },
            { 51, "General Purpose 4 LSB" },

            // Sound Controllers
            { 70, "Sound Controller 1 (Sound Variation)" },
            { 71, "Sound Controller 2 (Resonance)" },
            { 72, "Sound Controller 3 (Release Time)" },
            { 73, "Sound Controller 4 (Attack Time)" },
            { 74, "Sound Controller 5 (Brightness)" },
            { 75, "Sound Controller 6" },
            { 76, "Sound Controller 7" },
            { 77, "Sound Controller 8" },
            { 78, "Sound Controller 9" },
            { 79, "Sound Controller 10" },

            // Effects
            { 80, "General Purpose 5" },
            { 81, "General Purpose 6" },
            { 82, "General Purpose 7" },
            { 83, "General Purpose 8" },

            { 84, "Portamento Control" },

            { 91, "Reverb Level" },
            { 92, "Tremolo Depth" },
            { 93, "Chorus Level" },
            { 94, "Detune Depth" },
            { 95, "Phaser Depth" },

            // NRPN / RPN
            { 98, "NRPN LSB" },
            { 99, "NRPN MSB" },
            { 100, "RPN LSB" },
            { 101, "RPN MSB" },

            // Channel Mode Messages
            { 120, "All Sound Off" },
            { 121, "Reset All Controllers" },
            { 122, "Local Control" },
            { 123, "All Notes Off" },
            { 124, "Omni Off" },
            { 125, "Omni On" },
            { 126, "Mono Mode" },
            { 127, "Poly Mode" },
        };

        internal Stack<bool> dirty = [];
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public SEQTool()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("MusicNoteBlue");
            KeyPreview = true;
            MinimumSize = new Size(Width, 236);
            MaximumSize = new Size(Width, 8192);

            dgvEventsInit();
            dgvLoopInit(dgvLoopStart);
            dgvLoopInit(dgvLoopEnd);

            ToolStripButtonInit(tsbOpen, "FolderOpen", Resources.Toolbar_Open, $"{Resources.Toolbar_Open} (Ctrl + O)");
            ToolStripButtonInit(tsbSave, "Floppy", Resources.Toolbar_Save, $"{Resources.Toolbar_Save} (Ctrl + S)");
        }

        private void dgvLoopInit(DataGridView dgv)
        {
            DoubleBufferedDataGridView.Initialize(dgv);
            dgv.Columns.Add("", "");
            dgv.Columns.Add("", "");
            dgv.Columns[0].Width = 54;
            dgv.Columns[1].Width = 32;
            dgv.Rows.Add("Measure", 0);
            dgv.Rows.Add("Beat", 0);
            dgv.Rows.Add("Tick", 0);
        }

        private void ToolStripButtonInit(ToolStripButton tsb, string imageKey, string text, string tooltip)
        {
            tsb.Text = text;
            tsb.ImageKey = imageKey;
            tsb.ToolTipText = tooltip;
            //tsb.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            //tsb.TextImageRelation = TextImageRelation.ImageAboveText;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Text;
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new();
            dialog.Filter = FileFilters.MIDI + "|" + FileFilters.Any;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                dirty.Push(true);

                midi = new MidiFile(dialog.FileName, false);
                titleText = $"- {dialog.FileName}";
                Text = "SEQ Tool " + titleText;

                midi = MergeMIDITracks();
                midiEvents = midi.Events[0];
                OptimizeMIDI();

                UpdateEventGrids();
                UpdateLoopGrids();
                pnControls.Enabled = true;

                dirty.Pop();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (midi == null) return;
            try
            {
                short rhythm = (short)((timeSignatureInfo.Numerator << 8) | timeSignatureInfo.Denominator);
                SEQ seq = FromMidi(
                    midi.Events,
                    midiEvents,
                    Convert.ToInt64(numLoopStart.Value),
                    Convert.ToInt64(numLoopEnd.Value),
                    midiTempo.MicrosecondsPerQuarterNote,
                    rhythm
                    );
                FileUtil.SaveFile(seq.Save(), FileFilters.SEQ, FileFilters.Any);
            }
            catch
            {
            }
        }

        private void CloseMidi()
        {
            dgvEvents.SuspendLayout();
            dgvEvents.ScrollBars = ScrollBars.None;

            eventList.Clear();

            dgvEvents.ScrollBars = ScrollBars.Vertical;
            dgvEvents.ResumeLayout();

            midi = null;
            pnControls.Enabled = false;
        }

        #region Loops

        private void dgvLoop_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Cancel = true;
        }

        private void dgvLoop_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvLoopStart.SelectedCells.Count == 0) return;
            if (e.ColumnIndex == 0) return;

            string inputValue = e.FormattedValue?.ToString() ?? "";

            if (int.TryParse(inputValue, out int value))
            {
                int ppq = midi.DeltaTicksPerQuarterNote;
                int numerator = timeSignatureInfo.Numerator;
                int denominatorExponent = timeSignatureInfo.Denominator;
                int actualDenominator = 1 << denominatorExponent;
                int ticksPerBeat = ppq * 4 / actualDenominator;

                switch (e.RowIndex)
                {
                    case 0: // Measure
                        if (value < 1)
                        {
                            DarkMessageBox.ShowError("Measure must be 1 or greater.", Resources.Title_InputError);
                            e.Cancel = true;
                        }
                        break;

                    case 1: // Beat
                        if (value < 1 || value > numerator)
                        {
                            DarkMessageBox.ShowError($"Beat must be between 1 and {numerator}.", Resources.Title_InputError);
                            e.Cancel = true;
                        }
                        break;

                    case 2: // Tick
                        if (value < 0 || value >= ticksPerBeat)
                        {
                            DarkMessageBox.ShowError($"Tick must be between 0 and {ticksPerBeat - 1}.", Resources.Title_InputError);
                            e.Cancel = true;
                        }
                        break;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input.", Resources.Title_InputError);
                e.Cancel = true;
            }
        }

        private long MeasureBeatTickToTick(DataGridView dgv)
        {
            int measure = Convert.ToInt32(dgv[1, 0].Value);
            int beat = Convert.ToInt32(dgv[1, 1].Value);
            int tick = Convert.ToInt32(dgv[1, 2].Value);
            int ppq = midi.DeltaTicksPerQuarterNote;
            int numerator = timeSignatureInfo.Numerator;
            int denominatorExponent = timeSignatureInfo.Denominator;

            int actualDenominator = 1 << denominatorExponent;

            double ticksPerBeat = ppq * 4.0 / actualDenominator;
            double ticksPerMeasure = ticksPerBeat * numerator;

            long absoluteTick =
                (long)((measure - 1) * ticksPerMeasure +
                       (beat - 1) * ticksPerBeat +
                       tick);

            return absoluteTick;
        }

        private void dgvLoop_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dirty.Push(true);

            var dgv = (DataGridView)sender;
            long value = MeasureBeatTickToTick(dgv);
            DarkNumericUpDown numric;

            if (dgv == dgvLoopStart)
                numric = numLoopStart;
            else if (dgv == dgvLoopEnd)
                numric = numLoopEnd;
            else
                return;

            if (value <= numric.Maximum)
            {
                numric.Value = value;
            }
            else
            {
                numric.Value = numric.Maximum;
                var (measure, beat, tick) = GetTimeSignature((long)numric.Value);
                dgv.Rows[0].Cells[1].Value = measure;
                dgv.Rows[1].Cells[1].Value = beat;
                dgv.Rows[2].Cells[1].Value = tick;
            }

            dirty.Pop();
        }

        private (int measure, int beat, int tick) GetTimeSignature(long absoluteTick)
        {
            int ppq = midi.DeltaTicksPerQuarterNote;
            TimeSignatureInfo ts = timeSignatureInfo ?? new() { Numerator = 4, Denominator = 2 };

            int noteValue = 1 << ts.Denominator;  // 2^denominator
            double ticksPerBeat = ppq * 4.0 / noteValue;
            double ticksPerMeasure = ticksPerBeat * ts.Numerator;

            int measure = (int)(absoluteTick / ticksPerMeasure) + 1;
            long tickIntoMeasure = (long)(absoluteTick % ticksPerMeasure);

            int beat = (int)(tickIntoMeasure / ticksPerBeat) + 1;
            int tick = (int)(tickIntoMeasure % ticksPerBeat);

            return (measure, beat, tick);
        }

        private void UpdateLoopGrids()
        {
            // Loop start
            {
                (int measure, int beat, int tick) = GetTimeSignature((long)numLoopStart.Value);
                dgvLoopStart.Rows[0].Cells[1].Value = measure;
                dgvLoopStart.Rows[1].Cells[1].Value = beat;
                dgvLoopStart.Rows[2].Cells[1].Value = tick;
            }
            // Loop end
            {
                (int measure, int beat, int tick) = GetTimeSignature((long)numLoopEnd.Value);
                dgvLoopEnd.Rows[0].Cells[1].Value = measure;
                dgvLoopEnd.Rows[1].Cells[1].Value = beat;
                dgvLoopEnd.Rows[2].Cells[1].Value = tick;
            }
        }

        private void numLoopStart_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            var (measure, beat, tick) = GetTimeSignature((long)numLoopStart.Value);
            dgvLoopStart.Rows[0].Cells[1].Value = measure;
            dgvLoopStart.Rows[1].Cells[1].Value = beat;
            dgvLoopStart.Rows[2].Cells[1].Value = tick;
        }

        private void numLoopEnd_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            var (measure, beat, tick) = GetTimeSignature((long)numLoopEnd.Value);
            dgvLoopEnd.Rows[0].Cells[1].Value = measure;
            dgvLoopEnd.Rows[1].Cells[1].Value = beat;
            dgvLoopEnd.Rows[2].Cells[1].Value = tick;
        }

        #endregion

        #region Events

        private void dgvEventsInit()
        {
            DoubleBufferedDataGridView.Initialize(dgvEvents);
            dgvEvents.EditMode = DataGridViewEditMode.EditOnF2;
            dgvEvents.DataSource = eventList;
            dgvEvents.ReadOnly = true;
            //dgvEvents.AutoGenerateColumns = true;
            //dgvEvents.VirtualMode = true;
            //dgvEvents.CellValueNeeded += dgvEvents_CellValueNeeded;

            dgvEvents.Columns[0].Width = 60;
            dgvEvents.Columns[1].Width = 60;
            dgvEvents.Columns[2].Width = 120;
            dgvEvents.Columns[3].Width = 100;
            dgvEvents.Columns[4].Width = 100;
            foreach (DataGridViewColumn column in dgvEvents.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void dgvEvents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string text = dgvEvents.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

            switch (e.ColumnIndex)
            {
                case 2:
                    if (text == "ProgramChange")
                    {
                        dgvEvents.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Orange;
                    }
                    else if (text == "PitchWheel")
                    {
                        dgvEvents.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Gold;
                    }
                    break;
                case 3:
                    if (text == "Data MSB" || text == "NRPN MSB")
                    {
                        e.CellStyle.ForeColor = Color.MediumPurple;
                    }
                    else if (text == "EndTrack")
                    {
                        e.CellStyle.ForeColor = Color.SeaGreen;
                    }
                    else if (text == "Pan")
                    {
                        e.CellStyle.ForeColor = Color.DarkTurquoise;
                    }
                    else if (text == "Volume")
                    {
                        e.CellStyle.ForeColor = Color.MediumSpringGreen;
                    }
                    break;
            }
        }

        private async void UpdateEventGrids()
        {
            await LoadMidiAsync();
        }

        private static double NormalizePitchWheel(int pitch)
        {
            // pitch: -8192..+8191
            return (pitch - pitchAdjustment) / 8192.0;
        }

        private static string NoteNumberToName(int noteNumber)
        {
            string[] names = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];

            int note = noteNumber % 12;
            int octave = (noteNumber / 12) - 1;

            return $"{names[note]}{octave}";
        }

        private static string GetCCName(int ccNumber)
        {
            if (CCNameByNumber.TryGetValue(ccNumber, out var name))
                return name;
            return $"CC#{ccNumber}";
        }

        public async Task LoadMidiAsync()
        {
            dgvEvents.SuspendLayout();
            dgvEvents.ScrollBars = ScrollBars.None;

            // Prevent UI freezes
            //progressBar.Visible = true;
            progressBar.Visible = false;
            progressBar.Value = 0;

            var models = await Task.Run(() =>
            {
                var tmp = new List<MidiEventModel>(midiEvents.Count);
                int total = midiEvents.Count;
                int counter = 0;

                foreach (var ev in midiEvents)
                {
                    tmp.Add(ConvertEvent(ev));

                    counter++;
                    if (counter % 500 == 0)
                    {
                        int percent = (int)(counter / (double)total * 100);
                        BeginInvoke(new Action(() =>
                        {
                            progressBar.Value = percent;
                        }));
                    }
                }

                return tmp;
            });

            eventList.RaiseListChangedEvents = false;
            eventList.Clear();

            foreach (var m in models)
                eventList.Add(m);

            eventList.RaiseListChangedEvents = true;
            eventList.ResetBindings();

            dgvEvents.CurrentCell = null;
            dgvEvents.ClearSelection();
            dgvEvents.ScrollBars = ScrollBars.Vertical;
            dgvEvents.ResumeLayout();

            //progressBar.Visible = false;
        }

        private MidiEventModel ConvertEvent(MidiEvent ev)
        {
            string type = "";
            string data1 = "";
            string data2 = "";
            int ch = ev.Channel;

            switch (ev)
            {
                case NoteOnEvent on:
                    type = "NoteOn";
                    data1 = NoteNumberToName(on.NoteNumber);
                    data2 = on.Velocity.ToString();
                    break;

                case NoteEvent off when off.CommandCode == MidiCommandCode.NoteOff:
                    type = "NoteOff";
                    data1 = NoteNumberToName(off.NoteNumber);
                    data2 = off.Velocity.ToString();
                    break;

                case ControlChangeEvent cc:
                    type = "ControlChange";
                    data1 = GetCCName((int)cc.Controller);
                    data2 = cc.ControllerValue.ToString();
                    if ((int)cc.Controller == 99)
                    {
                        numLoopStart.Value = cc.AbsoluteTime;
                    }
                    break;

                case PitchWheelChangeEvent pw:
                    type = "PitchWheel";
                    data1 = NormalizePitchWheel(pw.Pitch).ToString("F5");
                    break;

                case PatchChangeEvent pc:
                    type = "ProgramChange";
                    data1 = pc.Patch.ToString();
                    break;

                case MetaEvent me:
                    type = "Meta";
                    ConvertMeta(me, out data1, out data2);
                    break;

                default:
                    type = ev.CommandCode.ToString();
                    data1 = ev.ToString();
                    break;
            }

            return new MidiEventModel
            {
                Tick = ev.AbsoluteTime,
                Channel = ch,
                EventType = type,
                Data1 = data1,
                Data2 = data2
            };
        }

        private void ConvertMeta(MetaEvent me, out string data1, out string data2)
        {
            data2 = "";
            switch (me.MetaEventType)
            {
                case MetaEventType.SetTempo:
                    var tempo = (TempoEvent)me;
                    data1 = tempo.MicrosecondsPerQuarterNote.ToString();
                    data2 = ((int)Math.Round(tempo.Tempo)).ToString() + " BPM";
                    midiTempo = tempo;
                    break;

                case MetaEventType.TimeSignature:
                    var ts = (TimeSignatureEvent)me;
                    data1 = ts.Numerator + "/" + (1 << ts.Denominator);
                    timeSignatureInfo = new()
                    {
                        Numerator = ts.Numerator,
                        Denominator = (int)Math.Log(1 << ts.Denominator, 2)
                    };
                    break;

                default:
                    data1 = me.MetaEventType.ToString();
                    break;
            }
        }

        private static string MakeKeyForMidiEvent(MidiEvent ev)
        {
            return ev switch
            {
                NoteOnEvent noteOn => $"NoteOn:{noteOn.Channel}:{noteOn.NoteNumber}:{noteOn.Velocity}",
                NoteEvent noteEv when ev.CommandCode == MidiCommandCode.NoteOff => $"NoteOff:{noteEv.Channel}:{noteEv.NoteNumber}:0",
                ControlChangeEvent cc => $"CC:{cc.Channel}:{(int)cc.Controller}:{cc.ControllerValue}",
                PatchChangeEvent pc => $"PC:{pc.Channel}:{pc.Patch}",
                PitchWheelChangeEvent pw => $"PW:{pw.Channel}:{pw.Pitch}",
                TempoEvent tempo => $"Tempo:{tempo.MicrosecondsPerQuarterNote}",
                _ => ev.GetType().Name + ":" + ev.ToString(),
            };
        }

        private void OptimizeMIDI()
        {
            // Remove unsupported events
            var filtered = midiEvents
             .Where(ev => IsSupportedEvent(ev, false))
             .ToList();

            midiEvents.Clear();
            foreach (var ev in filtered)
                midiEvents.Add(ev);
            

            long lastEventTick = 0;
            long actualLastEventTick = 0; // to remove events added by DAW

            for (int i = 0; i < midiEvents.Count; i++)
            {
                var ev = midiEvents[i];

                // Note Off => (Note On + vel 0)
                if (ev is NoteEvent note && ev.CommandCode == MidiCommandCode.NoteOff)
                {
                    var newEv = new NoteOnEvent(
                        note.AbsoluteTime,
                        note.Channel,
                        note.NoteNumber,
                        0,        // velocity 0
                        0         // duration
                    );

                    midiEvents[i] = newEv;
                }


                if (ev.AbsoluteTime > lastEventTick)
                {
                    actualLastEventTick = lastEventTick;
                    lastEventTick = ev.AbsoluteTime;
                }
            }


            // Remove unnecessary events at the end
            for (int i = midiEvents.Count - 1; i >= 0; i--)
            {
                var ev = midiEvents[i];
                if (ev.AbsoluteTime >= actualLastEventTick)
                {
                    if (!IsNoteOffEvent(ev) && ev.AbsoluteTime != 0)
                        midiEvents.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }


            // Add EndTrack
            var endTrack = new MetaEvent(MetaEventType.EndTrack, 0, actualLastEventTick);
            AddEventAtTick(midiEvents, endTrack, actualLastEventTick);
            midiEvents = [.. midiEvents
                 .OrderBy(ev => ev.AbsoluteTime)
                 .ThenBy(ev => ev is MetaEvent meta && meta.MetaEventType == MetaEventType.EndTrack)];


            // Remove duplicate events from tick0
            List<MidiEvent> tick0 = [.. midiEvents.Where(e => e.AbsoluteTime == 0)];
            var seen = new HashSet<string>();
            var toRemove = new List<MidiEvent>();

            foreach (var ev in tick0)
            {
                if (ev is NoteOnEvent || ev is NoteEvent)
                    continue;

                string key = MakeKeyForMidiEvent(ev);

                if (!seen.Add(key))
                {
                    toRemove.Add(ev);
                }
            }
            foreach (var ev in toRemove)
                midiEvents.Remove(ev);


            // Update numLoop
            numLoopStart.Maximum = actualLastEventTick;
            numLoopStart.Value = 0;
            numLoopEnd.Maximum = actualLastEventTick;
            numLoopEnd.Value = actualLastEventTick;
        }

        private MidiFile MergeMIDITracks()
        {
            MidiFile original = midi;
            int ppq = original.DeltaTicksPerQuarterNote;

            var merged = new MidiEventCollection(0, ppq);
            merged.AddTrack();

            for (int t = 0; t < original.Tracks; t++)
            {
                foreach (var ev in original.Events[t])
                {
                    merged[0].Add(ev);
                }
            }
            merged.PrepareForExport();

            string tempFile = Path.GetTempFileName();
            MidiFile.Export(tempFile, merged);

            MidiFile newMidi = new(tempFile, false);
            return newMidi;
        }

        #endregion

        #region Convert MIDI to SEQ

        public static SEQ FromMidi(MidiEventCollection midi, IList<MidiEvent> midiEvents, long loopStart, long loopEnd, int tempo, short rhythm)
        {
            short resolution = (short)midi.DeltaTicksPerQuarterNote;

            var events = InsertLoopEvent(midiEvents, loopStart, loopEnd);
            byte[] seqdata = ConvertTrackToRaw(events);

            return new SEQ(resolution, tempo, rhythm, seqdata);
        }

        private static void AddEventAtTick(IList<MidiEvent> events, MidiEvent newEvent, long tick)
        {
            newEvent.AbsoluteTime = tick;
            events.Add(newEvent);
        }

        private static IList<MidiEvent> InsertLoopEvent(IList<MidiEvent> midiEvents, long loopStartTick, long loopEndTick)
        {
            var events = new List<MidiEvent>(midiEvents);
            int channel = 1;
            int loopCunt = 127;

            // Loop Start
            var cc99Start = new ControlChangeEvent(loopStartTick, channel, (MidiController)99, 20);         // loop start
            var cc6LoopCount = new ControlChangeEvent(loopStartTick, channel, (MidiController)6, loopCunt); // loop count
            AddEventAtTick(events, cc99Start, loopStartTick);
            AddEventAtTick(events, cc6LoopCount, loopStartTick);
            events = [.. events
                .OrderBy(ev => ev.AbsoluteTime)
                .ThenByDescending(ev => ev is MetaEvent meta || ev == cc99Start || ev == cc6LoopCount)];    // priority: Meta, added events at the top

            // Loop End
            var cc99End = new ControlChangeEvent(loopEndTick, channel, (MidiController)99, 30);         // loop end
            AddEventAtTick(events, cc99End, loopEndTick);
            events = [.. events
                 .OrderBy(ev => ev.AbsoluteTime)
                 .ThenBy(ev => ev is MetaEvent meta && meta.MetaEventType == MetaEventType.EndTrack)];  // priority: EndTrack at the bottom

            return events;
        }

        private static bool IsSupportedEvent(MidiEvent ev, bool toSEQ)
        {
            return ev switch
            {
                NoteOnEvent => true,
                NoteEvent note when note.CommandCode == MidiCommandCode.NoteOff => true,
                PatchChangeEvent => true,
                PitchWheelChangeEvent => true,

                ControlChangeEvent cc => (int)cc.Controller switch
                {
                    7 or 10 or 11 or 98 or 99 => true,
                    6 when cc.ControllerValue == 127 => true,
                    _ => false
                },

                TempoEvent => true,

                MetaEvent meta when
                    (meta.MetaEventType == MetaEventType.TimeSignature && !toSEQ) ||
                    meta.MetaEventType == MetaEventType.EndTrack
                    => true,

                _ => false
            };
        }

        private static bool IsNoteOffEvent(MidiEvent ev)
        {
            if (ev is NoteEvent noteEv)
            {
                return noteEv.CommandCode == MidiCommandCode.NoteOff ||
                       (noteEv.CommandCode == MidiCommandCode.NoteOn && noteEv.Velocity == 0);
            }
            return false;
        }

        private static byte[] ConvertTrackToRaw(IList<MidiEvent> events)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            int lastCommand = -1;
            long lastTick = 0;

            foreach (var ev in events)
            {
                if (!IsSupportedEvent(ev, true))
                    continue;

                int dt = (int)(ev.AbsoluteTime - lastTick);
                lastTick = ev.AbsoluteTime;

                WriteVlq(bw, dt);

                if (ev is MetaEvent meta && meta.MetaEventType == MetaEventType.EndTrack)
                    ExportPs1EndTrack(meta, bw, ref lastCommand);
                else if (ev is TempoEvent tempo)
                    ExportPs1Tempo(tempo, bw, ref lastCommand);
                else
                    ExportPs1Event(ev, bw, ref lastCommand);
            }

            return ms.ToArray();
        }

        private static bool IsPs1SupportedCC(int cc)
        {
            return cc == 6   // Data Entry
                || cc == 7   // Volume
                || cc == 10  // Panpot
                || cc == 11  // Expression
                || cc == 98  // NRPN LSB
                || cc == 99; // NRPN MSB
        }

        private static void ExportPs1EndTrack(MetaEvent meta, BinaryWriter bw, ref int lastCommand)
        {
            bw.Write((byte)0xFF);
            bw.Write((byte)0x2F);
            bw.Write((byte)0x00);
        }

        private static void ExportPs1Tempo(TempoEvent tempo, BinaryWriter bw, ref int lastCommand)
        {
            // SEQ format： xx FF 51 yy yy yy

            //// Running status: Write only if 0xFF is not lastCommand
            //if (lastCommand != 0xFF)
            //{
            //    bw.Write((byte)0xFF);
            //    lastCommand = 0xFF;
            //}

            // Write 0xFF anyway, as some programmes cannot recognise it
            bw.Write((byte)0xFF);
            lastCommand = 0xFF;

            bw.Write((byte)0x51);

            int val = tempo.MicrosecondsPerQuarterNote;
            bw.Write((byte)((val >> 16) & 0xFF));
            bw.Write((byte)((val >> 8) & 0xFF));
            bw.Write((byte)(val & 0xFF));
        }

        private static int GetShiftedChannel(MidiEvent ev)
        {
            return ev switch
            {
                NoteEvent note => Math.Max(0, (note.Channel - 1) & 0x0F),
                ControlChangeEvent cc => Math.Max(0, (cc.Channel - 1) & 0x0F),
                PatchChangeEvent pc => Math.Max(0, (pc.Channel - 1) & 0x0F),
                PitchWheelChangeEvent pw => Math.Max(0, (pw.Channel - 1) & 0x0F),
                ChannelAfterTouchEvent cat => Math.Max(0, (cat.Channel - 1) & 0x0F),

                _ => -1
            };
        }

        private static int DenormalizePitchWheel(double value)
        {
            // value: -1.0 .. 1.0
            int pitch = (int)Math.Round((value - pitchAdjustment) * 8192.0);
            return Math.Clamp(pitch, 0, 16383);
        }

        private static void ExportPs1Event(MidiEvent ev, BinaryWriter bw, ref int lastCommand)
        {
            int ch = GetShiftedChannel(ev);

            if (ev is NoteOnEvent noteOn)
            {
                int status = 0x90 | ch;
                if (status != lastCommand)
                {
                    bw.Write((byte)status);
                    lastCommand = status;
                }
                bw.Write((byte)(noteOn.NoteNumber & 0x7F));
                bw.Write((byte)(noteOn.Velocity & 0x7F));
                return;
            }

            if (ev is NoteEvent noteEv && ev.CommandCode == MidiCommandCode.NoteOff)
            {
                int status = 0x90 | ch;
                if (status != lastCommand)
                {
                    bw.Write((byte)status);
                    lastCommand = status;
                }
                bw.Write((byte)(noteEv.NoteNumber & 0x7F));
                bw.Write((byte)0);
                return;
            }

            if (ev is ControlChangeEvent cc)
            {
                int ccNum = (int)cc.Controller;
                if (!IsPs1SupportedCC(ccNum)) return;

                int status = 0xB0 | ch;
                if (status != lastCommand)
                {
                    bw.Write((byte)status);
                    lastCommand = status;
                }
                bw.Write((byte)(ccNum & 0x7F));
                bw.Write((byte)(cc.ControllerValue & 0x7F));
                return;
            }

            if (ev is PatchChangeEvent pc)
            {
                int status = 0xC0 | ch;
                if (status != lastCommand)
                {
                    bw.Write((byte)status);
                    lastCommand = status;
                }
                bw.Write((byte)(pc.Patch & 0x7F));
                return;
            }

            if (ev is PitchWheelChangeEvent pw)
            {
                int status = 0xE0 | ch;
                if (status != lastCommand)
                {
                    bw.Write((byte)status);
                    lastCommand = status;
                }
                //int value14 = DenormalizePitchWheel(pw.Pitch);
                int value14 = pw.Pitch;
                bw.Write((byte)(value14 & 0x7F));
                bw.Write((byte)((value14 >> 7) & 0x7F));
                return;
            }

        }

        private static void WriteVlq(BinaryWriter bw, int value)
        {
            int buffer = value & 0x7F;
            while ((value >>= 7) > 0)
            {
                buffer <<= 8;
                buffer |= ((value & 0x7F) | 0x80);
            }
            while (true)
            {
                bw.Write((byte)buffer);
                if ((buffer & 0x80) != 0)
                    buffer >>= 8;
                else
                    break;
            }
        }

        #endregion

    }
}
