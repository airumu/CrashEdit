using AltUI.Forms;
using CrashEdit.CE.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using CrashEdit.Exporters;
using MetroSet_UI.Animates;
using System.Diagnostics;
using System.Media;
using System.Security.Cryptography;
using System.Text;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(NSF))]
    public sealed class NSFController : LegacyController
    {
        public NSFController(NSF nsf, SubcontrollerGroup parentGroup) : base(parentGroup, nsf)
        {
            NSF = nsf;
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddNormalChunk, "JournalOrange", Menu_Add_NormalChunk);
            if (GameVersion != GameVersion.Crash2 && GameVersion != GameVersion.Crash3 && GameVersion != GameVersion.Crash1 && GameVersion != GameVersion.Crash3BetaMAY14)
                AddMenu(CrashUI.Properties.Resources.NSFController_AcAddOldSoundChunk, "JournalBlue", Menu_Add_OldSoundChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddSoundChunk, "JournalBlue", Menu_Add_SoundChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddWavebankChunk, "JournalRed",Menu_Add_WavebankChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddSpeechChunk, "JournalWhite", Menu_Add_SpeechChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddTextureChunk, "Painting", Menu_Add_TextureChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcImportChunk, "Import", Menu_Import_Chunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcImportEntriesIntoChunks, "Import", Menu_Import_Entries_Into_New_Chunks);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcImportAndReplaceChunk, "ImportPlus", Menu_Import_And_Replace_Chunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcImportAndReplaceEntry, "ImportPlus", Menu_Import_And_Replace_Entry);
            if (GameVersion == GameVersion.Crash2 || GameVersion == GameVersion.Crash3 || GameVersion == GameVersion.Crash3BetaMAY14)
            {
                AddMenuSeparator();
                AddMenu(CrashUI.Properties.Resources.NSFController_AcAnalyzeLevel, "HardDisk", Menu_AnalyzeLevel);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcFindEntities, "Find", Menu_FindEntities);
                AddMenuSeparator();
                AddMenu(CrashUI.Properties.Resources.NSFController_AcFixDetonator, "Calculator", Menu_Fix_Detonator);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcFixBoxCount, "Calculator", Menu_Fix_BoxCount);
                AddMenuSeparator();
            }
            if (GameVersion == GameVersion.Crash1 || GameVersion == GameVersion.Crash1BetaMAR08 || GameVersion == GameVersion.Crash1BetaMAY11)
            {
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevel, "ThingBlue", Menu_ShowLevelC1);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevelZones, "ThingViolet", Menu_ShowLevelZonesC1);
                AddMenuSeparator();
                AddMenu(CrashUI.Properties.Resources.NSFController_AcExportScenery, Menu_ExportSceneryC1OBJ);
            }
            else if (GameVersion == GameVersion.Crash1Beta1995)
            {
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevel, "ThingBlue", Menu_ShowLevelC1Proto);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevelZones, "ThingViolet", Menu_ShowLevelZonesC1Proto);
            }
            else if (GameVersion == GameVersion.Crash2 || GameVersion == GameVersion.Crash3 || GameVersion == GameVersion.Crash3BetaMAY14)
            {
                AddMenu(CrashUI.Properties.Resources.NSFController_AcEditEntities, "Modify", Menu_EditEntitiesC2);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcEditScenery, "Wrench", Menu_EditSceneryC2);
                AddMenuSeparator();
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevel, "ThingBlue", Menu_ShowLevelC2);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevelZones, "ThingViolet", Menu_ShowLevelZonesC2);
                AddMenuSeparator();
                AddMenu(CrashUI.Properties.Resources.NSFController_AcExportScenery, Menu_ExportSceneryC2OBJ);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcExportZones, Menu_ExportZones);
            }
        }

        public NSF NSF { get; }

        private DarkForm? ShowLevelForm { get; set; }
        private DarkForm? ShowLevelZonesForm { get; set; }
        private EntityEditor? EntityEditorForm { get; set; }
        private SceneryEditor? SceneryEditorForm { get; set; }

        public void Kill()
        {
            ShowLevelForm?.Close();
            ShowLevelForm?.Dispose();
            ShowLevelForm = null;
            ShowLevelZonesForm?.Close();
            ShowLevelZonesForm?.Dispose();
            ShowLevelZonesForm = null;
            EntityEditorForm?.Close();
            EntityEditorForm?.Dispose();
            EntityEditorForm = null;
            SceneryEditorForm?.Close();
            SceneryEditorForm?.Dispose();
            SceneryEditorForm = null;
        }

        private void Menu_Add_NormalChunk()
        {
            NormalChunk chunk = new NormalChunk();
            NSF.Chunks.Add(chunk);
        }

        private void Menu_Add_OldSoundChunk()
        {
            OldSoundChunk chunk = new OldSoundChunk();
            NSF.Chunks.Add(chunk);
        }

        private void Menu_Add_SoundChunk()
        {
            SoundChunk chunk = new SoundChunk();
            NSF.Chunks.Add(chunk);
        }

        private void Menu_Add_WavebankChunk()
        {
            WavebankChunk chunk = new WavebankChunk();
            NSF.Chunks.Add(chunk);
        }

        private void Menu_Add_SpeechChunk()
        {
            SpeechChunk chunk = new SpeechChunk();
            NSF.Chunks.Add(chunk);
        }

        private void Menu_Add_TextureChunk()
        {
            int i = -1;

            while (NSF.Chunks
                .OfType<TextureChunk>()
                .Any(t => t.EName == ModelConverterForm.GetDefaultEID('T', i)))
            {
                i++;
            }

            int eid = Entry.ENameToEID(ModelConverterForm.GetDefaultEID('T', i));

            byte[] header = {
                0x34, 0x12, 0x01, 0x00,
                (byte)(eid & 0xFF), (byte)((eid >> 8) & 0xFF), (byte)((eid >> 16) & 0xFF), (byte)((eid >> 24) & 0xFF),
                0x05, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            byte[] newchunk = new byte[65536];
            Array.Copy(header, 0, newchunk, 0, header.Length);
            TextureChunk chunk = new(newchunk);
            BitConv.ToInt32(chunk.Data, 12, Chunk.CalculateChecksum(chunk.Data));
            NSF.Chunks.Add(chunk);
        }

        public void Menu_AnalyzeLevel()
        {
            List<string> loadlistLines = new List<string>();
            StringBuilder errorSb = new StringBuilder();
            Console.WriteLine("================================================================================");
            foreach (ZoneEntry zone in NSF.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    // Load lists
                    if (entity.LoadListA != null && entity.LoadListB != null)
                    {
                        List<int> loadedentries = new List<int>();
                        string eidlist = string.Empty;
                        for (int i = 0; i < entity.Positions.Count; ++i)
                        {
                            foreach (var row in entity.LoadListA.Rows)
                            {
                                if (row.MetaValue == i)
                                {
                                    // load
                                    foreach (int eid in row.Values)
                                    {
                                        loadedentries.Add(eid);
                                    }
                                }
                            }
                            foreach (var row in entity.LoadListB.Rows)
                            {
                                if (row.MetaValue == i)
                                {
                                    // unload
                                    foreach (int eid in row.Values)
                                    {
                                        if (!loadedentries.Remove(eid))
                                        {
                                            eidlist += $"\n\t\t  [position {i}] {Entry.EIDToEName(eid)}";
                                        }
                                    }
                                }
                            }
                        }
                        if (eidlist != string.Empty)
                        {
                            // Load List B
                            loadlistLines.Add($"[{zone.EName}, camera {entity.CameraIndex}] The following entries were already deloaded (Load List B):{eidlist}");
                        }
                        if (loadedentries.Count != 0)
                        {
                            string eidlist2 = string.Empty;
                            for (int i = 0; i < entity.Positions.Count; ++i)
                            {
                                foreach (var row in entity.LoadListA.Rows)
                                {
                                    if (row.MetaValue == i)
                                    {
                                        foreach (int eid in row.Values)
                                        {
                                            if (loadedentries.Remove(eid))
                                            {
                                                eidlist2 += $"\n\t\t  [position {i}] {Entry.EIDToEName(eid)}";
                                            }
                                        }
                                    }
                                }
                            }
                            // Load List A
                            loadlistLines.Add($"[{zone.EName}, camera {entity.CameraIndex}] The following entries are never deloaded (Load List A):{eidlist2}");
                        }
                    }

                    // Draw Lists
                    if (entity.DrawListA != null && entity.DrawListB != null)
                    {
                        Dictionary<int, int> globalDrawCounts = new Dictionary<int, int>();
                        Dictionary<int, int> globalUndrawCounts = new Dictionary<int, int>();
                        Dictionary<int, List<int>> drawMetas = new Dictionary<int, List<int>>();
                        Dictionary<int, List<int>> undrawMetas = new Dictionary<int, List<int>>();

                        // DrawListB
                        foreach (var row in entity.DrawListB.Rows)
                        {
                            if (row?.Values == null)
                                continue;
                            int meta = (int)row.MetaValue;
                            foreach (int rawId in row.Values)
                            {
                                int id = (rawId >> 8) & 0xFFFF;
                                if (globalDrawCounts.ContainsKey(id))
                                    globalDrawCounts[id]++;
                                else
                                    globalDrawCounts[id] = 1;

                                if (!drawMetas.ContainsKey(id))
                                    drawMetas[id] = new List<int>();
                                drawMetas[id].Add(meta);
                            }
                        }

                        // DrawListA
                        foreach (var row in entity.DrawListA.Rows)
                        {
                            if (row?.Values == null)
                                continue;
                            int meta = (int)row.MetaValue;
                            foreach (int rawId in row.Values)
                            {
                                int id = (rawId >> 8) & 0xFFFF;
                                if (globalUndrawCounts.ContainsKey(id))
                                    globalUndrawCounts[id]++;
                                else
                                    globalUndrawCounts[id] = 1;

                                if (!undrawMetas.ContainsKey(id))
                                    undrawMetas[id] = new List<int>();
                                undrawMetas[id].Add(meta);
                            }
                        }

                        HashSet<int> allIds = new HashSet<int>(globalDrawCounts.Keys);
                        foreach (var id in globalUndrawCounts.Keys)
                            allIds.Add(id);

                        foreach (int id in allIds)
                        {
                            int countDraw = globalDrawCounts.ContainsKey(id) ? globalDrawCounts[id] : 0;
                            int countUndraw = globalUndrawCounts.ContainsKey(id) ? globalUndrawCounts[id] : 0;
                            if (countDraw != countUndraw)
                            {
                                string pos = string.Empty;
                                if (drawMetas.ContainsKey(id))
                                    pos = $" at position {string.Join(", ", drawMetas[id])}";
                                if (undrawMetas.ContainsKey(id))
                                    pos = $" at position {string.Join(", ", undrawMetas[id])}";

                                errorSb.AppendLine($"[{zone.EName}, camera {entity.CameraIndex}] ID {id}{pos}: drawn {countDraw} times, undrawn {countUndraw} times.");
                            }
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Load list integrity check:");
            Console.ResetColor();
            if (loadlistLines.Count > 0)
                Console.WriteLine(string.Join(Environment.NewLine, loadlistLines.ToArray()));
            else
                Console.WriteLine("No load list issues were found.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Draw list integrity check:");
            Console.ResetColor();
            if (errorSb.Length > 0)
                Console.WriteLine(errorSb);
            else
                Console.WriteLine("No draw list issues were found.");

            SystemSounds.Asterisk.Play();
        }

        public void Menu_FindEntities()
        {
            int type = -1;
            int subtype = -1;
            using (InputWindow inputWindow = new InputWindow(CrashUI.Properties.Resources.NSFController_AcFindEntities, "Find",
                "Enter entity type:", string.Empty, -1,
                "Enter entity subtype (leave empty to search all):", string.Empty, -1))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(inputWindow.Input, out type) && type >= 0) { }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid input.", Resources.Title_InputError);
                        return;
                    }

                    if (!string.IsNullOrEmpty(inputWindow.Input2))
                    {
                        if (int.TryParse(inputWindow.Input2, out subtype) && subtype >= 0) { }
                        else
                        {
                            DarkMessageBox.ShowError("Invalid input.", Resources.Title_InputError);
                            return;
                        }
                    }
                }
                else return;
            }

            List<string> list = new List<string>();
            Console.WriteLine("================================================================================");
            foreach (ZoneEntry zone in NSF.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == type && (subtype == -1 || entity.Subtype == subtype))
                    {
                        list.Add($"Type {entity.Type:D2}, subtype {entity.Subtype:D2}: {entity.Name} [ID {entity.ID}]");
                    }
                }
            }
            list.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, list.ToArray()));
            Console.WriteLine($"Total count: {list.Count}");
        }

        private void Menu_Fix_Detonator()
        {
            using (ExternalData externalData = new ExternalData(CrashUI.Properties.Resources.NSFController_AcFixDetonator))
            {
                if (externalData.ShowDialog() == DialogResult.OK)
                {
                    List<Entity> nitros = new List<Entity>();
                    List<Entity> detonators = new List<Entity>();
                    foreach (ZoneEntry entry in NSF.GetEntries<ZoneEntry>())
                    {
                        foreach (Entity entity in entry.Entities)
                        {
                            if (entity.Type == 34 && entity.ID.HasValue)
                            {
                                if (entity.Subtype == 18)
                                {
                                    nitros.Add(entity);
                                    DebugOutput(externalData.outputResult, (int)entity.Type, (int)entity.Subtype, entity.ID.Value);
                                }
                                if (entity.Subtype == 24)
                                {
                                    detonators.Add(entity);
                                }
                            }

                            foreach (var pair in externalData.Group)
                            {
                                if (entity.Type == pair.Key && entity.ID.HasValue)
                                {
                                    if (entity.Subtype == pair.Value)
                                    {
                                        nitros.Add(entity);
                                        DebugOutput(externalData.outputResult, pair.Key, pair.Value, entity.ID.Value);
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Total nitro count: {nitros.Count}");
                    foreach (Entity detonator in detonators)
                    {
                        detonator.Victims.Clear();
                        foreach (Entity nitro in nitros)
                        {
                            detonator.Victims.Add(new EntityVictim((short)nitro.ID.Value));
                        }
                    }
                }
            }
          
        }

        private void Menu_Fix_BoxCount()
        {
            using (ExternalData externalData = new ExternalData(CrashUI.Properties.Resources.NSFController_AcFixBoxCount))
            {
                if (externalData.ShowDialog() == DialogResult.OK)
                {
                    int boxcount = 0;
                    List<Entity> willys = new List<Entity>();
                    foreach (ZoneEntry zone in NSF.GetEntries<ZoneEntry>())
                    {
                        foreach (Entity entity in zone.Entities)
                        {
                            if (Settings.Default.EnableCustomCrates)
                            {
                                if (entity.Type == 4 && entity.Subtype == 17)
                                    willys.Add(entity);
                            }
                            else
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                    willys.Add(entity);
                            }
                                
                            if (entity.Type == 34 && entity.ID.HasValue)
                            {
                                if (GameVersion != GameVersion.Crash2 && GameVersion != GameVersion.Crash3 && GameVersion != GameVersion.Crash3BetaMAY14)
                                {
                                    switch (entity.Subtype)
                                    {
                                        case 11: // pow
                                        case 16: // auto tnt
                                        case 17: // auto pickup
                                        case 20: // auto empty
                                        case 21: // empty 2
                                            boxcount++;
                                            DebugOutput(externalData.outputResult, (int)entity.Type, (int)entity.Subtype, entity.ID.Value);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else if (GameVersion == GameVersion.Crash3 || GameVersion == GameVersion.Crash3BetaMAY14)
                                {
                                    switch (entity.Subtype)
                                    {
                                        case 25: // slot
                                            boxcount++;
                                            DebugOutput(externalData.outputResult, (int)entity.Type, (int)entity.Subtype, entity.ID.Value);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else if (Settings.Default.EnableCustomCrates)
                                {
                                    switch (entity.Subtype)
                                    {
                                        case 11: // pow
                                        case 12: // purple
                                        case 17: // slot
                                        case 25: // steel pickup
                                        case 26: // steel fruit
                                            boxcount++;
                                            DebugOutput(externalData.outputResult, (int)entity.Type, (int)entity.Subtype, entity.ID.Value);
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                switch (entity.Subtype)
                                {
                                    case 0: // tnt
                                    case 2: // empty
                                    case 3: // spring
                                    case 4: // continue
                                    case 6: // fruit
                                    case 8: // life
                                    case 9: // doctor
                                    case 10: // pickup
                                    case 13: // ghost
                                    case 18: // nitro
                                    case 23: // steel
                                        boxcount++;
                                        DebugOutput(externalData.outputResult, (int)entity.Type, (int)entity.Subtype, entity.ID.Value);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            foreach (var pair in externalData.Group)
                            {
                                if (entity.Type == pair.Key)
                                {
                                    if (entity.Subtype == pair.Value && entity.ID.HasValue)
                                    {
                                        boxcount++;
                                        DebugOutput(externalData.outputResult, pair.Key, pair.Value, entity.ID.Value);
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine($"Total box count: {boxcount}");
                    foreach (Entity willy in willys)
                    {
                        if (willy.BoxCount.HasValue)
                        {
                            willy.BoxCount = new EntitySetting(0, boxcount);
                        }
                    }
                }
            }
        }

        private void DebugOutput(bool output, int type, int subtype, int id)
        {
            if (output)
            {
                Console.WriteLine($"{type:D2}, {subtype:D2}, [ID {id}]");
            }
        }

        private void Menu_ShowLevelC1()
        {
            if (ShowLevelForm != null)
            {
                ShowLevelForm.Focus();
                return;
            }
            ShowLevelForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelForm.Show();
            List<int> worlds = new();
            foreach (var entry in NSF.GetEntries<OldSceneryEntry>())
            {
                worlds.Add(entry.EID);
            }
            OldSceneryEntryViewer viewer = new(NSF, worlds) { Dock = DockStyle.Fill };
            ShowLevelForm.Controls.Add(viewer);
            ShowLevelForm.Text = string.Empty;
            ShowLevelForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelForm = null;
            };
        }

        private void Menu_ShowLevelZonesC1()
        {
            if (ShowLevelZonesForm != null)
            {
                ShowLevelZonesForm.Focus();
                return;
            }
            ShowLevelZonesForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelZonesForm.Show();
            List<int> zones = new();
            foreach (var entry in NSF.GetEntries<OldZoneEntry>())
            {
                zones.Add(entry.EID);
            }
            OldZoneEntryViewer viewer = new(NSF, zones) { Dock = DockStyle.Fill };
            ShowLevelZonesForm.Controls.Add(viewer);
            ShowLevelZonesForm.Text = string.Empty;
            ShowLevelZonesForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelZonesForm = null;
            };
        }

        private void Menu_ShowLevelC1Proto()
        {
            if (ShowLevelForm != null)
            {
                ShowLevelForm.Focus();
                return;
            }
            ShowLevelForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelForm.Show();
            List<int> worlds = new();
            foreach (var entry in NSF.GetEntries<ProtoSceneryEntry>())
            {
                worlds.Add(entry.EID);
            }
            ProtoSceneryEntryViewer viewer = new(NSF, worlds) { Dock = DockStyle.Fill };
            ShowLevelForm.Controls.Add(viewer);
            ShowLevelForm.Text = string.Empty;
            ShowLevelForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelForm = null;
            };
        }

        private void Menu_ShowLevelZonesC1Proto()
        {
            if (ShowLevelZonesForm != null)
            {
                ShowLevelZonesForm.Focus();
                return;
            }
            ShowLevelZonesForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelZonesForm.Show();
            List<int> zones = new();
            foreach (var entry in NSF.GetEntries<ProtoZoneEntry>())
            {
                zones.Add(entry.EID);
            }
            ProtoZoneEntryViewer viewer = new(NSF, zones) { Dock = DockStyle.Fill };
            ShowLevelZonesForm.Controls.Add(viewer);
            ShowLevelZonesForm.Text = string.Empty;
            ShowLevelZonesForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelZonesForm = null;
            };
        }

        private void Menu_ShowLevelC2()
        {
            if (ShowLevelForm != null)
            {
                ShowLevelForm.Focus();
                return;
            }
            ShowLevelForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelForm.Show();
            List<int> worlds = new();
            foreach (var entry in NSF.GetEntries<SceneryEntry>())
            {
                worlds.Add(entry.EID);
            }
            SceneryEntryViewer viewer = new(NSF, worlds) { Dock = DockStyle.Fill };
            ShowLevelForm.Controls.Add(viewer);
            ShowLevelForm.Text = string.Empty;
            ShowLevelForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelForm = null;
            };
        }

        private void Menu_ShowLevelZonesC2()
        {
            if (ShowLevelZonesForm != null)
            {
                ShowLevelZonesForm.Focus();
                return;
            }
            ShowLevelZonesForm = new() { Text = "Loading...", Width = 480, Height = 360 };
            ShowLevelZonesForm.Show();
            List<int> zones = new();
            foreach (var entry in NSF.GetEntries<ZoneEntry>())
            {
                zones.Add(entry.EID);
            }
            ZoneEntryViewer viewer = new(NSF, zones) { Dock = DockStyle.Fill };
            ShowLevelZonesForm.Controls.Add(viewer);
            ShowLevelZonesForm.Text = string.Empty;
            ShowLevelZonesForm.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                ShowLevelZonesForm = null;
            };
        }

        private void Menu_EditEntitiesC2()
        {
            if (EntityEditorForm != null)
            {
                EntityEditorForm.Focus();
                return;
            }
            EntityEditorForm = new(NSF);
            EntityEditorForm.FormClosed += (sender, e) =>
            {
                EntityEditorForm = null;
            };
            EntityEditorForm.Show();
        }

        private void Menu_EditSceneryC2()
        {
            if (SceneryEditorForm != null)
            {
                SceneryEditorForm.Focus();
                return;
            }
            SceneryEditorForm = new(NSF);
            SceneryEditorForm.FormClosed += (sender, e) =>
            {
                SceneryEditorForm = null;
            };
            SceneryEditorForm.Show();
        }

        private void Menu_ExportSceneryC1OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            ExportSceneryC1OBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
        }

        private void Menu_ExportSceneryC2OBJ()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            ExportSceneryC2OBJ(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
        }

        private void ExportSceneryC1OBJ(string path, string modelname)
        {
            var exporter = new OBJExporter();
            exporter.AddObject();

            foreach (OldSceneryEntry scenery in NSF.GetEntries<OldSceneryEntry>())
            {
                Console.WriteLine($"Exporting {scenery.EName}...");
                exporter.AddScenery(NSF, scenery);
            }
            exporter.Export(path, modelname, false);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        private void ExportSceneryC2OBJ(string path, string modelname)
        {
            var exporter = new OBJExporter();
            exporter.AddObject();

            foreach (SceneryEntry scenery in NSF.GetEntries<SceneryEntry>())
            {
                Console.WriteLine($"Exporting {scenery.EName}...");
                exporter.AddScenery(NSF, scenery);
            }
            exporter.Export(path, modelname, false);

            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        private void Menu_ExportZones()
        {
            if (!FileUtil.SelectSaveFile(out string filename, FileFilters.OBJ, FileFilters.Any))
                return;

            ExportZones(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
        }

        private void ExportZones(string path, string modelname)
        {
            var exporter = new ZoneExporter();

            foreach (ZoneEntry zone in NSF.GetEntries<ZoneEntry>())
            {
                //Console.WriteLine($"Exporting {zone.EName}...");
                exporter.AddZone(zone);
            }

            exporter.ExportZones(path, modelname);
            Console.WriteLine("Done.");
            SystemSounds.Asterisk.Play();
        }

        private void Menu_Import_Chunk()
        {
            byte[][] datas = FileUtil.OpenFiles([FileFilters.NSChunk, FileFilters.Any]);
            if (datas == null)
                return;
            ImportChunk(datas);
        }

        public void ImportChunk(byte[][] datas)
        {
            //bool process = DarkMessageBox.ShowMessage("Do you want to process the imported chunks?", "Import Chunk", DarkDialogButton.YesNo) == DialogResult.Yes;
            bool process = true; // always process

            foreach (var data in datas)
            {
                try
                {
                    UnprocessedChunk chunk = Chunk.Load(data);

                    // if the chunk is a texture chunk, always process
                    if (process || chunk.Type == 1)
                    {
                        Chunk processedchunk = chunk.Process();
                        if (processedchunk is EntryChunk)
                            ((EntryChunk)processedchunk).ProcessAll(GameVersion);
                        NSF.Chunks.Add(processedchunk);
                    }
                    else
                    {
                        NSF.Chunks.Add(chunk);
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }
        }

        private void Menu_Import_And_Replace_Chunk()
        {
            byte[][] datas = FileUtil.OpenFiles([FileFilters.NSChunk, FileFilters.Any]);
            if (datas == null)
                return;
            ImportAndReplaceChunk(datas);
        }

        public void ImportAndReplaceChunk(byte[][] datas)
        {
            foreach (var data in datas)
            {
                try
                {
                    UnprocessedChunk chunk = Chunk.Load(data);

                    int indexToReplace = -1;
                    for (int i = 0; i < NSF.Chunks.Count; ++i)
                    {
                        if (NSF.Chunks[i] is TextureChunk tex)
                        {
                            if (tex.EID == chunk.ChunkId)
                            {
                                indexToReplace = i;
                                break;
                            }
                        }
                        else
                        {
                            if (NSF.Chunks[i].ChunkId == chunk.ChunkId)
                            {
                                indexToReplace = i;
                                break;
                            }
                        }
                    }

                    Chunk processedchunk = chunk.Process();
                    if (processedchunk is EntryChunk)
                        ((EntryChunk)processedchunk).ProcessAll(GameVersion);
                    if (indexToReplace != -1)
                    {
                        NSF.Chunks[indexToReplace] = processedchunk;
                    }
                    else
                    {
                        NSF.Chunks.Add(processedchunk);
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }
        }

        private void Menu_Import_Entries_Into_New_Chunks()
        {
            byte[][] datas = FileUtil.OpenFiles(FileFilters.NSEntryExt, FileFilters.Any);
            if (datas == null)
                return;
            ImportEntriesIntoNewChunks(datas);
        }

        public void ImportEntriesIntoNewChunks(byte[][] datas)
        {
            bool process = DarkMessageBox.ShowMessage("Do you want to process the imported entries?", "Import Entry", DarkDialogButton.YesNo) == DialogResult.Yes;

            NormalChunk? currentChunk = null;
            int currentChunkSize = 0;

            foreach (var data in datas)
            {
                if (data.Length < 4 || data.Length >= 65536)
                    continue;
                try
                {
                    UnprocessedEntry new_entry = Entry.Load(data);

                    bool already_exists = false;
                    foreach (var ch in NSF.Chunks)
                    {
                        if (ch is EntryChunk ec)
                        {
                            foreach (var entry2 in ec.Entries)
                            {
                                if (entry2.EID == new_entry.EID)
                                {
                                    already_exists = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (already_exists)
                    {
                        ErrorManager.SignalIgnorableError($"Entry with EID {Entry.EIDToEName(new_entry.EID)} already exists. Skipping import.");
                        continue;
                    }

                    Entry entryToAdd = process ? new_entry.Process(GameVersion) : new_entry;                    
                    int entrySize = entryToAdd.Save().Length + 4;

                    if (currentChunk == null || currentChunkSize + entrySize > 65536)
                    {
                        currentChunk = new NormalChunk();
                        currentChunk.Entries.Add(entryToAdd);
                        NSF.Chunks.Add(currentChunk);
                        currentChunkSize = 0x14 + entrySize;
                    }
                    else
                    {                                                
                        currentChunk.Entries.Add(entryToAdd);
                        currentChunkSize += entrySize;                        
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }
            NeedsNewEditor = true;
        }

        private void Menu_Import_And_Replace_Entry()
        {
            byte[][] datas = FileUtil.OpenFiles(FileFilters.NSEntryExt, FileFilters.Any);
            if (datas == null)
                return;
            ImportAndReplaceEntry(datas);
        }

        public void ImportAndReplaceEntry(byte[][] datas)
        {
            bool process = true;

            NormalChunk? currentChunk = null;
            int currentChunkSize = 0;

            foreach (var data in datas)
            {
                if (data.Length < 4 || data.Length >= 65536)
                    continue;
                try
                {
                    UnprocessedEntry entry = Entry.Load(data);
                    Entry entryToAdd = process ? entry.Process(GameVersion) : entry;

                    bool replaced = false;
                    bool found = false;
                    foreach (Chunk chunk in NSF.Chunks)
                    {
                        if (chunk is EntryChunk entryChunk)
                        {
                            for (int i = 0; i < entryChunk.Entries.Count; ++i)
                            {
                                if (entryChunk.Entries[i].EID == entry.EID)
                                {
                                    entryChunk.Entries[i] = entryToAdd;

                                    // check if the chunk is now too big, and if so, remove the entry again and add it to a new chunk instead
                                    int totalSize = 0;
                                    for (int j = 0; j < entryChunk.Entries.Count; ++j)
                                    {
                                        totalSize += entryChunk.Entries[j].Save().Length + 4;
                                    }

                                    if (totalSize + 0x14 > Chunk.Length)
                                    {
                                        entryChunk.Entries.RemoveAt(i);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        replaced = true;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (found)
                            break;
                    }

                    // if it wasn't found, or it was found but couldn't be replaced, add it to a new chunk
                    if (!replaced)
                    {
                        int entrySize = entryToAdd.Save().Length + 4;

                        if (currentChunk == null || currentChunkSize + entrySize > 65536)
                        {
                            currentChunk = new NormalChunk();
                            currentChunk.Entries.Add(entryToAdd);
                            NSF.Chunks.Add(currentChunk);
                            currentChunkSize = 0x14 + entrySize;
                        }
                        else
                        {
                            currentChunk.Entries.Add(entryToAdd);
                            currentChunkSize += entrySize;
                        }
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }
            NeedsNewEditor = true;
        }
    }
}
