using Crash;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class SLSTSourceBox : UserControl
    {
        private ListBox lstValues;

        public SLSTSourceBox(SLSTSource slstitem)
        {
            lstValues = new ListBox
            {
                Dock = DockStyle.Fill
            };
            lstValues.BackColor = Color.FromArgb(30, 30, 30);
            lstValues.ForeColor = Color.FromArgb(220, 220, 220);
            lstValues.BorderStyle = BorderStyle.None;
            lstValues.Items.Add(string.Format("Count: {0}",slstitem.Polygons.Count));
            foreach (SLSTPolygonID value in slstitem.Polygons)
            {
                lstValues.Items.Add(string.Format("Polygon {2}-{0} (World {1})",value.ID,value.World,value.State));
            }
            Controls.Add(lstValues);
        }
    }
}
