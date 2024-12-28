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
                        ushort colorValue = BitConverter.ToUInt16(clut, i * 2);
                        row.Cells[i + 1].Tag = colorValue;

                        int r = (colorValue & 0x1F) << 3;
                        int g = ((colorValue >> 5) & 0x1F) << 3;
                        int b = ((colorValue >> 10) & 0x1F) << 3;
                        row.Cells[i + 1].Style.BackColor = Color.FromArgb(255, r, g, b);
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
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                var tagValue = grdCLUT.SelectedCells[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(tagValue))
                {
                    Clipboard.SetText(tagValue);
                }

            }
        }

    }
}
