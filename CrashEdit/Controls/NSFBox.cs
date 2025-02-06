using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public sealed class NSFBox : MainControl
    {
        public NSFBox(IUserInterface ui, LevelWorkspace ws) : base(ui, Controller.Make(ws, null))
        {
            Workspace = ws;

            if (ws.NSD != null)
            {
                NSD = ws.NSD;
                NSDController = (NSDController)RootController.SubcontrollerGroups[0].Members[0].Legacy;
            }
            else if (ws.OldNSD != null)
            {
                OldNSD = ws.OldNSD;
                NSDController = (NSDController)RootController.SubcontrollerGroups[1].Members[0].Legacy;
            }
            else if (ws.ProtoNSD != null)
            {
                ProtoNSD = ws.ProtoNSD;
                NSDController = (NSDController)RootController.SubcontrollerGroups[2].Members[0].Legacy;
            }
            else
            {
                NSDController = null;
            }

            NSF = ws.NSF;
            NSFController = (NSFController)RootController.SubcontrollerGroups[3].Members[0].Legacy;

            Sync();

            foreach (TreeNode node in ResourceTree.Nodes)
            {
                if (node.Text == "NSF")
                {
                    node.Expand();
                }
            }
        }

        public LevelWorkspace Workspace { get; }
        public NSD? NSD { get; }
        public OldNSD? OldNSD { get; }
        public ProtoNSD? ProtoNSD { get; }
        public NSDController? NSDController { get; }
        public NSF NSF { get; }
        public NSFController NSFController { get; }

        public override void Sync()
        {
            base.Sync();
        }

        public override void Kill()
        {
            base.Kill();
            NSFController.Kill();
            if (NSDController != null)
            {
                NSDController.Kill();
            }
        }
    }
}
