using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class SLSTSourceBox : UserControl
    {
        private DarkListBox lstValues;
        private ContextMenuStrip contextMenu;

        public SLSTSourceBox(SLSTSource slstitem)
        {
            BackColor = Color.FromArgb(31, 31, 32);

            // Initialize the list box
            lstValues = new DarkListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            lstValues.Items.Add(string.Format("Count: {0}", slstitem.Polygons.Count));
            foreach (SLSTPolygonID value in slstitem.Polygons)
            {
                lstValues.Items.Add(string.Format("Polygon {2}-{0} (World {1})", value.ID, value.World, value.State));
            }

            // Initialize the context menu
            contextMenu = new ContextMenuStrip();
            var copyAllMenuItem = new ToolStripMenuItem("Copy All", null, CopyAllToClipboard);
            contextMenu.Items.Add(copyAllMenuItem);

            // Attach the context menu to the list box
            lstValues.ContextMenuStrip = contextMenu;

            Controls.Add(lstValues);
        }

        private void CopyAllToClipboard(object sender, EventArgs e)
        {
            if (lstValues.Items.Count > 0)
            {
                // Concatenate all items into a single string with line breaks
                var allItems = string.Join(Environment.NewLine, lstValues.Items.Cast<string>());
                Clipboard.SetText(allItems); // Copy to clipboard
            }
        }
    }
}
