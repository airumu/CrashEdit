using System.Runtime;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    public sealed class EntryChunkBox : UserControl
    {
        private EntryChunkController controller;

        private int totalsize;

        private MetroSetListBox lstEntryList;

        public EntryChunkBox(EntryChunkController controller)
        {
            this.controller = controller;

            lstEntryList = new MetroSetListBox
            {
                Dock = DockStyle.Fill,
                ItemHeight = 16,
                ShowScrollBar = false,
                Style = MetroSet_UI.Enums.Style.Dark
            };

            Controls.Add(lstEntryList);

            Invalidated += EntryChunkBox_Invalidated;

            PopulateList();
        }

        private void EntryChunkBox_Invalidated(object sender, InvalidateEventArgs e)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            totalsize = 0;
            lstEntryList.Items.Clear();
            foreach (Entry entry in controller.EntryChunk.Entries)
            {
                var this_size = Aligner.Align(entry.Save().Length, controller.EntryChunk.Alignment);
                lstEntryList.Items.Add(string.Format("{0}: {1} bytes", entry.EName, this_size));
                totalsize += this_size;
            }
            lstEntryList.Items.Add(string.Format("Total size: {2} entries, {0} bytes ({1} remaining)", totalsize + 16 + ((controller.EntryChunk.Entries.Count + 1) * 4), Chunk.Length - (totalsize + 16 + ((controller.EntryChunk.Entries.Count + 1) * 4)), controller.EntryChunk.Entries.Count));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                lstEntryList.Dispose();
            }
        }
    }
}
