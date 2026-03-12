using System.Media;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(AnimationEntry))]
    public sealed class AnimationEntryController : EntryController
    {
        public AnimationEntryController(AnimationEntry animationentry, SubcontrollerGroup parentGroup) : base(animationentry, parentGroup)
        {
            AnimationEntry = animationentry;
            AddMenuSeparator();
            AddMenu("Decompress Animtaion", "Container", Menu_Decompress);
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.AnimationEntryController_AcExportAsOBJ, Menu_Export_OBJ);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new AnimationEntryViewer(GetNSF(), Entry.EID);
        }

        public AnimationEntry AnimationEntry { get; }

        private void Menu_Decompress()
        {
            int modelEID = AnimationEntry.Frames[0].ModelEID;
            ModelEntry? model = GetEntry<ModelEntry>(modelEID) ?? throw new InvalidOperationException("Linked model not found.");
            var newAniModel = ModelEntry.ConvertCompressedToUncompressed(model, AnimationEntry);

            FileUtil.SaveFile(newAniModel.anim.Save(), FileFilters.NSEntry, FileFilters.Any);
        }

        private void Menu_Export_OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string output, FileFilters.OBJ, FileFilters.Any))
                return;

            // modify the path to add a number before the extension
            string ext = Path.GetExtension(output);
            string filename = Path.GetFileNameWithoutExtension(output);
            string path = Path.GetDirectoryName(output);

            int id = 0;
            int count = AnimationEntry.Frames.Count.ToString().Length;

            foreach (var frame in AnimationEntry.Frames)
            {
                Console.WriteLine($"Exporting Frames[{id}]...");
                FrameController.ToOBJ(path, filename + "_" + id.ToString().PadLeft(count, '0'), GetNSF(), frame);
                id++;
            }

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }
    }
}
