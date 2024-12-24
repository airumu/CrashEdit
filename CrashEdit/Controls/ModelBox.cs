using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using CrashEdit.Crash;
using HslColor = Cyotek.Windows.Forms.HslColor;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : UserControl
    {
        private ModelEntryController controller;
        private ModelEntry model;

        private float MasterHue => hueColorSlider.Value;
        private float MasterSaturation => saturationColorSlider.Value;
        private float MasterLightness => lightnessColorSlider.Value;
        private bool EditMode { get; set; }

        public ModelBox(ModelEntryController controller)
        {
            this.controller = controller;
            model = controller.ModelEntry;
            InitializeComponent();
            UpdateInfo();


        }

        private void UpdateInfo()
        {
            listView1.Columns.Add("Index");
            listView1.Columns.Add("TPage");
            for (int i = 0; i < model.TPAGCount; ++i)
            {
                ListViewItem newitem = new ListViewItem(i.ToString());
                newitem.SubItems.Add(Entry.EIDToEName(model.GetTPAG(i)));
                listView1.Items.Add(newitem);
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
                    lstColor.Items.Add(lsi);
                    lstCopy.Items.Add((ListViewItem)lsi.Clone());
                }
            }
        }

        private void ResetColorList()
        {
            foreach (ListViewItem item in lstCopy.Items)
            {
                var i = (int)item.Tag;
                SetModelColor(Color.FromArgb(item.BackColor.R, item.BackColor.G, item.BackColor.B), i);
            }
            lstColor.Items.Clear();
            UpdateColorList();
        }

        private void Updatemodelcopy()
        {
            lstCopy.Items.Clear();
            foreach (ListViewItem item in lstColor.SelectedItems)
            {
                lstCopy.Items.Add((ListViewItem)item.Clone());
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
                        listView1.Font, Brushes.Red, e.Bounds, sf);

                    return;
                }

                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                e.DrawText(flags);
            }
        }

        private void UpdateTexture()
        {

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
            var i = lstColor.SelectedIndices[0];
            SetModelColor(color, i);
            lstColor.SelectedItems[0].Text = Convert.ToHexString(new byte[] { color.R, color.G, color.B });
            lstColor.SelectedItems[0].BackColor = color;
            lstColor.SelectedItems[0].ForeColor = getBrightness(lstColor.SelectedItems[0].BackColor) >= 0.5 ? Color.Black : Color.White;
            //Updatemodelcopy();

            colorEditor.Color = color;
            colorWheel.Color = color;
            picPreview.BackColor = color;
        }

        private void UpdateEntireColor()
        {
            if (EditMode)
            {
                foreach (ListViewItem item in lstCopy.Items)
                {
                    var i = (int)item.Tag;
                    Color itemColor = Color.FromArgb(item.BackColor.R, item.BackColor.G, item.BackColor.B);

                    HslColor hslColor = new HslColor(itemColor);

                    hslColor = ChangeHue(hslColor, hslColor.H + (MasterHue - 180));

                    hslColor.S += (double)(MasterSaturation - 50) / 100;
                    hslColor.L += (double)(MasterLightness - 50) / 100;

                    Color newColor = hslColor.ToRgbColor();


                    SetModelColor(newColor, i);
                    //item.Text = Convert.ToHexString(new byte[] { newColor.R, newColor.G, newColor.B });
                    //item.BackColor = newColor;
                    //item.ForeColor = getBrightness(item.BackColor) >= 0.5 ? Color.Black : Color.White;
                }
            }
        }

        private Color GetColorFromSelected()
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
            Color color = GetColorFromSelected();
            colorEditor.Color = color;
            colorWheel.Color = color;
            picPreview.BackColor = color;
        }

        // color brightness as perceived:
        float getBrightness(Color c)
        { return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f; }


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

        private void tbpColors_Click(object sender, EventArgs e)
        {

        }

        private void hueColorSlider_ValueChangedHandler(object sender, EventArgs e)
        {
            if (hueColorSlider.Focused)
            {
                UpdateEntireColor();
            }
        }

        private void saturationColorSlider_ValueChanged(object sender, EventArgs e)
        {
            if (saturationColorSlider.Focused)
            {
                UpdateEntireColor();
            }
        }

        private void lightnessColorSlider_ValueChanged(object sender, EventArgs e)
        {
            if (lightnessColorSlider.Focused)
            {
                UpdateEntireColor();
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

        private void swtEditEntire_SwitchedChanged(object sender)
        {
            EditMode = swtEditEntire.Switched;
            if (EditMode)
            {
                pnSliders.Enabled = false;
                pnEditMode.Enabled = true;
                cmdApply.Enabled = true;
                hueColorSlider.Value = 180;
                saturationColorSlider.Value = 50;
                lightnessColorSlider.Value = 50;
            }
            else
            {
                pnEditMode.Enabled = false;
                cmdApply.Enabled = false;
                ResetColorList();
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstColor.Items)
            {
                var i = (int)item.Tag;
                byte[] item_ = [model.Colors[i].Red, model.Colors[i].Green, model.Colors[i].Blue];
                SetModelColor(item.BackColor, i);
            }
            //Updatemodelcopy();
            swtEditEntire.Switched = false;
        }
    }
}
