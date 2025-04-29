using CrashEdit.CE.Controls;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(Scenery))]
    public sealed class SceneryController : LegacyController
    {
        public SceneryController(Scenery scenery, SubcontrollerGroup parentGroup) : base(parentGroup, scenery)
        {
            Scenery = scenery;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ModelBox(SceneryEntryController);
        }

        public SceneryEntryController SceneryEntryController => (SceneryEntryController)Modern.Parent.Legacy;
        public Scenery Scenery { get; }
    }
}
