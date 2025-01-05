using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Media.TextFormatting;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        //private ModelEntryController modelcontroller;
        //private ModelEntry model;
        //private SceneryEntryController scenerycontroller;
        //private SceneryEntry scenery;
        private dynamic controller;
        private dynamic model;
        private TextureChunk chunk { get; set; }

        private Rectangle selectedregion;

        private DarkToolTip tipReloadTPage;

        private bool isScenery;
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
        private int ColX4 = 10;
        private int ColY1 = 11;
        private int ColY2 = 12;
        private int ColY3 = 13;
        private int ColY4 = 14;
        private int ColBlendMode = 15;
        private int ColColorMode = 16;

        private float MasterHue => hueColorSlider.Value;
        private float MasterSaturation => saturationColorSlider.Value;
        private float MasterLightness => lightnessColorSlider.Value;

        public ModelBox(ModelEntryController controller)
        {
            MainInit(controller, false);
        }

        public ModelBox(SceneryEntryController controller)
        {
            MainInit(controller, true);
        }

        private void MainInit(object controller, bool isScenery)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.controller = controller;
            this.isScenery = isScenery;

            if (isScenery)
            {
                model = this.controller.SceneryEntry;

                fraScales.Visible = false;
                lblModelInfo.Visible = false;

                numOffsetX.Value = model.XOffset;
                numOffsetY.Value = model.YOffset;
                numOffsetZ.Value = model.ZOffset;
            }
            else
            {
                model = this.controller.ModelEntry;

                fraOffsets.Visible = false;

                numScaleX.Value = model.ScaleX;
                numScaleY.Value = model.ScaleY;
                numScaleZ.Value = model.ScaleZ;
                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            if (model.Positions == null)
            {
                lblModelInfo.Text = string.Format("Polygon count: {0}\nVertex count: {1}", model.PolyCount, model.VertexCount);
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
                lblModelInfo.Text = string.Format("Polygon count: {0}\nVertex count: {1}\nCompression ratio: {2:P1} ({3}/{4})", model.PolyCount, model.VertexCount, (float)bits / totalbits, bits, totalbits);
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
            //grdTextures.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewColumn column in grdTextures.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 44;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
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

        DataGridViewCellStyle maxValueStyle = new DataGridViewCellStyle
        {
            ForeColor = Color.Turquoise
        };

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
                            //    tags.Add("MaxValue");
                            row.Cells[col].Style = maxValueStyle;
                        }
                    }
                }
            });
        }

        private void CreateTextureListColumns()
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
            grdTextures.Columns.Add("X4", "X4");
            grdTextures.Columns.Add("Y1", "Y1");
            grdTextures.Columns.Add("Y2", "Y2");
            grdTextures.Columns.Add("Y3", "Y3");
            grdTextures.Columns.Add("Y4", "Y4");
            grdTextures.Columns.Add("BlendMode", "Blend");
            grdTextures.Columns.Add("ColorMode", "Color");

            for (int i = ColLeft; i <= ColHeight; i++)
            {
                grdTextures.Columns[i].Visible = false;
            }
            if (!isScenery)
            {
                grdTextures.Columns[ColX4].Visible = false;
                grdTextures.Columns[ColY4].Visible = false;
            }
            AdjustColumnWidths();
        }

        private async Task UpdateTextureListAsync(bool setMaxTags)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var seenTags = new ConcurrentDictionary<string, bool>();

            var rows = await Task.Run(() =>
            {
                var rowsToAdd = new ConcurrentBag<(int Index, DataGridViewRow Row)>();
                
                Parallel.ForEach(Enumerable.Range(0, (int)model.Textures.Count), (int i) =>
                {
                    var item = model.Textures[i];
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(grdTextures, item.Page, item.ClutX, item.ClutY, item.Left, item.Top, item.Width, item.Height, item.X1, item.X2, item.X3, item.X4, item.Y1, item.Y2, item.Y3, item.Y4, item.BlendMode, item.ColorMode);

                    var tagValue = $"{item.ClutX}, {item.ClutY}, {item.Left}, {item.Top}";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Tag = tagValue;
                    }

                    rowsToAdd.Add((i, row));
                });
                return rowsToAdd.OrderBy(pair => pair.Index).Select(pair => pair.Row).ToList();
            });

            grdTextures.Rows.Clear();
            foreach (var row in rows)
            {
                grdTextures.Rows.Add(row);
                if (simpleMode)
                {
                    string tagValue = row.Cells[0].Tag as string;
                    if (!seenTags.ContainsKey(tagValue))
                    {
                        seenTags.TryAdd(tagValue, true);
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }

            if (isScenery)
            {
                SetMaxValueTag(ColX1, ColX4);
                SetMaxValueTag(ColY1, ColY4);
            }
            else
            {
                SetMaxValueTag(ColX1, ColX3);
                SetMaxValueTag(ColY1, ColY3);
            }

            stopwatch.Stop();
            int count = simpleMode ? seenTags.Count : rows.Count;
            Console.WriteLine($"Row count: {count}");
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
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

                if (Settings.Default.OutputModelTextureInfo && grdTextures.CurrentCell.Tag is string tags)
                {
                    Console.WriteLine($"Row {grdTextures.CurrentCell.RowIndex} Tags: {string.Join(", ", tags)}");
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

        private async void cmdLoadTexture_Click(object sender, EventArgs e)
        {
            await UpdateTextureListAsync(true);

            if (grdTextures.Rows.Count > 0)
            {
                UpdateTPageButtons();
                fraSwitches.Enabled = true;
                fraReplace.Enabled = true;
                fraReplaceTexture.Enabled = true;
            }
        }

        private async void ToggleSimpleMode()
        {
            if (grdTextures.IsCurrentCellInEditMode)
                grdTextures.CancelEdit();
            grdTextures.SuspendLayout();
            grdTextures.ScrollBars = ScrollBars.None;

            await UpdateTextureListAsync(false);

            int endColX = isScenery ? ColX4 : ColX3;
            int endColY = isScenery ? ColY4 : ColY3;
            if (simpleMode)
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    grdTextures.Columns[i].Visible = true;

                for (int i = ColX1; i <= endColX; i++)
                    grdTextures.Columns[i].Visible = false;
                for (int i = ColY1; i <= endColY; i++)
                    grdTextures.Columns[i].Visible = false;
            }
            else
            {
                for (int i = ColLeft; i <= ColHeight; i++)
                    grdTextures.Columns[i].Visible = false;

                for (int i = ColX1; i <= endColX; i++)
                    grdTextures.Columns[i].Visible = true;
                for (int i = ColY1; i <= endColY; i++)
                    grdTextures.Columns[i].Visible = true;
            }

            grdTextures.ScrollBars = ScrollBars.Vertical;
            fraReplace.Enabled = !simpleMode;
            grdTextures.ResumeLayout();
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

        private void GetMaxValue(int rowIndex, int columnIndex, int newValue, out int minValue, out int maxValue, out bool isMaxCell)
        {
            isMaxCell = grdTextures.Rows[rowIndex].Cells[columnIndex].Style == maxValueStyle;
            maxValue = 0; minValue = 0;
            // Page
            if (columnIndex == ColPage)
                maxValue = lstTPages.Items.Count - 1;
            // ClutX
            else if (columnIndex == ColClutX)
                maxValue = 15;
            // ClutY
            else if (columnIndex == ColClutY)
                maxValue = 127;
            // X (Left)
            else if (columnIndex == ColLeft)
            {
                int width = (int)grdTextures.Rows[rowIndex].Cells[ColWidth].Value;
                GetXOff(currentColorMode, newValue, out int xoffUnit, out int segment, out int xoff);

                int pw = 256 << (2 - currentColorMode);
                if (newValue >= (isMaxCell ? pw + width : pw))
                {
                    maxValue = isMaxCell ? pw : pw - width;
                    return;
                }

                int xoffEnd = xoff + xoffUnit;
                maxValue = xoffEnd - width;
                Console.WriteLine($"Segment {segment}, maxValue {maxValue}");
            }
            // Y (Top)
            else if (columnIndex == ColTop)
                maxValue = 128 - (int)grdTextures.Rows[rowIndex].Cells[ColHeight].Value;
            // Width
            else if (columnIndex == ColWidth)
            {
                int value = (int)grdTextures.Rows[rowIndex].Cells[ColLeft].Value;
                GetXOff(currentColorMode, value, out int xoffUnit, out int segment, out int xoff);

                maxValue = xoffUnit - (value - xoff);
                minValue = 4;
                Console.WriteLine($"Segment {segment}, maxValue {maxValue}");
            }
            // Height
            else if (columnIndex == ColHeight)
            {
                maxValue = 128 - (int)grdTextures.Rows[rowIndex].Cells[ColTop].Value;
                minValue = 4;
            }
            // Blend Mode
            else if (columnIndex == ColBlendMode)
                maxValue = 3;
            // Color Mode
            else if (columnIndex == ColColorMode)
                maxValue = 2;
            // X1-X4
            else if (columnIndex >= ColX1 && columnIndex <= ColX4)
            {
                int width = (int)grdTextures.Rows[rowIndex].Cells[ColWidth].Value;
                GetXOff(currentColorMode, isMaxCell ? newValue - width : newValue, out int xoffUnit, out int segment, out int xoff);
                int xoffEnd = xoff + xoffUnit;

                int pw = 256 << (2 - currentColorMode);
                if (newValue >= (isMaxCell ? pw + width : pw))
                {
                    maxValue = isMaxCell ? pw : pw - width;
                    return;
                }

                if (isMaxCell)
                {
                    maxValue = xoffEnd;
                    minValue = xoff + width;
                }
                else
                {
                    maxValue = xoffEnd - width;
                    minValue = xoff;
                }
                Console.WriteLine($"Segment {segment}, minValue {minValue}, maxValue {maxValue}");
            }
            // Y1-Y4
            else if (columnIndex >= ColY1 && columnIndex <= ColY4)
                maxValue = 128 - (int)(isMaxCell ? 0 : grdTextures.Rows[rowIndex].Cells[ColHeight].Value);

        }

        private void grdTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (int.TryParse(e.FormattedValue.ToString(), out int newValue))
            {
                GetMaxValue(e.RowIndex, e.ColumnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);
                if (newValue > maxValue)
                {
                    if (e.ColumnIndex >= ColLeft && e.ColumnIndex <= ColHeight)
                        DarkMessageBox.ShowError($"The UV does not fit within the segment. The value must be less than or equal to {maxValue}.", "Input Error");
                    else
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", "Input Error");
                    e.Cancel = true;
                }
                else if (newValue < minValue)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to {minValue}.", "Input Error");
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", "Input Error");
                e.Cancel = true;
            }
        }

        private async void cmdReplace_Click(object sender, EventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0)
            {
                int rowIndex = (int)numRowIndex.Value;
                int columnIndex = grdTextures.CurrentCell.ColumnIndex;
                var row = grdTextures.Rows[rowIndex];
                string? editedCellTag = grdTextures.SelectedCells[0].Tag?.ToString();

                int newValue = (int)numReplaceTo.Value;
                GetMaxValue(rowIndex, columnIndex, newValue, out int minValue, out int maxValue, out bool isMaxCell);
                if (newValue > maxValue)
                {
                    if (columnIndex >= ColLeft && columnIndex <= ColHeight)
                        DarkMessageBox.ShowError($"The UV does not fit within the segment. The value must be less than or equal to {maxValue}.", "Input Error");
                    else
                        DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", "Input Error");
                    return;
                }

                int endColX = isScenery ? ColX4 : ColX3;
                int endColY = isScenery ? ColY4 : ColY3;
                if (columnIndex >= ColX1 && columnIndex <= endColX)
                {
                    if (isMaxCell)
                        newValue -= (int)row.Cells[ColWidth].Value;
                    if (newValue < 0)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {row.Cells[ColWidth].Value}.", "Input Error");
                        return;
                    }
                    await UpdateRowsXYAsync(rowIndex, newValue, newValue + (int)row.Cells[ColWidth].Value, true, editedCellTag);
                }
                else if (columnIndex >= ColY1 && columnIndex <= endColY)
                {
                    if (isMaxCell)
                        newValue -= (int)row.Cells[ColHeight].Value;
                    if (newValue < 0)
                    {
                        DarkMessageBox.ShowError($"The value must be greater than or equal to {row.Cells[ColHeight].Value}.", "Input Error");
                        return;
                    }
                    await UpdateRowsXYAsync(rowIndex, newValue, newValue + (int)row.Cells[ColHeight].Value, false, editedCellTag);
                }
                else if (columnIndex == ColColorMode)
                {
                    await UpdateRowsColorModeAsync(rowIndex, newValue, editedCellTag);
                }
                else
                {
                    await UpdateCellsByTagAsync(columnIndex, columnIndex, newValue, editedCellTag);
                }
                UpdatePicture();
            }
        }

        private async void grdTextures_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0 && simpleMode)
            {
                var editedRow = grdTextures.Rows[e.RowIndex];
                int newValue = Convert.ToInt32(editedRow.Cells[e.ColumnIndex].Value);
                string? editedCellTag = editedRow.Cells[e.ColumnIndex].Tag?.ToString();

                if (editedCellTag != null)
                {
                    if (e.ColumnIndex == ColLeft)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, newValue, newValue + (int)editedRow.Cells[ColWidth].Value, true, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColWidth)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, (int)editedRow.Cells[ColLeft].Value, (int)editedRow.Cells[ColLeft].Value + newValue, true, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColTop)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, newValue, newValue + (int)editedRow.Cells[ColHeight].Value, false, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColHeight)
                    {
                        await UpdateRowsXYAsync(e.RowIndex, (int)editedRow.Cells[ColTop].Value, (int)editedRow.Cells[ColTop].Value + newValue, false, editedCellTag);
                    }
                    else if (e.ColumnIndex == ColColorMode)
                    {
                        await UpdateRowsColorModeAsync(e.RowIndex, newValue, editedCellTag);
                    }
                    else
                    {
                        await UpdateCellsByTagAsync(e.ColumnIndex, e.ColumnIndex, newValue, editedCellTag);
                    }
                }
                UpdatePicture();
            }
        }

        private async Task UpdateCellsByTagAsync(int startColumn, int endColumn, int newValue, string editedCellTag)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var filteredRows = grdTextures.Rows.Cast<DataGridViewRow>()
                  .Where(row =>
                  {
                      var cell = row.Cells[0];
                      return cell.Tag is string tags && tags.Contains(editedCellTag);
                  })
                  .ToList();

            foreach (var row in filteredRows)
            {
                for (int col = startColumn; col <= endColumn; col++)
                {
                    row.Cells[col].Value = newValue;
                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }
            }

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
        }

        private void GetXOff(int colorMode, int value, out int xoffUnit, out int segment, out int xoff)
        {
            xoffUnit = (1 << (2 - colorMode)) * 64;
            segment = value / xoffUnit;
            xoff = xoffUnit * segment;
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
            // X (Left)
            else if (e.ColumnIndex == ColLeft)
                og.Left = Convert.ToInt32(item.Cells[ColLeft].Value);
            // Width
            else if (e.ColumnIndex == ColWidth)
                og.Width = Convert.ToInt32(item.Cells[ColWidth].Value);
            // Y (Top)
            else if (e.ColumnIndex == ColTop)
                og.Top = Convert.ToInt32(item.Cells[ColTop].Value);
            // Height
            else if (e.ColumnIndex == ColHeight)
                og.Height = Convert.ToInt32(item.Cells[ColHeight].Value);
            // Blend Mode
            else if (e.ColumnIndex == ColBlendMode)
                og.BlendMode = Convert.ToByte(item.Cells[ColBlendMode].Value);
            // Color Mode
            else if (e.ColumnIndex == ColColorMode)
                og.ColorMode = Convert.ToByte(item.Cells[ColColorMode].Value);
            // X1-X4
            else if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColX4)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);

                if (e.ColumnIndex == ColX1) og.X1 = value;
                else if (e.ColumnIndex == ColX2) og.X2 = value;
                else if (e.ColumnIndex == ColX3) og.X3 = value;
                else if (e.ColumnIndex == ColX4) og.X4 = value;

                GetXOff(colorMode, value - 1, out int xoffUnit, out int segment, out int xoff);
                int newU = value - xoff;

                if (item.Cells[e.ColumnIndex].Style == maxValueStyle)
                {
                    newU--;
                    og.Segment = (byte)segment;
                }

                if (e.ColumnIndex == ColX1) og.U1 = (byte)newU;
                else if (e.ColumnIndex == ColX2) og.U2 = (byte)newU;
                else if (e.ColumnIndex == ColX3) og.U3 = (byte)newU;
                else if (e.ColumnIndex == ColX4) og.U4 = (byte)newU;
            }
            // Y1-Y4
            else if (e.ColumnIndex >= ColY1 && e.ColumnIndex <= ColY4)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);

                if (e.ColumnIndex == ColY1) og.Y1 = value;
                else if (e.ColumnIndex == ColY2) og.Y2 = value;
                else if (e.ColumnIndex == ColY3) og.Y3 = value;
                else if (e.ColumnIndex == ColY4) og.Y4 = value;

                int newV = value;

                if (item.Cells[e.ColumnIndex].Style == maxValueStyle)
                {
                    newV--;
                }

                if (e.ColumnIndex == ColY1) og.V1 = (byte)newV;
                else if (e.ColumnIndex == ColY2) og.V2 = (byte)newV;
                else if (e.ColumnIndex == ColY3) og.V3 = (byte)newV;
                else if (e.ColumnIndex == ColY4) og.V4 = (byte)newV;
            }
        }

        private async Task UpdateRowsXYAsync(int rowIndex, int newMinUV, int newMaxUV, bool targetIsX, string editedCellTag)
        {
            if (rowIndex < 0 || rowIndex >= model.Textures.Count)
                return;

            int Col1, Col2, Col3, Col4, ColStart, ColLength;
            if (targetIsX)
            {
                Col1 = ColX1;
                Col2 = ColX2;
                Col3 = ColX3;
                Col4 = ColX4;
                ColStart = ColLeft;
                ColLength = ColWidth;
            }
            else
            {
                Col1 = ColY1;
                Col2 = ColY2;
                Col3 = ColY3;
                Col4 = ColY4;
                ColStart = ColTop;
                ColLength = ColHeight;
            }

            var targetRow = grdTextures.Rows[rowIndex];
   
            Stopwatch stopwatch = Stopwatch.StartNew();
            var updatedRows = new ConcurrentBag<(int RowIndex, int UV1, int UV2, int UV3, int UV4)>();

            await Task.Run(() =>
            {
                var filteredRows = grdTextures.Rows.Cast<DataGridViewRow>()
                     .Where(row =>
                    {
                        var cell = row.Cells[0];
                        return cell.Tag is string tags && tags.Contains(editedCellTag);
                    })
                    .ToList();

                Parallel.ForEach(filteredRows, row =>
                {
                    int UV4 = 0;
                    if (int.TryParse(row.Cells[Col1].Value?.ToString(), out int UV1) &&
                        int.TryParse(row.Cells[Col2].Value?.ToString(), out int UV2) &&
                        int.TryParse(row.Cells[Col3].Value?.ToString(), out int UV3) &&
                        (!isScenery || int.TryParse(row.Cells[Col4].Value?.ToString(), out UV4)))
                    {
                        int minUV = isScenery ? Math.Min(UV1, Math.Min(UV2, Math.Min(UV3, UV4))) : Math.Min(UV1, Math.Min(UV2, UV3));
                        int maxUV = isScenery ? Math.Max(UV1, Math.Max(UV2, Math.Max(UV3, UV4))) : Math.Max(UV1, Math.Max(UV2, UV3));

                        UV1 = (UV1 == minUV) ? newMinUV : newMaxUV;
                        UV2 = (UV2 == minUV) ? newMinUV : newMaxUV;
                        UV3 = (UV3 == minUV) ? newMinUV : newMaxUV;
                        if (isScenery) UV4 = (UV4 == minUV) ? newMinUV : newMaxUV;

                        updatedRows.Add((row.Index, UV1, UV2, UV3, UV4));
                    }
                });
            });

            grdTextures.Invoke(() =>
            {
                foreach (var (rowIndex, UV1, UV2, UV3, UV4) in updatedRows)
                {
                    var row = grdTextures.Rows[rowIndex];
                    row.Cells[Col1].Value = UV1;
                    row.Cells[Col2].Value = UV2;
                    row.Cells[Col3].Value = UV3;
                    if (isScenery) row.Cells[Col4].Value = UV4;

                    var texture = model.Textures[rowIndex];
                    if (targetIsX)
                    {
                        texture.Left = newMinUV;
                        texture.Width = newMaxUV - newMinUV;
                    }
                    else
                    {
                        texture.Top = newMinUV;
                        texture.Height = newMaxUV - newMinUV;
                    }

                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }

                targetRow.Cells[ColStart].Value = newMinUV;
                targetRow.Cells[ColLength].Value = newMaxUV - newMinUV;
            });

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
        }

        private async Task UpdateRowsColorModeAsync(int rowIndex, int newValue, string editedCellTag)
        {
            if (rowIndex < 0 || rowIndex >= model.Textures.Count)
                return;

            var targetRow = grdTextures.Rows[rowIndex];
            int oldColorMode = Convert.ToInt32(targetRow.Cells[ColColorMode].Value);

            Stopwatch stopwatch = Stopwatch.StartNew();

            var updatedRows = await Task.Run(() =>
            {
                var result = new ConcurrentBag<(DataGridViewRow Row, int[] UpdatedValues, int newLeft)>();

                var filteredRows = grdTextures.Rows.Cast<DataGridViewRow>()
                    .Where(row =>
                    {
                        var cell = row.Cells[0];
                        return cell.Tag is string tags && tags.Contains(editedCellTag);
                    })
                    .ToList();

                Parallel.ForEach(filteredRows, row =>
                {
                    var texture = model.Textures[row.Index];
                    int newLeft = newLeft = Convert.ToInt32(row.Cells[ColLeft].Value);
                    bool trimed = false;

                    var updatedValues = new int[(isScenery ? ColX4 : ColX3) - ColX1 + 1];

                    if ((int)row.Cells[ColLeft].Value + (int)row.Cells[ColWidth].Value > (256 << (2 - newValue)))
                    {
                        newLeft = (256 << (2 - newValue)) - Convert.ToInt32(row.Cells[ColWidth].Value);
                        trimed = true;
                    }

                    for (int i = ColX1; i <= (isScenery ? ColX4 : ColX3); i++)
                    {
                        int value = Convert.ToInt32(row.Cells[i].Value);
                        if (trimed)
                        {
                            value = newLeft;
                            if (row.Cells[i].Style == maxValueStyle)
                            {
                                value += (int)row.Cells[ColWidth].Value;
                            }
                        }
                        updatedValues[i - ColX1] = value;
                    }

                    result.Add((row, updatedValues, newLeft));
                });

                return result;
            });

            grdTextures.Invoke(() =>
            {
                foreach (var (row, updatedValues, newLeft) in updatedRows)
                {
                    for (int i = ColX1; i <= (isScenery ? ColX4 : ColX3); i++)
                    {
                        row.Cells[i].Value = updatedValues[i - ColX1];
                    }

                    row.Cells[ColLeft].Value = newLeft;
                    //model.Textures[row.Index].Left = newLeft;

                    row.Cells[ColColorMode].Value = Convert.ToByte(newValue);

                    if (Settings.Default.OutputModelTextureInfo)
                        Console.WriteLine($"Tags match in row {row.Index}");
                }
            });

            if (Settings.Default.OutputModelTextureInfo)
                Console.WriteLine($"Finished updating cells with tag: {editedCellTag}");

            stopwatch.Stop();
            Console.WriteLine($"Processing time: {stopwatch.Elapsed.TotalSeconds:F3} seconds");
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
            CreateTextureListColumns();
            UpdateTPageList();

            BGRAMode = true;
            replaceCLUT = true;

            if (grdTextures.Rows.Count > 0)
            {
                UpdateTPageButtons();
                fraSwitches.Enabled = true;
                fraReplace.Enabled = true;
                fraReplaceTexture.Enabled = true;
            }

            numReplaceTo.MouseWheel += new MouseEventHandler(ScrollHandlerFunction);

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
            else
            {
                if (lstTPages.Items.Count > 0)
                {
                    lstTPages.Items[0].Selected = true;
                    dpdTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
                }
                cmdAppendTPage.Enabled = false;
                cmdRemoveTPage.Enabled = false;
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
            if (globalControlMode)
            {
                tglGlobalControl.Switched = false;
                ResetColorSliders();
            }
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

        private void chkScalesAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numScaleX.Hexadecimal =
            numScaleY.Hexadecimal =
            numScaleZ.Hexadecimal = chkScalesShowAsHex.Checked;
        }

        private void numOffsetX_ValueChanged(object sender, EventArgs e)
        {
            model.XOffset = (int)numOffsetX.Value;
        }

        private void numOffsetY_ValueChanged(object sender, EventArgs e)
        {
            model.YOffset = (int)numOffsetY.Value;
        }

        private void numOffsetZ_ValueChanged(object sender, EventArgs e)
        {
            model.ZOffset = (int)numOffsetZ.Value;
        }

        private void chkOffsetsAsHex_CheckedChanged(object sender, EventArgs e)
        {
            numOffsetX.Hexadecimal =
            numOffsetY.Hexadecimal =
            numOffsetZ.Hexadecimal = chkOffsetsShowAsHex.Checked;
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