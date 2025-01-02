using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class OldSLSTSourceBox : UserControl
    {
        private DarkListBox lstValues;

        public OldSLSTSourceBox(OldSLSTSource slstitem)
        {
            BackColor = Color.FromArgb(31, 31, 32);
            lstValues = new DarkListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            lstValues.Items.Add(string.Format("Count: {0}", slstitem.Polygons.Count));
            lstValues.Items.Add(string.Format("Type: {0}", 0));
            foreach (OldSLSTPolygonID value in slstitem.Polygons)
            {
                lstValues.Items.Add(string.Format("Polygon {0} (World {1})", value.ID, value.World));
            }
            Controls.Add(lstValues);
        }
    }
}
