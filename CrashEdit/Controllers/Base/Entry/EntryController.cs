using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(Entry))]
    public class EntryController : LegacyController
    {
        public EntryController(Entry entry, SubcontrollerGroup parentGroup) : base(parentGroup, entry)
        {
            Entry = entry;
            AddMenu(string.Format(CrashUI.Properties.Resources.EntryController_AcRename, entry.EName), "Modify", Menu_Rename_Entry);
            AddMenu(string.Format(CrashUI.Properties.Resources.EntryController_AcDuplicate, entry.EName), "Copy", Menu_Duplicate_Entry);
            if (this is not UnprocessedEntryController)
            {
                AddMenuSeparator();
                AddMenu(string.Format(CrashUI.Properties.Resources.EntryController_AcDeprocess, entry.EName), "Pinion", Menu_Unprocess_Entry);
                AddMenu(string.Format(CrashUI.Properties.Resources.EntryController_AcReload, entry.EName), "ArrowRefresh", Menu_Reload_Entry);
                AddMenu(string.Format("Replace Entry", entry.EName), "ImportPlus", Menu_Replace_Entry);
            }
        }

        protected EntryChunkController EntryChunkController => (EntryChunkController)Modern.Parent.Legacy;
        public Entry Entry { get; }

        public override bool CanMoveTo(CrashEdit.LegacyController newcontroller)
        {
            if (newcontroller is EntryChunkController)
            {
                return true;
            }
            else
            {
                return base.CanMoveTo(newcontroller);
            }
        }

        public override void MoveTo(CrashEdit.LegacyController newcontroller)
        {
            if (newcontroller is EntryChunkController newecc)
            {
                EntryChunkController.EntryChunk.Entries.Remove(Entry);
                newecc.EntryChunk.Entries.Add(Entry);
            }
            else
            {
                base.MoveTo(newcontroller);
            }
        }

        protected T FindEID<T>(int eid) where T : class, IEntry
        {
            return GetEntry<T>(eid);
        }

        private void Menu_Unprocess_Entry()
        {
            int index = EntryChunkController.EntryChunk.Entries.IndexOf(Entry);
            UnprocessedEntry unprocessedentry = Entry.Unprocess();
            EntryChunkController.EntryChunk.Entries[index] = unprocessedentry;
        }

        private void Menu_Rename_Entry()
        {
            using (NewEntryForm newentrywindow = new NewEntryForm(GetNSF(), GameVersion))
            {
                newentrywindow.Text = "Rename Entry";
                newentrywindow.SetRenameMode(Entry.EName);
                if (newentrywindow.ShowDialog() == DialogResult.OK)
                {
                    Entry.EID = newentrywindow.EID;
                    EntryChunkController.NeedsNewEditor = true;
                    //LegacyVerbs[0]._text = string.Format(CrashUI.Properties.Resources.EntryController_AcRename, Entry.EName);
                    //LegacyVerbs[1]._text = string.Format(CrashUI.Properties.Resources.EntryController_AcDuplicate, Entry.EName);
                    //if (this is not UnprocessedEntryController)
                    //    LegacyVerbs[2]._text = string.Format(CrashUI.Properties.Resources.EntryController_AcDeprocess, Entry.EName);
                    //else
                    //    LegacyVerbs[2]._text = string.Format(CrashUI.Properties.Resources.UnprocessedEntryController_AcProcess, Entry.EName);
                }
            }
        }

        private void Menu_Duplicate_Entry()
        {
            using (NewEntryForm newentrywindow = new NewEntryForm(GetNSF(), GameVersion))
            {
                newentrywindow.Text = "Duplicate Entry";
                newentrywindow.SetRenameMode(Entry.EName);
                if (newentrywindow.ShowDialog() == DialogResult.OK)
                {
                    // create a clone by unprocessing and then reloading
                    UnprocessedEntry unprocessed = Entry.Unprocess();
                    UnprocessedEntry clonedUnprocessed = unprocessed.Clone(newentrywindow.EID);
                    clonedUnprocessed.EID = newentrywindow.EID;

                    Entry clonedProcessed;
                    try
                    {
                        clonedProcessed = clonedUnprocessed.Process(GameVersion);
                    }
                    catch (LoadAbortedException)
                    {
                        return;
                    }
                    EntryChunkController.EntryChunk.Entries.Add(clonedProcessed);
                }
            }
        }

        private void Menu_Reload_Entry()
        {
            int index = EntryChunkController.EntryChunk.Entries.IndexOf(Entry);
            UnprocessedEntry unprocessedentry = Entry.Unprocess();
            try
            {
                Entry reloadedentry = unprocessedentry.Process(GameVersion);
                EntryChunkController.EntryChunk.Entries[index] = reloadedentry;
            }
            catch (LoadAbortedException)
            {
                return;
            }
        }

        private void Menu_Replace_Entry()
        {
            byte[] data = FileUtil.OpenFile(FileFilters.NSEntryExt, FileFilters.Any);
            if (data == null)
                return;

            int index = EntryChunkController.EntryChunk.Entries.IndexOf(Entry);
            try
            {
                UnprocessedEntry newentry = Entry.Load(data);
                Entry processedentry = newentry.Process(GameVersion);
                EntryChunkController.EntryChunk.Entries[index] = processedentry;
            }
            catch (LoadAbortedException)
            {
                return;
            }
        }
    }
}
