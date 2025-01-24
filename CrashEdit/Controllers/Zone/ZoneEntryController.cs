using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(ZoneEntry))]
    public sealed class ZoneEntryController : EntryController
    {
        public ZoneEntryController(ZoneEntry zoneentry, SubcontrollerGroup parentGroup) : base(zoneentry, parentGroup)
        {
            ZoneEntry = zoneentry;
            AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcAddEntity, Menu_AddEntity);
            AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, Menu_ChangeCollisionType);
        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ZoneEntryViewer(GetNSF(), Entry.EID);
        }

        public ZoneEntry ZoneEntry { get; }

        void Menu_AddEntity()
        {
            short id = 10;
            while (true)
            {
                foreach (ZoneEntry zone in GetEntries<ZoneEntry>())
                {
                    foreach (Entity otherentity in zone.Entities)
                    {
                        if (otherentity.ID == id)
                        {
                            goto FOUND_ID;
                        }
                    }
                }
                break;
            FOUND_ID:
                ++id;
                continue;
            }
            Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
            newentity.ID = id;
            ZoneEntry.Entities.Add(newentity);
            ++ZoneEntry.EntityCount;
        }

        void Menu_ChangeCollisionType()
        {
            byte[] layout = ZoneEntry.Layout;

            byte[] searchPattern = null!;
            using (InputWindow inputWindows = new InputWindow("Enter collision type to replace:", "Change Collision Type", string.Empty))
            {
                if (inputWindows.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string input = inputWindows.Input;
                        if (input.Length % 4 != 0)
                        {
                            throw new ArgumentException("The input must be specified as a 4-digit hexadecimal number.");
                        }

                        ushort value = Convert.ToUInt16(input, 16);
                        searchPattern = BitConverter.GetBytes(value);
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"Error: {ex.Message}", Resources.Title_Error);
                        return;
                    }
                }
                else return;
            }

            byte[] replacementPattern = null!;
            using (InputWindow inputWindows = new InputWindow("Enter new collision type:", "Change Collision Type", string.Empty))
            {
                if (inputWindows.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string input = inputWindows.Input;
                        if (input.Length % 4 != 0)
                        {
                            throw new ArgumentException("The input must be specified as a 4-digit hexadecimal number.");
                        }

                        ushort value = Convert.ToUInt16(input, 16);
                        replacementPattern = BitConverter.GetBytes(value);
                    }
                    catch (Exception ex)
                    {
                        DarkMessageBox.ShowError($"Error: {ex.Message}", Resources.Title_Error);
                        return;
                    }
                }
                else return;
            }

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

            ZoneEntry.Layout = layout;
        }
    }
}
