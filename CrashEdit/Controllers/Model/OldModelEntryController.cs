using Crash;
using DarkUI.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class OldModelEntryController : EntryController
    {
        public OldModelEntryController(EntryChunkController entrychunkcontroller,OldModelEntry oldmodelentry) : base(entrychunkcontroller,oldmodelentry)
        {
            OldModelEntry = oldmodelentry;
            InvalidateNode();
            InvalidateNodeImage();
        }

        public override void InvalidateNode()
        {
            Node.Text = string.Format(Crash.UI.Properties.Resources.OldModelEntryController_Text,OldModelEntry.EName);
        }

        public override void InvalidateNodeImage()
        {
            Node.ImageKey = "crimsonb";
            Node.SelectedImageKey = "crimsonb";
        }

        protected override Control CreateEditor()
        {
            return new DarkLabel { Text = string.Format("Polygon count: {0}", BitConv.FromInt32(OldModelEntry.Info, 0)), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Yu Gothic UI ", 9F) };
        }

        public OldModelEntry OldModelEntry { get; }
    }
}
