using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using Color = System.Drawing.Color;
using HslColor = Cyotek.Windows.Forms.HslColor;
using Timer = System.Windows.Forms.Timer;

namespace CrashEdit.CE.Controls
{
    public partial class CLUTBox : UserControl
    {
        private TextureChunk chunk;

        private bool globalControlMode;
        private int editMode;
        private int modeCLUT = 0;
        private int modeSelectedCells = 1;

        private int editStartRow;
        private int editEndRow;

        private Rectangle animRect;
        private int animStep = 0;
        private int animDirection = 1;
        private Timer animTimer;

        private double MasterHue => colorEditorGlobal.HslColor.H;
        private double MasterSaturation => colorEditorGlobal.HslColor.S;
        private double MasterLightness => colorEditorGlobal.HslColor.L;

        internal bool dirty;

        public CLUTBox(TextureChunk texturechunk)
        {
            chunk = texturechunk;
        }

        public void OnTabSelected()
        {
            CLUTBox_Enter(this, EventArgs.Empty);
        }

        private void CLUTBox_Enter(object sender, EventArgs e)
        {
            InitializeComponent();
            DoubleBuffered = true;

            DoubleBufferedDataGridView.Initialize(dgvCLUT);
            ResetColorSliders();

            globalControlMode = false;
            editMode = 0;

            numClutX1.Enabled =
            numClutX2.Enabled =
            numClutY1.Enabled =
            numClutY2.Enabled = false;
            numClutX1.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numClutX2.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numClutY1.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numClutY2.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);
            numLoadClut.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

            // Animation Timer
            animTimer = new Timer();
            animTimer.Interval = 30;
            animTimer.Tick += AnimTimer_Tick;
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            animStep += animDirection;
            if (animStep >= 10) animDirection = -1;
            if (animStep <= 0) animDirection = 1;

            dgvCLUT.Invalidate(animRect);
        }

        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
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

        private void dgvCLUT_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                var cell = dgvCLUT[e.ColumnIndex, e.RowIndex];
                bool isSelected = cell.Selected;
                bool isCurrent = (dgvCLUT.CurrentCell != null &&
                                  dgvCLUT.CurrentCell.RowIndex == e.RowIndex &&
                                  dgvCLUT.CurrentCell.ColumnIndex == e.ColumnIndex);
                var tags = cell.Tag as List<object>;

                e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);

                if (isSelected)
                {
                    if (isCurrent)
                    {
                        Rectangle cellRect = dgvCLUT.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        int thickness = 1 + animStep / 2;
                        int alpha = 100 + animStep * 15;
                        alpha = Math.Min(alpha, 255);

                        using Pen p = new(Color.FromArgb(alpha, Color.White), thickness);
                        Rectangle r = e.CellBounds;
                        r.Width -= 1;
                        r.Height -= 1;
                        e.Graphics.DrawRectangle(p, r);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(Pens.Gainsboro, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);
                    }
                }
                else if (chkHighlightSTPbit.Checked & (int)tags[1] == 1)
                {
                    e.Graphics.DrawRectangle(Pens.Turquoise, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);
                }
                else
                {
                    return;
                }

                e.Handled = true;
            }
        }

        private async Task UpdateCLUTList()
        {
            dgvCLUT.SuspendLayout();

            dgvCLUT.ClearSelection();
            numClutX1.Value =
            numClutX2.Value =
            numClutY1.Value =
            numClutY2.Value = 0;

            dgvCLUT.Columns.Clear();
            dgvCLUT.Columns.Add($"CLUT", $"CLUT");
            for (int i = 0; i < 16; i++)
            {
                dgvCLUT.Columns.Add($"Color{i + 1}", $"{i + 1}");
            }
            foreach (DataGridViewColumn column in dgvCLUT.Columns)
            {
                column.Width = 25;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvCLUT.Columns[0].Width = 75;

            byte[] data = chunk.Data;
            List<byte[]> cluts = GetCLUT(data, 0x20, (int)numLoadClut.Value);

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();
                Parallel.For(0, cluts.Count, i =>
                {
                    var clut = cluts[i];
                    var row = new DataGridViewRow();

                    while (row.Cells.Count < 16 + 1)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell());
                    }

                    if (i % 16 == 0)
                    {
                        row.Cells[0].Style.ForeColor = Color.Turquoise;
                    }
                    else
                    {
                        row.Cells[0].Style.ForeColor = Color.Gainsboro;
                    }
                    row.Cells[0].Value = $"Y{i / 16}, X{i % 16}";

                    for (int j = 0; j < 16; j++)
                    {
                        if (j * 2 + 1 < clut.Length)
                        {
                            ushort colorValue = BitConverter.ToUInt16(clut, j * 2);
                            int r = (colorValue & 0x1F) << 3;
                            int g = ((colorValue >> 5) & 0x1F) << 3;
                            int b = ((colorValue >> 10) & 0x1F) << 3;
                            int a = ((colorValue >> 15) & 0x1);
                            Color color = Color.FromArgb(255, r, g, b);

                            row.Cells[j + 1].Style.ForeColor = Color.Transparent;
                            row.Cells[j + 1].Style.BackColor = color;
                            row.Cells[j + 1].Value = ColorTranslator.ToHtml(color);

                            List<object> tags = new List<object> { (j * 2) + (i * 32), a };
                            row.Cells[j + 1].Tag = tags;
                        }
                    }

                    rowsToAdd.Add((i, row));
                });

                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });

            dgvCLUT.Rows.AddRange(rows.ToArray());
            dgvCLUT.ResumeLayout();

            fraSlider.Enabled =
            fraGlobalControl.Enabled = true;
        }

        private async void cmdLoadCLUT_Click(object sender, EventArgs e)
        {
            await UpdateCLUTList();
        }

        private void dgvCLUT_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            //{
            //    var tagValue = dgvCLUT.SelectedCells[0].Tag?.ToString();
            //    if (!string.IsNullOrEmpty(tagValue))
            //    {
            //        Clipboard.SetDataObject(tagValue, true, 10, 100);
            //    }
            //}

            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (dgvCLUT.SelectedCells.Count == 0) return;
                string clipboardText = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(clipboardText)) return;

                MatchCollection matches = Regex.Matches(clipboardText, @"#?[0-9A-Fa-f]{6}\b");
                List<string> hexColors = matches.Cast<Match>().Select(m => m.Value).ToList();
                if (hexColors.Count == 0) return;

                List<List<string>> colorRows = new List<List<string>>();
                for (int i = 0; i < hexColors.Count; i += 16)
                {
                    List<string> group = hexColors.Skip(i).Take(16).ToList();
                    colorRows.Add(group);
                }

                int startRow = dgvCLUT.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Min(c => c.RowIndex);
                if (startRow == 0)
                {
                    DarkMessageBox.ShowError("Cannot paste colors into header row.", "Error");
                    return;
                }

                for (int r = 0; r < colorRows.Count && r < dgvCLUT.SelectedRows.Count; r++)
                {
                    int targetRow = startRow + r;
                    List<string> rowColors = colorRows[r];

                    for (int c = 0; c < rowColors.Count && c < dgvCLUT.Columns.Count; c++)
                    {
                        string hex = rowColors[c];
                        try
                        {
                            var cell = dgvCLUT[c + 1, targetRow];
                            Color color = HexToColor(hex);
                            cell.Style.BackColor = color;
                            cell.Value = hex;

                            var tags = cell.Tag as List<object>;
                            ushort rgba5551 = GetRGBA5551(color.B, color.G, color.R, Convert.ToByte(tags[1]));
                            byte[] convertedPalette = BitConverter.GetBytes(rgba5551);
                            int offset = (int)tags[0];
                            Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
                        }
                        catch (FormatException)
                        {
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Z)
            {
                if (tglGlobalControl.Switched)
                    tglGlobalControl.Switched = false;
                else
                    tglGlobalControl.Switched = true;
            }
        }

        private Color HexToColor(string hex)
        {
            hex = hex.Trim();
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length == 6)
            {
                int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(r, g, b);
            }
            else
            {
                throw new FormatException("Invalid hex color format.");
            }
        }


        private static List<byte[]> GetCLUT(byte[] source, int clutsize, int count)
        {
            List<byte[]> clut = new List<byte[]>();

            for (int i = 0; i < count * 0x200; i += clutsize)
            {
                byte[] chunk = new byte[clutsize];
                Array.Copy(source, i, chunk, 0, clutsize);
                clut.Add(chunk);
            }
            return clut;
        }

        private Color GetColor(Color itemColor)
        {
            HslColor hslColor = new HslColor(itemColor);

            hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue));
            hslColor.S += (double)(MasterSaturation - 0.5) / 1.0;
            hslColor.L += (double)(MasterLightness - 0.5) / 1.0;

            Color newColor = hslColor.ToRgbColor();
            return newColor;
        }

        private void UpdateMultipleCells(bool isUpdatingColor)
        {
            UpdateMultipleCells(true, -1);
        }

        private void UpdateMultipleCells(bool isUpdatingColor, int STPbit)
        {
            if (!globalControlMode) return;

            dgvCLUT.SuspendLayout();

            if (editMode == modeCLUT)
            {
                int startRow = (int)numClutX1.Value + (int)numClutY1.Value * 16;
                int endRow = (int)numClutX2.Value + (int)numClutY2.Value * 16;

                if (startRow == 0) startRow++;

                if (startRow <= editStartRow)
                    editStartRow = startRow;
                if (endRow >= editEndRow)
                    editEndRow = endRow;

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = 1; col <= 16; col++)
                    {
                        var cell = dgvCLUT.Rows[row].Cells[col];
                        UpdateColor(cell, STPbit);
                    }
                }
            }
            else
            {
                foreach (DataGridViewCell cell in dgvCLUT.SelectedCells)
                {
                    if (cell.RowIndex > 0 && cell.ColumnIndex > 0)
                    {
                        if (cell.RowIndex <= editStartRow)
                            editStartRow = cell.RowIndex;
                        if (cell.RowIndex >= editEndRow)
                            editEndRow = cell.RowIndex;

                        UpdateColor(cell, STPbit);
                    }
                }
            }
            dgvCLUT.ResumeLayout();
            dgvCLUT.Refresh();
        }

        private void UpdateColor(DataGridViewCell cell, int STPbit)
        {
            var tags = cell.Tag as List<object>;
            Color currentColor = LoadColorFromValue(cell);
            Color newColor = GetColor(currentColor);

            ushort rgba5551;
            if (STPbit >= 0) // set STPbit
            {
                tags[1] = STPbit;
                rgba5551 = GetRGBA5551(currentColor.B, currentColor.G, currentColor.R, Convert.ToByte(tags[1]));
            }
            else // change color
            {
                rgba5551 = GetRGBA5551(newColor.B, newColor.G, newColor.R, Convert.ToByte(tags[1]));
                cell.Style.BackColor = newColor;
            }

            byte[] convertedPalette = BitConverter.GetBytes(rgba5551);
            int offset = (int)tags[0];
            Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
        }

        private void UpdateSelectedColor(Color color)
        {
            var cell = dgvCLUT.SelectedCells;
            if (cell.Count > 0 && cell[0].ColumnIndex > 0 && cell[0].RowIndex > 0)
            {
                colorEditor.Enabled = true;
                cell[0].Style.BackColor = color;
                var tags = cell[0].Tag as List<object>;

                ushort rgba5551 = GetRGBA5551(color.B, color.G, color.R, Convert.ToByte(tags[1]));
                byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                int offset = (int)tags[0];
                Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                cell[0].Value = ColorTranslator.ToHtml(color);
            }
            else
            {
                colorEditor.Enabled = false;
                return;
            }
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (!globalControlMode)
                UpdateSelectedColor(colorEditor.Color);
        }

        private void colorEditorGlobal_ColorChanged(object sender, EventArgs e)
        {
            if (!dirty)
                UpdateMultipleCells(true);
            dirty = false;
        }

        private void ApplyChanges()
        {
            if (editStartRow == dgvCLUT.RowCount - 1 && editEndRow == 0) return;

            dgvCLUT.SuspendLayout();
            for (int row = editStartRow; row <= editEndRow; row++)
            {
                for (int col = 1; col <= 16; col++)
                {
                    var cell = dgvCLUT.Rows[row].Cells[col];

                    Color color = cell.Style.BackColor;
                    cell.Value = ColorTranslator.ToHtml(color);
                }
            }
            dgvCLUT.ResumeLayout();
            if (Settings.Default.OutputCLUTInfo)
            {
                int startX = editStartRow % 16;
                int startY = editStartRow / 16;
                int endX = editEndRow % 16;
                int endY = editEndRow / 16;
                Console.WriteLine($"Start [X{startX}, Y{startY}] End [X{endX}, Y{endY}]");
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            tglGlobalControl.Switched = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tglGlobalControl.Switched = false;
        }

        private void CLUTBox_Leave(object sender, EventArgs e)
        {
            //if (globalControlMode)
            //{
            //    if (DarkMessageBox.ShowWarning("The changes have not been saved. Do you want to apply them?", "Global Controller", DarkDialogButton.YesNo) == DialogResult.Yes)
            //    {
            //        ApplyChanges();
            //        tglGlobalControl.Switched = false;
            //    }
            //    else
            //    {
            //        tglGlobalControl.Switched = false;
            //    }
            //}
        }

        private void dgvCLUT_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCLUT.SelectedCells.Count > 0)
            {
                // Start animation
                int rowIndex = dgvCLUT.SelectedCells[0].RowIndex;
                int columnIndex = dgvCLUT.SelectedCells[0].ColumnIndex;
                animRect = dgvCLUT.GetCellDisplayRectangle(columnIndex, rowIndex, true);
                animStep = 0;
                animDirection = 1;
                animTimer.Start();
                dgvCLUT.Invalidate();

                var cell = dgvCLUT.SelectedCells[0];
                if (cell.RowIndex > 0 && cell.ColumnIndex > 0)
                {
                    var tags = cell.Tag as List<object>;

                    chkSTPbit.Enabled = true;
                    chkSTPbit.Checked = (int)tags[1] == 0 ? false : true;

                    if (Settings.Default.OutputCLUTInfo)
                        Console.WriteLine($"{cell.Style.BackColor}, Offset: {(int)tags[0]}, STP bit : {(int)tags[1]}");
                }
                else
                {
                    chkSTPbit.Enabled = false;
                    chkSTPbit.Checked = false;
                }

                colorEditor.Color = dgvCLUT.SelectedCells[0].Style.BackColor;
                UpdateNumricValues();
                ResetColorSliders();
            }
        }

        private void chkSTPbit_Click(object sender, EventArgs e)
        {
            if (dgvCLUT.SelectedCells.Count > 0)
            {
                var tags = dgvCLUT.SelectedCells[0].Tag as List<object>;
                tags[1] = chkSTPbit.Checked ? 1 : 0;

                Color currentColor = LoadColorFromValue(dgvCLUT.SelectedCells[0]);
                ushort rgba5551 = GetRGBA5551(currentColor.B, currentColor.G, currentColor.R, Convert.ToByte(tags[1]));
                byte[] convertedPalette = BitConverter.GetBytes(rgba5551);
                int offset = (int)tags[0];
                Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
            }
        }

        private void cmdSetSTPbit_Click(object sender, EventArgs e)
        {
            UpdateMultipleCells(false, 1);
        }

        private void cmdRemoveSTPbit_Click(object sender, EventArgs e)
        {
            UpdateMultipleCells(false, 0);
        }

        private void UpdateNumricValues()
        {
            if (dgvCLUT.SelectedCells.Count > 0 && editMode == modeCLUT)
            {
                var rowIndices = dgvCLUT.SelectedCells
                                          .Cast<DataGridViewCell>()
                                          .Select(cell => cell.RowIndex);

                int firstRowIndex = rowIndices.Min();
                int lastRowIndex = rowIndices.Max();

                numClutX1.Value = firstRowIndex % 16;
                numClutX2.Value = lastRowIndex % 16;
                numClutY1.Value = firstRowIndex / 16;
                numClutY2.Value = lastRowIndex / 16;
            }
        }

        private void tglGlobalControl_SwitchedChanged(object sender)
        {
            globalControlMode = tglGlobalControl.Switched;
            if (globalControlMode)
            {
                fraCount.Enabled =
                fraSlider.Enabled = false;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = true;
                UpdateNumricValues();
            }
            else
            {
                fraCount.Enabled =
                fraSlider.Enabled = true;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = false;
                ResetColorList();
                ResetColorSliders();
            }
            ResetEditRows();
        }

        private void ResetEditRows()
        {
            editStartRow = dgvCLUT.RowCount - 1;
            editEndRow = 0;
        }

        private void ResetColorSliders()
        {
            dirty = true;

            var hslColor = colorEditorGlobal.HslColor;
            hslColor.H = 180;
            hslColor.S = 0.5;
            hslColor.L = 0.5;
            colorEditorGlobal.HslColor = hslColor;
        }

        private void ResetColorList()
        {
            dgvCLUT.SuspendLayout();
            int startRow = 1;
            int endRow = dgvCLUT.RowCount;
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = 1; col <= 16; col++)
                {
                    var cell = dgvCLUT.Rows[row].Cells[col];
                    var tags = cell.Tag as List<object>;

                    Color oldColor = LoadColorFromValue(cell);

                    ushort rgba5551 = GetRGBA5551(oldColor.B, oldColor.G, oldColor.R, Convert.ToByte(tags[1]));
                    byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                    int offset = (int)tags[0];
                    Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                    cell.Style.BackColor = oldColor;
                }
            }
            dgvCLUT.ResumeLayout();
        }

        private Color LoadColorFromValue(DataGridViewCell cell)
        {
            if (cell.Value is string colorCode)
            {
                return ColorTranslator.FromHtml(colorCode);
            }
            return Color.Empty;
        }

        private void numClutX1_ValueChanged(object sender, EventArgs e)
        {
            //if (numClutX1.Value > numClutX2.Value)
            //    numClutX2.Value = numClutX1.Value;
            //if (numClutY1.Value == 0 && numClutX1.Value == 0)
            //    numClutX1.Value = 1;
        }

        private void numClutX2_ValueChanged(object sender, EventArgs e)
        {
            //if (numClutX2.Value < numClutX1.Value)
            //    numClutX1.Value = numClutX2.Value;
        }

        private void numClutY1_ValueChanged(object sender, EventArgs e)
        {
            //if (numClutY1.Value > numClutY2.Value)
            //    numClutY2.Value = numClutY1.Value;
            //if (numClutY1.Value == 0 && numClutX1.Value == 0)
            //    numClutX1.Value = 1;
            if (numClutY1.Value > dgvCLUT.RowCount / 16 - 1)
                numClutY1.Value = dgvCLUT.RowCount / 16 - 1;
        }

        private void numClutY2_ValueChanged(object sender, EventArgs e)
        {
            //if (numClutY2.Value < numClutY1.Value)
            //    numClutY1.Value = numClutY2.Value;
            if (numClutY2.Value > dgvCLUT.RowCount / 16 - 1)
                numClutY2.Value = dgvCLUT.RowCount / 16 - 1;
        }

        private void rdiModeCLUT_Click(object sender, EventArgs e)
        {
            // To prevent it being unchecked
            MetroSetRadioButton radioButton = sender as MetroSetRadioButton;
            if (radioButton != null && radioButton.Checked)
                radioButton.Checked = false;

            editMode = modeCLUT;
            fraCLUT.Enabled = true;
            UpdateNumricValues();
            ResetColorSliders();
        }

        private void rdiModeSelectedCells_Click(object sender, EventArgs e)
        {
            // To prevent it being unchecked
            MetroSetRadioButton radioButton = sender as MetroSetRadioButton;
            if (radioButton != null && radioButton.Checked)
                radioButton.Checked = false;

            editMode = modeSelectedCells;
            fraCLUT.Enabled = false;
            ResetColorSliders();
        }

        private void chkHighlightSTPbit_CheckedChanged(object sender, EventArgs e)
        {
            dgvCLUT.Refresh();
        }

        private ushort GetRGBA5551(byte r, byte g, byte b, byte a)
        {
            ushort rgba5551 = 0;

            rgba5551 |= (ushort)((r >> 3) << 10);  // Red: 5 bits
            rgba5551 |= (ushort)((g >> 3) << 5);   // Green: 5 bits
            rgba5551 |= (ushort)(b >> 3);          // Blue: 5 bits
            rgba5551 |= (ushort)(a << 15);         // Alpha: 1 bit

            return rgba5551;
        }

        internal static HslColor ChangeHue(HslColor color, double increment)
        {
            HslColor copy;
            double value;

            copy = new HslColor(color);
            value = copy.H + increment;

            if (increment > 0 && value > 359)
            {
                value -= 360;
            }
            else if (increment < 0 && value < 0)
            {
                value += 360;
            }

            copy.H = value;
            return copy;
        }
    }
}
