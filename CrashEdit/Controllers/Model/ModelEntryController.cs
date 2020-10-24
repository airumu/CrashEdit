using Crash;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace CrashEdit
{
    public sealed class ModelEntryController : EntryController
    {
        public ModelEntryController(EntryChunkController entrychunkcontroller,ModelEntry modelentry) : base(entrychunkcontroller,modelentry)
        {
            ModelEntry = modelentry;
            InvalidateNode();
            InvalidateNodeImage();
        }

        public override void InvalidateNode()
        {
            if (ModelEntry.Positions == null)
            {
                Node.Text = string.Format(Crash.UI.Properties.Resources.ModelEntryController_Text,ModelEntry.EName);
            }
            else
            {
                Node.Text = string.Format(Crash.UI.Properties.Resources.ModelEntryController_Compressed_Text,ModelEntry.EName);
            }
        }

        public override void InvalidateNodeImage()
        {
            if (ModelEntry.Positions == null)
            {
                Node.ImageKey = "crimsonb";
                Node.SelectedImageKey = "crimsonb";
            }
            else
            {
                Node.ImageKey = "redb";
                Node.SelectedImageKey = "redb";
            }
        }

        protected override Control CreateEditor()
        {
            if (ModelEntry.Positions == null)
                return new DarkLabel { Text = string.Format("Polygon count: {0}\nVertex count: {1}",ModelEntry.PolyCount,ModelEntry.VertexCount), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Yu Gothic UI ", 9F) };
            else
            {
                int totalbits = ModelEntry.Positions.Count * 8 * 3;
                int bits = 0;
                foreach (ModelPosition pos in ModelEntry.Positions)
                {
                    bits += 1+pos.XBits;
                    bits += 1+pos.YBits;
                    bits += 1+pos.ZBits;
                }
                return new DarkLabel { Text = string.Format("Polygon count: {0}\nVertex count: {1}\nCompression ratio: {2:0.0}% ({3}/{4})", ModelEntry.PolyCount,ModelEntry.VertexCount,(double)bits/totalbits * 100.0, bits, totalbits), TextAlign = ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Yu Gothic UI ", 9F)  };
            }
        }

        public ModelEntry ModelEntry { get; }
    }
}
