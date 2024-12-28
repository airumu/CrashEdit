using System.Drawing.Imaging;
using System.Globalization;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using static CrashEdit.CE.TextureViewer;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        private ModelEntryController controller;
        private ModelEntry model;
        public TexturePageList TPages { get; set; }
        private TextureChunk chunk { get; set; }

        private TextureType textype;
        private Rectangle selectedregion;

        private bool EditMode;
        private bool SimpleMode;
        private bool IsBGRA;
        private bool ReplaceCLUT;
        private int SelectedRegionX;
        private int SelectedRegionY;

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
            DoubleBuffered = true;
            InitializeComponent();
            UpdateInfo();
        }

        private void UpdateInfo()
        {

            if (model.Positions == null)
                label2.Text = string.Format("Polygon count: {0}\nVertex count: {1}", model.PolyCount, model.VertexCount);
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

        // Draws the backgrounds for entire ListView items.
        private void lstColor_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            bool isSelected = e.Item.Selected;
            e.DrawBackground(); // Draws background

            if (isSelected)
            {
                // Change background color for selected items
                //e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }

            // Draw the item text
            e.DrawText();
        }

        // Draws subitem text and applies content-based formatting.
        private void lstColor_DrawSubItem(object sender,
            DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }

                // Draw the text and background for a subitem with a 
                // negative value. 
                double subItemValue;
                if (e.ColumnIndex > 0 && Double.TryParse(
                    e.SubItem.Text, NumberStyles.Currency,
                    NumberFormatInfo.CurrentInfo, out subItemValue) &&
                    subItemValue < 0)
                {
                    // Unless the item is selected, draw the standard 
                    // background to make it stand out from the gradient.
                    if ((e.ItemState & ListViewItemStates.Selected) == 0)
                    {
                        e.DrawBackground();
                    }

                    // Draw the subitem text in red to highlight it. 
                    e.Graphics.DrawString(e.SubItem.Text,
                        lstTPages.Font, Brushes.Red, e.Bounds, sf);

                    return;
                }

                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                e.DrawText(flags);
            }
        }


        private void AdjustColumnWidths()
        {
            grdTextures.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            // グリッド全体の背景色
            dataGridView.BackgroundColor = Color.FromArgb(30, 30, 30);

            // グリッドの境界線の色
            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            // セルのデフォルトスタイル
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // 列ヘッダーのスタイル
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 行ヘッダーのスタイル
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // 奇数行と偶数行の背景色
            dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);

            // 行の境界線スタイル
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // ヘッダーとグリッド線の表示スタイル
            dataGridView.EnableHeadersVisualStyles = false;

            // その他の設定
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

        private void SetTag(int start, int end)
        {
            int startColumnIndex = start;
            int endColumnIndex = end;

            foreach (DataGridViewRow row in grdTextures.Rows)
            {
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
                            row.Cells[col].Tag = "MaxValue";
                            row.Cells[col].Style.ForeColor = Color.Turquoise;
                        }
                    }
                }
            }
        }

        private void UpdateTextureList()
        {
            grdTextures.SuspendLayout();

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
            //grdTextures.Columns.Add("X4", "X4);
            grdTextures.Columns.Add("Y1", "Y1");
            grdTextures.Columns.Add("Y2", "Y2");
            grdTextures.Columns.Add("Y3", "Y3");
            //grdTextures.Columns.Add("Y4", "Y4);
            grdTextures.Columns.Add("BlendMode", "Blend");
            grdTextures.Columns.Add("ColorMode", "Color");
            for (int i = 0; i < model.Textures.Count; ++i)
            {
                var item = model.Textures[i];
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(grdTextures, item.Page, item.ClutX, item.ClutY, item.Left, item.Top, item.Width, item.Height, item.X1, item.X2, item.X3, item.Y1, item.Y2, item.Y3, item.BlendMode, item.ColorMode);
                grdTextures.Rows.Add(row);
            }
            SetTag(9, 11);
            SetTag(12, 14);
            grdTextures.ResumeLayout();

            foreach (DataGridViewColumn column in grdTextures.Columns)
            {
                if (column.Index >= ColLeft && column.Index <= ColHeight)
                    column.Visible = false;

                if (column.Index >= ColX1 && column.Index <= ColY3)
                    column.Visible = true;
            }
            AdjustColumnWidths();
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

        private void RemoveDuplicateRowsExceptColumns(DataGridView dataGridView, params int[] excludedColumns)
        {
            HashSet<int> excludedColumnSet = new HashSet<int>(excludedColumns);

            HashSet<string> uniqueRows = new HashSet<string>();

            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                string rowData = string.Join(",", dataGridView.Rows[i].Cells.Cast<DataGridViewCell>()
                    .Where((cell, index) => !excludedColumnSet.Contains(index))
                    .Select(cell => cell.Value?.ToString() ?? ""));

                if (uniqueRows.Contains(rowData))
                {
                    // Todo fix
                    dataGridView.Rows.RemoveAt(i);
                }
                else
                {
                    uniqueRows.Add(rowData);
                }
            }
        }

        private void ToggleSimpleMode()
        {
            if (grdTextures.IsCurrentCellInEditMode)
                grdTextures.CancelEdit();

            grdTextures.Columns.Clear();
            grdTextures.Rows.Clear();
            UpdateTextureList();

            if (SimpleMode)
            {
                foreach (DataGridViewColumn column in grdTextures.Columns)
                {
                    if (column.Index >= ColLeft && column.Index <= ColHeight)
                        column.Visible = true;

                    if (column.Index >= ColX1 && column.Index <= ColY3)
                        column.Visible = false;
                }
                RemoveDuplicateRowsExceptColumns(grdTextures, ColX1, ColX2, ColX3, ColY1, ColY2, ColY3);
            }
            else
            {
                foreach (DataGridViewColumn column in grdTextures.Columns)
                {
                    if (column.Index >= ColLeft && column.Index <= ColHeight)
                        column.Visible = false;

                    if (column.Index >= ColX1 && column.Index <= ColY3)
                        column.Visible = true;
                }
            }
            AdjustColumnWidths();
        }

        private void ReplaceTextureFromFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.bmp;*.png;|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath).ToLower();
                    bool Failed = false;
                    switch (extension)
                    {
                        case ".bmp":
                            try
                            {
                                var result = ImageProcessor.ProcessBmp(filePath);
                                ReplaceTexture(result.rawImageData, result.palette, result.width, result.height);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        case ".png":
                            try
                            {
                                var result = ImageProcessor.ProcessPng(filePath, IsBGRA);
                                ReplaceTexture(result.rawImageData, result.palette, result.width, result.height);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;
                        default:
                            Console.WriteLine("The selected file is not a supported image format.");
                            Failed = true;
                            break;
                    }

                    if (Failed)
                    {
                        Console.WriteLine("Failed to process the image file.");
                    }
                }
            }
        }

        private void ReplaceTexture(byte[] _rawImageData, byte[] _palette, int _width, int _height)
        {
            byte[] rawImageData = _rawImageData;
            byte[] palette = _palette;
            int width = _width;
            int height = _height;

            byte[] rgba5551List = ImageProcessor.ConvertPaletteToRGBA5551(palette);
            int paletteCount = rgba5551List.Length / 2;
            int bpp = (paletteCount <= 16) ? 4 : 8;

            byte[] currentData = chunk.Data;

            int destX = SelectedRegionX;
            int clutX = 0, clutY = 0, old_bpp = 0;
            if (grdTextures.SelectedCells.Count > 0)
            {
                var row = grdTextures.Rows[grdTextures.SelectedCells[0].RowIndex];
                if (Convert.ToInt32(row.Cells[ColColorMode].Value) == 1)
                {
                    destX *= 2;
                    old_bpp = 8;
                }
                else
                {
                    old_bpp = 4;
                }
                clutX = Convert.ToInt32(row.Cells[ColClutX].Value);
                clutY = Convert.ToInt32(row.Cells[ColClutY].Value);
            }
            byte[] newTextureData = ImageProcessor.CopyTexture(rawImageData, currentData, width, height, bpp, 0, 0, width, height, destX / (bpp == 8 ? 2 : 1), SelectedRegionY);
            chunk.Data = newTextureData;
            WriteResult(rgba5551List, rawImageData, paletteCount, bpp, width, height);

            if (ReplaceCLUT)
            {
                bool doProcess = true;
                if (bpp != old_bpp)
                {
                    if (DarkMessageBox.ShowWarning("The color depth of the selected image differs from the current one. Do you want to continue anyway?", "", DarkDialogButton.YesNo) == DialogResult.Yes)
                        doProcess = true;
                    else
                        doProcess = false;
                }

                if (doProcess)
                {
                    int offset = clutX * 0x20 + clutY * 0x200;
                    Array.Copy(rgba5551List, 0, chunk.Data, offset, rgba5551List.Length);
                    Console.WriteLine("CLUT replacement completed.");
                }
                else
                {
                    Console.WriteLine("CLUT replacement cancelled.");
                }
            }
            BitConv.ToInt32(chunk.Data, 12, Chunk.CalculateChecksum(chunk.Data));
            UpdatePicture();
        }

        private void WriteResult(byte[] rgba5551List, byte[] rawImageData, int colorCount, int bpp, int width, int height)
        {
            Console.WriteLine($"Raw Image Data Length: {rawImageData.Length}");
            Console.WriteLine($"Image Size: {width} x {height}");
            Console.WriteLine($"Palette Count: {colorCount}, {bpp}bpp");
            string hexString = BitConverter.ToString(rgba5551List).Replace("-", "");
            Console.WriteLine($"Palette (RGBA5551 format):\r\n{hexString}");
        }

        private void cmdReplaceTexture_Click(object sender, EventArgs e)
        {
            ReplaceTextureFromFile();
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            //byte valueFrom = (byte)numReplace.Value;
            //byte valueTo = (byte)numReplaceTo.Value;
            //foreach (DataGridViewCell cell in grdTextures.SelectedCells)
            //{
            //    if (cell.Value== valueFrom.ToString())
            //        cell.Value = valueTo;
            //}
            int index = (int)numRowIndex.Value;
            int? col = grdTextures.CurrentCell?.ColumnIndex;
            if (col >= ColX1 && col <= ColX3)
                UpdateRowsX(index, (int)numReplaceTo.Value, (int)numReplaceTo.Value, (int)numReplaceTo.Value + (int)grdTextures.Rows[index].Cells[ColWidth].Value);
            else if (col >= ColY1 && col <= ColY3)
                UpdateRowsY(index, (int)numReplaceTo.Value, (int)numReplaceTo.Value, (int)numReplaceTo.Value + (int)grdTextures.Rows[index].Cells[ColHeight].Value);
            UpdatePicture();

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
                maxValue = 1023;
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
                maxValue = 1024;
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
                int value = Convert.ToInt32(item.Cells[ColLeft].Value);
                int width = Convert.ToInt32(item.Cells[ColWidth].Value) - 1; // fake value
                int U1 = og.U1, U2 = og.U2, U3 = og.U3;
                int minU = Math.Min(U1, Math.Min(U2, U3));

                int segment, xoff;
                GetXOff(colorMode, value, out segment, out xoff);
                int newU = value - xoff;

                //if (U1 == minU) U1 = newU; else U1 = newU + width;
                //if (U2 == minU) U2 = newU; else U2 = newU + width;
                //if (U3 == minU) U3 = newU; else U3 = newU + width;
                int[] ints = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    if (item.Cells[i + ColX1].Tag != null && item.Cells[i + ColX1].Tag.ToString() == "MaxValue")
                        ints[i] = width;
                }
                og.U1 = (byte)(newU + ints[0]);
                og.U2 = (byte)(newU + ints[1]);
                og.U3 = (byte)(newU + ints[2]);
                og.Segment = (byte)segment;

                UpdateRowsX(e.RowIndex, og.U1, og.U2, og.U3);

                //Console.WriteLine($"Recalculated U1: {U1}, U2: {U2}, U3: {U3}, xoffUnit: {xoffUnit}, segment: {segment}, xoff: {xoff}");
            }
            // Y (Top), Height
            else if (e.ColumnIndex == ColTop || e.ColumnIndex == ColHeight)
            {
                int value = Convert.ToInt32(item.Cells[ColTop].Value);
                int height = Convert.ToInt32(item.Cells[ColHeight].Value) - 1; // fake value
                int V1 = og.V1, V2 = og.V2, V3 = og.V3;
                int minV = Math.Min(V1, Math.Min(V2, V3));

                int newV = value;
                int[] ints = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    if (item.Cells[i + ColY1].Tag != null && item.Cells[i + ColY1].Tag.ToString() == "MaxValue")
                        ints[i] = height;
                }
                og.V1 = (byte)(newV + ints[0]);
                og.V2 = (byte)(newV + ints[1]);
                og.V3 = (byte)(newV + ints[2]);
            }
            // Blend Mode
            else if (e.ColumnIndex == ColBlendMode)
                og.ClutX = Convert.ToByte(item.Cells[ColBlendMode].Value);
            // Color Mode
            else if (e.ColumnIndex == ColColorMode)
                og.ClutX = Convert.ToByte(item.Cells[ColColorMode].Value);
            // X1, X2, X3
            else if (e.ColumnIndex >= ColX1 && e.ColumnIndex <= ColX3)
            {
                int value = Convert.ToInt32(item.Cells[e.ColumnIndex].Value);
                int U1 = og.U1, U2 = og.U2, U3 = og.U3;
                int minU = Math.Min(U1, Math.Min(U2, U3));

                int segment, xoff;
                GetXOff(colorMode, value, out segment, out xoff);
                int newU = value - xoff;

                if (item.Cells[e.ColumnIndex].Tag != null && item.Cells[e.ColumnIndex].Tag.ToString() == "MaxValue")
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
                int minV = Math.Min(V1, Math.Min(V2, V3));

                int newV = value;
                if (item.Cells[e.ColumnIndex].Tag != null && item.Cells[e.ColumnIndex].Tag.ToString() == "MaxValue")
                    newV--;

                if (e.ColumnIndex == ColY1) og.V1 = (byte)newV;
                else if (e.ColumnIndex == ColY2) og.V2 = (byte)newV;
                else if (e.ColumnIndex == ColY3) og.V3 = (byte)newV;
            }

            var pageIndex = Convert.ToInt32(item.Cells[ColPage].Value);
            string cid = lstTPages.Items[pageIndex].SubItems[1].Text;
            UpdatePicture();
        }

        private void UpdateRowsX(int targetRowIndex, int newX1, int newX2, int newX3)
        {
            var targetRow = grdTextures.Rows[targetRowIndex];
            var targetClutX = targetRow.Cells[ColClutX].Value?.ToString();
            var targetClutY = targetRow.Cells[ColClutY].Value?.ToString();

            if (targetClutX == null || targetClutY == null)
                return;

            int newMinU = Math.Min(newX1, Math.Min(newX2, newX3));
            int newMaxU = Math.Max(newX1, Math.Max(newX2, newX3));

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
                    }
                }
            }
        }


        private void UpdateRowsY(int targetRowIndex, int newY1, int newY2, int newY3)
        {
            var targetRow = grdTextures.Rows[targetRowIndex];
            var targetClutX = targetRow.Cells[ColClutX].Value?.ToString();
            var targetClutY = targetRow.Cells[ColClutY].Value?.ToString();

            if (targetClutX == null || targetClutY == null)
                return;

            int newMinV = Math.Min(newY1, Math.Min(newY2, newY3));
            int newMaxV = Math.Max(newY1, Math.Max(newY2, newY3));

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
            SetDarkTheme(grdTextures);
            EnableDoubleBuffering();
            UpdateTPageList();
            UpdateTextureList();
            UpdateTPageButtons();

            IsBGRA = true;
            ReplaceCLUT = true;
            chkOutput.Checked = Settings.Default.OutputCopyTextureResult;

            lblEIDError.Text = string.Empty;
            if (lstTPages.Items.Count > 0)
                txtTPage.Enabled = true;

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
            if (EditMode)
            {
                for (int i = 0; i < lstColor.Items.Count; i++)
                {
                    byte[] item = StringToByteArray(lstColor.Items[i].SubItems[1].Text);
                    Color itemColor = (Color.FromArgb(item[0], item[1], item[2]));

                    HslColor hslColor = new HslColor(itemColor);

                    hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue - 180));
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
            EditMode = tglGlobalControl.Switched;
            if (EditMode)
            {
                pnSliders.Enabled = false;
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = true;
                hueColorSlider.Value = 180;
                saturationColorSlider.Value = 50;
                lightnessColorSlider.Value = 50;
            }
            else
            {
                pnGlobalControl.Enabled =
                cmdApply.Enabled =
                cmdCancel.Enabled = false;
                ResetColorList();
            }
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
            if (lstColor.SelectedItems.Count <= 0 || EditMode)
            {
                pnSliders.Enabled = false;
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
            if (!EditMode)
                UpdateSelectedColor(colorEditor.Color);
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            if (!EditMode)
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
        }

        private void tbpColors_Leave(object sender, EventArgs e)
        {
            tglGlobalControl.Switched = false;
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
                SelectedRegionX = x;
                SelectedRegionY = y;
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Size = bitmap.Size;
            Width = 1024 + 32;
        }

        private void tglSimpleMode_SwitchedChanged(object sender)
        {
            SimpleMode = tglSimpleMode.Switched;
            ToggleSimpleMode();
        }

        private void lstPages_ColumnWidthChangingHandler(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstTPages.Columns[e.ColumnIndex].Width;
        }

        private void chkBGRA_CheckedChanged(object sender, EventArgs e)
        {
            IsBGRA = chkBGRA.Checked;
        }

        private void chkOutput_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OutputCopyTextureResult = chkOutput.Checked;
            Settings.Default.Save();
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

        private void txtTPage_TextChanged(object sender, EventArgs e)
        {
            lblEIDError.Text = Entry.CheckEIDErrors(txtTPage.Text, true);
        }

        private void UpdateEID()
        {
            if (lblEIDError.Text != string.Empty) return;

            lstTPages.SelectedItems[0].SubItems[1].Text = txtTPage.Text;
            model.SetTPAG(lstTPages.SelectedIndices[0], Entry.ENameToEID(txtTPage.Text));
        }

        private void txtTPage_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                UpdateEID();
        }

        private void txtTPage_LostFocus(object? sender, EventArgs e)
        {
            UpdateEID();
        }

        private void lstTPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTPages.Items.Count > 0 && lstTPages.SelectedItems.Count > 0)
                txtTPage.Text = lstTPages.SelectedItems[0].SubItems[1].Text;
        }

        private void chkReplaceCLUT_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceCLUT = chkReplaceCLUT.Checked;
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

    public class ImageProcessor
    {
        const int VRAMWidth = 512;
        const int VRAMHeight = 128;
        private static byte[] vram = new byte[VRAMWidth * VRAMHeight];
        const int BufferSize = 65536;

        public static byte[] CopyTexture(byte[] texture1, byte[] texture2, int textureWidth, int textureHeight, int bpp, int srcX, int srcY, int width, int height, int destX, int destY)
        {
            bool is8bpp = (bpp == 8);

            //if (textureWidth != VRAMWidth * (is8bpp ? 1 : 2))
            //    CreateBufferFromTexture(texture1, textureWidth, textureHeight, is8bpp);
            //else
            //    vram = texture1;
            CreateBufferFromTexture(texture1, textureWidth, textureHeight, is8bpp);

            if (bpp == 4)
            {
                for (int i = 0; i < height; i++)
                {
                    int srcOffset = (i + srcY) * 0x200 + srcX / 2;
                    int destOffset = (i + destY) * 0x200 + destX / 2;

                    int byteCount = (width + 1) / 2;
                    for (int j = 0; j < byteCount; j++)
                    {
                        byte srcByte = vram[srcOffset + j];
                        byte reversedByte = (byte)(((srcByte & 0xF) << 4) | ((srcByte & 0xF0) >> 4));
                        texture2[destOffset + j] = reversedByte;
                    }
                }
            }
            else if (bpp == 8)
            {
                for (int i = 0; i < height; i++)
                {
                    int offset1 = (i + srcY) * 0x200 + srcX;
                    int offset2 = (i + destY) * 0x200 + destX;
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"i: {i:D2} offset1: {offset1:D5}, offset2: {offset2:D5}");
                    Array.Copy(vram, offset1, texture2, offset2, width);
                }
            }
            else
            {
                Console.WriteLine("Unsupported bpp value.");
            }

            return texture2;
        }

        public static void CreateBufferFromTexture(byte[] texture1, int texture1Width, int texture1Height, bool is8bpp)
        {
            Console.WriteLine();
            Array.Clear(vram, 0, vram.Length);

            int bytesPerPixel = is8bpp ? 1 : 2;
            int rowBytes = is8bpp ? texture1Width : (texture1Width + 1) / 2;

            for (int y = 0; y < texture1Height; y++)
            {
                int sourceOffset = y * rowBytes;
                int destinationOffset = y * VRAMWidth;

                if (sourceOffset + rowBytes <= texture1.Length && destinationOffset + rowBytes <= vram.Length)
                {
                    if (Settings.Default.OutputCopyTextureResult)
                        Console.WriteLine($"y: {y:D2} sourceOffset: {sourceOffset:D5}, destinationOffset: {destinationOffset:D5}");
                    Array.Copy(texture1, sourceOffset, vram, destinationOffset, rowBytes);
                }
                else
                {
                    Console.WriteLine($"Error: Out of bounds copy. sourceOffset: {sourceOffset}, destinationOffset: {destinationOffset}");
                }
                //File.WriteAllBytes("raw_vram.bin", vram); // debug
            }
        }

        public static (byte[] rawImageData, byte[] palette, int width, int height) ProcessBmp(string filePath)
        {
            byte[] bmpData = File.ReadAllBytes(filePath);
            int width = BitConverter.ToInt32(bmpData, 18);
            int height = BitConverter.ToInt32(bmpData, 22);
            int offset = BitConverter.ToInt32(bmpData, 10);
            int bitsPerPixel = BitConverter.ToInt16(bmpData, 28);

            if (bitsPerPixel != 4 && bitsPerPixel != 8)
            {
                throw new InvalidOperationException("The loaded image is not 4bpp or 8bpp.");
            }

            int rowSize, paletteSize;
            if (bitsPerPixel == 4)
            {
                rowSize = (width + 1) / 2;
                paletteSize = 16 * 4;
            }
            else
            {
                rowSize = width;
                paletteSize = 256 * 4;
            }
            int pixelDataOffset = BitConverter.ToInt32(bmpData, 10);
            int pixelDataSize = rowSize * height;

            if (bmpData.Length < 54 + paletteSize)
            {
                throw new InvalidOperationException("The BMP file is corrupted or incomplete.");
            }
            byte[] paletteData = new byte[paletteSize];
            Array.Copy(bmpData, 54, paletteData, 0, paletteSize);

            byte[] pixelData = new byte[pixelDataSize];
            Array.Copy(bmpData, offset, pixelData, 0, pixelDataSize);

            byte[] rawImageData = new byte[pixelDataSize];

            for (int y = 0; y < height; y++)
            {
                int flippedY = height - 1 - y;
                int srcOffset = y * rowSize;
                int destOffset = flippedY * rowSize;

                Array.Copy(pixelData, srcOffset, rawImageData, destOffset, rowSize);
            }

            return (rawImageData, paletteData, width, height);
        }

        public static byte[] ConvertPaletteToRGBA5551(byte[] palette)
        {
            int paletteSize = palette.Length / 4;
            byte[] convertedPalette = new byte[paletteSize * 2];

            for (int i = 0; i < paletteSize; i++)
            {
                byte r = palette[i * 4];
                byte g = palette[i * 4 + 1];
                byte b = palette[i * 4 + 2];
                byte a = palette[i * 4 + 3];

                ushort rgba5551 = ConvertToRGBA5551(r, g, b, a);
                convertedPalette[i * 2] = (byte)(rgba5551 & 0xFF);
                convertedPalette[i * 2 + 1] = (byte)((rgba5551 >> 8) & 0xFF);
            }

            return convertedPalette;
        }

        private static ushort ConvertToRGBA5551(byte r, byte g, byte b, byte a)
        {
            ushort rgba5551 = 0;

            rgba5551 |= (ushort)((r >> 3) << 10);  // Red: 5 bits
            rgba5551 |= (ushort)((g >> 3) << 5);   // Green: 5 bits
            rgba5551 |= (ushort)((b >> 3));        // Blue: 5 bits
            rgba5551 |= (ushort)((a > 0 ? 1 : 0) << 15);  // Alpha: 1 bit (opaque)

            return rgba5551;
        }

        public static (byte[] rawImageData, byte[] palette, int width, int height) ProcessPng(string filePath, bool isBGRA)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                if (bitmap.PixelFormat != PixelFormat.Format4bppIndexed &&
                    bitmap.PixelFormat != PixelFormat.Format8bppIndexed)
                {
                    throw new InvalidOperationException($"Unsupported pixel format: {bitmap.PixelFormat}");
                }

                if (bitmap.Width <= 0 || bitmap.Height <= 0)
                {
                    throw new InvalidOperationException("Invalid image dimensions.");
                }

                ColorPalette palette = bitmap.Palette;
                Color[] paletteColors = new Color[palette.Entries.Length];
                if (isBGRA)
                {
                    for (int i = 0; i < palette.Entries.Length; i++)
                    {
                        Color color = palette.Entries[i];
                        paletteColors[i] = Color.FromArgb(color.A, color.B, color.G, color.R);
                    }
                }
                else
                {
                    paletteColors = palette.Entries;
                }

                byte[] paletteData = new byte[palette.Entries.Length * 4];
                int index = 0;
                foreach (Color color in paletteColors)
                {
                    paletteData[index++] = color.R;
                    paletteData[index++] = color.G;
                    paletteData[index++] = color.B;
                    paletteData[index++] = color.A;
                }

                byte[] rawImageData = ExtractRawImageData(bitmap);
                return (rawImageData, paletteData, bitmap.Width, bitmap.Height);
            }
        }

        private static byte[] ExtractRawImageData(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bitsPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat);
            int bytesPerPixel = bitsPerPixel / 8;
            int byteWidth = (bitmap.Width * bitsPerPixel + 7) / 8;

            if (byteWidth <= 0 || bitmap.Height <= 0)
            {
                throw new InvalidOperationException("Invalid image dimensions.");
            }

            byte[] rawImageData = new byte[byteWidth * bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                IntPtr rowPtr = bitmapData.Scan0 + y * bitmapData.Stride;
                System.Runtime.InteropServices.Marshal.Copy(rowPtr, rawImageData, y * byteWidth, byteWidth);
            }

            bitmap.UnlockBits(bitmapData);
            return rawImageData;
        }

    }
 
}