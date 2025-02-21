using System;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using NAudio.Wave;

namespace CrashEdit.CE.Forms
{
    public partial class VABTool : DarkForm
    {
        public const int ProgramSize = 16;
        public const int ToneSize = 32;
        public const int InstrumentSize = 0x200;
        public const int MaxVBSize = 0x7E000;

        public VAB? vab;
        public VH? vh;
        public SampleLine[]? vb; // this is unused actually

        private WaveOutEvent waveOut;
        private WaveStream waveStream;

        private ADSRForm? frmADSR;
        private VAGListForm? frmVAGList;
        private MidiForm? frmMidiForm;

        private int programIndex;
        private int toneIndex;

        private readonly int ColHeadVersion = 0;
        private readonly int ColHeadTotalSize = 1;
        private readonly int ColHeadVHSize = 2;
        private readonly int ColHeadVBSize = 3;
        private readonly int ColHeadPrograms = 4;
        private readonly int ColHeadTones = 5;
        private readonly int ColHeadVAGs = 6;
        private readonly int ColHeadMasterVolume = 7;
        private readonly int ColHeadMasterPan = 8;

        private readonly int ColProgProgramNumber = 0;
        private readonly int ColProgToneCount = 1;
        private readonly int ColProgVolume = 2;
        private readonly int ColProgPan = 3;

        private readonly int ColToneIndex = 0;
        private readonly int ColTonePriority = 1;
        private readonly int ColToneMode = 2;
        private readonly int ColToneVolume = 3;
        private readonly int ColTonePan = 4;
        private readonly int ColToneCenter = 5;
        private readonly int ColTonePitch = 6;
        private readonly int ColToneMinNote = 7;
        private readonly int ColToneMaxNote = 8;
        private readonly int ColTonePBmin = 9;
        private readonly int ColTonePBmax = 10;
        private readonly int ColToneADSR1 = 11;
        private readonly int ColToneADSR2 = 12;
        private readonly int ColToneVAG = 13;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public VABTool()
        {
            InitializeComponent();
            Icon = Embeds.GetIcon("Wrench");
            toolStrip.ImageList = Embeds.ImageList;
            ToolStripButtonInit(tbbOpen, "FolderOpen", Resources.Toolbar_Open, $"{Resources.Toolbar_Open} (Ctrl + O)");
            ToolStripButtonInit(tbbSave, "Floppy", Resources.Toolbar_Save, $"{Resources.Toolbar_Save} (Ctrl + S)");
            ToolStripButtonInit(tbbClose, "Folder", Resources.Toolbar_Close, $"{Resources.Toolbar_Close} (Ctrl + W)");

            DoubleBufferedDataGridView.Initialize(dgvHeader);
            DoubleBufferedDataGridView.Initialize(dgvPrograms);
            DoubleBufferedDataGridView.Initialize(dgvTones);
            dgvColumnsInit();

            waveOut = new WaveOutEvent { DesiredLatency = 200 };

            frmMidiForm = null;

            fraVABHeader.Enabled =
            fraVABPrograms.Enabled =
            fraTones.Enabled = false;

            numPriority.MouseWheel += ScrollHandlerFunction;
            numMode.MouseWheel += ScrollHandlerFunction;
            numVAG.MouseWheel += ScrollHandlerFunction;
            numNote.MouseWheel += ScrollHandlerFunction;

            // debug vab, remove this later
            {
                byte[] file = File.ReadAllBytes("test.vab");
                vab = VAB.Load(file);
                vab.Split(out VH _vh, out SampleLine[] _vb);
                vh = _vh;
                vb = _vb;

                fraVABHeader.Enabled =
                fraVABPrograms.Enabled =
                fraTones.Enabled = true;
                UpdateHeader();
            }
        }

        private void ToolStripButtonInit(ToolStripButton tbb, string imageKey, string text, string tooltip)
        {
            tbb.Text = text;
            tbb.ImageKey = imageKey;
            //tbb.ToolTipText = tooltip;
            //tbb.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            //tbb.TextImageRelation = TextImageRelation.ImageAboveText;
            tbb.DisplayStyle = ToolStripItemDisplayStyle.Text;
        }

        private void dgvColumnsInit()
        {
            dgvHeader.Columns.Add("Version", "Version");
            dgvHeader.Columns.Add("TotalSize", "Total Size");
            dgvHeader.Columns.Add("VHSize", "VH Size");
            dgvHeader.Columns.Add("VBSize", "VB Size");
            dgvHeader.Columns.Add("Programs", "Programs");
            dgvHeader.Columns.Add("Tones", "Tones");
            dgvHeader.Columns.Add("VAGs", "VAGs");
            dgvHeader.Columns.Add("MasterVolume", "Master\nVolume");
            dgvHeader.Columns.Add("Master Pan", "Master\nPan");

            dgvPrograms.Columns.Add("Program", "Program");
            dgvPrograms.Columns.Add("Tones", "Tones");
            dgvPrograms.Columns.Add("Volume", "Volume");
            dgvPrograms.Columns.Add("Pan", "Pan");

            dgvTones.Columns.Add("\u00A0\u00A0\u00A0\u00A0", "\u00A0\u00A0\u00A0\u00A0");
            dgvTones.Columns.Add("Priority", "Priority");
            dgvTones.Columns.Add("Mode", "Mode");
            dgvTones.Columns.Add("Volume", "Volume");
            dgvTones.Columns.Add("Pan", "Pan");
            dgvTones.Columns.Add("CenterNote", "Center");
            dgvTones.Columns.Add("PitchShift", "Pitch");
            dgvTones.Columns.Add("MinimumNote", "MinNote");
            dgvTones.Columns.Add("MaximumNote", "MaxNote");
            dgvTones.Columns.Add("PitchBendMinimum", "PBmin");
            dgvTones.Columns.Add("PitchBendMaximum", "PBmax");
            dgvTones.Columns.Add("ADSR1", "ADSR1");
            dgvTones.Columns.Add("ADSR2", "ADSR2");
            dgvTones.Columns.Add("VAG", "VAG");
            dgvTones.Columns[ColToneADSR1].Visible = false;
            dgvTones.Columns[ColToneADSR2].Visible = false;

            dgvHeader.ColumnHeadersHeight = 36;
            foreach (DataGridViewColumn column in dgvHeader.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            }
            foreach (DataGridViewColumn column in dgvPrograms.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 60;
            }
            foreach (DataGridViewColumn column in dgvTones.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 48;
            }
        }

        private void UpdateHeader()
        {
            dirty.Push(true);

            // Update the header.
            {
                dgvHeader.Rows.Clear();
                DataGridViewRow row = new DataGridViewRow();

                // Calculate the tone count.
                int tonecount = 0;
                foreach (VHProgram program in vh.Programs.Values)
                {
                    tonecount += program.Tones.Count;
                }

                // Calculate the VB size.
                int vbSize = vh.VBSize * 16;

                row.CreateCells(dgvHeader, vh.VHVersion, vh.Size, vh.Size - vbSize, vbSize, vh.Programs.Count, tonecount, vh.Waves.Count, vh.Volume, vh.Panning);
                dgvHeader.Rows.Add(row);
            }
            // Update the programs.
            {
                dgvPrograms.Rows.Clear();
                int nullCount = 0;
                for (int i = 0; i < vh.Programs.Count + nullCount; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    if (vh.Programs.ContainsKey(i))
                    {
                        VHProgram prog = vh.Programs[i];
                        row.CreateCells(dgvPrograms, i, prog.Tones.Count, prog.Volume, prog.Panning);
                    }
                    else if (vh.NullPrograms.ContainsKey(i))
                    {
                        VHProgram prog = vh.NullPrograms[i];
                        row.CreateCells(dgvPrograms, i, prog.Tones.Count, prog.Volume, prog.Panning);
                        ++nullCount;
                    }
                    else throw new Exception("Pograms do not contain the key.");

                    dgvPrograms.Rows.Add(row);
                }
                // Calculate the width.
                int totalWidth = dgvPrograms.RowHeadersWidth;
                foreach (DataGridViewColumn column in dgvPrograms.Columns)
                {
                    totalWidth += column.Width;
                }
                int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
                totalWidth += scrollbarWidth;
                dgvPrograms.Width = totalWidth;
            }

            numVAG.Minimum = 1;
            numVAG.Maximum = vh.Waves.Count;

            dirty.Pop();
        }

        private void tbbOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = FileFilters.VAB + "|" + FileFilters.Any;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] file = File.ReadAllBytes(dialog.FileName);
                    vab = VAB.Load(file);
                    vab.Split(out VH _vh, out SampleLine[] _vb);
                    vh = _vh;
                    vb = _vb;

                    fraVABHeader.Enabled =
                    fraVABPrograms.Enabled =
                    fraTones.Enabled = true;
                    UpdateHeader();
                }
            }
        }

        private void tbbSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = FileFilters.VAB + "|" + FileFilters.Any;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] file = vab.Save(vh);
                    File.WriteAllBytes(dialog.FileName, file);
                }
            }
        }

        private void tbbClose_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowWarning("Are you sure you want to close the VAB file?", Resources.Close_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                waveOut.Stop();
                waveStream?.Dispose();

                dgvHeader.Rows.Clear();
                dgvPrograms.Rows.Clear();
                dgvTones.Rows.Clear();
                vab = null;
                vh = null;
                vb = null;
                fraVABHeader.Enabled =
                fraVABPrograms.Enabled =
                fraTones.Enabled = false;
            }
        }

        private void GetSelectedRow(DataGridView dataGridView, out int rowIdx, out DataGridViewRow? selectedRow)
        {
            if (!(dataGridView.SelectedCells.Count > 0))
            {
                rowIdx = -1;
                selectedRow = null;
                return;
            }
            rowIdx = dataGridView.SelectedCells[0].RowIndex;
            selectedRow = dataGridView.Rows[rowIdx];
        }

        private void dgvVABPrograms_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvPrograms, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null) return;

            dirty.Push(true);

            programIndex = rowIdx;

            trkProgramVolume.Value = Convert.ToInt32(selectedRow.Cells[ColProgVolume].Value);
            trkProgramPan.Value = Convert.ToInt32(selectedRow.Cells[ColProgPan].Value);
            UpdateProgramVolumeText();
            UpdateProgramPanText();

            // Update the tones.
            dgvTones.Rows.Clear();
            if (vh.Programs.ContainsKey(rowIdx))
            {
                VHProgram vhProgram = vh.Programs[rowIdx];
                for (int i = 0; i < vhProgram.Tones.Count; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    VHTone tone = vhProgram.Tones[i];
                    // TODO: fix maximum vaue of volume (it could be greater than 127)
                    int maxmumNote = tone.MaximumNote > 127 ? 127 : tone.MaximumNote;
                    row.CreateCells(dgvTones, i, tone.Priority, tone.Mode, tone.Volume, tone.Panning, tone.CenterNote, tone.PitchShift,
                        tone.MinimumNote, maxmumNote, tone.PitchBendMinimum, tone.PitchBendMaximum,
                        tone.ADSR1.ToString("X"), tone.ADSR2.ToString("X"), tone.Wave);
                    dgvTones.Rows.Add(row);
                }
                // Calculate the width.
                int totalWidth = dgvTones.RowHeadersWidth;
                foreach (DataGridViewColumn column in dgvTones.Columns)
                {
                    totalWidth += column.Width;
                }
                int columnWidth = dgvTones.Columns[0].Width;
                totalWidth -= columnWidth * 2; // subtract the width of the hidden columns
                int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
                totalWidth += scrollbarWidth;
                dgvTones.Width = totalWidth;
            }
            else
            {
                ResetADSR();
            }

            dirty.Pop();
        }

        private void GetADSR(DataGridViewRow selectedRow)
        {
            if (frmADSR == null) return;

            ushort adsr1 = Convert.ToUInt16(selectedRow.Cells[ColToneADSR1].Value.ToString(), 16);
            ushort adsr2 = Convert.ToUInt16(selectedRow.Cells[ColToneADSR2].Value.ToString(), 16);
            ADSR envelope = PSXADSR.ComputeADSR(adsr1, adsr2);

            double attackTime = Math.Round(envelope.AttackTime, 2);
            double decayTime = Math.Round(envelope.DecayTime, 2);
            double sustainLevel = Math.Round(envelope.SustainLevel, 2);
            double sustainTime = Math.Round(envelope.SustainTime, 2);
            double releaseTime = Math.Round(envelope.ReleaseTime, 2);

            frmADSR.adsrEnvelope.Attack = attackTime;
            frmADSR.adsrEnvelope.Decay = decayTime;
            frmADSR.adsrEnvelope.SustainLevel = sustainLevel;
            frmADSR.adsrEnvelope.SustainDuration = sustainLevel;
            frmADSR.adsrEnvelope.Release = releaseTime;
            frmADSR.adsrEnvelope.Invalidate();

            string sustainTimeStr = sustainTime == -1 ? "Infinite" : $"{sustainTime}s";
            frmADSR.lbADSR.Text = $"AttackTime: {attackTime}s\nDecayTime: {decayTime}s\nSustainLevel: {sustainLevel}\nSustainTime: {sustainTimeStr}\nReleaseTime: {releaseTime}s";
        }

        private void ResetADSR()
        {
            if (frmADSR == null) return;

            frmADSR.adsrEnvelope.Attack = 0;
            frmADSR.adsrEnvelope.Decay = 1.0;
            frmADSR.adsrEnvelope.SustainLevel = 1.0;
            frmADSR.adsrEnvelope.SustainDuration = 0;
            frmADSR.adsrEnvelope.Release = 0;
            frmADSR.adsrEnvelope.Invalidate();
        }

        private void dgvTones_SelectionChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvTones, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null) return;

            dirty.Push(true);

            toneIndex = rowIdx;
            lbTone.Text = $"Tone {toneIndex}";

            trkVolume.Value = Convert.ToInt32(selectedRow.Cells[ColToneVolume].Value);
            trkPan.Value = Convert.ToInt32(selectedRow.Cells[ColTonePan].Value);
            trkCenter.Value = Convert.ToInt32(selectedRow.Cells[ColToneCenter].Value);
            trkPitch.Value = Convert.ToInt32(selectedRow.Cells[ColTonePitch].Value);
            trkMinNote.Value = Convert.ToInt32(selectedRow.Cells[ColToneMinNote].Value);
            trkMaxNote.Value = Convert.ToInt32(selectedRow.Cells[ColToneMaxNote].Value);
            trkPBmin.Value = Convert.ToInt32(selectedRow.Cells[ColTonePBmin].Value);
            trkPBmax.Value = Convert.ToInt32(selectedRow.Cells[ColTonePBmax].Value);

            numNote.Minimum = Convert.ToInt32(selectedRow.Cells[ColToneMinNote].Value);
            numNote.Maximum = Convert.ToInt32(selectedRow.Cells[ColToneMaxNote].Value);
            if ((int)numNote.Value < trkMinNote.Value)
                numNote.Value = trkMinNote.Value;
            if ((int)numNote.Value > trkMaxNote.Value)
                numNote.Value = trkMaxNote.Value;

            numVAG.Value = Convert.ToInt32(selectedRow.Cells[ColToneVAG].Value);

            if (chkAutoPlay.Checked)
                cmdPlayTone.PerformClick();

            GetADSR(selectedRow);

            dirty.Pop();
        }

        private void cmdPlayTone_Click(object sender, EventArgs e)
        {
            // Calculate the sample rate.
            int centerKey = trkCenter.Value;
            double pitch = trkPitch.Value / 100.0;
            int userNote = (int)numNote.Value;
            double semitoneDiff = userNote - centerKey;
            double pitchFactor = Math.Pow(2, (semitoneDiff + pitch) / 12.0);
            int baseSampleRate = 44100;
            int effectiveSampleRate = (int)(baseSampleRate * pitchFactor);

            // Get the sample.
            SampleSet sample = vab.Waves[(int)(numVAG.Value - 1)];
            byte[] wave = WaveConv.ToWave(sample.ToPCM(), effectiveSampleRate).Save();

            // waveOut init.
            waveOut.Stop();
            waveStream?.Dispose();
            MemoryStream memoryStream = new MemoryStream(wave);
            waveStream = new RawSourceWaveStream(memoryStream, new WaveFormat(effectiveSampleRate, 16, 1));
            var resampler = new MediaFoundationResampler(waveStream, new WaveFormat(44100, 16, 1));
            resampler.ResamplerQuality = 60;
            waveOut.Init(resampler);

            // Calculate the volume.
            float maxVolume = 0.5F;
            waveOut.Volume = (float)(trkVolume.Value / 127.0 * maxVolume);

            waveOut.Play();
        }

        private void VABTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveStream?.Dispose();
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

        private void UpdateProgramVolumeText()
        {
            lbProgramVolume.Text = $"Volume (0x{trkProgramVolume.Value.ToString("X")})";
        }

        private void UpdateProgramPanText()
        {
            lbProgramPan.Text = $"Pan (0x{trkProgramPan.Value.ToString("X")})";
        }

        private void trkProgramVolume_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvPrograms, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;
            selectedRow.Cells[ColProgVolume].Value = trkProgramVolume.Value;
            UpdateProgramVolumeText();
        }

        private void trkProgramPan_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvPrograms, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;
            selectedRow.Cells[ColProgPan].Value = trkProgramPan.Value;
            UpdateProgramPanText();
        }

        private void tonesTrackBar_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvTones, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;

            if (sender is TrackBar trackBar && trackBar.Tag is string tag)
            {
                if (tag == "Volume")
                {
                    selectedRow.Cells[ColToneVolume].Value = trkVolume.Value;
                    vh.Programs[programIndex].Tones[toneIndex].Volume = (byte)trkVolume.Value;
                }
                else if (tag == "Pan")
                {
                    selectedRow.Cells[ColTonePan].Value = trkPan.Value;
                    vh.Programs[programIndex].Tones[toneIndex].Panning = (byte)trkPan.Value;
                }
                else if (tag == "Center")
                {
                    selectedRow.Cells[ColToneCenter].Value = trkCenter.Value;
                    vh.Programs[programIndex].Tones[toneIndex].CenterNote = (byte)trkCenter.Value;
                }
                else if (tag == "Pitch")
                {
                    selectedRow.Cells[ColTonePitch].Value = trkPitch.Value;
                    vh.Programs[programIndex].Tones[toneIndex].PitchShift = (byte)trkPitch.Value;
                }
                else if (tag == "MinNote")
                {
                    if (trkMinNote.Value > trkMaxNote.Value)
                    {
                        trkMinNote.Value = trkMaxNote.Value;
                        return;
                    }
                    selectedRow.Cells[ColToneMinNote].Value = trkMinNote.Value;
                    vh.Programs[programIndex].Tones[toneIndex].MinimumNote = (byte)trkMinNote.Value;
                    numNote.Minimum = trkMinNote.Value;
                }
                else if (tag == "MaxNote")
                {
                    if (trkMaxNote.Value < trkMinNote.Value)
                    {
                        trkMaxNote.Value = trkMinNote.Value;
                        return;
                    }
                    selectedRow.Cells[ColToneMaxNote].Value = trkMaxNote.Value;
                    vh.Programs[programIndex].Tones[toneIndex].MaximumNote = (byte)trkMaxNote.Value;
                    numNote.Maximum = trkMaxNote.Value;
                }
                else if (tag == "PBmin")
                {
                    if (trkPBmin.Value > trkPBmax.Value)
                    {
                        trkPBmin.Value = trkPBmax.Value;
                        return;
                    }
                    selectedRow.Cells[ColTonePBmin].Value = trkPBmin.Value;
                    vh.Programs[programIndex].Tones[toneIndex].PitchBendMinimum = (byte)trkPBmin.Value;
                }
                else if (tag == "PBmax")
                {
                    if (trkPBmax.Value < trkPBmin.Value)
                    {
                        trkPBmax.Value = trkPBmin.Value;
                        return;
                    }
                    selectedRow.Cells[ColTonePBmax].Value = trkPBmax.Value;
                    vh.Programs[programIndex].Tones[toneIndex].PitchBendMaximum = (byte)trkPBmax.Value;
                }
            }
        }

        private void numVAG_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvTones, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;
            selectedRow.Cells[ColToneVAG].Value = numVAG.Value;
            vh.Programs[programIndex].Tones[toneIndex].Wave = (short)numVAG.Value;
        }

        private void numPriority_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvTones, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;
            selectedRow.Cells[ColTonePriority].Value = numPriority.Value;
            vh.Programs[programIndex].Tones[toneIndex].Priority = (byte)numPriority.Value;
        }

        private void numMode_ValueChanged(object sender, EventArgs e)
        {
            GetSelectedRow(dgvTones, out int rowIdx, out DataGridViewRow? selectedRow);
            if (selectedRow == null || Dirty) return;
            selectedRow.Cells[ColToneMode].Value = numMode.Value;
            vh.Programs[programIndex].Tones[toneIndex].Mode = (byte)numMode.Value;
        }

        private void numNote_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (chkAutoPlay.Checked)
                cmdPlayTone.PerformClick();
        }

        private void CalculateHeader(DataGridViewRow selectedHeadRow)
        {
            // Calculate the tone count.
            int tonecount = 0;
            foreach (VHProgram program in vh.Programs.Values)
            {
                tonecount += program.Tones.Count;
            }
            // Update the header.
            int vbSize = vh.VBSize * 16;
            selectedHeadRow.Cells[ColHeadTotalSize].Value = vh.Size;
            selectedHeadRow.Cells[ColHeadVHSize].Value = vh.Size - vbSize;
            selectedHeadRow.Cells[ColHeadPrograms].Value = vh.Programs.Count;
            selectedHeadRow.Cells[ColHeadTones].Value = tonecount;
        }

        private byte[] CreateNullProgram()
        {
            byte[] bytes = new byte[16];
            bytes[0] = 0;
            bytes[1] = 0x7F;
            bytes[2] = 0xFF;
            bytes[3] = 0xFF;
            bytes[4] = 0x40;
            bytes[5] = 0xFF;
            BitConv.ToInt16(bytes, 6, 0);
            BitConv.ToInt32(bytes, 8, -1);
            BitConv.ToInt32(bytes, 12, -1);
            return bytes;
        }

        private byte[] CreateNullTone(ushort programindex, short sample)
        {
            byte[] bytes = new byte[32];
            bytes[0] = 0;
            bytes[1] = 0;
            bytes[2] = 0x7F;
            bytes[3] = 0x40;
            bytes[4] = 0x40;
            bytes[5] = 0;
            bytes[6] = 0;
            bytes[7] = 0x7F;
            bytes[8] = 0;
            bytes[9] = 0;
            bytes[10] = 0;
            bytes[11] = 0;
            bytes[12] = 0;
            bytes[13] = 0;
            bytes[14] = 0xB1;
            bytes[15] = 0xB2;
            BitConv.ToUInt16(bytes, 16, 0x80DF);
            BitConv.ToUInt16(bytes, 18, 0x5FDF);
            BitConv.ToUInt16(bytes, 20, programindex);  // Parent program
            BitConv.ToInt16(bytes, 22, sample);         // Sample number
            BitConv.ToUInt16(bytes, 24, 0xC0);
            BitConv.ToUInt16(bytes, 26, 0xC1);
            BitConv.ToUInt16(bytes, 28, 0xC2);
            BitConv.ToUInt16(bytes, 30, 0xC3);
            return bytes;
        }

        private void cmdAppendProgram_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.Rows.Count > 255) return;

            GetSelectedRow(dgvHeader, out int rowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;

            ushort programindex = (ushort)(vh.Programs.Count + vh.NullPrograms.Count);

            // Create a new program.
            byte[] programdata = CreateNullProgram();

            // Create a new tone attributes table.
            byte[] nulltonedata = new byte[32 * 16];
            for (int i = 0; i < 16; ++i)
            {
                byte[] bytes = CreateNullTone(programindex, 0);
                Array.Copy(bytes, 0, nulltonedata, i * 32, 32);
            }

            // Add the program to NullPrograms.
            vh.NullPrograms.Add(programindex, VHProgram.Load(programdata, nulltonedata, vh.IsOldVersion));
            vh.Size += InstrumentSize;

            // Create a row.
            dgvPrograms.Rows.Add(programindex, 0, 127, 64);
            // Update the header.
            CalculateHeader(selectedHeadRow);
        }

        private void cmdInsertProgram_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.Rows.Count > 255) return;

            GetSelectedRow(dgvHeader, out int headRowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;
            GetSelectedRow(dgvPrograms, out int progRowIdx, out DataGridViewRow? selectedProgRow);
            if (selectedProgRow == null) return;

            if (Convert.ToInt32(selectedProgRow.Cells[ColProgToneCount].Value) == 0) return;

            VHProgram newProgram = vh.Programs[programIndex];

            // Shift the program indexes.
            var keysToShift = new List<int>();
            foreach (var key in vh.Programs.Keys)
            {
                if (key >= programIndex)
                    keysToShift.Add(key);
            }
            keysToShift.Sort((a, b) => b.CompareTo(a));
            foreach (var oldKey in keysToShift)
            {
                var value = vh.Programs[oldKey];
                vh.Programs.Remove(oldKey);
                vh.Programs[oldKey + 1] = value;
            }

            // Insert the program.
            vh.Programs[programIndex] = newProgram;

            // Create a row.
            dgvPrograms.Rows.Insert(programIndex, programIndex, newProgram.Tones.Count, newProgram.Volume, newProgram.Panning);
            // Update the header.
            CalculateHeader(selectedHeadRow);

            // Update the program numbers.
            for (int i = 0; i < dgvPrograms.Rows.Count; i++)
            {
                dgvPrograms.Rows[i].Cells[ColProgProgramNumber].Value = i;
            }
        }

        private void cmdDeleteProgram_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.Rows.Count <= 1) return;

            GetSelectedRow(dgvHeader, out int headRowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;
            GetSelectedRow(dgvPrograms, out int progRowIdx, out DataGridViewRow? selectedProgRow);
            if (selectedProgRow == null) return;

            if (vh.Programs.ContainsKey(progRowIdx))
            {
                // Remove the program from Programs.
                vh.Programs.Remove(progRowIdx);
                vh.Size -= InstrumentSize;
            }
            else if (vh.NullPrograms.ContainsKey(progRowIdx))
            {
                // Remove the program from NullPrograms.
                vh.NullPrograms.Remove(progRowIdx);
            }
            else throw new Exception("Pograms do not contain the key.");

            // Remove the row.
            dgvPrograms.Rows.RemoveAt(progRowIdx);
            // Update the header.
            CalculateHeader(selectedHeadRow);

            // Shift the program indexes.
            var keysToShift = new List<int>();
            foreach (var key in vh.Programs.Keys)
            {
                if (key > progRowIdx)
                    keysToShift.Add(key);
            }

            foreach (var oldKey in keysToShift)
            {
                var value = vh.Programs[oldKey];
                vh.Programs.Remove(oldKey);
                vh.Programs[oldKey - 1] = value;
            }

            // Update the program numbers.
            for (int i = 0; i < dgvPrograms.Rows.Count; i++)
            {
                dgvPrograms.Rows[i].Cells[ColProgProgramNumber].Value = i;
            }
        }

        private void cmdAppendTone_Click(object sender, EventArgs e)
        {
            if (dgvTones.Rows.Count >= 16)
            {
                DarkMessageBox.ShowError("The maximum number of tones is 16.", "VAB Tool");
                return;
            }

            GetSelectedRow(dgvHeader, out int headRowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;
            GetSelectedRow(dgvPrograms, out int progRowIdx, out DataGridViewRow? selectedProgRow);
            if (selectedProgRow == null) return;

            // If the program was in NullPrograms, move it to Programs.
            if (Convert.ToInt32(selectedProgRow.Cells[ColProgToneCount].Value) == 0)
            {
                vh.Programs.Add(progRowIdx, vh.NullPrograms[progRowIdx]);
                vh.NullPrograms.Remove(progRowIdx);
                // Upddate the program count in the header.
                selectedHeadRow.Cells[ColHeadPrograms].Value = vh.Programs.Count;
            }

            // Add a new tone.
            byte[] bytes = CreateNullTone((ushort)progRowIdx, 1);
            vh.Programs[progRowIdx].Tones.Add(VHTone.Load(bytes));

            // Create a row.
            dgvTones.Rows.Add(dgvTones.Rows.Count, 0, 0, 127, 64, 64, 0, 0, 127, 0, 0, 0x80FF.ToString("X"), 0x5FDF.ToString("X"), 1);
            // Update the tone count in the program.
            selectedProgRow.Cells[ColProgToneCount].Value = dgvTones.Rows.Count;
            // Update the header.
            CalculateHeader(selectedHeadRow);
        }

        private void cmdInsertTone_Click(object sender, EventArgs e)
        {
            if (dgvTones.Rows.Count == 0) return;
            if (dgvTones.Rows.Count >= 16)
            {
                DarkMessageBox.ShowError("The maximum number of tones is 16.", "VAB Tool");
                return;
            }

            GetSelectedRow(dgvHeader, out int headRowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;
            GetSelectedRow(dgvPrograms, out int progRowIdx, out DataGridViewRow? selectedProgRow);
            if (selectedProgRow == null) return;

            // Insert a new tone and update the tone count.
            vh.Programs[progRowIdx].Tones.Insert(toneIndex, vh.Programs[progRowIdx].Tones[toneIndex]);

            // Insert a row.
            DataGridViewRow clonedRow = (DataGridViewRow)dgvTones.Rows[toneIndex].Clone();
            for (int i = 0; i < dgvTones.Rows[toneIndex].Cells.Count; i++)
            {
                clonedRow.Cells[i].Value = dgvTones.Rows[toneIndex].Cells[i].Value;
            }
            dgvTones.Rows.Insert(toneIndex, clonedRow);

            // Update the tone count in the program.
            selectedProgRow.Cells[ColProgToneCount].Value = dgvTones.Rows.Count;
            // Update the header.
            CalculateHeader(selectedHeadRow);
        }

        private void cmdDeleteTone_Click(object sender, EventArgs e)
        {
            if (dgvTones.Rows.Count == 0) return;

            GetSelectedRow(dgvHeader, out int headRowIdx, out DataGridViewRow? selectedHeadRow);
            if (selectedHeadRow == null) return;
            GetSelectedRow(dgvPrograms, out int progRowIdx, out DataGridViewRow? selectedProgRow);
            if (selectedProgRow == null) return;
            GetSelectedRow(dgvTones, out int toneRoeIdx, out DataGridViewRow? selectedToneRow);
            if (selectedToneRow == null) return;

            // Remove the tone from the program.
            vh.Programs[progRowIdx].Tones.RemoveAt(toneRoeIdx);

            // If the program has no tones, move it to NullPrograms.
            if (vh.Programs[progRowIdx].Tones.Count == 0)
            {
                vh.NullPrograms.Add(progRowIdx, vh.Programs[progRowIdx]);
                vh.Programs.Remove(progRowIdx);
                // Update the program count in the header.
                selectedHeadRow.Cells[ColHeadPrograms].Value = vh.Programs.Count;
            }

            // Remove the row.
            dgvTones.Rows.Remove(selectedToneRow);
            // Update the tone count in the program.
            selectedProgRow.Cells[ColProgToneCount].Value = dgvTones.Rows.Count;
            // Update the header.
            CalculateHeader(selectedHeadRow);
        }

        private void cmdADSR_Click(object sender, EventArgs e)
        {
            if (frmADSR == null || frmADSR.IsDisposed)
            {
                frmADSR = new ADSRForm(this);
                frmADSR.FormClosed += (s, e) => frmADSR = null;
            }

            if (!frmADSR.Visible)
                frmADSR.Show();
            else
                frmADSR.Activate();
        }

        private void cmdViewVAG_Click(object sender, EventArgs e)
        {
            if (frmVAGList == null || frmVAGList.IsDisposed)
            {
                frmVAGList = new VAGListForm(this);
                frmVAGList.FormClosed += (s, e) => frmVAGList = null;
            }

            if (!frmVAGList.Visible)
                frmVAGList.Show();
            else
                frmVAGList.Activate();
        }

        private void cmdPreviewVAB_Click(object sender, EventArgs e)
        {
            if (frmMidiForm == null || frmMidiForm.IsDisposed)
            {
                frmMidiForm = new MidiForm(this);
                frmMidiForm.FormClosed += (s, e) => frmMidiForm = null;
            }

            if (!frmMidiForm.Visible)
                frmMidiForm.Show();
            else
                frmMidiForm.Activate();
        }


    }

    public class ADSRForm : DarkForm
    {
        private VABTool vabTool;

        public ADSREnvelope adsrEnvelope;
        public Label lbADSR;

        public ADSRForm(VABTool vabTool)
        {
            this.vabTool = vabTool;
            MainInit();
        }

        private void MainInit()
        {
            Text = "ADSR Settings";
            Size = new Size(660, 520);
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                AutoSize = true
            };

            adsrEnvelope = new ADSREnvelope
            {
                Location = new Point(0, 0),
                Size = new Size(300, 150),
                Margin = new Padding(3),
            };
            ResetADSR();

            lbADSR = new Label
            {
                Text = "AttackTime: 0.00s\r\nDecayTime: 0.00s\r\nSustainLevel: 0.00s\r\nSustainTime: 0.00s\r\nReleaseTime: 0.00s",
                AutoSize = true,
                ForeColor = SystemColors.MenuText
            };

            layout.Controls.Add(adsrEnvelope, 0, 0);
            layout.Controls.Add(lbADSR, 1, 0);
            Controls.Add(layout);
        }


        private void ResetADSR()
        {
            adsrEnvelope.Attack = 0;
            adsrEnvelope.Decay = 1.0;
            adsrEnvelope.SustainLevel = 1.0;
            adsrEnvelope.SustainDuration = 0;
            adsrEnvelope.Release = 0;
            adsrEnvelope.Invalidate();
        }
    }


    public class VAGListForm : DarkForm
    {
        private VABTool vabTool;

        private DataGridView dgvVAG;
        private DarkTextBox txtVAG;
        private Label lbVAGCount;


        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public VAGListForm(VABTool vabTool)
        {
            this.vabTool = vabTool;
            MainInit();
        }

        private void MainInit()
        {
            Text = "VAG Data";
            Size = new Size(660, 520);
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };

            int height = this.Height - 44;
            dgvVAG = new DataGridView
            {
                Height = height,
                AllowUserToAddRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ColumnHeadersHeight = 24,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersWidth = 24,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            DoubleBufferedDataGridView.Initialize(dgvVAG);
            dgvVAG.Columns.Add("Number", "Number");
            dgvVAG.Columns.Add("Offset", "Offset");
            dgvVAG.Columns.Add("Size", "Size");
            dgvVAG.Columns.Add("Name", "Name");
            foreach (DataGridViewColumn column in dgvVAG.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 64;
            }
            dgvVAG.Columns[3].Width = 128;

            // Calculate the width.
            int totalWidth = dgvVAG.RowHeadersWidth;
            foreach (DataGridViewColumn column in dgvVAG.Columns)
            {
                totalWidth += column.Width;
            }
            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
            totalWidth += scrollbarWidth;
            dgvVAG.Width = totalWidth;

            // Create the rows.
            int index = 1;
            int offset = 0;
            foreach (SampleSet wave in vabTool.vab.Waves)
            {
                int size = wave.SampleLines.Count * 16;
                dgvVAG.Rows.Add(index, offset, size, string.Empty);
                offset += size;
                index++;
            }

            dgvVAG.SelectionChanged += new EventHandler(dgvVAG_SelectionChanged);

            // main layout for the right side
            FlowLayoutPanel flowLayout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            // info
            DarkGroupBox fraInfo = new DarkGroupBox
            {
                Text = "Info",
                AutoSize = true,
                MinimumSize = new Size(200, 0)
            };
            FlowLayoutPanel flpInfo = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true
            };
            lbVAGCount = new Label
            {
                ForeColor = SystemColors.MenuText,
                AutoSize = true,
                Margin = new Padding(3, 3, 42, 18)
            };
            UpdateInfo();

            // vag group box
            DarkGroupBox fraVAG = new DarkGroupBox
            {
                Text = "VAG",
                AutoSize = true,
                MinimumSize = new Size(200, 0)
            };
            FlowLayoutPanel flpVAG = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true
            };
            Label lbVAG = new Label
            {
                Text = "VAG Name",
                ForeColor = SystemColors.MenuText,
                AutoSize = true,
                Margin = new Padding(3, 3, 42, 3)
            };
            txtVAG = new DarkTextBox
            {
                Width = 200,
                Height = 26
            };
            txtVAG.TextChanged += new EventHandler(txtVAG_TextChanged);
            DarkButton cmdAdd = new DarkButton
            {
                Text = "Add",
                Height = 26,
                Margin = new Padding(3, 24, 3, 3)
            };
            cmdAdd.Click += new EventHandler(cmdAdd_Click);
            DarkButton cmdDelete = new DarkButton
            {
                Text = "Delete Last",
                Height = 26
            };
            cmdDelete.Click += new EventHandler(cmdDelete_Click);
            DarkButton cmdReplace = new DarkButton
            {
                Text = "Replace",
                Height = 26,
                Margin = new Padding(3, 24, 3, 12)
            };

            flpVAG.Controls.Add(lbVAG);
            flpVAG.Controls.Add(txtVAG);
            flpVAG.Controls.Add(cmdAdd);
            flpVAG.Controls.Add(cmdDelete);
            flpVAG.Controls.Add(cmdReplace);
            fraVAG.Controls.Add(flpVAG);

            flpInfo.Controls.Add(lbVAGCount);
            fraInfo.Controls.Add(flpInfo);

            flowLayout.Controls.Add(fraInfo);
            flowLayout.Controls.Add(fraVAG);

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));

            layout.Controls.Add(dgvVAG, 0, 0);
            layout.Controls.Add(flowLayout, 1, 0);
            Controls.Add(layout);
        }

        private void dgvVAG_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVAG.SelectedCells.Count == 0) return;
            dirty.Push(true);
            int rowIdx = dgvVAG.SelectedCells[0].RowIndex;
            txtVAG.Text = dgvVAG.Rows[rowIdx].Cells[3].Value.ToString();
            dirty.Pop();
        }

        private void txtVAG_TextChanged(object sender, EventArgs e)
        {
            if (Dirty || dgvVAG.SelectedCells.Count == 0) return;
            int rowIdx = dgvVAG.SelectedCells[0].RowIndex;
            dgvVAG.Rows[rowIdx].Cells[3].Value = txtVAG.Text;
        }

        private void UpdateInfo()
        {
            int vbSize = vabTool.vh.VBSize * 16;
            lbVAGCount.Text = $"VAG Count: {vabTool.vab.Waves.Count}\nTotal VAG (VB) File Size: {vbSize}\nFree Space Left: {VABTool.MaxVBSize - vbSize}";
        }

        private void cmdAdd_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = FileFilters.VAG + "|" + FileFilters.Any;
                dialog.Multiselect = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    List<byte[]> files = new();
                    foreach (string filename in dialog.FileNames)
                    {
                        byte[] file = File.ReadAllBytes(filename);
                        SampleSet wave = SampleSet.Load(file);
                        int vagSize = wave.SampleLines.Count * 16;
                        if (vagSize > VABTool.MaxVBSize - vabTool.vh.VBSize * 16)
                        {
                            DarkMessageBox.ShowError("Not enough free space left.", "VAB Tool");
                            return;
                        }
                        vabTool.vab.Waves.Add(wave);
                        vabTool.vh.Waves.Add(wave.SampleLines.Count);
                        vabTool.vh.VBSize += wave.SampleLines.Count;

                        int lastRow = dgvVAG.Rows.Count - 1;
                        int offset = Convert.ToInt32(dgvVAG.Rows[lastRow].Cells[1].Value) + Convert.ToInt32(dgvVAG.Rows[lastRow].Cells[2].Value);
                        dgvVAG.Rows.Add(vabTool.vab.Waves.Count, offset, vagSize, Path.GetFileName(filename));
                        UpdateInfo();
                    }

                }
            }
        }

        private void cmdDelete_Click(object? sender, EventArgs e)
        {
            if (dgvVAG.Rows.Count <= 1) return;
            int lastRow = dgvVAG.Rows.Count - 1;
            int offset = Convert.ToInt32(dgvVAG.Rows[lastRow].Cells[1].Value);
            int size = Convert.ToInt32(dgvVAG.Rows[lastRow].Cells[2].Value);
            vabTool.vab.Waves.RemoveAt(lastRow);
            vabTool.vh.Waves.RemoveAt(lastRow);
            vabTool.vh.VBSize -= size / 16;
            dgvVAG.Rows.RemoveAt(lastRow);
            UpdateInfo();
        }


    }

    public class ADSREnvelope : Control
    {
        public double Attack { get; set; }
        public double Decay { get; set; }
        public double SustainLevel { get; set; }
        public double Release { get; set; }

        // For sustain time visualization
        public double SustainDuration { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGridAndAxes(e.Graphics, ClientRectangle);
            DrawEnvelope(e.Graphics, ClientRectangle);
        }

        private void DrawGridAndAxes(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw dotted grids
            using (Pen gridPen = new Pen(Color.Gray, 1))
            {
                gridPen.DashStyle = DashStyle.Dash;

                // Horizontal grid lines: Amplitude
                int horizontalDivisions = 4;
                for (int i = 0; i <= horizontalDivisions; i++)
                {
                    float amplitude = i / (float)horizontalDivisions;
                    float y = rect.Bottom - amplitude * rect.Height;
                    g.DrawLine(gridPen, rect.Left, y, rect.Right, y);

                    // Draw amplitude labels at the left
                    string label = i == 0 ? "0" : amplitude.ToString("0.00");
                    using (Font labelFont = new Font("Arial", 8))
                    {
                        SizeF labelSize = g.MeasureString(label, labelFont);
                        g.DrawString(label, labelFont, Brushes.Gainsboro, rect.Left, y - labelSize.Height);
                    }
                }

                // Vertical grid lines: Time
                double totalTime = Attack + Decay + SustainDuration + Release;
                int verticalDivisions = 5;
                for (int i = 0; i <= verticalDivisions; i++)
                {
                    if (i == 0) continue;

                    double timeValue = i * totalTime / verticalDivisions;
                    float x = (float)(rect.Left + (timeValue / totalTime) * rect.Width);
                    g.DrawLine(gridPen, x, rect.Top, x, rect.Bottom);

                    // Draw time labels at the bottom
                    string label = timeValue.ToString("0.00") + "s";
                    using (Font labelFont = new Font("Arial", 8))
                    {
                        SizeF labelSize = g.MeasureString(label, labelFont);
                        g.DrawString(label, labelFont, Brushes.Gainsboro, x - labelSize.Width / 2, rect.Bottom - labelSize.Height);
                    }
                }
            }

            // Draw box
            using (Pen axisPen = new Pen(Color.Gray, 1))
            {
                g.DrawRectangle(axisPen, rect.Left, rect.Top, rect.Right - 1, rect.Bottom - 1);
            }
        }

        private void DrawEnvelope(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen envelopePen = new Pen(SystemColors.Highlight, 2))
            {
                double totalTime = Attack + Decay + SustainDuration + Release;
                PointF[] points = new PointF[5];

                Func<double, float> mapX = t => (float)(rect.Left + (t / totalTime) * rect.Width);
                Func<double, float> mapY = amplitude => (float)(rect.Bottom - amplitude * rect.Height);

                points[0] = new PointF(mapX(0), mapY(0));                                           // Start
                points[1] = new PointF(mapX(Attack), mapY(1.0));                                    // Attack end
                points[2] = new PointF(mapX(Attack + Decay), mapY(SustainLevel));                   // Decay end
                points[3] = new PointF(mapX(Attack + Decay + SustainDuration), mapY(SustainLevel)); // Sustain duration
                points[4] = new PointF(mapX(totalTime), mapY(0));                                   // Release end

                g.DrawLines(envelopePen, points);
            }
        }
    }

}
