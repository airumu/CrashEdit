using System.Windows.Forms;
using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    partial class EntityBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            KillForm();
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chkType = new CheckBox();
            numType = new DarkNumericUpDown();
            fraType = new DarkGroupBox();
            chkSubtype = new CheckBox();
            numSubtype = new DarkNumericUpDown();
            fraPosition = new DarkGroupBox();
            cmdSyncEntities = new DarkButton();
            chkSyncEntities = new CheckBox();
            chkSyncPositions = new CheckBox();
            cmdEditPath = new DarkButton();
            lblPositionIndex = new Label();
            cmdNextPosition = new DarkButton();
            cmdPreviousPosition = new DarkButton();
            cmdInsertPosition = new DarkButton();
            lblZ = new Label();
            cmdRemovePosition = new DarkButton();
            lblY = new Label();
            cmdAppendPosition = new DarkButton();
            lblX = new Label();
            numZ = new DarkNumericUpDown();
            numY = new DarkNumericUpDown();
            numX = new DarkNumericUpDown();
            cmdInsertVictim = new DarkButton();
            fraID = new DarkGroupBox();
            chkID2 = new CheckBox();
            numID2 = new DarkNumericUpDown();
            chkID = new CheckBox();
            numID = new DarkNumericUpDown();
            fraSettings = new DarkGroupBox();
            cmdPasteSetting = new DarkButton();
            cmdCopySetting = new DarkButton();
            lblSettingB = new Label();
            lblSettingA = new Label();
            lblArgAs = new Label();
            chkSettingHex = new CheckBox();
            numSettingC = new DarkNumericUpDown();
            lblSettingIndex = new Label();
            cmdNextSetting = new DarkButton();
            cmdPreviousSetting = new DarkButton();
            cmdAddSetting = new DarkButton();
            cmdRemoveSetting = new DarkButton();
            numSettingB = new DarkNumericUpDown();
            numSettingA = new DarkNumericUpDown();
            fraName = new DarkGroupBox();
            txtName = new DarkTextBox();
            chkName = new CheckBox();
            tbcTabs = new MetroSetTabControl();
            tabGeneral = new TabPage();
            fraC2TTSet = new DarkGroupBox();
            fraC2TTGhostTarget = new DarkGroupBox();
            numC2TTGhostTarget = new DarkNumericUpDown();
            chkC2TTGhostTarget = new CheckBox();
            fraC2TTFlags = new DarkGroupBox();
            numC2TTFlags = new DarkNumericUpDown();
            chkC2TTFlags = new CheckBox();
            fraC2TTYRot = new DarkGroupBox();
            numC2TTYRot = new DarkNumericUpDown();
            chkC2TTYRot = new CheckBox();
            fraC2TTType = new DarkGroupBox();
            numC2TTType = new DarkNumericUpDown();
            chkC2TTType = new CheckBox();
            fraZMod = new DarkGroupBox();
            chkZMod = new CheckBox();
            numZMod = new DarkNumericUpDown();
            tabSpecial = new TabPage();
            fraTTReward = new DarkGroupBox();
            chkTTReward = new CheckBox();
            numTTReward = new DarkNumericUpDown();
            fraOtherSettings = new DarkGroupBox();
            chkOtherSettings = new CheckBox();
            numOtherSettings = new DarkNumericUpDown();
            fraScaling = new DarkGroupBox();
            chkScaling = new CheckBox();
            numScaling = new DarkNumericUpDown();
            fraDDASection = new DarkGroupBox();
            chkDDASection = new CheckBox();
            numDDASection = new DarkNumericUpDown();
            fraDDASettings = new DarkGroupBox();
            chkDDASettings = new CheckBox();
            numDDASettings = new DarkNumericUpDown();
            fraDrawOverrides = new DarkGroupBox();
            chkDrawOverrideId = new CheckBox();
            numDrawOverrideId = new DarkNumericUpDown();
            chkDrawOverrideMult = new CheckBox();
            numDrawOverrideMult = new DarkNumericUpDown();
            fraBoxCount = new DarkGroupBox();
            chkBonusBoxCount = new CheckBox();
            numBonusBoxCount = new DarkNumericUpDown();
            chkBoxCount = new CheckBox();
            numBoxCount = new DarkNumericUpDown();
            fraVictims = new DarkGroupBox();
            numEditVictimID = new DarkNumericUpDown();
            lbVictimID = new DarkListBox();
            cmdClearAllVictims = new DarkButton();
            cmdRemoveVictim = new DarkButton();
            lblVictimIndex = new Label();
            tabCamera = new TabPage();
            fraFOV = new DarkGroupBox();
            lblFOVPosition = new Label();
            cmdRemoveFOVFrame = new DarkButton();
            cmdInsertFOVFrame = new DarkButton();
            numFOVPosition = new DarkNumericUpDown();
            lblFOVFrame = new Label();
            fraFOVFrame = new DarkGroupBox();
            lblFOVIndex = new Label();
            cmdRemoveFOV = new DarkButton();
            cmdInsertFOV = new DarkButton();
            cmdPrevFOV = new DarkButton();
            cmdNextFOV = new DarkButton();
            lblFOV = new Label();
            numFOV = new DarkNumericUpDown();
            cmdPrevFOVFrame = new DarkButton();
            cmdNextFOVFrame = new DarkButton();
            fraNeighbor = new DarkGroupBox();
            lblNeighborPosition = new Label();
            cmdRemoveNeighbor = new DarkButton();
            cmdInsertNeighbor = new DarkButton();
            numNeighborPosition = new DarkNumericUpDown();
            lblNeighbor = new Label();
            fraNeighborSetting = new DarkGroupBox();
            lblNeighborSetting = new Label();
            cmdRemoveNeighborSetting = new DarkButton();
            cmdInsertNeighborSetting = new DarkButton();
            cmdPrevNeighborSetting = new DarkButton();
            cmdNextNeighborSetting = new DarkButton();
            lblNeighborLink = new Label();
            lblNeighborFlag = new Label();
            numNeighborFlag = new DarkNumericUpDown();
            lblNeighborCamera = new Label();
            lblNeighborZone = new Label();
            numNeighborLink = new DarkNumericUpDown();
            numNeighborCamera = new DarkNumericUpDown();
            numNeighborZone = new DarkNumericUpDown();
            cmdPrevNeighbor = new DarkButton();
            cmdNextNeighbor = new DarkButton();
            fraAvgDist = new DarkGroupBox();
            chkAvgDist = new CheckBox();
            numAvgDist = new DarkNumericUpDown();
            fraMode = new DarkGroupBox();
            chkMode = new CheckBox();
            numMode = new DarkNumericUpDown();
            fraCameraSubIndex = new DarkGroupBox();
            chkCameraSubIndex = new CheckBox();
            numCameraSubIndex = new DarkNumericUpDown();
            fraCameraIndex = new DarkGroupBox();
            chkCameraIndex = new CheckBox();
            numCameraIndex = new DarkNumericUpDown();
            fraSLST = new DarkGroupBox();
            lblEIDErr1 = new Label();
            txtSLST = new DarkTextBox();
            chkSLST = new CheckBox();
            tabLoadLists = new TabPage();
            lblEIDErrB = new Label();
            fraLoadListPayload = new DarkGroupBox();
            lblPayloadSound = new Label();
            lblPayloadTexture = new Label();
            lblVerifyLoadLists = new Label();
            lblPayload = new Label();
            cmdLoadListVerify = new DarkButton();
            cmdPayload = new DarkButton();
            lblPayloadPosition = new Label();
            numPayloadPosition = new DarkNumericUpDown();
            fraLoadListB = new DarkGroupBox();
            lblMetavalueLoadB = new Label();
            cmdRemoveRowB = new DarkButton();
            cmdInsertRowB = new DarkButton();
            numMetavalueLoadB = new DarkNumericUpDown();
            lblLoadListRowIndexB = new Label();
            fraEIDB = new DarkGroupBox();
            lbEIDB = new DarkListBox();
            txtEIDB = new DarkTextBox();
            lblEIDIndexB = new Label();
            cmdAppendEIDB = new DarkButton();
            cmdRemoveEIDB = new DarkButton();
            cmdInsertEIDB = new DarkButton();
            cmdPrevRowB = new DarkButton();
            cmdNextRowB = new DarkButton();
            lblEIDErrA = new Label();
            fraLoadListA = new DarkGroupBox();
            lblMetavalueLoadA = new Label();
            cmdRemoveRowA = new DarkButton();
            cmdInsertRowA = new DarkButton();
            numMetavalueLoadA = new DarkNumericUpDown();
            lblLoadListRowIndexA = new Label();
            fraEIDA = new DarkGroupBox();
            lbEIDA = new DarkListBox();
            txtEIDA = new DarkTextBox();
            lblEIDIndexA = new Label();
            cmdAppendEIDA = new DarkButton();
            cmdRemoveEIDA = new DarkButton();
            cmdInsertEIDA = new DarkButton();
            cmdPrevRowA = new DarkButton();
            cmdNextRowA = new DarkButton();
            tabDrawLists = new TabPage();
            fraVerifyDrawList = new DarkGroupBox();
            lblVerifyDrawLists = new Label();
            cmdVerifyDrawList = new DarkButton();
            fraDrawListB = new DarkGroupBox();
            lblMetavalueDrawB = new Label();
            cmdRemoveRowDrawB = new DarkButton();
            cmdInsertRowDrawB = new DarkButton();
            numMetavalueDrawB = new DarkNumericUpDown();
            lblDrawListRowIndexB = new Label();
            fraEntityB = new DarkGroupBox();
            lbEntityB = new DarkListBox();
            numEntityB = new DarkNumericUpDown();
            lblEntityIndexB = new Label();
            cmdAppendEntityB = new DarkButton();
            cmdRemoveEntityB = new DarkButton();
            cmdInsertEntityB = new DarkButton();
            cmdPrevRowDrawB = new DarkButton();
            cmdNextRowDrawB = new DarkButton();
            fraDrawListA = new DarkGroupBox();
            lblMetavalueDrawA = new Label();
            cmdRemoveRowDrawA = new DarkButton();
            cmdInsertRowDrawA = new DarkButton();
            numMetavalueDrawA = new DarkNumericUpDown();
            lblDrawListRowIndexA = new Label();
            fraEntityA = new DarkGroupBox();
            lbEntityA = new DarkListBox();
            numEntityA = new DarkNumericUpDown();
            lblEntityIndexA = new Label();
            cmdAppendEntityA = new DarkButton();
            cmdRemoveEntityA = new DarkButton();
            cmdInsertEntityA = new DarkButton();
            cmdPrevRowDrawA = new DarkButton();
            cmdNextRowDrawA = new DarkButton();
            ((System.ComponentModel.ISupportInitialize)numType).BeginInit();
            fraType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSubtype).BeginInit();
            fraPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            fraID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numID2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numID).BeginInit();
            fraSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSettingC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSettingB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSettingA).BeginInit();
            fraName.SuspendLayout();
            tbcTabs.SuspendLayout();
            tabGeneral.SuspendLayout();
            fraC2TTSet.SuspendLayout();
            fraC2TTGhostTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTGhostTarget).BeginInit();
            fraC2TTFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTFlags).BeginInit();
            fraC2TTYRot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTYRot).BeginInit();
            fraC2TTType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTType).BeginInit();
            fraZMod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZMod).BeginInit();
            tabSpecial.SuspendLayout();
            fraTTReward.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTTReward).BeginInit();
            fraOtherSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numOtherSettings).BeginInit();
            fraScaling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaling).BeginInit();
            fraDDASection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASection).BeginInit();
            fraDDASettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASettings).BeginInit();
            fraDrawOverrides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideMult).BeginInit();
            fraBoxCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBonusBoxCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBoxCount).BeginInit();
            fraVictims.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEditVictimID).BeginInit();
            tabCamera.SuspendLayout();
            fraFOV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFOVPosition).BeginInit();
            fraFOVFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFOV).BeginInit();
            fraNeighbor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNeighborPosition).BeginInit();
            fraNeighborSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numNeighborFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborLink).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborCamera).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborZone).BeginInit();
            fraAvgDist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAvgDist).BeginInit();
            fraMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).BeginInit();
            fraCameraSubIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCameraSubIndex).BeginInit();
            fraCameraIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCameraIndex).BeginInit();
            fraSLST.SuspendLayout();
            tabLoadLists.SuspendLayout();
            fraLoadListPayload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPayloadPosition).BeginInit();
            fraLoadListB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueLoadB).BeginInit();
            fraEIDB.SuspendLayout();
            fraLoadListA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueLoadA).BeginInit();
            fraEIDA.SuspendLayout();
            tabDrawLists.SuspendLayout();
            fraVerifyDrawList.SuspendLayout();
            fraDrawListB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueDrawB).BeginInit();
            fraEntityB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEntityB).BeginInit();
            fraDrawListA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueDrawA).BeginInit();
            fraEntityA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEntityA).BeginInit();
            SuspendLayout();
            // 
            // chkType
            // 
            chkType.AutoSize = true;
            chkType.BackColor = Color.Transparent;
            chkType.Location = new Point(7, 22);
            chkType.Margin = new Padding(4, 3, 4, 3);
            chkType.Name = "chkType";
            chkType.Size = new Size(68, 19);
            chkType.TabIndex = 0;
            chkType.Text = "Enabled";
            chkType.UseVisualStyleBackColor = false;
            chkType.CheckedChanged += chkType_CheckedChanged;
            // 
            // numType
            // 
            numType.Location = new Point(7, 48);
            numType.Margin = new Padding(4, 3, 4, 3);
            numType.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numType.Name = "numType";
            numType.Size = new Size(117, 23);
            numType.TabIndex = 1;
            numType.ValueChanged += numType_ValueChanged;
            // 
            // fraType
            // 
            fraType.Controls.Add(chkSubtype);
            fraType.Controls.Add(numSubtype);
            fraType.Controls.Add(chkType);
            fraType.Controls.Add(numType);
            fraType.Location = new Point(384, 3);
            fraType.Margin = new Padding(4, 3, 4, 3);
            fraType.Name = "fraType";
            fraType.Padding = new Padding(4, 3, 4, 3);
            fraType.Size = new Size(132, 137);
            fraType.TabIndex = 4;
            fraType.TabStop = false;
            fraType.Text = "Type & Subtype";
            // 
            // chkSubtype
            // 
            chkSubtype.AutoSize = true;
            chkSubtype.BackColor = Color.Transparent;
            chkSubtype.Location = new Point(7, 77);
            chkSubtype.Margin = new Padding(4, 3, 4, 3);
            chkSubtype.Name = "chkSubtype";
            chkSubtype.Size = new Size(68, 19);
            chkSubtype.TabIndex = 0;
            chkSubtype.Text = "Enabled";
            chkSubtype.UseVisualStyleBackColor = false;
            chkSubtype.CheckedChanged += chkSubtype_CheckedChanged;
            // 
            // numSubtype
            // 
            numSubtype.Location = new Point(7, 102);
            numSubtype.Margin = new Padding(4, 3, 4, 3);
            numSubtype.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numSubtype.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numSubtype.Name = "numSubtype";
            numSubtype.Size = new Size(117, 23);
            numSubtype.TabIndex = 1;
            numSubtype.ValueChanged += numSubtype_ValueChanged;
            // 
            // fraPosition
            // 
            fraPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPosition.Controls.Add(cmdSyncEntities);
            fraPosition.Controls.Add(chkSyncEntities);
            fraPosition.Controls.Add(chkSyncPositions);
            fraPosition.Controls.Add(cmdEditPath);
            fraPosition.Controls.Add(lblPositionIndex);
            fraPosition.Controls.Add(cmdNextPosition);
            fraPosition.Controls.Add(cmdPreviousPosition);
            fraPosition.Controls.Add(cmdInsertPosition);
            fraPosition.Controls.Add(lblZ);
            fraPosition.Controls.Add(cmdRemovePosition);
            fraPosition.Controls.Add(lblY);
            fraPosition.Controls.Add(cmdAppendPosition);
            fraPosition.Controls.Add(lblX);
            fraPosition.Controls.Add(numZ);
            fraPosition.Controls.Add(numY);
            fraPosition.Controls.Add(numX);
            fraPosition.Location = new Point(4, 91);
            fraPosition.Margin = new Padding(4, 3, 4, 3);
            fraPosition.Name = "fraPosition";
            fraPosition.Padding = new Padding(4, 3, 4, 3);
            fraPosition.Size = new Size(233, 224);
            fraPosition.TabIndex = 1;
            fraPosition.TabStop = false;
            fraPosition.Text = "Position(s)";
            // 
            // cmdSyncEntities
            // 
            cmdSyncEntities.BorderColour = Color.Empty;
            cmdSyncEntities.CustomColour = false;
            cmdSyncEntities.FlatBottom = false;
            cmdSyncEntities.FlatTop = false;
            cmdSyncEntities.Location = new Point(7, 185);
            cmdSyncEntities.Name = "cmdSyncEntities";
            cmdSyncEntities.Padding = new Padding(5);
            cmdSyncEntities.Size = new Size(74, 29);
            cmdSyncEntities.TabIndex = 10;
            cmdSyncEntities.Text = "View List";
            cmdSyncEntities.Click += cmdSyncList_Click;
            // 
            // chkSyncEntities
            // 
            chkSyncEntities.AutoSize = true;
            chkSyncEntities.BackColor = Color.Transparent;
            chkSyncEntities.Location = new Point(88, 191);
            chkSyncEntities.Name = "chkSyncEntities";
            chkSyncEntities.Size = new Size(92, 19);
            chkSyncEntities.TabIndex = 9;
            chkSyncEntities.Text = "Sync Entities";
            chkSyncEntities.UseVisualStyleBackColor = false;
            chkSyncEntities.CheckedChanged += chkSyncEntities_CheckedChanged;
            // 
            // chkSyncPositions
            // 
            chkSyncPositions.AutoSize = true;
            chkSyncPositions.BackColor = Color.Transparent;
            chkSyncPositions.Location = new Point(88, 156);
            chkSyncPositions.Name = "chkSyncPositions";
            chkSyncPositions.Size = new Size(102, 19);
            chkSyncPositions.TabIndex = 9;
            chkSyncPositions.Text = "Sync Positions";
            chkSyncPositions.UseVisualStyleBackColor = false;
            // 
            // cmdEditPath
            // 
            cmdEditPath.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cmdEditPath.BorderColour = Color.Empty;
            cmdEditPath.CustomColour = false;
            cmdEditPath.FlatBottom = false;
            cmdEditPath.FlatTop = false;
            cmdEditPath.Location = new Point(7, 150);
            cmdEditPath.Margin = new Padding(4, 3, 4, 3);
            cmdEditPath.Name = "cmdEditPath";
            cmdEditPath.Padding = new Padding(5);
            cmdEditPath.Size = new Size(74, 29);
            cmdEditPath.TabIndex = 8;
            cmdEditPath.Text = "Edit Path";
            cmdEditPath.Click += cmdInterpolate_Click;
            // 
            // lblPositionIndex
            // 
            lblPositionIndex.BackColor = Color.Transparent;
            lblPositionIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPositionIndex.Location = new Point(82, 22);
            lblPositionIndex.Margin = new Padding(4, 0, 4, 0);
            lblPositionIndex.Name = "lblPositionIndex";
            lblPositionIndex.Size = new Size(70, 27);
            lblPositionIndex.TabIndex = 5;
            lblPositionIndex.Text = "?? / ??";
            lblPositionIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdNextPosition
            // 
            cmdNextPosition.BorderColour = Color.Empty;
            cmdNextPosition.CustomColour = false;
            cmdNextPosition.FlatBottom = false;
            cmdNextPosition.FlatTop = false;
            cmdNextPosition.Location = new Point(159, 22);
            cmdNextPosition.Margin = new Padding(4, 3, 4, 3);
            cmdNextPosition.Name = "cmdNextPosition";
            cmdNextPosition.Padding = new Padding(5);
            cmdNextPosition.Size = new Size(68, 27);
            cmdNextPosition.TabIndex = 1;
            cmdNextPosition.Text = "Next";
            cmdNextPosition.Click += cmdNextPosition_Click;
            // 
            // cmdPreviousPosition
            // 
            cmdPreviousPosition.BorderColour = Color.Empty;
            cmdPreviousPosition.CustomColour = false;
            cmdPreviousPosition.FlatBottom = false;
            cmdPreviousPosition.FlatTop = false;
            cmdPreviousPosition.Location = new Point(7, 22);
            cmdPreviousPosition.Margin = new Padding(4, 3, 4, 3);
            cmdPreviousPosition.Name = "cmdPreviousPosition";
            cmdPreviousPosition.Padding = new Padding(5);
            cmdPreviousPosition.Size = new Size(68, 27);
            cmdPreviousPosition.TabIndex = 0;
            cmdPreviousPosition.Text = "Previous";
            cmdPreviousPosition.Click += cmdPreviousPosition_Click;
            // 
            // cmdInsertPosition
            // 
            cmdInsertPosition.BorderColour = Color.Empty;
            cmdInsertPosition.CustomColour = false;
            cmdInsertPosition.FlatBottom = false;
            cmdInsertPosition.FlatTop = false;
            cmdInsertPosition.Location = new Point(139, 87);
            cmdInsertPosition.Margin = new Padding(4, 3, 4, 3);
            cmdInsertPosition.Name = "cmdInsertPosition";
            cmdInsertPosition.Padding = new Padding(5);
            cmdInsertPosition.Size = new Size(88, 27);
            cmdInsertPosition.TabIndex = 6;
            cmdInsertPosition.Text = "Insert";
            cmdInsertPosition.Click += cmdInsertPosition_Click;
            // 
            // lblZ
            // 
            lblZ.AutoSize = true;
            lblZ.BackColor = Color.Transparent;
            lblZ.Location = new Point(7, 122);
            lblZ.Margin = new Padding(4, 0, 4, 0);
            lblZ.Name = "lblZ";
            lblZ.Size = new Size(14, 15);
            lblZ.TabIndex = 5;
            lblZ.Text = "Z";
            // 
            // cmdRemovePosition
            // 
            cmdRemovePosition.BorderColour = Color.Empty;
            cmdRemovePosition.CustomColour = false;
            cmdRemovePosition.FlatBottom = false;
            cmdRemovePosition.FlatTop = false;
            cmdRemovePosition.Location = new Point(139, 117);
            cmdRemovePosition.Margin = new Padding(4, 3, 4, 3);
            cmdRemovePosition.Name = "cmdRemovePosition";
            cmdRemovePosition.Padding = new Padding(5);
            cmdRemovePosition.Size = new Size(88, 27);
            cmdRemovePosition.TabIndex = 7;
            cmdRemovePosition.Text = "Remove";
            cmdRemovePosition.Click += cmdRemovePosition_Click;
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.BackColor = Color.Transparent;
            lblY.Location = new Point(7, 92);
            lblY.Margin = new Padding(4, 0, 4, 0);
            lblY.Name = "lblY";
            lblY.Size = new Size(14, 15);
            lblY.TabIndex = 4;
            lblY.Text = "Y";
            // 
            // cmdAppendPosition
            // 
            cmdAppendPosition.BorderColour = Color.Empty;
            cmdAppendPosition.CustomColour = false;
            cmdAppendPosition.FlatBottom = false;
            cmdAppendPosition.FlatTop = false;
            cmdAppendPosition.Location = new Point(139, 57);
            cmdAppendPosition.Margin = new Padding(4, 3, 4, 3);
            cmdAppendPosition.Name = "cmdAppendPosition";
            cmdAppendPosition.Padding = new Padding(5);
            cmdAppendPosition.Size = new Size(88, 27);
            cmdAppendPosition.TabIndex = 5;
            cmdAppendPosition.Text = "Append";
            cmdAppendPosition.Click += cmdAppendPosition_Click;
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.BackColor = Color.Transparent;
            lblX.Location = new Point(7, 62);
            lblX.Margin = new Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new Size(14, 15);
            lblX.TabIndex = 3;
            lblX.Text = "X";
            // 
            // numZ
            // 
            numZ.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numZ.Location = new Point(30, 120);
            numZ.Margin = new Padding(4, 3, 4, 3);
            numZ.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numZ.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numZ.Name = "numZ";
            numZ.Size = new Size(100, 23);
            numZ.TabIndex = 4;
            numZ.ValueChanged += numZ_ValueChanged;
            // 
            // numY
            // 
            numY.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numY.Location = new Point(30, 90);
            numY.Margin = new Padding(4, 3, 4, 3);
            numY.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numY.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numY.Name = "numY";
            numY.Size = new Size(100, 23);
            numY.TabIndex = 3;
            numY.ValueChanged += numY_ValueChanged;
            // 
            // numX
            // 
            numX.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numX.Location = new Point(30, 60);
            numX.Margin = new Padding(4, 3, 4, 3);
            numX.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numX.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numX.Name = "numX";
            numX.Size = new Size(100, 23);
            numX.TabIndex = 2;
            numX.ValueChanged += numX_ValueChanged;
            // 
            // cmdInsertVictim
            // 
            cmdInsertVictim.BorderColour = Color.Empty;
            cmdInsertVictim.CustomColour = false;
            cmdInsertVictim.FlatBottom = false;
            cmdInsertVictim.FlatTop = false;
            cmdInsertVictim.Location = new Point(79, 49);
            cmdInsertVictim.Margin = new Padding(4, 3, 4, 3);
            cmdInsertVictim.Name = "cmdInsertVictim";
            cmdInsertVictim.Padding = new Padding(5);
            cmdInsertVictim.Size = new Size(67, 27);
            cmdInsertVictim.TabIndex = 6;
            cmdInsertVictim.Text = "Insert";
            cmdInsertVictim.Click += cmdInsertVictim_Click;
            // 
            // fraID
            // 
            fraID.Controls.Add(chkID2);
            fraID.Controls.Add(numID2);
            fraID.Controls.Add(chkID);
            fraID.Controls.Add(numID);
            fraID.Location = new Point(244, 3);
            fraID.Margin = new Padding(4, 3, 4, 3);
            fraID.Name = "fraID";
            fraID.Padding = new Padding(4, 3, 4, 3);
            fraID.Size = new Size(132, 137);
            fraID.TabIndex = 3;
            fraID.TabStop = false;
            fraID.Text = "ID & Look-up ID";
            // 
            // chkID2
            // 
            chkID2.AutoSize = true;
            chkID2.BackColor = Color.Transparent;
            chkID2.Location = new Point(7, 77);
            chkID2.Margin = new Padding(4, 3, 4, 3);
            chkID2.Name = "chkID2";
            chkID2.Size = new Size(68, 19);
            chkID2.TabIndex = 2;
            chkID2.Text = "Enabled";
            chkID2.UseVisualStyleBackColor = false;
            chkID2.CheckedChanged += chkID2_CheckedChanged;
            // 
            // numID2
            // 
            numID2.Location = new Point(7, 102);
            numID2.Margin = new Padding(4, 3, 4, 3);
            numID2.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numID2.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numID2.Name = "numID2";
            numID2.Size = new Size(117, 23);
            numID2.TabIndex = 3;
            numID2.ValueChanged += numID2_ValueChanged;
            // 
            // chkID
            // 
            chkID.AutoSize = true;
            chkID.BackColor = Color.Transparent;
            chkID.Location = new Point(7, 22);
            chkID.Margin = new Padding(4, 3, 4, 3);
            chkID.Name = "chkID";
            chkID.Size = new Size(68, 19);
            chkID.TabIndex = 0;
            chkID.Text = "Enabled";
            chkID.UseVisualStyleBackColor = false;
            chkID.CheckedChanged += chkID_CheckedChanged;
            // 
            // numID
            // 
            numID.Location = new Point(7, 48);
            numID.Margin = new Padding(4, 3, 4, 3);
            numID.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numID.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numID.Name = "numID";
            numID.Size = new Size(117, 23);
            numID.TabIndex = 1;
            numID.ValueChanged += numID_ValueChanged;
            // 
            // fraSettings
            // 
            fraSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSettings.Controls.Add(cmdPasteSetting);
            fraSettings.Controls.Add(cmdCopySetting);
            fraSettings.Controls.Add(lblSettingB);
            fraSettings.Controls.Add(lblSettingA);
            fraSettings.Controls.Add(lblArgAs);
            fraSettings.Controls.Add(chkSettingHex);
            fraSettings.Controls.Add(numSettingC);
            fraSettings.Controls.Add(lblSettingIndex);
            fraSettings.Controls.Add(cmdNextSetting);
            fraSettings.Controls.Add(cmdPreviousSetting);
            fraSettings.Controls.Add(cmdAddSetting);
            fraSettings.Controls.Add(cmdRemoveSetting);
            fraSettings.Controls.Add(numSettingB);
            fraSettings.Controls.Add(numSettingA);
            fraSettings.Location = new Point(4, 321);
            fraSettings.Margin = new Padding(4, 3, 4, 3);
            fraSettings.Name = "fraSettings";
            fraSettings.Padding = new Padding(4, 3, 4, 3);
            fraSettings.Size = new Size(233, 210);
            fraSettings.TabIndex = 2;
            fraSettings.TabStop = false;
            fraSettings.Text = "Argument(s)";
            // 
            // cmdPasteSetting
            // 
            cmdPasteSetting.BorderColour = Color.Empty;
            cmdPasteSetting.CustomColour = false;
            cmdPasteSetting.FlatBottom = false;
            cmdPasteSetting.FlatTop = false;
            cmdPasteSetting.Location = new Point(159, 173);
            cmdPasteSetting.Name = "cmdPasteSetting";
            cmdPasteSetting.Padding = new Padding(5);
            cmdPasteSetting.Size = new Size(67, 27);
            cmdPasteSetting.TabIndex = 7;
            cmdPasteSetting.Text = "Paste";
            cmdPasteSetting.Click += cmdPasteSetting_Click;
            // 
            // cmdCopySetting
            // 
            cmdCopySetting.BorderColour = Color.Empty;
            cmdCopySetting.CustomColour = false;
            cmdCopySetting.FlatBottom = false;
            cmdCopySetting.FlatTop = false;
            cmdCopySetting.Location = new Point(159, 144);
            cmdCopySetting.Name = "cmdCopySetting";
            cmdCopySetting.Padding = new Padding(5);
            cmdCopySetting.Size = new Size(67, 27);
            cmdCopySetting.TabIndex = 7;
            cmdCopySetting.Text = "Copy";
            cmdCopySetting.Click += cmdCopySetting_Click;
            // 
            // lblSettingB
            // 
            lblSettingB.AutoSize = true;
            lblSettingB.BackColor = Color.Transparent;
            lblSettingB.Font = new Font("Segoe UI", 9F);
            lblSettingB.Location = new Point(8, 82);
            lblSettingB.Margin = new Padding(4, 0, 4, 0);
            lblSettingB.Name = "lblSettingB";
            lblSettingB.Size = new Size(14, 15);
            lblSettingB.TabIndex = 11;
            lblSettingB.Text = "B";
            lblSettingB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSettingA
            // 
            lblSettingA.AutoSize = true;
            lblSettingA.BackColor = Color.Transparent;
            lblSettingA.Font = new Font("Segoe UI", 9F);
            lblSettingA.Location = new Point(8, 58);
            lblSettingA.Margin = new Padding(4, 0, 4, 0);
            lblSettingA.Name = "lblSettingA";
            lblSettingA.Size = new Size(15, 15);
            lblSettingA.TabIndex = 10;
            lblSettingA.Text = "A";
            lblSettingA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblArgAs
            // 
            lblArgAs.AutoSize = true;
            lblArgAs.BackColor = Color.Transparent;
            lblArgAs.ForeColor = SystemColors.InfoText;
            lblArgAs.Location = new Point(8, 138);
            lblArgAs.Margin = new Padding(4, 0, 4, 0);
            lblArgAs.Name = "lblArgAs";
            lblArgAs.Size = new Size(123, 60);
            lblArgAs.TabIndex = 9;
            lblArgAs.Text = "<EntityBox_lblArgAs>\r\n<EntityBox_lblArgAs>\r\n<EntityBox_lblArgAs>\r\n<EntityBox_lblArgAs>";
            // 
            // chkSettingHex
            // 
            chkSettingHex.AutoSize = true;
            chkSettingHex.BackColor = Color.Transparent;
            chkSettingHex.Checked = true;
            chkSettingHex.CheckState = CheckState.Checked;
            chkSettingHex.Location = new Point(139, 110);
            chkSettingHex.Margin = new Padding(4, 3, 4, 3);
            chkSettingHex.Name = "chkSettingHex";
            chkSettingHex.Size = new Size(47, 19);
            chkSettingHex.TabIndex = 8;
            chkSettingHex.Text = "Hex";
            chkSettingHex.UseVisualStyleBackColor = false;
            chkSettingHex.CheckedChanged += chkSettingHex_CheckedChanged;
            // 
            // numSettingC
            // 
            numSettingC.Hexadecimal = true;
            numSettingC.Location = new Point(7, 108);
            numSettingC.Margin = new Padding(4, 3, 4, 3);
            numSettingC.Maximum = new decimal(new int[] { -1, int.MaxValue, 0, 0 });
            numSettingC.Minimum = new decimal(new int[] { 0, int.MinValue, 0, int.MinValue });
            numSettingC.Name = "numSettingC";
            numSettingC.Size = new Size(123, 23);
            numSettingC.TabIndex = 7;
            numSettingC.ValueChanged += numSettingC_ValueChanged;
            // 
            // lblSettingIndex
            // 
            lblSettingIndex.BackColor = Color.Transparent;
            lblSettingIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSettingIndex.Location = new Point(82, 22);
            lblSettingIndex.Margin = new Padding(4, 0, 4, 0);
            lblSettingIndex.Name = "lblSettingIndex";
            lblSettingIndex.Size = new Size(70, 27);
            lblSettingIndex.TabIndex = 5;
            lblSettingIndex.Text = "?? / ??";
            lblSettingIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdNextSetting
            // 
            cmdNextSetting.BorderColour = Color.Empty;
            cmdNextSetting.CustomColour = false;
            cmdNextSetting.FlatBottom = false;
            cmdNextSetting.FlatTop = false;
            cmdNextSetting.Location = new Point(159, 22);
            cmdNextSetting.Margin = new Padding(4, 3, 4, 3);
            cmdNextSetting.Name = "cmdNextSetting";
            cmdNextSetting.Padding = new Padding(5);
            cmdNextSetting.Size = new Size(68, 27);
            cmdNextSetting.TabIndex = 1;
            cmdNextSetting.Text = "Next";
            cmdNextSetting.Click += cmdNextSetting_Click;
            // 
            // cmdPreviousSetting
            // 
            cmdPreviousSetting.BorderColour = Color.Empty;
            cmdPreviousSetting.CustomColour = false;
            cmdPreviousSetting.FlatBottom = false;
            cmdPreviousSetting.FlatTop = false;
            cmdPreviousSetting.Location = new Point(7, 22);
            cmdPreviousSetting.Margin = new Padding(4, 3, 4, 3);
            cmdPreviousSetting.Name = "cmdPreviousSetting";
            cmdPreviousSetting.Padding = new Padding(5);
            cmdPreviousSetting.Size = new Size(68, 27);
            cmdPreviousSetting.TabIndex = 0;
            cmdPreviousSetting.Text = "Previous";
            cmdPreviousSetting.Click += cmdPreviousSetting_Click;
            // 
            // cmdAddSetting
            // 
            cmdAddSetting.BorderColour = Color.Empty;
            cmdAddSetting.CustomColour = false;
            cmdAddSetting.FlatBottom = false;
            cmdAddSetting.FlatTop = false;
            cmdAddSetting.Location = new Point(139, 52);
            cmdAddSetting.Margin = new Padding(4, 3, 4, 3);
            cmdAddSetting.Name = "cmdAddSetting";
            cmdAddSetting.Padding = new Padding(5);
            cmdAddSetting.Size = new Size(88, 27);
            cmdAddSetting.TabIndex = 4;
            cmdAddSetting.Text = "Add";
            cmdAddSetting.Click += cmdAddSetting_Click;
            // 
            // cmdRemoveSetting
            // 
            cmdRemoveSetting.BorderColour = Color.Empty;
            cmdRemoveSetting.CustomColour = false;
            cmdRemoveSetting.FlatBottom = false;
            cmdRemoveSetting.FlatTop = false;
            cmdRemoveSetting.Location = new Point(139, 80);
            cmdRemoveSetting.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveSetting.Name = "cmdRemoveSetting";
            cmdRemoveSetting.Padding = new Padding(5);
            cmdRemoveSetting.Size = new Size(88, 27);
            cmdRemoveSetting.TabIndex = 5;
            cmdRemoveSetting.Text = "Remove";
            cmdRemoveSetting.Click += cmdRemoveSetting_Click;
            // 
            // numSettingB
            // 
            numSettingB.Location = new Point(30, 80);
            numSettingB.Margin = new Padding(4, 3, 4, 3);
            numSettingB.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numSettingB.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numSettingB.Name = "numSettingB";
            numSettingB.Size = new Size(100, 23);
            numSettingB.TabIndex = 3;
            numSettingB.ValueChanged += numSettingB_ValueChanged;
            // 
            // numSettingA
            // 
            numSettingA.Location = new Point(30, 55);
            numSettingA.Margin = new Padding(4, 3, 4, 3);
            numSettingA.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numSettingA.Name = "numSettingA";
            numSettingA.Size = new Size(100, 23);
            numSettingA.TabIndex = 2;
            numSettingA.ValueChanged += numSettingA_ValueChanged;
            // 
            // fraName
            // 
            fraName.Controls.Add(txtName);
            fraName.Controls.Add(chkName);
            fraName.Location = new Point(4, 3);
            fraName.Margin = new Padding(4, 3, 4, 3);
            fraName.Name = "fraName";
            fraName.Padding = new Padding(4, 3, 4, 3);
            fraName.Size = new Size(233, 83);
            fraName.TabIndex = 0;
            fraName.TabStop = false;
            fraName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(26, 26, 28);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(213, 213, 213);
            txtName.Location = new Point(7, 48);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(219, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // chkName
            // 
            chkName.AutoSize = true;
            chkName.BackColor = Color.Transparent;
            chkName.Location = new Point(7, 22);
            chkName.Margin = new Padding(4, 3, 4, 3);
            chkName.Name = "chkName";
            chkName.Size = new Size(68, 19);
            chkName.TabIndex = 0;
            chkName.Text = "Enabled";
            chkName.UseVisualStyleBackColor = false;
            chkName.CheckedChanged += chkName_CheckedChanged;
            // 
            // tbcTabs
            // 
            tbcTabs.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tbcTabs.AnimateTime = 200;
            tbcTabs.BackgroundColor = Color.FromArgb(31, 31, 32);
            tbcTabs.Controls.Add(tabGeneral);
            tbcTabs.Controls.Add(tabSpecial);
            tbcTabs.Controls.Add(tabCamera);
            tbcTabs.Controls.Add(tabLoadLists);
            tbcTabs.Controls.Add(tabDrawLists);
            tbcTabs.Dock = DockStyle.Fill;
            tbcTabs.IsDerivedStyle = false;
            tbcTabs.ItemSize = new Size(100, 28);
            tbcTabs.Location = new Point(0, 0);
            tbcTabs.Margin = new Padding(4, 3, 4, 3);
            tbcTabs.Name = "tbcTabs";
            tbcTabs.SelectedIndex = 0;
            tbcTabs.SelectedTextColor = Color.White;
            tbcTabs.Size = new Size(600, 600);
            tbcTabs.SizeMode = TabSizeMode.Fixed;
            tbcTabs.Speed = 100;
            tbcTabs.Style = MetroSet_UI.Enums.Style.Dark;
            tbcTabs.StyleManager = null;
            tbcTabs.TabIndex = 7;
            tbcTabs.ThemeAuthor = "Narwin";
            tbcTabs.ThemeName = "MetroDark";
            tbcTabs.UnselectedTextColor = Color.Gray;
            tbcTabs.UseAnimation = false;
            // 
            // tabGeneral
            // 
            tabGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tabGeneral.Controls.Add(fraC2TTSet);
            tabGeneral.Controls.Add(fraName);
            tabGeneral.Controls.Add(fraType);
            tabGeneral.Controls.Add(fraSettings);
            tabGeneral.Controls.Add(fraPosition);
            tabGeneral.Controls.Add(fraID);
            tabGeneral.Controls.Add(fraZMod);
            tabGeneral.Location = new Point(4, 32);
            tabGeneral.Margin = new Padding(4, 3, 4, 3);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(4, 3, 4, 3);
            tabGeneral.Size = new Size(592, 564);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            // 
            // fraC2TTSet
            // 
            fraC2TTSet.Controls.Add(fraC2TTGhostTarget);
            fraC2TTSet.Controls.Add(fraC2TTFlags);
            fraC2TTSet.Controls.Add(fraC2TTYRot);
            fraC2TTSet.Controls.Add(fraC2TTType);
            fraC2TTSet.Location = new Point(245, 146);
            fraC2TTSet.Margin = new Padding(4, 3, 4, 3);
            fraC2TTSet.Name = "fraC2TTSet";
            fraC2TTSet.Padding = new Padding(4, 3, 4, 3);
            fraC2TTSet.Size = new Size(132, 356);
            fraC2TTSet.TabIndex = 6;
            fraC2TTSet.TabStop = false;
            fraC2TTSet.Text = "C2 Time Trials";
            fraC2TTSet.Visible = false;
            // 
            // fraC2TTGhostTarget
            // 
            fraC2TTGhostTarget.BackColor = Color.Transparent;
            fraC2TTGhostTarget.Controls.Add(numC2TTGhostTarget);
            fraC2TTGhostTarget.Controls.Add(chkC2TTGhostTarget);
            fraC2TTGhostTarget.Location = new Point(7, 268);
            fraC2TTGhostTarget.Name = "fraC2TTGhostTarget";
            fraC2TTGhostTarget.Size = new Size(116, 76);
            fraC2TTGhostTarget.TabIndex = 5;
            fraC2TTGhostTarget.TabStop = false;
            fraC2TTGhostTarget.Text = "Ghost Target";
            // 
            // numC2TTGhostTarget
            // 
            numC2TTGhostTarget.Location = new Point(7, 47);
            numC2TTGhostTarget.Margin = new Padding(4, 3, 4, 3);
            numC2TTGhostTarget.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numC2TTGhostTarget.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numC2TTGhostTarget.Name = "numC2TTGhostTarget";
            numC2TTGhostTarget.Size = new Size(102, 23);
            numC2TTGhostTarget.TabIndex = 1;
            numC2TTGhostTarget.ValueChanged += numC2TTGhostTarget_ValueChanged;
            // 
            // chkC2TTGhostTarget
            // 
            chkC2TTGhostTarget.AutoSize = true;
            chkC2TTGhostTarget.BackColor = Color.Transparent;
            chkC2TTGhostTarget.Location = new Point(7, 22);
            chkC2TTGhostTarget.Margin = new Padding(4, 3, 4, 3);
            chkC2TTGhostTarget.Name = "chkC2TTGhostTarget";
            chkC2TTGhostTarget.Size = new Size(68, 19);
            chkC2TTGhostTarget.TabIndex = 0;
            chkC2TTGhostTarget.Text = "Enabled";
            chkC2TTGhostTarget.UseVisualStyleBackColor = false;
            chkC2TTGhostTarget.CheckedChanged += chkC2TTGhostTarget_CheckedChanged;
            // 
            // fraC2TTFlags
            // 
            fraC2TTFlags.BackColor = Color.Transparent;
            fraC2TTFlags.Controls.Add(numC2TTFlags);
            fraC2TTFlags.Controls.Add(chkC2TTFlags);
            fraC2TTFlags.Location = new Point(7, 186);
            fraC2TTFlags.Name = "fraC2TTFlags";
            fraC2TTFlags.Size = new Size(116, 76);
            fraC2TTFlags.TabIndex = 6;
            fraC2TTFlags.TabStop = false;
            fraC2TTFlags.Text = "Flags";
            // 
            // numC2TTFlags
            // 
            numC2TTFlags.Location = new Point(7, 47);
            numC2TTFlags.Margin = new Padding(4, 3, 4, 3);
            numC2TTFlags.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numC2TTFlags.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numC2TTFlags.Name = "numC2TTFlags";
            numC2TTFlags.Size = new Size(102, 23);
            numC2TTFlags.TabIndex = 1;
            numC2TTFlags.ValueChanged += numC2TTFlags_ValueChanged;
            // 
            // chkC2TTFlags
            // 
            chkC2TTFlags.AutoSize = true;
            chkC2TTFlags.BackColor = Color.Transparent;
            chkC2TTFlags.Location = new Point(7, 22);
            chkC2TTFlags.Margin = new Padding(4, 3, 4, 3);
            chkC2TTFlags.Name = "chkC2TTFlags";
            chkC2TTFlags.Size = new Size(68, 19);
            chkC2TTFlags.TabIndex = 0;
            chkC2TTFlags.Text = "Enabled";
            chkC2TTFlags.UseVisualStyleBackColor = false;
            chkC2TTFlags.CheckedChanged += chkC2TTFlags_CheckedChanged;
            // 
            // fraC2TTYRot
            // 
            fraC2TTYRot.BackColor = Color.Transparent;
            fraC2TTYRot.Controls.Add(numC2TTYRot);
            fraC2TTYRot.Controls.Add(chkC2TTYRot);
            fraC2TTYRot.Location = new Point(7, 104);
            fraC2TTYRot.Name = "fraC2TTYRot";
            fraC2TTYRot.Size = new Size(116, 76);
            fraC2TTYRot.TabIndex = 5;
            fraC2TTYRot.TabStop = false;
            fraC2TTYRot.Text = "RotY";
            // 
            // numC2TTYRot
            // 
            numC2TTYRot.Location = new Point(7, 47);
            numC2TTYRot.Margin = new Padding(4, 3, 4, 3);
            numC2TTYRot.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numC2TTYRot.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numC2TTYRot.Name = "numC2TTYRot";
            numC2TTYRot.Size = new Size(102, 23);
            numC2TTYRot.TabIndex = 1;
            numC2TTYRot.ValueChanged += numC2TTYRot_ValueChanged;
            // 
            // chkC2TTYRot
            // 
            chkC2TTYRot.AutoSize = true;
            chkC2TTYRot.BackColor = Color.Transparent;
            chkC2TTYRot.Location = new Point(7, 22);
            chkC2TTYRot.Margin = new Padding(4, 3, 4, 3);
            chkC2TTYRot.Name = "chkC2TTYRot";
            chkC2TTYRot.Size = new Size(68, 19);
            chkC2TTYRot.TabIndex = 0;
            chkC2TTYRot.Text = "Enabled";
            chkC2TTYRot.UseVisualStyleBackColor = false;
            chkC2TTYRot.CheckedChanged += chkC2TTYRot_CheckedChanged;
            // 
            // fraC2TTType
            // 
            fraC2TTType.BackColor = Color.Transparent;
            fraC2TTType.Controls.Add(numC2TTType);
            fraC2TTType.Controls.Add(chkC2TTType);
            fraC2TTType.Location = new Point(7, 22);
            fraC2TTType.Name = "fraC2TTType";
            fraC2TTType.Size = new Size(116, 76);
            fraC2TTType.TabIndex = 4;
            fraC2TTType.TabStop = false;
            fraC2TTType.Text = "Type";
            // 
            // numC2TTType
            // 
            numC2TTType.Location = new Point(7, 47);
            numC2TTType.Margin = new Padding(4, 3, 4, 3);
            numC2TTType.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numC2TTType.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numC2TTType.Name = "numC2TTType";
            numC2TTType.Size = new Size(102, 23);
            numC2TTType.TabIndex = 1;
            numC2TTType.ValueChanged += numC2TTType_ValueChanged;
            // 
            // chkC2TTType
            // 
            chkC2TTType.AutoSize = true;
            chkC2TTType.BackColor = Color.Transparent;
            chkC2TTType.Location = new Point(7, 22);
            chkC2TTType.Margin = new Padding(4, 3, 4, 3);
            chkC2TTType.Name = "chkC2TTType";
            chkC2TTType.Size = new Size(68, 19);
            chkC2TTType.TabIndex = 0;
            chkC2TTType.Text = "Enabled";
            chkC2TTType.UseVisualStyleBackColor = false;
            chkC2TTType.CheckedChanged += chkC2TTType_CheckedChanged;
            // 
            // fraZMod
            // 
            fraZMod.Controls.Add(chkZMod);
            fraZMod.Controls.Add(numZMod);
            fraZMod.Location = new Point(384, 146);
            fraZMod.Margin = new Padding(4, 3, 4, 3);
            fraZMod.Name = "fraZMod";
            fraZMod.Padding = new Padding(4, 3, 4, 3);
            fraZMod.Size = new Size(132, 81);
            fraZMod.TabIndex = 6;
            fraZMod.TabStop = false;
            fraZMod.Text = "Depth Modifier";
            // 
            // chkZMod
            // 
            chkZMod.AutoSize = true;
            chkZMod.BackColor = Color.Transparent;
            chkZMod.Location = new Point(7, 22);
            chkZMod.Margin = new Padding(4, 3, 4, 3);
            chkZMod.Name = "chkZMod";
            chkZMod.Size = new Size(68, 19);
            chkZMod.TabIndex = 0;
            chkZMod.Text = "Enabled";
            chkZMod.UseVisualStyleBackColor = false;
            chkZMod.CheckedChanged += chkZMod_CheckedChanged;
            // 
            // numZMod
            // 
            numZMod.Location = new Point(7, 48);
            numZMod.Margin = new Padding(4, 3, 4, 3);
            numZMod.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numZMod.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numZMod.Name = "numZMod";
            numZMod.Size = new Size(117, 23);
            numZMod.TabIndex = 1;
            numZMod.ValueChanged += numZMod_ValueChanged;
            // 
            // tabSpecial
            // 
            tabSpecial.BackColor = Color.FromArgb(31, 31, 32);
            tabSpecial.Controls.Add(fraTTReward);
            tabSpecial.Controls.Add(fraOtherSettings);
            tabSpecial.Controls.Add(fraScaling);
            tabSpecial.Controls.Add(fraDDASection);
            tabSpecial.Controls.Add(fraDDASettings);
            tabSpecial.Controls.Add(fraDrawOverrides);
            tabSpecial.Controls.Add(fraBoxCount);
            tabSpecial.Controls.Add(fraVictims);
            tabSpecial.Location = new Point(4, 32);
            tabSpecial.Margin = new Padding(4, 3, 4, 3);
            tabSpecial.Name = "tabSpecial";
            tabSpecial.Padding = new Padding(4, 3, 4, 3);
            tabSpecial.Size = new Size(592, 564);
            tabSpecial.TabIndex = 1;
            tabSpecial.Text = "Special";
            tabSpecial.Enter += tabSpecial_Enter;
            // 
            // fraTTReward
            // 
            fraTTReward.Controls.Add(chkTTReward);
            fraTTReward.Controls.Add(numTTReward);
            fraTTReward.Location = new Point(312, 146);
            fraTTReward.Margin = new Padding(4, 3, 4, 3);
            fraTTReward.Name = "fraTTReward";
            fraTTReward.Padding = new Padding(4, 3, 4, 3);
            fraTTReward.Size = new Size(140, 81);
            fraTTReward.TabIndex = 12;
            fraTTReward.TabStop = false;
            fraTTReward.Text = "Time Trial Reward";
            // 
            // chkTTReward
            // 
            chkTTReward.AutoSize = true;
            chkTTReward.BackColor = Color.Transparent;
            chkTTReward.Location = new Point(7, 22);
            chkTTReward.Margin = new Padding(4, 3, 4, 3);
            chkTTReward.Name = "chkTTReward";
            chkTTReward.Size = new Size(68, 19);
            chkTTReward.TabIndex = 0;
            chkTTReward.Text = "Enabled";
            chkTTReward.UseVisualStyleBackColor = false;
            chkTTReward.CheckedChanged += chkTTReward_CheckedChanged;
            // 
            // numTTReward
            // 
            numTTReward.Location = new Point(7, 48);
            numTTReward.Margin = new Padding(4, 3, 4, 3);
            numTTReward.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numTTReward.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numTTReward.Name = "numTTReward";
            numTTReward.Size = new Size(126, 23);
            numTTReward.TabIndex = 1;
            numTTReward.ValueChanged += numTTReward_ValueChanged;
            // 
            // fraOtherSettings
            // 
            fraOtherSettings.Controls.Add(chkOtherSettings);
            fraOtherSettings.Controls.Add(numOtherSettings);
            fraOtherSettings.Location = new Point(312, 321);
            fraOtherSettings.Margin = new Padding(4, 3, 4, 3);
            fraOtherSettings.Name = "fraOtherSettings";
            fraOtherSettings.Padding = new Padding(4, 3, 4, 3);
            fraOtherSettings.Size = new Size(140, 81);
            fraOtherSettings.TabIndex = 10;
            fraOtherSettings.TabStop = false;
            fraOtherSettings.Text = "Other Settings";
            // 
            // chkOtherSettings
            // 
            chkOtherSettings.AutoSize = true;
            chkOtherSettings.BackColor = Color.Transparent;
            chkOtherSettings.Location = new Point(7, 22);
            chkOtherSettings.Margin = new Padding(4, 3, 4, 3);
            chkOtherSettings.Name = "chkOtherSettings";
            chkOtherSettings.Size = new Size(68, 19);
            chkOtherSettings.TabIndex = 0;
            chkOtherSettings.Text = "Enabled";
            chkOtherSettings.UseVisualStyleBackColor = false;
            chkOtherSettings.CheckedChanged += chkOtherSettings_CheckedChanged;
            // 
            // numOtherSettings
            // 
            numOtherSettings.Location = new Point(7, 48);
            numOtherSettings.Margin = new Padding(4, 3, 4, 3);
            numOtherSettings.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numOtherSettings.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numOtherSettings.Name = "numOtherSettings";
            numOtherSettings.Size = new Size(126, 23);
            numOtherSettings.TabIndex = 1;
            numOtherSettings.ValueChanged += numOtherSettings_ValueChanged;
            // 
            // fraScaling
            // 
            fraScaling.Controls.Add(chkScaling);
            fraScaling.Controls.Add(numScaling);
            fraScaling.Location = new Point(312, 234);
            fraScaling.Margin = new Padding(4, 3, 4, 3);
            fraScaling.Name = "fraScaling";
            fraScaling.Padding = new Padding(4, 3, 4, 3);
            fraScaling.Size = new Size(140, 81);
            fraScaling.TabIndex = 11;
            fraScaling.TabStop = false;
            fraScaling.Text = "Scale Modifier";
            // 
            // chkScaling
            // 
            chkScaling.AutoSize = true;
            chkScaling.BackColor = Color.Transparent;
            chkScaling.Location = new Point(7, 22);
            chkScaling.Margin = new Padding(4, 3, 4, 3);
            chkScaling.Name = "chkScaling";
            chkScaling.Size = new Size(68, 19);
            chkScaling.TabIndex = 0;
            chkScaling.Text = "Enabled";
            chkScaling.UseVisualStyleBackColor = false;
            chkScaling.CheckedChanged += chkScaling_CheckedChanged;
            // 
            // numScaling
            // 
            numScaling.Location = new Point(7, 48);
            numScaling.Margin = new Padding(4, 3, 4, 3);
            numScaling.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaling.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaling.Name = "numScaling";
            numScaling.Size = new Size(126, 23);
            numScaling.TabIndex = 1;
            numScaling.ValueChanged += numScaling_ValueChanged;
            // 
            // fraDDASection
            // 
            fraDDASection.Controls.Add(chkDDASection);
            fraDDASection.Controls.Add(numDDASection);
            fraDDASection.Location = new Point(166, 146);
            fraDDASection.Margin = new Padding(4, 3, 4, 3);
            fraDDASection.Name = "fraDDASection";
            fraDDASection.Padding = new Padding(4, 3, 4, 3);
            fraDDASection.Size = new Size(140, 81);
            fraDDASection.TabIndex = 10;
            fraDDASection.TabStop = false;
            fraDDASection.Text = "DDA Section";
            // 
            // chkDDASection
            // 
            chkDDASection.AutoSize = true;
            chkDDASection.BackColor = Color.Transparent;
            chkDDASection.Location = new Point(7, 22);
            chkDDASection.Margin = new Padding(4, 3, 4, 3);
            chkDDASection.Name = "chkDDASection";
            chkDDASection.Size = new Size(68, 19);
            chkDDASection.TabIndex = 0;
            chkDDASection.Text = "Enabled";
            chkDDASection.UseVisualStyleBackColor = false;
            chkDDASection.CheckedChanged += chkDDASection_CheckedChanged;
            // 
            // numDDASection
            // 
            numDDASection.Location = new Point(7, 48);
            numDDASection.Margin = new Padding(4, 3, 4, 3);
            numDDASection.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numDDASection.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numDDASection.Name = "numDDASection";
            numDDASection.Size = new Size(126, 23);
            numDDASection.TabIndex = 1;
            numDDASection.ValueChanged += numDDASection_ValueChanged;
            // 
            // fraDDASettings
            // 
            fraDDASettings.Controls.Add(chkDDASettings);
            fraDDASettings.Controls.Add(numDDASettings);
            fraDDASettings.Location = new Point(166, 234);
            fraDDASettings.Margin = new Padding(4, 3, 4, 3);
            fraDDASettings.Name = "fraDDASettings";
            fraDDASettings.Padding = new Padding(4, 3, 4, 3);
            fraDDASettings.Size = new Size(140, 81);
            fraDDASettings.TabIndex = 9;
            fraDDASettings.TabStop = false;
            fraDDASettings.Text = "DDA Death Count";
            // 
            // chkDDASettings
            // 
            chkDDASettings.AutoSize = true;
            chkDDASettings.BackColor = Color.Transparent;
            chkDDASettings.Location = new Point(7, 22);
            chkDDASettings.Margin = new Padding(4, 3, 4, 3);
            chkDDASettings.Name = "chkDDASettings";
            chkDDASettings.Size = new Size(68, 19);
            chkDDASettings.TabIndex = 0;
            chkDDASettings.Text = "Enabled";
            chkDDASettings.UseVisualStyleBackColor = false;
            chkDDASettings.CheckedChanged += chkDDASettings_CheckedChanged;
            // 
            // numDDASettings
            // 
            numDDASettings.Location = new Point(7, 48);
            numDDASettings.Margin = new Padding(4, 3, 4, 3);
            numDDASettings.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numDDASettings.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numDDASettings.Name = "numDDASettings";
            numDDASettings.Size = new Size(126, 23);
            numDDASettings.TabIndex = 1;
            numDDASettings.ValueChanged += numDDASettings_ValueChanged;
            // 
            // fraDrawOverrides
            // 
            fraDrawOverrides.Controls.Add(chkDrawOverrideId);
            fraDrawOverrides.Controls.Add(numDrawOverrideId);
            fraDrawOverrides.Controls.Add(chkDrawOverrideMult);
            fraDrawOverrides.Controls.Add(numDrawOverrideMult);
            fraDrawOverrides.Location = new Point(166, 322);
            fraDrawOverrides.Margin = new Padding(4, 3, 4, 3);
            fraDrawOverrides.Name = "fraDrawOverrides";
            fraDrawOverrides.Padding = new Padding(4, 3, 4, 3);
            fraDrawOverrides.Size = new Size(140, 140);
            fraDrawOverrides.TabIndex = 9;
            fraDrawOverrides.TabStop = false;
            fraDrawOverrides.Text = "Draw List Overrides";
            // 
            // chkDrawOverrideId
            // 
            chkDrawOverrideId.AutoSize = true;
            chkDrawOverrideId.BackColor = Color.Transparent;
            chkDrawOverrideId.Location = new Point(7, 22);
            chkDrawOverrideId.Margin = new Padding(4, 3, 4, 3);
            chkDrawOverrideId.Name = "chkDrawOverrideId";
            chkDrawOverrideId.Size = new Size(85, 19);
            chkDrawOverrideId.TabIndex = 0;
            chkDrawOverrideId.Text = "Override ID";
            chkDrawOverrideId.UseVisualStyleBackColor = false;
            chkDrawOverrideId.CheckedChanged += chkDrawOverrideId_Changed;
            // 
            // numDrawOverrideId
            // 
            numDrawOverrideId.Location = new Point(7, 48);
            numDrawOverrideId.Margin = new Padding(4, 3, 4, 3);
            numDrawOverrideId.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numDrawOverrideId.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numDrawOverrideId.Name = "numDrawOverrideId";
            numDrawOverrideId.Size = new Size(126, 23);
            numDrawOverrideId.TabIndex = 1;
            numDrawOverrideId.ValueChanged += numDrawOverrideId_Changed;
            // 
            // chkDrawOverrideMult
            // 
            chkDrawOverrideMult.AutoSize = true;
            chkDrawOverrideMult.BackColor = Color.Transparent;
            chkDrawOverrideMult.Location = new Point(7, 80);
            chkDrawOverrideMult.Margin = new Padding(4, 3, 4, 3);
            chkDrawOverrideMult.Name = "chkDrawOverrideMult";
            chkDrawOverrideMult.Size = new Size(99, 19);
            chkDrawOverrideMult.TabIndex = 0;
            chkDrawOverrideMult.Text = "Override Mult";
            chkDrawOverrideMult.UseVisualStyleBackColor = false;
            chkDrawOverrideMult.CheckedChanged += chkDrawOverrideMult_Changed;
            // 
            // numDrawOverrideMult
            // 
            numDrawOverrideMult.Location = new Point(7, 106);
            numDrawOverrideMult.Margin = new Padding(4, 3, 4, 3);
            numDrawOverrideMult.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numDrawOverrideMult.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numDrawOverrideMult.Name = "numDrawOverrideMult";
            numDrawOverrideMult.Size = new Size(126, 23);
            numDrawOverrideMult.TabIndex = 1;
            numDrawOverrideMult.ValueChanged += numDrawOverrideMult_Changed;
            // 
            // fraBoxCount
            // 
            fraBoxCount.Controls.Add(chkBonusBoxCount);
            fraBoxCount.Controls.Add(numBonusBoxCount);
            fraBoxCount.Controls.Add(chkBoxCount);
            fraBoxCount.Controls.Add(numBoxCount);
            fraBoxCount.Location = new Point(166, 3);
            fraBoxCount.Margin = new Padding(4, 3, 4, 3);
            fraBoxCount.Name = "fraBoxCount";
            fraBoxCount.Padding = new Padding(4, 3, 4, 3);
            fraBoxCount.Size = new Size(140, 137);
            fraBoxCount.TabIndex = 8;
            fraBoxCount.TabStop = false;
            fraBoxCount.Text = "Box Count";
            // 
            // chkBonusBoxCount
            // 
            chkBonusBoxCount.AutoSize = true;
            chkBonusBoxCount.BackColor = Color.Transparent;
            chkBonusBoxCount.Location = new Point(7, 78);
            chkBonusBoxCount.Margin = new Padding(4, 3, 4, 3);
            chkBonusBoxCount.Name = "chkBonusBoxCount";
            chkBonusBoxCount.Size = new Size(112, 19);
            chkBonusBoxCount.TabIndex = 2;
            chkBonusBoxCount.Text = "Enabled (Bonus)";
            chkBonusBoxCount.UseVisualStyleBackColor = false;
            chkBonusBoxCount.CheckedChanged += chkBonusBoxCount_CheckedChanged;
            // 
            // numBonusBoxCount
            // 
            numBonusBoxCount.Location = new Point(7, 105);
            numBonusBoxCount.Margin = new Padding(4, 3, 4, 3);
            numBonusBoxCount.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numBonusBoxCount.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numBonusBoxCount.Name = "numBonusBoxCount";
            numBonusBoxCount.Size = new Size(126, 23);
            numBonusBoxCount.TabIndex = 3;
            numBonusBoxCount.ValueChanged += numBonusBoxCount_ValueChanged;
            // 
            // chkBoxCount
            // 
            chkBoxCount.AutoSize = true;
            chkBoxCount.BackColor = Color.Transparent;
            chkBoxCount.Location = new Point(7, 22);
            chkBoxCount.Margin = new Padding(4, 3, 4, 3);
            chkBoxCount.Name = "chkBoxCount";
            chkBoxCount.Size = new Size(68, 19);
            chkBoxCount.TabIndex = 0;
            chkBoxCount.Text = "Enabled";
            chkBoxCount.UseVisualStyleBackColor = false;
            chkBoxCount.CheckedChanged += chkBoxCount_CheckedChanged;
            // 
            // numBoxCount
            // 
            numBoxCount.Location = new Point(7, 48);
            numBoxCount.Margin = new Padding(4, 3, 4, 3);
            numBoxCount.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numBoxCount.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numBoxCount.Name = "numBoxCount";
            numBoxCount.Size = new Size(126, 23);
            numBoxCount.TabIndex = 1;
            numBoxCount.ValueChanged += numBoxCount_ValueChanged;
            // 
            // fraVictims
            // 
            fraVictims.Controls.Add(numEditVictimID);
            fraVictims.Controls.Add(lbVictimID);
            fraVictims.Controls.Add(cmdClearAllVictims);
            fraVictims.Controls.Add(cmdRemoveVictim);
            fraVictims.Controls.Add(cmdInsertVictim);
            fraVictims.Controls.Add(lblVictimIndex);
            fraVictims.Location = new Point(4, 3);
            fraVictims.Margin = new Padding(4, 3, 4, 3);
            fraVictims.Name = "fraVictims";
            fraVictims.Padding = new Padding(4, 3, 4, 3);
            fraVictims.Size = new Size(154, 439);
            fraVictims.TabIndex = 7;
            fraVictims.TabStop = false;
            fraVictims.Text = "Victims";
            // 
            // numEditVictimID
            // 
            numEditVictimID.Enabled = false;
            numEditVictimID.Location = new Point(8, 115);
            numEditVictimID.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numEditVictimID.Name = "numEditVictimID";
            numEditVictimID.Size = new Size(138, 23);
            numEditVictimID.TabIndex = 15;
            // 
            // lbVictimID
            // 
            lbVictimID.BackColor = Color.FromArgb(26, 26, 28);
            lbVictimID.BorderStyle = BorderStyle.FixedSingle;
            lbVictimID.ForeColor = Color.FromArgb(213, 213, 213);
            lbVictimID.Location = new Point(8, 142);
            lbVictimID.Name = "lbVictimID";
            lbVictimID.Size = new Size(138, 287);
            lbVictimID.TabIndex = 14;
            lbVictimID.SelectedIndexChanged += lbVictimID_SelectedIndexChanged;
            lbVictimID.DoubleClick += lbVictimID_DoubleClick;
            lbVictimID.KeyDown += lbVictimID_KeyDown;
            lbVictimID.KeyPress += lbVictimID_KeyPress;
            // 
            // cmdClearAllVictims
            // 
            cmdClearAllVictims.BorderColour = Color.Empty;
            cmdClearAllVictims.CustomColour = false;
            cmdClearAllVictims.FlatBottom = false;
            cmdClearAllVictims.FlatTop = false;
            cmdClearAllVictims.Location = new Point(8, 82);
            cmdClearAllVictims.Margin = new Padding(4, 3, 4, 3);
            cmdClearAllVictims.Name = "cmdClearAllVictims";
            cmdClearAllVictims.Padding = new Padding(5);
            cmdClearAllVictims.Size = new Size(138, 27);
            cmdClearAllVictims.TabIndex = 5;
            cmdClearAllVictims.Text = "Clear All";
            cmdClearAllVictims.Click += cmdClearAllVictims_Click;
            // 
            // cmdRemoveVictim
            // 
            cmdRemoveVictim.BorderColour = Color.Empty;
            cmdRemoveVictim.CustomColour = false;
            cmdRemoveVictim.FlatBottom = false;
            cmdRemoveVictim.FlatTop = false;
            cmdRemoveVictim.Location = new Point(8, 49);
            cmdRemoveVictim.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveVictim.Name = "cmdRemoveVictim";
            cmdRemoveVictim.Padding = new Padding(5);
            cmdRemoveVictim.Size = new Size(67, 27);
            cmdRemoveVictim.TabIndex = 4;
            cmdRemoveVictim.Text = "Remove";
            cmdRemoveVictim.Click += cmdRemoveVictim_Click;
            // 
            // lblVictimIndex
            // 
            lblVictimIndex.BackColor = Color.Transparent;
            lblVictimIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVictimIndex.Location = new Point(8, 19);
            lblVictimIndex.Margin = new Padding(4, 0, 4, 0);
            lblVictimIndex.Name = "lblVictimIndex";
            lblVictimIndex.Size = new Size(138, 27);
            lblVictimIndex.TabIndex = 7;
            lblVictimIndex.Text = "?? / ??";
            lblVictimIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabCamera
            // 
            tabCamera.BackColor = Color.FromArgb(31, 31, 32);
            tabCamera.Controls.Add(fraFOV);
            tabCamera.Controls.Add(fraNeighbor);
            tabCamera.Controls.Add(fraAvgDist);
            tabCamera.Controls.Add(fraMode);
            tabCamera.Controls.Add(fraCameraSubIndex);
            tabCamera.Controls.Add(fraCameraIndex);
            tabCamera.Controls.Add(fraSLST);
            tabCamera.Location = new Point(4, 32);
            tabCamera.Margin = new Padding(4, 3, 4, 3);
            tabCamera.Name = "tabCamera";
            tabCamera.Padding = new Padding(4, 3, 4, 3);
            tabCamera.Size = new Size(592, 564);
            tabCamera.TabIndex = 2;
            tabCamera.Text = "Camera";
            tabCamera.Enter += tabCamera_Enter;
            // 
            // fraFOV
            // 
            fraFOV.Controls.Add(lblFOVPosition);
            fraFOV.Controls.Add(cmdRemoveFOVFrame);
            fraFOV.Controls.Add(cmdInsertFOVFrame);
            fraFOV.Controls.Add(numFOVPosition);
            fraFOV.Controls.Add(lblFOVFrame);
            fraFOV.Controls.Add(fraFOVFrame);
            fraFOV.Controls.Add(cmdPrevFOVFrame);
            fraFOV.Controls.Add(cmdNextFOVFrame);
            fraFOV.Location = new Point(337, 107);
            fraFOV.Margin = new Padding(4, 3, 4, 3);
            fraFOV.Name = "fraFOV";
            fraFOV.Padding = new Padding(4, 3, 4, 3);
            fraFOV.Size = new Size(176, 307);
            fraFOV.TabIndex = 21;
            fraFOV.TabStop = false;
            fraFOV.Text = "Field-of-View Timeline";
            // 
            // lblFOVPosition
            // 
            lblFOVPosition.AutoSize = true;
            lblFOVPosition.BackColor = Color.Transparent;
            lblFOVPosition.Location = new Point(7, 84);
            lblFOVPosition.Margin = new Padding(4, 0, 4, 0);
            lblFOVPosition.Name = "lblFOVPosition";
            lblFOVPosition.Size = new Size(50, 15);
            lblFOVPosition.TabIndex = 20;
            lblFOVPosition.Text = "Position";
            // 
            // cmdRemoveFOVFrame
            // 
            cmdRemoveFOVFrame.BorderColour = Color.Empty;
            cmdRemoveFOVFrame.CustomColour = false;
            cmdRemoveFOVFrame.FlatBottom = false;
            cmdRemoveFOVFrame.FlatTop = false;
            cmdRemoveFOVFrame.Location = new Point(8, 112);
            cmdRemoveFOVFrame.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveFOVFrame.Name = "cmdRemoveFOVFrame";
            cmdRemoveFOVFrame.Padding = new Padding(5);
            cmdRemoveFOVFrame.Size = new Size(77, 27);
            cmdRemoveFOVFrame.TabIndex = 16;
            cmdRemoveFOVFrame.Text = "Remove";
            cmdRemoveFOVFrame.Click += cmdRemoveFOVFrame_Click;
            // 
            // cmdInsertFOVFrame
            // 
            cmdInsertFOVFrame.BorderColour = Color.Empty;
            cmdInsertFOVFrame.CustomColour = false;
            cmdInsertFOVFrame.FlatBottom = false;
            cmdInsertFOVFrame.FlatTop = false;
            cmdInsertFOVFrame.Location = new Point(92, 112);
            cmdInsertFOVFrame.Margin = new Padding(4, 3, 4, 3);
            cmdInsertFOVFrame.Name = "cmdInsertFOVFrame";
            cmdInsertFOVFrame.Padding = new Padding(5);
            cmdInsertFOVFrame.Size = new Size(77, 27);
            cmdInsertFOVFrame.TabIndex = 17;
            cmdInsertFOVFrame.Text = "Insert";
            cmdInsertFOVFrame.Click += cmdInsertFOVFrame_Click;
            // 
            // numFOVPosition
            // 
            numFOVPosition.Location = new Point(65, 82);
            numFOVPosition.Margin = new Padding(4, 3, 4, 3);
            numFOVPosition.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numFOVPosition.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numFOVPosition.Name = "numFOVPosition";
            numFOVPosition.Size = new Size(79, 23);
            numFOVPosition.TabIndex = 19;
            numFOVPosition.ValueChanged += numFOVPosition_ValueChanged;
            // 
            // lblFOVFrame
            // 
            lblFOVFrame.BackColor = Color.Transparent;
            lblFOVFrame.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFOVFrame.Location = new Point(7, 52);
            lblFOVFrame.Margin = new Padding(4, 0, 4, 0);
            lblFOVFrame.Name = "lblFOVFrame";
            lblFOVFrame.Size = new Size(162, 27);
            lblFOVFrame.TabIndex = 17;
            lblFOVFrame.Text = "?? / ??";
            lblFOVFrame.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraFOVFrame
            // 
            fraFOVFrame.Controls.Add(lblFOVIndex);
            fraFOVFrame.Controls.Add(cmdRemoveFOV);
            fraFOVFrame.Controls.Add(cmdInsertFOV);
            fraFOVFrame.Controls.Add(cmdPrevFOV);
            fraFOVFrame.Controls.Add(cmdNextFOV);
            fraFOVFrame.Controls.Add(lblFOV);
            fraFOVFrame.Controls.Add(numFOV);
            fraFOVFrame.Location = new Point(7, 145);
            fraFOVFrame.Margin = new Padding(4, 3, 4, 3);
            fraFOVFrame.Name = "fraFOVFrame";
            fraFOVFrame.Padding = new Padding(4, 3, 4, 3);
            fraFOVFrame.Size = new Size(162, 148);
            fraFOVFrame.TabIndex = 15;
            fraFOVFrame.TabStop = false;
            fraFOVFrame.Text = "Field-of-View";
            // 
            // lblFOVIndex
            // 
            lblFOVIndex.BackColor = Color.Transparent;
            lblFOVIndex.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFOVIndex.Location = new Point(7, 52);
            lblFOVIndex.Margin = new Padding(4, 0, 4, 0);
            lblFOVIndex.Name = "lblFOVIndex";
            lblFOVIndex.Size = new Size(148, 27);
            lblFOVIndex.TabIndex = 21;
            lblFOVIndex.Text = "?? / ??";
            lblFOVIndex.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdRemoveFOV
            // 
            cmdRemoveFOV.BorderColour = Color.Empty;
            cmdRemoveFOV.CustomColour = false;
            cmdRemoveFOV.FlatBottom = false;
            cmdRemoveFOV.FlatTop = false;
            cmdRemoveFOV.Location = new Point(8, 82);
            cmdRemoveFOV.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveFOV.Name = "cmdRemoveFOV";
            cmdRemoveFOV.Padding = new Padding(5);
            cmdRemoveFOV.Size = new Size(70, 27);
            cmdRemoveFOV.TabIndex = 46;
            cmdRemoveFOV.Text = "Remove";
            cmdRemoveFOV.Click += cmdRemoveFOV_Click;
            // 
            // cmdInsertFOV
            // 
            cmdInsertFOV.BorderColour = Color.Empty;
            cmdInsertFOV.CustomColour = false;
            cmdInsertFOV.FlatBottom = false;
            cmdInsertFOV.FlatTop = false;
            cmdInsertFOV.Location = new Point(85, 82);
            cmdInsertFOV.Margin = new Padding(4, 3, 4, 3);
            cmdInsertFOV.Name = "cmdInsertFOV";
            cmdInsertFOV.Padding = new Padding(5);
            cmdInsertFOV.Size = new Size(70, 27);
            cmdInsertFOV.TabIndex = 47;
            cmdInsertFOV.Text = "Insert";
            cmdInsertFOV.Click += cmdInsertFOV_Click;
            // 
            // cmdPrevFOV
            // 
            cmdPrevFOV.BorderColour = Color.Empty;
            cmdPrevFOV.CustomColour = false;
            cmdPrevFOV.FlatBottom = false;
            cmdPrevFOV.FlatTop = false;
            cmdPrevFOV.Location = new Point(7, 22);
            cmdPrevFOV.Margin = new Padding(4, 3, 4, 3);
            cmdPrevFOV.Name = "cmdPrevFOV";
            cmdPrevFOV.Padding = new Padding(5);
            cmdPrevFOV.Size = new Size(70, 27);
            cmdPrevFOV.TabIndex = 22;
            cmdPrevFOV.Text = "Previous";
            cmdPrevFOV.Click += cmdPrevFOV_Click;
            // 
            // cmdNextFOV
            // 
            cmdNextFOV.BorderColour = Color.Empty;
            cmdNextFOV.CustomColour = false;
            cmdNextFOV.FlatBottom = false;
            cmdNextFOV.FlatTop = false;
            cmdNextFOV.Location = new Point(84, 22);
            cmdNextFOV.Margin = new Padding(4, 3, 4, 3);
            cmdNextFOV.Name = "cmdNextFOV";
            cmdNextFOV.Padding = new Padding(5);
            cmdNextFOV.Size = new Size(70, 27);
            cmdNextFOV.TabIndex = 23;
            cmdNextFOV.Text = "Next";
            cmdNextFOV.Click += cmdNextFOV_Click;
            // 
            // lblFOV
            // 
            lblFOV.AutoSize = true;
            lblFOV.BackColor = Color.Transparent;
            lblFOV.Location = new Point(7, 117);
            lblFOV.Margin = new Padding(4, 0, 4, 0);
            lblFOV.Name = "lblFOV";
            lblFOV.Size = new Size(29, 15);
            lblFOV.TabIndex = 39;
            lblFOV.Text = "FOV";
            // 
            // numFOV
            // 
            numFOV.Location = new Point(47, 114);
            numFOV.Margin = new Padding(4, 3, 4, 3);
            numFOV.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numFOV.Name = "numFOV";
            numFOV.Size = new Size(108, 23);
            numFOV.TabIndex = 38;
            numFOV.ValueChanged += numFOV_ValueChanged;
            // 
            // cmdPrevFOVFrame
            // 
            cmdPrevFOVFrame.BorderColour = Color.Empty;
            cmdPrevFOVFrame.CustomColour = false;
            cmdPrevFOVFrame.FlatBottom = false;
            cmdPrevFOVFrame.FlatTop = false;
            cmdPrevFOVFrame.Location = new Point(8, 22);
            cmdPrevFOVFrame.Margin = new Padding(4, 3, 4, 3);
            cmdPrevFOVFrame.Name = "cmdPrevFOVFrame";
            cmdPrevFOVFrame.Padding = new Padding(5);
            cmdPrevFOVFrame.Size = new Size(77, 27);
            cmdPrevFOVFrame.TabIndex = 15;
            cmdPrevFOVFrame.Text = "Previous";
            cmdPrevFOVFrame.Click += cmdPrevFOVFrame_Click;
            // 
            // cmdNextFOVFrame
            // 
            cmdNextFOVFrame.BorderColour = Color.Empty;
            cmdNextFOVFrame.CustomColour = false;
            cmdNextFOVFrame.FlatBottom = false;
            cmdNextFOVFrame.FlatTop = false;
            cmdNextFOVFrame.Location = new Point(92, 22);
            cmdNextFOVFrame.Margin = new Padding(4, 3, 4, 3);
            cmdNextFOVFrame.Name = "cmdNextFOVFrame";
            cmdNextFOVFrame.Padding = new Padding(5);
            cmdNextFOVFrame.Size = new Size(77, 27);
            cmdNextFOVFrame.TabIndex = 16;
            cmdNextFOVFrame.Text = "Next";
            cmdNextFOVFrame.Click += cmdNextFOVFrame_Click;
            // 
            // fraNeighbor
            // 
            fraNeighbor.Controls.Add(lblNeighborPosition);
            fraNeighbor.Controls.Add(cmdRemoveNeighbor);
            fraNeighbor.Controls.Add(cmdInsertNeighbor);
            fraNeighbor.Controls.Add(numNeighborPosition);
            fraNeighbor.Controls.Add(lblNeighbor);
            fraNeighbor.Controls.Add(fraNeighborSetting);
            fraNeighbor.Controls.Add(cmdPrevNeighbor);
            fraNeighbor.Controls.Add(cmdNextNeighbor);
            fraNeighbor.Location = new Point(154, 107);
            fraNeighbor.Margin = new Padding(4, 3, 4, 3);
            fraNeighbor.Name = "fraNeighbor";
            fraNeighbor.Padding = new Padding(4, 3, 4, 3);
            fraNeighbor.Size = new Size(176, 387);
            fraNeighbor.TabIndex = 8;
            fraNeighbor.TabStop = false;
            fraNeighbor.Text = "Neighbors";
            // 
            // lblNeighborPosition
            // 
            lblNeighborPosition.AutoSize = true;
            lblNeighborPosition.BackColor = Color.Transparent;
            lblNeighborPosition.Location = new Point(7, 84);
            lblNeighborPosition.Margin = new Padding(4, 0, 4, 0);
            lblNeighborPosition.Name = "lblNeighborPosition";
            lblNeighborPosition.Size = new Size(50, 15);
            lblNeighborPosition.TabIndex = 20;
            lblNeighborPosition.Text = "Position";
            // 
            // cmdRemoveNeighbor
            // 
            cmdRemoveNeighbor.BorderColour = Color.Empty;
            cmdRemoveNeighbor.CustomColour = false;
            cmdRemoveNeighbor.FlatBottom = false;
            cmdRemoveNeighbor.FlatTop = false;
            cmdRemoveNeighbor.Location = new Point(8, 112);
            cmdRemoveNeighbor.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveNeighbor.Name = "cmdRemoveNeighbor";
            cmdRemoveNeighbor.Padding = new Padding(5);
            cmdRemoveNeighbor.Size = new Size(77, 27);
            cmdRemoveNeighbor.TabIndex = 16;
            cmdRemoveNeighbor.Text = "Remove";
            cmdRemoveNeighbor.Click += cmdRemoveNeighbor_Click;
            // 
            // cmdInsertNeighbor
            // 
            cmdInsertNeighbor.BorderColour = Color.Empty;
            cmdInsertNeighbor.CustomColour = false;
            cmdInsertNeighbor.FlatBottom = false;
            cmdInsertNeighbor.FlatTop = false;
            cmdInsertNeighbor.Location = new Point(92, 112);
            cmdInsertNeighbor.Margin = new Padding(4, 3, 4, 3);
            cmdInsertNeighbor.Name = "cmdInsertNeighbor";
            cmdInsertNeighbor.Padding = new Padding(5);
            cmdInsertNeighbor.Size = new Size(77, 27);
            cmdInsertNeighbor.TabIndex = 17;
            cmdInsertNeighbor.Text = "Insert";
            cmdInsertNeighbor.Click += cmdInsertNeighbor_Click;
            // 
            // numNeighborPosition
            // 
            numNeighborPosition.Location = new Point(65, 82);
            numNeighborPosition.Margin = new Padding(4, 3, 4, 3);
            numNeighborPosition.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numNeighborPosition.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numNeighborPosition.Name = "numNeighborPosition";
            numNeighborPosition.Size = new Size(79, 23);
            numNeighborPosition.TabIndex = 19;
            numNeighborPosition.ValueChanged += numNeighborPosition_ValueChanged;
            // 
            // lblNeighbor
            // 
            lblNeighbor.BackColor = Color.Transparent;
            lblNeighbor.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNeighbor.Location = new Point(7, 52);
            lblNeighbor.Margin = new Padding(4, 0, 4, 0);
            lblNeighbor.Name = "lblNeighbor";
            lblNeighbor.Size = new Size(162, 27);
            lblNeighbor.TabIndex = 17;
            lblNeighbor.Text = "?? / ??";
            lblNeighbor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraNeighborSetting
            // 
            fraNeighborSetting.Controls.Add(lblNeighborSetting);
            fraNeighborSetting.Controls.Add(cmdRemoveNeighborSetting);
            fraNeighborSetting.Controls.Add(cmdInsertNeighborSetting);
            fraNeighborSetting.Controls.Add(cmdPrevNeighborSetting);
            fraNeighborSetting.Controls.Add(cmdNextNeighborSetting);
            fraNeighborSetting.Controls.Add(lblNeighborLink);
            fraNeighborSetting.Controls.Add(lblNeighborFlag);
            fraNeighborSetting.Controls.Add(numNeighborFlag);
            fraNeighborSetting.Controls.Add(lblNeighborCamera);
            fraNeighborSetting.Controls.Add(lblNeighborZone);
            fraNeighborSetting.Controls.Add(numNeighborLink);
            fraNeighborSetting.Controls.Add(numNeighborCamera);
            fraNeighborSetting.Controls.Add(numNeighborZone);
            fraNeighborSetting.Location = new Point(7, 145);
            fraNeighborSetting.Margin = new Padding(4, 3, 4, 3);
            fraNeighborSetting.Name = "fraNeighborSetting";
            fraNeighborSetting.Padding = new Padding(4, 3, 4, 3);
            fraNeighborSetting.Size = new Size(162, 234);
            fraNeighborSetting.TabIndex = 15;
            fraNeighborSetting.TabStop = false;
            fraNeighborSetting.Text = "Neighbor";
            // 
            // lblNeighborSetting
            // 
            lblNeighborSetting.BackColor = Color.Transparent;
            lblNeighborSetting.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNeighborSetting.Location = new Point(7, 52);
            lblNeighborSetting.Margin = new Padding(4, 0, 4, 0);
            lblNeighborSetting.Name = "lblNeighborSetting";
            lblNeighborSetting.Size = new Size(148, 27);
            lblNeighborSetting.TabIndex = 21;
            lblNeighborSetting.Text = "?? / ??";
            lblNeighborSetting.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdRemoveNeighborSetting
            // 
            cmdRemoveNeighborSetting.BorderColour = Color.Empty;
            cmdRemoveNeighborSetting.CustomColour = false;
            cmdRemoveNeighborSetting.FlatBottom = false;
            cmdRemoveNeighborSetting.FlatTop = false;
            cmdRemoveNeighborSetting.Location = new Point(8, 82);
            cmdRemoveNeighborSetting.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveNeighborSetting.Name = "cmdRemoveNeighborSetting";
            cmdRemoveNeighborSetting.Padding = new Padding(5);
            cmdRemoveNeighborSetting.Size = new Size(70, 27);
            cmdRemoveNeighborSetting.TabIndex = 46;
            cmdRemoveNeighborSetting.Text = "Remove";
            cmdRemoveNeighborSetting.Click += cmdRemoveNeighborSetting_Click;
            // 
            // cmdInsertNeighborSetting
            // 
            cmdInsertNeighborSetting.BorderColour = Color.Empty;
            cmdInsertNeighborSetting.CustomColour = false;
            cmdInsertNeighborSetting.FlatBottom = false;
            cmdInsertNeighborSetting.FlatTop = false;
            cmdInsertNeighborSetting.Location = new Point(85, 82);
            cmdInsertNeighborSetting.Margin = new Padding(4, 3, 4, 3);
            cmdInsertNeighborSetting.Name = "cmdInsertNeighborSetting";
            cmdInsertNeighborSetting.Padding = new Padding(5);
            cmdInsertNeighborSetting.Size = new Size(70, 27);
            cmdInsertNeighborSetting.TabIndex = 47;
            cmdInsertNeighborSetting.Text = "Insert";
            cmdInsertNeighborSetting.Click += cmdInsertNeighborSetting_Click;
            // 
            // cmdPrevNeighborSetting
            // 
            cmdPrevNeighborSetting.BorderColour = Color.Empty;
            cmdPrevNeighborSetting.CustomColour = false;
            cmdPrevNeighborSetting.FlatBottom = false;
            cmdPrevNeighborSetting.FlatTop = false;
            cmdPrevNeighborSetting.Location = new Point(7, 22);
            cmdPrevNeighborSetting.Margin = new Padding(4, 3, 4, 3);
            cmdPrevNeighborSetting.Name = "cmdPrevNeighborSetting";
            cmdPrevNeighborSetting.Padding = new Padding(5);
            cmdPrevNeighborSetting.Size = new Size(70, 27);
            cmdPrevNeighborSetting.TabIndex = 22;
            cmdPrevNeighborSetting.Text = "Previous";
            cmdPrevNeighborSetting.Click += cmdPrevNeighborSetting_Click;
            // 
            // cmdNextNeighborSetting
            // 
            cmdNextNeighborSetting.BorderColour = Color.Empty;
            cmdNextNeighborSetting.CustomColour = false;
            cmdNextNeighborSetting.FlatBottom = false;
            cmdNextNeighborSetting.FlatTop = false;
            cmdNextNeighborSetting.Location = new Point(84, 22);
            cmdNextNeighborSetting.Margin = new Padding(4, 3, 4, 3);
            cmdNextNeighborSetting.Name = "cmdNextNeighborSetting";
            cmdNextNeighborSetting.Padding = new Padding(5);
            cmdNextNeighborSetting.Size = new Size(70, 27);
            cmdNextNeighborSetting.TabIndex = 23;
            cmdNextNeighborSetting.Text = "Next";
            cmdNextNeighborSetting.Click += cmdNextNeighborSetting_Click;
            // 
            // lblNeighborLink
            // 
            lblNeighborLink.AutoSize = true;
            lblNeighborLink.BackColor = Color.Transparent;
            lblNeighborLink.Location = new Point(7, 207);
            lblNeighborLink.Margin = new Padding(4, 0, 4, 0);
            lblNeighborLink.Name = "lblNeighborLink";
            lblNeighborLink.Size = new Size(56, 15);
            lblNeighborLink.TabIndex = 45;
            lblNeighborLink.Text = "Link Type";
            // 
            // lblNeighborFlag
            // 
            lblNeighborFlag.AutoSize = true;
            lblNeighborFlag.BackColor = Color.Transparent;
            lblNeighborFlag.Location = new Point(7, 117);
            lblNeighborFlag.Margin = new Padding(4, 0, 4, 0);
            lblNeighborFlag.Name = "lblNeighborFlag";
            lblNeighborFlag.Size = new Size(29, 15);
            lblNeighborFlag.TabIndex = 39;
            lblNeighborFlag.Text = "Flag";
            // 
            // numNeighborFlag
            // 
            numNeighborFlag.Location = new Point(85, 114);
            numNeighborFlag.Margin = new Padding(4, 3, 4, 3);
            numNeighborFlag.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNeighborFlag.Name = "numNeighborFlag";
            numNeighborFlag.Size = new Size(70, 23);
            numNeighborFlag.TabIndex = 38;
            numNeighborFlag.ValueChanged += numNeighborFlag_ValueChanged;
            // 
            // lblNeighborCamera
            // 
            lblNeighborCamera.AutoSize = true;
            lblNeighborCamera.BackColor = Color.Transparent;
            lblNeighborCamera.Location = new Point(7, 147);
            lblNeighborCamera.Margin = new Padding(4, 0, 4, 0);
            lblNeighborCamera.Name = "lblNeighborCamera";
            lblNeighborCamera.Size = new Size(67, 15);
            lblNeighborCamera.TabIndex = 44;
            lblNeighborCamera.Text = "Cam. Index";
            // 
            // lblNeighborZone
            // 
            lblNeighborZone.AutoSize = true;
            lblNeighborZone.BackColor = Color.Transparent;
            lblNeighborZone.Location = new Point(7, 177);
            lblNeighborZone.Margin = new Padding(4, 0, 4, 0);
            lblNeighborZone.Name = "lblNeighborZone";
            lblNeighborZone.Size = new Size(66, 15);
            lblNeighborZone.TabIndex = 41;
            lblNeighborZone.Text = "Zone Index";
            // 
            // numNeighborLink
            // 
            numNeighborLink.Location = new Point(85, 204);
            numNeighborLink.Margin = new Padding(4, 3, 4, 3);
            numNeighborLink.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNeighborLink.Name = "numNeighborLink";
            numNeighborLink.Size = new Size(70, 23);
            numNeighborLink.TabIndex = 43;
            numNeighborLink.ValueChanged += numNeighborLink_ValueChanged;
            // 
            // numNeighborCamera
            // 
            numNeighborCamera.Location = new Point(85, 144);
            numNeighborCamera.Margin = new Padding(4, 3, 4, 3);
            numNeighborCamera.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNeighborCamera.Name = "numNeighborCamera";
            numNeighborCamera.Size = new Size(70, 23);
            numNeighborCamera.TabIndex = 40;
            numNeighborCamera.ValueChanged += numNeighborCamera_ValueChanged;
            // 
            // numNeighborZone
            // 
            numNeighborZone.Location = new Point(85, 174);
            numNeighborZone.Margin = new Padding(4, 3, 4, 3);
            numNeighborZone.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numNeighborZone.Name = "numNeighborZone";
            numNeighborZone.Size = new Size(70, 23);
            numNeighborZone.TabIndex = 42;
            numNeighborZone.ValueChanged += numNeighborZone_ValueChanged;
            // 
            // cmdPrevNeighbor
            // 
            cmdPrevNeighbor.BorderColour = Color.Empty;
            cmdPrevNeighbor.CustomColour = false;
            cmdPrevNeighbor.FlatBottom = false;
            cmdPrevNeighbor.FlatTop = false;
            cmdPrevNeighbor.Location = new Point(8, 22);
            cmdPrevNeighbor.Margin = new Padding(4, 3, 4, 3);
            cmdPrevNeighbor.Name = "cmdPrevNeighbor";
            cmdPrevNeighbor.Padding = new Padding(5);
            cmdPrevNeighbor.Size = new Size(77, 27);
            cmdPrevNeighbor.TabIndex = 15;
            cmdPrevNeighbor.Text = "Previous";
            cmdPrevNeighbor.Click += cmdPrevNeighbor_Click;
            // 
            // cmdNextNeighbor
            // 
            cmdNextNeighbor.BorderColour = Color.Empty;
            cmdNextNeighbor.CustomColour = false;
            cmdNextNeighbor.FlatBottom = false;
            cmdNextNeighbor.FlatTop = false;
            cmdNextNeighbor.Location = new Point(92, 22);
            cmdNextNeighbor.Margin = new Padding(4, 3, 4, 3);
            cmdNextNeighbor.Name = "cmdNextNeighbor";
            cmdNextNeighbor.Padding = new Padding(5);
            cmdNextNeighbor.Size = new Size(77, 27);
            cmdNextNeighbor.TabIndex = 16;
            cmdNextNeighbor.Text = "Next";
            cmdNextNeighbor.Click += cmdNextNeighbor_Click;
            // 
            // fraAvgDist
            // 
            fraAvgDist.Controls.Add(chkAvgDist);
            fraAvgDist.Controls.Add(numAvgDist);
            fraAvgDist.Location = new Point(7, 197);
            fraAvgDist.Margin = new Padding(4, 3, 4, 3);
            fraAvgDist.Name = "fraAvgDist";
            fraAvgDist.Padding = new Padding(4, 3, 4, 3);
            fraAvgDist.Size = new Size(140, 83);
            fraAvgDist.TabIndex = 7;
            fraAvgDist.TabStop = false;
            fraAvgDist.Text = "Point Distance";
            // 
            // chkAvgDist
            // 
            chkAvgDist.AutoSize = true;
            chkAvgDist.BackColor = Color.Transparent;
            chkAvgDist.Location = new Point(7, 22);
            chkAvgDist.Margin = new Padding(4, 3, 4, 3);
            chkAvgDist.Name = "chkAvgDist";
            chkAvgDist.Size = new Size(68, 19);
            chkAvgDist.TabIndex = 0;
            chkAvgDist.Text = "Enabled";
            chkAvgDist.UseVisualStyleBackColor = false;
            chkAvgDist.CheckedChanged += chkAvgDist_CheckedChanged;
            // 
            // numAvgDist
            // 
            numAvgDist.Location = new Point(7, 48);
            numAvgDist.Margin = new Padding(4, 3, 4, 3);
            numAvgDist.Maximum = new decimal(new int[] { 8388607, 0, 0, 0 });
            numAvgDist.Minimum = new decimal(new int[] { 8388608, 0, 0, int.MinValue });
            numAvgDist.Name = "numAvgDist";
            numAvgDist.Size = new Size(126, 23);
            numAvgDist.TabIndex = 1;
            numAvgDist.ValueChanged += numAvgDist_ValueChanged;
            // 
            // fraMode
            // 
            fraMode.Controls.Add(chkMode);
            fraMode.Controls.Add(numMode);
            fraMode.Enabled = false;
            fraMode.Location = new Point(7, 107);
            fraMode.Margin = new Padding(4, 3, 4, 3);
            fraMode.Name = "fraMode";
            fraMode.Padding = new Padding(4, 3, 4, 3);
            fraMode.Size = new Size(140, 83);
            fraMode.TabIndex = 6;
            fraMode.TabStop = false;
            fraMode.Text = "Camera Mode";
            // 
            // chkMode
            // 
            chkMode.AutoSize = true;
            chkMode.BackColor = Color.Transparent;
            chkMode.Location = new Point(7, 22);
            chkMode.Margin = new Padding(4, 3, 4, 3);
            chkMode.Name = "chkMode";
            chkMode.Size = new Size(68, 19);
            chkMode.TabIndex = 0;
            chkMode.Text = "Enabled";
            chkMode.UseVisualStyleBackColor = false;
            chkMode.CheckedChanged += chkMode_CheckedChanged;
            // 
            // numMode
            // 
            numMode.Location = new Point(7, 48);
            numMode.Margin = new Padding(4, 3, 4, 3);
            numMode.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numMode.Name = "numMode";
            numMode.Size = new Size(126, 23);
            numMode.TabIndex = 1;
            numMode.ValueChanged += numMode_ValueChanged;
            // 
            // fraCameraSubIndex
            // 
            fraCameraSubIndex.Controls.Add(chkCameraSubIndex);
            fraCameraSubIndex.Controls.Add(numCameraSubIndex);
            fraCameraSubIndex.Location = new Point(7, 377);
            fraCameraSubIndex.Margin = new Padding(4, 3, 4, 3);
            fraCameraSubIndex.Name = "fraCameraSubIndex";
            fraCameraSubIndex.Padding = new Padding(4, 3, 4, 3);
            fraCameraSubIndex.Size = new Size(140, 83);
            fraCameraSubIndex.TabIndex = 6;
            fraCameraSubIndex.TabStop = false;
            fraCameraSubIndex.Text = "Camera Subindex";
            fraCameraSubIndex.UseCompatibleTextRendering = true;
            // 
            // chkCameraSubIndex
            // 
            chkCameraSubIndex.AutoSize = true;
            chkCameraSubIndex.BackColor = Color.Transparent;
            chkCameraSubIndex.Location = new Point(7, 22);
            chkCameraSubIndex.Margin = new Padding(4, 3, 4, 3);
            chkCameraSubIndex.Name = "chkCameraSubIndex";
            chkCameraSubIndex.Size = new Size(68, 19);
            chkCameraSubIndex.TabIndex = 0;
            chkCameraSubIndex.Text = "Enabled";
            chkCameraSubIndex.UseVisualStyleBackColor = false;
            chkCameraSubIndex.CheckedChanged += chkCameraSubIndex_CheckedChanged;
            // 
            // numCameraSubIndex
            // 
            numCameraSubIndex.Location = new Point(7, 48);
            numCameraSubIndex.Margin = new Padding(4, 3, 4, 3);
            numCameraSubIndex.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numCameraSubIndex.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numCameraSubIndex.Name = "numCameraSubIndex";
            numCameraSubIndex.Size = new Size(126, 23);
            numCameraSubIndex.TabIndex = 1;
            numCameraSubIndex.ValueChanged += numCameraSubIndex_ValueChanged;
            // 
            // fraCameraIndex
            // 
            fraCameraIndex.Controls.Add(chkCameraIndex);
            fraCameraIndex.Controls.Add(numCameraIndex);
            fraCameraIndex.Location = new Point(7, 287);
            fraCameraIndex.Margin = new Padding(4, 3, 4, 3);
            fraCameraIndex.Name = "fraCameraIndex";
            fraCameraIndex.Padding = new Padding(4, 3, 4, 3);
            fraCameraIndex.Size = new Size(140, 83);
            fraCameraIndex.TabIndex = 5;
            fraCameraIndex.TabStop = false;
            fraCameraIndex.Text = "Camera Index";
            // 
            // chkCameraIndex
            // 
            chkCameraIndex.AutoSize = true;
            chkCameraIndex.BackColor = Color.Transparent;
            chkCameraIndex.Location = new Point(7, 22);
            chkCameraIndex.Margin = new Padding(4, 3, 4, 3);
            chkCameraIndex.Name = "chkCameraIndex";
            chkCameraIndex.Size = new Size(68, 19);
            chkCameraIndex.TabIndex = 0;
            chkCameraIndex.Text = "Enabled";
            chkCameraIndex.UseVisualStyleBackColor = false;
            chkCameraIndex.CheckedChanged += chkCameraIndex_CheckedChanged;
            // 
            // numCameraIndex
            // 
            numCameraIndex.Location = new Point(7, 48);
            numCameraIndex.Margin = new Padding(4, 3, 4, 3);
            numCameraIndex.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numCameraIndex.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numCameraIndex.Name = "numCameraIndex";
            numCameraIndex.Size = new Size(126, 23);
            numCameraIndex.TabIndex = 1;
            numCameraIndex.ValueChanged += numCameraIndex_ValueChanged;
            // 
            // fraSLST
            // 
            fraSLST.AutoSize = true;
            fraSLST.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSLST.Controls.Add(lblEIDErr1);
            fraSLST.Controls.Add(txtSLST);
            fraSLST.Controls.Add(chkSLST);
            fraSLST.Location = new Point(7, 7);
            fraSLST.Margin = new Padding(4, 3, 4, 3);
            fraSLST.Name = "fraSLST";
            fraSLST.Padding = new Padding(4, 3, 4, 3);
            fraSLST.Size = new Size(218, 93);
            fraSLST.TabIndex = 1;
            fraSLST.TabStop = false;
            fraSLST.Text = "Sort List";
            // 
            // lblEIDErr1
            // 
            lblEIDErr1.AutoSize = true;
            lblEIDErr1.BackColor = Color.Transparent;
            lblEIDErr1.ForeColor = Color.Red;
            lblEIDErr1.Location = new Point(86, 52);
            lblEIDErr1.Margin = new Padding(4, 0, 4, 0);
            lblEIDErr1.Name = "lblEIDErr1";
            lblEIDErr1.Size = new Size(124, 15);
            lblEIDErr1.TabIndex = 2;
            lblEIDErr1.Text = "EID ERROR WARNING!";
            // 
            // txtSLST
            // 
            txtSLST.BackColor = Color.FromArgb(26, 26, 28);
            txtSLST.BorderStyle = BorderStyle.FixedSingle;
            txtSLST.ForeColor = Color.FromArgb(213, 213, 213);
            txtSLST.Location = new Point(7, 48);
            txtSLST.Margin = new Padding(4, 3, 4, 3);
            txtSLST.MaxLength = 5;
            txtSLST.Name = "txtSLST";
            txtSLST.Size = new Size(72, 23);
            txtSLST.TabIndex = 1;
            txtSLST.Text = "NONE!";
            txtSLST.TextChanged += txtSLST_TextChanged;
            // 
            // chkSLST
            // 
            chkSLST.AutoSize = true;
            chkSLST.BackColor = Color.Transparent;
            chkSLST.Location = new Point(7, 22);
            chkSLST.Margin = new Padding(4, 3, 4, 3);
            chkSLST.Name = "chkSLST";
            chkSLST.Size = new Size(68, 19);
            chkSLST.TabIndex = 0;
            chkSLST.Text = "Enabled";
            chkSLST.UseVisualStyleBackColor = false;
            chkSLST.CheckedChanged += chkSLST_CheckedChanged;
            // 
            // tabLoadLists
            // 
            tabLoadLists.BackColor = Color.FromArgb(31, 31, 32);
            tabLoadLists.Controls.Add(lblEIDErrB);
            tabLoadLists.Controls.Add(fraLoadListPayload);
            tabLoadLists.Controls.Add(fraLoadListB);
            tabLoadLists.Controls.Add(lblEIDErrA);
            tabLoadLists.Controls.Add(fraLoadListA);
            tabLoadLists.Location = new Point(4, 32);
            tabLoadLists.Margin = new Padding(4, 3, 4, 3);
            tabLoadLists.Name = "tabLoadLists";
            tabLoadLists.Padding = new Padding(4, 3, 4, 3);
            tabLoadLists.Size = new Size(592, 564);
            tabLoadLists.TabIndex = 2;
            tabLoadLists.Text = "Load Lists";
            tabLoadLists.Enter += tabLoadLists_Enter;
            // 
            // lblEIDErrB
            // 
            lblEIDErrB.AutoSize = true;
            lblEIDErrB.BackColor = Color.Transparent;
            lblEIDErrB.ForeColor = Color.Red;
            lblEIDErrB.Location = new Point(184, 524);
            lblEIDErrB.Margin = new Padding(4, 0, 4, 0);
            lblEIDErrB.Name = "lblEIDErrB";
            lblEIDErrB.Size = new Size(124, 15);
            lblEIDErrB.TabIndex = 23;
            lblEIDErrB.Text = "EID ERROR WARNING!";
            // 
            // fraLoadListPayload
            // 
            fraLoadListPayload.Controls.Add(lblPayloadSound);
            fraLoadListPayload.Controls.Add(lblPayloadTexture);
            fraLoadListPayload.Controls.Add(lblVerifyLoadLists);
            fraLoadListPayload.Controls.Add(lblPayload);
            fraLoadListPayload.Controls.Add(cmdLoadListVerify);
            fraLoadListPayload.Controls.Add(cmdPayload);
            fraLoadListPayload.Controls.Add(lblPayloadPosition);
            fraLoadListPayload.Controls.Add(numPayloadPosition);
            fraLoadListPayload.Location = new Point(356, 7);
            fraLoadListPayload.Margin = new Padding(4, 3, 4, 3);
            fraLoadListPayload.Name = "fraLoadListPayload";
            fraLoadListPayload.Padding = new Padding(4, 3, 4, 3);
            fraLoadListPayload.Size = new Size(181, 232);
            fraLoadListPayload.TabIndex = 22;
            fraLoadListPayload.TabStop = false;
            fraLoadListPayload.Text = "Verify Load Lists";
            // 
            // lblPayloadSound
            // 
            lblPayloadSound.AutoSize = true;
            lblPayloadSound.BackColor = Color.Transparent;
            lblPayloadSound.Location = new Point(5, 187);
            lblPayloadSound.Margin = new Padding(4, 0, 4, 0);
            lblPayloadSound.Name = "lblPayloadSound";
            lblPayloadSound.Size = new Size(139, 30);
            lblPayloadSound.TabIndex = 27;
            lblPayloadSound.Text = "Payload is ?? - ??\r\nsound/wavebank chunks";
            lblPayloadSound.Visible = false;
            // 
            // lblPayloadTexture
            // 
            lblPayloadTexture.AutoSize = true;
            lblPayloadTexture.BackColor = Color.Transparent;
            lblPayloadTexture.Location = new Point(5, 171);
            lblPayloadTexture.Margin = new Padding(4, 0, 4, 0);
            lblPayloadTexture.Name = "lblPayloadTexture";
            lblPayloadTexture.Size = new Size(154, 15);
            lblPayloadTexture.TabIndex = 26;
            lblPayloadTexture.Text = "Payload is ?? texture chunks";
            lblPayloadTexture.Visible = false;
            // 
            // lblVerifyLoadLists
            // 
            lblVerifyLoadLists.AutoSize = true;
            lblVerifyLoadLists.BackColor = Color.Transparent;
            lblVerifyLoadLists.ForeColor = Color.MediumTurquoise;
            lblVerifyLoadLists.Location = new Point(7, 52);
            lblVerifyLoadLists.Margin = new Padding(4, 0, 4, 0);
            lblVerifyLoadLists.Name = "lblVerifyLoadLists";
            lblVerifyLoadLists.Size = new Size(118, 15);
            lblVerifyLoadLists.TabIndex = 25;
            lblVerifyLoadLists.Text = "Load lists are correct.";
            lblVerifyLoadLists.Visible = false;
            // 
            // lblPayload
            // 
            lblPayload.AutoSize = true;
            lblPayload.BackColor = Color.Transparent;
            lblPayload.Location = new Point(5, 155);
            lblPayload.Margin = new Padding(4, 0, 4, 0);
            lblPayload.Name = "lblPayload";
            lblPayload.Size = new Size(155, 15);
            lblPayload.TabIndex = 24;
            lblPayload.Text = "Payload is ?? normal chunks";
            lblPayload.Visible = false;
            // 
            // cmdLoadListVerify
            // 
            cmdLoadListVerify.BorderColour = Color.Empty;
            cmdLoadListVerify.CustomColour = false;
            cmdLoadListVerify.FlatBottom = false;
            cmdLoadListVerify.FlatTop = false;
            cmdLoadListVerify.Location = new Point(6, 22);
            cmdLoadListVerify.Margin = new Padding(4, 3, 4, 3);
            cmdLoadListVerify.Name = "cmdLoadListVerify";
            cmdLoadListVerify.Padding = new Padding(5);
            cmdLoadListVerify.Size = new Size(122, 27);
            cmdLoadListVerify.TabIndex = 0;
            cmdLoadListVerify.Text = "Verify List Integrity";
            cmdLoadListVerify.Click += cmdLoadListVerify_Click;
            // 
            // cmdPayload
            // 
            cmdPayload.BorderColour = Color.Empty;
            cmdPayload.CustomColour = false;
            cmdPayload.FlatBottom = false;
            cmdPayload.FlatTop = false;
            cmdPayload.Location = new Point(6, 81);
            cmdPayload.Margin = new Padding(4, 3, 4, 3);
            cmdPayload.Name = "cmdPayload";
            cmdPayload.Padding = new Padding(5);
            cmdPayload.Size = new Size(108, 27);
            cmdPayload.TabIndex = 23;
            cmdPayload.Text = "Check Payload";
            cmdPayload.Click += cmdPayload_Click;
            // 
            // lblPayloadPosition
            // 
            lblPayloadPosition.AutoSize = true;
            lblPayloadPosition.BackColor = Color.Transparent;
            lblPayloadPosition.Location = new Point(5, 123);
            lblPayloadPosition.Margin = new Padding(4, 0, 4, 0);
            lblPayloadPosition.Name = "lblPayloadPosition";
            lblPayloadPosition.Size = new Size(50, 15);
            lblPayloadPosition.TabIndex = 22;
            lblPayloadPosition.Text = "Position";
            // 
            // numPayloadPosition
            // 
            numPayloadPosition.Location = new Point(63, 120);
            numPayloadPosition.Margin = new Padding(4, 3, 4, 3);
            numPayloadPosition.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPayloadPosition.Name = "numPayloadPosition";
            numPayloadPosition.Size = new Size(79, 23);
            numPayloadPosition.TabIndex = 21;
            // 
            // fraLoadListB
            // 
            fraLoadListB.Controls.Add(lblMetavalueLoadB);
            fraLoadListB.Controls.Add(cmdRemoveRowB);
            fraLoadListB.Controls.Add(cmdInsertRowB);
            fraLoadListB.Controls.Add(numMetavalueLoadB);
            fraLoadListB.Controls.Add(lblLoadListRowIndexB);
            fraLoadListB.Controls.Add(fraEIDB);
            fraLoadListB.Controls.Add(cmdPrevRowB);
            fraLoadListB.Controls.Add(cmdNextRowB);
            fraLoadListB.Location = new Point(181, 7);
            fraLoadListB.Margin = new Padding(4, 3, 4, 3);
            fraLoadListB.Name = "fraLoadListB";
            fraLoadListB.Padding = new Padding(4, 3, 4, 3);
            fraLoadListB.Size = new Size(167, 514);
            fraLoadListB.TabIndex = 21;
            fraLoadListB.TabStop = false;
            fraLoadListB.Text = "Load List B";
            // 
            // lblMetavalueLoadB
            // 
            lblMetavalueLoadB.AutoSize = true;
            lblMetavalueLoadB.BackColor = Color.Transparent;
            lblMetavalueLoadB.Location = new Point(7, 87);
            lblMetavalueLoadB.Margin = new Padding(4, 0, 4, 0);
            lblMetavalueLoadB.Name = "lblMetavalueLoadB";
            lblMetavalueLoadB.Size = new Size(50, 15);
            lblMetavalueLoadB.TabIndex = 20;
            lblMetavalueLoadB.Text = "Position";
            // 
            // cmdRemoveRowB
            // 
            cmdRemoveRowB.BorderColour = Color.Empty;
            cmdRemoveRowB.CustomColour = false;
            cmdRemoveRowB.FlatBottom = false;
            cmdRemoveRowB.FlatTop = false;
            cmdRemoveRowB.Location = new Point(7, 114);
            cmdRemoveRowB.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveRowB.Name = "cmdRemoveRowB";
            cmdRemoveRowB.Padding = new Padding(5);
            cmdRemoveRowB.Size = new Size(75, 27);
            cmdRemoveRowB.TabIndex = 16;
            cmdRemoveRowB.Text = "Remove";
            cmdRemoveRowB.Click += cmdRemoveRowB_Click;
            // 
            // cmdInsertRowB
            // 
            cmdInsertRowB.BorderColour = Color.Empty;
            cmdInsertRowB.CustomColour = false;
            cmdInsertRowB.FlatBottom = false;
            cmdInsertRowB.FlatTop = false;
            cmdInsertRowB.Location = new Point(86, 114);
            cmdInsertRowB.Margin = new Padding(4, 3, 4, 3);
            cmdInsertRowB.Name = "cmdInsertRowB";
            cmdInsertRowB.Padding = new Padding(5);
            cmdInsertRowB.Size = new Size(74, 27);
            cmdInsertRowB.TabIndex = 17;
            cmdInsertRowB.Text = "Insert";
            cmdInsertRowB.Click += cmdInsertRowB_Click;
            // 
            // numMetavalueLoadB
            // 
            numMetavalueLoadB.Location = new Point(80, 84);
            numMetavalueLoadB.Margin = new Padding(4, 3, 4, 3);
            numMetavalueLoadB.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numMetavalueLoadB.Name = "numMetavalueLoadB";
            numMetavalueLoadB.Size = new Size(79, 23);
            numMetavalueLoadB.TabIndex = 19;
            numMetavalueLoadB.ValueChanged += numMetavalueLoadB_ValueChanged;
            // 
            // lblLoadListRowIndexB
            // 
            lblLoadListRowIndexB.BackColor = Color.Transparent;
            lblLoadListRowIndexB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoadListRowIndexB.Location = new Point(7, 52);
            lblLoadListRowIndexB.Margin = new Padding(4, 0, 4, 0);
            lblLoadListRowIndexB.Name = "lblLoadListRowIndexB";
            lblLoadListRowIndexB.Size = new Size(153, 27);
            lblLoadListRowIndexB.TabIndex = 17;
            lblLoadListRowIndexB.Text = "?? / ??";
            lblLoadListRowIndexB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraEIDB
            // 
            fraEIDB.Controls.Add(lbEIDB);
            fraEIDB.Controls.Add(txtEIDB);
            fraEIDB.Controls.Add(lblEIDIndexB);
            fraEIDB.Controls.Add(cmdAppendEIDB);
            fraEIDB.Controls.Add(cmdRemoveEIDB);
            fraEIDB.Controls.Add(cmdInsertEIDB);
            fraEIDB.Location = new Point(7, 152);
            fraEIDB.Margin = new Padding(4, 3, 4, 3);
            fraEIDB.Name = "fraEIDB";
            fraEIDB.Padding = new Padding(4, 3, 4, 3);
            fraEIDB.Size = new Size(153, 356);
            fraEIDB.TabIndex = 15;
            fraEIDB.TabStop = false;
            fraEIDB.Text = "Entries";
            // 
            // lbEIDB
            // 
            lbEIDB.BackColor = Color.FromArgb(26, 26, 28);
            lbEIDB.BorderStyle = BorderStyle.FixedSingle;
            lbEIDB.ForeColor = Color.FromArgb(213, 213, 213);
            lbEIDB.FormattingEnabled = true;
            lbEIDB.Location = new Point(7, 123);
            lbEIDB.Name = "lbEIDB";
            lbEIDB.Size = new Size(140, 227);
            lbEIDB.TabIndex = 25;
            lbEIDB.SelectedIndexChanged += lbEIDB_SelectedIndexChanged;
            lbEIDB.DoubleClick += lbEIDB_DoubleClick;
            lbEIDB.KeyDown += lbEIDB_KeyDown;
            lbEIDB.KeyPress += lbEIDB_KeyPress;
            // 
            // txtEIDB
            // 
            txtEIDB.BackColor = Color.FromArgb(26, 26, 28);
            txtEIDB.BorderStyle = BorderStyle.FixedSingle;
            txtEIDB.Enabled = false;
            txtEIDB.ForeColor = Color.FromArgb(213, 213, 213);
            txtEIDB.Location = new Point(7, 100);
            txtEIDB.Margin = new Padding(4, 3, 4, 3);
            txtEIDB.MaxLength = 5;
            txtEIDB.Name = "txtEIDB";
            txtEIDB.Size = new Size(140, 23);
            txtEIDB.TabIndex = 15;
            txtEIDB.TextChanged += txtEIDB_TextChanged;
            txtEIDB.LostFocus += txtEIDB_LostFocus;
            // 
            // lblEIDIndexB
            // 
            lblEIDIndexB.BackColor = Color.Transparent;
            lblEIDIndexB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEIDIndexB.Location = new Point(7, 9);
            lblEIDIndexB.Margin = new Padding(4, 0, 4, 0);
            lblEIDIndexB.Name = "lblEIDIndexB";
            lblEIDIndexB.Size = new Size(135, 27);
            lblEIDIndexB.TabIndex = 14;
            lblEIDIndexB.Text = "?? / ??";
            lblEIDIndexB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdAppendEIDB
            // 
            cmdAppendEIDB.BorderColour = Color.Empty;
            cmdAppendEIDB.CustomColour = false;
            cmdAppendEIDB.FlatBottom = false;
            cmdAppendEIDB.FlatTop = false;
            cmdAppendEIDB.Location = new Point(7, 68);
            cmdAppendEIDB.Margin = new Padding(4, 3, 4, 3);
            cmdAppendEIDB.Name = "cmdAppendEIDB";
            cmdAppendEIDB.Padding = new Padding(5);
            cmdAppendEIDB.Size = new Size(140, 27);
            cmdAppendEIDB.TabIndex = 12;
            cmdAppendEIDB.Text = "Append";
            cmdAppendEIDB.Click += cmdAppendEIDB_Click;
            // 
            // cmdRemoveEIDB
            // 
            cmdRemoveEIDB.BorderColour = Color.Empty;
            cmdRemoveEIDB.CustomColour = false;
            cmdRemoveEIDB.FlatBottom = false;
            cmdRemoveEIDB.FlatTop = false;
            cmdRemoveEIDB.Location = new Point(7, 35);
            cmdRemoveEIDB.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveEIDB.Name = "cmdRemoveEIDB";
            cmdRemoveEIDB.Padding = new Padding(5);
            cmdRemoveEIDB.Size = new Size(68, 27);
            cmdRemoveEIDB.TabIndex = 11;
            cmdRemoveEIDB.Text = "Remove";
            cmdRemoveEIDB.Click += cmdRemoveEIDB_Click;
            // 
            // cmdInsertEIDB
            // 
            cmdInsertEIDB.BorderColour = Color.Empty;
            cmdInsertEIDB.CustomColour = false;
            cmdInsertEIDB.FlatBottom = false;
            cmdInsertEIDB.FlatTop = false;
            cmdInsertEIDB.Location = new Point(79, 35);
            cmdInsertEIDB.Margin = new Padding(4, 3, 4, 3);
            cmdInsertEIDB.Name = "cmdInsertEIDB";
            cmdInsertEIDB.Padding = new Padding(5);
            cmdInsertEIDB.Size = new Size(68, 27);
            cmdInsertEIDB.TabIndex = 13;
            cmdInsertEIDB.Text = "Insert";
            cmdInsertEIDB.Click += cmdInsertEIDB_Click;
            // 
            // cmdPrevRowB
            // 
            cmdPrevRowB.BorderColour = Color.Empty;
            cmdPrevRowB.CustomColour = false;
            cmdPrevRowB.FlatBottom = false;
            cmdPrevRowB.FlatTop = false;
            cmdPrevRowB.Location = new Point(7, 22);
            cmdPrevRowB.Margin = new Padding(4, 3, 4, 3);
            cmdPrevRowB.Name = "cmdPrevRowB";
            cmdPrevRowB.Padding = new Padding(5);
            cmdPrevRowB.Size = new Size(75, 27);
            cmdPrevRowB.TabIndex = 15;
            cmdPrevRowB.Text = "Previous";
            cmdPrevRowB.Click += cmdPrevRowB_Click;
            // 
            // cmdNextRowB
            // 
            cmdNextRowB.BorderColour = Color.Empty;
            cmdNextRowB.CustomColour = false;
            cmdNextRowB.FlatBottom = false;
            cmdNextRowB.FlatTop = false;
            cmdNextRowB.Location = new Point(86, 22);
            cmdNextRowB.Margin = new Padding(4, 3, 4, 3);
            cmdNextRowB.Name = "cmdNextRowB";
            cmdNextRowB.Padding = new Padding(5);
            cmdNextRowB.Size = new Size(74, 27);
            cmdNextRowB.TabIndex = 16;
            cmdNextRowB.Text = "Next";
            cmdNextRowB.Click += cmdNextRowB_Click;
            // 
            // lblEIDErrA
            // 
            lblEIDErrA.AutoSize = true;
            lblEIDErrA.BackColor = Color.Transparent;
            lblEIDErrA.ForeColor = Color.Red;
            lblEIDErrA.Location = new Point(10, 524);
            lblEIDErrA.Margin = new Padding(4, 0, 4, 0);
            lblEIDErrA.Name = "lblEIDErrA";
            lblEIDErrA.Size = new Size(124, 15);
            lblEIDErrA.TabIndex = 4;
            lblEIDErrA.Text = "EID ERROR WARNING!";
            // 
            // fraLoadListA
            // 
            fraLoadListA.Controls.Add(lblMetavalueLoadA);
            fraLoadListA.Controls.Add(cmdRemoveRowA);
            fraLoadListA.Controls.Add(cmdInsertRowA);
            fraLoadListA.Controls.Add(numMetavalueLoadA);
            fraLoadListA.Controls.Add(lblLoadListRowIndexA);
            fraLoadListA.Controls.Add(fraEIDA);
            fraLoadListA.Controls.Add(cmdPrevRowA);
            fraLoadListA.Controls.Add(cmdNextRowA);
            fraLoadListA.Location = new Point(7, 7);
            fraLoadListA.Margin = new Padding(4, 3, 4, 3);
            fraLoadListA.Name = "fraLoadListA";
            fraLoadListA.Padding = new Padding(4, 3, 4, 3);
            fraLoadListA.Size = new Size(167, 514);
            fraLoadListA.TabIndex = 0;
            fraLoadListA.TabStop = false;
            fraLoadListA.Text = "Load List A";
            // 
            // lblMetavalueLoadA
            // 
            lblMetavalueLoadA.AutoSize = true;
            lblMetavalueLoadA.BackColor = Color.Transparent;
            lblMetavalueLoadA.Location = new Point(7, 87);
            lblMetavalueLoadA.Margin = new Padding(4, 0, 4, 0);
            lblMetavalueLoadA.Name = "lblMetavalueLoadA";
            lblMetavalueLoadA.Size = new Size(50, 15);
            lblMetavalueLoadA.TabIndex = 20;
            lblMetavalueLoadA.Text = "Position";
            // 
            // cmdRemoveRowA
            // 
            cmdRemoveRowA.BorderColour = Color.Empty;
            cmdRemoveRowA.CustomColour = false;
            cmdRemoveRowA.FlatBottom = false;
            cmdRemoveRowA.FlatTop = false;
            cmdRemoveRowA.Location = new Point(7, 114);
            cmdRemoveRowA.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveRowA.Name = "cmdRemoveRowA";
            cmdRemoveRowA.Padding = new Padding(5);
            cmdRemoveRowA.Size = new Size(75, 27);
            cmdRemoveRowA.TabIndex = 16;
            cmdRemoveRowA.Text = "Remove";
            cmdRemoveRowA.Click += cmdRemoveRowA_Click;
            // 
            // cmdInsertRowA
            // 
            cmdInsertRowA.BorderColour = Color.Empty;
            cmdInsertRowA.CustomColour = false;
            cmdInsertRowA.FlatBottom = false;
            cmdInsertRowA.FlatTop = false;
            cmdInsertRowA.Location = new Point(86, 114);
            cmdInsertRowA.Margin = new Padding(4, 3, 4, 3);
            cmdInsertRowA.Name = "cmdInsertRowA";
            cmdInsertRowA.Padding = new Padding(5);
            cmdInsertRowA.Size = new Size(74, 27);
            cmdInsertRowA.TabIndex = 17;
            cmdInsertRowA.Text = "Insert";
            cmdInsertRowA.Click += cmdInsertRowA_Click;
            // 
            // numMetavalueLoadA
            // 
            numMetavalueLoadA.Location = new Point(80, 84);
            numMetavalueLoadA.Margin = new Padding(4, 3, 4, 3);
            numMetavalueLoadA.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numMetavalueLoadA.Name = "numMetavalueLoadA";
            numMetavalueLoadA.Size = new Size(79, 23);
            numMetavalueLoadA.TabIndex = 19;
            numMetavalueLoadA.ValueChanged += numMetavalueLoadA_ValueChanged;
            // 
            // lblLoadListRowIndexA
            // 
            lblLoadListRowIndexA.BackColor = Color.Transparent;
            lblLoadListRowIndexA.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLoadListRowIndexA.Location = new Point(7, 52);
            lblLoadListRowIndexA.Margin = new Padding(4, 0, 4, 0);
            lblLoadListRowIndexA.Name = "lblLoadListRowIndexA";
            lblLoadListRowIndexA.Size = new Size(153, 27);
            lblLoadListRowIndexA.TabIndex = 17;
            lblLoadListRowIndexA.Text = "?? / ??";
            lblLoadListRowIndexA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraEIDA
            // 
            fraEIDA.Controls.Add(lbEIDA);
            fraEIDA.Controls.Add(txtEIDA);
            fraEIDA.Controls.Add(lblEIDIndexA);
            fraEIDA.Controls.Add(cmdAppendEIDA);
            fraEIDA.Controls.Add(cmdRemoveEIDA);
            fraEIDA.Controls.Add(cmdInsertEIDA);
            fraEIDA.Location = new Point(7, 152);
            fraEIDA.Margin = new Padding(4, 3, 4, 3);
            fraEIDA.Name = "fraEIDA";
            fraEIDA.Padding = new Padding(4, 3, 4, 3);
            fraEIDA.Size = new Size(153, 356);
            fraEIDA.TabIndex = 15;
            fraEIDA.TabStop = false;
            fraEIDA.Text = "Entries";
            // 
            // lbEIDA
            // 
            lbEIDA.BackColor = Color.FromArgb(26, 26, 28);
            lbEIDA.BorderStyle = BorderStyle.FixedSingle;
            lbEIDA.ForeColor = Color.FromArgb(213, 213, 213);
            lbEIDA.FormattingEnabled = true;
            lbEIDA.Location = new Point(7, 123);
            lbEIDA.Name = "lbEIDA";
            lbEIDA.Size = new Size(140, 227);
            lbEIDA.TabIndex = 24;
            lbEIDA.SelectedIndexChanged += lbEIDA_SelectedIndexChanged;
            lbEIDA.DoubleClick += lbEIDA_DoubleClick;
            lbEIDA.KeyDown += lbEIDA_KeyDown;
            lbEIDA.KeyPress += lbEIDA_KeyPress;
            // 
            // txtEIDA
            // 
            txtEIDA.BackColor = Color.FromArgb(26, 26, 28);
            txtEIDA.BorderStyle = BorderStyle.FixedSingle;
            txtEIDA.Enabled = false;
            txtEIDA.ForeColor = Color.FromArgb(213, 213, 213);
            txtEIDA.Location = new Point(7, 100);
            txtEIDA.Margin = new Padding(4, 3, 4, 3);
            txtEIDA.MaxLength = 5;
            txtEIDA.Name = "txtEIDA";
            txtEIDA.Size = new Size(140, 23);
            txtEIDA.TabIndex = 15;
            txtEIDA.TextChanged += txtEIDA_TextChanged;
            txtEIDA.LostFocus += txtEIDA_LostFocus;
            // 
            // lblEIDIndexA
            // 
            lblEIDIndexA.BackColor = Color.Transparent;
            lblEIDIndexA.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEIDIndexA.Location = new Point(7, 9);
            lblEIDIndexA.Margin = new Padding(4, 0, 4, 0);
            lblEIDIndexA.Name = "lblEIDIndexA";
            lblEIDIndexA.Size = new Size(140, 27);
            lblEIDIndexA.TabIndex = 14;
            lblEIDIndexA.Text = "?? / ??";
            lblEIDIndexA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdAppendEIDA
            // 
            cmdAppendEIDA.BorderColour = Color.Empty;
            cmdAppendEIDA.CustomColour = false;
            cmdAppendEIDA.FlatBottom = false;
            cmdAppendEIDA.FlatTop = false;
            cmdAppendEIDA.Location = new Point(7, 68);
            cmdAppendEIDA.Margin = new Padding(4, 3, 4, 3);
            cmdAppendEIDA.Name = "cmdAppendEIDA";
            cmdAppendEIDA.Padding = new Padding(5);
            cmdAppendEIDA.Size = new Size(140, 27);
            cmdAppendEIDA.TabIndex = 12;
            cmdAppendEIDA.Text = "Append";
            cmdAppendEIDA.Click += cmdAppendEIDA_Click;
            // 
            // cmdRemoveEIDA
            // 
            cmdRemoveEIDA.BorderColour = Color.Empty;
            cmdRemoveEIDA.CustomColour = false;
            cmdRemoveEIDA.FlatBottom = false;
            cmdRemoveEIDA.FlatTop = false;
            cmdRemoveEIDA.Location = new Point(7, 35);
            cmdRemoveEIDA.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveEIDA.Name = "cmdRemoveEIDA";
            cmdRemoveEIDA.Padding = new Padding(5);
            cmdRemoveEIDA.Size = new Size(68, 27);
            cmdRemoveEIDA.TabIndex = 11;
            cmdRemoveEIDA.Text = "Remove";
            cmdRemoveEIDA.Click += cmdRemoveEIDA_Click;
            // 
            // cmdInsertEIDA
            // 
            cmdInsertEIDA.BorderColour = Color.Empty;
            cmdInsertEIDA.CustomColour = false;
            cmdInsertEIDA.FlatBottom = false;
            cmdInsertEIDA.FlatTop = false;
            cmdInsertEIDA.Location = new Point(79, 35);
            cmdInsertEIDA.Margin = new Padding(4, 3, 4, 3);
            cmdInsertEIDA.Name = "cmdInsertEIDA";
            cmdInsertEIDA.Padding = new Padding(5);
            cmdInsertEIDA.Size = new Size(68, 27);
            cmdInsertEIDA.TabIndex = 13;
            cmdInsertEIDA.Text = "Insert";
            cmdInsertEIDA.Click += cmdInsertEIDA_Click;
            // 
            // cmdPrevRowA
            // 
            cmdPrevRowA.BorderColour = Color.Empty;
            cmdPrevRowA.CustomColour = false;
            cmdPrevRowA.FlatBottom = false;
            cmdPrevRowA.FlatTop = false;
            cmdPrevRowA.Location = new Point(7, 22);
            cmdPrevRowA.Margin = new Padding(4, 3, 4, 3);
            cmdPrevRowA.Name = "cmdPrevRowA";
            cmdPrevRowA.Padding = new Padding(5);
            cmdPrevRowA.Size = new Size(75, 27);
            cmdPrevRowA.TabIndex = 15;
            cmdPrevRowA.Text = "Previous";
            cmdPrevRowA.Click += cmdPrevRowA_Click;
            // 
            // cmdNextRowA
            // 
            cmdNextRowA.BorderColour = Color.Empty;
            cmdNextRowA.CustomColour = false;
            cmdNextRowA.FlatBottom = false;
            cmdNextRowA.FlatTop = false;
            cmdNextRowA.Location = new Point(86, 22);
            cmdNextRowA.Margin = new Padding(4, 3, 4, 3);
            cmdNextRowA.Name = "cmdNextRowA";
            cmdNextRowA.Padding = new Padding(5);
            cmdNextRowA.Size = new Size(74, 27);
            cmdNextRowA.TabIndex = 16;
            cmdNextRowA.Text = "Next";
            cmdNextRowA.Click += cmdNextRowA_Click;
            // 
            // tabDrawLists
            // 
            tabDrawLists.BackColor = Color.FromArgb(31, 31, 32);
            tabDrawLists.Controls.Add(fraVerifyDrawList);
            tabDrawLists.Controls.Add(fraDrawListB);
            tabDrawLists.Controls.Add(fraDrawListA);
            tabDrawLists.Location = new Point(4, 32);
            tabDrawLists.Margin = new Padding(4, 3, 4, 3);
            tabDrawLists.Name = "tabDrawLists";
            tabDrawLists.Padding = new Padding(4, 3, 4, 3);
            tabDrawLists.Size = new Size(592, 564);
            tabDrawLists.TabIndex = 3;
            tabDrawLists.Text = "Draw Lists";
            tabDrawLists.Enter += tabDrawLists_Enter;
            // 
            // fraVerifyDrawList
            // 
            fraVerifyDrawList.Controls.Add(lblVerifyDrawLists);
            fraVerifyDrawList.Controls.Add(cmdVerifyDrawList);
            fraVerifyDrawList.Location = new Point(356, 7);
            fraVerifyDrawList.Name = "fraVerifyDrawList";
            fraVerifyDrawList.Size = new Size(136, 79);
            fraVerifyDrawList.TabIndex = 22;
            fraVerifyDrawList.TabStop = false;
            fraVerifyDrawList.Text = "Verify Draw Lists";
            // 
            // lblVerifyDrawLists
            // 
            lblVerifyDrawLists.AutoSize = true;
            lblVerifyDrawLists.BackColor = Color.Transparent;
            lblVerifyDrawLists.ForeColor = Color.Turquoise;
            lblVerifyDrawLists.Location = new Point(7, 52);
            lblVerifyDrawLists.Margin = new Padding(4, 0, 4, 0);
            lblVerifyDrawLists.Name = "lblVerifyDrawLists";
            lblVerifyDrawLists.Size = new Size(119, 15);
            lblVerifyDrawLists.TabIndex = 21;
            lblVerifyDrawLists.Text = "Draw lists are correct.";
            lblVerifyDrawLists.Visible = false;
            // 
            // cmdVerifyDrawList
            // 
            cmdVerifyDrawList.BorderColour = Color.Empty;
            cmdVerifyDrawList.CustomColour = false;
            cmdVerifyDrawList.FlatBottom = false;
            cmdVerifyDrawList.FlatTop = false;
            cmdVerifyDrawList.Location = new Point(6, 22);
            cmdVerifyDrawList.Name = "cmdVerifyDrawList";
            cmdVerifyDrawList.Padding = new Padding(5);
            cmdVerifyDrawList.Size = new Size(122, 27);
            cmdVerifyDrawList.TabIndex = 0;
            cmdVerifyDrawList.Text = "Verify List Integrity";
            cmdVerifyDrawList.Click += cmdVerifyDrawList_Click;
            // 
            // fraDrawListB
            // 
            fraDrawListB.Controls.Add(lblMetavalueDrawB);
            fraDrawListB.Controls.Add(cmdRemoveRowDrawB);
            fraDrawListB.Controls.Add(cmdInsertRowDrawB);
            fraDrawListB.Controls.Add(numMetavalueDrawB);
            fraDrawListB.Controls.Add(lblDrawListRowIndexB);
            fraDrawListB.Controls.Add(fraEntityB);
            fraDrawListB.Controls.Add(cmdPrevRowDrawB);
            fraDrawListB.Controls.Add(cmdNextRowDrawB);
            fraDrawListB.Location = new Point(181, 7);
            fraDrawListB.Margin = new Padding(4, 3, 4, 3);
            fraDrawListB.Name = "fraDrawListB";
            fraDrawListB.Padding = new Padding(4, 3, 4, 3);
            fraDrawListB.Size = new Size(167, 514);
            fraDrawListB.TabIndex = 21;
            fraDrawListB.TabStop = false;
            fraDrawListB.Text = "Draw List B";
            // 
            // lblMetavalueDrawB
            // 
            lblMetavalueDrawB.AutoSize = true;
            lblMetavalueDrawB.BackColor = Color.Transparent;
            lblMetavalueDrawB.Location = new Point(7, 87);
            lblMetavalueDrawB.Margin = new Padding(4, 0, 4, 0);
            lblMetavalueDrawB.Name = "lblMetavalueDrawB";
            lblMetavalueDrawB.Size = new Size(50, 15);
            lblMetavalueDrawB.TabIndex = 20;
            lblMetavalueDrawB.Text = "Position";
            // 
            // cmdRemoveRowDrawB
            // 
            cmdRemoveRowDrawB.BorderColour = Color.Empty;
            cmdRemoveRowDrawB.CustomColour = false;
            cmdRemoveRowDrawB.FlatBottom = false;
            cmdRemoveRowDrawB.FlatTop = false;
            cmdRemoveRowDrawB.Location = new Point(7, 114);
            cmdRemoveRowDrawB.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveRowDrawB.Name = "cmdRemoveRowDrawB";
            cmdRemoveRowDrawB.Padding = new Padding(5);
            cmdRemoveRowDrawB.Size = new Size(75, 27);
            cmdRemoveRowDrawB.TabIndex = 16;
            cmdRemoveRowDrawB.Text = "Remove";
            cmdRemoveRowDrawB.Click += cmdRemoveRowDrawB_Click;
            // 
            // cmdInsertRowDrawB
            // 
            cmdInsertRowDrawB.BorderColour = Color.Empty;
            cmdInsertRowDrawB.CustomColour = false;
            cmdInsertRowDrawB.FlatBottom = false;
            cmdInsertRowDrawB.FlatTop = false;
            cmdInsertRowDrawB.Location = new Point(86, 114);
            cmdInsertRowDrawB.Margin = new Padding(4, 3, 4, 3);
            cmdInsertRowDrawB.Name = "cmdInsertRowDrawB";
            cmdInsertRowDrawB.Padding = new Padding(5);
            cmdInsertRowDrawB.Size = new Size(74, 27);
            cmdInsertRowDrawB.TabIndex = 17;
            cmdInsertRowDrawB.Text = "Insert";
            cmdInsertRowDrawB.Click += cmdInsertRowDrawB_Click;
            // 
            // numMetavalueDrawB
            // 
            numMetavalueDrawB.Location = new Point(80, 84);
            numMetavalueDrawB.Margin = new Padding(4, 3, 4, 3);
            numMetavalueDrawB.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numMetavalueDrawB.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numMetavalueDrawB.Name = "numMetavalueDrawB";
            numMetavalueDrawB.Size = new Size(79, 23);
            numMetavalueDrawB.TabIndex = 19;
            numMetavalueDrawB.ValueChanged += numMetavalueDrawB_ValueChanged;
            // 
            // lblDrawListRowIndexB
            // 
            lblDrawListRowIndexB.BackColor = Color.Transparent;
            lblDrawListRowIndexB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDrawListRowIndexB.Location = new Point(7, 52);
            lblDrawListRowIndexB.Margin = new Padding(4, 0, 4, 0);
            lblDrawListRowIndexB.Name = "lblDrawListRowIndexB";
            lblDrawListRowIndexB.Size = new Size(153, 27);
            lblDrawListRowIndexB.TabIndex = 17;
            lblDrawListRowIndexB.Text = "?? / ??";
            lblDrawListRowIndexB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraEntityB
            // 
            fraEntityB.Controls.Add(lbEntityB);
            fraEntityB.Controls.Add(numEntityB);
            fraEntityB.Controls.Add(lblEntityIndexB);
            fraEntityB.Controls.Add(cmdAppendEntityB);
            fraEntityB.Controls.Add(cmdRemoveEntityB);
            fraEntityB.Controls.Add(cmdInsertEntityB);
            fraEntityB.Location = new Point(7, 152);
            fraEntityB.Margin = new Padding(4, 3, 4, 3);
            fraEntityB.Name = "fraEntityB";
            fraEntityB.Padding = new Padding(4, 3, 4, 3);
            fraEntityB.Size = new Size(153, 356);
            fraEntityB.TabIndex = 15;
            fraEntityB.TabStop = false;
            fraEntityB.Text = "Entities";
            // 
            // lbEntityB
            // 
            lbEntityB.BackColor = Color.FromArgb(26, 26, 28);
            lbEntityB.BorderStyle = BorderStyle.FixedSingle;
            lbEntityB.ForeColor = Color.FromArgb(213, 213, 213);
            lbEntityB.FormattingEnabled = true;
            lbEntityB.Location = new Point(7, 123);
            lbEntityB.Name = "lbEntityB";
            lbEntityB.Size = new Size(139, 227);
            lbEntityB.TabIndex = 22;
            lbEntityB.SelectedIndexChanged += lbEntityB_SelectedIndexChanged;
            lbEntityB.DoubleClick += lbEntityB_DoubleClick;
            lbEntityB.KeyDown += lbEntityB_KeyDown;
            lbEntityB.KeyPress += lbEntityB_KeyPress;
            // 
            // numEntityB
            // 
            numEntityB.Enabled = false;
            numEntityB.Location = new Point(7, 100);
            numEntityB.Margin = new Padding(4, 3, 4, 3);
            numEntityB.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numEntityB.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numEntityB.Name = "numEntityB";
            numEntityB.Size = new Size(140, 23);
            numEntityB.TabIndex = 21;
            numEntityB.ValueChanged += numEntityB_ValueChanged;
            // 
            // lblEntityIndexB
            // 
            lblEntityIndexB.BackColor = Color.Transparent;
            lblEntityIndexB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEntityIndexB.Location = new Point(7, 9);
            lblEntityIndexB.Margin = new Padding(4, 0, 4, 0);
            lblEntityIndexB.Name = "lblEntityIndexB";
            lblEntityIndexB.Size = new Size(140, 27);
            lblEntityIndexB.TabIndex = 14;
            lblEntityIndexB.Text = "?? / ??";
            lblEntityIndexB.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdAppendEntityB
            // 
            cmdAppendEntityB.BorderColour = Color.Empty;
            cmdAppendEntityB.CustomColour = false;
            cmdAppendEntityB.FlatBottom = false;
            cmdAppendEntityB.FlatTop = false;
            cmdAppendEntityB.Location = new Point(7, 68);
            cmdAppendEntityB.Margin = new Padding(4, 3, 4, 3);
            cmdAppendEntityB.Name = "cmdAppendEntityB";
            cmdAppendEntityB.Padding = new Padding(5);
            cmdAppendEntityB.Size = new Size(140, 27);
            cmdAppendEntityB.TabIndex = 12;
            cmdAppendEntityB.Text = "Append";
            cmdAppendEntityB.Click += cmdAppendEntityB_Click;
            // 
            // cmdRemoveEntityB
            // 
            cmdRemoveEntityB.BorderColour = Color.Empty;
            cmdRemoveEntityB.CustomColour = false;
            cmdRemoveEntityB.FlatBottom = false;
            cmdRemoveEntityB.FlatTop = false;
            cmdRemoveEntityB.Location = new Point(7, 35);
            cmdRemoveEntityB.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveEntityB.Name = "cmdRemoveEntityB";
            cmdRemoveEntityB.Padding = new Padding(5);
            cmdRemoveEntityB.Size = new Size(68, 27);
            cmdRemoveEntityB.TabIndex = 11;
            cmdRemoveEntityB.Text = "Remove";
            cmdRemoveEntityB.Click += cmdRemoveEntityB_Click;
            // 
            // cmdInsertEntityB
            // 
            cmdInsertEntityB.BorderColour = Color.Empty;
            cmdInsertEntityB.CustomColour = false;
            cmdInsertEntityB.FlatBottom = false;
            cmdInsertEntityB.FlatTop = false;
            cmdInsertEntityB.Location = new Point(79, 35);
            cmdInsertEntityB.Margin = new Padding(4, 3, 4, 3);
            cmdInsertEntityB.Name = "cmdInsertEntityB";
            cmdInsertEntityB.Padding = new Padding(5);
            cmdInsertEntityB.Size = new Size(68, 27);
            cmdInsertEntityB.TabIndex = 13;
            cmdInsertEntityB.Text = "Insert";
            cmdInsertEntityB.Click += cmdInsertEntityB_Click;
            // 
            // cmdPrevRowDrawB
            // 
            cmdPrevRowDrawB.BorderColour = Color.Empty;
            cmdPrevRowDrawB.CustomColour = false;
            cmdPrevRowDrawB.FlatBottom = false;
            cmdPrevRowDrawB.FlatTop = false;
            cmdPrevRowDrawB.Location = new Point(7, 22);
            cmdPrevRowDrawB.Margin = new Padding(4, 3, 4, 3);
            cmdPrevRowDrawB.Name = "cmdPrevRowDrawB";
            cmdPrevRowDrawB.Padding = new Padding(5);
            cmdPrevRowDrawB.Size = new Size(75, 27);
            cmdPrevRowDrawB.TabIndex = 15;
            cmdPrevRowDrawB.Text = "Previous";
            cmdPrevRowDrawB.Click += cmdPrevRowDrawB_Click;
            // 
            // cmdNextRowDrawB
            // 
            cmdNextRowDrawB.BorderColour = Color.Empty;
            cmdNextRowDrawB.CustomColour = false;
            cmdNextRowDrawB.FlatBottom = false;
            cmdNextRowDrawB.FlatTop = false;
            cmdNextRowDrawB.Location = new Point(86, 22);
            cmdNextRowDrawB.Margin = new Padding(4, 3, 4, 3);
            cmdNextRowDrawB.Name = "cmdNextRowDrawB";
            cmdNextRowDrawB.Padding = new Padding(5);
            cmdNextRowDrawB.Size = new Size(74, 27);
            cmdNextRowDrawB.TabIndex = 16;
            cmdNextRowDrawB.Text = "Next";
            cmdNextRowDrawB.Click += cmdNextRowDrawB_Click;
            // 
            // fraDrawListA
            // 
            fraDrawListA.Controls.Add(lblMetavalueDrawA);
            fraDrawListA.Controls.Add(cmdRemoveRowDrawA);
            fraDrawListA.Controls.Add(cmdInsertRowDrawA);
            fraDrawListA.Controls.Add(numMetavalueDrawA);
            fraDrawListA.Controls.Add(lblDrawListRowIndexA);
            fraDrawListA.Controls.Add(fraEntityA);
            fraDrawListA.Controls.Add(cmdPrevRowDrawA);
            fraDrawListA.Controls.Add(cmdNextRowDrawA);
            fraDrawListA.Location = new Point(7, 7);
            fraDrawListA.Margin = new Padding(4, 3, 4, 3);
            fraDrawListA.Name = "fraDrawListA";
            fraDrawListA.Padding = new Padding(4, 3, 4, 3);
            fraDrawListA.Size = new Size(167, 514);
            fraDrawListA.TabIndex = 1;
            fraDrawListA.TabStop = false;
            fraDrawListA.Text = "Draw List A";
            // 
            // lblMetavalueDrawA
            // 
            lblMetavalueDrawA.AutoSize = true;
            lblMetavalueDrawA.BackColor = Color.Transparent;
            lblMetavalueDrawA.Location = new Point(7, 87);
            lblMetavalueDrawA.Margin = new Padding(4, 0, 4, 0);
            lblMetavalueDrawA.Name = "lblMetavalueDrawA";
            lblMetavalueDrawA.Size = new Size(50, 15);
            lblMetavalueDrawA.TabIndex = 20;
            lblMetavalueDrawA.Text = "Position";
            // 
            // cmdRemoveRowDrawA
            // 
            cmdRemoveRowDrawA.BorderColour = Color.Empty;
            cmdRemoveRowDrawA.CustomColour = false;
            cmdRemoveRowDrawA.FlatBottom = false;
            cmdRemoveRowDrawA.FlatTop = false;
            cmdRemoveRowDrawA.Location = new Point(7, 114);
            cmdRemoveRowDrawA.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveRowDrawA.Name = "cmdRemoveRowDrawA";
            cmdRemoveRowDrawA.Padding = new Padding(5);
            cmdRemoveRowDrawA.Size = new Size(75, 27);
            cmdRemoveRowDrawA.TabIndex = 16;
            cmdRemoveRowDrawA.Text = "Remove";
            cmdRemoveRowDrawA.Click += cmdRemoveRowDrawA_Click;
            // 
            // cmdInsertRowDrawA
            // 
            cmdInsertRowDrawA.BorderColour = Color.Empty;
            cmdInsertRowDrawA.CustomColour = false;
            cmdInsertRowDrawA.FlatBottom = false;
            cmdInsertRowDrawA.FlatTop = false;
            cmdInsertRowDrawA.Location = new Point(86, 114);
            cmdInsertRowDrawA.Margin = new Padding(4, 3, 4, 3);
            cmdInsertRowDrawA.Name = "cmdInsertRowDrawA";
            cmdInsertRowDrawA.Padding = new Padding(5);
            cmdInsertRowDrawA.Size = new Size(74, 27);
            cmdInsertRowDrawA.TabIndex = 17;
            cmdInsertRowDrawA.Text = "Insert";
            cmdInsertRowDrawA.Click += cmdInsertRowDrawA_Click;
            // 
            // numMetavalueDrawA
            // 
            numMetavalueDrawA.Location = new Point(80, 84);
            numMetavalueDrawA.Margin = new Padding(4, 3, 4, 3);
            numMetavalueDrawA.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numMetavalueDrawA.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numMetavalueDrawA.Name = "numMetavalueDrawA";
            numMetavalueDrawA.Size = new Size(79, 23);
            numMetavalueDrawA.TabIndex = 19;
            numMetavalueDrawA.ValueChanged += numMetavalueDrawA_ValueChanged;
            // 
            // lblDrawListRowIndexA
            // 
            lblDrawListRowIndexA.BackColor = Color.Transparent;
            lblDrawListRowIndexA.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDrawListRowIndexA.Location = new Point(7, 52);
            lblDrawListRowIndexA.Margin = new Padding(4, 0, 4, 0);
            lblDrawListRowIndexA.Name = "lblDrawListRowIndexA";
            lblDrawListRowIndexA.Size = new Size(153, 27);
            lblDrawListRowIndexA.TabIndex = 17;
            lblDrawListRowIndexA.Text = "?? / ??";
            lblDrawListRowIndexA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fraEntityA
            // 
            fraEntityA.Controls.Add(lbEntityA);
            fraEntityA.Controls.Add(numEntityA);
            fraEntityA.Controls.Add(lblEntityIndexA);
            fraEntityA.Controls.Add(cmdAppendEntityA);
            fraEntityA.Controls.Add(cmdRemoveEntityA);
            fraEntityA.Controls.Add(cmdInsertEntityA);
            fraEntityA.Location = new Point(7, 152);
            fraEntityA.Margin = new Padding(4, 3, 4, 3);
            fraEntityA.Name = "fraEntityA";
            fraEntityA.Padding = new Padding(4, 3, 4, 3);
            fraEntityA.Size = new Size(153, 356);
            fraEntityA.TabIndex = 15;
            fraEntityA.TabStop = false;
            fraEntityA.Text = "Entities";
            // 
            // lbEntityA
            // 
            lbEntityA.BackColor = Color.FromArgb(26, 26, 28);
            lbEntityA.BorderStyle = BorderStyle.FixedSingle;
            lbEntityA.ForeColor = Color.FromArgb(213, 213, 213);
            lbEntityA.FormattingEnabled = true;
            lbEntityA.Location = new Point(7, 123);
            lbEntityA.Name = "lbEntityA";
            lbEntityA.Size = new Size(139, 227);
            lbEntityA.TabIndex = 22;
            lbEntityA.SelectedIndexChanged += lbEntityA_SelectedIndexChanged;
            lbEntityA.DoubleClick += lbEntityA_DoubleClick;
            lbEntityA.KeyDown += lbEntityA_KeyDown;
            lbEntityA.KeyPress += lbEntityA_KeyPress;
            // 
            // numEntityA
            // 
            numEntityA.Enabled = false;
            numEntityA.Location = new Point(7, 100);
            numEntityA.Margin = new Padding(4, 3, 4, 3);
            numEntityA.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numEntityA.Minimum = new decimal(new int[] { 32768, 0, 0, int.MinValue });
            numEntityA.Name = "numEntityA";
            numEntityA.Size = new Size(140, 23);
            numEntityA.TabIndex = 21;
            numEntityA.ValueChanged += numEntityA_ValueChanged;
            // 
            // lblEntityIndexA
            // 
            lblEntityIndexA.BackColor = Color.Transparent;
            lblEntityIndexA.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEntityIndexA.Location = new Point(7, 9);
            lblEntityIndexA.Margin = new Padding(4, 0, 4, 0);
            lblEntityIndexA.Name = "lblEntityIndexA";
            lblEntityIndexA.Size = new Size(140, 27);
            lblEntityIndexA.TabIndex = 14;
            lblEntityIndexA.Text = "?? / ??";
            lblEntityIndexA.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmdAppendEntityA
            // 
            cmdAppendEntityA.BorderColour = Color.Empty;
            cmdAppendEntityA.CustomColour = false;
            cmdAppendEntityA.FlatBottom = false;
            cmdAppendEntityA.FlatTop = false;
            cmdAppendEntityA.Location = new Point(7, 68);
            cmdAppendEntityA.Margin = new Padding(4, 3, 4, 3);
            cmdAppendEntityA.Name = "cmdAppendEntityA";
            cmdAppendEntityA.Padding = new Padding(5);
            cmdAppendEntityA.Size = new Size(140, 27);
            cmdAppendEntityA.TabIndex = 12;
            cmdAppendEntityA.Text = "Append";
            cmdAppendEntityA.Click += cmdAppendEntityA_Click;
            // 
            // cmdRemoveEntityA
            // 
            cmdRemoveEntityA.BorderColour = Color.Empty;
            cmdRemoveEntityA.CustomColour = false;
            cmdRemoveEntityA.FlatBottom = false;
            cmdRemoveEntityA.FlatTop = false;
            cmdRemoveEntityA.Location = new Point(7, 35);
            cmdRemoveEntityA.Margin = new Padding(4, 3, 4, 3);
            cmdRemoveEntityA.Name = "cmdRemoveEntityA";
            cmdRemoveEntityA.Padding = new Padding(5);
            cmdRemoveEntityA.Size = new Size(68, 27);
            cmdRemoveEntityA.TabIndex = 11;
            cmdRemoveEntityA.Text = "Remove";
            cmdRemoveEntityA.Click += cmdRemoveEntityA_Click;
            // 
            // cmdInsertEntityA
            // 
            cmdInsertEntityA.BorderColour = Color.Empty;
            cmdInsertEntityA.CustomColour = false;
            cmdInsertEntityA.FlatBottom = false;
            cmdInsertEntityA.FlatTop = false;
            cmdInsertEntityA.Location = new Point(79, 35);
            cmdInsertEntityA.Margin = new Padding(4, 3, 4, 3);
            cmdInsertEntityA.Name = "cmdInsertEntityA";
            cmdInsertEntityA.Padding = new Padding(5);
            cmdInsertEntityA.Size = new Size(68, 27);
            cmdInsertEntityA.TabIndex = 13;
            cmdInsertEntityA.Text = "Insert";
            cmdInsertEntityA.Click += cmdInsertEntityA_Click;
            // 
            // cmdPrevRowDrawA
            // 
            cmdPrevRowDrawA.BorderColour = Color.Empty;
            cmdPrevRowDrawA.CustomColour = false;
            cmdPrevRowDrawA.FlatBottom = false;
            cmdPrevRowDrawA.FlatTop = false;
            cmdPrevRowDrawA.Location = new Point(7, 22);
            cmdPrevRowDrawA.Margin = new Padding(4, 3, 4, 3);
            cmdPrevRowDrawA.Name = "cmdPrevRowDrawA";
            cmdPrevRowDrawA.Padding = new Padding(5);
            cmdPrevRowDrawA.Size = new Size(75, 27);
            cmdPrevRowDrawA.TabIndex = 15;
            cmdPrevRowDrawA.Text = "Previous";
            cmdPrevRowDrawA.Click += cmdPrevRowDrawA_Click;
            // 
            // cmdNextRowDrawA
            // 
            cmdNextRowDrawA.BorderColour = Color.Empty;
            cmdNextRowDrawA.CustomColour = false;
            cmdNextRowDrawA.FlatBottom = false;
            cmdNextRowDrawA.FlatTop = false;
            cmdNextRowDrawA.Location = new Point(86, 22);
            cmdNextRowDrawA.Margin = new Padding(4, 3, 4, 3);
            cmdNextRowDrawA.Name = "cmdNextRowDrawA";
            cmdNextRowDrawA.Padding = new Padding(5);
            cmdNextRowDrawA.Size = new Size(74, 27);
            cmdNextRowDrawA.TabIndex = 16;
            cmdNextRowDrawA.Text = "Next";
            cmdNextRowDrawA.Click += cmdNextRowDrawA_Click;
            // 
            // EntityBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tbcTabs);
            Margin = new Padding(4, 3, 4, 3);
            Name = "EntityBox";
            Size = new Size(600, 600);
            VisibleChanged += EntityBox_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)numType).EndInit();
            fraType.ResumeLayout(false);
            fraType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSubtype).EndInit();
            fraPosition.ResumeLayout(false);
            fraPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            fraID.ResumeLayout(false);
            fraID.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numID2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numID).EndInit();
            fraSettings.ResumeLayout(false);
            fraSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSettingC).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSettingB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSettingA).EndInit();
            fraName.ResumeLayout(false);
            fraName.PerformLayout();
            tbcTabs.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            fraC2TTSet.ResumeLayout(false);
            fraC2TTGhostTarget.ResumeLayout(false);
            fraC2TTGhostTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTGhostTarget).EndInit();
            fraC2TTFlags.ResumeLayout(false);
            fraC2TTFlags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTFlags).EndInit();
            fraC2TTYRot.ResumeLayout(false);
            fraC2TTYRot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTYRot).EndInit();
            fraC2TTType.ResumeLayout(false);
            fraC2TTType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTType).EndInit();
            fraZMod.ResumeLayout(false);
            fraZMod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZMod).EndInit();
            tabSpecial.ResumeLayout(false);
            fraTTReward.ResumeLayout(false);
            fraTTReward.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTTReward).EndInit();
            fraOtherSettings.ResumeLayout(false);
            fraOtherSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numOtherSettings).EndInit();
            fraScaling.ResumeLayout(false);
            fraScaling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaling).EndInit();
            fraDDASection.ResumeLayout(false);
            fraDDASection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASection).EndInit();
            fraDDASettings.ResumeLayout(false);
            fraDDASettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASettings).EndInit();
            fraDrawOverrides.ResumeLayout(false);
            fraDrawOverrides.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideId).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideMult).EndInit();
            fraBoxCount.ResumeLayout(false);
            fraBoxCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBonusBoxCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBoxCount).EndInit();
            fraVictims.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numEditVictimID).EndInit();
            tabCamera.ResumeLayout(false);
            tabCamera.PerformLayout();
            fraFOV.ResumeLayout(false);
            fraFOV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFOVPosition).EndInit();
            fraFOVFrame.ResumeLayout(false);
            fraFOVFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFOV).EndInit();
            fraNeighbor.ResumeLayout(false);
            fraNeighbor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNeighborPosition).EndInit();
            fraNeighborSetting.ResumeLayout(false);
            fraNeighborSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNeighborFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborLink).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborCamera).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNeighborZone).EndInit();
            fraAvgDist.ResumeLayout(false);
            fraAvgDist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAvgDist).EndInit();
            fraMode.ResumeLayout(false);
            fraMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMode).EndInit();
            fraCameraSubIndex.ResumeLayout(false);
            fraCameraSubIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCameraSubIndex).EndInit();
            fraCameraIndex.ResumeLayout(false);
            fraCameraIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCameraIndex).EndInit();
            fraSLST.ResumeLayout(false);
            fraSLST.PerformLayout();
            tabLoadLists.ResumeLayout(false);
            tabLoadLists.PerformLayout();
            fraLoadListPayload.ResumeLayout(false);
            fraLoadListPayload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPayloadPosition).EndInit();
            fraLoadListB.ResumeLayout(false);
            fraLoadListB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueLoadB).EndInit();
            fraEIDB.ResumeLayout(false);
            fraEIDB.PerformLayout();
            fraLoadListA.ResumeLayout(false);
            fraLoadListA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueLoadA).EndInit();
            fraEIDA.ResumeLayout(false);
            fraEIDA.PerformLayout();
            tabDrawLists.ResumeLayout(false);
            fraVerifyDrawList.ResumeLayout(false);
            fraVerifyDrawList.PerformLayout();
            fraDrawListB.ResumeLayout(false);
            fraDrawListB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueDrawB).EndInit();
            fraEntityB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numEntityB).EndInit();
            fraDrawListA.ResumeLayout(false);
            fraDrawListA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMetavalueDrawA).EndInit();
            fraEntityA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numEntityA).EndInit();
            ResumeLayout(false);
        }

        private void DgvpropertyValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.CheckBox chkType;
        private DarkNumericUpDown numType;
        private DarkGroupBox fraType;
        private System.Windows.Forms.CheckBox chkSubtype;
        private DarkNumericUpDown numSubtype;
        private DarkGroupBox fraPosition;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private DarkNumericUpDown numZ;
        private DarkNumericUpDown numY;
        private DarkNumericUpDown numX;
        private DarkButton cmdInsertPosition;
        private DarkButton cmdInsertVictim;
        private DarkButton cmdRemovePosition;
        private DarkButton cmdAppendPosition;
        private DarkButton cmdNextPosition;
        private DarkButton cmdPreviousPosition;
        private System.Windows.Forms.Label lblPositionIndex;
        private System.Windows.Forms.Label lblVictimIndex;
        private DarkGroupBox fraID;
        private System.Windows.Forms.CheckBox chkID2;
        private DarkNumericUpDown numID2;
        private System.Windows.Forms.CheckBox chkID;
        private DarkNumericUpDown numID;
        private DarkGroupBox fraSettings;
        private System.Windows.Forms.Label lblSettingIndex;
        private DarkButton cmdNextSetting;
        private DarkButton cmdPreviousSetting;
        private DarkButton cmdAddSetting;
        private DarkButton cmdRemoveSetting;
        private DarkNumericUpDown numSettingB;
        private DarkNumericUpDown numSettingA;
        private DarkGroupBox fraName;
        private DarkTextBox txtName;
        private System.Windows.Forms.CheckBox chkName;
        private MetroSetTabControl tbcTabs;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabSpecial;
        private DarkGroupBox fraVictims;
        private DarkButton cmdRemoveVictim;
        private DarkGroupBox fraBoxCount;
        private System.Windows.Forms.CheckBox chkBoxCount;
        private DarkNumericUpDown numBoxCount;
        private DarkButton cmdClearAllVictims;
        private DarkGroupBox fraDDASettings;
        private System.Windows.Forms.CheckBox chkDDASettings;
        private DarkNumericUpDown numDDASettings;
        private DarkGroupBox fraDDASection;
        private System.Windows.Forms.CheckBox chkDDASection;
        private DarkNumericUpDown numDDASection;
        private DarkGroupBox fraDrawOverrides;
        private System.Windows.Forms.CheckBox chkDrawOverrideId;
        private DarkNumericUpDown numDrawOverrideId;
        private System.Windows.Forms.CheckBox chkDrawOverrideMult;
        private DarkNumericUpDown numDrawOverrideMult;
        private System.Windows.Forms.TabPage tabLoadLists;
        private DarkGroupBox fraLoadListA;
        private DarkButton cmdRemoveEIDA;
        private DarkButton cmdInsertEIDA;
        private System.Windows.Forms.Label lblEIDIndexA;
        private DarkButton cmdAppendEIDA;
        private System.Windows.Forms.Label lblLoadListRowIndexA;
        private DarkGroupBox fraEIDA;
        private DarkButton cmdPrevRowA;
        private DarkButton cmdNextRowA;
        private DarkTextBox txtEIDA;
        private DarkButton cmdRemoveRowA;
        private DarkButton cmdInsertRowA;
        private DarkNumericUpDown numMetavalueLoadA;
        private System.Windows.Forms.Label lblMetavalueLoadA;
        private DarkGroupBox fraScaling;
        private System.Windows.Forms.CheckBox chkScaling;
        private DarkNumericUpDown numScaling;
        private System.Windows.Forms.CheckBox chkBonusBoxCount;
        private DarkNumericUpDown numBonusBoxCount;
        private DarkGroupBox fraOtherSettings;
        private System.Windows.Forms.CheckBox chkOtherSettings;
        private DarkNumericUpDown numOtherSettings;
        private DarkGroupBox fraSLST;
        private DarkTextBox txtSLST;
        private System.Windows.Forms.CheckBox chkSLST;
        private System.Windows.Forms.TabPage tabCamera;
        private System.Windows.Forms.Label lblEIDErr1;
        private System.Windows.Forms.Label lblEIDErrA;
        private DarkGroupBox fraLoadListB;
        private System.Windows.Forms.Label lblMetavalueLoadB;
        private DarkButton cmdRemoveRowB;
        private DarkButton cmdInsertRowB;
        private DarkNumericUpDown numMetavalueLoadB;
        private System.Windows.Forms.Label lblLoadListRowIndexB;
        private DarkGroupBox fraEIDB;
        private DarkTextBox txtEIDB;
        private System.Windows.Forms.Label lblEIDIndexB;
        private DarkButton cmdAppendEIDB;
        private DarkButton cmdRemoveEIDB;
        private DarkButton cmdInsertEIDB;
        private DarkButton cmdPrevRowB;
        private DarkButton cmdNextRowB;
        private System.Windows.Forms.TabPage tabDrawLists;
        private DarkGroupBox fraDrawListA;
        private System.Windows.Forms.Label lblMetavalueDrawA;
        private DarkButton cmdRemoveRowDrawA;
        private DarkButton cmdInsertRowDrawA;
        private DarkNumericUpDown numMetavalueDrawA;
        private System.Windows.Forms.Label lblDrawListRowIndexA;
        private DarkGroupBox fraEntityA;
        private System.Windows.Forms.Label lblEntityIndexA;
        private DarkButton cmdAppendEntityA;
        private DarkButton cmdRemoveEntityA;
        private DarkButton cmdInsertEntityA;
        private DarkButton cmdPrevRowDrawA;
        private DarkButton cmdNextRowDrawA;
        private DarkNumericUpDown numEntityA;
        private DarkGroupBox fraDrawListB;
        private System.Windows.Forms.Label lblMetavalueDrawB;
        private DarkButton cmdRemoveRowDrawB;
        private DarkButton cmdInsertRowDrawB;
        private DarkNumericUpDown numMetavalueDrawB;
        private System.Windows.Forms.Label lblDrawListRowIndexB;
        private DarkGroupBox fraEntityB;
        private DarkNumericUpDown numEntityB;
        private System.Windows.Forms.Label lblEntityIndexB;
        private DarkButton cmdAppendEntityB;
        private DarkButton cmdRemoveEntityB;
        private DarkButton cmdInsertEntityB;
        private DarkButton cmdPrevRowDrawB;
        private DarkButton cmdNextRowDrawB;
        private DarkButton cmdLoadListVerify;
        private DarkGroupBox fraLoadListPayload;
        private DarkButton cmdPayload;
        private System.Windows.Forms.Label lblPayloadPosition;
        private DarkNumericUpDown numPayloadPosition;
        private System.Windows.Forms.Label lblPayload;
        private DarkNumericUpDown numSettingC;
        private System.Windows.Forms.CheckBox chkSettingHex;
        private System.Windows.Forms.Label lblEIDErrB;
        private DarkButton cmdEditPath;
        private DarkGroupBox fraTTReward;
        private System.Windows.Forms.CheckBox chkTTReward;
        private DarkNumericUpDown numTTReward;
        private DarkGroupBox fraCameraSubIndex;
        private System.Windows.Forms.CheckBox chkCameraSubIndex;
        private DarkNumericUpDown numCameraSubIndex;
        private DarkGroupBox fraCameraIndex;
        private System.Windows.Forms.CheckBox chkCameraIndex;
        private DarkNumericUpDown numCameraIndex;
        private DarkGroupBox fraMode;
        private System.Windows.Forms.CheckBox chkMode;
        private DarkNumericUpDown numMode;
        private DarkGroupBox fraAvgDist;
        private System.Windows.Forms.CheckBox chkAvgDist;
        private DarkNumericUpDown numAvgDist;
        private DarkGroupBox fraNeighbor;
        private System.Windows.Forms.Label lblNeighborPosition;
        private DarkButton cmdRemoveNeighbor;
        private DarkButton cmdInsertNeighbor;
        private DarkNumericUpDown numNeighborPosition;
        private System.Windows.Forms.Label lblNeighbor;
        private DarkGroupBox fraNeighborSetting;
        private DarkButton cmdPrevNeighbor;
        private DarkButton cmdNextNeighbor;
        private System.Windows.Forms.Label lblNeighborLink;
        private System.Windows.Forms.Label lblNeighborFlag;
        private DarkNumericUpDown numNeighborFlag;
        private System.Windows.Forms.Label lblNeighborCamera;
        private System.Windows.Forms.Label lblNeighborZone;
        private DarkNumericUpDown numNeighborLink;
        private DarkNumericUpDown numNeighborCamera;
        private DarkNumericUpDown numNeighborZone;
        private System.Windows.Forms.Label lblNeighborSetting;
        private DarkButton cmdRemoveNeighborSetting;
        private DarkButton cmdInsertNeighborSetting;
        private DarkButton cmdPrevNeighborSetting;
        private DarkButton cmdNextNeighborSetting;
        private DarkGroupBox fraZMod;
        private System.Windows.Forms.CheckBox chkZMod;
        private DarkNumericUpDown numZMod;
        private DarkGroupBox fraFOV;
        private System.Windows.Forms.Label lblFOVPosition;
        private DarkButton cmdRemoveFOVFrame;
        private DarkButton cmdInsertFOVFrame;
        private DarkNumericUpDown numFOVPosition;
        private System.Windows.Forms.Label lblFOVFrame;
        private DarkGroupBox fraFOVFrame;
        private System.Windows.Forms.Label lblFOVIndex;
        private DarkButton cmdRemoveFOV;
        private DarkButton cmdInsertFOV;
        private DarkButton cmdPrevFOV;
        private DarkButton cmdNextFOV;
        private System.Windows.Forms.Label lblFOV;
        private DarkNumericUpDown numFOV;
        private DarkButton cmdPrevFOVFrame;
        private DarkButton cmdNextFOVFrame;
        private System.Windows.Forms.Label lblArgAs;
        private Label lblVerifyLoadLists;
        private Label lblPayloadSound;
        private Label lblPayloadTexture;
        private DarkGroupBox fraC2TTSet;
        private CheckBox chkC2TTType;
        private DarkNumericUpDown numC2TTYrot;
        private DarkNumericUpDown numC2TTType;
        private DarkGroupBox fraC2TTType;
        private DarkGroupBox fraC2TTGhostTarget;
        private DarkNumericUpDown numC2TTGhostTarget;
        private CheckBox chkC2TTGhostTarget;
        private DarkGroupBox fraC2TTFlags;
        private DarkGroupBox fraC2TTYRot;
        private DarkNumericUpDown numC2TTYRot;
        private CheckBox chkC2TTYRot;
        private Label lblSettingB;
        private Label lblSettingA;
        private DarkNumericUpDown numC2TTFlags;
        private CheckBox chkC2TTFlags;
        private DarkListBox lbVictimID;
        private DarkNumericUpDown numEditVictimID;
        private DarkListBox lbEIDA;
        private DarkListBox lbEIDB;
        private DarkGroupBox fraVerifyDrawList;
        private Label lblVerifyDrawLists;
        private DarkButton cmdVerifyDrawList;
        private DarkListBox lbEntityA;
        private DarkListBox lbEntityB;
        private DarkButton cmdCopySetting;
        private DarkButton cmdPasteSetting;
        private CheckBox chkSyncPositions;
        private DarkButton cmdSyncEntities;
        private CheckBox chkSyncEntities;
    }
}
