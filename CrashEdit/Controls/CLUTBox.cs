using CrashEdit.Crash;
using MetroSet_UI.Controls;
using Color = System.Drawing.Color;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class CLUTBox : UserControl
    {
        private TextureChunk chunk;

        private bool IsEditMode;
        private int EditMode;

        private float MasterHue => hueColorSlider.Value;
        private float MasterSaturation => saturationColorSlider.Value;
        private float MasterLightness => lightnessColorSlider.Value;

        public CLUTBox(TextureChunk texturechunk)
        {
            chunk = texturechunk;
            DoubleBuffered = true;
            InitializeComponent();

            SetDarkTheme(grdCLUT);
            EnableDoubleBuffering();

            IsEditMode = false;
            EditMode = 0;

            numClutX1.Enabled = false;
            numClutX2.Enabled = false;
            numClutY1.Enabled = false;
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

        private void SetDarkTheme(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);

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

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, grdCLUT, new object[] { true });
        }

        private void grdCLUT_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                var cell = grdCLUT[e.ColumnIndex, e.RowIndex];
                if (cell.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);
                    e.Graphics.DrawRectangle(Pens.Gainsboro, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);

                    e.Handled = true;
                }
            }
        }

        private void cmdLoadCLUT_Click(object sender, EventArgs e)
        {
            grdCLUT.ClearSelection();
            numClutX1.Value =
            numClutX2.Value =
            numClutY1.Value =
            numClutY2.Value = 0;
            grdCLUT.Columns.Clear();

            grdCLUT.SuspendLayout();

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

            foreach (byte[] clut in cluts)
            {
                int rowIndex = grdCLUT.Rows.Add();
                var row = grdCLUT.Rows[rowIndex];

                while (row.Cells.Count < 17)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell());
                }

                row.Cells[0].Value = $"X{rowIndex % 16}, Y{rowIndex / 16}";

                for (int i = 0; i < 16; i++)
                {
                    if (i * 2 + 1 < clut.Length)
                    {
                        short hexString = BitConverter.ToInt16(clut);
                        row.Cells[i + 1].Tag = (i * 2) + (rowIndex * 32);

                        ushort colorValue = BitConverter.ToUInt16(clut, i * 2);
                        int r = (colorValue & 0x1F) << 3;
                        int g = ((colorValue >> 5) & 0x1F) << 3;
                        int b = ((colorValue >> 10) & 0x1F) << 3;
                        Color color = Color.FromArgb(255, r, g, b);
                        row.Cells[i + 1].Style.ForeColor = Color.Transparent;
                        row.Cells[i + 1].Style.BackColor = color;
                        row.Cells[i + 1].Value = ColorTranslator.ToHtml(color);
                    }
                }
            }

            grdCLUT.ResumeLayout();
            fraSlider.Enabled =
            fraGlobalControl.Enabled = true;
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

        private Color GetColor(Color itemColor)
        {
            HslColor hslColor = new HslColor(itemColor);

            hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue));
            hslColor.S += (double)(MasterSaturation - 50) / 100;
            hslColor.L += (double)(MasterLightness - 50) / 100;

            Color newColor = hslColor.ToRgbColor();
            return newColor;
        }

        private void UpdateAllColors()
        {
            if (IsEditMode)
            {
                grdCLUT.SuspendLayout();
                if (EditMode == 0)
                {
                    int startRow = (int)numClutX1.Value + (int)numClutY1.Value * 16;
                    int endRow = (int)numClutX2.Value + (int)numClutY2.Value * 16;

                    if (startRow == 0)
                        startRow++;

                    for (int row = startRow; row <= endRow; row++)
                    {
                        for (int col = 1; col <= 16; col++)
                        {
                            var cell = grdCLUT.Rows[row].Cells[col];

                            Color itemColor = LoadColorFromValue(cell);
                            Color newColor = GetColor(itemColor);

                            ushort rgba5551 = TextureConv.ConvertToRGBA5551(newColor.B, newColor.G, newColor.R, newColor.A);
                            byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                            int offset = (int)cell.Tag;
                            Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                            cell.Style.BackColor = newColor;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in grdCLUT.SelectedCells)
                    {
                        if (grdCLUT.SelectedCells.Count > 0 && cell.RowIndex > 0 && cell.ColumnIndex > 0)
                        {
                            Color itemColor = LoadColorFromValue(cell);
                            Color newColor = GetColor(itemColor);

                            ushort rgba5551 = TextureConv.ConvertToRGBA5551(newColor.B, newColor.G, newColor.R, newColor.A);
                            byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                            int offset = (int)cell.Tag;
                            Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                            cell.Style.BackColor = newColor;
                        }
                    }
                }
                grdCLUT.ResumeLayout();
            }
        }

        private void UpdateSelectedColor(Color color)
        {
            var cell = grdCLUT.SelectedCells;
            if (cell.Count > 0 && cell[0].ColumnIndex > 0 && cell[0].RowIndex > 0)
            {
                colorEditor.Enabled = true;
                grdCLUT.SelectedCells[0].Style.BackColor = color;

                ushort rgba5551 = TextureConv.ConvertToRGBA5551(color.B, color.G, color.R, color.A);
                byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                int offset = (int)grdCLUT.SelectedCells[0].Tag;
                Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);
            }
            else
            {
                colorEditor.Enabled = false;
                return;
            }
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

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (!IsEditMode)
                UpdateSelectedColor(colorEditor.Color);
        }

        private void hueColorSlider_ValueChanged(object sender, EventArgs e)
        {
            if (hueColorSlider.Focused)
            {
                UpdateAllColors();
            }
        }

        private void saturationColorSlider_ValueChanged(object sender, EventArgs e)
        {
            if (saturationColorSlider.Focused)
            {
                UpdateAllColors();
            }
        }

        private void lightnessColorSlider_ValueChanged(object sender, EventArgs e)
        {
            if (lightnessColorSlider.Focused)
            {
                UpdateAllColors();
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            grdCLUT.SuspendLayout();

            int startRow = (int)numClutX1.Value + (int)numClutY1.Value * 16;
            int endRow = (int)numClutX2.Value + (int)numClutY2.Value * 16;
            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = 1; col <= 16; col++)
                {
                    var item = grdCLUT.Rows[row].Cells[col];

                    Color color = item.Style.BackColor;
                    item.Value = ColorTranslator.ToHtml(color);
                }
            }
            grdCLUT.ResumeLayout();
            tglGlobalControl.Switched = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ResetColorList();
            tglGlobalControl.Switched = false;
        }

        //private void tbpColors_Leave(object sender, EventArgs e)
        //{
        //    ResetColorList();
        //    tglGlobalControl.Switched = false;
        //}

        private void grdCLUT_SelectionChanged(object sender, EventArgs e)
        {
            UpdateNumricValues();
            ResetColorSliders();
        }

        private void UpdateNumricValues()
        {
            if (grdCLUT.SelectedCells.Count > 0)
            {
                colorEditor.Color = grdCLUT.SelectedCells[0].Style.BackColor;

                if (EditMode == 0)
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
        }

        private void tglGlobalControl_SwitchedChanged(object sender)
        {
            IsEditMode = tglGlobalControl.Switched;
            if (IsEditMode)
            {
                fraCount.Enabled =
                pnSliders.Enabled = false;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = true;
                UpdateNumricValues();
            }
            else
            {
                fraCount.Enabled =
                pnSliders.Enabled = true;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = false;
                ResetColorList();
            }
            ResetColorSliders();
        }

        private void ResetColorSliders()
        {
            hueColorSlider.Value = 180F;
            saturationColorSlider.Value = 50F;
            lightnessColorSlider.Value = 50F;
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
                    var item = grdCLUT.Rows[row].Cells[col];

                    Color oldColor = LoadColorFromValue(item);

                    ushort rgba5551 = TextureConv.ConvertToRGBA5551(oldColor.B, oldColor.G, oldColor.R, oldColor.A);
                    byte[] convertedPalette = BitConverter.GetBytes(rgba5551);

                    int offset = (int)item.Tag;
                    Array.Copy(convertedPalette, 0, chunk.Data, offset, 2);

                    item.Style.BackColor = oldColor;
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

            EditMode = 0;
            pnCLUT.Enabled = true;
            UpdateNumricValues();
        }

        private void rdiModeSelectedCells_Click(object sender, EventArgs e)
        {
            // To prevent it being unchecked
            MetroSetRadioButton radioButton = sender as MetroSetRadioButton;
            if (radioButton != null && radioButton.Checked)
                radioButton.Checked = false;

            EditMode = 1;
            pnCLUT.Enabled = false;
        }

    }
}
