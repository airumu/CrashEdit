namespace CrashEdit.Crash
{
    [EntryType(7, GameVersion.Crash2)]
    public sealed class ZoneEntryLoader : EntryLoader
    {
        public override Entry Load(byte[][] items, int eid)
        {
            if (items.Length < 2)
            {
                ErrorManager.SignalError("ZoneEntry: Wrong number of items");
            }
            byte[] layout = items[1];
            Entity[] entities = new Entity[items.Length - 2];
            for (int i = 2; i < items.Length; i++)
            {
                entities[i - 2] = Entity.Load(items[i]);
            }
            return new ZoneEntry(ZoneHeader.Load(items[0]), layout, entities, eid);
        }
    }

    [EntryType(7, GameVersion.Crash3)]
    public sealed class NewZoneEntryLoader : EntryLoader
    {
        public override Entry Load(byte[][] items, int eid)
        {
            if (items.Length < 2)
            {
                ErrorManager.SignalError("ZoneEntry: Wrong number of items");
            }
            byte[] layout = items[1];
            Entity[] entities = new Entity[items.Length - 2];
            for (int i = 2; i < items.Length; i++)
            {
                entities[i - 2] = Entity.Load(items[i]);
            }
            return new ZoneEntry(ZoneHeader.LoadNew(items[0]), layout, entities, eid);
        }
    }
}
