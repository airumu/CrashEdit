using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class SLSTSourceBox : UserControl
    {
        private DarkListBox lstValues;

        public SLSTSourceBox(SLSTSource slstitem)
        {
            lstValues = new DarkListBox
            {
                Dock = DockStyle.Fill
            };
            lstValues.Items.Add(string.Format("Count: {0}", slstitem.Polygons.Count));
            foreach (SLSTPolygonID value in slstitem.Polygons)
            {
                lstValues.Items.Add(string.Format("Polygon {2}-{0} (World {1})", value.ID, value.World, value.State));
            }
            Controls.Add(lstValues);
        }
    }
}
