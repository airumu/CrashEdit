using System.Text;
using System.Text.RegularExpressions;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class EntityBox : UserControl
    {
        private EntityController controller;
        private Entity entity;

        private int positionindex;
        private int settingindex;
        private int victimindex;
        private int loadlistarowindex;
        private int loadlistaeidindex;
        private int loadlistbrowindex;
        private int loadlistbeidindex;
        private int drawlistarowindex;
        private int drawlistaentityindex;
        private int drawlistbrowindex;
        private int drawlistbentityindex;
        private int neighborindex;
        private int neighborsettingindex;
        private int fovframeindex;
        private int fovindex;
        private int victimlistindex => lbVictimID.SelectedIndex;
        private int lbeidalindex => lbEIDA.SelectedIndex;
        private int lbeidblindex => lbEIDB.SelectedIndex;
        private int lbentityaindex => lbEntityA.SelectedIndex;
        private int lbentitybindex => lbEntityB.SelectedIndex;

        private System.Windows.Forms.Timer argtexttimer;

        private DarkToolTip tipVictim;
        private DarkToolTip tipEIDA;
        private DarkToolTip tipEIDB;
        private DarkToolTip tipEntityA;
        private DarkToolTip tipEntityB;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        internal void MainInit()
        {
            InitializeComponent();
            UpdateName();
            UpdatePosition();
            UpdateType();
            UpdateSubtype();
            UpdateSettings();
            UpdateID();
            UpdateZMod();

            if (Settings.Default.EnableLegacyEntityBox == false)
            {
                // Check CameraCout to see if the entity is a camera
                ZoneEntry zone = controller.ZoneEntryController.ZoneEntry;
                if (zone.Entities.IndexOf(entity) < BitConv.FromInt32(zone.Header, 0x188))
                {
                    if (entity.CameraSubIndex == 0)
                    {
                        OldMainForm.ListUpdated += OnEntityListUpdated;
                    }
                    else
                    {
                        tbcTabs.Controls.Remove(tabLoadLists);
                        tbcTabs.Controls.Remove(tabDrawLists);
                        tabLoadLists.Dispose();
                        tabDrawLists.Dispose();
                    }
                    tbcTabs.Controls.Remove(tabSpecial);
                    tabSpecial.Dispose();
                    tabGeneral.Controls.Remove(fraName);
                    tabGeneral.Controls.Remove(fraID);
                    tabGeneral.Controls.Remove(fraType);
                    tabGeneral.Controls.Remove(fraSettings);
                    tabGeneral.Controls.Remove(fraZMod);
                    fraPosition.Location = new Point(4, 3);
                }
                else
                {
                    UpdateC2TTSets();
                    tbcTabs.Controls.Remove(tabCamera);
                    tbcTabs.Controls.Remove(tabLoadLists);
                    tbcTabs.Controls.Remove(tabDrawLists);
                    tabCamera.Dispose();
                    tabLoadLists.Dispose();
                    tabDrawLists.Dispose();
                }
            }
            else
                UpdateC2TTSets();

            positionindex = 0;
            victimindex = 0;
            loadlistarowindex = 0;
            loadlistaeidindex = 0;
            loadlistbrowindex = 0;
            loadlistbeidindex = 0;
            drawlistarowindex = 0;
            drawlistaentityindex = 0;
            drawlistbrowindex = 0;
            drawlistbentityindex = 0;
            neighborindex = 0;
            neighborsettingindex = 0;
            fovframeindex = 0;
            fovindex = 0;
            tabGeneral.Text = Resources.EntityBox_TabGeneral;
            tabSpecial.Text = Resources.EntityBox_TabSpecial;
            tabCamera.Text = Resources.EntityBox_TabCamera;
            tabLoadLists.Text = Resources.EntityBox_TabLoadList;
            tabDrawLists.Text = Resources.EntityBox_TabDrawList;
            fraName.Text = Resources.EntityBox_FraName;
            foreach (CheckBox chk in this.GetAll(typeof(CheckBox)))
            {
                if (chk.Text == "Enabled")
                {
                    chk.Text = Resources.EntityBox_ChkEnabled;
                }
            }
            chkBonusBoxCount.Text = Resources.EntityBox_ChkBonusBoxCount;
            fraID.Text = Resources.EntityBox_FraID;
            fraType.Text = Resources.EntityBox_FraType;
            fraPosition.Text = Resources.EntityBox_FraPosition;
            fraSettings.Text = Resources.EntityBox_FraSettings;
            foreach (Button cmd in this.GetAll(typeof(Button)))
            {
                if (cmd.Text == "Previous")
                {
                    cmd.Text = Resources.EntityBox_CmdPrevious;
                }
                else if (cmd.Text == "Next")
                {
                    cmd.Text = Resources.EntityBox_CmdNext;
                }
                else if (cmd.Text == "Remove")
                {
                    cmd.Text = Resources.EntityBox_CmdRemove;
                }
                else if (cmd.Text == "Insert")
                {
                    cmd.Text = Resources.EntityBox_CmdInsert;
                }
                else if (cmd.Text == "Append")
                {
                    cmd.Text = Resources.EntityBox_CmdAppend;
                }
                else if (cmd.Text == "Add")
                {
                    cmd.Text = Resources.EntityBox_CmdAdd;
                }
            }
            chkSettingHex.Text = Resources.EntityBox_ChkHex;
            cmdInterpolate.Text = Resources.EntityBox_CmdInterpolate;
            fraVictims.Text = Resources.EntityBox_FraVictims;
            fraBoxCount.Text = Resources.EntityBox_FraBoxCount;
            fraDDASection.Text = Resources.EntityBox_FraDDASection;
            fraDDASettings.Text = Resources.EntityBox_FraDDASettings;
            fraOtherSettings.Text = Resources.EntityBox_FraOtherSettings;
            fraZMod.Text = Resources.EntityBox_FraZMod;
            fraScaling.Text = Resources.EntityBox_FraScaling;
            fraTTReward.Text = Resources.EntityBox_FraTTReward;
            fraSLST.Text = Resources.EntityBox_FraSLST;
            cmdClearAllVictims.Text = Resources.EntityBox_cmdClearAllVictims;
            fraMode.Text = Resources.EntityBox_FraMode;
            fraAvgDist.Text = Resources.EntityBox_fraAvgDist;
            fraCameraIndex.Text = Resources.EntityBox_fraCameraIndex;
            fraCameraSubIndex.Text = Resources.EntityBox_fraCameraSubIndex;
            fraNeighbor.Text = Resources.EntityBox_fraNeighbor;
            lblNeighborPosition.Text = Resources.EntityBox_lblNeighborPosition;
            fraNeighborSetting.Text = Resources.EntityBox_fraNeighborSetting;
            lblNeighborCamera.Text = Resources.EntityBox_lblNeighborCamera;
            lblNeighborFlag.Text = Resources.EntityBox_lblNeighborFlag;
            lblNeighborLink.Text = Resources.EntityBox_lblNeighborLink;
            lblNeighborZone.Text = Resources.EntityBox_lblNeighborZone;
            fraFOV.Text = Resources.EntityBox_fraFOV;
            lblFOVPosition.Text = Resources.EntityBox_lblFOVPosition;
            fraFOVFrame.Text = Resources.EntityBox_fraFOVFrame;
            fraLoadListA.Text = Resources.EntityBox_fraLoadListA;
            fraLoadListB.Text = Resources.EntityBox_fraLoadListB;
            lblMetavalueLoadA.Text = Resources.EntityBox_lblMetavalueLoadA;
            lblMetavalueLoadB.Text = Resources.EntityBox_lblMetavalueLoadB;
            fraEIDA.Text = Resources.EntityBox_fraEIDA;
            fraEIDB.Text = Resources.EntityBox_fraEIDB;
            fraLoadListPayload.Text = Resources.EntityBox_fraLoadListPayload;
            cmdLoadListVerify.Text = Resources.EntityBox_cmdLoadListVerify;
            lblPayloadPosition.Text = Resources.EntityBox_lblPayloadPosition;
            cmdPayload.Text = Resources.EntityBox_cmdPayload;
            fraDrawListA.Text = Resources.EntityBox_fraDrawListA;
            fraDrawListB.Text = Resources.EntityBox_fraDrawListB;
            lblMetavalueDrawA.Text = Resources.EntityBox_lblMetavalueDrawA;
            lblMetavalueDrawB.Text = Resources.EntityBox_lblMetavalueDrawB;
            fraEntityA.Text = Resources.EntityBox_fraEntityA;
            fraEntityB.Text = Resources.EntityBox_fraEntityB;
            fraVerifyDrawList.Text = Resources.EntityBox_fraVerifyDrawList;
            cmdVerifyDrawList.Text = Resources.EntityBox_cmdLoadListVerify;
            lblArgAs.Text = MakeArgAsText();
            chkSettingHex_CheckedChanged(null, null);

            tipVictim = new DarkToolTip();
            tipVictim.SetToolTip(lbVictimID, Resources.EntityBox_tipLists);
            tipEIDA = new DarkToolTip();
            tipEIDA.SetToolTip(lbEIDA, Resources.EntityBox_tipLists);
            tipEIDB = new DarkToolTip();
            tipEIDB.SetToolTip(lbEIDB, Resources.EntityBox_tipLists);
            tipEntityA = new DarkToolTip();
            tipEntityA.SetToolTip(lbEntityA, Resources.EntityBox_tipLists);
            tipEntityB = new DarkToolTip();
            tipEntityB.SetToolTip(lbEntityB, Resources.EntityBox_tipLists);

            // use a Timer because of PAL switch
            argtexttimer = new()
            {
                Enabled = true,
                Interval = 40
            };
            argtexttimer.Tick += (object sender, EventArgs e) =>
            {
                lblArgAs.Text = MakeArgAsText();
            };
        }

        public EntityBox(EntityController controller)
        {
            this.controller = controller;
            entity = controller.Entity;
            MainInit();
        }

        internal string MakeArgAsText()
        {
            int arg = entity.Settings.Count > 0 ? entity.Settings[settingindex].Value : 0;
            return string.Format(Resources.EntityBox_lblArgAs,
                arg / 256F,
                arg / (float)0x1000 * 360,
                arg / (OldMainForm.PAL ? 25F : 30F),
                arg / (256F * 400));
        }

        private void UpdateName()
        {
            if (entity.Name != null)
            {
                txtName.Text = entity.Name;
                chkName.Checked = true;
            }
            else
            {
                txtName.Enabled = false;
                chkName.Checked = false;
            }
        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = chkName.Checked;
            entity.Name = chkName.Checked ? txtName.Text : null;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            entity.Name = txtName.Text;
        }

        private void UpdatePosition()
        {
            dirty.Push(true);
            if (positionindex >= entity.Positions.Count)
            {
                positionindex = entity.Positions.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (positionindex < 0)
            {
                positionindex = 0;
            }
            // Do not remove this either
            if (positionindex >= entity.Positions.Count)
            {
                lblPositionIndex.Text = "-- / --";
                cmdPreviousPosition.Enabled =
                cmdNextPosition.Enabled =
                cmdInsertPosition.Enabled =
                cmdRemovePosition.Enabled =
                cmdInterpolate.Enabled = false;
                lblX.Enabled = lblY.Enabled = lblZ.Enabled = numX.Enabled = numY.Enabled = numZ.Enabled = false;
            }
            else
            {
                lblPositionIndex.Text = $"{positionindex + 1} / {entity.Positions.Count}";
                cmdPreviousPosition.Enabled = positionindex > 0;
                cmdNextPosition.Enabled = positionindex < entity.Positions.Count - 1;
                cmdInsertPosition.Enabled = true;
                cmdRemovePosition.Enabled = true;
                lblX.Enabled = lblY.Enabled = lblZ.Enabled = numX.Enabled = numY.Enabled = numZ.Enabled = true;
                numX.Value = entity.Positions[positionindex].X;
                numY.Value = entity.Positions[positionindex].Y;
                numZ.Value = entity.Positions[positionindex].Z;
                cmdInterpolate.Enabled = entity.Positions.Count >= 2;
            }
            dirty.Pop();
        }

        private void cmdPreviousPosition_Click(object sender, EventArgs e)
        {
            --positionindex;
            UpdatePosition();
        }

        private void cmdNextPosition_Click(object sender, EventArgs e)
        {
            ++positionindex;
            UpdatePosition();
        }

        private void cmdInsertPosition_Click(object sender, EventArgs e)
        {
            entity.Positions.Insert(positionindex, entity.Positions[positionindex]);
            UpdatePosition();
        }

        private void cmdRemovePosition_Click(object sender, EventArgs e)
        {
            entity.Positions.RemoveAt(positionindex);
            UpdatePosition();
        }

        private void cmdAppendPosition_Click(object sender, EventArgs e)
        {
            positionindex = entity.Positions.Count;
            if (entity.Positions.Count > 0)
            {
                entity.Positions.Add(entity.Positions[positionindex - 1]);
            }
            else
            {
                entity.Positions.Add(new EntityPosition(0, 0, 0));
            }
            UpdatePosition();
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntityPosition pos = entity.Positions[positionindex];
                entity.Positions[positionindex] = new EntityPosition((short)numX.Value, pos.Y, pos.Z);
            }
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntityPosition pos = entity.Positions[positionindex];
                entity.Positions[positionindex] = new EntityPosition(pos.X, (short)numY.Value, pos.Z);
            }
        }

        private void numZ_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntityPosition pos = entity.Positions[positionindex];
                entity.Positions[positionindex] = new EntityPosition(pos.X, pos.Y, (short)numZ.Value);
            }
        }

        private void UpdateSettings()
        {
            dirty.Push(true);
            if (settingindex >= entity.Settings.Count)
            {
                settingindex = entity.Settings.Count - 1;
            }
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (settingindex < 0)
            {
                settingindex = 0;
            }
            // Do not remove this either
            if (settingindex >= entity.Settings.Count)
            {
                lblSettingIndex.Text = "-- / --";
                lblArgAs.Enabled =
                cmdPreviousSetting.Enabled =
                cmdNextSetting.Enabled =
                cmdRemoveSetting.Enabled =
                numSettingA.Enabled =
                numSettingB.Enabled =
                numSettingC.Enabled = false;
            }
            else
            {
                lblSettingIndex.Text = $"{settingindex + 1} / {entity.Settings.Count}";
                lblArgAs.Text = MakeArgAsText();
                cmdPreviousSetting.Enabled = settingindex > 0;
                cmdNextSetting.Enabled = settingindex < entity.Settings.Count - 1;
                cmdRemoveSetting.Enabled =
                lblArgAs.Enabled =
                numSettingA.Enabled =
                numSettingB.Enabled =
                numSettingC.Enabled = true;
                numSettingA.Value = entity.Settings[settingindex].ValueA;
                numSettingB.Value = entity.Settings[settingindex].ValueB;
                SetCVal(entity.Settings[settingindex].Value);
            }
            dirty.Pop();
        }

        private void cmdPreviousSetting_Click(object sender, EventArgs e)
        {
            --settingindex;
            UpdateSettings();
        }

        private void cmdNextSetting_Click(object sender, EventArgs e)
        {
            ++settingindex;
            UpdateSettings();
        }

        private void cmdAddSetting_Click(object sender, EventArgs e)
        {
            entity.Settings.Add(new EntitySetting(0, 0));
            UpdateSettings();
        }

        private void cmdRemoveSetting_Click(object sender, EventArgs e)
        {
            entity.Settings.RemoveAt(settingindex);
            UpdateSettings();
        }

        private void numSettingA_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntitySetting s = entity.Settings[settingindex];
                entity.Settings[settingindex] = new EntitySetting((byte)numSettingA.Value, s.ValueB);
                SetCVal(entity.Settings[settingindex].Value);
                lblArgAs.Text = MakeArgAsText();
            }
        }

        private void numSettingB_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                EntitySetting s = entity.Settings[settingindex];
                entity.Settings[settingindex] = new EntitySetting(s.ValueA, (int)numSettingB.Value);
                SetCVal(entity.Settings[settingindex].Value);
                lblArgAs.Text = MakeArgAsText();
            }
        }

        internal void SetCVal(long val)
        {
            dirty.Push(true);
            // this is fucking stupid
            if (numSettingC.Hexadecimal)
            {
                if (val > 0xFFFFFFFF) val = 0xFFFFFFFF;
                else if (val < 0) val &= 0xFFFFFFFF;
                numSettingC.Value = unchecked((uint)val);
            }
            else
            {
                if (val > 0xFFFFFFFF) val = 0x7FFFFFFF;
                else if (val > 0x7FFFFFFF) val = -0x100000000 + val;
                else if (val < -0x80000000) val = -0x80000000;
                numSettingC.Value = unchecked((int)val);
            }
            dirty.Pop();
        }

        private void numSettingC_ValueChanged(object sender, EventArgs e)
        {
            if (!Dirty)
            {
                SetCVal((long)numSettingC.Value);
                entity.Settings[settingindex] = new EntitySetting(((long)numSettingC.Value).UInt32ToInt32());
                dirty.Push(true);
                numSettingA.Value = entity.Settings[settingindex].ValueA;
                numSettingB.Value = entity.Settings[settingindex].ValueB;
                dirty.Pop();
                lblArgAs.Text = MakeArgAsText();
            }
        }

        private void UpdateID()
        {
            if (entity.ID.HasValue)
            {
                numID.Value = entity.ID.Value;
                if (entity.AlternateID.HasValue)
                {
                    numID2.Value = entity.AlternateID.Value;
                }
                numID2.Enabled = entity.AlternateID.HasValue;
                chkID2.Checked = entity.AlternateID.HasValue;
            }
            else
            {
                numID2.Enabled = false;
            }
            numID.Enabled = entity.ID.HasValue;
            chkID.Checked = entity.ID.HasValue;
            chkID2.Enabled = entity.ID.HasValue;
        }

        private void chkID_CheckedChanged(object sender, EventArgs e)
        {
            numID.Enabled = chkID.Checked;
            chkID2.Enabled = chkID.Checked;
            if (chkID.Checked)
            {
                entity.ID = (int)numID.Value;
            }
            else
            {
                chkID2.Checked = false;
                entity.ID = null;
            }
        }

        private void numID_ValueChanged(object sender, EventArgs e)
        {
            entity.ID = (int)numID.Value;
        }

        private void chkID2_CheckedChanged(object sender, EventArgs e)
        {
            numID2.Enabled = chkID2.Checked;
            if (chkID2.Checked)
            {
                entity.AlternateID = (int)numID2.Value;
            }
            else
            {
                entity.AlternateID = null;
            }
        }

        private void numID2_ValueChanged(object sender, EventArgs e)
        {
            entity.AlternateID = (int)numID2.Value;
        }

        private void UpdateType()
        {
            if (entity.Type.HasValue)
            {
                numType.Value = entity.Type.Value;
            }
            numType.Enabled = entity.Type.HasValue;
            chkType.Checked = entity.Type.HasValue;
        }

        private void chkType_CheckedChanged(object sender, EventArgs e)
        {
            numType.Enabled = chkType.Checked;
            if (chkType.Checked)
            {
                entity.Type = (int)numType.Value;
            }
            else
            {
                entity.Type = null;
            }
        }

        private void numType_ValueChanged(object sender, EventArgs e)
        {
            entity.Type = (int)numType.Value;
        }

        private void UpdateSubtype()
        {
            if (entity.Subtype.HasValue)
            {
                numSubtype.Value = entity.Subtype.Value;
            }
            numSubtype.Enabled = entity.Subtype.HasValue;
            chkSubtype.Checked = entity.Subtype.HasValue;
        }

        private void chkSubtype_CheckedChanged(object sender, EventArgs e)
        {
            numSubtype.Enabled = chkSubtype.Checked;
            if (chkSubtype.Checked)
            {
                entity.Subtype = (int)numSubtype.Value;
            }
            else
            {
                entity.Subtype = null;
            }
        }

        private void numSubtype_ValueChanged(object sender, EventArgs e)
        {
            entity.Subtype = (int)numSubtype.Value;
        }

        private void UpdateBoxCount()
        {
            if (entity.BoxCount.HasValue)
            {
                numBoxCount.Value = entity.BoxCount.Value.ValueB;
            }
            numBoxCount.Enabled = entity.BoxCount.HasValue;
            chkBoxCount.Checked = entity.BoxCount.HasValue;
            if (entity.BonusBoxCount.HasValue)
            {
                numBonusBoxCount.Value = entity.BonusBoxCount.Value.ValueB;
            }
            numBonusBoxCount.Enabled = entity.BonusBoxCount.HasValue;
            chkBonusBoxCount.Checked = entity.BonusBoxCount.HasValue;
        }

        private void chkBoxCount_CheckedChanged(object sender, EventArgs e)
        {
            numBoxCount.Enabled = chkBoxCount.Checked;
            if (chkBoxCount.Checked)
            {
                entity.BoxCount = new EntitySetting(0, (int)numBoxCount.Value);
            }
            else
            {
                entity.BoxCount = null;
            }
        }

        private void numBoxCount_ValueChanged(object sender, EventArgs e)
        {
            entity.BoxCount = new EntitySetting(0, (int)numBoxCount.Value);
        }

        private void chkBonusBoxCount_CheckedChanged(object sender, EventArgs e)
        {
            numBonusBoxCount.Enabled = chkBonusBoxCount.Checked;
            if (chkBonusBoxCount.Checked)
            {
                entity.BonusBoxCount = new EntitySetting(0, (int)numBonusBoxCount.Value);
            }
            else
            {
                entity.BonusBoxCount = null;
            }
        }

        private void numBonusBoxCount_ValueChanged(object sender, EventArgs e)
        {
            entity.BonusBoxCount = new EntitySetting(0, (int)numBonusBoxCount.Value);
        }

        private void UpdateVictim()
        {
            dirty.Push(true);
            if (victimindex >= entity.Victims.Count)
                victimindex = entity.Victims.Count - 1;
            // Do not make this else if,
            // sometimes both will run.
            // (this is intentional)
            if (victimindex < 0)
                victimindex = 0;
            if (victimindex >= entity.Victims.Count)
            {
                lblVictimIndex.Text = "-- / --";
                cmdRemoveVictim.Enabled =
                cmdClearAllVictims.Enabled = false;
            }
            else
            {
                lblVictimIndex.Text = $"{victimindex + 1} / {entity.Victims.Count}";
                cmdRemoveVictim.Enabled =
                cmdClearAllVictims.Enabled = true;
            }
            dirty.Pop();
        }

        private void LoadVictimList()
        {
            if (entity.Victims.Count > 0)
            {
                for (int i = 0; i < entity.Victims.Count; ++i)
                {
                    lbVictimID.Items.Add(entity.Victims[i].VictimID);
                }
                lbVictimID.SelectedIndex = 0;
            }
        }

        private void lbVictimID_SelectedIndexChanged(object sender, EventArgs e)
        {
            victimindex = lbVictimID.SelectedIndex;
            lblVictimIndex.Text = $"{victimindex + 1} / {entity.Victims.Count}";
        }

        private void lbVictimID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableVictimEditor(sender);
        }

        private void lbVictimID_DoubleClick(object sender, EventArgs e)
        {
            EnableVictimEditor(sender);
        }

        private void lbVictimID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableVictimEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbVictimID.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbVictimID.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

                if (e.KeyCode == Keys.X) // clear
                {
                    entity.Victims.Clear();
                    lbVictimID.Items.Clear();
                    UpdateVictim();
                }
            }
            // pate list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lbVictimID.Items.Count >= 1023) break;
                    var stripped = Regex.Replace(line, "[^0-9]", "");
                    if (stripped.Length > 0)
                    {
                        short victimid = Convert.ToInt16(stripped);
                        entity.Victims.Add(new(victimid));
                        lbVictimID.Items.Add(victimid);
                    }
                }
                if (lbVictimID.Items.Count > 0 && lbVictimID.SelectedIndex == -1)
                    lbVictimID.SelectedIndex = 0;
                UpdateVictim();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbVictimID.Items.Count <= 0) return;

                string s = lbVictimID.Items[victimlistindex].ToString();
                Clipboard.SetText(s);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbVictimID.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                var match = Regex.Match(s, @"\d+");
                if (match.Success)
                {
                    short victimid = Convert.ToInt16(match.Value);
                    entity.Victims[victimlistindex] = new EntityVictim(victimid);
                    lbVictimID.Items[victimlistindex] = victimid;
                    UpdateVictim();
                }
            }
        }

        private void EnableVictimEditor(object sender)
        {
            if (lbVictimID.Items.Count <= 0) return;

            lbVictimID = (DarkListBox)sender;
            numEditVictimID.Enabled = true;
            numEditVictimID.Value = entity.Victims[victimlistindex].VictimID;
            numEditVictimID.Focus();
            numEditVictimID.Select(0, numEditVictimID.Text.Length);
            numEditVictimID.KeyPress += new KeyPressEventHandler(VictimEditor_EditOver);
            numEditVictimID.LostFocus += VictimEditor_FocusOver;
        }

        private void VictimEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateVictimList(false);
        }

        private void VictimEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // avoid to play "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateVictimList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateVictimList(true);
            }
        }

        private void UpdateVictimList(bool cancel)
        {
            // if the input is empty
            if (numEditVictimID.Text == "")
            {
                numEditVictimID.Value = 0;
            }
            // if the number is invalid or pressed escape key
            else if (numEditVictimID.Value > 32767 || cancel)
            {
                numEditVictimID.Value = entity.Victims[victimlistindex].VictimID;
            }
            else
            {
                entity.Victims[victimlistindex] = new EntityVictim((short)numEditVictimID.Value);
                lbVictimID.Items[victimlistindex] = numEditVictimID.Value;
            }
            UpdateVictim();
            numEditVictimID.Enabled = false;
            lbVictimID.Focus();
        }

        private void cmdInsertVictim_Click(object sender, EventArgs e)
        {
            if (entity.Victims.Count > 0)
            {
                entity.Victims.Insert(victimlistindex, entity.Victims[victimlistindex]);
                lbVictimID.Items.Insert(victimlistindex, entity.Victims[victimlistindex].VictimID);
            }
            else
            {
                entity.Victims.Add(new EntityVictim(10));
                lbVictimID.Items.Add(10);
                victimindex = 0;
                lbVictimID.SelectedIndex = 0;
            }
            UpdateVictim();
        }

        private void cmdRemoveVictim_Click(object sender, EventArgs e)
        {
            int selectedindex = victimlistindex;
            entity.Victims.RemoveAt(victimlistindex);
            lbVictimID.Items.RemoveAt(victimlistindex);
            UpdateVictim();
            if (lbVictimID.Items.Count > 0)
            {
                if (selectedindex >= lbVictimID.Items.Count)
                    selectedindex = lbVictimID.Items.Count - 1;
                lbVictimID.Focus();
                lbVictimID.SelectedIndex = selectedindex;
            }
        }

        private void cmdClearAllVictims_Click(object sender, EventArgs e)
        {
            entity.Victims.Clear();
            lbVictimID.Items.Clear();
            UpdateVictim();
        }

        public static string CheckEname(string ename)
        {
            if (ename.Length != 5)
            {
                return string.Empty;
            }
            int eid = Entry.NullEID;
            try
            {
                eid = Entry.ENameToEID(ename);
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }
            return ename;
        }

        private void OnEntityListUpdated(object sender, EventArgs e)
        {
            if (InvokeRequired) Invoke(new Action(UpdateLoadLists));
            else UpdateLoadLists();
        }

        private void UpdateLoadLists()
        {
            LoadEIDAList();
            LoadEIDBList();
        }

        private void UpdateLoadListA()
        {
            if (entity.LoadListA != null && entity.LoadListA.RowCount != 0)
            {
                fraLoadListPayload.Enabled = entity.LoadListB != null;
                if (loadlistarowindex >= entity.LoadListA.RowCount)
                    loadlistarowindex = entity.LoadListA.RowCount - 1;
                lblMetavalueLoadA.Enabled = true;
                numMetavalueLoadA.Enabled = true;
                lblLoadListRowIndexA.Text = $"{loadlistarowindex + 1} / {entity.LoadListA.RowCount}";
                numMetavalueLoadA.Value = entity.LoadListA.Rows[loadlistarowindex].MetaValue.Value;
                cmdPrevRowA.Enabled = loadlistarowindex > 0;
                cmdNextRowA.Enabled = loadlistarowindex + 1 < entity.LoadListA.RowCount;
                cmdRemoveRowA.Enabled = true;
                if (entity.LoadListA.Rows[loadlistarowindex].Values.Count > 0)
                {
                    if (loadlistaeidindex >= entity.LoadListA.Rows[loadlistarowindex].Values.Count)
                        loadlistaeidindex = entity.LoadListA.Rows[loadlistarowindex].Values.Count - 1;
                    cmdInsertEIDA.Enabled = true;
                    cmdRemoveEIDA.Enabled = true;
                    //txtEIDA.Enabled = true;
                    lblEIDErrA.Visible = true;
                    lblEIDIndexA.Text = $"{lbeidalindex + 1} / {entity.LoadListA.Rows[loadlistarowindex].Values.Count}";
                    //txtEIDA.Text = Entry.EIDToEName(entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex]);
                }
                else
                {
                    cmdAppendEIDA.Enabled = true;
                    cmdInsertEIDA.Enabled = false;
                    cmdRemoveEIDA.Enabled = false;
                    txtEIDA.Enabled = false;
                    lblEIDErrA.Visible = false;
                    lblEIDIndexA.Text = "-- / --";
                }
            }
            else
            {
                fraLoadListPayload.Enabled = false;
                entity.LoadListA = null;
                lblLoadListRowIndexA.Text = "-- / --";
                lblEIDIndexA.Text = "-- / --";
                lblMetavalueLoadA.Enabled = false;
                numMetavalueLoadA.Enabled = false;
                cmdPrevRowA.Enabled = false;
                cmdNextRowA.Enabled = false;
                cmdRemoveRowA.Enabled = false;
                txtEIDA.Enabled = false;
                cmdRemoveEIDA.Enabled = false;
                cmdInsertEIDA.Enabled = false;
                cmdAppendEIDA.Enabled = false;
                lblEIDErrA.Visible = false;
            }
        }

        private void LoadEIDAList()
        {
            lbEIDA.Items.Clear();
            if (entity.LoadListA != null && entity.LoadListA.RowCount != 0)
            {
                if (entity.LoadListA.Rows[loadlistarowindex].Values.Count != 0)
                {
                    for (int i = 0; i < entity.LoadListA.Rows[loadlistarowindex].Values.Count; ++i)
                    {
                        string item = Entry.EIDToEName(entity.LoadListA.Rows[loadlistarowindex].Values[i]);
                        lbEIDA.Items.Add(item);
                    }
                    lbEIDA.SelectedIndex = 0;
                    UpdatetxtEIDA();
                }
            }
        }

        private void lbEIDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableEIDAEditor(sender);

            e.Handled = true;
        }

        private void lbEIDA_DoubleClick(object sender, EventArgs e)
        {
            EnableEIDAEditor(sender);
        }

        private void lbEIDA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableEIDAEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbEIDA.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbEIDA.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

                if (e.KeyCode == Keys.X) // clear
                {
                    entity.LoadListA.Rows[loadlistarowindex].Values.Clear();
                    lbEIDA.Items.Clear();
                    UpdateLoadListA();
                }
            }
            // paste list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (entity.LoadListA.Rows[loadlistarowindex].Values.Count >= 1023) break;
                    if (CheckEname(line).Length > 0)
                    {
                        entity.LoadListA.Rows[loadlistarowindex].Values.Add(Entry.ENameToEID(line));
                        lbEIDA.Items.Add(line);
                    }
                }
                if (lbEIDA.Items.Count > 0 && lbEIDA.SelectedIndex == -1)
                    lbEIDA.SelectedIndex = 0;
                UpdateLoadListA();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbEIDA.Items.Count <= 0) return;

                string s = lbEIDA.Items[lbeidalindex].ToString();
                Clipboard.SetText(s);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbEIDA.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                if (CheckEname(s).Length > 0)
                {
                    entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex] = Entry.ENameToEID(s);
                    lbEIDA.Items[lbeidalindex] = s;
                    UpdateLoadListA();
                }
            }
        }

        private void EnableEIDAEditor(object sender)
        {
            if (lbEIDA.Items.Count <= 0) return;

            lbEIDA = (DarkListBox)sender;
            txtEIDA.Enabled = true;
            UpdatetxtEIDA();
            txtEIDA.Focus();
            txtEIDA.SelectAll();
            txtEIDA.KeyPress += new KeyPressEventHandler(EIDAEditor_EditOver);
            txtEIDA.LostFocus += EIDAEditor_FocusOver;
        }

        private void EIDAEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateEIDAList(false);
        }

        private void EIDAEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // prevent "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateEIDAList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateEIDAList(true);
            }
        }

        private void UpdateEIDAList(bool cancel)
        {
            lblEIDErrA.Text = Entry.CheckEIDErrors(txtEIDA.Text, true);
            if (lblEIDErrA.Text != string.Empty || cancel)
            {
                UpdatetxtEIDA();
            }
            else
            {
                entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex] = Entry.ENameToEID(txtEIDA.Text);
                lbEIDA.Items[lbeidalindex] = txtEIDA.Text;
                UpdateLoadListA();
            }
            txtEIDA.Enabled = false;
            lbEIDA.Focus();
        }

        private void lbEIDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEIDIndexA.Text = $"{lbeidalindex + 1} / {entity.LoadListA.Rows[loadlistarowindex].Values.Count}";
        }

        private void UpdatetxtEIDA()
        {
            txtEIDA.Text = Entry.EIDToEName(entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex]);
        }

        private void cmdRemoveEIDA_Click(object sender, EventArgs e)
        {
            int selectedindex = lbeidalindex;
            entity.LoadListA.Rows[loadlistarowindex].Values.RemoveAt(lbeidalindex);
            lbEIDA.Items.RemoveAt(lbeidalindex);
            UpdateLoadListA();
            if (lbEIDA.Items.Count > 0)
            {
                if (selectedindex >= lbEIDA.Items.Count)
                    selectedindex = lbEIDA.Items.Count - 1;
                lbEIDA.SelectedIndex = selectedindex;
                lbEIDA.Focus();
            }
        }

        private void cmdInsertEIDA_Click(object sender, EventArgs e)
        {
            int item = entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex];
            string item_n = Entry.EIDToEName(item);
            entity.LoadListA.Rows[loadlistarowindex].Values.Insert(lbeidalindex, item);
            lbEIDA.Items.Insert(lbeidalindex, item_n);
            UpdateLoadListA();
        }

        private void cmdAppendEIDA_Click(object sender, EventArgs e)
        {
            loadlistaeidindex = entity.LoadListA.Rows[loadlistarowindex].Values.Count;
            if (entity.LoadListA.Rows[loadlistarowindex].Values.Count > 0)
            {
                int item = entity.LoadListA.Rows[loadlistarowindex].Values[loadlistaeidindex - 1];
                string item_n = Entry.EIDToEName(item);
                entity.LoadListA.Rows[loadlistarowindex].Values.Add(item);
                lbEIDA.Items.Add(item_n);
                lbEIDA.SelectedIndex = loadlistaeidindex;
            }
            else
            {
                entity.LoadListA.Rows[loadlistarowindex].Values.Add(Entry.NullEID);
                lbEIDA.Items.Add(Entry.EIDToEName(Entry.NullEID));
                lbEIDA.SelectedIndex = 0;
            }
            UpdateLoadListA();
        }

        private void txtEIDA_TextChanged(object sender, EventArgs e)
        {
            lblEIDErrA.Text = Entry.CheckEIDErrors(txtEIDA.Text, true);
            if (lblEIDErrA.Text != string.Empty) return;
            //entity.LoadListA.Rows[loadlistarowindex].Values[lbeidalindex] = Entry.ENameToEID(txtEIDA.Text);
        }

        private void cmdPrevRowA_Click(object sender, EventArgs e)
        {
            --loadlistarowindex;
            UpdateLoadListA();
            LoadEIDAList();
        }

        private void cmdNextRowA_Click(object sender, EventArgs e)
        {
            ++loadlistarowindex;
            UpdateLoadListA();
            LoadEIDAList();
        }

        private void cmdRemoveRowA_Click(object sender, EventArgs e)
        {
            entity.LoadListA.Rows.RemoveAt(loadlistarowindex);
            UpdateLoadListA();
            LoadEIDAList();
        }

        private void cmdInsertRowA_Click(object sender, EventArgs e)
        {
            if (entity.LoadListA == null || entity.LoadListA.Rows.Count == 0)
            {
                entity.LoadListA = new EntityT4Property();
                entity.LoadListA.Rows.Add(new EntityPropertyRow<int>());
                entity.LoadListA.Rows[entity.LoadListA.RowCount - 1].MetaValue = 0;
                loadlistarowindex = entity.LoadListA.RowCount - 1;
                loadlistaeidindex = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<int>();
                newrow.MetaValue = entity.LoadListA.Rows[loadlistarowindex].MetaValue;
                foreach (var val in entity.LoadListA.Rows[loadlistarowindex].Values)
                    newrow.Values.Add(val);
                entity.LoadListA.Rows.Insert(loadlistarowindex, newrow);
            }
            UpdateLoadListA();
            LoadEIDAList();
        }

        private void numMetavalueLoadA_ValueChanged(object sender, EventArgs e)
        {
            entity.LoadListA.Rows[loadlistarowindex].MetaValue = (short)numMetavalueLoadA.Value;
        }

        private void UpdateLoadListB()
        {
            if (entity.LoadListB != null && entity.LoadListB.RowCount != 0)
            {
                fraLoadListPayload.Enabled = entity.LoadListB != null;
                if (loadlistbrowindex >= entity.LoadListB.RowCount)
                    loadlistbrowindex = entity.LoadListB.RowCount - 1;
                lblMetavalueLoadB.Enabled = true;
                numMetavalueLoadB.Enabled = true;
                lblLoadListRowIndexB.Text = $"{loadlistbrowindex + 1} / {entity.LoadListB.RowCount}";
                numMetavalueLoadB.Value = entity.LoadListB.Rows[loadlistbrowindex].MetaValue.Value;
                cmdPrevRowB.Enabled = loadlistbrowindex > 0;
                cmdNextRowB.Enabled = loadlistbrowindex + 1 < entity.LoadListB.RowCount;
                cmdRemoveRowB.Enabled = true;
                if (entity.LoadListB.Rows[loadlistbrowindex].Values.Count > 0)
                {
                    if (loadlistbeidindex >= entity.LoadListB.Rows[loadlistbrowindex].Values.Count)
                        loadlistbeidindex = entity.LoadListB.Rows[loadlistbrowindex].Values.Count - 1;
                    cmdInsertEIDB.Enabled = true;
                    cmdRemoveEIDB.Enabled = true;
                    //txtEIDB.Enabled = true;
                    lblEIDErrB.Visible = true;
                    lblEIDIndexB.Text = $"{lbeidblindex + 1} / {entity.LoadListB.Rows[loadlistbrowindex].Values.Count}";
                    //txtEIDB.Text = Entry.EIDToEName(entity.LoadListB.Rows[loadlistbrowindex].Values[loadlistbeidindex]);
                }
                else
                {
                    cmdAppendEIDB.Enabled = true;
                    cmdInsertEIDB.Enabled = false;
                    cmdRemoveEIDB.Enabled = false;
                    txtEIDB.Enabled = false;
                    lblEIDErrB.Visible = false;
                    lblEIDIndexB.Text = "-- / --";
                }
            }
            else
            {
                fraLoadListPayload.Enabled = false;
                entity.LoadListB = null;
                lblLoadListRowIndexB.Text = "-- / --";
                lblEIDIndexB.Text = "-- / --";
                lblMetavalueLoadB.Enabled = false;
                numMetavalueLoadB.Enabled = false;
                cmdPrevRowB.Enabled = false;
                cmdNextRowB.Enabled = false;
                cmdRemoveRowB.Enabled = false;
                txtEIDB.Enabled = false;
                cmdRemoveEIDB.Enabled = false;
                cmdInsertEIDB.Enabled = false;
                cmdAppendEIDB.Enabled = false;
                lblEIDErrB.Visible = false;
            }
        }

        private void LoadEIDBList()
        {
            lbEIDB.Items.Clear();
            if (entity.LoadListB != null && entity.LoadListB.RowCount != 0)
            {
                if (entity.LoadListB.Rows[loadlistbrowindex].Values.Count != 0)
                {
                    for (int i = 0; i < entity.LoadListB.Rows[loadlistbrowindex].Values.Count; ++i)
                    {
                        string item = Entry.EIDToEName(entity.LoadListB.Rows[loadlistbrowindex].Values[i]);
                        lbEIDB.Items.Add(item);
                    }
                    lbEIDB.SelectedIndex = 0;
                    UpdatetxtEIDB();
                }
            }
        }

        private void lbEIDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableEIDBEditor(sender);

            e.Handled = true;
        }

        private void lbEIDB_DoubleClick(object sender, EventArgs e)
        {
            EnableEIDBEditor(sender);
        }

        private void lbEIDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableEIDBEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbEIDB.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbEIDB.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

                if (e.KeyCode == Keys.X) // clear
                {
                    entity.LoadListB.Rows[loadlistbrowindex].Values.Clear();
                    lbEIDB.Items.Clear();
                    UpdateLoadListB();
                }
            }
            // pate list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (entity.LoadListB.Rows[loadlistbrowindex].Values.Count >= 1023) break;
                    if (CheckEname(line).Length > 0)
                    {
                        entity.LoadListB.Rows[loadlistbrowindex].Values.Add(Entry.ENameToEID(line));
                        lbEIDB.Items.Add(line);
                    }
                }
                if (lbEIDB.Items.Count > 0 && lbEIDB.SelectedIndex == -1)
                    lbEIDB.SelectedIndex = 0;
                UpdateLoadListB();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbEIDB.Items.Count <= 0) return;

                string s = lbEIDB.Items[lbeidblindex].ToString();
                Clipboard.SetText(s);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbEIDB.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                if (CheckEname(s).Length > 0)
                {
                    entity.LoadListB.Rows[loadlistbrowindex].Values[lbeidblindex] = Entry.ENameToEID(s);
                    lbEIDB.Items[lbeidblindex] = s;
                    UpdateLoadListB();
                }
            }
        }

        private void EnableEIDBEditor(object sender)
        {
            if (lbEIDB.Items.Count <= 0) return;

            lbEIDB = (DarkListBox)sender;
            txtEIDB.Enabled = true;
            UpdatetxtEIDB();
            txtEIDB.Focus();
            txtEIDB.SelectAll();
            txtEIDB.KeyPress += new KeyPressEventHandler(EIDBEditor_EditOver);
            txtEIDB.LostFocus += EIDBEditor_FocusOver;
        }

        private void EIDBEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateEIDBList(false);
        }

        private void EIDBEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // prevent "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateEIDBList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateEIDBList(true);
            }
        }

        private void UpdateEIDBList(bool cancel)
        {
            lblEIDErrB.Text = Entry.CheckEIDErrors(txtEIDB.Text, true);
            if (lblEIDErrB.Text != string.Empty || cancel)
            {
                UpdatetxtEIDB();
            }
            else
            {
                entity.LoadListB.Rows[loadlistbrowindex].Values[lbeidblindex] = Entry.ENameToEID(txtEIDB.Text);
                lbEIDB.Items[lbeidblindex] = txtEIDB.Text;
                UpdateLoadListB();
            }
            txtEIDB.Enabled = false;
            lbEIDB.Focus();
        }

        private void lbEIDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEIDIndexB.Text = $"{lbeidblindex + 1} / {entity.LoadListB.Rows[loadlistbrowindex].Values.Count}";
        }

        private void UpdatetxtEIDB()
        {
            txtEIDB.Text = Entry.EIDToEName(entity.LoadListB.Rows[loadlistbrowindex].Values[lbeidblindex]);
        }

        private void cmdRemoveEIDB_Click(object sender, EventArgs e)
        {
            int selectedindex = lbeidblindex;
            entity.LoadListB.Rows[loadlistbrowindex].Values.RemoveAt(lbeidblindex);
            lbEIDB.Items.RemoveAt(lbeidblindex);
            UpdateLoadListB();
            if (lbEIDB.Items.Count > 0)
            {
                if (selectedindex >= lbEIDB.Items.Count)
                    selectedindex = lbEIDB.Items.Count - 1;
                lbEIDB.SelectedIndex = selectedindex;
                lbEIDB.Focus();
            }
        }

        private void cmdInsertEIDB_Click(object sender, EventArgs e)
        {
            int item = entity.LoadListB.Rows[loadlistbrowindex].Values[lbeidblindex];
            string item_n = Entry.EIDToEName(item);
            entity.LoadListB.Rows[loadlistbrowindex].Values.Insert(lbeidblindex, item);
            lbEIDB.Items.Insert(lbeidblindex, item_n);
            UpdateLoadListB();
        }

        private void cmdAppendEIDB_Click(object sender, EventArgs e)
        {
            loadlistbeidindex = entity.LoadListB.Rows[loadlistbrowindex].Values.Count;
            if (entity.LoadListB.Rows[loadlistbrowindex].Values.Count > 0)
            {
                int item = entity.LoadListB.Rows[loadlistbrowindex].Values[loadlistbeidindex - 1];
                string item_n = Entry.EIDToEName(item);
                entity.LoadListB.Rows[loadlistbrowindex].Values.Add(item);
                lbEIDB.Items.Add(item_n);
                lbEIDB.SelectedIndex = loadlistbeidindex;
            }
            else
            {
                entity.LoadListB.Rows[loadlistbrowindex].Values.Add(Entry.NullEID);
                lbEIDB.Items.Add(Entry.EIDToEName(Entry.NullEID));
                lbEIDB.SelectedIndex = 0;
            }
            UpdateLoadListB();
        }

        private void txtEIDB_TextChanged(object sender, EventArgs e)
        {
            lblEIDErrB.Text = Entry.CheckEIDErrors(txtEIDB.Text, true);
            if (lblEIDErrB.Text != string.Empty) return;
            //entity.LoadListB.Rows[loadlistbrowindex].Values[lbeidblindex] = Entry.ENameToEID(txtEIDB.Text);
        }

        private void cmdPrevRowB_Click(object sender, EventArgs e)
        {
            --loadlistbrowindex;
            UpdateLoadListB();
            LoadEIDBList();
        }

        private void cmdNextRowB_Click(object sender, EventArgs e)
        {
            ++loadlistbrowindex;
            UpdateLoadListB();
            LoadEIDBList();
        }

        private void cmdRemoveRowB_Click(object sender, EventArgs e)
        {
            entity.LoadListB.Rows.RemoveAt(loadlistbrowindex);
            UpdateLoadListB();
            LoadEIDBList();
        }

        private void cmdInsertRowB_Click(object sender, EventArgs e)
        {
            if (entity.LoadListB == null || entity.LoadListB.Rows.Count == 0)
            {
                entity.LoadListB = new EntityT4Property();
                entity.LoadListB.Rows.Add(new EntityPropertyRow<int>());
                entity.LoadListB.Rows[entity.LoadListB.RowCount - 1].MetaValue = 0;
                loadlistbrowindex = entity.LoadListB.RowCount - 1;
                loadlistbeidindex = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<int>();
                newrow.MetaValue = entity.LoadListB.Rows[loadlistbrowindex].MetaValue;
                foreach (var val in entity.LoadListB.Rows[loadlistbrowindex].Values)
                    newrow.Values.Add(val);
                entity.LoadListB.Rows.Insert(loadlistbrowindex, newrow);
            }
            UpdateLoadListB();
            LoadEIDBList();
        }

        private void numMetavalueLoadB_ValueChanged(object sender, EventArgs e)
        {
            entity.LoadListB.Rows[loadlistbrowindex].MetaValue = (short)numMetavalueLoadB.Value;
        }

        private int GetEntityID(decimal value)
        {
            foreach (ZoneEntry zone in controller.GetEntries<ZoneEntry>())
            {
                foreach (Entity otherentity in zone.Entities)
                {
                    if (otherentity.ID.HasValue && otherentity.ID.Value == value)
                    {
                        for (int i = 0; i < controller.ZoneEntryController.ZoneEntry.ZoneCount; ++i)
                        {
                            if (zone.EID == BitConv.FromInt32(controller.ZoneEntryController.ZoneEntry.Header, 0x194 + i * 4))
                            {
                                return (int)(i | (otherentity.ID << 8) | ((zone.Entities.IndexOf(otherentity) - BitConv.FromInt32(zone.Header, 0x188)) << 24));
                            }
                        }
                    }
                }
            }
            return 0;
        }

        private void UpdateDrawListA()
        {
            if (entity.DrawListA != null && entity.DrawListA.RowCount != 0)
            {
                fraVerifyDrawList.Enabled = entity.DrawListB != null;
                if (drawlistarowindex >= entity.DrawListA.RowCount)
                    drawlistarowindex = entity.DrawListA.RowCount - 1;
                lblMetavalueDrawA.Enabled = true;
                numMetavalueDrawA.Enabled = true;
                lblDrawListRowIndexA.Text = $"{drawlistarowindex + 1} / {entity.DrawListA.RowCount}";
                numMetavalueDrawA.Value = entity.DrawListA.Rows[drawlistarowindex].MetaValue.Value;
                cmdPrevRowDrawA.Enabled = drawlistarowindex > 0;
                cmdNextRowDrawA.Enabled = drawlistarowindex + 1 < entity.DrawListA.RowCount;
                cmdRemoveRowDrawA.Enabled = true;
                if (entity.DrawListA.Rows[drawlistarowindex].Values.Count > 0)
                {
                    if (drawlistaentityindex >= entity.DrawListA.Rows[drawlistarowindex].Values.Count)
                        drawlistaentityindex = entity.DrawListA.Rows[drawlistarowindex].Values.Count - 1;
                    cmdInsertEntityA.Enabled = true;
                    cmdRemoveEntityA.Enabled = true;
                    //numEntityA.Enabled = true;
                    lblEntityIndexA.Text = $"{drawlistaentityindex + 1} / {entity.DrawListA.Rows[drawlistarowindex].Values.Count}";
                    //numEntityA.Value = entity.DrawListA.Rows[drawlistarowindex].Values[drawlistaentityindex] >> 8 & 0xFFFF;
                }
                else
                {
                    cmdAppendEntityA.Enabled = true;
                    cmdInsertEntityA.Enabled = false;
                    cmdRemoveEntityA.Enabled = false;
                    numEntityA.Enabled = false;
                    lblEntityIndexA.Text = "-- / --";
                }
            }
            else
            {
                fraVerifyDrawList.Enabled = false;
                entity.DrawListA = null;
                lblDrawListRowIndexA.Text = "-- / --";
                lblEntityIndexA.Text = "-- / --";
                lblMetavalueDrawA.Enabled = false;
                numMetavalueDrawA.Enabled = false;
                cmdPrevRowDrawA.Enabled = false;
                cmdNextRowDrawA.Enabled = false;
                cmdRemoveRowDrawA.Enabled = false;
                numEntityA.Enabled = false;
                cmdRemoveEntityA.Enabled = false;
                cmdInsertEntityA.Enabled = false;
                cmdAppendEntityA.Enabled = false;
            }
        }

        private void LoadDrawListAList()
        {
            lbEntityA.Items.Clear();
            if (entity.DrawListA != null && entity.DrawListA.RowCount != 0)
            {
                if (entity.DrawListA.Rows[drawlistarowindex].Values.Count > 0)
                {
                    for (int i = 0; i < entity.DrawListA.Rows[drawlistarowindex].Values.Count; ++i)
                    {
                        lbEntityA.Items.Add(entity.DrawListA.Rows[drawlistarowindex].Values[i] >> 8 & 0xFFFF);
                    }
                    lbEntityA.SelectedIndex = 0;
                    UpdatenumEntityA();
                }
            }
        }

        private void lbEntityA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableDrawListAEditor(sender);
        }

        private void lbEntityA_DoubleClick(object sender, EventArgs e)
        {
            EnableDrawListAEditor(sender);
        }

        private void lbEntityA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableDrawListAEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbEntityA.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbEntityA.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

                if (e.KeyCode == Keys.X) // clear
                {
                    entity.DrawListA.Rows[drawlistarowindex].Values.Clear();
                    lbEntityA.Items.Clear();
                    UpdateDrawListA();
                }
            }
            // paste list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (entity.DrawListA.Rows[drawlistarowindex].Values.Count >= 1023) break;
                    var stripped = Regex.Replace(line, "[^0-9]", "");
                    if (stripped.Length > 0)
                    {
                        int id = GetEntityID(Convert.ToInt16(stripped));
                        if (id > 0)
                        {
                            drawlistaentityindex = entity.DrawListA.Rows[drawlistarowindex].Values.Count;
                            entity.DrawListA.Rows[drawlistarowindex].Values.Add(id);
                            lbEntityA.Items.Add(id >> 8 & 0xFFFF);
                        }
                    }
                }
                if (lbEntityA.Items.Count > 0 && lbEntityA.SelectedIndex == -1)
                    lbEntityA.SelectedIndex = 0;
                UpdateDrawListA();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbEntityA.Items.Count <= 0) return;

                string s = lbEntityA.Items[lbentityaindex].ToString();
                Clipboard.SetText(s);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbEntityA.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                var match = Regex.Match(s, @"\d+");
                if (match.Success)
                {
                    int id = GetEntityID(Convert.ToInt16(match.Value));
                    if (id > 0)
                    {
                        entity.DrawListA.Rows[drawlistarowindex].Values[lbentityaindex] = id;
                        lbEntityA.Items[lbentityaindex] = id >> 8 & 0xFFFF;
                        UpdateDrawListA();
                    }
                }
            }
        }

        private void EnableDrawListAEditor(object sender)
        {
            if (lbEntityA.Items.Count <= 0) return;

            lbEntityA = (DarkListBox)sender;
            numEntityA.Enabled = true;
            UpdatenumEntityA();
            numEntityA.Focus();
            numEntityA.Select(0, numEntityA.Text.Length);
            numEntityA.KeyPress += new KeyPressEventHandler(DrawListAEditor_EditOver);
            numEntityA.LostFocus += DrawListAEditor_FocusOver;
        }

        private void DrawListAEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateDrawListAList(false);
        }

        private void DrawListAEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // prevent "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateDrawListAList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateDrawListAList(true);
            }
        }

        private void UpdateDrawListAList(bool cancel)
        {
            if (numEntityA.Text == "") numEntityA.Value = 0;
            int id = GetEntityID(numEntityA.Value);
            if (id > 0 && !cancel)
            {
                entity.DrawListA.Rows[drawlistarowindex].Values[lbentityaindex] = id;
                lbEntityA.Items[lbentityaindex] = id >> 8 & 0xFFFF;
            }
            else numEntityA.Value = entity.DrawListA.Rows[drawlistarowindex].Values[lbentityaindex] >> 8 & 0xFFFF;
            UpdateDrawListA();
            UpdatenumEntityA();
            numEntityA.Enabled = false;
            lbEntityA.Focus();
        }

        private void lbEntityA_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEntityIndexA.Text = $"{lbentityaindex + 1} / {entity.DrawListA.Rows[drawlistarowindex].Values.Count}";
        }

        private void UpdatenumEntityA()
        {
            numEntityA.Value = entity.DrawListA.Rows[drawlistarowindex].Values[lbentityaindex] >> 8 & 0xFFFF;
        }

        private void cmdRemoveEntityA_Click(object sender, EventArgs e)
        {
            int selectedindex = lbentityaindex;
            entity.DrawListA.Rows[drawlistarowindex].Values.RemoveAt(lbentityaindex);
            lbEntityA.Items.RemoveAt(lbentityaindex);
            UpdateDrawListA();
            if (lbEntityA.Items.Count > 0)
            {
                if (selectedindex >= lbEntityA.Items.Count)
                    selectedindex = lbEntityA.Items.Count - 1;
                lbEntityA.SelectedIndex = selectedindex;
                lbEntityA.Focus();
            }
        }

        private void cmdInsertEntityA_Click(object sender, EventArgs e)
        {
            int item = entity.DrawListA.Rows[drawlistarowindex].Values[lbentityaindex];
            entity.DrawListA.Rows[drawlistarowindex].Values.Insert(lbentityaindex, item);
            lbEntityA.Items.Insert(lbentityaindex, item >> 8 & 0xFFFF);
            UpdateDrawListA();
        }

        private void cmdAppendEntityA_Click(object sender, EventArgs e)
        {
            drawlistaentityindex = entity.DrawListA.Rows[drawlistarowindex].Values.Count;
            if (entity.DrawListA.Rows[drawlistarowindex].Values.Count > 0)
            {
                int item = entity.DrawListA.Rows[drawlistarowindex].Values[drawlistaentityindex - 1];
                entity.DrawListA.Rows[drawlistarowindex].Values.Add(item);
                lbEntityA.Items.Add(item >> 8 & 0xFFFF);
                lbEntityA.SelectedIndex = drawlistaentityindex;
            }
            else
            {
                entity.DrawListA.Rows[drawlistarowindex].Values.Add(0);
                lbEntityA.Items.Add(0);
            }
            if (lbEntityA.SelectedIndex == -1)
                lbEntityA.SelectedIndex = 0;
            UpdateDrawListA();
        }

        private void numEntityA_ValueChanged(object sender, EventArgs e)
        {
        }

        private void cmdPrevRowDrawA_Click(object sender, EventArgs e)
        {
            --drawlistarowindex;
            UpdateDrawListA();
            LoadDrawListAList();
        }

        private void cmdNextRowDrawA_Click(object sender, EventArgs e)
        {
            ++drawlistarowindex;
            UpdateDrawListA();
            LoadDrawListAList();
        }

        private void cmdRemoveRowDrawA_Click(object sender, EventArgs e)
        {
            entity.DrawListA.Rows.RemoveAt(drawlistarowindex);
            UpdateDrawListA();
            LoadDrawListAList();
        }

        private void cmdInsertRowDrawA_Click(object sender, EventArgs e)
        {
            if (entity.DrawListA == null || entity.DrawListA.Rows.Count == 0)
            {
                entity.DrawListA = new EntityInt32Property();
                entity.DrawListA.Rows.Add(new EntityPropertyRow<int>());
                entity.DrawListA.Rows[entity.DrawListA.RowCount - 1].MetaValue = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<int>();
                newrow.MetaValue = entity.DrawListA.Rows[drawlistarowindex].MetaValue;
                foreach (var val in entity.DrawListA.Rows[drawlistarowindex].Values)
                    newrow.Values.Add(val);
                entity.DrawListA.Rows.Insert(drawlistarowindex, newrow);
            }
            UpdateDrawListA();
            LoadDrawListAList();
        }

        private void numMetavalueDrawA_ValueChanged(object sender, EventArgs e)
        {
            entity.DrawListA.Rows[drawlistarowindex].MetaValue = (short)numMetavalueDrawA.Value;
        }

        private void UpdateDrawListB()
        {
            if (entity.DrawListB != null && entity.DrawListB.RowCount != 0)
            {
                fraVerifyDrawList.Enabled = entity.DrawListB != null;
                if (drawlistbrowindex >= entity.DrawListB.RowCount)
                    drawlistbrowindex = entity.DrawListB.RowCount - 1;
                lblMetavalueDrawB.Enabled = true;
                numMetavalueDrawB.Enabled = true;
                lblDrawListRowIndexB.Text = $"{drawlistbrowindex + 1} / {entity.DrawListB.RowCount}";
                numMetavalueDrawB.Value = entity.DrawListB.Rows[drawlistbrowindex].MetaValue.Value;
                cmdPrevRowDrawB.Enabled = drawlistbrowindex > 0;
                cmdNextRowDrawB.Enabled = drawlistbrowindex + 1 < entity.DrawListB.RowCount;
                cmdRemoveRowDrawB.Enabled = true;
                if (entity.DrawListB.Rows[drawlistbrowindex].Values.Count > 0)
                {
                    if (drawlistbentityindex >= entity.DrawListB.Rows[drawlistbrowindex].Values.Count)
                        drawlistbentityindex = entity.DrawListB.Rows[drawlistbrowindex].Values.Count - 1;
                    cmdInsertEntityB.Enabled = true;
                    cmdRemoveEntityB.Enabled = true;
                    //numEntityB.Enabled = true;
                    lblEntityIndexB.Text = $"{drawlistbentityindex + 1} / {entity.DrawListB.Rows[drawlistbrowindex].Values.Count}";
                    //numEntityB.Value = entity.DrawListB.Rows[drawlistbrowindex].Values[drawlistbentityindex] >> 8 & 0xFFFF;
                }
                else
                {
                    cmdAppendEntityB.Enabled = true;
                    cmdInsertEntityB.Enabled = false;
                    cmdRemoveEntityB.Enabled = false;
                    numEntityB.Enabled = false;
                    lblEntityIndexB.Text = "-- / --";
                }
            }
            else
            {
                fraVerifyDrawList.Enabled = false;
                entity.DrawListB = null;
                lblDrawListRowIndexB.Text = "-- / --";
                lblEntityIndexB.Text = "-- / --";
                lblMetavalueDrawB.Enabled = false;
                numMetavalueDrawB.Enabled = false;
                cmdPrevRowDrawB.Enabled = false;
                cmdNextRowDrawB.Enabled = false;
                cmdRemoveRowDrawB.Enabled = false;
                numEntityB.Enabled = false;
                cmdRemoveEntityB.Enabled = false;
                cmdInsertEntityB.Enabled = false;
                cmdAppendEntityB.Enabled = false;
            }
        }

        private void LoadDrawListBList()
        {
            lbEntityB.Items.Clear();
            if (entity.DrawListB != null && entity.DrawListB.RowCount != 0)
            {
                if (entity.DrawListB.Rows[drawlistbrowindex].Values.Count > 0)
                {
                    for (int i = 0; i < entity.DrawListB.Rows[drawlistbrowindex].Values.Count; ++i)
                    {
                        lbEntityB.Items.Add(entity.DrawListB.Rows[drawlistbrowindex].Values[i] >> 8 & 0xFFFF);
                    }
                    lbEntityB.SelectedIndex = 0;
                    UpdatenumEntityB();
                }
            }
        }

        private void lbEntityB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                EnableDrawListBEditor(sender);
        }

        private void lbEntityB_DoubleClick(object sender, EventArgs e)
        {
            EnableDrawListBEditor(sender);
        }

        private void lbEntityB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                EnableDrawListBEditor(sender);
            }
            // copy list
            else if ((e.KeyCode == Keys.C || e.KeyCode == Keys.X) && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                if (lbEntityB.Items.Count <= 0) return;

                StringBuilder sb = new StringBuilder();
                foreach (object item in lbEntityB.Items)
                {
                    sb.Append(item + Environment.NewLine);
                }
                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

                if (e.KeyCode == Keys.X) // clear 
                {
                    entity.DrawListB.Rows[drawlistbrowindex].Values.Clear();
                    lbEntityB.Items.Clear();
                    UpdateDrawListB();
                }
            }
            // paste list
            else if (e.KeyCode == Keys.V && (e.Modifiers & Keys.Control) == Keys.Control && (e.Modifiers & Keys.Shift) == Keys.Shift)
            {
                StringReader sr = new StringReader(Clipboard.GetText());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (entity.DrawListB.Rows[drawlistbrowindex].Values.Count >= 1023) break;
                    var stripped = Regex.Replace(line, "[^0-9]", "");
                    if (stripped.Length > 0)
                    {
                        int id = GetEntityID(Convert.ToInt16(stripped));
                        if (id > 0)
                        {
                            drawlistbentityindex = entity.DrawListB.Rows[drawlistbrowindex].Values.Count;
                            entity.DrawListB.Rows[drawlistbrowindex].Values.Add(id);
                            lbEntityB.Items.Add(id >> 8 & 0xFFFF);
                        }
                    }
                }
                if (lbEntityB.Items.Count > 0 && lbEntityB.SelectedIndex == -1)
                    lbEntityB.SelectedIndex = 0;
                UpdateDrawListB();
            }
            // copy selected item's eid
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                if (lbEntityB.Items.Count <= 0) return;

                string s = lbEntityB.Items[lbentitybindex].ToString();
                Clipboard.SetText(s);
            }
            // paste eid to selected item
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (lbEntityB.Items.Count <= 0) return;

                string s = Clipboard.GetText();
                var match = Regex.Match(s, @"\d+");
                if (match.Success)
                {
                    int id = GetEntityID(Convert.ToInt16(match.Value));
                    if (id > 0)
                    {
                        entity.DrawListB.Rows[drawlistbrowindex].Values[lbentitybindex] = id;
                        lbEntityB.Items[lbentitybindex] = id >> 8 & 0xFFFF;
                        UpdateDrawListB();
                    }
                }
            }
        }

        private void EnableDrawListBEditor(object sender)
        {
            if (lbEntityB.Items.Count <= 0) return;

            lbEntityB = (DarkListBox)sender;
            numEntityB.Enabled = true;
            UpdatenumEntityB();
            numEntityB.Focus();
            numEntityB.Select(0, numEntityB.Text.Length);
            numEntityB.KeyPress += new KeyPressEventHandler(DrawListBEditor_EditOver);
            numEntityB.LostFocus += DrawListBEditor_FocusOver;
        }

        private void DrawListBEditor_FocusOver(object sender, EventArgs e)
        {
            UpdateDrawListBList(false);
        }

        private void DrawListBEditor_EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // prevent "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                UpdateDrawListBList(false);
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                UpdateDrawListBList(true);
            }
        }

        private void UpdateDrawListBList(bool cancel)
        {
            if (numEntityB.Text == "") numEntityB.Value = 0;
            int id = GetEntityID(numEntityB.Value);
            if (id > 0 && !cancel)
            {
                entity.DrawListB.Rows[drawlistbrowindex].Values[lbentitybindex] = id;
                lbEntityB.Items[lbentitybindex] = id >> 8 & 0xFFFF;
            }
            else numEntityB.Value = entity.DrawListB.Rows[drawlistbrowindex].Values[lbentitybindex] >> 8 & 0xFFFF;
            UpdateDrawListB();
            UpdatenumEntityB();
            numEntityB.Enabled = false;
            lbEntityB.Focus();
        }

        private void lbEntityB_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEntityIndexB.Text = $"{lbentitybindex + 1} / {entity.DrawListB.Rows[drawlistbrowindex].Values.Count}";
        }

        private void UpdatenumEntityB()
        {
            numEntityB.Value = entity.DrawListB.Rows[drawlistbrowindex].Values[lbentitybindex] >> 8 & 0xFFFF;
        }

        private void cmdRemoveEntityB_Click(object sender, EventArgs e)
        {
            int selectedindex = lbentitybindex;
            entity.DrawListB.Rows[drawlistbrowindex].Values.RemoveAt(lbentitybindex);
            lbEntityB.Items.RemoveAt(lbentitybindex);
            UpdateDrawListB();
            if (lbEntityB.Items.Count > 0)
            {
                if (selectedindex >= lbEntityB.Items.Count)
                    selectedindex = lbEntityB.Items.Count - 1;
                lbEntityB.SelectedIndex = selectedindex;
                lbEntityB.Focus();
            }
        }

        private void cmdInsertEntityB_Click(object sender, EventArgs e)
        {
            int item = entity.DrawListB.Rows[drawlistbrowindex].Values[lbentitybindex];
            entity.DrawListB.Rows[drawlistbrowindex].Values.Insert(lbentitybindex, item);
            lbEntityB.Items.Insert(lbentitybindex, item >> 8 & 0xFFFF);
            UpdateDrawListB();
        }

        private void cmdAppendEntityB_Click(object sender, EventArgs e)
        {
            drawlistbentityindex = entity.DrawListB.Rows[drawlistbrowindex].Values.Count;
            if (entity.DrawListB.Rows[drawlistbrowindex].Values.Count > 0)
            {
                int item = entity.DrawListB.Rows[drawlistbrowindex].Values[drawlistbentityindex - 1];
                entity.DrawListB.Rows[drawlistbrowindex].Values.Add(item);
                lbEntityB.Items.Add(item >> 8 & 0xFFFF);
                lbEntityB.SelectedIndex = drawlistbentityindex;
            }
            else
            {
                entity.DrawListB.Rows[drawlistbrowindex].Values.Add(0);
                lbEntityB.Items.Add(0);
            }
            if (lbEntityB.SelectedIndex == -1)
                lbEntityB.SelectedIndex = 0;
            UpdateDrawListB();
        }

        private void numEntityB_ValueChanged(object sender, EventArgs e)
        {
        }

        private void cmdPrevRowDrawB_Click(object sender, EventArgs e)
        {
            --drawlistbrowindex;
            UpdateDrawListB();
            LoadDrawListBList();
        }

        private void cmdNextRowDrawB_Click(object sender, EventArgs e)
        {
            ++drawlistbrowindex;
            UpdateDrawListB();
            LoadDrawListBList();
        }

        private void cmdRemoveRowDrawB_Click(object sender, EventArgs e)
        {
            entity.DrawListB.Rows.RemoveAt(drawlistbrowindex);
            UpdateDrawListB();
            LoadDrawListBList();
        }

        private void cmdInsertRowDrawB_Click(object sender, EventArgs e)
        {
            if (entity.DrawListB == null || entity.DrawListB.Rows.Count == 0)
            {
                entity.DrawListB = new EntityInt32Property();
                entity.DrawListB.Rows.Add(new EntityPropertyRow<int>());
                entity.DrawListB.Rows[entity.DrawListB.RowCount - 1].MetaValue = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<int>();
                newrow.MetaValue = entity.DrawListB.Rows[drawlistbrowindex].MetaValue;
                foreach (var val in entity.DrawListB.Rows[drawlistbrowindex].Values)
                    newrow.Values.Add(val);
                entity.DrawListB.Rows.Insert(drawlistbrowindex, newrow);
            }
            UpdateDrawListB();
            LoadDrawListBList();
        }

        private void numMetavalueDrawB_ValueChanged(object sender, EventArgs e)
        {
            entity.DrawListB.Rows[drawlistbrowindex].MetaValue = (short)numMetavalueDrawB.Value;
        }

        private void UpdateDDASettings()
        {
            if (entity.DDASettings.HasValue)
            {
                numDDASettings.Value = entity.DDASettings.Value >> 8;
            }
            numDDASettings.Enabled = entity.DDASettings.HasValue;
            chkDDASettings.Checked = entity.DDASettings.HasValue;
        }

        private void chkDDASettings_CheckedChanged(object sender, EventArgs e)
        {
            numDDASettings.Enabled = chkDDASettings.Checked;
            if (chkDDASettings.Checked)
            {
                entity.DDASettings = (int)numDDASettings.Value << 8;
            }
            else
            {
                entity.DDASettings = null;
            }
        }

        private void numDDASettings_ValueChanged(object sender, EventArgs e)
        {
            entity.DDASettings = (int)numDDASettings.Value << 8;
        }

        private void UpdateDDASection()
        {
            if (entity.DDASection.HasValue)
            {
                numDDASection.Value = entity.DDASection.Value;
            }
            numDDASection.Enabled = entity.DDASection.HasValue;
            chkDDASection.Checked = entity.DDASection.HasValue;
        }

        private void chkDDASection_CheckedChanged(object sender, EventArgs e)
        {
            numDDASection.Enabled = chkDDASection.Checked;
            if (chkDDASection.Checked)
            {
                entity.DDASection = (int)numDDASection.Value;
            }
            else
            {
                entity.DDASection = null;
            }
        }

        private void numDDASection_ValueChanged(object sender, EventArgs e)
        {
            entity.DDASection = (int)numDDASection.Value;
        }

        private void UpdateScaling()
        {
            if (entity.Scaling.HasValue)
            {
                numScaling.Value = entity.Scaling.Value;
            }
            numScaling.Enabled = entity.Scaling.HasValue;
            chkScaling.Checked = entity.Scaling.HasValue;
        }

        private void chkScaling_CheckedChanged(object sender, EventArgs e)
        {
            numScaling.Enabled = chkScaling.Checked;
            if (chkScaling.Checked)
            {
                entity.Scaling = (int)numScaling.Value;
            }
            else
            {
                entity.Scaling = null;
            }
        }

        private void numScaling_ValueChanged(object sender, EventArgs e)
        {
            entity.Scaling = (int)numScaling.Value;
        }

        private void UpdateOtherSettings()
        {
            if (entity.OtherSettings.HasValue)
            {
                numOtherSettings.Value = entity.OtherSettings.Value;
            }
            numOtherSettings.Enabled = entity.OtherSettings.HasValue;
            chkOtherSettings.Checked = entity.OtherSettings.HasValue;
        }

        private void chkOtherSettings_CheckedChanged(object sender, EventArgs e)
        {
            numOtherSettings.Enabled = chkOtherSettings.Checked;
            if (chkOtherSettings.Checked)
            {
                entity.OtherSettings = (int)numOtherSettings.Value;
            }
            else
            {
                entity.OtherSettings = null;
            }
        }

        private void numOtherSettings_ValueChanged(object sender, EventArgs e)
        {
            entity.OtherSettings = (int)numOtherSettings.Value;
        }

        private void UpdateSLST()
        {
            if (entity.SLST != null)
            {
                txtSLST.Text = Entry.EIDToEName(entity.SLST.Rows[0].Values[0]);
                chkSLST.Checked = true;
                lblEIDErr1.Visible = true;
                txtSLST.Enabled = true;
            }
            else
            {
                txtSLST.Enabled = false;
                chkSLST.Checked = false;
                lblEIDErr1.Visible = false;
                txtSLST.Enabled = false;
            }
        }

        private void chkSLST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSLST.Checked)
            {
                lblEIDErr1.Text = Entry.CheckEIDErrors(txtSLST.Text, true);
                entity.SLST = new EntityT4Property();
                entity.SLST.Rows.Add(new EntityPropertyRow<int>());
                if (lblEIDErr1.Text != string.Empty)
                    entity.SLST.Rows[0].Values.Add(Entry.NullEID);
                else
                    entity.SLST.Rows[0].Values.Add(Entry.ENameToEID(txtSLST.Text));
            }
            else
            {
                entity.SLST = null;
            }
            UpdateSLST();
        }

        private void txtSLST_TextChanged(object sender, EventArgs e)
        {
            lblEIDErr1.Text = Entry.CheckEIDErrors(txtSLST.Text, true);
            if (lblEIDErr1.Text != string.Empty) return;
            entity.SLST.Rows[0].Values[0] = Entry.ENameToEID(txtSLST.Text);
        }

        private void tabSpecial_Enter(object sender, EventArgs e)
        {
            LoadVictimList();
            UpdateVictim();
            UpdateBoxCount();
            UpdateDDASection();
            UpdateDDASettings();
            if (controller.GetNSF().Version == GameVersion.Crash3)
            {
                UpdateScaling();
                UpdateOtherSettings();
                UpdateTTReward();
            }
            else
            {
                tabSpecial.Controls.Remove(fraTTReward);
                tabSpecial.Controls.Remove(fraOtherSettings);
                tabSpecial.Controls.Remove(fraScaling);
            }
            tabSpecial.Enter -= tabSpecial_Enter;
        }

        private void tabCamera_Enter(object sender, EventArgs e)
        {
            UpdateSLST();
            UpdateCameraIndex();
            UpdateCameraSubIndex();
            UpdateMode();
            UpdateAvgDist();
            UpdateNeighbors();
            UpdateFOV();
            tabCamera.Enter -= tabCamera_Enter;
        }

        private void tabLoadLists_Enter(object sender, EventArgs e)
        {
            LoadEIDAList();
            LoadEIDBList();
            UpdateLoadListA();
            UpdateLoadListB();
            //CheckPayload();
            tabLoadLists.Enter -= tabLoadLists_Enter;
        }

        private void tabDrawLists_Enter(object sender, EventArgs e)
        {
            UpdateDrawListA();
            UpdateDrawListB();
            LoadDrawListAList();
            LoadDrawListBList();
            tabDrawLists.Enter -= tabDrawLists_Enter;
        }

        private void tabProperties_Enter(object sender, EventArgs e)
        {
            EnableDoubleBuffering(dgvPropertyHeader);
            EnableDoubleBuffering(dgvPropertyRaw);
            EnableDoubleBuffering(dgvPropertyMetaValues);
            EnableDoubleBuffering(dgvPropertyValues);
            SetDarkTheme(dgvPropertyHeader);
            SetDarkTheme(dgvPropertyRaw);
            SetDarkTheme(dgvPropertyMetaValues);
            SetDarkTheme(dgvPropertyValues);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem insertRowItem = new ToolStripMenuItem("Insert Row");
            ToolStripMenuItem deleteRowItem = new ToolStripMenuItem("Delete Row");

            insertRowItem.Click += InsertRowItem_Click;
            deleteRowItem.Click += DeleteRowItem_Click;
            contextMenu.Items.Add(insertRowItem);
            contextMenu.Items.Add(deleteRowItem);

            dgvPropertyMetaValues.ContextMenuStrip = contextMenu;
            dgvPropertyValues.ContextMenuStrip = contextMenu;
            dgvPropertyMetaValues.CellMouseDown += DataGridView_CellMouseDown;
            dgvPropertyValues.CellMouseDown += DataGridView_CellMouseDown;

            CreatePropertyHeaderColumns();
            CreatePropertyRawColumns();
            CreatePropertyMetaValuesColumns();
            UpdatePropertyIDList();
            tabProperties.Enter -= tabProperties_Enter;
        }

        private DataGridView currentDataGridView;
        private object selectedField = new object();
        private bool propertyStyle = false;
        private const string titleError = "Error";
        private const string titleInputError = "Input Error";

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                currentDataGridView = sender as DataGridView;
                if (e.RowIndex >= 0)
                {
                    currentDataGridView.ClearSelection();
                    currentDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
        }

        private void InsertRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("Please select a row to insert.", titleError);
                return;
            }
            if (lbProperties.SelectedItem == null) return;

            dynamic field = selectedField;
            if (field == null) return;

            if (currentDataGridView == dgvPropertyMetaValues)
            {
                dynamic newRow = null!;
                int selectedRow = -1;

                if (field == null || field.RowCount == 0)
                {
                    if (field is EntityVictimProperty)
                    {
                        field = new EntityVictimProperty();
                        field.Rows.Add(new EntityPropertyRow<EntityVictim>());
                    }
                    else if (field is EntityInt32Property)
                    {
                        field = new EntityInt32Property();
                        field.Rows.Add(new EntityPropertyRow<int>());
                    }
                    else if (field is EntityUInt32Property)
                    {
                        field = new EntityUInt32Property();
                        field.Rows.Add(new EntityPropertyRow<uint>());
                    }
                    else if (field is EntitySettingProperty)
                    {
                        field = new EntitySettingProperty();
                        field.Rows.Add(new EntityPropertyRow<EntitySetting>());
                    }
                    field.Rows[field.RowCount - 1].MetaValue = 0;
                    Console.WriteLine("Added MetaValue.");
                }
                else
                {
                    if (!(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
                    selectedRow = dgvPropertyMetaValues.SelectedCells[0].RowIndex;
                    if (field is EntityVictimProperty)
                    {
                        newRow = new EntityPropertyRow<EntityVictim>();
                    }
                    else if (field is EntityInt32Property)
                    {
                        newRow = new EntityPropertyRow<int>();
                    }
                    else if (field is EntityUInt32Property)
                    {
                        newRow = new EntityPropertyRow<uint>();
                    }
                    else if (field is EntitySettingProperty)
                    {
                        newRow = new EntityPropertyRow<EntitySetting>();
                    }
                    newRow.MetaValue = field.Rows[selectedRow].MetaValue;
                    foreach (var val in field.Rows[selectedRow].Values)
                        newRow.Values.Add(val);
                    field.Rows.Insert(selectedRow, newRow);
                    Console.WriteLine("Inserted MetaValue.");
                }

                if (selectedRow == -1)
                    dgvPropertyMetaValues.Rows.Add("0");
                else
                    dgvPropertyMetaValues.Rows.Insert(selectedRow, $"{newRow.MetaValue}");
            }
            else if (currentDataGridView == dgvPropertyValues)
            {
                dynamic newValue = null!;
                int selectedRow = -1;
                int rowindex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

                if (field is EntityVictimProperty)
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(new EntityVictim());
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                        newValue = newValue.VictimID; // to fix the row value
                    }
                }
                else if (field is EntitySettingProperty)
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(new EntitySetting());
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                        newValue = newValue.Value; // to fix the row value
                    }
                }
                else
                {
                    if (field.Rows[rowindex].Values.Count == 0)
                        field.Rows[rowindex].Values.Add(0);
                    else
                    {
                        selectedRow = dgvPropertyValues.SelectedCells[0].RowIndex;
                        newValue = field.Rows[rowindex].Values[selectedRow];
                        field.Rows[rowindex].Values.Insert(selectedRow, newValue);
                    }
                }

                if (field is EntityVictimProperty || field is EntitySettingProperty)
                {
                    if (selectedRow == -1)
                        dgvPropertyValues.Rows.Add("0");
                    else
                        dgvPropertyValues.Rows.Insert(selectedRow, newValue.ToString("X"));
                }
                else
                {
                    if (selectedRow == -1)
                        dgvPropertyValues.Rows.Add("0", "0", "0", "0");
                    else
                    {
                        long value1 = (newValue & (0xFF << 0)) >> 0;
                        long value2 = (newValue & (0xFF << 8)) >> 8;
                        long value3 = (newValue & (0xFF << 16)) >> 16;
                        long value4 = (newValue & (0xFF << 24)) >> 24;
                        dgvPropertyValues.Rows.Insert(selectedRow, value1.ToString("X"), value2.ToString("X"), value3.ToString("X"), value4.ToString("X"));
                    }
                }
            }
            UpdatePropertyHeader();
        }

        private void DeleteRowItem_Click(object sender, EventArgs e)
        {
            if (currentDataGridView == null)
            {
                DarkMessageBox.ShowError("Please select a row to delete.", titleError);
                return;
            }
            if (lbProperties.SelectedItem == null || !(currentDataGridView.SelectedRows.Count > 0) || !(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
            int rowindex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

            dynamic field = selectedField;
            if (field == null) return;

            foreach (DataGridViewRow selectedRow in currentDataGridView.SelectedRows)
            {
                if (!selectedRow.IsNewRow)
                {
                    if (currentDataGridView == dgvPropertyMetaValues)
                    {
                        field.Rows.RemoveAt(selectedRow.Index);
                        if (field.RowCount == 0)
                        {
                            short id = Convert.ToInt16(lbProperties.SelectedItem.ToString(), 16);
                            NullifyField(id);
                        }
                    }
                    else if (currentDataGridView == dgvPropertyValues)
                    {
                        field.Rows[rowindex].Values.RemoveAt(selectedRow.Index);
                    }

                    currentDataGridView.Rows.Remove(selectedRow);
                }
            }
            UpdatePropertyHeader();
        }

        private void NullifyField(short id)
        {
            switch (id)
            {
                case 0x183: entity.Field0x183 = null!; break;
                case 0x185: entity.Flags = null!; break;
                case 0x198: entity.Field0x198 = null!; break;
                case 0x1A8: entity.Field0x1A8 = null!; break;
                case 0x1B5: entity.Rain1 = null!; break;
                case 0x1B6: entity.Rain2 = null!; break;
                case 0x1B8: entity.Rain4 = null!; break;
                case 0x1F9: entity.Field0x1F9 = null!; break;
                case 0x1FA: entity.Field0x1FA = null!; break;
            }
        }

        private void AddField(short id)
        {
            switch (id)
            {
                case 0x183:
                    entity.Field0x183 = new EntityVictimProperty();
                    entity.Field0x183.Rows.Add(new EntityPropertyRow<EntityVictim>());
                    entity.Field0x183.Rows[0].MetaValue = 0;
                    break;
                case 0x185:
                    entity.Flags = new EntityUInt32Property();
                    entity.Flags.Rows.Add(new EntityPropertyRow<uint>());
                    entity.Flags.Rows[0].MetaValue = 0;
                    break;
                case 0x198:
                    entity.Field0x198 = new EntitySettingProperty();
                    entity.Field0x198.Rows.Add(new EntityPropertyRow<EntitySetting>());
                    entity.Field0x198.Rows[0].MetaValue = 0;
                    break;
                case 0x1A8:
                    entity.Field0x1A8 = new EntityUInt32Property();
                    entity.Field0x1A8.Rows.Add(new EntityPropertyRow<uint>());
                    entity.Field0x1A8.Rows[0].MetaValue = 0;
                    break;
                case 0x1B5:
                    entity.Rain1 = new EntityVictimProperty();
                    entity.Rain1.Rows.Add(new EntityPropertyRow<EntityVictim>());
                    entity.Rain1.Rows[0].MetaValue = 0;
                    break;
                case 0x1B6:
                    entity.Rain2 = new EntityUInt32Property();
                    entity.Rain2.Rows.Add(new EntityPropertyRow<uint>());
                    entity.Rain2.Rows[0].MetaValue = 0;
                    break;
                case 0x1B7:
                    entity.Rain4 = new EntityInt32Property();
                    entity.Rain4.Rows.Add(new EntityPropertyRow<int>());
                    entity.Rain4.Rows[0].MetaValue = 0;
                    break;
                case 0x1DE:
                    entity.FogDist = new EntityUInt32Property();
                    entity.FogDist.Rows.Add(new EntityPropertyRow<uint>());
                    entity.FogDist.Rows[0].MetaValue = 0;
                    break;
                case 0x1F9:
                    entity.Field0x1F9 = new EntityVictimProperty();
                    entity.Field0x1F9.Rows.Add(new EntityPropertyRow<EntityVictim>());
                    entity.Field0x1F9.Rows[0].MetaValue = 0;
                    break;
                case 0x1FA:
                    entity.Field0x1FA = new EntityUInt32Property();
                    entity.Field0x1FA.Rows.Add(new EntityPropertyRow<uint>());
                    entity.Field0x1FA.Rows[0].MetaValue = 0;
                    break;
                default:
                    DarkMessageBox.ShowError($"Unsupported or invalid field: {id:X}", titleError);
                    return;
            }
        }

        private const short Field0x183 = 0x183;
        private const short Flags = 0x185;
        private const short Field0x198 = 0x198;
        private const short Field0x1A8 = 0x1A8;
        private const short Rain1 = 0x1B5;
        private const short Rain2 = 0x1B6;
        private const short Rain4 = 0x1B8;
        private const short FogDist = 0x1DE;
        private const short Field0x1F9 = 0x1F9;
        private const short Field0x1FA = 0x1FA;

        private object GetField(short id, Entity entity)
        {
            return id switch
            {
                Field0x183 => entity.Field0x183,
                Flags => entity.Flags,
                Rain1 => entity.Rain1,
                Field0x198 => entity.Field0x198,
                Field0x1A8 => entity.Field0x1A8,
                Rain2 => entity.Rain2,
                Rain4 => entity.Rain4,
                FogDist => entity.FogDist,
                Field0x1F9 => entity.Field0x1F9,
                Field0x1FA => entity.Field0x1FA,
                _ => null!
            };
        }

        private void CreatePropertyHeaderColumns()
        {
            dgvPropertyHeader.Columns.Add("Type", "Type");
            dgvPropertyHeader.Columns.Add("ElementSize", "ElementSize");
            dgvPropertyHeader.Columns.Add("RowCount", "RowCount");
            dgvPropertyHeader.Columns.Add("IsSparse", "IsSparse");
            dgvPropertyHeader.Columns.Add("HasMetaValues", "HasMetaValues");

            foreach (DataGridViewColumn column in dgvPropertyHeader.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void CreatePropertyRawColumns()
        {
            dgvPropertyRaw.Columns.Add("Raw", "Raw");
            foreach (DataGridViewColumn column in dgvPropertyRaw.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void CreatePropertyMetaValuesColumns()
        {
            dgvPropertyMetaValues.Columns.Clear();
            string columnMetaValue = "Position";
            dgvPropertyMetaValues.Columns.Add(columnMetaValue, columnMetaValue);
        }

        private void CreatePropertyValuesColumns()
        {
            dgvPropertyValues.Columns.Clear();

            if (!(selectedField is EntityVictimProperty) && !(selectedField is EntitySettingProperty))
            {
                string columnValue = "Value";
                dgvPropertyValues.Columns.Add(columnValue, columnValue);
                string columnValue1 = "Seg1";
                string columnValue2 = "Seg2";
                string columnValue3 = "Seg3";
                string columnValue4 = "Seg4";
                dgvPropertyValues.Columns.Add(columnValue1, columnValue1);
                dgvPropertyValues.Columns.Add(columnValue2, columnValue2);
                dgvPropertyValues.Columns.Add(columnValue3, columnValue3);
                dgvPropertyValues.Columns.Add(columnValue4, columnValue4);
                if (propertyStyle)
                {
                    dgvPropertyValues.Columns[1].Visible = false;
                    dgvPropertyValues.Columns[2].Visible = false;
                    dgvPropertyValues.Columns[3].Visible = false;
                    dgvPropertyValues.Columns[4].Visible = false;
                }
                else
                {
                    dgvPropertyValues.Columns[0].Visible = false;
                }
            }
            else
            {
                string columnValue = "Value";
                dgvPropertyValues.Columns.Add(columnValue, columnValue);
            }
        }

        private void UpdatePropertyIDList()
        {
            if (entity.KnownProperties != null && entity.KnownProperties.Count > 0)
            {
                lbProperties.Enabled = true;
                lbProperties.Items.Clear();
                foreach (var item in entity.KnownProperties)
                {
                    lbProperties.Items.Add(item.Key.ToString("X"));
                }
                lbProperties.SelectedIndex = 0;
                LoadField();
            }
            else
                lbProperties.Enabled = false;
        }

        private void LoadField()
        {
            if (lbProperties.SelectedItem == null) return;
            short id = Convert.ToInt16(lbProperties.SelectedItem.ToString(), 16);

            object field = GetField(id, entity);
            if (field is EntityVictimProperty victimProperty)
            {
                selectedField = victimProperty;
            }
            else if (field is EntityInt32Property int32Property)
            {
                selectedField = int32Property;
            }
            else if (field is EntityUInt32Property uint32Property)
            {
                selectedField = uint32Property;
            }
            else if (field is EntitySettingProperty entitySettingProperty)
            {
                selectedField = entitySettingProperty;
            }
            else
            {
                selectedField = null!;
            }
            Console.WriteLine(selectedField);

            lblUnsupportedProperty.Visible = field == null;
        }

        private void UpdatePropertyHeader()
        {
            if (lbProperties.SelectedItem == null) return;
            short id = Convert.ToInt16(lbProperties.SelectedItem.ToString(), 16);
            if (entity.KnownProperties.Keys.Contains(id))
            {
                var item = entity.KnownProperties[id];
                {
                    dgvPropertyHeader.Rows.Clear();

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvPropertyHeader, item.Type, item.ElementSize, item.RowCount, item.IsSparse, item.HasMetaValues);
                    dgvPropertyHeader.Rows.Add(row);
                }
                {
                    dgvPropertyRaw.Rows.Clear();

                    byte[] values = item.Save();
                    string result = string.Join(" ", values.Select(b => b.ToString("X2")));

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvPropertyRaw, result);
                    dgvPropertyRaw.Rows.Add(row);
                }
            }
        }

        private void UpdatePropertyMetaValues()
        {
            if (lbProperties.SelectedItem == null) return;

            dgvPropertyMetaValues.Rows.Clear();

            dynamic field = selectedField;
            if (field == null) return;

            foreach (var row in field.Rows)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(dgvPropertyMetaValues, row.MetaValue);
                dgvPropertyMetaValues.Rows.Add(dgvRow);
            }
        }

        private void UpdatePropertyValues()
        {
            if (lbProperties.SelectedItem == null) return;
            dgvPropertyValues.Rows.Clear();

            if (!(dgvPropertyMetaValues.SelectedCells.Count > 0) || !(dgvPropertyMetaValues.Rows.Count > 0)) return;
            int rowIndex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;

            dynamic field = selectedField;
            if (field == null) return;

            if (field.Rows[rowIndex].Values.Count > 0)
            {
                foreach (var value in field.Rows[rowIndex].Values)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    if (field is EntityVictimProperty)
                    {
                        row.CreateCells(dgvPropertyValues, value.VictimID.ToString("X"));
                    }
                    else if (field is EntitySettingProperty)
                    {
                        row.CreateCells(dgvPropertyValues, value.Value);
                    }
                    else if (field is EntityUInt32Property)
                    {
                        long value1 = (value & (0xFF << 0)) >> 0;
                        long value2 = (value & (0xFF << 8)) >> 8;
                        long value3 = (value & (0xFF << 16)) >> 16;
                        long value4 = (value & (0xFF << 24)) >> 24;
                        row.CreateCells(dgvPropertyValues, value.ToString("X"), value1.ToString("X2"), value2.ToString("X2"), value3.ToString("X2"), value4.ToString("X2"));
                    }
                    else
                    {
                        byte byte0 = (byte)(value & 0xFF);
                        byte byte1 = (byte)((value >> 8) & 0xFF);
                        byte byte2 = (byte)((value >> 16) & 0xFF);
                        byte byte3 = (byte)((value >> 24) & 0xFF);
                        row.CreateCells(dgvPropertyValues, value.ToString("X"), byte0.ToString("X2"), byte1.ToString("X2"), byte2.ToString("X2"), byte3.ToString("X2"));

                    }
                    dgvPropertyValues.Rows.Add(row);
                }
            }
        }

        private void dgvPropertyMetaValues_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePropertyValues();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvPropertyMetaValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox_KeyPress;
                textbox.KeyPress += TextBox_KeyPress;
            }
        }

        private void dgvPropertyMetaValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lbProperties.SelectedItem == null || !(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
            short newValue = Convert.ToInt16(dgvPropertyMetaValues.Rows[e.RowIndex].Cells[0].Value);

            dynamic field = selectedField;
            if (field == null) return;

            field.Rows[e.RowIndex].MetaValue = newValue;
            UpdatePropertyHeader();
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                (e.KeyChar < 'A' || e.KeyChar > 'F') &&
                (e.KeyChar < 'a' || e.KeyChar > 'f') &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void dgvPropertyValues_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textbox)
            {
                textbox.KeyPress -= TextBox2_KeyPress;
                textbox.KeyPress += TextBox2_KeyPress;

                if (dgvPropertyValues.CurrentCell.ColumnIndex != 0)
                    textbox.MaxLength = 2;
                else if (selectedField is EntityVictimProperty)
                    textbox.MaxLength = 4;
                else
                    textbox.MaxLength = 8;
            }
        }

        private void dgvPropertyValues_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string inputValue = e.FormattedValue.ToString();
            if (!Regex.IsMatch(inputValue, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                DarkMessageBox.ShowError("Please enter a valid hexadecimal value.", titleInputError);
                dgvPropertyValues.CancelEdit();
                e.Cancel = true;
            }
        }

        private void dgvPropertyValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lbProperties.SelectedItem == null || !(dgvPropertyValues.SelectedCells.Count > 0) || !(dgvPropertyMetaValues.SelectedCells.Count > 0)) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            dynamic field = selectedField;
            if (field == null) return;

            int rowIndex = dgvPropertyMetaValues.SelectedCells[0].RowIndex;
            object cellValue = dgvPropertyValues.SelectedCells[0].Value;

            if (field is EntityVictimProperty)
            {
                field.Rows[rowIndex].Values[e.RowIndex] = new EntityVictim(Convert.ToInt16(cellValue));
            }
            else if (field is EntityInt32Property)
            {
                if (e.ColumnIndex == 0)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] = Convert.ToInt32(cellValue);
                }
                else if (e.ColumnIndex == 1)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFFFF00;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 0);
                }
                else if (e.ColumnIndex == 2)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFF00FF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 8);
                }
                else if (e.ColumnIndex == 3)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFF00FFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 16);
                }
                else if (e.ColumnIndex == 4)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0x00FFFFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (int)((byte)Convert.ToByte(cellValue) << 24);
                }
            }
            else if (field is EntityUInt32Property)
            {
                if (e.ColumnIndex == 0)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] = Convert.ToUInt32(cellValue);
                }
                else if (e.ColumnIndex == 1)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFFFF00;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 0);
                }
                else if (e.ColumnIndex == 2)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFFFF00FF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 8);
                }
                else if (e.ColumnIndex == 3)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0xFF00FFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 16);
                }
                else if (e.ColumnIndex == 4)
                {
                    field.Rows[rowIndex].Values[e.RowIndex] &= 0x00FFFFFF;
                    field.Rows[rowIndex].Values[e.RowIndex] |= (uint)((byte)Convert.ToByte(cellValue) << 24);
                }
            }
            else if (field is EntitySettingProperty)
            {
                field.Rows[rowIndex].Values[e.RowIndex] = new EntitySetting(Convert.ToInt32(cellValue));
            }
            UpdatePropertyHeader();
        }

        private void dgvPropertyValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is uint uintValue)
            {
                e.Value = uintValue.ToString("X");
                e.FormattingApplied = true;
            }
            else if (e.Value is int intValue)
            {
                e.Value = intValue.ToString("X2");
                e.FormattingApplied = true;
            }
            else if(e.Value is short shortValue)
            {
                e.Value = shortValue.ToString("X");
                e.FormattingApplied = true;
            }
            else if (e.Value is byte byteValue)
            {
                e.Value = byteValue.ToString("X2");
                e.FormattingApplied = true;
            }
        }

        private void dgvPropertyValues_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.Value is string inputValue)
            {
                try
                {
                    if (selectedField is EntityVictimProperty)
                    {
                        int parsedValue = Convert.ToInt32(inputValue, 16);

                        if (parsedValue > short.MaxValue)
                        {
                            e.Value = (short)(parsedValue - 0x10000);
                        }
                        else
                        {
                            e.Value = (short)parsedValue;
                        }
                    }
                    else if (selectedField is EntityUInt32Property)
                    {
                        ulong parsedValue = Convert.ToUInt64(inputValue, 16);

                        if (parsedValue > uint.MaxValue)
                        {
                            DarkMessageBox.ShowWarning("The entered value exceeds the range of a 32-bit unsigned integer.", titleInputError);
                            e.Value = uint.MaxValue;
                        }
                        else
                        {
                            e.Value = (uint)parsedValue;
                        }
                    }
                    else
                    {
                        e.Value = Convert.ToInt32(inputValue, 16);
                        e.ParsingApplied = true;
                    }

                    e.ParsingApplied = true;
                }
                catch
                {
                    DarkMessageBox.ShowError("Invalid hex value. Please enter a valid 16-bit hex value.", titleInputError);
                    e.ParsingApplied = false;
                }
            }
        }


        private void lbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadField();
            UpdatePropertyHeader();
            CreatePropertyValuesColumns();
            UpdatePropertyMetaValues();
        }

        private void chkPropertyStyle_CheckedChanged(object sender, EventArgs e)
        {
            propertyStyle = chkPropertyStyle.Checked;
            LoadField();
            UpdatePropertyHeader();
            CreatePropertyValuesColumns();
            UpdatePropertyMetaValues();
        }

        private void cmdAddProperty_Click(object sender, EventArgs e)
        {
            short id = Convert.ToInt16(txtProperty.Text.ToString(), 16);
            if (lbProperties.Items.Contains(txtProperty.Text))
            {
                DarkMessageBox.ShowError($"Duplicated field: {id:X}", titleError);
                return;
            }

            AddField(id);
            lbProperties.Items.Add(id.ToString("X"));
            Console.WriteLine($"Added field: {id:X}");
        }

        private void txtEIDA_LostFocus(object sender, EventArgs e)
        {
            UpdateLoadListA();
        }

        private void txtEIDB_LostFocus(object sender, EventArgs e)
        {
            UpdateLoadListB();
        }

        private void cmdLoadListVerify_Click(object sender, EventArgs e)
        {
            bool haserror = false;
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
                                eidlist += "[position " + i + "] " + Entry.EIDToEName(eid) + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            if (eidlist != string.Empty)
            {
                lblVerifyLoadLists.Visible = false;
                DarkMessageBox.ShowWarning($"Load lists are incorrect. The following entries were already deloaded:\n{eidlist}", "Load list verification exception");
                haserror = true;
            }
            if (loadedentries.Count == 0 && !haserror)
                //DarkMessageBox.ShowMessage("Load lists are correct.", "Load list verification exception.");
                lblVerifyLoadLists.Visible = true;
            else if (loadedentries.Count != 0)
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
                                    eidlist2 += "[position " + i + "] " + Entry.EIDToEName(eid) + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                lblVerifyLoadLists.Visible = false;
                DarkMessageBox.ShowWarning($"Load lists are incorrect. The following entries are never deloaded:\n{eidlist2}", "Load list verification exception");
            }
        }

        private void cmdPayload_Click(object sender, EventArgs e)
        {
            CheckPayload();
        }

        private void CheckPayload()
        {
            List<int> loadedentries = new List<int>();
            for (int i = 0; i < numPayloadPosition.Value + 1; ++i)
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
                                lblVerifyLoadLists.Visible = false;
                                DarkMessageBox.ShowWarning($"Load lists are incorrect. {Entry.EIDToEName(eid)} was already deloaded by position {i}.", "Load list verification exception.");
                                return;
                            }
                        }
                    }
                }
            }
            List<Chunk> chunks = null;
            HashSet<Entry> entries = null;
            chunks = controller.GetNSF().Chunks;
            entries = new HashSet<Entry>();
            foreach (int eid in loadedentries)
            {
                entries.Add(controller.GetEntry<Entry>(eid));
            }
            HashSet<Chunk> loadedchunks = new HashSet<Chunk>();
            HashSet<Chunk> loadedsoundchunks = new HashSet<Chunk>();
            HashSet<Chunk> loadedtexturechunks = new HashSet<Chunk>();
            HashSet<Chunk> loadedwavebankchunks = new HashSet<Chunk>();
            foreach (Chunk chunk in chunks)
            {
                if (chunk is NormalChunk c)
                {
                    foreach (Entry entry in entries)
                    {
                        if (c.Entries.Contains(entry))
                            loadedchunks.Add(chunk);
                    }
                }
                else if (chunk is SoundChunk s)
                {
                    foreach (Entry entry in entries)
                    {
                        if (s.Entries.Contains(entry))
                            loadedsoundchunks.Add(chunk);
                    }
                }
                else if (chunk is TextureChunk t)
                {
                    foreach (Entry entry in entries)
                    {
                        if (loadedentries.Contains(t.EID))
                            loadedtexturechunks.Add(chunk);
                    }
                }
                else if (chunk is WavebankChunk w)
                {
                    foreach (Entry entry in entries)
                    {
                        loadedwavebankchunks.Add(chunk);
                    }
                }
            }
            lblPayload.Visible = true;
            lblPayload.Text = $"Payload is {loadedchunks.Count} normal chunks";
            lblPayloadTexture.Visible = true;
            lblPayloadTexture.Text = $"Payload is {loadedtexturechunks.Count} texture chunks";
            lblPayloadSound.Visible = true;
            lblPayloadSound.Text = $"Payload is {loadedsoundchunks.Count} - {loadedwavebankchunks.Count}\nsound/wavebank chunks";
            if (loadedchunks.Count < 20)
            {
                lblPayload.ForeColor = Color.LimeGreen;
            }
            else if (loadedchunks.Count <= 21)
            {
                lblPayload.ForeColor = Color.Goldenrod;
            }
            else
            {
                lblPayload.ForeColor = Color.Red;
            }

            if (loadedtexturechunks.Count <= 7)
            {
                lblPayloadTexture.ForeColor = Color.LimeGreen;
            }
            else if (loadedtexturechunks.Count == 8)
            {
                lblPayloadTexture.ForeColor = Color.Goldenrod;
            }
            else
            {
                lblPayloadTexture.ForeColor = Color.Red;
            }

            if (loadedsoundchunks.Count + loadedwavebankchunks.Count <= 8)
            {
                lblPayloadSound.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                lblPayloadSound.ForeColor = Color.Red;
            }
        }

        private void cmdVerifyDrawList_Click(object sender, EventArgs e)
        {
            {
                bool haserror = false;
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
                                    idlist += "[position " + i + "] " + (id >> 8 & 0xFFFF) + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                if (idlist != string.Empty)
                {
                    lblVerifyDrawLists.Visible = false;
                    DarkMessageBox.ShowWarning($"Draw lists are incorrect. The following entries were already undrawn:\n{idlist}", "Draw list verification exception");
                    haserror = true;
                }
                if (drawnids.Count == 0 && !haserror)
                    lblVerifyDrawLists.Visible = true;
                else if (drawnids.Count != 0)
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
                                        idlist2 += "[position " + i + "] " + (id >> 8 & 0xFFFF) + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                    lblVerifyDrawLists.Visible = false;
                    DarkMessageBox.ShowWarning($"Draw lists are incorrect. The following entries are never undrawn:\n{idlist2}", "Draw list verification exception");
                }
            }
        }

        private void chkSettingHex_CheckedChanged(object sender, EventArgs e)
        {
            numSettingC.Hexadecimal = chkSettingHex.Checked;
            SetCVal((long)numSettingC.Value);
        }

        private void cmdInterpolate_Click(object sender, EventArgs e)
        {
            Position[] pos = new Position[entity.Positions.Count];
            for (int i = 0; i < entity.Positions.Count; ++i)
            {
                pos[i] = new Position(entity.Positions[i].X, entity.Positions[i].Y, entity.Positions[i].Z);
            }
            using (InterpolatorForm interpolator = new InterpolatorForm(pos))
            {
                if (interpolator.ShowDialog() == DialogResult.OK)
                {
                    for (int m = interpolator.Start - 1, i = interpolator.End - 2; i > m; --i)
                    {
                        entity.Positions.RemoveAt(i);
                    }
                    for (int i = 0; i < interpolator.Amount; ++i)
                    {
                        entity.Positions.Insert(i + interpolator.Start, new EntityPosition(interpolator.NewPositions[i + 1]));
                    }
                    UpdatePosition();
                }
            }
        }

        private void UpdateTTReward()
        {
            if (entity.TimeTrialReward.HasValue)
            {
                numTTReward.Value = entity.TimeTrialReward.Value >> 8;
            }
            numTTReward.Enabled = entity.TimeTrialReward.HasValue;
            chkTTReward.Checked = entity.TimeTrialReward.HasValue;
        }

        private void chkTTReward_CheckedChanged(object sender, EventArgs e)
        {
            numTTReward.Enabled = chkTTReward.Checked;
            if (chkTTReward.Checked)
            {
                entity.TimeTrialReward = (int)numTTReward.Value << 8;
            }
            else
            {
                entity.TimeTrialReward = null;
            }
        }

        private void numTTReward_ValueChanged(object sender, EventArgs e)
        {
            entity.TimeTrialReward = (int)numTTReward.Value << 8;
        }

        // C2-tweaked
        private void UpdateC2TTSets()
        {
            if (Settings.Default.EnableC2TTEditor)
            {
                fraC2TTSet.Visible = true;
                fraC2TTSet.Location = new Point(245, 146);
                fraZMod.Location = new Point(384, 146);
                UpdateC2TTType();
                UpdateC2TTYRot();
                UpdateC2TTBoxFlag();
                UpdateC2TTGhostTarget();
            }
            else
            {
                fraC2TTSet.Visible = false;
                fraC2TTSet.Location = new Point(245, 146);
                fraZMod.Location = new Point(245, 146);
            }
        }

        private void UpdateC2TTType()
        {
            if (entity.C2TTType.HasValue)
            {
                numC2TTType.Value = entity.C2TTType.Value >> 8;
            }
            numC2TTType.Enabled = entity.C2TTType.HasValue;
            chkC2TTType.Checked = entity.C2TTType.HasValue;
        }

        private void chkC2TTType_CheckedChanged(object sender, EventArgs e)
        {
            numC2TTType.Enabled = chkC2TTType.Checked;
            if (chkC2TTType.Checked)
            {
                entity.C2TTType = (int)numC2TTType.Value << 8;
            }
            else
            {
                entity.C2TTType = null;
            }
        }

        private void numC2TTType_ValueChanged(object sender, EventArgs e)
        {
            entity.C2TTType = (int)numC2TTType.Value << 8;
        }

        private void UpdateC2TTYRot()
        {
            if (entity.C2TTYRot.HasValue)
            {
                numC2TTYRot.Value = entity.C2TTYRot.Value >> 8;
            }
            numC2TTYRot.Enabled = entity.C2TTYRot.HasValue;
            chkC2TTYRot.Checked = entity.C2TTYRot.HasValue;
        }

        private void chkC2TTYRot_CheckedChanged(object sender, EventArgs e)
        {
            numC2TTYRot.Enabled = chkC2TTYRot.Checked;
            if (chkC2TTYRot.Checked)
            {
                entity.C2TTYRot = (int)numC2TTYRot.Value << 8;
            }
            else
            {
                entity.C2TTYRot = null;
            }
        }

        private void numC2TTYRot_ValueChanged(object sender, EventArgs e)
        {
            entity.C2TTYRot = (int)numC2TTYRot.Value << 8;
        }

        private void UpdateC2TTBoxFlag()
        {
            if (entity.C2TTBoxFlag.HasValue)
            {
                numC2TTFlags.Value = entity.C2TTBoxFlag.Value >> 8;
            }
            numC2TTFlags.Enabled = entity.C2TTBoxFlag.HasValue;
            chkC2TTFlags.Checked = entity.C2TTBoxFlag.HasValue;
        }

        private void chkC2TTFlags_CheckedChanged(object sender, EventArgs e)
        {
            numC2TTFlags.Enabled = chkC2TTFlags.Checked;
            if (chkC2TTFlags.Checked)
            {
                entity.C2TTBoxFlag = (int)numC2TTFlags.Value << 8;
            }
            else
            {
                entity.C2TTBoxFlag = null;
            }
        }

        private void numC2TTFlags_ValueChanged(object sender, EventArgs e)
        {
            entity.C2TTBoxFlag = (int)numC2TTFlags.Value << 8;
        }

        private void UpdateC2TTGhostTarget()
        {
            if (entity.C2TTGhostTarget.HasValue)
            {
                numC2TTGhostTarget.Value = entity.C2TTGhostTarget.Value >> 8;
            }
            numC2TTGhostTarget.Enabled = entity.C2TTGhostTarget.HasValue;
            chkC2TTGhostTarget.Checked = entity.C2TTGhostTarget.HasValue;
        }

        private void chkC2TTGhostTarget_CheckedChanged(object sender, EventArgs e)
        {
            numC2TTGhostTarget.Enabled = chkC2TTGhostTarget.Checked;
            if (chkC2TTGhostTarget.Checked)
            {
                entity.C2TTGhostTarget = (int)numC2TTGhostTarget.Value << 8;
            }
            else
            {
                entity.C2TTGhostTarget = null;
            }
        }

        private void numC2TTGhostTarget_ValueChanged(object sender, EventArgs e)
        {
            entity.C2TTGhostTarget = (int)numC2TTGhostTarget.Value << 8;
        }

        private void UpdateCameraIndex()
        {
            if (entity.CameraIndex.HasValue)
            {
                numCameraIndex.Value = entity.CameraIndex.Value;
            }
            numCameraIndex.Enabled = entity.CameraIndex.HasValue;
            chkCameraIndex.Checked = entity.CameraIndex.HasValue;
        }

        private void UpdateCameraSubIndex()
        {
            if (entity.CameraSubIndex.HasValue)
            {
                numCameraSubIndex.Value = entity.CameraSubIndex.Value;
            }
            numCameraSubIndex.Enabled = entity.CameraSubIndex.HasValue;
            chkCameraSubIndex.Checked = entity.CameraSubIndex.HasValue;
        }

        private void numCameraIndex_ValueChanged(object sender, EventArgs e)
        {
            entity.CameraIndex = (int)numCameraIndex.Value;
        }

        private void chkCameraIndex_CheckedChanged(object sender, EventArgs e)
        {
            numCameraIndex.Enabled = chkCameraIndex.Checked;
            if (chkCameraIndex.Checked)
                entity.CameraIndex = (int)numCameraIndex.Value;
            else
                entity.CameraIndex = null;
        }

        private void numCameraSubIndex_ValueChanged(object sender, EventArgs e)
        {
            entity.CameraSubIndex = (int)numCameraSubIndex.Value;
        }

        private void chkCameraSubIndex_CheckedChanged(object sender, EventArgs e)
        {
            numCameraSubIndex.Enabled = chkCameraSubIndex.Checked;
            if (chkCameraSubIndex.Checked)
                entity.CameraSubIndex = (int)numCameraSubIndex.Value;
            else
                entity.CameraSubIndex = null;
        }

        private void UpdateMode()
        {
            //if (entity.Mode.HasValue)
            //{
            //    numMode.Value = entity.Mode.Value;
            //}
            //numMode.Enabled = entity.Mode.HasValue;
            //chkMode.Checked = entity.Mode.HasValue;
        }

        private void numMode_ValueChanged(object sender, EventArgs e)
        {
            //entity.Mode = (byte)numMode.Value;
        }

        private void chkMode_CheckedChanged(object sender, EventArgs e)
        {
            //numMode.Enabled = chkMode.Checked;
            //if (chkMode.Checked)
            //    entity.Mode = (byte)numMode.Value;
            //else
            //    entity.Mode = null;
        }

        private void UpdateAvgDist()
        {
            if (entity.AverageDistance.HasValue)
            {
                numAvgDist.Value = entity.AverageDistance.Value.ValueB;
            }
            numAvgDist.Enabled = entity.AverageDistance.HasValue;
            chkAvgDist.Checked = entity.AverageDistance.HasValue;
        }

        private void numAvgDist_ValueChanged(object sender, EventArgs e)
        {
            entity.AverageDistance = new EntitySetting(0, (int)numAvgDist.Value);
        }

        private void chkAvgDist_CheckedChanged(object sender, EventArgs e)
        {
            numAvgDist.Enabled = chkAvgDist.Checked;
            if (chkAvgDist.Checked)
                entity.AverageDistance = new EntitySetting(0, (int)numAvgDist.Value);
            else
                entity.AverageDistance = null;
        }

        private void UpdateNeighbors()
        {
            if (entity.Neighbors != null && entity.Neighbors.RowCount != 0)
            {
                if (neighborindex >= entity.Neighbors.RowCount)
                    neighborindex = entity.Neighbors.RowCount - 1;
                numNeighborPosition.Value = entity.Neighbors.Rows[neighborindex].MetaValue.Value;
                lblNeighbor.Text = $"{neighborindex + 1} / {entity.Neighbors.RowCount}";
                cmdPrevNeighbor.Enabled = neighborindex > 0;
                cmdNextNeighbor.Enabled = neighborindex + 1 < entity.Neighbors.RowCount;
                lblNeighborPosition.Enabled =
                numNeighborPosition.Enabled =
                cmdRemoveNeighbor.Enabled = true;
                cmdInsertNeighborSetting.Enabled = true;
                neighborsettingindex = Math.Min(entity.Neighbors.Rows[neighborindex].Values.Count - 1, neighborsettingindex);
                if (entity.Neighbors.Rows[neighborindex].Values.Count > 0)
                {
                    lblNeighborSetting.Text = $"{neighborsettingindex + 1} / {entity.Neighbors.Rows[neighborindex].Values.Count}";
                    cmdPrevNeighborSetting.Enabled = neighborsettingindex > 0;
                    cmdNextNeighborSetting.Enabled = neighborsettingindex + 1 < entity.Neighbors.Rows[neighborindex].Values.Count;
                    cmdRemoveNeighborSetting.Enabled =
                    numNeighborFlag.Enabled =
                    numNeighborZone.Enabled =
                    numNeighborCamera.Enabled =
                    numNeighborLink.Enabled =
                    lblNeighborFlag.Enabled =
                    lblNeighborZone.Enabled =
                    lblNeighborCamera.Enabled =
                    lblNeighborLink.Enabled = true;
                    numNeighborFlag.Value = (entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] & (0xFF << 0)) >> 0;
                    numNeighborCamera.Value = (entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] & (0xFF << 8)) >> 8;
                    numNeighborZone.Value = (entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] & (0xFF << 16)) >> 16;
                    numNeighborLink.Value = (entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] & (0xFF << 24)) >> 24;
                }
                else
                {
                    lblNeighborSetting.Text = "-- / --";
                    cmdPrevNeighborSetting.Enabled =
                    cmdNextNeighborSetting.Enabled =
                    cmdRemoveNeighborSetting.Enabled =
                    numNeighborFlag.Enabled =
                    numNeighborZone.Enabled =
                    numNeighborCamera.Enabled =
                    numNeighborLink.Enabled =
                    lblNeighborFlag.Enabled =
                    lblNeighborZone.Enabled =
                    lblNeighborCamera.Enabled =
                    lblNeighborLink.Enabled = false;
                }
            }
            else
            {
                entity.Neighbors = null;
                lblNeighbor.Text = "-- / --";
                lblNeighborSetting.Text = "-- / --";
                lblNeighborPosition.Enabled =
                numNeighborPosition.Enabled =
                cmdPrevNeighbor.Enabled =
                cmdNextNeighbor.Enabled =
                cmdRemoveNeighbor.Enabled =
                cmdInsertNeighborSetting.Enabled =
                cmdPrevNeighborSetting.Enabled =
                cmdNextNeighborSetting.Enabled =
                cmdRemoveNeighborSetting.Enabled =
                numNeighborFlag.Enabled =
                numNeighborZone.Enabled =
                numNeighborCamera.Enabled =
                numNeighborLink.Enabled =
                lblNeighborFlag.Enabled =
                lblNeighborZone.Enabled =
                lblNeighborCamera.Enabled =
                lblNeighborLink.Enabled = false;
            }
        }

        private void cmdNextNeighbor_Click(object sender, EventArgs e)
        {
            ++neighborindex;
            UpdateNeighbors();
        }

        private void cmdPrevNeighbor_Click(object sender, EventArgs e)
        {
            --neighborindex;
            UpdateNeighbors();
        }

        private void cmdRemoveNeighbor_Click(object sender, EventArgs e)
        {
            entity.Neighbors.Rows.RemoveAt(neighborindex);
            UpdateNeighbors();
        }

        private void cmdInsertNeighbor_Click(object sender, EventArgs e)
        {
            if (entity.Neighbors == null || entity.Neighbors.Rows.Count == 0)
            {
                entity.Neighbors = new EntityUInt32Property();
                entity.Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                entity.Neighbors.Rows[entity.Neighbors.RowCount - 1].MetaValue = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<uint>();
                newrow.MetaValue = entity.Neighbors.Rows[neighborindex].MetaValue;
                foreach (var val in entity.Neighbors.Rows[neighborindex].Values)
                    newrow.Values.Add(val);
                entity.Neighbors.Rows.Insert(neighborindex, newrow);
            }
            UpdateNeighbors();
        }

        private void numNeighborFlag_ValueChanged(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)numNeighborFlag.Value << 0);
        }

        private void numNeighborCamera_ValueChanged(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)numNeighborCamera.Value << 8);
        }

        private void numNeighborZone_ValueChanged(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)numNeighborZone.Value << 16);
        }

        private void numNeighborLink_ValueChanged(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
            entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)numNeighborLink.Value << 24);
        }

        private void numNeighborPosition_ValueChanged(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].MetaValue = (short)numNeighborPosition.Value;
        }

        private void cmdPrevNeighborSetting_Click(object sender, EventArgs e)
        {
            --neighborsettingindex;
            UpdateNeighbors();
        }

        private void cmdNextNeighborSetting_Click(object sender, EventArgs e)
        {
            ++neighborsettingindex;
            UpdateNeighbors();
        }

        private void cmdRemoveNeighborSetting_Click(object sender, EventArgs e)
        {
            entity.Neighbors.Rows[neighborindex].Values.RemoveAt(neighborsettingindex);
            UpdateNeighbors();
        }

        private void cmdInsertNeighborSetting_Click(object sender, EventArgs e)
        {
            if (entity.Neighbors.Rows[neighborindex].Values.Count == 0)
                entity.Neighbors.Rows[neighborindex].Values.Add(0);
            else
                entity.Neighbors.Rows[neighborindex].Values.Insert(neighborsettingindex, entity.Neighbors.Rows[neighborindex].Values[neighborsettingindex]);
            UpdateNeighbors();
        }

        private void UpdateZMod()
        {
            if (entity.ZMod.HasValue)
            {
                numZMod.Value = entity.ZMod.Value;
            }
            numZMod.Enabled = entity.ZMod.HasValue;
            chkZMod.Checked = entity.ZMod.HasValue;
        }

        private void chkZMod_CheckedChanged(object sender, EventArgs e)
        {
            numZMod.Enabled = chkZMod.Checked;
            if (chkZMod.Checked)
            {
                entity.ZMod = (int)numZMod.Value;
            }
            else
            {
                entity.ZMod = null;
            }
        }

        private void numZMod_ValueChanged(object sender, EventArgs e)
        {
            entity.ZMod = (int)numZMod.Value;
        }

        private void UpdateFOV()
        {
            if (entity.FOV != null && entity.FOV.RowCount != 0)
            {
                if (fovframeindex >= entity.FOV.RowCount)
                    fovframeindex = entity.FOV.RowCount - 1;
                lblFOVPosition.Enabled = true;
                numFOVPosition.Enabled = true;
                numFOVPosition.Value = entity.FOV.Rows[fovframeindex].MetaValue.Value;
                lblFOVFrame.Text = $"{fovframeindex + 1} / {entity.FOV.RowCount}";
                cmdPrevFOVFrame.Enabled = fovframeindex > 0;
                cmdNextFOVFrame.Enabled = fovframeindex + 1 < entity.FOV.RowCount;
                cmdRemoveFOVFrame.Enabled = true;
                if (entity.FOV.Rows[fovframeindex].Values.Count > 0)
                {
                    if (fovindex >= entity.FOV.Rows[fovframeindex].Values.Count)
                        fovindex = entity.FOV.Rows[fovframeindex].Values.Count - 1;
                    cmdInsertFOV.Enabled = true;
                    cmdRemoveFOV.Enabled = true;
                    lblFOV.Enabled = true;
                    numFOV.Enabled = true;
                    cmdPrevFOV.Enabled = fovindex > 0;
                    cmdNextFOV.Enabled = fovindex + 1 < entity.FOV.Rows[fovframeindex].Values.Count;
                    lblFOVIndex.Text = $"{fovindex + 1} / {entity.FOV.Rows[fovframeindex].Values.Count}";
                    numFOV.Value = entity.FOV.Rows[fovframeindex].Values[fovindex].VictimID;
                }
                else
                {
                    cmdInsertFOV.Enabled = false;
                    cmdRemoveFOV.Enabled = false;
                    lblFOV.Enabled = false;
                    numFOV.Enabled = false;
                    cmdPrevFOV.Enabled = false;
                    cmdNextFOV.Enabled = false;
                    lblFOVIndex.Text = "-- / --";
                }
            }
            else
            {
                entity.FOV = null;
                lblFOVFrame.Text = "-- / --";
                lblFOVIndex.Text = "-- / --";
                lblFOVPosition.Enabled = false;
                cmdPrevFOVFrame.Enabled = false;
                cmdNextFOVFrame.Enabled = false;
                cmdRemoveFOVFrame.Enabled = false;
                lblFOV.Enabled = false;
                numFOV.Enabled = false;
                cmdPrevFOV.Enabled = false;
                cmdNextFOV.Enabled = false;
                cmdRemoveFOV.Enabled = false;
                cmdInsertFOV.Enabled = false;
            }
        }

        private void cmdPrevFOVFrame_Click(object sender, EventArgs e)
        {
            --fovframeindex;
            UpdateFOV();
        }

        private void cmdNextFOVFrame_Click(object sender, EventArgs e)
        {
            ++fovframeindex;
            UpdateFOV();
        }

        private void cmdRemoveFOVFrame_Click(object sender, EventArgs e)
        {
            entity.FOV.Rows.RemoveAt(fovframeindex);
            UpdateFOV();
        }

        private void cmdInsertFOVFrame_Click(object sender, EventArgs e)
        {
            if (entity.FOV == null || entity.FOV.Rows.Count == 0)
            {
                entity.FOV = new EntityVictimProperty();
                entity.FOV.Rows.Add(new EntityPropertyRow<EntityVictim>());
                entity.FOV.Rows[entity.FOV.RowCount - 1].MetaValue = 0;
            }
            else
            {
                var newrow = new EntityPropertyRow<EntityVictim>();
                newrow.MetaValue = entity.FOV.Rows[fovframeindex].MetaValue;
                foreach (var val in entity.FOV.Rows[fovframeindex].Values)
                    newrow.Values.Add(val);
                entity.FOV.Rows.Insert(fovframeindex, newrow);
            }
            UpdateFOV();
        }

        private void cmdPrevFOV_Click(object sender, EventArgs e)
        {
            --fovindex;
            UpdateFOV();
        }

        private void cmdNextFOV_Click(object sender, EventArgs e)
        {
            ++fovindex;
            UpdateFOV();
        }

        private void cmdRemoveFOV_Click(object sender, EventArgs e)
        {
            entity.FOV.Rows[fovframeindex].Values.RemoveAt(fovindex);
            UpdateFOV();
        }

        private void cmdInsertFOV_Click(object sender, EventArgs e)
        {
            if (entity.FOV.Rows[fovframeindex].Values.Count == 0)
                entity.FOV.Rows[fovframeindex].Values.Add(new EntityVictim());
            else
                entity.FOV.Rows[fovframeindex].Values.Insert(fovindex, entity.FOV.Rows[fovframeindex].Values[fovindex]);
            UpdateFOV();
        }

        private void numFOVPosition_ValueChanged(object sender, EventArgs e)
        {
            entity.FOV.Rows[fovframeindex].MetaValue = (short)numFOVPosition.Value;
        }

        private void numFOV_ValueChanged(object sender, EventArgs e)
        {
            entity.FOV.Rows[fovframeindex].Values[fovindex] = new EntityVictim((short)numFOV.Value);
        }



        private void EnableDoubleBuffering(DataGridView dataGridView)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dataGridView, new object[] { true });
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            Color clrBackground = Color.FromArgb(40, 40, 40);
            Color clrAltBackground = Color.FromArgb(34, 34, 34);
            Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
            Color clrText = Color.Gainsboro;

            // Background color of the entire grid
            dataGridView.BackgroundColor = Color.FromArgb(31, 31, 32);

            // Color of the grid lines
            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = clrBackground;
            dataGridView.DefaultCellStyle.ForeColor = clrText;
            dataGridView.DefaultCellStyle.SelectionBackColor = clrSelectionBackground;
            dataGridView.DefaultCellStyle.SelectionForeColor = clrText;

            // Style for column headers
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Style for row headers
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = clrText;

            // Background color for odd and even rows
            dataGridView.RowsDefaultCellStyle.BackColor = clrBackground;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = clrAltBackground;

            // Row border style
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header and gridline styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Additional settings
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }


    }
}
