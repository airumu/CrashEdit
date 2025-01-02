using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        private ModelEntryController controller;
        private ModelEntry model;
        public TexturePageList TPages { get; set; }
        private TextureChunk chunk { get; set; }

        private Rectangle selectedregion;

        private DarkToolTip tipReloadTPage;

        private bool globalControlMode;

        private bool simpleMode;
        private bool BGRAMode;
        private bool replaceCLUT;
        private int selectedRegionX;
        private int selectedRegionY;
        private int currentColorMode;

        private int ColPage = 0;
        private int ColClutX = 1;
        private int ColClutY = 2;
        private int ColLeft = 3;
        private int ColTop = 4;
        private int ColWidth = 5;
        private int ColHeight = 6;
        private int ColX1 = 7;
        private int ColX2 = 8;
        private int ColX3 = 9;
        private int ColY1 = 10;
        private int ColY2 = 11;
        private int ColY3 = 12;
        private int ColBlendMode = 13;
        private int ColColorMode = 14;

        private float MasterHue => hueColorSlider.Value;
        private float MasterSaturation => saturationColorSlider.Value;
        private float MasterLightness => lightnessColorSlider.Value;

        public ModelBox(ModelEntryController controller)
        {
            this.controller = controller;
            model = controller.ModelEntry;
            InitializeComponent();

            DoubleBuffered = true;
            numScaleX.Value = model.ScaleX;
            numScaleY.Value = model.ScaleY;
            numScaleZ.Value = model.ScaleZ;
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (model.Positions == null)
            {
                label2.Text = string.Format("Polygon count: {0}\nVertex count: {1}", model.PolyCount, model.VertexCount);
            }
            else
            {
                int totalbits = model.Positions.Count * 8 * 3;
                int bits = 0;
                foreach (ModelPosition pos in model.Positions)
                {
                    bits += 1 + pos.XBits;
                    bits += 1 + pos.YBits;
                    bits += 1 + pos.ZBits;
                }
                label2.Text = string.Format("Polygon count: {0}\nVertex count: {1}\nCompression ratio: {2:P1} ({3}/{4})", model.PolyCount, model.VertexCount, (float)bits / totalbits, bits, totalbits);
            }
        }

        private void UpdateColorList()
        {
            if (!(lstColor.Items.Count > 0))
            {
                int colorcount = model.Colors.Count;
                for (int i = 0; i < colorcount; ++i)
                {
                    byte[] item = [model.Colors[i].Red, model.Colors[i].Green, model.Colors[i].Blue];
                    ListViewItem lsi = new();
                    lsi.Text = Convert.ToHexString(item);
                    lsi.BackColor = Color.FromArgb(item[0], item[1], item[2]);
                    lsi.ForeColor = getBrightness(lsi.BackColor) >= 0.5 ? Color.Black : Color.White;
                    lsi.Tag = i;
                    // SubItems[0] is the original color, SubItems[1] is the copy
                    lsi.SubItems.Add(Convert.ToHexString(item));
                    lsi.SubItems.Add(Convert.ToHexString(item));
                    lstColor.Items.Add(lsi);
                }
            }
        }

        private void ResetColorList()
        {
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                SetModelColor(Color.FromArgb(item[0], item[1], item[2]), i);
            }
            lstColor.Items.Clear();
            UpdateColorList();
        }

        private void UpdateColorCopy()
        {
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                lstColor.Items[i].SubItems[1].Text = lstColor.Items[i].SubItems[0].Text;
            }
        }

        private void lstColor_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter;
            using (StringFormat sf = new StringFormat())
            {
                bool isSelected = e.Item.Selected;

                e.DrawBackground();

                sf.Alignment = StringAlignment.Center;

                if (isSelected)
                {
                    //e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    e.Graphics.DrawString(e.Item.Text, lstColor.Font, Brushes.White, e.Bounds, sf);
                }
                else
                {
                    e.DrawText(flags);
                }

            }
        }

        private void lstColor_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.Location;
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                ListViewItem item = lstColor.Items[i];
                Rectangle itemBounds = item.Bounds;
                if (itemBounds.Contains(mousePosition))
                {
                    lstColor.SelectedItems.Clear();
                    item.Selected = true;
                    break;
                }
            }
        }

        private void lstColor_MouseUp(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.Location;
            for (int i = 0; i < lstColor.Items.Count; i++)
            {
                ListViewItem item = lstColor.Items[i];
                Rectangle itemBounds = item.Bounds;
                if (itemBounds.Contains(mousePosition))
                {
                    lstColor.SelectedItems.Clear();
                    item.Selected = true;
                    break;
                }
            }
        }

        private void AdjustColumnWidths()
        {
            grdTextures.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            // Background color of the entire grid
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);

            // Color of the grid lines
            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // Style for column headers
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style for row headers
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Background color for odd and even rows
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);

            // Row border style
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header and gridline styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Additional settings
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, grdTextures, new object[] { true });
        }

        private void SetMaxValueTag(int start, int end)
        {
            int startColumnIndex = start;
            int endColumnIndex = end;

            Parallel.For(0, grdTextures.Rows.Count, rowIndex =>
            {
                var row = grdTextures.Rows[rowIndex];
                double maxValue = double.MinValue;

                for (int col = startColumnIndex; col <= endColumnIndex; col++)
                {
                    var cellValue = row.Cells[col].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        if (value > maxValue)
                        {
                            maxValue = value;
                        }
                    }
                }
                for (int col = startColumnIndex; col <= endColumnIndex; col++)
                {
                    var cellValue = row.Cells[col].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        if (value == maxValue)
                        {
                            //if (row.Cells[col].Tag is HashSet<string> tags)
                            //{
                            //    tags.Add("MaxValue");
                            //    row.Cells[col].Style.ForeColor = Color.Turquoise;
                            //}
                            row.Cells[col].Style = maxValueStyle;
                        }
                    }
                }
            });
        }

        DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };

        private void UpdateTextureList(bool creatreColumns)
        {
            grdTextures.SuspendLayout();

            if (creatreColumns)
            {
                grdTextures.Columns.Add("Page", "Page");
                grdTextures.Columns.Add("ClutX", "Clut X");
                grdTextures.Columns.Add("ClutY", "Clut Y");
                grdTextures.Columns.Add("Left", "X  ");
                grdTextures.Columns.Add("Top", "Y  ");
                grdTextures.Columns.Add("Width", "Width");
                grdTextures.Columns.Add("Height", "Height");
                grdTextures.Columns.Add("X1", "X1");
                grdTextures.Columns.Add("X2", "X2");
                grdTextures.Columns.Add("X3", "X3");
                grdTextures.Columns.Add("Y1", "Y1");
                grdTextures.Columns.Add("Y2", "Y2");
                grdTextures.Columns.Add("Y3", "Y3");
                grdTextures.Columns.Add("BlendMode", "Blend");
                grdTextures.Columns.Add("ColorMode", "Color");
            }

            List<string> seenTags = new List<string>();
            for (int i = 0; i < model.Textures.Count; ++i)
            {
                var item = model.Textures[i];
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(grdTextures, item.Page, item.ClutX, item.ClutY, item.Left, item.Top, item.Width, item.Height, item.X1, item.X2, item.X3, item.Y1, item.Y2, item.Y3, item.BlendMode, item.ColorMode);

                var tagValue = $"{item.ClutX}, {item.ClutY}, {item.Left}, {item.Top}";
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Tag = tagValue;
                }
                grdTextures.Rows.Add(row);

                if (simpleMode)
                {
                    var tags = row.Cells[0].Tag as string;
                    if (tags != null)
                    {
                        if (seenTags.Contains(tags))
                        {
                            row.Visible = false;
                            if (Settings.Default.OutputModelTextureInfo)
                                Console.WriteLine($"Hide row at: {row.Index}");
                        }
                        else
                        {
                            seenTags.Add(tags);
                        }
                    }
                }
            }
            SetMaxValueTag(ColX1, ColX3);
            SetMaxValueTag(ColY1, ColY3);

            for (int i = ColLeft; i <= ColHeight; i++)
            {
                grdTextures.Columns[i].Visible = false;
            }
            AdjustColumnWidths();
            grdTextures.ResumeLayout();
        }

        private void grdTextures_SelectionChanged(object sender, EventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0)
            {
                int rowIndex = grdTextures.SelectedCells[0].RowIndex;
                var row = grdTextures.Rows[rowIndex];

                var pageIndex = Convert.ToInt32(row.Cells[ColPage].Value);
                string cid = lstTPages.Items[pageIndex].SubItems[1].Text;

                UpdatePicture();

                numReplace.Value = Convert.ToInt32(grdTextures.CurrentCell.Value);
                numReplaceTo.Value = numReplace.Value;
                numRowIndex.Value = grdTextures.CurrentCell.RowIndex;
                lstTPages.SelectedItems.Clear();
                lstTPages.Items[pageIndex].Selected = true;
                //lstPages.EnsureVisible(pageIndex);

                if (Settings.Default.OutputModelTextureInfo)
                {
                    if (grdTextures.CurrentCell.Tag is HashSet<string> tags)
                    {
                        Console.WriteLine($"[X{grdTextures.CurrentCell.ColumnIndex} - Y{grdTextures.CurrentCell.RowIndex}]");
                        foreach (string tag in tags)
                        {
                            Console.WriteLine(tag);
                        }
                    }
                }
            }
        }

        private void grdTextures_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox_KeyPress;
                textbox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        //private void HideDuplicateRowsByTag()
        //{
        //    List<string> seenTags = new List<string>();
        //    for (int i = grdTextures.Rows.Count - 1; i >= 0; i--)
        //    {
        //        var row = grdTextures.Rows[i];
        //        var tags = row.Cells[0].Tag as string;
        //        if (tags != null)
        //        {
        //            if (seenTags.Contains(tags))
        //            {
        //                row.Visible = false;
        //                Console.WriteLine($"{row.Index}");
        //            }
        //            else
        //            {
        //                seenTags.Add(tags);
        //            }
        //        }
        //    }
        //}

        private void ToggleSimpleMode()
        {
            if (grdTextures.IsCurrentCellInEditMode)
                grdTextures.CancelEdit();
            grdTextures.ScrollBars = ScrollBars.None;
            grdTextures.SuspendLayout();

            grdTextures.Rows.Clear();
            UpdateTextureList(false);

            if (simpleMode)
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    grdTextures.Columns[i].Visible = true;

                for (int i = ColX1; i <= ColY3; i++)
                    grdTextures.Columns[i].Visible = false;
            }
            else
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    grdTextures.Columns[i].Visible = false;

                for (int i = ColX1; i <= ColY3; i++)
                    grdTextures.Columns[i].Visible = true;
            }
            grdTextures.ResumeLayout();

            grdTextures.ScrollBars = ScrollBars.Vertical;
            fraReplace.Enabled = !simpleMode;
        }

        private void cmdReplaceTexture_Click(object sender, EventArgs e)
        {
            if (selectedRegionX < 32 && selectedRegionY == 0)
            {
                DarkMessageBox.ShowError("Textures cannot be replaced on the header.", "Texture replacement");
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.png;|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();

                    int destX = selectedRegionX;
                    int destY = selectedRegionY;
                    if (grdTextures.SelectedCells.Count > 0)
                    {
                        var row = grdTextures.Rows[grdTextures.SelectedCells[0].RowIndex];
                        int clutX = Convert.ToInt32(row.Cells[ColClutX].Value);
                        int clutY = Convert.ToInt32(row.Cells[ColClutY].Value);

                        int oldBpp = 4;
                        if (Convert.ToInt32(row.Cells[ColColorMode].Value) == 1)
                        {
                            oldBpp = 8;
                            destX *= 2;
                            clutX = 0;
                        }

                        chunk.Data = TextureConv.ReplaceTextureFromFile(filePath, extension, BGRAMode, chunk.Data, destX, destY, replaceCLUT, oldBpp, clutX, clutY);
                        UpdatePicture();
                    }
                }
            }
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0)
            {
                int index = (int)numRowIndex.Value;
                int col = grdTextures.CurrentCell.ColumnIndex;
                if (col >= ColX1 && col <= ColX3)
                {
                    UpdateRowsX(index, (int)numReplaceTo.Value, (int)numReplaceTo.Value + (int)grdTextures.Rows[index].Cells[ColWidth].Value);
                }
                else if (col >= ColY1 && col <= ColY3)
                {
                    UpdateRowsY(index, (int)numReplaceTo.Value, (int)numReplaceTo.Value + (int)grdTextures.Rows[index].Cells[ColHeight].Value);
                }
                else
                {
                    var editedCell = grdTextures.SelectedCells[0];
                    var newValue = (int)numReplaceTo.Value;
                    var editedCellTag = editedCell.Tag.ToString();

                    UpdateCellsByTag(col, newValue, editedCellTag);
                }
                UpdatePicture();
            }
        }

        private void grdTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int maxValue = 0;

            // Page
            if (e.ColumnIndex == ColPage)
                maxValue = lstTPages.Items.Count - 1;
            // ClutX
            else if (e.ColumnIndex == ColClutX)
                maxValue = 15;
            // ClutY
            else if (e.ColumnIndex == ColClutY)
                maxValue = 127;
            // X (Left)
            else if (e.ColumnIndex == ColLeft)
                maxValue = (256 << (2 - currentColorMode)) - 1;
            // Y (Top)
            else if (e.ColumnIndex == ColTop)
                maxValue = 127;
            // Width
            else if (e.ColumnIndex == ColWidth)
                maxValue = 1024;
            // Height
            else if (e.ColumnIndex == ColHeight)
                maxValue = 128;
            // Blend Mode
            else if (e.ColumnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (e.ColumnIndex == ColColorMode)
                maxValue = 2;
            // X 1-3
            else if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColX3)
                maxValue = 256 << (2 - currentColorMode);
            // Y 1-3
            else if (e.ColumnIndex >= ColY1 && e.ColumnIndex <= ColY3)
                maxValue = 128;

            if (int.TryParse(e.FormattedValue.ToString(), out int value))
            {
                if (value > maxValue)
                {
                    DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", "Input Error");
                    e.Cancel = true;
                }
                else if (value < 0)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to 0.", "Input Error");
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", "Input Error");
                e.Cancel = true;
            }
        }

        private void grdTextures_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0 && simpleMode)
            {
                var editedCell = grdTextures.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var newValue = editedCell.Value;
                var editedCellTag = editedCell.Tag.ToString();

                UpdateCellsByTag(e.ColumnIndex, newValue, editedCellTag);
            }
        }

        private void UpdateCellsByTag(int columnIndex, object newValue, string editedCellTag)
        {
            if (editedCellTag == null) return;
            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Edited cell tags: {editedCellTag}");
            foreach (DataGridViewRow row in grdTextures.Rows)
            {
                DataGridViewCell cell = row.Cells[columnIndex];

                if (cell.Tag is string tags && tags.Contains(editedCellTag))
                {
                    cell.Value = newValue;
                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}: {string.Join(", ", tags)}");
                }
            }
        }

        private void GetXOff(int colorMode, int value, out int segment, out int xoff)
        {
            int xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
            return;
        }

        private void grdTextures_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var og = model.Textures[e.RowIndex];
            var item = grdTextures.Rows[e.RowIndex];
            int colorMode = Convert.ToInt32(item.Cells[ColColorMode].Value);

            // Page
            if (e.ColumnIndex == ColPage)
            {
                og.Page = Convert.ToByte(item.Cells[ColPage].Value);
                UpdateTPageButtons();
            }
            // ClutX
            else if (e.ColumnIndex == ColClutX)
                og.ClutX = Convert.ToByte(item.Cells[ColClutX].Value);
            // ClutY
            else if (e.ColumnIndex == ColClutY)
            {
                byte value = Convert.ToByte(item.Cells[ColClutY].Value);
                og.ClutY1 = (byte)((value & 0x3) << 2);
                og.ClutY2 = (byte)(value >> 2);
            }
            // X (Left), Width
            else if (e.ColumnIndex == ColLeft || e.ColumnIndex == ColWidth)
            {
                int left = Convert.ToInt32(item.Cells[ColLeft].Value);
                int width = Convert.ToInt32(item.Cells[ColWidth].Value);
                int index = (int)numRowIndex.Value;
                UpdateRowsX(index, left, left + width);
                og.Left = left;
                og.Width = width;
            }
            // Y (Top), Height
            else if (e.ColumnIndex == ColTop || e.ColumnIndex == ColHeight)
            {
                int top = Convert.ToInt32(item.Cells[ColTop].Value);
                int height = Convert.ToInt32(item.Cells[ColHeight].Value);
                int index = (int)numRowIndex.Value;
                UpdateRowsY(index, top, top + height);
                og.Top = top;
                og.Height = height;
            }
            // Blend Mode
            else if (e.ColumnIndex == ColBlendMode)
                og.BlendMode = Convert.ToByte(item.Cells[ColBlendMode].Value);
            // Color Mode
            else if (e.ColumnIndex == ColColorMode)
                og.ColorMode = Convert.ToByte(item.Cells[ColColorMode].Value);
            // X1, X2, X3
            else if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColX3)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);
                int U1 = og.U1, U2 = og.U2, U3 = og.U3;

                if (e.ColumnIndex == ColX1) og.X1 = value;
                else if (e.ColumnIndex == ColX2) og.X2 = value;
                else if (e.ColumnIndex == ColX3) og.X3 = value;

                int segment, xoff;
                GetXOff(colorMode, value, out segment, out xoff);
                int newU = value - xoff;

                //if (item.Cells[e.ColumnIndex].Tag is HashSet<string> tags && tags.Contains("MaxValue"))
                if (item.Cells[e.ColumnIndex].Style == maxValueStyle)
                    newU--;

                if (e.ColumnIndex == ColX1) og.U1 = (byte)newU;
                else if (e.ColumnIndex == ColX2) og.U2 = (byte)newU;
                else if (e.ColumnIndex == ColX3) og.U3 = (byte)newU;
                og.Segment = (byte)segment;
            }
            // Y1, Y2, Y3
            else if (e.ColumnIndex >= ColY1 && e.ColumnIndex <= ColY3)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);
                int V1 = og.V1, V2 = og.V2, V3 = og.V3;

                if (e.ColumnIndex == ColY1) og.Y1 = value;
                else if (e.ColumnIndex == ColY2) og.Y2 = value;
                else if (e.ColumnIndex == ColY3) og.Y3 = value;

                int newV = value;
                //if (item.Cells[e.ColumnIndex].Tag is HashSet<string> tags && tags.Contains("MaxValue"))
                if (item.Cells[e.ColumnIndex].Style == maxValueStyle)
                    newV--;

                if (e.ColumnIndex == ColY1) og.V1 = (byte)newV;
                else if (e.ColumnIndex == ColY2) og.V2 = (byte)newV;
                else if (e.ColumnIndex == ColY3) og.V3 = (byte)newV;
            }

            UpdatePicture();
        }

        private void UpdateRowsX(int targetRowIndex, int newMinU, int newMaxU)
        {
            var targetRow = grdTextures.Rows[targetRowIndex];
            var targetClutX = targetRow.Cells[ColClutX].Value?.ToString();
            var targetClutY = targetRow.Cells[ColClutY].Value?.ToString();

            if (targetClutX == null || targetClutY == null)
                return;

            foreach (DataGridViewRow row in grdTextures.Rows)
            {
                if (row.Cells[ColClutX].Value?.ToString() == targetClutX &&
                    row.Cells[ColClutY].Value?.ToString() == targetClutY)
                {
                    if (int.TryParse(row.Cells[ColX1].Value?.ToString(), out int U1) &&
                        int.TryParse(row.Cells[ColX2].Value?.ToString(), out int U2) &&
                        int.TryParse(row.Cells[ColX3].Value?.ToString(), out int U3))
                    {
                        int minU = Math.Min(U1, Math.Min(U2, U3));
                        int maxU = Math.Max(U1, Math.Max(U2, U3));

                        U1 = (U1 == minU) ? newMinU : newMaxU;
                        U2 = (U2 == minU) ? newMinU : newMaxU;
                        U3 = (U3 == minU) ? newMinU : newMaxU;

                        row.Cells[ColX1].Value = U1;
                        row.Cells[ColX2].Value = U2;
                        row.Cells[ColX3].Value = U3;

                        model.Textures[row.Index].Left = newMinU;
                    }
                }
            }
        }

        private void UpdateRowsY(int targetRowIndex, int newMinV, int newMaxV)
        {
            var targetRow = grdTextures.Rows[targetRowIndex];
            var targetClutX = targetRow.Cells[ColClutX].Value?.ToString();
            var targetClutY = targetRow.Cells[ColClutY].Value?.ToString();

            if (targetClutX == null || targetClutY == null)
                return;

            foreach (DataGridViewRow row in grdTextures.Rows)
            {
                if (row.Cells[ColClutX].Value?.ToString() == targetClutX &&
                    row.Cells[ColClutY].Value?.ToString() == targetClutY)
                {
                    if (int.TryParse(row.Cells[ColY1].Value?.ToString(), out int V1) &&
                        int.TryParse(row.Cells[ColY2].Value?.ToString(), out int V2) &&
                        int.TryParse(row.Cells[ColY3].Value?.ToString(), out int V3))
                    {
                        int minV = Math.Min(V1, Math.Min(V2, V3));
                        int maxV = Math.Max(V1, Math.Max(V2, V3));

                        V1 = (V1 == minV) ? newMinV : newMaxV;
                        V2 = (V2 == minV) ? newMinV : newMaxV;
                        V3 = (V3 == minV) ? newMinV : newMaxV;

                        row.Cells[ColY1].Value = V1;
                        row.Cells[ColY2].Value = V2;
                        row.Cells[ColY3].Value = V3;

                        model.Textures[row.Index].Top = newMinV;
                    }
                }
            }
        }

        private void tbpColors_Enter(object sender, EventArgs e)
        {
            UpdateColorList();
            tbpColors.Enter -= tbpColors_Enter;
        }

        private void tbpTextures_Enter(object sender, EventArgs e)
        {
            tipReloadTPage = new DarkToolTip();
            tipReloadTPage.SetToolTip(rbtReloadTPage, "Reload");
            SetDarkTheme(grdTextures);
            EnableDoubleBuffering();
            UpdateTPageList();
            UpdateTextureList(true);
            UpdateTPageButtons();

            BGRAMode = true;
            replaceCLUT = true;

            if (grdTextures.Rows.Count > 0)
            {
                fraSwitches.Enabled = true;
                fraReplace.Enabled = true;
                fraReplaceTexture.Enabled = true;
            }

            tbpTextures.Enter -= tbpTextures_Enter;
        }

        private void UpdateTPageList()
        {
            lstTPages.Columns.Add("Index");
            lstTPages.Columns.Add("Page");
            for (int i = 0; i < model.TPAGCount; ++i)
            {
                ListViewItem newitem = new ListViewItem(i.ToString());
                newitem.SubItems.Add(Entry.EIDToEName(model.GetTPAG(i)));
                lstTPages.Items.Add(newitem);
            }

            if (lstTPages.Items.Count > 0)
            {
                rbtReloadTPage.Enabled = true;
                dpdTPage.Enabled = true;
                List<Chunk> chunks = null;
                chunks = controller.GetNSF().Chunks;
                foreach (Chunk chunk in chunks)
                {
                    if (chunk is TextureChunk t)
                    {
                        dpdTPage.Items.Add(Entry.EIDToEName(t.EID));
                    }
                }
            }

            UpdateTPageButtons();
        }

        private void UpdateTPageButtons()
        {
            if (model.TPAGCount > 7 || model.TPAGCount == 0)
                cmdAppendTPage.Enabled = false;
            else
                cmdAppendTPage.Enabled = true;

            if (model.TPAGCount == 0)
                cmdRemoveTPage.Enabled = false;
            else
                cmdRemoveTPage.Enabled = true;

            if (grdTextures.Rows.Count > 0)
            {
                int maxIndex = 0;
                foreach (DataGridViewRow row in grdTextures.Rows)
                {
                    int curIndex = Convert.ToInt32(row.Cells[ColPage].Value.ToString());
                    if (curIndex > maxIndex)
                        maxIndex = curIndex;
                }
                if (lstTPages.Items.Count <= maxIndex + 1)
                    cmdRemoveTPage.Enabled = false;
                else
                    cmdRemoveTPage.Enabled = true;
            }
        }

        private void SetModelColor(Color color, int i)
        {
            SceneryColor updatedColor = model.Colors[i];
            updatedColor.Red = color.R;
            updatedColor.Green = color.G;
            updatedColor.Blue = color.B;
            updatedColor.Extra = 0;
            model.Colors[i] = updatedColor;
        }

        private void UpdateSelectedColor(Color clr)
        {
            if (lstColor.SelectedItems.Count <= 0)
            {
                pnSliders.Enabled = false;
                return;
            }

            Color color = clr;
            var i = (int)lstColor.SelectedItems[0].Tag;
            SetModelColor(color, i);
            lstColor.Items[i].SubItems[0].Text = Convert.ToHexString(new byte[] { color.R, color.G, color.B });
            lstColor.Items[i].SubItems[0].BackColor = color;
            lstColor.Items[i].SubItems[0].ForeColor = getBrightness(lstColor.Items[i].SubItems[0].BackColor) >= 0.5 ? Color.Black : Color.White;
            UpdateColorCopy();

            colorEditor.Color = color;
            colorWheel.Color = color;
            picPreview.BackColor = color;
        }

        private void UpdateAllColors()
        {
            if (globalControlMode)
            {
                for (int i = 0; i < lstColor.Items.Count; i++)
                {
                    byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                    Color itemColor = (Color.FromArgb(item[0], item[1], item[2]));

                    HslColor hslColor = new HslColor(itemColor);

                    hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue));
                    hslColor.S += (double)(MasterSaturation - 50) / 100;
                    hslColor.L += (double)(MasterLightness - 50) / 100;

                    Color newColor = hslColor.ToRgbColor();

                    SetModelColor(newColor, i);
                    lstColor.Items[i].SubItems[0].Text = Convert.ToHexString(new byte[] { newColor.R, newColor.G, newColor.B });
                    lstColor.Items[i].SubItems[0].BackColor = newColor;
                    lstColor.Items[i].SubItems[0].ForeColor = getBrightness(lstColor.Items[i].SubItems[0].BackColor) >= 0.5 ? Color.Black : Color.White;
                }
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        // color brightness as perceived:
        float getBrightness(Color c)
        { return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f; }

        private void tglGlobalControl_SwitchedChanged(object sender)
        {
            globalControlMode = tglGlobalControl.Switched;
            if (globalControlMode)
            {
                pnSliders.Enabled = false;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = true;
            }
            else
            {
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

        private Color GetSelectedItemColor()
        {
            string hexcolor = lstColor.SelectedItems[0].Text;
            int color = Int32.Parse(hexcolor.Replace("#", ""), NumberStyles.HexNumber);
            int alpha = 255;
            Color result = Color.FromArgb(alpha, Color.FromArgb(color));
            return result;
        }

        private void lstColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstColor.SelectedItems.Count <= 0 || globalControlMode)
            {
                //pnSliders.Enabled = false;
                return;
            }
            pnSliders.Enabled = true;
            Color color = GetSelectedItemColor();
            colorEditor.Color = color;
            colorWheel.Color = color;
            picPreview.BackColor = color;
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            if (!globalControlMode)
                UpdateSelectedColor(colorEditor.Color);
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            if (!globalControlMode)
                UpdateSelectedColor(colorWheel.Color);
        }

        private void hueColorSlider_ValueChangedHandler(object sender, EventArgs e)
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

        private void cmdApply_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstColor.Items)
            {
                var i = (int)item.Tag;
                byte[] item_ = [model.Colors[i].Red, model.Colors[i].Green, model.Colors[i].Blue];
                SetModelColor(item.BackColor, i);
            }
            UpdateColorCopy();
            tglGlobalControl.Switched = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            tglGlobalControl.Switched = false;
            ResetColorSliders();
        }

        private void tbpColors_Leave(object sender, EventArgs e)
        {
            tglGlobalControl.Switched = false;
            ResetColorSliders();
        }

        private void UpdatePicture()
        {
            if (!(grdTextures.SelectedCells.Count > 0)) return;

            var cell = grdTextures.Rows[grdTextures.SelectedCells[0].RowIndex];
            var pageIndex = Convert.ToInt32(cell.Cells[ColPage].Value);
            string cid = lstTPages.Items[pageIndex].SubItems[1].Text;

            int TexCX = Convert.ToInt32(cell.Cells[ColClutX].Value);
            int TexCY = Convert.ToInt32(cell.Cells[ColClutY].Value);
            int TexX = Convert.ToInt32(cell.Cells[ColLeft].Value);
            int TexY = Convert.ToInt32(cell.Cells[ColTop].Value);
            int TexW = Convert.ToInt32(cell.Cells[ColWidth].Value);
            int TexH = Convert.ToInt32(cell.Cells[ColHeight].Value);
            int colormode = Convert.ToInt32(cell.Cells[ColColorMode].Value);
            int blendmode = 3; //Convert.ToInt32(cell.Cells["ColBlendMode"].Value);
            chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(cid));
            int pw = 256 << (2 - colormode);
            int ph = 128;
            Bitmap bitmap = new Bitmap(pw + 2, ph + 2, PixelFormat.Format32bppArgb);
            Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
            BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] palette = null;
            if (colormode == 0)
            {
                int clutx = TexCX;
                int cluty = TexCY;
                palette = new int[16];
                for (int x = 0; x < 16; ++x)
                {
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, cluty * 512 + (clutx * 16 + x) * 2), blendmode);
                }
            }
            else if (colormode == 1)
            {
                int cluty = TexCY;
                palette = new int[256];
                for (int x = 0; x < 256; ++x)
                {
                    palette[x] = PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, cluty * 512 + x * 2), blendmode);
                }
            }
            try
            {
                for (int y = 0; y < ph; y++)
                {
                    for (int x = 0; x < pw; x++)
                    {
                        int pixel = colormode == 0 ? palette[chunk.Data[x / 2 + y * 512] >> ((x & 1) == 0 ? 0 : 4) & 0xF] :
                        colormode == 1 ? palette[chunk.Data[x + y * 512]] :
                                    colormode == 2 ? PixelConv.Convert5551_8888(BitConv.FromInt16(chunk.Data, x * 2 + y * 512), blendmode)
                                    : throw new Exception("invalid colormode");
                        System.Runtime.InteropServices.Marshal.WriteInt32(bdata.Scan0, x * 4 + y * bdata.Stride, pixel);
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bdata);
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int x = TexX;
                int y = TexY;
                int w = TexW;
                int h = TexH;
                using (var brush = new SolidBrush(Color.FromArgb(127, 0, 0, 0)))
                using (var pen = new Pen(Color.Black))
                {
                    int minh = Math.Min(h, ph - y);
                    g.FillRectangles(brush, new Rectangle[4]
                    {
                        new Rectangle(0, 0, pw, y),
                        new Rectangle(0, y, x, minh),
                        new Rectangle(x+w, y, Math.Max(pw-(x+w),0), minh),
                        new Rectangle(0, y+h, pw, Math.Max(ph-(y+h),0))
                    });
                    g.DrawRectangles(pen, new Rectangle[2]
                    {
                        new Rectangle(x-1,y-1,w+1,h+1),
                        new Rectangle(x-3,y-3,w+5,h+5)
                    });
                    pen.Color = Color.White;
                    g.DrawRectangle(pen, new Rectangle(x - 2, y - 2, w + 3, h + 3));
                }
                selectedregion.X = x;
                selectedregion.Y = y;
                selectedregion.Width = w;
                selectedregion.Height = h;
                selectedRegionX = x;
                selectedRegionY = y;
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Size = bitmap.Size;
            Width = 1024 + 32;

            currentColorMode = colormode;
        }

        private void tglSimpleMode_SwitchedChanged(object sender)
        {
            simpleMode = tglSimpleMode.Switched;
            ToggleSimpleMode();
        }

        private void lstPages_ColumnWidthChangingHandler(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstTPages.Columns[e.ColumnIndex].Width;
        }

        private void chkBGRA_CheckedChanged(object sender, EventArgs e)
        {
            BGRAMode = chkBGRA.Checked;
        }

        private void cmdAppendTPage_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count < 8)
            {
                int index = lstTPages.Items.Count;
                string name = lstTPages.Items[index - 1].SubItems[1].Text;
                model.SetTPAG(index, Entry.ENameToEID(name));
                ++model.TPAGCount;

                ListViewItem newitem = new ListViewItem((index).ToString());
                newitem.SubItems.Add(name);
                lstTPages.Items.Add(newitem);
                UpdateTPageButtons();
            }
        }

        private void cmdRemoveTPage_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0)
            {
                int index = lstTPages.Items.Count;
                int name = 0;
                model.SetTPAG(index - 1, name);
                --model.TPAGCount;

                lstTPages.Items.RemoveAt(index - 1);
                UpdateTPageButtons();
            }
        }

        private void lstTPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0 && lstTPages.SelectedItems.Count > 0)
                dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
        }

        private void chkReplaceCLUT_CheckedChanged(object sender, EventArgs e)
        {
            replaceCLUT = chkReplaceCLUT.Checked;
        }

        private void dpdTPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = dpdTPage.Text;
            if (lstTPages.SelectedItems.Count > 0)
                lstTPages.SelectedItems[0].SubItems[1].Text = text;
            model.SetTPAG(lstTPages.SelectedIndices[0], Entry.ENameToEID(text));
            UpdatePicture();
        }

        private void metroSetRadioButton1_Click(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0)
            {
                dpdTPage.Items.Clear();
                List<Chunk> chunks = null;
                chunks = controller.GetNSF().Chunks;
                foreach (Chunk chunk in chunks)
                {
                    if (chunk is TextureChunk t)
                    {
                        dpdTPage.Items.Add(Entry.EIDToEName(t.EID));
                    }
                }
                if (lstTPages.Items.Count > 0 && lstTPages.SelectedItems.Count > 0)
                    dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
            }
            rbtReloadTPage.Checked = false;
        }

        private void numScaleX_ValueChanged(object sender, EventArgs e)
        {
            model.ScaleX = (int)numScaleX.Value;
        }

        private void numScaleY_ValueChanged(object sender, EventArgs e)
        {
            model.ScaleY = (int)numScaleY.Value;
        }

        private void numScaleZ_ValueChanged(object sender, EventArgs e)
        {
            model.ScaleZ = (int)numScaleZ.Value;
        }

        private void chkShowAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numScaleX.Hexadecimal =
            numScaleY.Hexadecimal =
            numScaleZ.Hexadecimal = chkShowAsHex.Checked;
        }
    }

    public class ListViewEditInfo
    {
        public ListViewItem Item { get; set; }
        public int SubItemIndex { get; set; }

        public ListViewEditInfo(ListViewItem item, int subItemIndex)
        {
            Item = item;
            SubItemIndex = subItemIndex;
        }
    }


}