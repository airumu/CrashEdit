using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

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

            OBJExporter exporter = new();
            for (int i = 0; i < OldAnimationEntry.Frames.Count; i++)
            {
                exporter.AddObject();
                OldFrame frame = OldAnimationEntry.Frames[i];
                Console.WriteLine($"Exporting Frames[{i}]...");

                OldFrameController.ToOBJ(exporter, GetNSF(), frame, false);
            }
            exporter.Export(path, filename, true);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }
    }
}
