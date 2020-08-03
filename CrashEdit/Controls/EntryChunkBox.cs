using Crash;
using System.Windows.Forms;
using DarkUI.Controls;
using System.Drawing;

namespace CrashEdit
{
    public sealed class EntryChunkBox : UserControl
    {
        private EntryChunkController controller;

        private int totalsize;

        private DarkListView lstEntryList;

        public EntryChunkBox(EntryChunkController controller)
        {
            this.controller = controller;

            lstEntryList = new DarkUI.Controls.DarkListView { Dock = DockStyle.Fill };

            Controls.Add(lstEntryList);
            
            Invalidated += EntryChunkBox_Invalidated;
        }

        private void EntryChunkBox_Invalidated(object sender, InvalidateEventArgs e)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            totalsize = 0;
            lstEntryList.Items.Clear();
            lstEntryList.Font = new Font("Arial", 9F);
            lstEntryList.BackColor = Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            lstEntryList.ForeColor = SystemColors.Control;
            foreach (Entry entry in controller.EntryChunk.Entries)
            {
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
                var this_size = Aligner.Align(entry.Save().Length, controller.EntryChunk.Alignment);
                var item = new DarkListItem(string.Format("{0}: {1} bytes", entry.EName, this_size));
                lstEntryList.Items.Add(item);
                totalsize += this_size;
            }
            var item2 = new DarkListItem(string.Format("Total size: {2} entries, {0} bytes ({1} remaining)", totalsize + 16 + ((controller.EntryChunk.Entries.Count + 1) * 4), Chunk.Length - (totalsize + 16 + ((controller.EntryChunk.Entries.Count + 1) * 4)), controller.EntryChunk.Entries.Count));
            lstEntryList.Items.Add(item2);
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
