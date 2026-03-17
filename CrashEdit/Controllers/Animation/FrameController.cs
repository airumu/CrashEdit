using CrashEdit.Crash;
using CrashEdit.Exporters;
using System.Media;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(Frame))]
    public sealed class FrameController : LegacyController
    {
        public FrameController(Frame frame, SubcontrollerGroup parentGroup) : base(parentGroup, frame)
        {
            Frame = frame;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            var entry = AnimationEntryController.AnimationEntry;
            if (!Frame.IsNew)
            {
                return new FrameBox(this);
            }
            else
            {
                return new AnimationEntryViewer(GetNSF(), entry.EID, entry.Frames.IndexOf(Frame));
            }
        }

        public AnimationEntryController AnimationEntryController => (AnimationEntryController)Modern.Parent.Legacy;
        public Frame Frame { get; }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            Console.WriteLine($"Exporting Frame...");
            ToOBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), GetNSF(), Frame, AnimationEntryController.AnimationEntry);
            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        public static void ToOBJ(string path, string modelname, NSF nsf, Frame frame, AnimationEntry anim)
        {
            var exporter = new OBJExporter();

            exporter.AddFrame(nsf, frame, anim);
            exporter.Export(path, modelname);
        }
    }
}
