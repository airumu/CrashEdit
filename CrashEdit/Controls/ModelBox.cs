using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using AltUI.Forms;
using CrashEdit.Crash;
using CrashEdit.Crash.GOOLIns;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static CrashEdit.CE.TextureViewer;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        private ModelEntryController controller;
        private ModelEntry model;

        public TexturePageList TPages { get; set; }

        private float MasterHue => hueColorSlider.Value;
        private float MasterSaturation => saturationColorSlider.Value;
        private float MasterLightness => lightnessColorSlider.Value;
        private bool EditMode { get; set; }
        private bool SimpleMode { get; set; }
        private bool Expand { get; set; }

        private TextureChunk chunk;
        private TextureType textype;
        private Rectangle selectedregion;

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

            lstPages.Columns.Add("Index");
            lstPages.Columns.Add("Page");
            for (int i = 0; i < model.TPAGCount; ++i)
            {
                ListViewItem newitem = new ListViewItem(i.ToString());
                newitem.SubItems.Add(Entry.EIDToEName(model.GetTPAG(i)));
                lstPages.Items.Add(newitem);
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
                        lstPages.Font, Brushes.Red, e.Bounds, sf);

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

        private void UpdateTexture()
        {
            grdTextures.SuspendLayout();

            grdTextures.Columns.Add("Page", "Page");
            grdTextures.Columns.Add("ClutX", "Clut X");
            grdTextures.Columns.Add("ClutY", "Clut Y");
            grdTextures.Columns.Add("Left", "X  ");
            grdTextures.Columns.Add("Top", "Y  ");
            grdTextures.Columns.Add("Width", "Width");
            grdTextures.Columns.Add("Height", "Height");
            grdTextures.Columns.Add("BlendMode", "Blend");
            grdTextures.Columns.Add("ColorMode", "Color");
            grdTextures.Columns.Add("X1", "X1"); // 9
            grdTextures.Columns.Add("X2", "X2"); // 10
            grdTextures.Columns.Add("X3", "X3"); // 11
            //grdTextures.Columns.Add("X4", "X4);
            grdTextures.Columns.Add("Y1", "Y1"); // 12
            grdTextures.Columns.Add("Y2", "Y2"); // 13
            grdTextures.Columns.Add("Y3", "Y3"); // 14
            //grdTextures.Columns.Add("Y4", "Y4);
            for (int i = 0; i < model.Textures.Count; ++i)
            {
                var item = model.Textures[i];
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(grdTextures, item.Page, item.ClutX, item.ClutY, item.Left, item.Top, item.Width, item.Height, item.BlendMode, item.ColorMode, item.X1, item.X2, item.X3, item.Y1, item.Y2, item.Y3);
                grdTextures.Rows.Add(row);
            }
            grdTextures.ResumeLayout();
            ToggleExpand();

            if (grdTextures.Rows.Count > 0)
            {
                fraSwitches.Enabled = true;
                fraReplace.Enabled = true;
            }
        }

        private void UpdateTextureList()
        {
            if (grdTextures.IsCurrentCellInEditMode)
                grdTextures.CancelEdit();
            grdTextures.Columns.Clear();
            grdTextures.Rows.Clear();
            UpdateTexture();
        }

        private void grdTextures_SelectionChanged(object sender, EventArgs e)
        {
            if (grdTextures.SelectedCells.Count > 0)
            {
                int rowIndex = grdTextures.SelectedCells[0].RowIndex;
                var item = grdTextures.Rows[rowIndex];

                var pageIndex = Convert.ToInt32(item.Cells[0].Value);
                string cid = lstPages.Items[pageIndex].SubItems[1].Text;

                UpdatePicture(cid, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value, item.Cells[4].Value, item.Cells[5].Value, item.Cells[6].Value, item.Cells[7].Value, item.Cells[8].Value);

                numReplace.Value = Convert.ToInt32(grdTextures.CurrentCell.Value);
                lstPages.SelectedItems.Clear();
                lstPages.Items[pageIndex].Selected = true;
                //lstPages.EnsureVisible(pageIndex);
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
            UpdateTextureList();
            if (SimpleMode)
            {
                RemoveDuplicateRowsExceptColumns(grdTextures, 9, 10, 11, 12, 13, 14);
                tglExpand.Enabled = false;
            }
            else
            {
                tglExpand.Enabled = true;
            }
        }

        private void ToggleExpand()
        {
            if (Expand)
            {
                foreach (DataGridViewColumn column in grdTextures.Columns)
                {
                    if (column.Index >= 3 && column.Index <= 6)
                        column.Visible = false;

                    if (column.Index >= 9)
                        column.Visible = true;
                }
                tglSimpleMode.Enabled = false;
            }
            else
            {
                foreach (DataGridViewColumn column in grdTextures.Columns)
                {
                    if (column.Index >= 3 && column.Index <= 6)
                        column.Visible = true;

                    if (column.Index >= 9)
                        column.Visible = false;
                }
                tglSimpleMode.Enabled = true;
            }
            AdjustColumnWidths();
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            string newValue = numReplace.Value.ToString();
            foreach (DataGridViewCell cell in grdTextures.SelectedCells)
            {
                cell.Value = newValue;
            }
        }

        private void grdTextures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int maxValue = 0;

            // Page
            if (e.ColumnIndex == 0)
                maxValue = lstPages.Items.Count - 1;
            // ClutX
            else if (e.ColumnIndex == 1)
                maxValue = 15;
            // ClutY
            else if (e.ColumnIndex == 2)
                maxValue = 127;
            // X
            else if (e.ColumnIndex == 3)
                maxValue = 1023;
            // Y
            else if (e.ColumnIndex == 4)
                maxValue = 127;
            // Width
            else if (e.ColumnIndex == 5)
                maxValue = 1024;
            // Height
            else if (e.ColumnIndex == 6)
                maxValue = 128;
            // Blend Mode
            else if (e.ColumnIndex == 7)
                maxValue = 3;
            // Color Mode
            else if (e.ColumnIndex == 8)
                maxValue = 2;
            // X 1-3
            else if (e.ColumnIndex >= 9 && e.ColumnIndex <= 11)
                maxValue = 1024;
            // Y 1-3
            else if (e.ColumnIndex >= 12 && e.ColumnIndex <= 14)
                maxValue = 128;

            if (int.TryParse(e.FormattedValue.ToString(), out int value))
            {
                if (value > maxValue)
                {
                    DarkMessageBox.ShowError($"The value must be less than or equal to {maxValue}.", "");
                    e.Cancel = true;
                }
                else if (value < 0)
                {
                    DarkMessageBox.ShowError($"The value must be greater than or equal to 0.", "");
                    e.Cancel = true;
                }
            }
            else
            {
                DarkMessageBox.ShowError($"Invalid input. Please enter an integer.", "");
                e.Cancel = true;
            }
        }

        private void grdTextures_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdTextures_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var item = grdTextures.Rows[e.RowIndex];
            var og = model.Textures[e.RowIndex];

            // Page
            if (e.ColumnIndex == 0)
                og.Page = Convert.ToByte(item.Cells[0].Value);
            // ClutX
            else if (e.ColumnIndex == 1)
                og.ClutX = Convert.ToByte(item.Cells[1].Value);
            // ClutY
            else if (e.ColumnIndex == 2)
            {
                byte value = Convert.ToByte(item.Cells[2].Value);
                og.ClutY1 = (byte)((value & 0x3) << 2);
                og.ClutY2 = (byte)(value >> 2);
            }


            var pageIndex = Convert.ToInt32(item.Cells[0].Value);
            string cid = lstPages.Items[pageIndex].SubItems[1].Text;
            UpdatePicture(cid, item.Cells[1].Value, item.Cells[2].Value, item.Cells[3].Value, item.Cells[4].Value, item.Cells[5].Value, item.Cells[6].Value, item.Cells[7].Value, item.Cells[8].Value);
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
            UpdateTexture();
            tbpTextures.Enter -= tbpTextures_Enter;
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

        private void UpdatePicture(string cid, object texCX, object texCY, object texX, object texY, object texW, object texH, object blendMode, object colorMode)
        {
            int TexCX = Convert.ToInt32(texCX);
            int TexCY = Convert.ToInt32(texCY);
            int TexX = Convert.ToInt32(texX);
            int TexY = Convert.ToInt32(texY);
            int TexW = Convert.ToInt32(texW);
            int TexH = Convert.ToInt32(texH);
            int colormode = Convert.ToInt32(colorMode);
            int blendmode = 3;
            TextureChunk chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(cid));
            int pw = 256 << (2 - colormode);
            int ph = 128;
            // Bitmap bitmap = new Bitmap(pw + 64, ph + 64, PixelFormat.Format32bppArgb); // we give the image some buffer space for the selection graphic
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
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Size = bitmap.Size;
            /*            if (Width != pw + 16)
                            Width = pw + 16;*/
            Width = 1024 + 32;
        }

        private void tglSimpleMode_SwitchedChanged(object sender)
        {
            SimpleMode = tglSimpleMode.Switched;
            ToggleSimpleMode();
        }

        private void tglExpand_SwitchedChanged(object sender)
        {
            Expand = tglExpand.Switched;
            UpdateTextureList();
        }

        private void lblSimpleMode_Click(object sender, EventArgs e)
        {

        }

        private void lstPages_ColumnWidthChangingHandler(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstPages.Columns[e.ColumnIndex].Width;
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
