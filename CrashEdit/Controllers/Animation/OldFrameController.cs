using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldFrame))]
    public sealed class OldFrameController : LegacyController
    {
        public OldFrameController(OldFrame oldframe, SubcontrollerGroup parentGroup) : base(parentGroup, oldframe)
        {
            OldFrame = oldframe;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldFrameBox(this);
        }

        public OldAnimationEntryController OldAnimationEntryController => Modern.Parent.Legacy as OldAnimationEntryController;
        public ColoredAnimationEntryController ColoredAnimationEntryController => Modern.Parent.Legacy as ColoredAnimationEntryController;
        public OldFrame OldFrame { get; }
        public bool IsColored => Modern.Parent.Text.Contains("Colored Animation");

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            string path = Path.GetDirectoryName(filename);
            string modelname = Path.GetFileNameWithoutExtension(filename);

            Console.WriteLine($"Exporting Frame...");

            var exporter = new OBJExporter();
            exporter.AddObject();
            ToOBJ(exporter, GetNSF(), OldFrame, IsColored);
            exporter.Export(path, modelname, false);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        public static void ToOBJ(OBJExporter exporter, NSF nsf, OldFrame oldFrame, bool isColored)
        {
            exporter.AddFrame_Old(nsf, oldFrame, isColored);
        }
    }
}
