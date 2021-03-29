using Crash;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class MapEntryController : EntryController
    {
        public MapEntryController(EntryChunkController entrychunkcontroller,MapEntry mapentry) : base(entrychunkcontroller,mapentry)
        {
            MapEntry = mapentry;
            AddNode(new ItemController(null,mapentry.Header));
            AddNode(new ItemController(null,mapentry.Layout));
            foreach (OldEntity entity in mapentry.Entities)
            {
                AddNode(new OldEntityController(this,entity));
            }
            AddMenu("Add Entity",Menu_AddEntity);
            InvalidateNode();
            InvalidateNodeImage();
        }

        public override void InvalidateNode()
        {
            Node.Text = string.Format(Crash.UI.Properties.Resources.MapEntryController_Text,MapEntry.EName);
        }

        public override void InvalidateNodeImage()
        {
            Node.ImageKey = "yellowb";
            Node.SelectedImageKey = "yellowb";
        }

        protected override Control CreateEditor()
        {
            return new MapEntryViewer(this);
        }

        public MapEntry MapEntry { get; }

        void Menu_AddEntity()
        {
            short id = 1;
            while (true)
            {
                foreach (Chunk chunk in EntryChunkController.NSFController.NSF.Chunks)
                {
                    if (chunk is EntryChunk entrychunk)
                    {
                        foreach (Entry entry in entrychunk.Entries)
                        {
                            if (entry is MapEntry zone)
                            {
                                foreach (OldEntity otherentity in zone.Entities)
                                {
                                    if (otherentity.ID == id)
                                    {
                                        goto FOUND_ID;
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            FOUND_ID:
                ++id;
                continue;
            }
            OldEntity newentity = OldEntity.Load(new OldEntity(0,0x00030018,id,0,0,0,0,0,new List<EntityPosition>() { new EntityPosition(0,0,0) },0).Save());
            MapEntry.Entities.Add(newentity);
            AddNode(new OldEntityController(this,newentity));
            MapEntry.EntityCount = MapEntry.Entities.Count;
        }
    }
}
