using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(ZoneHeader))]
    public sealed class ZoneHeaderController : LegacyController
    {
        public ZoneHeaderController(ZoneHeader zoneheader, SubcontrollerGroup parentGroup) : base(parentGroup, zoneheader)
        {
            ZoneHeader = zoneheader;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ZoneHeaderBox(this);
        }

        public ZoneHeader ZoneHeader { get; }
    }
}
