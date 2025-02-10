using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(NSF))]
    public sealed class NSFController : LegacyController
    {
        public NSFController(NSF nsf, SubcontrollerGroup parentGroup) : base(parentGroup, nsf)
        {
            NSF = nsf;
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddNormalChunk, "JournalOrange", Menu_Add_NormalChunk);
            if (GameVersion != GameVersion.Crash2 && GameVersion != GameVersion.Crash3 && GameVersion != GameVersion.Crash1)
                AddMenu(CrashUI.Properties.Resources.NSFController_AcAddOldSoundChunk, "JournalBlue", Menu_Add_OldSoundChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddSoundChunk, "JournalBlue", Menu_Add_SoundChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddWavebankChunk, "JournalRed",Menu_Add_WavebankChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddSpeechChunk, "JournalWhite", Menu_Add_SpeechChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcAddTextureChunk, "Painting", Menu_Add_TextureChunk);
            AddMenu(CrashUI.Properties.Resources.NSFController_AcImportChunk, "Import", Menu_Import_Chunk);
            if (GameVersion == GameVersion.Crash2 || GameVersion == GameVersion.Crash3)
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
            }
            else if (GameVersion == GameVersion.Crash1Beta1995)
            {
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevel, "ThingBlue", Menu_ShowLevelC1Proto);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevelZones, "ThingViolet", Menu_ShowLevelZonesC1Proto);
            }
            else if (GameVersion == GameVersion.Crash2 || GameVersion == GameVersion.Crash3)
            {
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevel, "ThingBlue", Menu_ShowLevelC2);
                AddMenu(CrashUI.Properties.Resources.NSFController_AcShowLevelZones, "ThingViolet", Menu_ShowLevelZonesC2);
            }
        }

        public NSF NSF { get; }

        private DarkForm? ShowLevelForm { get; set; }
        private DarkForm? ShowLevelZonesForm { get; set; }

        public void Kill()
        {
            ShowLevelForm?.Close();
            ShowLevelZonesForm?.Close();
            ShowLevelForm?.Dispose();
            ShowLevelZonesForm?.Dispose();
            ShowLevelForm = null;
            ShowLevelZonesForm = null;
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
            byte[] header = { 0x34, 0x12, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0xAE, 0x5B, 0x34, 0x56};
            byte[] newchunk = new byte[65536];
            Array.Copy(header, 0, newchunk, 0, header.Length);
            TextureChunk chunk = new TextureChunk(newchunk);
            NSF.Chunks.Add(chunk);
        }

        public void Menu_AnalyzeLevel()
        {
            List<string> loadlistLines = new List<string>();
            List<string> drawlistLines = new List<string>();
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
                        List<int> drawnids = new List<int>();
                        string idlist = string.Empty;
                        for (int i = 0; i < entity.Positions.Count; ++i)
                        {
                            foreach (var row in entity.DrawListB.Rows)
                            {
                                if (row.MetaValue == i)
                                {
                                    // draw
                                    foreach (int id in row.Values)
                                    {
                                        drawnids.Add(id);
                                    }
                                }
                            }
                            foreach (var row in entity.DrawListA.Rows)
                            {
                                if (row.MetaValue == i)
                                {
                                    // undraw
                                    foreach (int id in row.Values)
                                    {
                                        if (!drawnids.Remove(id))
                                        {
                                            idlist += $"\n\t\t  [position {i}] {id >> 8 & 0xFFFF}";
                                        }
                                    }
                                }
                            }
                        }
                        if (idlist != string.Empty)
                        {
                            // Draw List A
                            drawlistLines.Add($"[{zone.EName}, camera {entity.CameraIndex}] The following entities were already undrawn (Draw List A):{idlist}");
                        }
                        if (drawnids.Count != 0)
                        {
                            string idlist2 = string.Empty;
                            for (int i = 0; i < entity.Positions.Count; ++i)
                            {
                                foreach (var row in entity.DrawListB.Rows)
                                {
                                    if (row.MetaValue == i)
                                    {
                                        foreach (int id in row.Values)
                                        {
                                            if (drawnids.Remove(id))
                                            {
                                                idlist2 += $"\n\t\t  [position {i}] {id >> 8 & 0xFFFF}";
                                            }
                                        }
                                    }
                                }
                            }
                            // Draw List B
                            drawlistLines.Add($"[{zone.EName}, camera {entity.CameraIndex}] The following entities are never undrawn (Draw List B):{idlist2}");
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
            if (drawlistLines.Count > 0)
                Console.WriteLine(string.Join(Environment.NewLine, drawlistLines.ToArray()));
            else
                Console.WriteLine("No draw list issues were found.");
        }

        public void Menu_FindEntities()
        {
            int type = -1;
            int subtype = -1;
            using (InputWindow inputWindow = new InputWindow("Enter entity type:", CrashUI.Properties.Resources.NSFController_AcFindEntities, string.Empty))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(inputWindow.Input, out type) && type >= 0) { }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid input.", Resources.Title_InputError);
                        return;
                    }
                }
                else return;
            }
            using (InputWindow inputWindow = new InputWindow("Enter entity subtype (leave empty to search all):", CrashUI.Properties.Resources.NSFController_AcFindEntities, string.Empty))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(inputWindow.Input))
                    {
                        if (int.TryParse(inputWindow.Input, out subtype) && subtype >= 0) { }
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
                    Console.WriteLine($"Total: {nitros.Count}");
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
                            if ((entity.Type == 0 && entity.Subtype == 0) || (entity.Type == 4 && entity.Subtype == 17))
                            {
                                willys.Add(entity);
                            }
                            else if (entity.Type == 34 && entity.ID.HasValue)
                            {
                                if (GameVersion != GameVersion.Crash2 && GameVersion != GameVersion.Crash3)
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
                                else if (GameVersion == GameVersion.Crash3)
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
                    Console.WriteLine($"Total: {boxcount}");
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

        private void Menu_Import_Chunk()
        {
            byte[][] datas = FileUtil.OpenFiles(FileFilters.Any);
            if (datas == null)
                return;
            bool process = DarkMessageBox.ShowMessage("Do you want to process the imported chunks?", "Import Chunk", DarkDialogButton.YesNo) == DialogResult.Yes;
            foreach (var data in datas)
            {
                try
                {
                    UnprocessedChunk chunk = Chunk.Load(data);
                    if (chunk.Type == 1) // Texture Chunk
                        process = true;
                    if (process)
                    {
                        Chunk processedchunk = chunk.Process();
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
    }
}
