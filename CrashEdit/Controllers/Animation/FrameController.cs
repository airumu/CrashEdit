using System.Windows.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(Frame))]
    public sealed class FrameController : LegacyController
    {
        private SplitContainer pnSplit;
        public FrameController(Frame frame, SubcontrollerGroup parentGroup) : base(parentGroup, frame)
        {
            Frame = frame;
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            if (!Frame.IsNew)
            {
                MetroSetTabControl tbcTabs = new MetroSetTabControl()
                {
                    BackgroundColor = Color.FromArgb(31, 31, 32),
                    Dock = DockStyle.Fill,
                    IsDerivedStyle = false,
                    ItemSize = new Size(100, 28),
                    Style = MetroSet_UI.Enums.Style.Dark,
                    TabStyle = MetroSet_UI.Enums.TabStyle.Style1
                };
                ModelEntry modelentry = GetEntry<ModelEntry>(Frame.ModelEID);

                var entry = AnimationEntryController.AnimationEntry;

                var framebox = new FrameBox(this, entry)
                {
                    Dock = DockStyle.Fill
                };
                var viewerbox = new AnimationEntryViewer(GetNSF(), entry.EID, entry.Frames.IndexOf(Frame))
                {
                    Dock = DockStyle.Fill
                };

                if (Settings.Default.SplitAnimViewerPanels)
                {
                    pnSplit = new SplitContainer
                    {
                        Orientation = Orientation.Horizontal,
                        SplitterDistance = 35,
                        IsSplitterFixed = true,
                        Dock = DockStyle.Fill 
                    };
                    pnSplit.Panel1.Controls.Add(framebox);
                    pnSplit.Panel2.Controls.Add(viewerbox);
                    return pnSplit;
                }
                else
                {
                    TabPage edittab = new TabPage("Editor");
                    edittab.Controls.Add(framebox);
                    TabPage viewertab = new TabPage("Viewer");
                    viewertab.Controls.Add(viewerbox);

                    tbcTabs.TabPages.Add(viewertab);
                    tbcTabs.TabPages.Add(edittab);
                    tbcTabs.SelectedTab = viewertab;
                    return tbcTabs;
                }
            }
            else
            {
                var entry = AnimationEntryController.AnimationEntry;
                return new AnimationEntryViewer(GetNSF(), entry.EID, entry.Frames.IndexOf(Frame));
            }
        }

        public AnimationEntryController AnimationEntryController => (AnimationEntryController)Modern.Parent.Legacy;
        public Frame Frame { get; }
        public ModelEntry Model { get; }
    }
}
