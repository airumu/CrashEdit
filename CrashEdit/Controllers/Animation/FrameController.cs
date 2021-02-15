using Crash;
using MetroFramework.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class FrameController : Controller
    {
        private SplitContainer pnSplit;

        public FrameController(AnimationEntryController animationentrycontroller,Frame frame)
        {
            AnimationEntryController = animationentrycontroller;
            Frame = frame;
            InvalidateNode();
            InvalidateNodeImage();
        }

        public override void InvalidateNode()
        {
            Node.Text = Crash.UI.Properties.Resources.FrameController_Text;
        }

        public override void InvalidateNodeImage()
        {
            Node.ImageKey = "arrow";
            Node.SelectedImageKey = "arrow";
        }

        protected override Control CreateEditor()
        {
            /*
            if (!Frame.IsNew)
            {
                ModelEntry modelentry = AnimationEntryController.EntryChunkController.NSFController.NSF.FindEID<ModelEntry>(Frame.ModelEID);
                TextureChunk[] texturechunks = new TextureChunk[8];
                for (int i = 0; i < 8; ++i)
                {
                    texturechunks[i] = AnimationEntryController.EntryChunkController.NSFController.NSF.FindEID<TextureChunk>(BitConv.FromInt32(modelentry.Info,0xC+i*4));
                }
                return new UndockableControl(new AnimationEntryViewer(Frame,modelentry,texturechunks));
            }
            else
            {
                return new Crash3AnimationSelector(AnimationEntryController.AnimationEntry, Frame, AnimationEntryController.EntryChunkController.NSFController.NSF);
            }
            */
            if (!Frame.IsNew)
            {
                ModelEntry modelentry = AnimationEntryController.EntryChunkController.NSFController.NSF.FindEID<ModelEntry>(Frame.ModelEID);

                FrameBox framebox = new FrameBox(this);
                framebox.Dock = DockStyle.Fill;
                TextureChunk[] texturechunks = new TextureChunk[8];
                for (int i = 0; i < 8; ++i)
                {
                    texturechunks[i] = AnimationEntryController.EntryChunkController.NSFController.NSF.FindEID<TextureChunk>(BitConv.FromInt32(modelentry.Info, 0xC + i * 4));
                }

                UndockableControl viewerbox = new UndockableControl(new AnimationEntryViewer(Frame, modelentry, texturechunks)) { Dock = DockStyle.Fill };

                if (Properties.Settings.Default.AnimViewPanel)
                {
                    pnSplit = new SplitContainer { Dock = DockStyle.Fill };
                    pnSplit.BackColor = Color.FromArgb(30, 30, 30);
                    pnSplit.Orientation = Orientation.Horizontal;
                    pnSplit.SplitterDistance = 50;

                    pnSplit.Panel1.Controls.Add(framebox);
                    pnSplit.Panel2.Controls.Add(viewerbox);

                    return pnSplit;
                }
                else
                {
                    MetroTabControl tbcTabs = new MetroTabControl()
                    {
                        Dock = DockStyle.Fill,
                        FontSize = MetroFramework.MetroTabControlSize.Medium,
                        FontWeight = MetroFramework.MetroTabControlWeight.Regular,
                        Style = MetroFramework.MetroColorStyle.Teal,
                        Theme = MetroFramework.MetroThemeStyle.Dark
                    };

                    TabPage edittab = new TabPage("Editor");
                    edittab.Controls.Add(framebox);
                    TabPage viewertab = new TabPage("Viewer");
                    viewertab.Controls.Add(new UndockableControl(viewerbox));

                    tbcTabs.TabPages.Add(viewertab);
                    tbcTabs.TabPages.Add(edittab);
                    tbcTabs.SelectedTab = viewertab;

                    return tbcTabs;
                }
            }
            else
            {
                return new Crash3AnimationSelector(AnimationEntryController.AnimationEntry, Frame, AnimationEntryController.EntryChunkController.NSFController.NSF);
            }
        }

        public AnimationEntryController AnimationEntryController { get; }
        public Frame Frame { get; }
    }
}
