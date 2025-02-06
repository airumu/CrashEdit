using AltUI.Forms;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(OldZoneEntry))]
    public sealed class OldZoneEntryController : EntryController
    {
        public OldZoneEntryController(OldZoneEntry zoneentry, SubcontrollerGroup parentGroup)
            : base(zoneentry, parentGroup)
        {
            OldZoneEntry = zoneentry;
            AddMenu("Add Entity", "Add", Menu_AddEntity);
            AddMenu("Add Camera", Menu_AddCamera);
            AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, Menu_ChangeCollisionType);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new OldZoneEntryViewer(GetNSF(), Entry.EID);
        }

        public OldZoneEntry OldZoneEntry { get; }

        void Menu_AddEntity()
        {
            short id = 6;
            var entities = new List<OldEntity>();

            foreach (OldZoneEntry zone in GetEntries<OldZoneEntry>())
            {
                entities.AddRange(zone.Entities);
            }
            while (entities.Find(x => x.ID == id) != null)
            {
                ++id;
            }

            OldEntity newentity = OldEntity.Load(new OldEntity(0x0018, 3, 0, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            OldZoneEntry.Entities.Add(newentity);
        }

        void Menu_AddCamera()
        {
            OldCamera newcam = OldCamera.Load(new OldCamera(Entry.ENameToEID("NONE!"), 0, 0, new OldCameraNeighbor[4], 0, 0, 0, 0, 1600, 0, 0, 0, 0, 0, 0, new List<OldCameraPosition>(), 0).Save());
            OldZoneEntry.Cameras.Add(newcam);
        }

        void Menu_ChangeCollisionType()
        {
            try
            {
                byte[] searchPattern = null!;
                using (InputWindow inputWindows = new InputWindow("Enter the collision type (as a literal) to replace:", CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, string.Empty))
                {
                    if (inputWindows.ShowDialog() == DialogResult.OK)
                    {
                        string input = inputWindows.Input;
                        if (input.Length % 4 != 0)
                        {
                            throw new ArgumentException("The input must be specified as a 4-digit hexadecimal number.");
                        }

                        ushort value = Convert.ToUInt16(input, 16);
                        searchPattern = BitConverter.GetBytes(value);
                    }
                    else return;
                }

                byte[] replacementPattern = null!;
                using (InputWindow inputWindows = new InputWindow("Enter the new collision type (as a literal):", CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, string.Empty))
                {
                    if (inputWindows.ShowDialog() == DialogResult.OK)
                    {
                        string input = inputWindows.Input;
                        if (input.Length % 4 != 0)
                        {
                            throw new ArgumentException("The input must be specified as a 4-digit hexadecimal number.");
                        }

                        ushort value = Convert.ToUInt16(input, 16);
                        replacementPattern = BitConverter.GetBytes(value);
                    }
                    else return;
                }

                byte[] layout = OldZoneEntry.Layout;
                for (int i = 0x24; i <= layout.Length - searchPattern.Length; i += 2)
                {
                    bool isMatch = true;

                    for (int j = 0; j < searchPattern.Length; j++)
                    {
                        if (layout[i + j] != searchPattern[j])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        for (int j = 0; j < replacementPattern.Length; j++)
                        {
                            layout[i + j] = replacementPattern[j];
                        }
                    }
                }

                OldZoneEntry.Layout = layout;
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"{ex.Message}", CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType);
                return;
            }
        }
    }
}
