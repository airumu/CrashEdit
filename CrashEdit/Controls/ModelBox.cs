using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;
using CrashEdit.Crash;
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

            lstTex.Columns.Add("Index");
            lstTex.Columns.Add("TPage");
            for (int i = 0; i < model.TPAGCount; ++i)
            {
                ListViewItem newitem = new ListViewItem(i.ToString());
                newitem.SubItems.Add(Entry.EIDToEName(model.GetTPAG(i)));
                lstTex.Items.Add(newitem);
            }
        }

        private void UpdateColorList()
        {
            //lstColor.View = View.Details;
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
                        lstTex.Font, Brushes.Red, e.Bounds, sf);

                    return;
                }

                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                e.DrawText(flags);
            }
        }



        private void UpdateTexture()
        {

            lstTextures.Columns.Add("ClutX");
            lstTextures.Columns.Add("ClutY");
            lstTextures.Columns.Add("Left");
            lstTextures.Columns.Add("Top");
            lstTextures.Columns.Add("Width");
            lstTextures.Columns.Add("Height");
            lstTextures.Columns.Add("BlendMode");
            lstTextures.Columns.Add("ColorMode");
            lstTextures.Columns.Add("Page");
            for (int i = 0; i < model.Textures.Count; ++i)
            {
                var item = model.Textures[i];
                ListViewItem lsi = new(item.ClutX.ToString());
                lsi.SubItems.Add(item.ClutY.ToString());
                lsi.SubItems.Add(item.Left.ToString());
                lsi.SubItems.Add(item.Top.ToString());
                lsi.SubItems.Add(item.Width.ToString());
                lsi.SubItems.Add(item.Height.ToString());
                lsi.SubItems.Add(item.BlendMode.ToString());
                lsi.SubItems.Add(item.ColorMode.ToString());
                lsi.SubItems.Add(item.Page.ToString());
                lstTextures.Items.Add(lsi);
            }
        }

        private void lstTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTextures.SelectedItems.Count <= 0) return;

            int i = lstTextures.SelectedItems[0].Index;
            var item = lstTextures.Items[i];
            var index = Convert.ToInt32(item.SubItems[8].Text);
            string cid = lstTex.Items[index].SubItems[1].Text;
            UpdatePicture(cid, Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(item.SubItems[2].Text), Convert.ToInt32(item.SubItems[3].Text), Convert.ToInt32(item.SubItems[4].Text), Convert.ToInt32(item.SubItems[5].Text));
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hitTestInfo = lstTextures.HitTest(e.Location);
            var clickedItem = hitTestInfo.Item;
            var clickedSubItem = hitTestInfo.SubItem;

            if (clickedItem != null && clickedSubItem != null)
            {
                int subItemIndex = clickedItem.SubItems.IndexOf(clickedSubItem);

                numEdit.Tag = new ListViewEditInfo(clickedItem, subItemIndex);
                numEdit.Enabled = true;
                numEdit.Value = Convert.ToInt32(clickedSubItem.Text);
                numEdit.Select(0, numEdit.Text.Length);
                numEdit.Focus();
            }
        }

        private void NumEdit_LostFocus(object sender, EventArgs e)
        {
            ApplyEdit(true);
        }

        private void NumEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ApplyEdit(true);
            if (e.KeyCode == Keys.Escape)
                ApplyEdit(false);
        }

        private void ApplyEdit(bool apply)
        {
            if (numEdit.Tag is ListViewEditInfo editInfo)
            {
                if (apply)
                {
                    int value = (int)numEdit.Value;
                    editInfo.Item.SubItems[editInfo.SubItemIndex].Text = value.ToString();
                }

                numEdit.Tag = null;
            }
            numEdit.Enabled = false;
        }

        private void UpdateTextureList()
        {
            if (Expand)
            {
                lstTextures.Columns.Clear();
                lstTextures.Columns.Add("ClutX");
                lstTextures.Columns.Add("ClutY");
                lstTextures.Columns.Add("X1");
                lstTextures.Columns.Add("X2");
                lstTextures.Columns.Add("X3");
                //lstTextures.Columns.Add("X4");
                lstTextures.Columns.Add("Y1");
                lstTextures.Columns.Add("Y2");
                lstTextures.Columns.Add("Y3");
                //lstTextures.Columns.Add("Y4");

                lstTextures.Items.Clear();
                for (int i = 0; i < model.Textures.Count; ++i)
                {
                    var item = model.Textures[i];
                    ListViewItem lsi = new ListViewItem(item.ClutX.ToString());
                    lsi.SubItems.Add(item.ClutY.ToString());
                    lsi.SubItems.Add(item.X1.ToString());
                    lsi.SubItems.Add(item.X2.ToString());
                    lsi.SubItems.Add(item.X3.ToString());
                    //lsi.SubItems.Add(item.X4.ToString());
                    lsi.SubItems.Add(item.Y1.ToString());
                    lsi.SubItems.Add(item.Y2.ToString());
                    lsi.SubItems.Add(item.Y3.ToString());
                    //lsi.SubItems.Add(item.Y4.ToString());
                    lstTextures.Items.Add(lsi);
                }
            }
            else
            {
                lstTextures.Columns.Clear();
                lstTextures.Columns.Add("ClutX");
                lstTextures.Columns.Add("ClutY");
                lstTextures.Columns.Add("Left");
                lstTextures.Columns.Add("Top");
                lstTextures.Columns.Add("Width");
                lstTextures.Columns.Add("Height");
                lstTextures.Columns.Add("BlendMode");
                lstTextures.Columns.Add("ColorMode");
                lstTextures.Columns.Add("Page");

                lstTextures.Items.Clear();
                for (int i = 0; i < model.Textures.Count; ++i)
                {
                    var item = model.Textures[i];
                    ListViewItem lsi = new ListViewItem(item.ClutX.ToString());
                    lsi.SubItems.Add(item.ClutY.ToString());
                    lsi.SubItems.Add(item.Left.ToString());
                    lsi.SubItems.Add(item.Top.ToString());
                    lsi.SubItems.Add(item.Width.ToString());
                    lsi.SubItems.Add(item.Height.ToString());
                    lsi.SubItems.Add(item.Page.ToString());
                    lsi.SubItems.Add(item.BlendMode.ToString());
                    lsi.SubItems.Add(item.ColorMode.ToString());
                    lstTextures.Items.Add(lsi);
                }
            }
        }

        private void TrimTextureList()
        {
            if (SimpleMode)
            {
                RemoveDuplicateNodes(lstTextures);

                //// Output result for demonstration
                //foreach (ListViewItem item in lstTextures.Items)
                //{
                //    Console.WriteLine(string.Join(", ", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(sub => sub.Text)));
                //}
            }
        }


        private void cmdEditAll_Click(object sender, EventArgs e)
        {
        }

        public static void RemoveDuplicateNodes(ListView listView)
        {
            var uniqueItems = new HashSet<string>();
            var itemsToRemove = new List<ListViewItem>();

            // Gather duplicate items
            foreach (ListViewItem item in listView.Items)
            {
                var key = string.Join(",", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(sub => sub.Text));

                if (!uniqueItems.Add(key))
                {
                    itemsToRemove.Add(item);
                }
            }

            // Disable updates to avoid UI flickering
            listView.BeginUpdate();

            // Deselect all items to avoid selection errors
            listView.SelectedItems.Clear();

            // Remove duplicate items
            foreach (var item in itemsToRemove)
            {
                listView.Items.Remove(item);
            }

            // Re-enable updates
            listView.EndUpdate();
        }

        private void listView1_Click(object? sender, EventArgs e)
        {

        }

        private void tbpColors_Enter(object sender, EventArgs e)
        {
            UpdateColorList();
            tbpColors.Enter -= tbpColors_Enter;
        }

        private void tbpTextures_Enter(object sender, EventArgs e)
        {
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

        private void UpdatePicture(string cid, int TexCX, int TexCY, int TexX, int TexY, int TexW, int TexH)
        {
            TextureChunk chunk = controller.GetEntry<TextureChunk>(Entry.ENameToEID(cid));
            int pw = 1024;
            int ph = 128;
            // Bitmap bitmap = new Bitmap(pw + 64, ph + 64, PixelFormat.Format32bppArgb); // we give the image some buffer space for the selection graphic
            Bitmap bitmap = new Bitmap(pw + 2, ph + 2, PixelFormat.Format32bppArgb);
            Rectangle brect = new Rectangle(Point.Empty, bitmap.Size);
            BitmapData bdata = bitmap.LockBits(brect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] palette = null;
            int colormode = 0;
            int blendmode = 3;
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
            TrimTextureList();
        }

        private void tglExpand_SwitchedChanged(object sender)
        {
            Expand = tglExpand.Switched;
            UpdateTextureList();
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
