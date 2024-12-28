using System.Drawing;
using System.Windows.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE.Controls
{
    public partial class CLUTBox : UserControl
    {
        private TextureChunk chunk;

        public CLUTBox(TextureChunk texturechunk)
        {
            chunk = texturechunk;
            DoubleBuffered = true;
            InitializeComponent();

            SetDarkTheme(grdCLUT);
            EnableDoubleBuffering();

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
                    e.Graphics.DrawRectangle(Pens.Turquoise, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);

                    e.Handled = true;
                }
            }
        }

        private void cmdLoadCLUT_Click(object sender, EventArgs e)
        {
            grdCLUT.SuspendLayout();

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
                        //row.Cells[i + 1].Value = (i * 2) + (rowIndex * 32);
                        row.Cells[i + 1].Tag = (i * 2) + (rowIndex * 32);

                        ushort colorValue = BitConverter.ToUInt16(clut, i * 2);
                        int r = (colorValue & 0x1F) << 3;
                        int g = ((colorValue >> 5) & 0x1F) << 3;
                        int b = ((colorValue >> 10) & 0x1F) << 3;
                        row.Cells[i + 1].Style.BackColor = Color.FromArgb(255, r, g, b);
                        row.Cells[i + 1].Style.ForeColor = Color.Transparent;
                    }
                }
            }

            grdCLUT.ResumeLayout();
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
        private void UpdateSelectedColor(Color color)
        {
            if (grdCLUT.SelectedCells.Count > 0 && grdCLUT.SelectedCells[0].ColumnIndex > 0 && grdCLUT.SelectedCells[0].RowIndex > 0)
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

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            //if (!EditMode)
            UpdateSelectedColor(colorEditor.Color);
        }

        private void grdCLUT_SelectionChanged(object sender, EventArgs e)
        {
            if (grdCLUT.SelectedCells.Count > 0)
            {
                colorEditor.Color = grdCLUT.SelectedCells[0].Style.BackColor;
            }
        }


    }
}
