using System.Media;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldAnimationEntry))]
    public sealed class OldAnimationEntryController : EntryController
    {
        public OldAnimationEntryController(OldAnimationEntry oldanimationentry, SubcontrollerGroup parentGroup) : base(oldanimationentry, parentGroup)
        {
            OldAnimationEntry = oldanimationentry;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldAnimationEntryViewer(GetNSF(), Entry.EID);
        }

        public OldAnimationEntry OldAnimationEntry { get; }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string output, FileFilters.OBJ, FileFilters.Any))
                return;

            // modify the path to add a number before the extension
            string ext = Path.GetExtension(output);
            string filename = Path.GetFileNameWithoutExtension(output);
            string path = Path.GetDirectoryName(output);

            int id = 0;
            int count = OldAnimationEntry.Frames.Count.ToString().Length;

            foreach (var frame in OldAnimationEntry.Frames)
            {
                Console.WriteLine($"Exporting Frames[{id}]...");
                OldFrameController.ToOBJ_Old(path, filename + "_" + id.ToString().PadLeft(count, '0'), GetNSF(), frame);
                id++;
            }

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }
    }
}
