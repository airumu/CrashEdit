using System.Collections.Concurrent;
using System.Windows.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;
using Color = System.Drawing.Color;
using HslColor = Cyotek.Windows.Forms.HslColor;

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

        private double MasterHue => colorEditorGlobal.HslColor.H;
        private double MasterSaturation => colorEditorGlobal.HslColor.S;
        private double MasterLightness => colorEditorGlobal.HslColor.L;

        internal bool dirty;

        public CLUTBox(TextureChunk texturechunk)
        {
            chunk = texturechunk;
            DoubleBuffered = true;
            InitializeComponent();

            SetDarkTheme(grdCLUT);
            EnableDoubleBuffering();
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

        private void grdCLUT_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                var cell = grdCLUT[e.ColumnIndex, e.RowIndex];
                var tags = cell.Tag as List<object>;
                if (cell.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);
                    e.Graphics.DrawRectangle(Pens.Gainsboro, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);

                    e.Handled = true;
                }
                else if (chkHighlightSTPbit.Checked & (int)tags[1] == 1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);
                    e.Graphics.DrawRectangle(Pens.Turquoise, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);

                    e.Handled = true;
                }
            }
        }

        private async Task UpdateCLUTList()
        {
            grdCLUT.SuspendLayout();

            grdCLUT.ClearSelection();
            numClutX1.Value =
            numClutX2.Value =
            numClutY1.Value =
            numClutY2.Value = 0;

            grdCLUT.Columns.Clear();
            grdCLUT.Columns.Add($"Clut", $"Clut");
            for (int i = 0; i < 16; i++)
            {
                grdCLUT.Columns.Add($"Color{i + 1}", $"{i + 1}");
            }
            foreach (DataGridViewColumn column in grdCLUT.Columns)
            {
                column.Width = 25;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            grdCLUT.Columns[0].Width = 75;

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

                    row.Cells[0].Value = $"X{i % 16}, Y{i / 16}";

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

            grdCLUT.Rows.AddRange(rows.ToArray());
            grdCLUT.ResumeLayout();

            fraSlider.Enabled =
            fraGlobalControl.Enabled = true;
        }

        private async void cmdLoadCLUT_Click(object sender, EventArgs e)
        {
            await UpdateCLUTList();
        }

        private void grdCLUT_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            //{
            //    var tagValue = grdCLUT.SelectedCells[0].Tag?.ToString();
            //    if (!string.IsNullOrEmpty(tagValue))
            //    {
            //        Clipboard.SetText(tagValue);
            //    }
            //}
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

            grdCLUT.SuspendLayout();

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
                        var cell = grdCLUT.Rows[row].Cells[col];
                        UpdateColor(cell, STPbit);
                    }
                }
            }
            else
            {
                foreach (DataGridViewCell cell in grdCLUT.SelectedCells)
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
            if (Settings.Default.OutputCLUTInfo)
                Console.WriteLine($"Start {editStartRow}, End {editEndRow}");
            grdCLUT.ResumeLayout();
            grdCLUT.Refresh();
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
                rgba5551 = TextureConv.ConvertToRGBA5551(currentColor.B, currentColor.G, currentColor.R, Convert.ToByte(tags[1]));
            }
            else // change color
            {
                rgba5551 = TextureConv.ConvertToRGBA5551(newColor.B, newColor.G, newColor.R, Convert.ToByte(tags[1]));
                cell.Style.BackColor = newColor;
            }

            byte[] convertedPalette = BitConverter.GetBytes(rgba5551);
            int offset = (int)tags[0];
            Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
        }

        private void UpdateSelectedColor(Color color)
        {
            var cell = grdCLUT.SelectedCells;
            if (cell.Count > 0 && cell[0].ColumnIndex > 0 && cell[0].RowIndex > 0)
            {
                colorEditor.Enabled = true;
                cell[0].Style.BackColor = color;
                var tags = cell[0].Tag as List<object>;

                ushort rgba5551 = TextureConv.ConvertToRGBA5551(color.B, color.G, color.R, Convert.ToByte(tags[1]));
                byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                int offset = (int)tags[0];
                Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
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
            grdCLUT.SuspendLayout();
            for (int row = editStartRow; row <= editEndRow; row++)
            {
                for (int col = 1; col <= 16; col++)
                {
                    var cell = grdCLUT.Rows[row].Cells[col];

                    Color color = cell.Style.BackColor;
                    cell.Value = ColorTranslator.ToHtml(color);
                }
            }
            grdCLUT.ResumeLayout();
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

        private void grdCLUT_SelectionChanged(object sender, EventArgs e)
        {
            if (grdCLUT.SelectedCells.Count > 0)
            {
                var cell = grdCLUT.SelectedCells[0];
                if (cell.RowIndex > 0 && cell.ColumnIndex > 0)
                {
                    var tags = cell.Tag as List<object>;

                    chkSTPbit.Checked = (int)tags[1] == 0 ? false : true;

                    if (Settings.Default.OutputCLUTInfo)
                        Console.WriteLine($"{cell.Style.BackColor}, Offset: {(int)tags[0]}, STP bit : {(int)tags[1]}");
                }

                colorEditor.Color = grdCLUT.SelectedCells[0].Style.BackColor;
                UpdateNumricValues();
                ResetColorSliders();
            }
        }

        private void chkSTPbit_Click(object sender, EventArgs e)
        {
            if (grdCLUT.SelectedCells.Count > 0)
            {
                var tags = grdCLUT.SelectedCells[0].Tag as List<object>;
                tags[1] = chkSTPbit.Checked ? 1 : 0;
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
            if (grdCLUT.SelectedCells.Count > 0 && editMode == modeCLUT)
            {
                var rowIndices = grdCLUT.SelectedCells
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
            editStartRow = grdCLUT.RowCount - 1;
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
            grdCLUT.SuspendLayout();
            int startRow = 1;
            int endRow = grdCLUT.RowCount;
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = 1; col <= 16; col++)
                {
                    var cell = grdCLUT.Rows[row].Cells[col];
                    var tags = cell.Tag as List<object>;

                    Color oldColor = LoadColorFromValue(cell);

                    ushort rgba5551 = TextureConv.ConvertToRGBA5551(oldColor.B, oldColor.G, oldColor.R, Convert.ToByte(tags[1]));
                    byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                    int offset = (int)tags[0];
                    Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                    cell.Style.BackColor = oldColor;
                }
            }
            grdCLUT.ResumeLayout();
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
            if (numClutY1.Value > grdCLUT.RowCount / 16 - 1)
                numClutY1.Value = grdCLUT.RowCount / 16 - 1;
        }

        private void numClutY2_ValueChanged(object sender, EventArgs e)
        {
            //if (numClutY2.Value < numClutY1.Value)
            //    numClutY1.Value = numClutY2.Value;
            if (numClutY2.Value > grdCLUT.RowCount / 16 - 1)
                numClutY2.Value = grdCLUT.RowCount / 16 - 1;
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
            grdCLUT.Refresh();
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

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, grdCLUT, new object[] { true });
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.FromArgb(31, 31, 32);

            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);

            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

    }
}
