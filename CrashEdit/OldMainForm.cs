using AltUI.Forms;
using CrashEdit.CE.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using CrashEdit.CrashUI;
using DiscUtils.Iso9660;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CrashEdit.CE
{
    public sealed class OldMainForm : MainForm
    {
        private ToolStripButton tbbOpen = new();
        private ToolStripButton tbbSave = new();
        private ToolStripButton tbbPatchNSD = new();
        private ToolStripButton tbbClose = new();
        private ToolStripButton tbbPlay = new();
        private ToolStripButton tbbBIN = new();
        private ToolStripButton tbbPAL = new();
        private ToolStripLabel tlbDefaultVersion = new();
        private ToolStripComboBox tbxDefaultVersion = new();
        private ToolStripMenuItem tbxMakeBIN = new();
        private ToolStripMenuItem tbxConvertVHVB = new();
        private ToolStripMenuItem tbxConvertVAB = new();
        private ToolStripMenuItem tbxConvertAnimations = new();
        private ToolStripMenuItem tbxShowGOOLMap = new();
        private ToolStripMenuItem tbxGenerateSpawnPoint = new();
        private ToolStripMenuItem tbbExtra = new();

        private TabControl tbcTabs;
        private GameVersionForm dlgGameVersion;
        private BackgroundWorker bgwMakeBIN;
        private ProgressBarForm dlgProgress;

        private ConvertAnimationsForm formConvertAnimations;
        private MakeBin formMakebin;
        private Dictionary<string, DarkForm> formShowGOOLMap;

        public static bool PAL { get; private set; } = Settings.Default.ModePAL;
        private const int RateNTSC = 30;
        private const int RatePAL = 25;

        public OldMainForm()
        {
            ToolStripButtonInit(tbbOpen, "FolderOpen", Resources.Toolbar_Open, $"{Resources.Toolbar_Open} (Ctrl + O)");
            tbbOpen.Click += new EventHandler(tbbOpen_Click);

            ToolStripButtonInit(tbbSave, "Floppy", Resources.Toolbar_Save, $"{Resources.Toolbar_Save} (Ctrl + S)");
            tbbSave.Click += new EventHandler(tbbSave_Click);

            ToolStripButtonInit(tbbPatchNSD, "Floppy2", Resources.Toolbar_Patch, $"{Resources.Toolbar_PatchNSD} (Ctrl + Shift + S)");
            tbbPatchNSD.Click += new EventHandler(tbbPatchNSD_Click);

            ToolStripButtonInit(tbbClose, "Folder", Resources.Toolbar_Close, $"{Resources.Toolbar_Close} (Ctrl + W)");
            tbbClose.Click += new EventHandler(tbbClose_Click);

            ToolStripButtonInit(tbbPAL, "Earth", Resources.Toolbar_PAL, Resources.Toolbar_PAL);
            tbbPAL.CheckOnClick = true;
            tbbPAL.Checked = Settings.Default.ModePAL;
            tbbPAL.Click += new EventHandler(tbbPAL_Click);

            ToolStripButtonInit(tbbPlay, "Controller", Resources.Toolbar_Play, $"{Resources.Toolbar_Play} (F1)");
            tbbPlay.Click += new EventHandler(tbbPlay_Click);

            ToolStripButtonInit(tbbBIN, "CD", Resources.Toolbar_BIN, "Make Bin");
            tbbBIN.Click += new EventHandler(tbbBIN_Click);

            tlbDefaultVersion.Text = "Set default game version:";

            tbxDefaultVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            tbxDefaultVersion.ComboBox.Items.AddRange(new string[] { "Default", "Crash1", "Crash2", "Crash3" });
            tbxDefaultVersion.SelectedIndex = Settings.Default.DefaultGameVersion;
            tbxDefaultVersion.SelectedIndexChanged += new EventHandler(tbxDefaultVersion_SelectedIndexChanged);

            tbxMakeBIN.Text = Resources.OldMainForm_tbxMakeBIN;
            tbxMakeBIN.Click += new EventHandler(tbxMakeBIN_Click);

            tbxConvertVHVB.Text = Resources.OldMainForm_tbxConvertVHVB;
            tbxConvertVHVB.Click += new EventHandler(tbxConvertVHVB_Click);

            tbxConvertVAB.Text = Resources.OldMainForm_tbxConvertVAB;
            tbxConvertVAB.Click += new EventHandler(tbxConvertVAB_Click);

            tbxConvertAnimations.Text = Resources.OldMainForm_tbxConvertAnimations;
            tbxConvertAnimations.Click += new EventHandler(tbxConvertAnimations_Click);

            tbxShowGOOLMap.Text = Resources.OldMainForm_tbxShowGOOLMap;
            tbxShowGOOLMap.Click += new EventHandler(tbxShowGOOLMap_Click);

            tbxGenerateSpawnPoint.Text = Resources.OldMainForm_tbxGenerateSpawnPoint;
            tbxGenerateSpawnPoint.Click += new EventHandler(tbxGenerateSpawnPoint_Click);

            tbbExtra.Text = Resources.OldMainForm_tbbExtra;
            tbbExtra.ImageKey = "Dropdown";
            tbbExtra.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tbbExtra.TextImageRelation = TextImageRelation.TextBeforeImage;
            tbbExtra.DropDown.Items.Add(tlbDefaultVersion);
            tbbExtra.DropDown.Items.Add(tbxDefaultVersion);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxMakeBIN);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxConvertVHVB);
            tbbExtra.DropDown.Items.Add(tbxConvertVAB);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxConvertAnimations);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxShowGOOLMap);
            tbbExtra.DropDown.Items.Add(tbxGenerateSpawnPoint);

            ToolStrip.Items.Insert(0, tbbOpen);
            ToolStrip.Items.Insert(1, tbbSave);
            ToolStrip.Items.Insert(2, tbbPatchNSD);
            ToolStrip.Items.Insert(3, tbbClose);
            ToolStrip.Items.Insert(4, new ToolStripSeparator());
            ToolStrip.Items.Insert(5, tbbPAL);
            ToolStrip.Items.Insert(6, tbbPlay);
            ToolStrip.Items.Insert(7, new ToolStripSeparator());
            ToolStrip.Items.Insert(8, tbbBIN);
            ToolStrip.Items.Insert(9, new ToolStripSeparator());
            ToolStrip.Items.Add(tbbExtra);

            tbcTabs = TabControl;
            tbcTabs.SelectedIndexChanged += tbcTabs_SelectedIndexChanged;

            TabPage configtab = new TabPage("CrashEdit")
            {
                Tag = new ConfigEditor() { Dock = DockStyle.Fill }
            };
            configtab.Controls.Add((ConfigEditor)configtab.Tag);

            tbcTabs.TabPages.Add(configtab);

            tbcTabs_SelectedIndexChanged(null, null);

            dlgGameVersion = new GameVersionForm();

            bgwMakeBIN = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            bgwMakeBIN.DoWork += new DoWorkEventHandler(bgwMakeBIN_DoWork);
            bgwMakeBIN.ProgressChanged += new ProgressChangedEventHandler(bgwMakeBIN_ProgressChanged);
            bgwMakeBIN.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwMakeBIN_RunWorkerCompleted);
            dlgProgress = null!;

            formConvertAnimations = null!;
            formMakebin = null!;
            formShowGOOLMap = new();

            Icon = OldResources.CBHacksIconAlt;
            // Width = Settings.Default.DefaultFormW;
            // Height = Settings.Default.DefaultFormH;
            Load += new EventHandler(OldMainForm_Load);
            FormClosing += new FormClosingEventHandler(OldMainForm_FormClosing);
            Text = $"CrashEdit v{Assembly.GetExecutingAssembly().GetName().Version}";

            if (Settings.Default.ApplyMica)
            {
                BackColor = Color.FromArgb(31, 31, 32);
            }
            else
            {
                // This must be a color that never used in the other controls
                BackColor = Color.FromArgb(29, 30, 31);
            }
        }

        public void ToolStripButtonInit(ToolStripButton tbb, string imageKey, string text, string tooltip)
        {
            tbb.Text = text;
            tbb.ImageKey = imageKey;
            tbb.ToolTipText = tooltip;
            tbb.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tbb.TextImageRelation = TextImageRelation.ImageAboveText;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                // Open NSF
                case (Keys.Control | Keys.O):
                    ToolStrip.Items[0].PerformClick();
                    break;
                // Save NSF
                case (Keys.Control | Keys.S):
                    ToolStrip.Items[1].PerformClick();
                    break;
                // Patch NSD
                case (Keys.Control | Keys.Shift | Keys.S):
                    ToolStrip.Items[2].PerformClick();
                    break;
                // Close NSF
                case (Keys.Control | Keys.W):
                    ToolStrip.Items[3].PerformClick();
                    break;
                // Play
                case (Keys.F1):
                    ToolStrip.Items[6].PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tbcTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tab = tbcTabs.SelectedTab;
            tbbSave.Enabled =
            tbbPatchNSD.Enabled =
            tbbClose.Enabled =
            tbbPlay.Enabled =
            tbxShowGOOLMap.Enabled =
            tbxGenerateSpawnPoint.Enabled = tab != null && tab.Tag is NSFBox;
        }

        void tbbPAL_Click(object sender, EventArgs e)
        {
            PAL = tbbPAL.Checked;
            Settings.Default.ModePAL = tbbPAL.Checked;
            Settings.Default.Save();
        }

        void tbbPlay_Click(object sender, EventArgs e)
        {
            var tab = tbcTabs.SelectedTab;
            if (tab == null || !(tab.Tag is NSFBox))
                return;

            var nsfBox = (NSFBox)tab.Tag;
            var nsf = nsfBox.NSF;

            var nsfFilename = tbcTabs.SelectedTab.Text;

            var nsfFilenameBase = Path.GetFileName(nsfFilename);
            if (nsfFilenameBase.Length != 12 || !int.TryParse(nsfFilenameBase.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null, out var levelID))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error1, nsfFilename), Resources.Playtest_Title);
                return;
            }

            string nsdFilename = GetNSDFileName(nsfFilename);
            if (string.IsNullOrEmpty(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error2, nsfFilename), Resources.Playtest_Title);
                return;
            }
            if (!File.Exists(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error3, nsdFilename), Resources.Playtest_Title);
                return;
            }

            string exeFilename = null;
            var isofsPath = Path.GetDirectoryName(Path.GetDirectoryName(nsfFilename));
            foreach (string s in Directory.GetFiles(isofsPath))
            {
                if (Regex.IsMatch(Path.GetFileName(s).ToUpper(), @"^(S[CL][UEP]S_\d\d\d\.\d\d|PSX\.EXE)$"))
                {
                    exeFilename = s;
                    break;
                }
            }
            if (exeFilename == null)
            {
                DarkMessageBox.ShowError(Resources.Playtest_Error4, Resources.Playtest_Title);
                return;
            }

            string kdatDir = Path.Combine(isofsPath, "S3");
            string kdatFilename = null;
            if (Directory.Exists(kdatDir))
            {
                foreach (string s in Directory.GetFiles(kdatDir))
                {
                    if (Path.GetFileName(s).ToUpper() == "KDAT.DAT")
                    {
                        kdatFilename = s;
                        break;
                    }
                }
            }

            string warpscusDir = Path.Combine(isofsPath, "S0");
            string warpscusFilename = null;
            if (Directory.Exists(warpscusDir))
            {
                foreach (string s in Directory.GetFiles(warpscusDir))
                {
                    if (Regex.IsMatch(Path.GetFileName(s).ToUpper(), @"^WARPSC[UEP]S\.BIN$"))
                    {
                        warpscusFilename = s;
                        break;
                    }
                }
            }

            string basePath;
            do
            {
                basePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            } while (Directory.Exists(basePath));
            Directory.CreateDirectory(basePath);

            File.Copy(nsfFilename, Path.Combine(basePath, Path.GetFileName(nsfFilename)));
            File.Copy(nsdFilename, Path.Combine(basePath, Path.GetFileName(nsdFilename)));
            nsfFilename = Path.Combine(basePath, Path.GetFileName(nsfFilename));
            nsdFilename = Path.Combine(basePath, Path.GetFileName(nsdFilename));
            bool temp_nsf_autosave_setting = Settings.Default.PatchNSDSavesNSF;
            Settings.Default.PatchNSDSavesNSF = false;
            PatchNSD(nsdFilename, true, nsfBox.NSFController, true, true);
            SaveNSF(nsfFilename, nsf, true);
            Settings.Default.PatchNSDSavesNSF = temp_nsf_autosave_setting;
            var fs = new CDBuilder();
            fs.AddFile("S0\\" + Path.GetFileName(nsfFilename) + ";1", nsfFilename);
            fs.AddFile("S0\\" + Path.GetFileName(nsdFilename) + ";1", nsdFilename);
            fs.AddFile("M0\\" + Path.GetFileName(nsfFilename) + ";1", nsfFilename);
            fs.AddFile("M0\\" + Path.GetFileName(nsdFilename) + ";1", nsdFilename);
            fs.AddFile("PSX.EXE;1", exeFilename);
            if (warpscusFilename != null) fs.AddFile("S0\\" + Path.GetFileName(warpscusFilename) + ";1", warpscusFilename);
            if (kdatFilename != null) fs.AddFile("S3\\" + Path.GetFileName(kdatFilename) + ";1", kdatFilename);

            string binPath = Path.Combine(basePath, "game.bin");
            MakeBinWithProgressBar(fs, binPath);

            var regionStr = PAL ? "pal" : "ntsc";

            Task.Run(() =>
            {
                try
                {
                    ExternalTool.Invoke("pcsx-hdbg", $"gamefile=\"{binPath}\" bootlevel={levelID} region={regionStr}");
                }
                catch (FileNotFoundException)
                {
                    DarkMessageBox.ShowError(Resources.Playtest_Error5, Resources.Playtest_Title);
                }
                Directory.Delete(basePath, true);
            });
        }

        public static int GetRate()
        {
            return PAL ? RatePAL : RateNTSC;
        }

        void tbbOpen_Click(object sender, EventArgs e)
        {
            OpenNSF();
        }

        void tbbSave_Click(object sender, EventArgs e)
        {
            SaveNSF(false);
        }

        void tbbPatchNSD_Click(object sender, EventArgs e)
        {
            PatchNSD();
        }

        void tbbClose_Click(object sender, EventArgs e)
        {
            CloseNSF();
        }

        public void OpenNSF()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = FileFilters.NSF + "|" + FileFilters.Any;
                dialog.Multiselect = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string filename in dialog.FileNames)
                    {
                        OpenNSF(filename);
                    }
                }
            }
        }

        public void OpenNSF(string filename)
        {
            try
            {
                byte[] nsfdata = File.ReadAllBytes(filename);
                bool isdefault = true;
                if (Settings.Default.DefaultGameVersion != 0)
                {
                    dlgGameVersion.SelectedVersion = (GameVersion)Enum.Parse(typeof(GameVersion), tbxDefaultVersion.Text);
                    isdefault = false;
                }
                if (!isdefault || dlgGameVersion.ShowDialog(this) == DialogResult.OK)
                {
                    NSF nsf = NSF.LoadAndProcess(nsfdata, dlgGameVersion.SelectedVersion);
                    OpenNSF(filename, nsf, dlgGameVersion.SelectedVersion);
                }
            }
            catch (LoadAbortedException)
            {
            }
        }

        public void OpenNSF(string filename, NSF nsf, GameVersion gameversion)
        {
            var ws = new LevelWorkspace();
            ws.NSF = nsf;
            ws.GameVersion = gameversion;
            NSFBox nsfbox = new NSFBox(this, ws)
            {
                Dock = DockStyle.Fill
            };
            nsfbox.ActiveControllerChanged += MainControl_ActiveControllerChanged;

            TabPage nsftab = new TabPage(filename)
            {
                Tag = nsfbox
            };
            nsftab.Controls.Add(nsfbox);

            tbcTabs.TabPages.Add(nsftab);
            tbcTabs.SelectedTab = nsftab;
        }

        public void SaveNSF(bool ignore_warnings)
        {
            if (tbcTabs.SelectedTab != null)
            {
                string filename = tbcTabs.SelectedTab.Text;
                NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
                NSF nsf = nsfbox.NSF;
                SaveNSF(filename, nsf, ignore_warnings);
                switch (nsfbox.NSFController.GameVersion)
                {
                    case GameVersion.Crash1:
                        foreach (OldZoneEntry zone in nsf.GetEntries<OldZoneEntry>())
                        {
                            foreach (OldEntity entity in zone.Entities)
                            {
                                if (entity.ID >= 0x130)
                                {
                                    DarkMessageBox.ShowWarning(string.Format("An entity (ID {0}) exceeds maximum ID of 303.", entity.ID), "Entity ID Error");
                                }
                                else if (entity.ID <= 0)
                                {
                                    DarkMessageBox.ShowWarning(string.Format("An entity has invalid ID {0}.", entity.ID), "Entity ID Error");
                                }
                            }
                        }
                        break;
                    case GameVersion.Crash2:
                        foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
                        {
                            foreach (Entity entity in zone.Entities)
                            {
                                if ((entity.ID != null && entity.ID >= 0x400) || (entity.AlternateID != null && entity.AlternateID >= 0x400))
                                {
                                    if (entity.Name != null)
                                    {
                                        DarkMessageBox.ShowWarning(string.Format("Entity {0} (ID {1}) exceeds maximum ID of 1023.", entity.Name, entity.ID != null ? entity.ID : entity.AlternateID), "Entity ID Error");
                                    }
                                    else
                                    {
                                        DarkMessageBox.ShowWarning(string.Format("An entity (ID {0}) exceeds maximum ID of 1023.", entity.ID != null ? entity.ID : entity.AlternateID), "Entity ID Error");
                                    }
                                }
                                else if ((entity.ID != null && entity.ID <= 0) || (entity.AlternateID != null && entity.AlternateID <= 0))
                                {
                                    if (entity.Name != null)
                                    {
                                        DarkMessageBox.ShowWarning(string.Format("Entity {0} has invalid ID {1}.", entity.Name, entity.ID != null ? entity.ID : entity.AlternateID), "Entity ID Error");
                                    }
                                    else
                                    {
                                        DarkMessageBox.ShowWarning(string.Format("An entity has invalid ID {0}.", entity.ID != null ? entity.ID : entity.AlternateID), "Entity ID Error");
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }

        public void SaveNSF(string filename, NSF nsf, bool ignore_warnings)
        {
            try
            {
                byte[] nsfdata = nsf.Save();
                if (ignore_warnings || DarkMessageBox.ShowMessage(Resources.SaveNSF, Resources.Save_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
                {
                    File.WriteAllBytes(filename, nsfdata);
                }
            }
            catch (PackingException ex)
            {
                DarkMessageBox.ShowError(string.Format(Resources.SaveNSF_Error1, Entry.EIDToEName(ex.EID)), Resources.SaveNSF_Title);
            }
            catch (IOException ex)
            {
                DarkMessageBox.ShowError(Resources.SaveNSF_Error2 + ex.Message, Resources.SaveNSF_Title);
            }
            catch (UnauthorizedAccessException ex)
            {
                DarkMessageBox.ShowError(Resources.SaveNSF_Error3 + ex.Message, Resources.SaveNSF_Title);
            }
        }

        public void PatchNSD()
        {
            if (tbcTabs.SelectedTab != null)
            {
                string filename = tbcTabs.SelectedTab.Text;
                if (filename.EndsWith("F"))
                {
                    filename = filename.Remove(filename.Length - 1);
                    filename += "D";
                }
                else if (filename.EndsWith("f"))
                {
                    filename = filename.Remove(filename.Length - 1);
                    filename += "d";
                }
                else
                {
                    DarkMessageBox.ShowError(string.Format(Resources.PatchNSD_Error1, filename), Resources.PatchNSD_Title1);
                    return;
                }
                NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
                bool exists = true;
                if (!File.Exists(filename))
                {
                    DarkMessageBox.ShowError(string.Format(Resources.PatchNSD_Error3, filename), Resources.PatchNSD_Title1);
                    return;
                }
                PatchNSD(filename, exists, nsfbox.NSFController, false);
                nsfbox.Sync();
                OnResyncSuggested(EventArgs.Empty);
            }
        }

        public void PatchNSD(string filename, bool exists, NSFController nsfc, bool ignore_warnings, bool no_nsf_overwrite = false)
        {
            if (ignore_warnings ? true : DarkMessageBox.ShowMessage(Resources.PatchNSD1, Resources.Save_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                NSF nsf = nsfc.NSF;
                byte[] data = exists ? File.ReadAllBytes(filename) : null;
                try
                {
                    switch (nsfc.GameVersion)
                    {
                        case GameVersion.Crash1BetaMAR08:
                            {
                                ProtoNSD nsd = data != null ? ProtoNSD.Load(data) : new ProtoNSD(new int[256], 0, new NSDLink[0]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash1:
                            {
                                OldNSD nsd = data != null ? OldNSD.Load(data) : new OldNSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 1, 0x3F, Entry.NullEID, 0, 0, new int[64], new byte[0xFC]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash2:
                            {
                                NSD nsd = data != null ? NSD.Load(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[64], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash3:
                            {
                                NSD nsd = data != null ? NSD.LoadC3(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[128], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        default:
                            if (!ignore_warnings) DarkMessageBox.ShowWarning(Resources.PatchNSD_Error2, Resources.PatchNSD_Title1);
                            return;
                    }
                    bool order_updated = false;
                    // FIXME - reimplement below to not use controller tree, or better yet rework entire NSD patching system
                    order_updated = true;
#if false
                foreach (var ecc in nsfc.LegacySubcontrollers.OfType<EntryChunkController>()) // nsd patching might have moved entries, recreate moved entry chunks if that's the case
                {
                    for (int i = 0; i < ecc.LegacySubcontrollers.Count; i++)
                    {
                        var c = (EntryController)ecc.LegacySubcontrollers[i];
                        if (c.Entry != ecc.EntryChunk.Entries[i])
                        {
                            ecc.LegacySubcontrollers.Clear();
                            ecc.PopulateNodes();
                            order_updated = true;
                            break;
                        }
                    }
                }
#endif
                    if (!no_nsf_overwrite)
                    {
                        //if (ignore_warnings || Settings.Default.PatchNSDSavesNSF ? true : (order_updated && DarkMessageBox.ShowMessage(Resources.PatchNSD3, Resources.PatchNSD_Title1, DarkDialogButton.YesNo) == DialogResult.Yes))
                        //{
                        //    SaveNSF(true);
                        //}
                        if (ignore_warnings || Settings.Default.PatchNSDSavesNSF)
                            SaveNSF(true);
                    }
                }
                catch (LoadAbortedException)
                {
                }
            }

        }

        public void PatchNSD(NSD nsd, NSF nsf, string path, bool ignore_warnings)
        {
            if (!Settings.Default.UseOldPatchNSD)
            {
                nsd.ChunkCount = nsf.Chunks.Count;
                var indexdata = nsf.MakeNSDIndex();
                nsd.HashKeyMap = indexdata.Item1;
                nsd.Index = indexdata.Item2;
            }
            PatchNSDGoolMap(nsd.GOOLMap, nsf, ignore_warnings);

            // patch object entity count
            nsd.EntityCount = 0;
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
                foreach (Entity ent in zone.Entities)
                    if (ent.ID != null)
                        ++nsd.EntityCount;

            File.WriteAllBytes(path, nsd.Save());
            //if (!ignore_warnings && DarkMessageBox.ShowMessage(Resources.PatchNSD2, Resources.PatchNSD_Title2, DarkDialogButton.YesNo) == DialogResult.Yes)
            //{
            int[] eids = new int[nsd.Index.Count];
            for (int i = 0; i < eids.Length; ++i)
                eids[i] = nsd.Index[i].EntryID;
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.LoadListA != null)
                    {
                        foreach (EntityPropertyRow<int> row in ent.LoadListA.Rows)
                        {
                            List<int> values = (List<int>)row.Values;
                            values.Sort(delegate (int a, int b)
                            {
                                return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                            });
                            if (Settings.Default.DeleteInvalidEntries) values.RemoveAll(eid => nsf.GetEntry<IEntry>(eid) == null);
                        }
                    }
                    if (ent.LoadListB != null)
                    {
                        foreach (EntityPropertyRow<int> row in ent.LoadListB.Rows)
                        {
                            List<int> values = (List<int>)row.Values;
                            values.Sort(delegate (int a, int b)
                            {
                                return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                            });
                            if (Settings.Default.DeleteInvalidEntries) values.RemoveAll(eid => nsf.GetEntry<IEntry>(eid) == null);
                        }
                    }
                }
            }
            NotifyListUpdated();
            //}
        }

        public void PatchNSD(OldNSD nsd, NSF nsf, string path, bool ignore_warnings)
        {
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
            PatchNSDGoolMap(nsd.GOOLMap, nsf, ignore_warnings);
            if (ignore_warnings ? true : DarkMessageBox.ShowMessage(Resources.PatchNSD1, Resources.Save_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                File.WriteAllBytes(path, nsd.Save());
            }
        }

        public void PatchNSD(ProtoNSD nsd, NSF nsf, string path, bool ignore_warnings)
        {
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
            if (ignore_warnings ? true : DarkMessageBox.ShowMessage(Resources.PatchNSD1, Resources.Save_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                File.WriteAllBytes(path, nsd.Save());
            }
        }

        public void PatchNSDGoolMap(int[] map, NSF nsf, bool ignore_warnings)
        {
            for (int i = 0; i < map.Length; ++i)
            {
                map[i] = Entry.NullEID;
            }
            foreach (GOOLEntry gool in nsf.GetEntries<GOOLEntry>())
            {
                if (gool.Format == 1)
                {
                    int gool_id = BitConv.FromInt32(gool.Header, 0);
                    if (gool_id >= map.Length)
                    {
                        if (!ignore_warnings) DarkMessageBox.ShowWarning(string.Format("GOOL entry {0} has invalid object typeID {1} (cannot be larger than {2}).", gool.EName, gool_id, map.Length - 1), Resources.Save_ConfirmationPrompt);
                    }
                    else if (gool_id < 0)
                    {
                        if (!ignore_warnings) DarkMessageBox.ShowWarning(string.Format("GOOL entry {0} has invalid object typeID {1} (cannot be negative).", gool.EName, gool_id), Resources.Save_ConfirmationPrompt);
                    }
                    else
                    {
                        map[BitConv.FromInt32(gool.Header, 0)] = gool.EID;
                    }
                }
            }
        }

        public void CloseNSF()
        {
            string filename = tbcTabs.SelectedTab.Text;
            NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
            byte[] nsfdata;
            try
            {
                nsfdata = nsfbox.NSF.Save();
            }
            catch
            {
                nsfdata = null;
            }
            byte[] olddata = File.Exists(filename) ? File.ReadAllBytes(filename) : null;
            if ((olddata != null && (nsfdata == null || (nsfdata.Length == olddata.Length && nsfdata.SequenceEqual(olddata)))) || DarkMessageBox.ShowWarning(Resources.CloseNSF, Resources.Close_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                TabPage tab = tbcTabs.SelectedTab;
                if (tab != null)
                {
                    (tab.Tag as NSFBox)?.Kill();
                    tab.Tag = null;
                    tbcTabs.TabPages.Remove(tab);
                    tab.Dispose();
                }
            }
        }

        private void bgwMakeBIN_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            CDBuilder fs = (CDBuilder)args[0];
            string filename = (string)args[1];
            while (!dlgProgress.IsShown) ;
            using (FileStream output = new FileStream(filename, FileMode.Create, FileAccess.Write))
            using (Stream input = fs.Build())
            {
                ISO2PSX.Run(input, output, bgwMakeBIN);
            }
        }

        private void bgwMakeBIN_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dlgProgress.ProgressBar.Value = e.ProgressPercentage;
        }

        private void bgwMakeBIN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dlgProgress.Close();
        }

        internal void MakeBinWithProgressBar(CDBuilder fs, string filename)
        {
            using (dlgProgress = new ProgressBarForm())
            {
                dlgProgress.ProgressBar.Value = 0;
                dlgProgress.Text = Resources.MakeBIN_Making;
                bgwMakeBIN.RunWorkerAsync(new object[] { fs, filename });
                dlgProgress.ShowDialog(this);
            }
        }

        void tbbBIN_Click(object sender, EventArgs e)
        {
            if (formMakebin == null || formMakebin.IsDisposed)
                formMakebin = new MakeBin(this, true);

            if (!formMakebin.Visible)
                formMakebin.Show();
            else
                formMakebin.Activate();
        }

        void tbxDefaultVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultGameVersion = tbxDefaultVersion.SelectedIndex;
            Settings.Default.Save();
        }

        void tbxMakeBIN_Click(object sender, EventArgs e)
        {
            if (formMakebin == null || formMakebin.IsDisposed)
                formMakebin = new MakeBin(this, false);

            if (!formMakebin.Visible)
                formMakebin.Show();
            else
                formMakebin.Activate();
        }

        void tbxConvertVHVB_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] vh_data = FileUtil.OpenFile(FileFilters.VH, FileFilters.Any);
                if (vh_data == null) throw new LoadAbortedException();
                byte[] vb_data = FileUtil.OpenFile(FileFilters.VB, FileFilters.Any);
                if (vb_data == null) throw new LoadAbortedException();

                VH vh = VH.Load(vh_data);

                if (vb_data.Length / 16 != vh.VBSize)
                {
                    ErrorManager.SignalIgnorableError(Resources.ConvertVHVB_Error);
                }
                SampleLine[] vb = new SampleLine[vb_data.Length / 16];
                byte[] line_data = new byte[16];
                for (int i = 0; i < vb.Length; i++)
                {
                    Array.Copy(vb_data, i * 16, line_data, 0, 16);
                    vb[i] = SampleLine.Load(line_data);
                }

                VAB vab = VAB.Join(vh, vb);

                FileUtil.SaveFile(vab.ToDLS().Save(), FileFilters.DLS, FileFilters.Any);
            }
            catch (LoadAbortedException)
            {
            }
        }

        void tbxConvertVAB_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] vab_data = FileUtil.OpenFile(FileFilters.VAB, FileFilters.Any);

                if (vab_data == null) throw new LoadAbortedException();

                VH vh = VH.Load(vab_data);

                int vb_offset = 2592 + 32 * 16 * vh.Programs.Count;
                if ((vab_data.Length - vb_offset) % 16 != 0)
                {
                    ErrorManager.SignalIgnorableError(Resources.ConvertVAB_Error);
                }
                vh.VBSize = (vab_data.Length - vb_offset) / 16;
                SampleLine[] vb = new SampleLine[vh.VBSize];
                byte[] line_data = new byte[16];
                for (int i = 0; i < vb.Length; i++)
                {
                    Array.Copy(vab_data, vb_offset + i * 16, line_data, 0, 16);
                    vb[i] = SampleLine.Load(line_data);
                }

                VAB vab = VAB.Join(vh, vb);

                FileUtil.SaveFile(vab.ToDLS().Save(), FileFilters.DLS, FileFilters.Any);
            }
            catch (LoadAbortedException)
            {
            }
        }

        void tbxConvertAnimations_Click(object sender, EventArgs e)
        {
            if (formConvertAnimations != null)
            {
                formConvertAnimations.Focus();
                return;
            }
            formConvertAnimations = new ConvertAnimationsForm();
            formConvertAnimations.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                formConvertAnimations = null;
            };
            formConvertAnimations.Show();
        }

        void GetNSD(out string nsdFilename, out dynamic? nsd)
        {
            nsd = null;
            nsdFilename = string.Empty;
            if (tbcTabs.SelectedTab == null || !(tbcTabs.SelectedTab.Tag is NSFBox)) return;

            string nsfFilename = tbcTabs.SelectedTab.Text;
            nsdFilename = GetNSDFileName(nsfFilename);
            if (string.IsNullOrEmpty(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error2, nsfFilename), Resources.OldMainForm_tbxShowGOOLMap);
                return;
            }
            if (!File.Exists(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error3, nsdFilename), Resources.OldMainForm_tbxShowGOOLMap);
                return;
            }

            byte[] data = File.ReadAllBytes(nsdFilename);

            NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
            NSFController nsfc = nsfbox.NSFController;
            switch (nsfc.GameVersion)
            {
                case GameVersion.Crash1BetaMAR08:
                    nsd = data != null ? ProtoNSD.Load(data) : new ProtoNSD(new int[256], 0, new NSDLink[0]);
                    break;
                case GameVersion.Crash1:
                    nsd = data != null ? OldNSD.Load(data) : new OldNSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 1, 0x3F, Entry.NullEID, 0, 0, new int[64], new byte[0xFC]);
                    break;
                case GameVersion.Crash2:
                    nsd = data != null ? NSD.Load(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[64], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                    break;
                case GameVersion.Crash3:
                    nsd = data != null ? NSD.LoadC3(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[128], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                    break;
            }
        }

        string GetNSDFileName(string nsfFilename)
        {
            string nsdFilename = string.Empty;
            if (nsfFilename.EndsWith("F"))
            {
                nsdFilename = nsfFilename.Remove(nsfFilename.Length - 1);
                nsdFilename += "D";
            }
            else if (nsfFilename.EndsWith("f"))
            {
                nsdFilename = nsfFilename.Remove(nsfFilename.Length - 1);
                nsdFilename += "d";
            }
            return nsdFilename;
        }

        void tbxShowGOOLMap_Click(object sender, EventArgs e)
        {
            if (tbcTabs.SelectedTab == null) return;

            string nsfFilename = tbcTabs.SelectedTab.Text;
            GetNSD(out string nsdFilename, out dynamic? nsd);
            if (nsd == null) return;

            if (formShowGOOLMap.TryGetValue(nsdFilename, out DarkForm? value))
            {
                value.Focus();
                return;
            }
            DarkForm goolmap = new()
            {
                Text = $"GOOL Map ({nsdFilename})",
                BackColor = Color.FromArgb(31, 31, 32),
                MaximizeBox = false,
                MinimizeBox = false,
                Width = 584,
                Height = 380
            };
            ListView lst = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            List<string> BaseGOOL = new() { "WillC", "WarpC", "FruiC", "DispC", "DoctC", "PartC", "ShadC", "BoxsC", "WEfOC", "EntNC" };
            for (int i = 0; i < nsd.GOOLMap.Length; ++i)
            {
                ListViewItem lsi = new();
                lsi.Text = $"{i:D2}: {Entry.EIDToEName(nsd.GOOLMap[i])}";
                lsi.ForeColor = lsi.Text.Contains(Entry.NullEName) ? SystemColors.ControlDarkDark :
                                BaseGOOL.Any(item => lsi.Text.Contains(item)) ? Color.Turquoise :
                                Color.Gainsboro;
                lst.Items.Add(lsi);
            }
            goolmap.Controls.Add(lst);
            goolmap.FormClosing += (object sender, FormClosingEventArgs e) =>
            {
                formShowGOOLMap.Remove(nsdFilename);
            };
            formShowGOOLMap.Add(nsdFilename, goolmap);
            goolmap.Show();
        }

        void tbxGenerateSpawnPoint_Click(object sender, EventArgs e)
        {
            using (InputWindow inputWindow = new InputWindow("Enter entity ID:", Resources.GenerateSpawnPoint_Title, string.Empty))
            {
                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    string input = inputWindow.Input;
                    if (string.IsNullOrEmpty(input)) return;

                    if (int.TryParse(input, out int targetID))
                    {
                        NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
                        NSF nsf = nsfbox.NSF;
                        foreach (ZoneEntry entry in nsf.GetEntries<ZoneEntry>())
                        {
                            foreach (Entity entity in entry.Entities)
                            {
                                if (entity.ID == targetID)
                                {
                                    int zone = entry.EID;
                                    int cameraIdx = 0;
                                    string cameraIndex = string.Empty;
                                    if (entry.CameraCount > 3)
                                    {
                                        int cameraMaxIdx = (entry.CameraCount - 1) / 3;
                                        using (InputWindow inputWindows = new InputWindow($"Enter camera index [0-{cameraMaxIdx}]:", Resources.GenerateSpawnPoint_Title, "0"))
                                        {
                                            if (inputWindows.ShowDialog() == DialogResult.OK)
                                            {
                                                bool valid = false;
                                                if (int.TryParse(inputWindows.Input, out cameraIdx))
                                                {
                                                    if (cameraIdx >= 0 && cameraIdx <= cameraMaxIdx)
                                                    {
                                                        valid = true;
                                                        cameraIndex = $" [Camera: {cameraIdx}]";
                                                    }
                                                }

                                                if (!valid)
                                                {
                                                    DarkMessageBox.ShowError("Invalid camera index.", Resources.GenerateSpawnPoint_Title);
                                                    return;
                                                }
                                            }
                                            else return;
                                        }
                                    }
                                    int x = (entry.X + 4 * entity.Positions[0].X) << 8;
                                    int y = (entry.Y + 4 * entity.Positions[0].Y) << 8;
                                    int z = (entry.Z + 4 * entity.Positions[0].Z) << 8;
                                    byte[] data = new byte[24];
                                    BitConv.ToInt32(data, 0, zone);
                                    BitConv.ToInt32(data, 4, cameraIdx);
                                    BitConv.ToInt32(data, 8, 0);
                                    BitConv.ToInt32(data, 12, x);
                                    BitConv.ToInt32(data, 16, y);
                                    BitConv.ToInt32(data, 20, z);
                                    string result = BitConverter.ToString(data).Replace("-", "");
                                    Console.WriteLine($"[{Entry.EIDToEName(zone)}]{cameraIndex} [ID: {entity.ID}]\n{result}");
                                    Clipboard.SetText(result);
                                    Console.WriteLine("Copied to the clipboard.");
                                    DarkMessageBox.ShowInformation("Spawn point generated and output to the console.", Resources.GenerateSpawnPoint_Title);
                                    return;
                                }
                            }
                        }
                        DarkMessageBox.ShowError("Entity not found.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Invalid entity ID.", Resources.GenerateSpawnPoint_Title);
                        return;
                    }
                }
            }
        }

        public void ResetConfig()
        {
            TabPage configtab = tbcTabs.TabPages[0];
            if (configtab.Tag is ConfigEditor)
            {
                configtab.Controls.Clear();
                configtab.Tag = new ConfigEditor() { Dock = DockStyle.Fill };
                configtab.Controls.Add((ConfigEditor)configtab.Tag);
            }
        }

        private void OldMainForm_Load(object sender, EventArgs e)
        {
            Bounds = Settings.Default.FormBounds;
            WindowState = Settings.Default.FormWindowState;
        }

        private void OldMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                Settings.Default.FormBounds = Bounds;
            else
                Settings.Default.FormBounds = RestoreBounds;

            Settings.Default.FormWindowState = WindowState;

            Settings.Default.Save();
        }

        public static event EventHandler ListUpdated;

        public static void NotifyListUpdated()
        {
            ListUpdated?.Invoke(null, EventArgs.Empty);
        }
    }

}
