using AltUI.Controls;
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
        private ToolStripButton tbbUndock = new();
        private ToolStripButton tbbReload = new();
        private ToolStripButton tbbRebuild = new();
        private ToolStripButton tbbPlay = new();
        private ToolStripButton tbbBIN = new();
        private ToolStripButton tbbPAL = new();
        private ToolStripLabel tlbDefaultVersion = new();
        private ToolStripComboBox tbxDefaultVersion = new();
        private ToolStripMenuItem tbxMakeBIN = new();
        private ToolStripMenuItem tbxConvertVHVB = new();
        private ToolStripMenuItem tbxConvertVAB = new();
        private ToolStripMenuItem tbxModelConverter = new();
        private ToolStripMenuItem tbxEntryConverter = new();
        private ToolStripMenuItem tbxGenerateEID = new();
        private ToolStripMenuItem tbxVABTool = new();
        private ToolStripMenuItem tbxSEQTool = new();
        private ToolStripMenuItem tbbExtra = new();

        private TabControl tbcTabs;
        private GameVersionForm dlgGameVersion;
        private BackgroundWorker bgwMakeBIN;
        private ProgressBarForm dlgProgress;

        private ModelConverterForm? frmModelConverter;
        private EntryConverterForm? frmEntryConverter;
        private MakeBin? frmMakebin;
        private RebuildForm? frmRebuild;
        private VABTool? frmVABTool;
        private SEQTool? frmSEQTool;
        private DarkForm? frmGenerateEID;

        public static bool PAL { get; private set; } = Settings.Default.ModePAL;
        private const int RateNTSC = 30;
        private const int RatePAL = 25;

        enum ToolstripButtons : int
        {
            Open,
            Save,
            PatchNSD,
            Close,
            Separator1,
            PAL,
            Play,
            Separator2,
            BIN,
            Separator3,
            Undock,
            Reload,
        };

        public OldMainForm()
        {
            ToolStripButtonInit(tbbOpen, "FolderOpen", Resources.Toolbar_Open, $"{Resources.Toolbar_Open} (Ctrl + O)");
            tbbOpen.Click += new EventHandler(tbbOpen_Click);

            ToolStripButtonInit(tbbSave, "Floppy", Resources.Toolbar_Save, $"{Resources.Toolbar_Save} (Ctrl + S)");
            tbbSave.Click += new EventHandler(tbbSave_Click);

            ToolStripButtonInit(tbbPatchNSD, "Floppy2", Resources.Toolbar_Patch, $"{Resources.Toolbar_PatchNSD} (Ctrl + Shift + S)");
            tbbPatchNSD.Click += new EventHandler(tbbPatchNSD_Click);

            ToolStripButtonInit(tbbClose, "Folder", Resources.Toolbar_Close, $"{Resources.Toolbar_Close} (Ctrl + W)\nClose all (Ctrl + Shift + W)");
            tbbClose.Click += new EventHandler(tbbClose_Click);

            ToolStripButtonInit(tbbUndock, "Anchor", Resources.Toolbar_Undock, $"{Resources.Toolbar_Undock} (Ctrl + D)");
            tbbUndock.Click += new EventHandler(tbbUndock_Click);

            ToolStripButtonInit(tbbReload, "ArrowRefresh", Resources.Toolbar_Reload, $"{Resources.Toolbar_Reload} NSF");
            tbbReload.Click += new EventHandler(tbbReload_Click);

            ToolStripButtonInit(tbbRebuild, "Wrench", Resources.Toolbar_Rebuild, "(C2) Rebuild NSF using c2export");
            tbbRebuild.Click += new EventHandler(tbbRebuild_Click);

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

            tbxModelConverter.Text = Resources.OldMainForm_tbxModelConverter;
            tbxModelConverter.Click += new EventHandler(tbxModelConverter_Click);

            tbxEntryConverter.Text = Resources.OldMainForm_tbxEntryConverter;
            tbxEntryConverter.Click += new EventHandler(tbxEntryConverter_Click);

            tbxGenerateEID.Text = Resources.OldMainForm_tbxGenerateEID;
            tbxGenerateEID.Click += new EventHandler(tbxGenerateEID_Click);

            tbxVABTool.Text = Resources.OldMainForm_tbxVABTool;
            tbxVABTool.Click += new EventHandler(tbxVABTool_Click);

            tbxSEQTool.Text = "SEQ Tool";
            tbxSEQTool.Click += new EventHandler(tbxSEQTool_Click);

            tbbExtra.Text = Resources.OldMainForm_tbbExtra;
            tbbExtra.ToolTipText = "Extra features";
            tbbExtra.ImageKey = "Dropdown";
            tbbExtra.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tbbExtra.TextImageRelation = TextImageRelation.TextBeforeImage;
            tbbExtra.DropDown.Items.Add(tlbDefaultVersion);
            tbbExtra.DropDown.Items.Add(tbxDefaultVersion);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxMakeBIN);
            tbbExtra.DropDown.Items.Add("-");
            //tbbExtra.DropDown.Items.Add(tbxConvertVHVB);
            //tbbExtra.DropDown.Items.Add(tbxConvertVAB);
            //tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxModelConverter);
            tbbExtra.DropDown.Items.Add(tbxEntryConverter);
            tbbExtra.DropDown.Items.Add(tbxVABTool);
            tbbExtra.DropDown.Items.Add(tbxSEQTool);
            tbbExtra.DropDown.Items.Add("-");
            tbbExtra.DropDown.Items.Add(tbxGenerateEID);

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
            ToolStrip.Items.Insert(10, tbbUndock);
            ToolStrip.Items.Insert(11, tbbReload);
            ToolStrip.Items.Add(tbbRebuild);
            ToolStrip.Items.Add(tbbExtra);

            tbcTabs = TabControl;
            tbcTabs.SelectedIndexChanged += tbcTabs_SelectedIndexChanged;

            TabPage configtab = new TabPage("CrashEdit")
            {
                Tag = new ConfigEditor(this) { Dock = DockStyle.Fill }
            };
            configtab.Controls.Add((ConfigEditor)configtab.Tag);

            tbcTabs.TabPages.Add(configtab);

            UpdateToolbarButtonsVisibility();

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

            frmEntryConverter = null;
            frmModelConverter = null;
            frmMakebin = null;
            frmRebuild = null;
            frmVABTool = null;
            frmSEQTool = null;
            frmGenerateEID = null;

            Icon = OldResources.CBHacksIconAlt;
            // Width = Settings.Default.DefaultFormW;
            // Height = Settings.Default.DefaultFormH;
            Load += new EventHandler(OldMainForm_Load);
            FormClosing += new FormClosingEventHandler(OldMainForm_FormClosing);
            Text = $"CrashEdit: Re v{Assembly.GetExecutingAssembly().GetName().Version}";
            AllowDrop = true;
            DragEnter += OldMainForm_DragEnter;
            DragDrop += OldMainForm_DragDrop;

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

        public void UpdateToolbarButtonsVisibility()
        {
            tbbReload.Visible = Settings.Default.ShowRefreshButton;
            tbbUndock.Visible = Settings.Default.ShowUndockButton;
            tbbRebuild.Visible = Settings.Default.ShowRebuildUI;
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
                    ToolStrip.Items[(int)ToolstripButtons.Open].PerformClick();
                    break;
                // Save NSF  
                case (Keys.Control | Keys.S):
                    ToolStrip.Items[(int)ToolstripButtons.Save].PerformClick();
                    break;
                // Patch NSD  
                case (Keys.Control | Keys.Shift | Keys.S):
                    ToolStrip.Items[(int)ToolstripButtons.PatchNSD].PerformClick();
                    break;
                // Close NSF  
                case (Keys.Control | Keys.W):
                    ToolStrip.Items[(int)ToolstripButtons.Close].PerformClick();
                    break;
                // Play  
                case (Keys.F1):
                    ToolStrip.Items[(int)ToolstripButtons.Play].PerformClick();
                    break;
                // Close all  
                case (Keys.Control | Keys.Shift | Keys.W):
                    CloseAllNSF();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool TabIsNSF()
        {
            TabPage tab = tbcTabs.SelectedTab;
            return tab != null && tab.Tag is NSFBox;
        }

        private void tbcTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tab = tbcTabs.SelectedTab;
            tbbSave.Enabled =
            tbbPatchNSD.Enabled =
            tbbClose.Enabled =
            tbbUndock.Enabled =
            tbbReload.Enabled =
            // tbbRebuild.Enabled =
            tbbPlay.Enabled = TabIsNSF();
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
            if (!TabIsNSF())
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
            //bool temp_nsf_autosave_setting = Settings.Default.PatchNSDSavesNSF;
            //Settings.Default.PatchNSDSavesNSF = false;
            //PatchNSD(nsdFilename, true, nsfBox, nsfBox.NSFController, true, true);
            SaveNSF(nsfFilename, nsf, true);
            //Settings.Default.PatchNSDSavesNSF = temp_nsf_autosave_setting;
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

        void CloseAllNSF()
        {
            for (int i = tbcTabs.TabPages.Count - 1; i > 0; i--)
            {
                tbcTabs.SelectedIndex = i;
                CloseNSF();
            }
        }

        void tbbUndock_Click(object sender, EventArgs e)
        {
            var undock_command = new UndockCommand(this);
            undock_command.Execute();
        }
        void tbbReload_Click(object sender, EventArgs e)
        {
            bool canReopen = false;
            string filename = "";

            if (tbcTabs.SelectedTab != null)
            {
                filename = tbcTabs.SelectedTab.Text;
                canReopen = true;
            }

            bool did_close = CloseNSF();
            if (canReopen && did_close)
            {
                OpenNSF(filename);
            }
        }

        void tbbRebuild_Click(object sender, EventArgs e)
        {
            ShowRebuildForm();
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
        private void AddRecentNSF(string filename)
        {
            var recent = Settings.Default.RecentNSFFiles;
            if (recent.Contains(filename))
                recent.Remove(filename);
            recent.Insert(0, filename);
            while (recent.Count > 10)
                recent.RemoveAt(recent.Count - 1);
            Settings.Default.Save();
            ConfigEditor configEditor = (ConfigEditor)tbcTabs.TabPages[0].Tag;
            configEditor?.UpdateRecentNSFList();
        }

        public void OpenNSF(string filename)
        {
            // if multiopen is disallowed, switch to existing tab if the file is already open
            if (!Settings.Default.AllowMultiopenNSF)
            {                
                if (tbcTabs.TabPages.Cast<TabPage>().Any(tab => tab.Text == filename))
                {
                    tbcTabs.SelectedTab = tbcTabs.TabPages.Cast<TabPage>().First(tab => tab.Text == filename);
                    AddRecentNSF(filename);
                    return;
                }
            }

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
                    AddRecentNSF(filename);
                }
            }
            catch (LoadAbortedException)
            {
            }
        }

        public void OpenNSF(string filename, NSF nsf, GameVersion gameversion)
        {
            var ws = new LevelWorkspace();
            ws.FileName = filename;
            ws.NSF = nsf;
            GetNSD(filename, gameversion, out string nsdFilename, out dynamic? nsd);
            if (nsd is ProtoNSD)
            {
                ws.ProtoNSD = nsd;
            }
            else if (nsd is OldNSD)
            {
                ws.OldNSD = nsd;
            }
            else if (nsd is NSD)
            {
                ws.NSD = nsd;
            }
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

        public void PatchNSD(bool ignore_warnings = false)
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
                bool exists = true;
                if (!File.Exists(filename))
                {
                    DarkMessageBox.ShowError(string.Format(Resources.PatchNSD_Error3, filename), Resources.PatchNSD_Title1);
                    return;
                }
                NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
                PatchNSD(filename, exists, nsfbox, nsfbox.NSFController, ignore_warnings);
                nsfbox.Sync();
                OnResyncSuggested(EventArgs.Empty);
            }
        }

        public void PatchNSD(string filename, bool exists, NSFBox nsfbox, NSFController nsfc, bool ignore_warnings, bool no_nsf_overwrite = false)
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
                                //ProtoNSD nsd = data != null ? ProtoNSD.Load(data) : new ProtoNSD(new int[256], 0, new NSDLink[0]);
                                ProtoNSD nsd = nsfbox.ProtoNSD ?? new ProtoNSD(new int[256], 0, new NSDLink[0]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash1:
                            {
                                //OldNSD nsd = data != null ? OldNSD.Load(data) : new OldNSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 1, 0x3F, Entry.NullEID, 0, 0, new int[64], new byte[0xFC]);
                                OldNSD nsd = nsfbox.OldNSD ?? new OldNSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 1, 0x3F, Entry.NullEID, 0, 0, new int[64], new byte[0xFC]);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash2:
                            {
                                //NSD nsd = data != null ? NSD.Load(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[64], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                                NSD nsd = nsfbox.NSD ?? new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[64], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0], false);
                                PatchNSD(nsd, nsf, filename, ignore_warnings);
                            }
                            break;
                        case GameVersion.Crash3:
                            {
                                //NSD nsd = data != null ? NSD.LoadC3(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[128], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0]);
                                NSD nsd = nsfbox.NSD ?? new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[128], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0], true);
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
            nsd.ChunkCount = nsf.Chunks.Count;
            if (Settings.Default.UseOldPatchNSD)
            {
                Dictionary<int, int> newindex = new Dictionary<int, int>();
                List<int> _eids = new List<int>();
                for (int i = 0; i < nsf.Chunks.Count; i++)
                {
                    if (nsf.Chunks[i] is IEntry ientry)
                    {
                        newindex.Add(ientry.EID, i * 2 + 1);
                    }
                    if (nsf.Chunks[i] is EntryChunk chunk)
                    {
                        foreach (Entry entry in chunk.Entries)
                        {
                            newindex.Add(entry.EID, i * 2 + 1);
                        }
                    }
                }
                HashSet<NSDLink> unused = new HashSet<NSDLink>();
                foreach (NSDLink link in nsd.Index)
                {
                    _eids.Add(link.EntryID);
                    if (newindex.ContainsKey(link.EntryID))
                    {
                        link.ChunkID = newindex[link.EntryID];
                        newindex.Remove(link.EntryID);
                    }
                    else // NSD contains nonexistant entry
                    {
                        unused.Add(link);
                    }
                }
                if (unused.Count > 0)
                {
                    foreach (NSDLink link in unused)
                    {
                        nsd.Index.Remove(link);
                    }
                    for (int i = 0; i < 256; ++i)
                    {
                        nsd.HashKeyMap[i] = Math.Min(nsd.HashKeyMap[i], nsd.Index.Count - 1);
                    }
                }
                if (newindex.Count > 0)
                {
                    List<string> neweids = new List<string>();
                    foreach (KeyValuePair<int, int> kvp in newindex)
                    {
                        neweids.Add(Entry.EIDToEName(kvp.Key));
                    }
                    foreach (KeyValuePair<int, int> kvp in newindex)
                    {
                        nsd.Index.Add(new NSDLink(kvp.Value, kvp.Key));
                    }
                }

                // check list
                for (int i = 0; i < nsf.Chunks.Count; i++)
                {
                    if (nsf.Chunks[i] is EntryChunk chunk)
                    {
                        List<int> nsdchunkentries = new List<int>();
                        for (int j = 0; j < nsd.Index.Count; ++j)
                        {
                            NSDLink link = nsd.Index[j];
                            if (i * 2 + 1 == link.ChunkID)
                            {
                                nsdchunkentries.Add(j);
                            }
                        }
                        for (int j = 0; j < chunk.Entries.Count; ++j)
                        {
                            Entry entry = chunk.Entries[j];
                            if (entry.EID != nsd.Index[nsdchunkentries[j]].EntryID)
                            {
                                int k = j;
                                for (; k < nsdchunkentries.Count; ++k)
                                    if (entry.EID == nsd.Index[nsdchunkentries[k]].EntryID) break;
                                nsd.Index.Swap(nsdchunkentries[j], nsdchunkentries[k]);
                            }
                        }
                    }
                }
            }
            else
            {
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

        public bool CloseNSF(bool skip_dialog = false)
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
            if ((olddata != null && (nsfdata == null || (nsfdata.Length == olddata.Length && nsfdata.SequenceEqual(olddata)))) || skip_dialog || DarkMessageBox.ShowWarning(Resources.CloseNSF, Resources.Close_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                TabPage tab = tbcTabs.SelectedTab;
                if (tab != null)
                {
                    (tab.Tag as NSFBox)?.Kill();
                    tab.Tag = null;
                    tbcTabs.TabPages.Remove(tab);
                    tab.Dispose();
                    return true;
                }
            }
            return false;
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

        void tbxDefaultVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultGameVersion = tbxDefaultVersion.SelectedIndex;
            Settings.Default.Save();
        }

        void ShowMakeBinForm(bool autoMake)
        {
            if (frmMakebin == null || frmMakebin.IsDisposed)
                frmMakebin = new MakeBin(this, autoMake);

            if (!frmMakebin.Visible)
                frmMakebin.Show();
            else
                frmMakebin.Activate();
        }

        void ShowRebuildForm()
        {
            if (frmRebuild?.IsDisposed == true)
            {
                frmRebuild = null;
            }
            
            if (frmRebuild == null)
            {
                frmRebuild = new RebuildForm(this);
                frmRebuild.FormClosed += (s, e) => {
                    frmRebuild?.Dispose();
                    frmRebuild = null;
                };
            }

            if (!frmRebuild.Visible)
                frmRebuild.Show(this);

            if (frmRebuild.WindowState == FormWindowState.Minimized)
                frmRebuild.WindowState = FormWindowState.Normal;            

            frmRebuild.Focus();
            frmRebuild.BringToFront();
            frmRebuild.Activate();
        }

        void tbbBIN_Click(object sender, EventArgs e)
        {
            ShowMakeBinForm(true);
        }

        void tbxMakeBIN_Click(object sender, EventArgs e)
        {
            ShowMakeBinForm(false);
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

        void tbxModelConverter_Click(object sender, EventArgs e)
        {
            if (frmModelConverter != null)
            {
                frmModelConverter.Focus();
                return;
            }
            frmModelConverter = new ModelConverterForm();
            frmModelConverter.FormClosing += (object? sender, FormClosingEventArgs e) =>
            {
                frmModelConverter = null;
            };
            frmModelConverter.Show();
        }

        void tbxEntryConverter_Click(object sender, EventArgs e)
        {
            if (frmEntryConverter != null)
            {
                frmEntryConverter.Focus();
                return;
            }
            frmEntryConverter = new EntryConverterForm();
            frmEntryConverter.FormClosing += (object? sender, FormClosingEventArgs e) =>
            {
                frmEntryConverter = null;
            };
            frmEntryConverter.Show();
        }

        void tbxGenerateEID_Click(object sender, EventArgs e)
        {
            if (frmGenerateEID != null)
            {
                frmGenerateEID.Focus();
                return;
            }
            frmGenerateEID = new DarkForm()
            {
                Text = "Generate EID",
                Icon = Embeds.GetIcon("Calculator"),
                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(260, 190),
                MinimizeBox = false,
                MaximizeBox = false,
                TopMost = true
            };

            FlowLayoutPanel flp = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(9, 3, 3, 3),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            Label lbText = new Label()
            {
                Text = "Enter entry name:",
                Margin = new Padding(3, 3, 3, 0),
            };
            DarkTextBox txtEName = new DarkTextBox()
            {
                MaxLength = 5,
                Width = 80,
                Margin = new Padding(3, 0, 3, 3),
            };
            DarkTextBox txtEID = new DarkTextBox()
            {
                ReadOnly = true,
                Width = 80,
                Margin = new Padding(3, 15, 3, 3),
            };
            DarkButton cmdCopy = new DarkButton()
            {
                Text = "Copy"
            };
            Label lbEIDError = new Label()
            {
                AutoSize = true,
                ForeColor = Color.Red
            };

            txtEName.TextChanged += (sender, e) =>
            {
                lbEIDError.Text = Entry.CheckEIDErrors(txtEName.Text, true);
                if (lbEIDError.Text == string.Empty)
                {
                    int chunk = Entry.ENameToEID(txtEName.Text);
                    int temp = 0;
                    List<byte> eid = new List<byte>();
                    for (int i = 0; i < 8; i++)
                    {
                        if (i % 2 == 0)
                        {
                            temp = chunk & 0xF;
                            chunk >>= 4;
                            eid.Add((byte)(chunk & 0xF));
                        }
                        else
                        {
                            eid.Add((byte)temp);
                            chunk >>= 4;
                        }
                    }
                    txtEID.Text = string.Join("", eid.Select(b => b.ToString("X")));
                }
                else
                {
                    txtEID.Text = string.Empty;
                }
            };
            cmdCopy.Click += (sender, e) =>
            {
                Clipboard.SetDataObject(txtEID.Text, true, 10, 100);
            };

            flp.Controls.Add(lbText);
            flp.Controls.Add(txtEName);
            flp.Controls.Add(lbEIDError);
            flp.Controls.Add(txtEID);
            flp.Controls.Add(cmdCopy);
            frmGenerateEID.Controls.Add(flp);

            frmGenerateEID.FormClosing += (object? sender, FormClosingEventArgs e) =>
            {
                frmGenerateEID = null;
            };
            frmGenerateEID.Show();
        }

        void tbxVABTool_Click(object sender, EventArgs e)
        {
            if (frmVABTool == null || frmVABTool.IsDisposed)
                frmVABTool = new();

            if (!frmVABTool.Visible)
                frmVABTool.Show();
            else
                frmVABTool.Activate();
        }

        void tbxSEQTool_Click(object sender, EventArgs e)
        {
            if (frmSEQTool == null || frmSEQTool.IsDisposed)
                frmSEQTool = new(null);

            if (!frmSEQTool.Visible)
                frmSEQTool.Show();
            else
                frmSEQTool.Activate();
        }

        void GetNSD(string filename, GameVersion gameversion, out string nsdFilename, out dynamic? nsd)
        {
            nsd = null;
            nsdFilename = string.Empty;

            nsdFilename = GetNSDFileName(filename);
            if (string.IsNullOrEmpty(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error2, filename), "NSD");
                return;
            }
            if (!File.Exists(nsdFilename))
            {
                DarkMessageBox.ShowError(string.Format(Resources.Playtest_Error3, nsdFilename), "NSD");
                return;
            }

            byte[] data = File.ReadAllBytes(nsdFilename);

            switch (gameversion)
            {
                case GameVersion.Crash1BetaMAR08:
                    nsd = data != null ? ProtoNSD.Load(data) : new ProtoNSD(new int[256], 0, new NSDLink[0]);
                    break;
                case GameVersion.Crash1:
                    nsd = data != null ? OldNSD.Load(data) : new OldNSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 1, 0x3F, Entry.NullEID, 0, 0, new int[64], new byte[0xFC]);
                    break;
                case GameVersion.Crash2:
                    nsd = data != null ? NSD.Load(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[64], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0], false);
                    break;
                case GameVersion.Crash3:
                    nsd = data != null ? NSD.LoadC3(data) : new NSD(new int[256], 0, new int[4], 0, 0, new int[64], new NSDLink[0], 0, 0x3F, 0, new int[128], new byte[0xFC], new NSDSpawnPoint[1] { new NSDSpawnPoint(Entry.NullEID, 0, 0, 0, 0, 0) }, new byte[0], true);
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

        public void ResetConfig()
        {
            TabPage configtab = tbcTabs.TabPages[0];
            if (configtab.Tag is ConfigEditor)
            {
                configtab.Controls.Clear();
                configtab.Tag = new ConfigEditor(this) { Dock = DockStyle.Fill };
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

        private void OldMainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                HashSet<string> allowed = TabIsNSF() ?
                    new(StringComparer.OrdinalIgnoreCase)
                    {
                        ".nsf",
                        ".nschunk",
                        ".nsentry"
                    } :
                    new(StringComparer.OrdinalIgnoreCase)
                    {
                        ".nsf"
                    };

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Any(file => allowed.Contains(Path.GetExtension(file))))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OldMainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                List<string> nsfFiles = [];
                List<string> chunkFiles = [];
                List<string> entryFiles = [];

                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file);

                    if (ext.Equals(".nsf", StringComparison.OrdinalIgnoreCase))
                        nsfFiles.Add(file);
                    else if (ext.Equals(".nschunk", StringComparison.OrdinalIgnoreCase))
                        chunkFiles.Add(file);
                    else if (ext.Equals(".nsentry", StringComparison.OrdinalIgnoreCase))
                        entryFiles.Add(file);
                }

                if (TabIsNSF())
                {
                    NSFBox nsfbox = (NSFBox)tbcTabs.SelectedTab.Tag;
                    NSFController nsfc = nsfbox.NSFController;

                    if (chunkFiles.Count > 0)
                        nsfc.ImportAndReplaceChunk(ReadBytes(chunkFiles.ToArray()));

                    if (entryFiles.Count > 0)
                        nsfc.ImportAndReplaceEntry(ReadBytes(entryFiles.ToArray()));

                    nsfbox.Sync();
                }

                foreach (string nsf in nsfFiles)
                {
                    OpenNSF(nsf);
                }

                BringToFront();
                Activate();
            }
        }

        private static byte[][] ReadBytes(string[] files)
        {
            byte[][] result = new byte[files.Length][];
            for (int i = 0; i < files.Length; i++)
            {
                result[i] = File.ReadAllBytes(files[i]);
            }
            return result;
        }
    }
}
