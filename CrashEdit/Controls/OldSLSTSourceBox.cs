using Crash;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class OldSLSTSourceBox : UserControl
    {
        private ListBox lstValues;

        public OldSLSTSourceBox(OldSLSTSource slstitem)
        {
            lstValues = new ListBox
            {
                Dock = DockStyle.Fill
            };
            lstValues.BackColor = Color.FromArgb(30, 30, 30);
            lstValues.ForeColor = Color.FromArgb(220, 220, 220);
            lstValues.Items.Add(string.Format("Count: {0}",slstitem.Polygons.Count));
            lstValues.Items.Add(string.Format("Type: {0}",0));
            foreach (OldSLSTPolygonID value in slstitem.Polygons)
            {
                lstValues.Items.Add(string.Format("Polygon {0} (World {1})",value.ID,value.World));
            }
            Controls.Add(lstValues);
        }
    }
}
