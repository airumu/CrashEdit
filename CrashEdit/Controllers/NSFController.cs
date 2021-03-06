using Crash;
using CrashEdit.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class NSFController : Controller
    {
        public NSFController(NSF nsf,GameVersion gameversion)
        {
            NSF = nsf;
            GameVersion = gameversion;
            foreach (Chunk chunk in nsf.Chunks)
            {
                AddNode(CreateChunkController(chunk));
            }
            AddMenu(Crash.UI.Properties.Resources.NSFController_AcAddNormalChunk,Menu_Add_NormalChunk);
            if (GameVersion != GameVersion.Crash2 && GameVersion != GameVersion.Crash3 && GameVersion != GameVersion.Crash1)
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcAddOldSoundChunk,Menu_Add_OldSoundChunk);
            AddMenu(Crash.UI.Properties.Resources.NSFController_AcAddSoundChunk,Menu_Add_SoundChunk);
            AddMenu(Crash.UI.Properties.Resources.NSFController_AcAddWavebankChunk,Menu_Add_WavebankChunk);
            AddMenu(Crash.UI.Properties.Resources.NSFController_AcAddSpeechChunk,Menu_Add_SpeechChunk);
            AddMenu(Crash.UI.Properties.Resources.NSFController_AcImportChunk,Menu_Import_Chunk);
            if (GameVersion == GameVersion.Crash2 || GameVersion == GameVersion.Crash3)
            {
                AddMenuSeparator();
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcFixDetonator,Menu_Fix_Detonator);
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcFixBoxCount,Menu_Fix_BoxCount);
                AddMenuSeparator();
            }
            if (GameVersion == GameVersion.Crash1 || GameVersion == GameVersion.Crash1BetaMAR08 || GameVersion == GameVersion.Crash1BetaMAY11)
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcShowLevel,Menu_ShowLevelC1);
            else if (GameVersion == GameVersion.Crash2)
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcShowLevel,Menu_ShowLevelC2);
            else if (GameVersion == GameVersion.Crash3)
                AddMenu(Crash.UI.Properties.Resources.NSFController_AcShowLevel,Menu_ShowLevelC3);
            InvalidateNode();
            InvalidateNodeImage();
        }

        public override void InvalidateNode()
        {
            Node.Text = Crash.UI.Properties.Resources.NSFController_Text;
        }

        public override void InvalidateNodeImage()
        {
            Node.ImageKey = "nsf";
            Node.SelectedImageKey = "nsf";
        }

        public NSF NSF { get; }
        public GameVersion GameVersion { get; }

        public ChunkController CreateChunkController(Chunk chunk)
        {
            if (chunk is NormalChunk)
            {
                return new NormalChunkController(this, (NormalChunk)chunk);
            }
            else if (chunk is TextureChunk)
            {
                return new TextureChunkController(this, (TextureChunk)chunk);
            }
            else if (chunk is OldSoundChunk)
            {
                return new OldSoundChunkController(this, (OldSoundChunk)chunk);
            }
            else if (chunk is SoundChunk)
            {
                return new SoundChunkController(this, (SoundChunk)chunk);
            }
            else if (chunk is WavebankChunk)
            {
                return new WavebankChunkController(this, (WavebankChunk)chunk);
            }
            else if (chunk is SpeechChunk)
            {
                return new SpeechChunkController(this, (SpeechChunk)chunk);
            }
            else if (chunk is UnprocessedChunk)
            {
                return new UnprocessedChunkController(this, (UnprocessedChunk)chunk);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void Menu_Add_NormalChunk()
        {
            NormalChunk chunk = new NormalChunk();
            NSF.Chunks.Add(chunk);
            NormalChunkController controller = new NormalChunkController(this,chunk);
            AddNode(controller);
        }

        private void Menu_Add_OldSoundChunk()
        {
            OldSoundChunk chunk = new OldSoundChunk();
            NSF.Chunks.Add(chunk);
            OldSoundChunkController controller = new OldSoundChunkController(this,chunk);
            AddNode(controller);
        }

        private void Menu_Add_SoundChunk()
        {
            SoundChunk chunk = new SoundChunk();
            NSF.Chunks.Add(chunk);
            SoundChunkController controller = new SoundChunkController(this,chunk);
            AddNode(controller);
        }

        private void Menu_Add_WavebankChunk()
        {
            WavebankChunk chunk = new WavebankChunk();
            NSF.Chunks.Add(chunk);
            WavebankChunkController controller = new WavebankChunkController(this,chunk);
            AddNode(controller);
        }

        private void Menu_Add_SpeechChunk()
        {
            SpeechChunk chunk = new SpeechChunk();
            NSF.Chunks.Add(chunk);
            SpeechChunkController controller = new SpeechChunkController(this,chunk);
            AddNode(controller);
        }

        private void Menu_Fix_Detonator()
        {
            List<Entity> nitros = new List<Entity>();
            List<Entity> detonators = new List<Entity>();
            foreach (Chunk chunk in NSF.Chunks)
            {
                if (chunk is EntryChunk)
                {
                    foreach (Entry entry in ((EntryChunk)chunk).Entries)
                    {
                        if (entry is NewZoneEntry)
                        {
                            foreach (Entity entity in ((NewZoneEntry)entry).Entities)
                            {
                                if (entity.Type == 34)
                                {
                                    if (entity.Subtype == 18 && entity.ID.HasValue)
                                    {
                                        nitros.Add(entity);
                                    }
                                    else if (entity.Subtype == 24)
                                    {
                                        detonators.Add(entity);
                                    }
                                }
                            }
                        }
                        if (entry is ZoneEntry)
                        {
                            foreach (Entity entity in ((ZoneEntry)entry).Entities)
                            {
                                if (entity.Type == 34)
                                {
                                    if (entity.Subtype == 18 && entity.ID.HasValue)
                                    {
                                        nitros.Add(entity);
                                    }
                                    else if (entity.Subtype == 24)
                                    {
                                        detonators.Add(entity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (Entity detonator in detonators)
            {
                detonator.Victims.Clear();
                foreach (Entity nitro in nitros)
                {
                    detonator.Victims.Add(new EntityVictim((short)nitro.ID.Value));
                }
            }
        }

        private void Menu_Fix_BoxCount()
        {
            int boxcount = 0;
            List<Entity> willys = new List<Entity>();
            foreach (Chunk chunk in NSF.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is ZoneEntry zone2)
                        {
                            foreach (Entity entity in zone2.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                                else if (entity.Type == 34)
                                {
                                    if (Settings.Default.UseCustomCrates)
                                    {
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
                                            case 11: // pow
                                            case 13: // ghost
                                            case 17: // auto pickup
                                            case 18: // nitro
                                            case 20: // auto empty
                                            case 21: // empty 2
                                            case 23: // steel
                                            case 25: // steel pickup
                                            case 26: // steel fruit
                                                boxcount++;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    else
                                    {
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
                                            case 17: // auto pickup
                                            case 18: // nitro
                                            case 20: // auto empty
                                            case 21: // empty 2
                                            case 23: // steel
                                                boxcount++;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        if (entry is NewZoneEntry zone3)
                        {
                            foreach (Entity entity in zone3.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                                else if (entity.Type == 34)
                                {
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
                                        case 11: // pow
                                        case 13: // ghost
                                        case 17: // auto pickup
                                        case 18: // nitro
                                        case 20: // auto empty
                                        case 21: // empty 2
                                        case 23: // steel
                                        case 25: // slot
                                            boxcount++;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else if (entity.Type == 36)
                                {
                                    if (entity.Subtype == 1)
                                    {
                                        boxcount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (Entity willy in willys)
            {
                if (willy.BoxCount.HasValue)
                {
                    willy.BoxCount = new EntitySetting(0,boxcount);
                }
            }
        }

        private void Menu_ShowLevelC1()
        {
            List<TextureChunk[]> sortedtexturechunks = new List<TextureChunk[]>();
            List<OldSceneryEntry> sceneryentries = new List<OldSceneryEntry>();
            foreach (Chunk chunk in NSF.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is OldSceneryEntry sceneryentry)
                        {
                            sceneryentries.Add(sceneryentry);
                            TextureChunk[] texturechunks = new TextureChunk[BitConv.FromInt32(sceneryentry.Info,0x18)];
                            for (int i = 0; i < texturechunks.Length; ++i)
                            {
                                texturechunks[i] = NSF.FindEID<TextureChunk>(BitConv.FromInt32(sceneryentry.Info,0x20+i*4));
                            }
                            sortedtexturechunks.Add(texturechunks);
                        }
                    }
                }
            }
            Form frm = new DarkUI.Forms.DarkForm() { Text = "Loading...", Width = 480, Height = 360 };
            frm.Show();
            OldSceneryEntryViewer viewer = new OldSceneryEntryViewer(sceneryentries,sortedtexturechunks.ToArray()) { Dock = DockStyle.Fill };
            frm.Controls.Add(viewer);
            frm.Text = string.Empty;
        }
        
        private void Menu_ShowLevelC2()
        {
            List<TextureChunk[]> sortedtexturechunks = new List<TextureChunk[]>();
            List<SceneryEntry> sceneryentries = new List<SceneryEntry>();
            foreach (Chunk chunk in NSF.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is SceneryEntry sceneryentry)
                        {
                            sceneryentries.Add(sceneryentry);
                            TextureChunk[] texturechunks = new TextureChunk[BitConv.FromInt32(sceneryentry.Info,0x28)];
                            for (int i = 0; i < texturechunks.Length; ++i)
                            {
                                texturechunks[i] = NSF.FindEID<TextureChunk>(BitConv.FromInt32(sceneryentry.Info,0x2C+i*4));
                            }
                            sortedtexturechunks.Add(texturechunks);
                        }
                    }
                }
            }
            Form frm = new DarkUI.Forms.DarkForm() { Text = "Loading...", Width = 480, Height = 360 };
            frm.Show();
            SceneryEntryViewer viewer = new SceneryEntryViewer(sceneryentries,sortedtexturechunks.ToArray()) { Dock = DockStyle.Fill };
            frm.Controls.Add(viewer);
            frm.Text = string.Empty;
        }

        private void Menu_ShowLevelC3()
        {
            List<TextureChunk[]> sortedtexturechunks = new List<TextureChunk[]>();
            List<NewSceneryEntry> sceneryentries = new List<NewSceneryEntry>();
            foreach (Chunk chunk in NSF.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is NewSceneryEntry newsceneryentry)
                        {
                            sceneryentries.Add(newsceneryentry);
                            TextureChunk[] texturechunks = new TextureChunk[BitConv.FromInt32(newsceneryentry.Info,0x28)];
                            for (int i = 0; i < texturechunks.Length; ++i)
                            {
                                texturechunks[i] = NSF.FindEID<TextureChunk>(BitConv.FromInt32(newsceneryentry.Info,0x2C+i*4));
                            }
                            sortedtexturechunks.Add(texturechunks);
                        }
                    }
                }
            }
            Form frm = new DarkUI.Forms.DarkForm() { Text = "Loading...", Width = 480, Height = 360 };
            frm.Show();
            NewSceneryEntryViewer viewer = new NewSceneryEntryViewer(sceneryentries,sortedtexturechunks.ToArray()) { Dock = DockStyle.Fill };
            frm.Controls.Add(viewer);
            frm.Text = string.Empty;
        }

        private void Menu_Import_Chunk()
        {
            byte[][] datas = FileUtil.OpenFiles(FileFilters.Any);
            if (datas == null)
                return;
            bool process = DarkUI.Forms.DarkMessageBox.ShowInformation("Do you want to process the imported chunks?", "Import Chunk", DarkUI.Forms.DarkDialogButton.YesNo) == DialogResult.Yes;
            foreach (var data in datas)
            {
                try
                {
                    UnprocessedChunk chunk = Chunk.Load(data);
                    if (process)
                    {
                        Chunk processedchunk = chunk.Process(NSF.Chunks.Count*2 + 1);
                        NSF.Chunks.Add(processedchunk);
                        AddNode(CreateChunkController(processedchunk));
                    }
                    else
                    {
                        NSF.Chunks.Add(chunk);
                        AddNode(new UnprocessedChunkController(this,chunk));
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }
        }
    }
}
