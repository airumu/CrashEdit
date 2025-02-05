using AltUI.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(NSD))]
    public sealed class NSDController : LegacyController
    {
        public NSDController(NSD nsd, SubcontrollerGroup parentGroup) : base(parentGroup, nsd)
        {
            NSD = nsd;
            AddMenu("Show GOOL Map", "ThingCode", Menu_ShowGOOLMap);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new NSDBox(this);
        }

        public NSD NSD { get; }

        private DarkForm? showGOOLMapForm { get; set; }

        public void Kill()
        {
            showGOOLMapForm?.Close();
            showGOOLMapForm?.Dispose();
            showGOOLMapForm = null;
        }

        private void Menu_ShowGOOLMap()
        {
            if (showGOOLMapForm != null)
            {
                showGOOLMapForm.Focus();
                return;
            }
            showGOOLMapForm = new DarkForm()
            {
                Text = $"GOOL Map ({NSD.ID.ToString("X2")})",
                BackColor = Color.FromArgb(31, 31, 32),
                MaximizeBox = false,
                MinimizeBox = false,
                Width = 584,
                Height = 380
            };
            ListView lst = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            List<string> BaseGOOL = new() { "WillC", "WarpC", "FruiC", "DispC", "DoctC", "PartC", "ShadC", "BoxsC", "WEfOC", "EntNC" };
            for (int i = 0; i < NSD.GOOLMap.Length; ++i)
            {
                ListViewItem lsi = new();
                lsi.Text = $"{i:D2}: {Entry.EIDToEName(NSD.GOOLMap[i])}";
                lsi.ForeColor = lsi.Text.Contains(Entry.NullEName) ? SystemColors.ControlDarkDark :
                                BaseGOOL.Any(item => lsi.Text.Contains(item)) ? Color.Turquoise :
                                Color.Gainsboro;
                lst.Items.Add(lsi);
            }
            showGOOLMapForm.Controls.Add(lst);
            showGOOLMapForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                showGOOLMapForm = null;
            };
            showGOOLMapForm.Show();
        }
    }
}
