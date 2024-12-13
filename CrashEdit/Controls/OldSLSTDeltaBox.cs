using AltUI.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class OldSLSTDeltaBox : UserControl
    {
        private DarkListBox lstValues;

        public OldSLSTDeltaBox(OldSLSTDelta slstitem)
        {
            lstValues = new DarkListBox
            {
                Dock = DockStyle.Fill
            };
            lstValues.Items.Add(string.Format("Type: {0}", 1));
            lstValues.Items.Add(string.Format("Remove Nodes: {0}", slstitem.RemoveNodes.Count));
            lstValues.Items.Add(string.Format("Add Nodes: {0}", slstitem.AddNodes.Count));
            lstValues.Items.Add(string.Format("Swap Nodes: {0}", slstitem.SwapNodes.Count));
            Controls.Add(lstValues);
        }
    }
}
