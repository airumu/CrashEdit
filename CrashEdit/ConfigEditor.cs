using AltUI.Forms;
using CrashEdit.CE.Properties;
using Settings = CrashEdit.CE.Properties.Settings;

namespace CrashEdit.CE
{
    public partial class ConfigEditor : UserControl
    {
        public static readonly List<string> Languages = new() { "en", "ja" };

        private static readonly List<string> FontFileNames = new();
        private static readonly List<string> FontExtensions = new() { ".ttf", ".otf" };

        public HelpWindow? frmhelp = null;
        private OldMainForm owner = null;
        private System.Windows.Forms.Timer recentNSFTimer;

        private void MakeFontsList()
        {
            var add_font = (string f) =>
            {
                if (FontExtensions.Contains(Path.GetExtension(f).ToLower()))
                {
                    var shortname = Path.GetFileName(f);
                    if (!dpdFont.Items.Contains(shortname))
                    {
                        dpdFont.Items.Add(shortname);
                        FontFileNames.Add(f);
                    }
                }
            };

            dpdFont.Items.Clear();
            FontFileNames.Clear();

            foreach (var f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Fonts)))
            {
                add_font(f);
            }
            try
            {
                foreach (var f in Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows", "Fonts")))
                {
                    add_font(f);
                }
            }
            catch (Exception ex) when (
                ex is DirectoryNotFoundException
                )
            {
            }
        }

        public void UpdateRecentNSFList()
        {
            lstRecentNSF.Items.Clear();
            foreach (string file in Settings.Default.RecentNSFFiles)
                lstRecentNSF.Items.Add(file);
        }

        public ConfigEditor(OldMainForm ow)
        {
            owner = ow;
            InitializeComponent();
            UpdateRecentNSFList();
            recentNSFTimer = new System.Windows.Forms.Timer();
            recentNSFTimer.Interval = 5000;
            recentNSFTimer.Tick += (s, e) => RecentNSFTimer_Tick();
            recentNSFTimer.Start();

            // note: if language data is not found, this will just grab the english name. TODO fix this
            foreach (string lang in Languages)
            {
                string name = Resources.ResourceManager.GetString("Language", new System.Globalization.CultureInfo(lang)) ?? "N/A";
                dpdLang.Items.Add($"{name} ({lang})");
                if (lang == Settings.Default.Language)
                    dpdLang.SelectedIndex = dpdLang.Items.Count - 1;
            }
            dpdLang.SelectedItem = Resources.ResourceManager.GetString("Language", new System.Globalization.CultureInfo(Settings.Default.Language));
            dpdLang.SelectedIndexChanged += new EventHandler(dpdLang_SelectedIndexChanged);
            MakeFontsList();
            dpdFont.SelectedIndexChanged += new EventHandler(dpdFont_SelectedIndexChanged);
            if (dpdFont.Items.Contains(Settings.Default.FontName))
                dpdFont.SelectedItem = Settings.Default.FontName;
            else if (FontFileNames.Contains(Settings.Default.FontName))
                dpdFont.SelectedIndex = FontFileNames.IndexOf(Settings.Default.FontName);
            else
                dpdFont.SelectedIndex = 0;

            dpdHexView.Items.AddRange(new object[] { "Small", "Medium", "Large" });
            dpdHexView.SelectedItem = Settings.Default.HexViewCellSize;
            dpdHexView.SelectedIndexChanged += new EventHandler(dpdHexView_SelectedIndexChanged);

            numFontSize.Value = (decimal)Settings.Default.FontSize;
            numW.Value = Settings.Default.DefaultFormW;
            numH.Value = Settings.Default.DefaultFormH;
            numAnimGrid.Value = Settings.Default.AnimGridLen;
            sldNodeShadeAmt.Value = (int)(Settings.Default.NodeShadeMax * 100);
            cdlClearCol.Color = picClearCol.BackColor = Color.FromArgb(Settings.Default.ClearColorRGB);
            cdlClearCol.Color = picClearCol.BackColor = Color.FromArgb(Settings.Default.ClearColorRGB);

            // chk.Checked
            chkNormalDisplay.Checked = Settings.Default.DisplayNormals;
            chkCollisionDisplay.Checked = Settings.Default.DisplayFrameCollision;
            chkDeleteInvalidEntries.Checked = Settings.Default.DeleteInvalidEntries;
            chkAnimGrid.Checked = Settings.Default.DisplayAnimGrid;
            chkFont3DEnable.Checked = Settings.Default.Font3DEnable;
            chkFont2DEnable.Checked = Settings.Default.Font2DEnable;
            chkViewerShowHelp.Checked = Settings.Default.ViewerShowHelp;
            chkViewZoneBox.Checked = Settings.Default.ViewZoneBox;
            chkViewZoneName.Checked = Settings.Default.ViewZoneName;
            chkViewCamera.Checked = Settings.Default.ViewCamera;
            chkViewCameraAngle.Checked = Settings.Default.ViewCameraAngle;
            chkShowEntityParams.Checked = Settings.Default.ShowEntityParams;
            chkPatchNSDSavesNSF.Checked = Settings.Default.PatchNSDSavesNSF;

            chkLagacyPatchNSD.Checked = Settings.Default.UseOldPatchNSD;
            chkLiteralCollisionTypes.Checked = Settings.Default.ShowliteralCollisionTypes;
            chkEnableCustomCrates.Checked = Settings.Default.EnableCustomCrates;
            chkEnableC2TT.Checked = Settings.Default.EnableC2TTEditor;
            chkPatchGOOLC3toC2.Checked = Settings.Default.PatchGOOLC3toC2;
            chkSplitViewerPanels.Checked = Settings.Default.SplitAnimViewerPanels;
            chkEnableLegacyEntityBox.Checked = Settings.Default.EnableLegacyEntityBox;
            chkOutputCopyTextureResult.Checked = Settings.Default.OutputCopyTextureResult;
            chkOutputModelTextureInfo.Checked = Settings.Default.OutputModelTextureInfo;
            chkOutputCLUTInfo.Checked = Settings.Default.OutputCLUTInfo;
            chkApplyMica.Checked = Settings.Default.ApplyMica;
            chkIgnoreDuplicatedEntryError.Checked = Settings.Default.IgnoreDuplicatedEntryError;
            chkShowRenderingErrors.Checked = Settings.Default.ShowRenderingErrors;
            chkShowUndockButton.Checked = Settings.Default.ShowUndockButton;
            chkShowRefresh.Checked = Settings.Default.ShowRefreshButton;
            chkShowRebuild.Checked = Settings.Default.ShowRebuildUI;
            chkAllowMultiopenNSF.Checked = Settings.Default.AllowMultiopenNSF;

            // chk.Enabled
            chkViewCameraAngle.Enabled = chkViewCamera.Checked;

            // chk.Text
            fraSize.Text = Resources.Config_fraSize;
            fraClearCol.Text = Resources.Config_fraClearCol;
            fraFont.Text = Resources.Config_fraFont;
            fraNodeShadeAmt.Text = Resources.Config_fraNodeShadeAmt;
            fraLang.Text = Resources.Config_lblLang;
            lblAnimGrid.Text = Resources.Config_lblAnimGrid;
            lblFontName.Text = Resources.Config_lblFontName;
            lblFontSize.Text = Resources.Config_lblFontSize;
            fraAnimGrid.Text = Resources.Config_fraAnimGrid;
            chkAnimGrid.Text = Resources.Config_chkAnimGrid;
            lblAnimGrid.Text = Resources.Config_lblAnimGrid;
            chkNormalDisplay.Text = Resources.Config_chkNormalDisplay;
            chkCollisionDisplay.Text = Resources.Config_chkCollisionDisplay;
            chkDeleteInvalidEntries.Text = Resources.Config_chkDeleteInvalidEntries;
            chkPatchNSDSavesNSF.Text = Resources.Config_chkPatchNSDSavesNSF;
            chkFont3DEnable.Text = Resources.Config_chkFont3DEnable;
            chkFont2DEnable.Text = Resources.Config_chkFont2DEnable;
            chkViewerShowHelp.Text = Resources.Config_chkViewerShowHelp;
            chkViewZoneBox.Text = Resources.Config_chkViewZoneBox;
            chkViewZoneName.Text = Resources.Config_chkViewZoneName;
            chkViewCamera.Text = Resources.Config_chkViewCamera;
            chkViewCameraAngle.Text = Resources.Config_chkViewCameraAngle;
            chkShowEntityParams.Text = Resources.Config_chkShowEntityParams;
            lblNodeShadeAmt.Text = string.Format("{0:F0}%", sldNodeShadeAmt.Value);
            cmdReset.Text = Resources.Config_cmdReset;

            chkLagacyPatchNSD.Text = Resources.Config_chkLegacyPatchNSD;
            chkLiteralCollisionTypes.Text = Resources.Config_chkLiteralCollisionTypes;
            chkEnableCustomCrates.Text = Resources.Config_chkEnableCustomCrates;
            chkEnableC2TT.Text = Resources.Config_chkEnableC2TT;
            chkPatchGOOLC3toC2.Text = Resources.Config_chkPatchGOOLC3toC2;
            chkSplitViewerPanels.Text = Resources.Config_chkSplitViewerPanels;
            chkEnableLegacyEntityBox.Text = Resources.Config_chkEnableLegacyEntityBox;
            chkOutputCopyTextureResult.Text = Resources.Config_chkOutputCopyTextureResult;
            chkOutputModelTextureInfo.Text = Resources.Config_chkOutputModelTextureInfo;
            chkOutputCLUTInfo.Text = Resources.Config_chkOutputCLUTInfo;
            chkApplyMica.Text = Resources.Config_chkApplyMica;
            chkIgnoreDuplicatedEntryError.Text = Resources.Config_chkIgnoreDuplicatedEntryError;
            chkShowRenderingErrors.Text = Resources.Config_chkShowRenderingErrors;
        }

        private void AskRestartProgram()
        {
            if (DarkMessageBox.ShowInformation(Resources.Restart, Resources.Restart_ConfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            if (frmhelp == null || frmhelp.IsDisposed)
            {
                frmhelp = new HelpWindow();
                frmhelp.FormClosing += (object? sender, FormClosingEventArgs e) =>
                {
                    frmhelp = null;
                };
            }
            if (!frmhelp.Visible)
            {
                frmhelp.Show();
            }
            else
            {
                frmhelp.Activate();
            }
        }

        private void dpdLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Language = Languages[dpdLang.SelectedIndex];
            Settings.Default.Save();
            AskRestartProgram();
        }

        private void dpdFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.FontName = FontFileNames[dpdFont.SelectedIndex];
            Settings.Default.Save();
        }

        private void dpdHexView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.HexViewCellSize = Convert.ToString(dpdHexView.SelectedItem);
            Settings.Default.Save();
            AskRestartProgram();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            if (DarkMessageBox.ShowWarning("Are you sure you want to reset the settings?", Resources.Reset_onfirmationPrompt, DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                Settings.Default.Reset();
                ((OldMainForm)TopLevelControl).ResetConfig();
            }
        }

        private void numW_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultFormW = (int)numW.Value;
            Settings.Default.Save();
        }

        private void numH_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.DefaultFormH = (int)numH.Value;
            Settings.Default.Save();
        }

        private void chkNormalDisplay_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DisplayNormals = chkNormalDisplay.Checked;
            Settings.Default.Save();
        }

        private void chkCollisionDisplay_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DisplayFrameCollision = chkCollisionDisplay.Checked;
            Settings.Default.Save();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (cdlClearCol.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ClearColorRGB = cdlClearCol.Color.ToArgb();
                picClearCol.BackColor = System.Drawing.Color.FromArgb(Settings.Default.ClearColorRGB);
                Settings.Default.Save();
            }
        }

        private void chkDeleteInvalidEntries_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DeleteInvalidEntries = chkDeleteInvalidEntries.Checked;
            Settings.Default.Save();
        }

        private void chkAnimGrid_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DisplayAnimGrid = chkAnimGrid.Checked;
            Settings.Default.Save();
        }

        private void numAnimGrid_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.AnimGridLen = (int)numAnimGrid.Value;
            Settings.Default.Save();
        }

        private void chkPatchNSDSavesNSF_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.PatchNSDSavesNSF = chkPatchNSDSavesNSF.Checked;
            Settings.Default.Save();
        }
        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.FontSize = (float)numFontSize.Value;
            Settings.Default.Save();
        }

        private void chkFont3DEnable_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Font3DEnable = chkFont3DEnable.Checked;
            Settings.Default.Save();
        }

        private void chkFont2DEnable_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Font2DEnable = chkFont2DEnable.Checked;
            Settings.Default.Save();
        }

        private void chkViewerShowHelp_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewerShowHelp = chkViewerShowHelp.Checked;
            Settings.Default.Save();
        }

        //private void sldNodeShadeAmt_Scroll(object sender, EventArgs e)
        //{
        //    Settings.Default.NodeShadeMax = sldNodeShadeAmt.Value / 100f;
        //    lblNodeShadeAmt.Text = string.Format("{0:F0}%", sldNodeShadeAmt.Value);
        //    Settings.Default.Save();
        //}

        private void sldNodeShadeAmt_ValueChangedl(object sender, EventArgs e)
        {
            Settings.Default.NodeShadeMax = sldNodeShadeAmt.Value / 100f;
            lblNodeShadeAmt.Text = string.Format("{0:F0}%", sldNodeShadeAmt.Value);
            Settings.Default.Save();
        }


        private void chkViewZoneBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewZoneBox = chkViewZoneBox.Checked;
            Settings.Default.Save();
        }

        private void chkViewZoneName_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewZoneName = chkViewZoneName.Checked;
            Settings.Default.Save();
        }

        private void chkViewCamera_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewCamera = chkViewCamera.Checked;
            Settings.Default.Save();
            chkViewCameraAngle.Enabled = chkViewCamera.Checked;
        }

        private void chkViewCameraAngle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ViewCameraAngle = chkViewCameraAngle.Checked;
            Settings.Default.Save();
        }

        private void chkShowEntityParams_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowEntityParams = chkShowEntityParams.Checked;
            Settings.Default.Save();
        }

        private void chkOldPatchNSD_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UseOldPatchNSD = chkLagacyPatchNSD.Checked;
            Settings.Default.Save();
        }

        private void chkDetailedCollision_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowliteralCollisionTypes = chkLiteralCollisionTypes.Checked;
            Settings.Default.Save();
        }

        private void chkShowCustomCrates_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableCustomCrates = chkEnableCustomCrates.Checked;
            Settings.Default.Save();
        }

        private void chkShowRefresh_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowRefreshButton = chkShowRefresh.Checked;
            Settings.Default.Save();
            ((OldMainForm)TopLevelControl)?.UpdateToolbarButtonsVisibility();
        }

        private void chkEnableC2Rebuild_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowRebuildUI = chkShowRebuild.Checked;
            Settings.Default.Save();
            ((OldMainForm)TopLevelControl)?.UpdateToolbarButtonsVisibility();
        }

        private void chkShowUndockButton_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowUndockButton = chkShowUndockButton.Checked;
            Settings.Default.Save();
            ((OldMainForm)TopLevelControl)?.UpdateToolbarButtonsVisibility();
        }

        private void chkEnableC2TT_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableC2TTEditor = chkEnableC2TT.Checked;
            Settings.Default.Save();
        }

        private void chkPatchGOOLC3toC2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.PatchGOOLC3toC2 = chkPatchGOOLC3toC2.Checked;
            Settings.Default.Save();
        }

        private void chkPatchGOOLC3toC2_Click(object sender, EventArgs e)
        {
            Settings.Default.PatchGOOLC3toC2 = chkPatchGOOLC3toC2.Checked;
            Settings.Default.Save();
        }

        private void chkSplitViewerPanels_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SplitAnimViewerPanels = chkSplitViewerPanels.Checked;
            Settings.Default.Save();
        }

        private void chkEnableLegacyEntityBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableLegacyEntityBox = chkEnableLegacyEntityBox.Checked;
            Settings.Default.Save();
        }

        private void chkOutputCopyTextureResult_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OutputCopyTextureResult = chkOutputCopyTextureResult.Checked;
            Settings.Default.Save();
        }

        private void chkOutputModelTextureInfo_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OutputModelTextureInfo = chkOutputModelTextureInfo.Checked;
            Settings.Default.Save();
        }

        private void chkApplyMica_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ApplyMica = chkApplyMica.Checked;
            Settings.Default.Save();
        }

        private void chkApplyMica_Click(object sender, EventArgs e)
        {
            Settings.Default.ApplyMica = chkApplyMica.Checked;
            Settings.Default.Save();
            AskRestartProgram();
        }

        private void chkOutputCLUTInfo_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OutputCLUTInfo = chkOutputCLUTInfo.Checked;
            Settings.Default.Save();
        }

        private void chkIgnoreDuplicatedEntryError_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IgnoreDuplicatedEntryError = chkIgnoreDuplicatedEntryError.Checked;
            Settings.Default.Save();
        }

        private void chkShowRenderingErrors_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowRenderingErrors = chkShowRenderingErrors.Checked;
            Settings.Default.Save();
        }

        private void lstRecentNSF_Click(object sender, EventArgs e)
        {
            if (lstRecentNSF.SelectedItem is string filename && File.Exists(filename))
            {
                ((OldMainForm)owner).OpenNSF(filename);
            }
        }

        private void cmdClearRecentFiles_Click(object sender, EventArgs e)
        {
            Settings.Default.RecentNSFFiles.Clear();
            Settings.Default.Save();
            lstRecentNSF.Items.Clear();
        }

        private void lstRecentNSF_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstRecentNSF.Items.Count)
                return;

            string? path = lstRecentNSF.Items[e.Index] as string;
            bool exists = path != null && File.Exists(path);

            // Determine colors and font
            Color foreColor;
            Color backColor;
            Font font = e.Font;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                backColor = SystemColors.Highlight;
                if (exists)
                {
                    foreColor = Color.White;
                    font = new Font(e.Font, FontStyle.Underline);
                }
                else
                {
                    foreColor = SystemColors.GrayText;
                }
            }
            else
            {
                backColor = lstRecentNSF.BackColor;
                if (exists)
                {
                    foreColor = Color.DeepSkyBlue;
                    font = new Font(e.Font, FontStyle.Underline);
                }
                else
                {
                    foreColor = SystemColors.GrayText;
                }
            }

            using (SolidBrush backgroundBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }
            using (Brush brush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(
                    path ?? string.Empty,
                    font,
                    brush,
                    e.Bounds
                );
            }
            e.DrawFocusRectangle();

            // Dispose the custom font if created
            if (!ReferenceEquals(font, e.Font))
                font.Dispose();
        }

        private void RecentNSFTimer_Tick()
        {
            lstRecentNSF.Refresh();
        }

        private void chkAllowMultiopenNSF_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AllowMultiopenNSF = chkAllowMultiopenNSF.Checked;
            Settings.Default.Save();
        }

    }
}
