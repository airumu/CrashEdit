using CrashEdit.Forms;
using CrashEdit.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public partial class ConfigEditor : UserControl
    {
        public static readonly List<string> Languages = new List<string> { "en", "ja", "pt" };

        public HelpWindow frmhelp = null;

        public ConfigEditor()
        {
            InitializeComponent();
            foreach (string lang in Languages)
                dpdLang.Items.Add(Resources.ResourceManager.GetString("Language", new System.Globalization.CultureInfo(lang)));
            dpdLang.SelectedItem = Resources.ResourceManager.GetString("Language", new System.Globalization.CultureInfo(Settings.Default.Language));
            dpdLang.SelectedIndexChanged += new EventHandler(dpdLang_SelectedIndexChanged);
            numW.Value = Settings.Default.DefaultFormW;
            numH.Value = Settings.Default.DefaultFormH;
            chkNormalDisplay.Checked = Settings.Default.DisplayNormals;
            chkCollisionDisplay.Checked = Settings.Default.DisplayFrameCollision;
            chkUseAnimLinks.Checked = Settings.Default.UseAnimLinks;
            cdlClearCol.Color = picClearCol.BackColor = System.Drawing.Color.FromArgb(Settings.Default.ClearColorRGB);
            chkDeleteInvalidEntries.Checked = Settings.Default.DeleteInvalidEntries;
            chkAnimGrid.Checked = Settings.Default.DisplayAnimGrid;
            numAnimGrid.Value = Settings.Default.AnimGridLen;
            fraLang.Text = Resources.Config_FraLang;
            fraSize.Text = Resources.Config_fraSize;
            lblW.Text = Resources.Config_lblW;
            lblH.Text = Resources.Config_lblH;
            fraClearCol.Text = Resources.Config_fraClearCol;
            fraAnimGrid.Text = Resources.Config_fraAnimGrid;
            chkAnimGrid.Text = Resources.Config_chkAnimGrid;
            lblAnimGrid.Text = Resources.Config_lblAnimGrid;
            chkNormalDisplay.Text = Resources.Config_chkNormalDisplay;
            chkCollisionDisplay.Text = Resources.Config_chkCollisionDisplay;
            chkDeleteInvalidEntries.Text = Resources.Config_chkDeleteInvalidEntries;
            chkUseAnimLinks.Text = Resources.Config_chkUseAnimLinks;
            chkPatchNSDSavesNSF.Text = Resources.Config_chkPatchNSDSavesNSF;
            chkPatchNSDSavesNSF.Checked = Settings.Default.PatchNSDSavesNSF;
            chkOldPatchNSD.Text = Resources.Config_chkOldPatchNSD;
            chkOldPatchNSD.Checked = Settings.Default.OldPatchNSD;
            chkDetailedCollision.Text = Resources.Config_chkDetailedCollision;
            chkDetailedCollision.Checked = Settings.Default.DetailedCollision;
            fraKeyBinds.Text = Resources.Config_fraKeyBinds;
            tglKeyBinds.Checked = Settings.Default.NewKeyBinds;
            chkCustomCrates.Checked = Settings.Default.UseCustomCrates;
            chkAnimViewPanel.Text = Resources.Config_chkAnimViewPanel;
            chkAnimViewPanel.Checked = Settings.Default.AnimViewPanel;
            chkCustomCrates.Text = Resources.Config_chkCustomCrates;
            cmdReset.Text = Resources.Config_cmdReset;
            cmdHelp.Text = Resources.Config_cmdHelp;
        }

        private void dpdLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Language = Languages[dpdLang.SelectedIndex];
            Settings.Default.Save();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning(Resources.Config_cmdResetText, Resources.Config_cmdResetTitle, DarkUI.Forms.DarkDialogButton.YesNo) == DialogResult.Yes)
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

        private void chkUseAnimLinks_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UseAnimLinks = chkUseAnimLinks.Checked;
            Settings.Default.Save();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (cdlClearCol.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ClearColorRGB = cdlClearCol.Color.ToArgb();
                picClearCol.BackColor = Color.FromArgb(Settings.Default.ClearColorRGB);
                Settings.Default.Save();
            }
        }

        private void cmdClearCol_EnabledChanged(object sender, EventArgs e)
        {
            if (cmdClearCol.Enabled)
            {
                cmdClearCol.BackColor = Color.Black;
            }
            else
            {
                cmdClearCol.BackColor = Color.White;
            }
        }

        private void cmdClearCol_Click(object sender, EventArgs e)
        {
            Settings.Default.ClearColorRGB = cdlClearCol.Color.ToArgb();
            cdlClearCol.Color = picClearCol.BackColor = Color.Black;

            cmdClearCol.Enabled = true;
            //背景色を灰色にしない
            cmdClearCol.BackColor = cmdClearCol.BackColor;

            Settings.Default.ClearColorRGB = cdlClearCol.Color.ToArgb();
            cdlClearCol.Color = picClearCol.BackColor = Color.Black;
            Settings.Default.Save();
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

        private void chkOldPatchNSD_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OldPatchNSD = chkOldPatchNSD.Checked;
            Settings.Default.Save();
        }

        private void chkDetailedCollision_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DetailedCollision = chkDetailedCollision.Checked;
            Settings.Default.Save();
        }

        private void TglKeyBinds_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.NewKeyBinds = tglKeyBinds.Checked;
            Settings.Default.Save();
        }

        private void CmdHelp_Click(object sender, EventArgs e)
        {
            if (frmhelp == null || frmhelp.IsDisposed)
            {
                frmhelp = new HelpWindow();
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

        private void ChkCustomCrates_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UseCustomCrates = chkCustomCrates.Checked;
            Settings.Default.Save();
        }

        private void ChkAnimViewPanel_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AnimViewPanel = chkAnimViewPanel.Checked;
            Settings.Default.Save();
        }
    }
}
