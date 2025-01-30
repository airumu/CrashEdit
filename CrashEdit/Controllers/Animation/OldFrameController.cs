using AltUI.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldFrame))]
    public sealed class OldFrameController : LegacyController
    {
        public OldFrameController(OldFrame oldframe, SubcontrollerGroup parentGroup) : base(parentGroup, oldframe)
        {
            OldFrame = oldframe;
            AddMenu("Export as OBJ", Menu_Export_OBJ);
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
            OldModelEntry modelentry = GetEntry<OldModelEntry>(OldFrame.ModelEID);
            if (modelentry == null)
            {
                throw new GUIException("The linked model entry could not be found.");
            }
            if (DarkMessageBox.ShowWarning("Texture and color information will not be exported.\n\nContinue anyway?", "Export as OBJ", DarkDialogButton.YesNo) != DialogResult.Yes)
            {
                return;
            }
            FileUtil.SaveFile(OldFrame.ToOBJ(modelentry), FileFilters.OBJ, FileFilters.Any);
        }
    }
}
