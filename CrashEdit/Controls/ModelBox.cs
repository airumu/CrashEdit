using System;
using System.Globalization;
using System.Windows.Forms;
using AltUI.ColorPicker;
using CrashEdit.Crash;
using Cyotek.Windows.Forms;

namespace CrashEdit.CE.Controls
{
    public partial class ModelBox : System.Windows.Forms.UserControl
    {
        private ModelEntryController controller;
        private ModelEntry model;

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

        private void UpdateColor()
        {
            int colorcount = model.Colors.Count;
            for (int i = 0; i < colorcount; ++i)
            {
                byte[] item = new byte[3];
                item[0] = model.Colors[i].Red;
                item[1] = model.Colors[i].Green;
                item[2] = model.Colors[i].Blue;
                lstColor.Items.Add(Convert.ToHexString(item));
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
            UpdateColor();
        }

        private void tbpTextures_Enter(object sender, EventArgs e)
        {
            UpdateTexture();
        }

        private void UpdateSelectedColor(Color clr)
        {
            SceneryColor updatedColor = model.Colors[lstColor.SelectedIndex];
            updatedColor.Red = clr.R;
            updatedColor.Green = clr.G;
            updatedColor.Blue = clr.B;
            updatedColor.Extra = 0;
            model.Colors[lstColor.SelectedIndex] = updatedColor;

            lstColor.SelectedItem = Convert.ToHexString(new byte[] { clr.R, clr.G, clr.B });
            colorEditor.Color = clr;
            colorWheel.Color = clr;
        }

        private void lstColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hexcolor = lstColor.SelectedItem.ToString();
            int color = Int32.Parse(hexcolor.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb(color);
            int alphat = 255;
            colorEditor.Color = Color.FromArgb(alphat, clr);
            colorWheel.Color = Color.FromArgb(alphat, clr);
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            UpdateSelectedColor(colorEditor.Color);
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            UpdateSelectedColor(colorWheel.Color);
        }
    }
}
