using System.Windows.Forms;

namespace CrashEdit.CE.Forms
{
    partial class EntityEditor
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
            CloseEntityEditor();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityEditor));
            dgvEntities = new DataGridView();
            fraName = new AltUI.Controls.DarkGroupBox();
            txtName = new AltUI.Controls.DarkTextBox();
            chkName = new CheckBox();
            fraPosition = new AltUI.Controls.DarkGroupBox();
            chkSyncPositions = new CheckBox();
            cmdEditPath = new AltUI.Controls.DarkButton();
            lblPositionIndex = new Label();
            cmdNextPosition = new AltUI.Controls.DarkButton();
            cmdPreviousPosition = new AltUI.Controls.DarkButton();
            cmdInsertPosition = new AltUI.Controls.DarkButton();
            lblZ = new Label();
            cmdRemovePosition = new AltUI.Controls.DarkButton();
            lblY = new Label();
            cmdAppendPosition = new AltUI.Controls.DarkButton();
            lblX = new Label();
            numZ = new AltUI.Controls.DarkNumericUpDown();
            numY = new AltUI.Controls.DarkNumericUpDown();
            numX = new AltUI.Controls.DarkNumericUpDown();
            cmdSyncEntities = new AltUI.Controls.DarkButton();
            fraSettings = new AltUI.Controls.DarkGroupBox();
            cmdPasteSetting = new AltUI.Controls.DarkButton();
            cmdCopySetting = new AltUI.Controls.DarkButton();
            lblSettingB = new Label();
            lblSettingA = new Label();
            lblArgAs = new Label();
            chkSettingHex = new CheckBox();
            numSettingC = new AltUI.Controls.DarkNumericUpDown();
            lblSettingIndex = new Label();
            cmdNextSetting = new AltUI.Controls.DarkButton();
            cmdPreviousSetting = new AltUI.Controls.DarkButton();
            cmdAddSetting = new AltUI.Controls.DarkButton();
            cmdRemoveSetting = new AltUI.Controls.DarkButton();
            numSettingB = new AltUI.Controls.DarkNumericUpDown();
            numSettingA = new AltUI.Controls.DarkNumericUpDown();
            lbZones = new DoubleBufferedListBox();
            pnProperties = new Panel();
            tabEntity = new MetroSet_UI.Controls.MetroSetTabControl();
            tbpGeneral = new TabPage();
            fraType = new AltUI.Controls.DarkGroupBox();
            lblGOOL = new Label();
            chkSubtype = new CheckBox();
            numSubtype = new AltUI.Controls.DarkNumericUpDown();
            chkType = new CheckBox();
            numType = new AltUI.Controls.DarkNumericUpDown();
            fraID = new AltUI.Controls.DarkGroupBox();
            chkID = new CheckBox();
            numID = new AltUI.Controls.DarkNumericUpDown();
            fraC2TTSet = new AltUI.Controls.DarkGroupBox();
            fraC2TTGhostTarget = new AltUI.Controls.DarkGroupBox();
            numC2TTGhostTarget = new AltUI.Controls.DarkNumericUpDown();
            chkC2TTGhostTarget = new CheckBox();
            fraC2TTFlags = new AltUI.Controls.DarkGroupBox();
            numC2TTFlags = new AltUI.Controls.DarkNumericUpDown();
            chkC2TTFlags = new CheckBox();
            fraC2TTYRot = new AltUI.Controls.DarkGroupBox();
            lblC2TTYRot = new Label();
            numC2TTYRot = new AltUI.Controls.DarkNumericUpDown();
            chkC2TTYRot = new CheckBox();
            fraC2TTType = new AltUI.Controls.DarkGroupBox();
            cmbC2TTType = new AltUI.Controls.DarkComboBox();
            chkC2TTType = new CheckBox();
            fraZMod = new AltUI.Controls.DarkGroupBox();
            chkZMod = new CheckBox();
            numZMod = new AltUI.Controls.DarkNumericUpDown();
            tbpSpecial = new TabPage();
            fraDDASection = new AltUI.Controls.DarkGroupBox();
            chkDDASection = new CheckBox();
            numDDASection = new AltUI.Controls.DarkNumericUpDown();
            fraDDASettings = new AltUI.Controls.DarkGroupBox();
            chkDDASettings = new CheckBox();
            numDDASettings = new AltUI.Controls.DarkNumericUpDown();
            fraDrawOverrides = new AltUI.Controls.DarkGroupBox();
            picHelpOverrideMult = new PictureBox();
            picHelpOverrideId = new PictureBox();
            chkDrawOverrideId = new CheckBox();
            numDrawOverrideId = new AltUI.Controls.DarkNumericUpDown();
            chkDrawOverrideMult = new CheckBox();
            numDrawOverrideMult = new AltUI.Controls.DarkNumericUpDown();
            fraBoxCount = new AltUI.Controls.DarkGroupBox();
            chkBonusBoxCount = new CheckBox();
            numBonusBoxCount = new AltUI.Controls.DarkNumericUpDown();
            chkBoxCount = new CheckBox();
            numBoxCount = new AltUI.Controls.DarkNumericUpDown();
            fraVictims = new AltUI.Controls.DarkGroupBox();
            picHelpVictimDistance = new PictureBox();
            lblVictimDistance = new Label();
            numVictimDistance = new AltUI.Controls.DarkNumericUpDown();
            cmdCalculateVictims = new AltUI.Controls.DarkButton();
            numEditVictimID = new AltUI.Controls.DarkNumericUpDown();
            lbVictimID = new AltUI.Controls.DarkListBox();
            cmdClearAllVictims = new AltUI.Controls.DarkButton();
            cmdRemoveVictim = new AltUI.Controls.DarkButton();
            cmdInsertVictim = new AltUI.Controls.DarkButton();
            lblVictimIndex = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            pnLists = new Panel();
            chkHideNoEntityZone = new CheckBox();
            toolStrip1 = new ToolStrip();
            tstSearch = new ToolStripTextBox();
            tslSearch = new ToolStripLabel();
            tsbEditDDA = new ToolStripButton();
            pnSyncEdit = new Panel();
            tglSyncEdit = new MetroSet_UI.Controls.MetroSetSwitch();
            lblSyncEdit = new Label();
            chkShowCameras = new CheckBox();
            txtFilter = new AltUI.Controls.DarkTextBox();
            chkShowZone = new CheckBox();
            splitContainer1 = new SplitContainer();
            tsbObjects = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)dgvEntities).BeginInit();
            fraName.SuspendLayout();
            fraPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            fraSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSettingC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSettingB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSettingA).BeginInit();
            pnProperties.SuspendLayout();
            tabEntity.SuspendLayout();
            tbpGeneral.SuspendLayout();
            fraType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSubtype).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numType).BeginInit();
            fraID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numID).BeginInit();
            fraC2TTSet.SuspendLayout();
            fraC2TTGhostTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTGhostTarget).BeginInit();
            fraC2TTFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTFlags).BeginInit();
            fraC2TTYRot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numC2TTYRot).BeginInit();
            fraC2TTType.SuspendLayout();
            fraZMod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numZMod).BeginInit();
            tbpSpecial.SuspendLayout();
            fraDDASection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASection).BeginInit();
            fraDDASettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASettings).BeginInit();
            fraDrawOverrides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHelpOverrideMult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picHelpOverrideId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideMult).BeginInit();
            fraBoxCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numBonusBoxCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBoxCount).BeginInit();
            fraVictims.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHelpVictimDistance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVictimDistance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEditVictimID).BeginInit();
            panel2.SuspendLayout();
            pnLists.SuspendLayout();
            toolStrip1.SuspendLayout();
            pnSyncEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvEntities
            // 
            dgvEntities.AllowUserToAddRows = false;
            dgvEntities.AllowUserToResizeColumns = false;
            dgvEntities.AllowUserToResizeRows = false;
            dgvEntities.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEntities.ColumnHeadersHeight = 24;
            dgvEntities.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEntities.Location = new Point(79, 34);
            dgvEntities.Name = "dgvEntities";
            dgvEntities.RowHeadersWidth = 24;
            dgvEntities.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvEntities.ShowCellToolTips = false;
            dgvEntities.Size = new Size(350, 505);
            dgvEntities.TabIndex = 0;
            dgvEntities.CellBeginEdit += dgvEntities_CellBeginEdit;
            dgvEntities.CellFormatting += dgvEntities_CellFormatting;
            dgvEntities.CellMouseDown += dgvEntities_CellMouseDown;
            dgvEntities.CellValidating += dgvEntities_CellValidating;
            dgvEntities.CellValueChanged += dgvEntities_CellValueChanged;
            dgvEntities.EditingControlShowing += dgvEntities_EditingControlShowing;
            dgvEntities.SelectionChanged += dgvEntities_SelectionChanged;
            // 
            // fraName
            // 
            fraName.BackColor = Color.Transparent;
            fraName.Controls.Add(txtName);
            fraName.Controls.Add(chkName);
            fraName.Location = new Point(4, 3);
            fraName.Margin = new Padding(4, 3, 4, 3);
            fraName.Name = "fraName";
            fraName.Padding = new Padding(4, 3, 4, 3);
            fraName.Size = new Size(233, 83);
            fraName.TabIndex = 2;
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
            chkName.Location = new Point(8, 22);
            chkName.Margin = new Padding(4, 3, 4, 3);
            chkName.Name = "chkName";
            chkName.Size = new Size(68, 19);
            chkName.TabIndex = 0;
            chkName.Text = "Enabled";
            chkName.UseVisualStyleBackColor = false;
            chkName.CheckedChanged += chkName_CheckedChanged;
            // 
            // fraPosition
            // 
            fraPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraPosition.BackColor = Color.Transparent;
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
            fraPosition.Location = new Point(4, 92);
            fraPosition.Margin = new Padding(4, 3, 4, 3);
            fraPosition.Name = "fraPosition";
            fraPosition.Padding = new Padding(4, 3, 4, 3);
            fraPosition.Size = new Size(233, 190);
            fraPosition.TabIndex = 3;
            fraPosition.TabStop = false;
            fraPosition.Text = "Position(s)";
            // 
            // chkSyncPositions
            // 
            chkSyncPositions.AutoSize = true;
            chkSyncPositions.BackColor = Color.Transparent;
            chkSyncPositions.Location = new Point(89, 156);
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
            lblZ.Location = new Point(8, 122);
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
            lblY.Location = new Point(8, 92);
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
            lblX.Location = new Point(8, 62);
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
            // cmdSyncEntities
            // 
            cmdSyncEntities.BorderColour = Color.Empty;
            cmdSyncEntities.CustomColour = false;
            cmdSyncEntities.FlatBottom = false;
            cmdSyncEntities.FlatTop = false;
            cmdSyncEntities.Location = new Point(351, 595);
            cmdSyncEntities.Name = "cmdSyncEntities";
            cmdSyncEntities.Padding = new Padding(5);
            cmdSyncEntities.Size = new Size(74, 29);
            cmdSyncEntities.TabIndex = 10;
            cmdSyncEntities.Text = "View List";
            cmdSyncEntities.Visible = false;
            cmdSyncEntities.Click += cmdSyncList_Click;
            // 
            // fraSettings
            // 
            fraSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSettings.BackColor = Color.Transparent;
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
            fraSettings.Location = new Point(4, 288);
            fraSettings.Margin = new Padding(4, 3, 4, 3);
            fraSettings.Name = "fraSettings";
            fraSettings.Padding = new Padding(4, 3, 4, 3);
            fraSettings.Size = new Size(233, 210);
            fraSettings.TabIndex = 4;
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
            lblSettingB.Location = new Point(9, 82);
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
            lblSettingA.Location = new Point(9, 58);
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
            lblArgAs.Location = new Point(9, 138);
            lblArgAs.Margin = new Padding(4, 0, 4, 0);
            lblArgAs.Name = "lblArgAs";
            lblArgAs.Size = new Size(68, 60);
            lblArgAs.TabIndex = 9;
            lblArgAs.Text = "as Number:\r\nas Angle:\r\nas Time:\r\nAs Meters:";
            // 
            // chkSettingHex
            // 
            chkSettingHex.AutoSize = true;
            chkSettingHex.BackColor = Color.Transparent;
            chkSettingHex.Checked = true;
            chkSettingHex.CheckState = CheckState.Checked;
            chkSettingHex.Location = new Point(140, 110);
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
            // lbZones
            // 
            lbZones.BackColor = Color.FromArgb(26, 26, 28);
            lbZones.BorderStyle = BorderStyle.FixedSingle;
            lbZones.ForeColor = Color.FromArgb(213, 213, 213);
            lbZones.FormattingEnabled = true;
            lbZones.IntegralHeight = false;
            lbZones.Location = new Point(3, 57);
            lbZones.Name = "lbZones";
            lbZones.Size = new Size(70, 482);
            lbZones.TabIndex = 5;
            lbZones.SelectedIndexChanged += lbZones_SelectedIndexChanged;
            // 
            // pnProperties
            // 
            pnProperties.Controls.Add(tabEntity);
            pnProperties.Location = new Point(437, 3);
            pnProperties.Name = "pnProperties";
            pnProperties.Size = new Size(447, 761);
            pnProperties.TabIndex = 6;
            // 
            // tabEntity
            // 
            tabEntity.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            tabEntity.AnimateTime = 200;
            tabEntity.BackgroundColor = Color.FromArgb(31, 31, 32);
            tabEntity.Controls.Add(tbpGeneral);
            tabEntity.Controls.Add(tbpSpecial);
            tabEntity.Dock = DockStyle.Fill;
            tabEntity.IsDerivedStyle = false;
            tabEntity.ItemSize = new Size(100, 28);
            tabEntity.Location = new Point(0, 0);
            tabEntity.Multiline = true;
            tabEntity.Name = "tabEntity";
            tabEntity.SelectedIndex = 0;
            tabEntity.SelectedTextColor = Color.White;
            tabEntity.Size = new Size(447, 761);
            tabEntity.SizeMode = TabSizeMode.Fixed;
            tabEntity.Speed = 100;
            tabEntity.Style = MetroSet_UI.Enums.Style.Dark;
            tabEntity.StyleManager = null;
            tabEntity.TabIndex = 8;
            tabEntity.ThemeAuthor = "Narwin";
            tabEntity.ThemeName = "MetroDark";
            tabEntity.UnselectedTextColor = Color.Gray;
            tabEntity.UseAnimation = false;
            // 
            // tbpGeneral
            // 
            tbpGeneral.AutoScroll = true;
            tbpGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tbpGeneral.Controls.Add(fraType);
            tbpGeneral.Controls.Add(fraID);
            tbpGeneral.Controls.Add(fraC2TTSet);
            tbpGeneral.Controls.Add(fraName);
            tbpGeneral.Controls.Add(fraZMod);
            tbpGeneral.Controls.Add(fraSettings);
            tbpGeneral.Controls.Add(fraPosition);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Size = new Size(439, 725);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            // 
            // fraType
            // 
            fraType.BackColor = Color.Transparent;
            fraType.Controls.Add(lblGOOL);
            fraType.Controls.Add(chkSubtype);
            fraType.Controls.Add(numSubtype);
            fraType.Controls.Add(chkType);
            fraType.Controls.Add(numType);
            fraType.Location = new Point(245, 92);
            fraType.Margin = new Padding(4, 3, 4, 3);
            fraType.Name = "fraType";
            fraType.Padding = new Padding(4, 3, 4, 3);
            fraType.Size = new Size(132, 137);
            fraType.TabIndex = 12;
            fraType.TabStop = false;
            fraType.Text = "Type & Subtype";
            // 
            // lblGOOL
            // 
            lblGOOL.AutoSize = true;
            lblGOOL.ForeColor = Color.DarkTurquoise;
            lblGOOL.Location = new Point(80, 22);
            lblGOOL.Name = "lblGOOL";
            lblGOOL.Size = new Size(47, 15);
            lblGOOL.TabIndex = 2;
            lblGOOL.Text = "(GOOL)";
            // 
            // chkSubtype
            // 
            chkSubtype.AutoSize = true;
            chkSubtype.BackColor = Color.Transparent;
            chkSubtype.Location = new Point(8, 77);
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
            // chkType
            // 
            chkType.AutoSize = true;
            chkType.BackColor = Color.Transparent;
            chkType.Location = new Point(8, 22);
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
            // fraID
            // 
            fraID.BackColor = Color.Transparent;
            fraID.Controls.Add(chkID);
            fraID.Controls.Add(numID);
            fraID.Location = new Point(245, 3);
            fraID.Margin = new Padding(4, 3, 4, 3);
            fraID.Name = "fraID";
            fraID.Padding = new Padding(4, 3, 4, 3);
            fraID.Size = new Size(132, 83);
            fraID.TabIndex = 11;
            fraID.TabStop = false;
            fraID.Text = "ID";
            // 
            // chkID
            // 
            chkID.AutoSize = true;
            chkID.BackColor = Color.Transparent;
            chkID.Location = new Point(8, 22);
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
            // fraC2TTSet
            // 
            fraC2TTSet.BackColor = Color.Transparent;
            fraC2TTSet.Controls.Add(fraC2TTGhostTarget);
            fraC2TTSet.Controls.Add(fraC2TTFlags);
            fraC2TTSet.Controls.Add(fraC2TTYRot);
            fraC2TTSet.Controls.Add(fraC2TTType);
            fraC2TTSet.Location = new Point(245, 322);
            fraC2TTSet.Margin = new Padding(4, 3, 4, 3);
            fraC2TTSet.Name = "fraC2TTSet";
            fraC2TTSet.Padding = new Padding(4, 3, 4, 3);
            fraC2TTSet.Size = new Size(132, 368);
            fraC2TTSet.TabIndex = 8;
            fraC2TTSet.TabStop = false;
            fraC2TTSet.Text = "C2 Time Trials";
            fraC2TTSet.Visible = false;
            // 
            // fraC2TTGhostTarget
            // 
            fraC2TTGhostTarget.BackColor = Color.Transparent;
            fraC2TTGhostTarget.Controls.Add(numC2TTGhostTarget);
            fraC2TTGhostTarget.Controls.Add(chkC2TTGhostTarget);
            fraC2TTGhostTarget.Location = new Point(7, 282);
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
            fraC2TTFlags.Location = new Point(7, 206);
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
            fraC2TTYRot.Controls.Add(lblC2TTYRot);
            fraC2TTYRot.Controls.Add(numC2TTYRot);
            fraC2TTYRot.Controls.Add(chkC2TTYRot);
            fraC2TTYRot.Location = new Point(7, 104);
            fraC2TTYRot.Name = "fraC2TTYRot";
            fraC2TTYRot.Size = new Size(116, 99);
            fraC2TTYRot.TabIndex = 5;
            fraC2TTYRot.TabStop = false;
            fraC2TTYRot.Text = "RotY";
            // 
            // lblC2TTYRot
            // 
            lblC2TTYRot.AutoSize = true;
            lblC2TTYRot.Location = new Point(7, 73);
            lblC2TTYRot.Name = "lblC2TTYRot";
            lblC2TTYRot.Size = new Size(27, 15);
            lblC2TTYRot.TabIndex = 2;
            lblC2TTYRot.Text = "deg";
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
            fraC2TTType.Controls.Add(cmbC2TTType);
            fraC2TTType.Controls.Add(chkC2TTType);
            fraC2TTType.Location = new Point(7, 22);
            fraC2TTType.Name = "fraC2TTType";
            fraC2TTType.Size = new Size(116, 76);
            fraC2TTType.TabIndex = 4;
            fraC2TTType.TabStop = false;
            fraC2TTType.Text = "Type";
            // 
            // cmbC2TTType
            // 
            cmbC2TTType.DrawMode = DrawMode.OwnerDrawVariable;
            cmbC2TTType.FormattingEnabled = true;
            cmbC2TTType.Items.AddRange(new object[] { "-", "Timed-1", "Timed-2", "Timed-3", "Mask", "TNT", "Nitro", "Pow", "Empty", "Action", "Iron", "Iron Arrow" });
            cmbC2TTType.Location = new Point(7, 47);
            cmbC2TTType.Name = "cmbC2TTType";
            cmbC2TTType.Size = new Size(102, 24);
            cmbC2TTType.TabIndex = 7;
            cmbC2TTType.SelectedIndexChanged += cmbC2TTType_SelectedIndexChanged;
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
            fraZMod.BackColor = Color.Transparent;
            fraZMod.Controls.Add(chkZMod);
            fraZMod.Controls.Add(numZMod);
            fraZMod.Location = new Point(245, 235);
            fraZMod.Margin = new Padding(4, 3, 4, 3);
            fraZMod.Name = "fraZMod";
            fraZMod.Padding = new Padding(4, 3, 4, 3);
            fraZMod.Size = new Size(132, 81);
            fraZMod.TabIndex = 7;
            fraZMod.TabStop = false;
            fraZMod.Text = "Depth Modifier";
            // 
            // chkZMod
            // 
            chkZMod.AutoSize = true;
            chkZMod.BackColor = Color.Transparent;
            chkZMod.Location = new Point(8, 22);
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
            // tbpSpecial
            // 
            tbpSpecial.AutoScroll = true;
            tbpSpecial.BackColor = Color.FromArgb(31, 31, 32);
            tbpSpecial.Controls.Add(fraDDASection);
            tbpSpecial.Controls.Add(fraDDASettings);
            tbpSpecial.Controls.Add(fraDrawOverrides);
            tbpSpecial.Controls.Add(fraBoxCount);
            tbpSpecial.Controls.Add(fraVictims);
            tbpSpecial.Location = new Point(4, 32);
            tbpSpecial.Name = "tbpSpecial";
            tbpSpecial.Size = new Size(439, 725);
            tbpSpecial.TabIndex = 0;
            tbpSpecial.Text = "Special";
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
            fraDDASection.TabIndex = 15;
            fraDDASection.TabStop = false;
            fraDDASection.Text = "DDA Section";
            // 
            // chkDDASection
            // 
            chkDDASection.AutoSize = true;
            chkDDASection.BackColor = Color.Transparent;
            chkDDASection.Location = new Point(8, 22);
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
            fraDDASettings.TabIndex = 13;
            fraDDASettings.TabStop = false;
            fraDDASettings.Text = "DDA Death Count";
            // 
            // chkDDASettings
            // 
            chkDDASettings.AutoSize = true;
            chkDDASettings.BackColor = Color.Transparent;
            chkDDASettings.Location = new Point(8, 22);
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
            fraDrawOverrides.Controls.Add(picHelpOverrideMult);
            fraDrawOverrides.Controls.Add(picHelpOverrideId);
            fraDrawOverrides.Controls.Add(chkDrawOverrideId);
            fraDrawOverrides.Controls.Add(numDrawOverrideId);
            fraDrawOverrides.Controls.Add(chkDrawOverrideMult);
            fraDrawOverrides.Controls.Add(numDrawOverrideMult);
            fraDrawOverrides.Location = new Point(166, 322);
            fraDrawOverrides.Margin = new Padding(4, 3, 4, 3);
            fraDrawOverrides.Name = "fraDrawOverrides";
            fraDrawOverrides.Padding = new Padding(4, 3, 4, 3);
            fraDrawOverrides.Size = new Size(140, 140);
            fraDrawOverrides.TabIndex = 14;
            fraDrawOverrides.TabStop = false;
            fraDrawOverrides.Text = "Draw List Overrides";
            // 
            // picHelpOverrideMult
            // 
            picHelpOverrideMult.BackColor = Color.Transparent;
            picHelpOverrideMult.Cursor = Cursors.Help;
            picHelpOverrideMult.Location = new Point(114, 80);
            picHelpOverrideMult.Name = "picHelpOverrideMult";
            picHelpOverrideMult.Size = new Size(16, 16);
            picHelpOverrideMult.TabIndex = 19;
            picHelpOverrideMult.TabStop = false;
            // 
            // picHelpOverrideId
            // 
            picHelpOverrideId.BackColor = Color.Transparent;
            picHelpOverrideId.Cursor = Cursors.Help;
            picHelpOverrideId.Location = new Point(100, 22);
            picHelpOverrideId.Name = "picHelpOverrideId";
            picHelpOverrideId.Size = new Size(16, 16);
            picHelpOverrideId.TabIndex = 19;
            picHelpOverrideId.TabStop = false;
            // 
            // chkDrawOverrideId
            // 
            chkDrawOverrideId.AutoSize = true;
            chkDrawOverrideId.BackColor = Color.Transparent;
            chkDrawOverrideId.Location = new Point(8, 22);
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
            chkDrawOverrideMult.Location = new Point(8, 80);
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
            fraBoxCount.TabIndex = 12;
            fraBoxCount.TabStop = false;
            fraBoxCount.Text = "Box Count";
            // 
            // chkBonusBoxCount
            // 
            chkBonusBoxCount.AutoSize = true;
            chkBonusBoxCount.BackColor = Color.Transparent;
            chkBonusBoxCount.Location = new Point(8, 78);
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
            chkBoxCount.Location = new Point(8, 22);
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
            fraVictims.Controls.Add(picHelpVictimDistance);
            fraVictims.Controls.Add(lblVictimDistance);
            fraVictims.Controls.Add(numVictimDistance);
            fraVictims.Controls.Add(cmdCalculateVictims);
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
            fraVictims.Size = new Size(154, 525);
            fraVictims.TabIndex = 11;
            fraVictims.TabStop = false;
            fraVictims.Text = "Victims";
            // 
            // picHelpVictimDistance
            // 
            picHelpVictimDistance.BackColor = Color.Transparent;
            picHelpVictimDistance.Cursor = Cursors.Help;
            picHelpVictimDistance.Location = new Point(43, 476);
            picHelpVictimDistance.Name = "picHelpVictimDistance";
            picHelpVictimDistance.Size = new Size(16, 16);
            picHelpVictimDistance.TabIndex = 19;
            picHelpVictimDistance.TabStop = false;
            // 
            // lblVictimDistance
            // 
            lblVictimDistance.AutoSize = true;
            lblVictimDistance.Location = new Point(7, 444);
            lblVictimDistance.Name = "lblVictimDistance";
            lblVictimDistance.Size = new Size(52, 15);
            lblVictimDistance.TabIndex = 18;
            lblVictimDistance.Text = "Distance";
            // 
            // numVictimDistance
            // 
            numVictimDistance.Location = new Point(65, 442);
            numVictimDistance.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numVictimDistance.Name = "numVictimDistance";
            numVictimDistance.Size = new Size(81, 23);
            numVictimDistance.TabIndex = 17;
            numVictimDistance.Value = new decimal(new int[] { 300, 0, 0, 0 });
            // 
            // cmdCalculateVictims
            // 
            cmdCalculateVictims.BorderColour = Color.Empty;
            cmdCalculateVictims.CustomColour = false;
            cmdCalculateVictims.FlatBottom = false;
            cmdCalculateVictims.FlatTop = false;
            cmdCalculateVictims.Location = new Point(65, 471);
            cmdCalculateVictims.Name = "cmdCalculateVictims";
            cmdCalculateVictims.Padding = new Padding(5);
            cmdCalculateVictims.Size = new Size(81, 27);
            cmdCalculateVictims.TabIndex = 16;
            cmdCalculateVictims.Text = "Calculate!";
            cmdCalculateVictims.Click += cmdCalculateVictims_Click;
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
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(641, 776);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(pnLists);
            panel2.Controls.Add(pnProperties);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(860, 776);
            panel2.TabIndex = 8;
            // 
            // pnLists
            // 
            pnLists.Controls.Add(chkHideNoEntityZone);
            pnLists.Controls.Add(toolStrip1);
            pnLists.Controls.Add(pnSyncEdit);
            pnLists.Controls.Add(lbZones);
            pnLists.Controls.Add(chkShowCameras);
            pnLists.Controls.Add(dgvEntities);
            pnLists.Controls.Add(txtFilter);
            pnLists.Controls.Add(cmdSyncEntities);
            pnLists.Controls.Add(chkShowZone);
            pnLists.Location = new Point(3, 3);
            pnLists.Name = "pnLists";
            pnLists.Size = new Size(435, 757);
            pnLists.TabIndex = 0;
            // 
            // chkHideNoEntityZone
            // 
            chkHideNoEntityZone.AutoSize = true;
            chkHideNoEntityZone.Location = new Point(3, 570);
            chkHideNoEntityZone.Name = "chkHideNoEntityZone";
            chkHideNoEntityZone.Size = new Size(140, 19);
            chkHideNoEntityZone.TabIndex = 16;
            chkHideNoEntityZone.Text = "Hide No-Entity Zones";
            chkHideNoEntityZone.UseVisualStyleBackColor = true;
            chkHideNoEntityZone.CheckedChanged += chkHideNoEntityZones_CheckedChanged;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tstSearch, tslSearch, tsbEditDDA, tsbObjects });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(435, 25);
            toolStrip1.TabIndex = 15;
            toolStrip1.Text = "toolStrip1";
            // 
            // tstSearch
            // 
            tstSearch.Alignment = ToolStripItemAlignment.Right;
            tstSearch.Margin = new Padding(1, 0, 13, 0);
            tstSearch.MergeIndex = 1;
            tstSearch.Name = "tstSearch";
            tstSearch.Size = new Size(100, 25);
            tstSearch.KeyDown += tstSearch_KeyDown;
            // 
            // tslSearch
            // 
            tslSearch.Alignment = ToolStripItemAlignment.Right;
            tslSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tslSearch.Name = "tslSearch";
            tslSearch.Size = new Size(0, 22);
            // 
            // tsbEditDDA
            // 
            tsbEditDDA.Image = (Image)resources.GetObject("tsbEditDDA.Image");
            tsbEditDDA.ImageTransparentColor = Color.Magenta;
            tsbEditDDA.Name = "tsbEditDDA";
            tsbEditDDA.Size = new Size(72, 22);
            tsbEditDDA.Text = "DDA List";
            tsbEditDDA.Click += tsbEditDDA_Click;
            // 
            // pnSyncEdit
            // 
            pnSyncEdit.Controls.Add(tglSyncEdit);
            pnSyncEdit.Controls.Add(lblSyncEdit);
            pnSyncEdit.Location = new Point(302, 545);
            pnSyncEdit.Name = "pnSyncEdit";
            pnSyncEdit.Size = new Size(129, 44);
            pnSyncEdit.TabIndex = 14;
            // 
            // tglSyncEdit
            // 
            tglSyncEdit.BackColor = Color.Transparent;
            tglSyncEdit.BackgroundColor = Color.Empty;
            tglSyncEdit.BorderColor = Color.FromArgb(155, 155, 155);
            tglSyncEdit.CheckColor = Color.FromArgb(65, 177, 225);
            tglSyncEdit.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            tglSyncEdit.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            tglSyncEdit.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            tglSyncEdit.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            tglSyncEdit.IsDerivedStyle = true;
            tglSyncEdit.Location = new Point(65, 3);
            tglSyncEdit.Name = "tglSyncEdit";
            tglSyncEdit.Size = new Size(58, 22);
            tglSyncEdit.Style = MetroSet_UI.Enums.Style.Dark;
            tglSyncEdit.StyleManager = null;
            tglSyncEdit.Switched = false;
            tglSyncEdit.SymbolColor = Color.FromArgb(92, 92, 92);
            tglSyncEdit.TabIndex = 12;
            tglSyncEdit.Text = "metroSetSwitch1";
            tglSyncEdit.ThemeAuthor = "Narwin";
            tglSyncEdit.ThemeName = "MetroDark";
            tglSyncEdit.UnCheckColor = Color.FromArgb(155, 155, 155);
            // 
            // lblSyncEdit
            // 
            lblSyncEdit.AutoSize = true;
            lblSyncEdit.Location = new Point(4, 7);
            lblSyncEdit.Name = "lblSyncEdit";
            lblSyncEdit.Size = new Size(55, 15);
            lblSyncEdit.TabIndex = 13;
            lblSyncEdit.Text = "Sync Edit";
            // 
            // chkShowCameras
            // 
            chkShowCameras.AutoSize = true;
            chkShowCameras.Location = new Point(3, 617);
            chkShowCameras.Name = "chkShowCameras";
            chkShowCameras.Size = new Size(104, 19);
            chkShowCameras.TabIndex = 11;
            chkShowCameras.Text = "Show Cameras";
            chkShowCameras.UseVisualStyleBackColor = true;
            chkShowCameras.CheckedChanged += chkShowCameras_CheckedChanged;
            // 
            // txtFilter
            // 
            txtFilter.BackColor = Color.FromArgb(26, 26, 28);
            txtFilter.BorderStyle = BorderStyle.FixedSingle;
            txtFilter.ForeColor = Color.FromArgb(213, 213, 213);
            txtFilter.Location = new Point(3, 34);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(70, 23);
            txtFilter.TabIndex = 8;
            txtFilter.Click += txtFilter_Click;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // chkShowZone
            // 
            chkShowZone.AutoSize = true;
            chkShowZone.Checked = true;
            chkShowZone.CheckState = CheckState.Checked;
            chkShowZone.Location = new Point(3, 545);
            chkShowZone.Name = "chkShowZone";
            chkShowZone.Size = new Size(83, 19);
            chkShowZone.TabIndex = 7;
            chkShowZone.Text = "Show zone";
            chkShowZone.UseVisualStyleBackColor = true;
            chkShowZone.CheckedChanged += chkShowZone_CheckedChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(1505, 776);
            splitContainer1.SplitterDistance = 641;
            splitContainer1.TabIndex = 9;
            // 
            // tsbObjects
            // 
            tsbObjects.Image = (Image)resources.GetObject("tsbObjects.Image");
            tsbObjects.ImageTransparentColor = Color.Magenta;
            tsbObjects.Name = "tsbObjects";
            tsbObjects.Size = new Size(67, 22);
            tsbObjects.Text = "Objects";
            tsbObjects.Click += tsbObjects_Click;
            // 
            // EntityEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1505, 776);
            Controls.Add(splitContainer1);
            CornerStyle = CornerPreference.Default;
            Name = "EntityEditor";
            Text = "Entity Editor";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            KeyDown += EntityEditor_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvEntities).EndInit();
            fraName.ResumeLayout(false);
            fraName.PerformLayout();
            fraPosition.ResumeLayout(false);
            fraPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            fraSettings.ResumeLayout(false);
            fraSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSettingC).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSettingB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSettingA).EndInit();
            pnProperties.ResumeLayout(false);
            tabEntity.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            fraType.ResumeLayout(false);
            fraType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSubtype).EndInit();
            ((System.ComponentModel.ISupportInitialize)numType).EndInit();
            fraID.ResumeLayout(false);
            fraID.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numID).EndInit();
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
            fraZMod.ResumeLayout(false);
            fraZMod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numZMod).EndInit();
            tbpSpecial.ResumeLayout(false);
            fraDDASection.ResumeLayout(false);
            fraDDASection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASection).EndInit();
            fraDDASettings.ResumeLayout(false);
            fraDDASettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDDASettings).EndInit();
            fraDrawOverrides.ResumeLayout(false);
            fraDrawOverrides.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHelpOverrideMult).EndInit();
            ((System.ComponentModel.ISupportInitialize)picHelpOverrideId).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideId).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDrawOverrideMult).EndInit();
            fraBoxCount.ResumeLayout(false);
            fraBoxCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numBonusBoxCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBoxCount).EndInit();
            fraVictims.ResumeLayout(false);
            fraVictims.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHelpVictimDistance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVictimDistance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEditVictimID).EndInit();
            panel2.ResumeLayout(false);
            pnLists.ResumeLayout(false);
            pnLists.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            pnSyncEdit.ResumeLayout(false);
            pnSyncEdit.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }


        #endregion

        private DataGridView dgvEntities;
        private AltUI.Controls.DarkGroupBox fraName;
        private AltUI.Controls.DarkTextBox txtName;
        private CheckBox chkName;
        private AltUI.Controls.DarkGroupBox fraPosition;
        private AltUI.Controls.DarkButton cmdSyncEntities;
        private CheckBox chkSyncPositions;
        private AltUI.Controls.DarkButton cmdEditPath;
        private Label lblPositionIndex;
        private AltUI.Controls.DarkButton cmdNextPosition;
        private AltUI.Controls.DarkButton cmdPreviousPosition;
        private AltUI.Controls.DarkButton cmdInsertPosition;
        private Label lblZ;
        private AltUI.Controls.DarkButton cmdRemovePosition;
        private Label lblY;
        private AltUI.Controls.DarkButton cmdAppendPosition;
        private Label lblX;
        private AltUI.Controls.DarkNumericUpDown numZ;
        private AltUI.Controls.DarkNumericUpDown numY;
        private AltUI.Controls.DarkNumericUpDown numX;
        private AltUI.Controls.DarkGroupBox fraSettings;
        private AltUI.Controls.DarkButton cmdPasteSetting;
        private AltUI.Controls.DarkButton cmdCopySetting;
        private Label lblSettingB;
        private Label lblSettingA;
        private Label lblArgAs;
        private CheckBox chkSettingHex;
        private AltUI.Controls.DarkNumericUpDown numSettingC;
        private Label lblSettingIndex;
        private AltUI.Controls.DarkButton cmdNextSetting;
        private AltUI.Controls.DarkButton cmdPreviousSetting;
        private AltUI.Controls.DarkButton cmdAddSetting;
        private AltUI.Controls.DarkButton cmdRemoveSetting;
        private AltUI.Controls.DarkNumericUpDown numSettingB;
        private AltUI.Controls.DarkNumericUpDown numSettingA;
        private DoubleBufferedListBox lbZones;
        private Panel pnProperties;
        private Panel panel1;
        private Panel panel2;
        private CheckBox chkShowZone;
        private SplitContainer splitContainer1;
        private AltUI.Controls.DarkGroupBox fraZMod;
        private CheckBox chkZMod;
        private AltUI.Controls.DarkNumericUpDown numZMod;
        private AltUI.Controls.DarkTextBox txtFilter;
        private MetroSet_UI.Controls.MetroSetTabControl tabEntity;
        private TabPage tbpGeneral;
        private TabPage tbpSpecial;
        private AltUI.Controls.DarkGroupBox fraC2TTSet;
        private AltUI.Controls.DarkGroupBox fraC2TTGhostTarget;
        private AltUI.Controls.DarkNumericUpDown numC2TTGhostTarget;
        private CheckBox chkC2TTGhostTarget;
        private AltUI.Controls.DarkGroupBox fraC2TTFlags;
        private AltUI.Controls.DarkNumericUpDown numC2TTFlags;
        private CheckBox chkC2TTFlags;
        private AltUI.Controls.DarkGroupBox fraC2TTYRot;
        private AltUI.Controls.DarkNumericUpDown numC2TTYRot;
        private CheckBox chkC2TTYRot;
        private AltUI.Controls.DarkGroupBox fraC2TTType;
        private CheckBox chkC2TTType;
        private Label lblC2TTYRot;
        private AltUI.Controls.DarkComboBox cmbC2TTType;
        private AltUI.Controls.DarkGroupBox fraDDASection;
        private CheckBox chkDDASection;
        private AltUI.Controls.DarkNumericUpDown numDDASection;
        private AltUI.Controls.DarkGroupBox fraDDASettings;
        private CheckBox chkDDASettings;
        private AltUI.Controls.DarkNumericUpDown numDDASettings;
        private AltUI.Controls.DarkGroupBox fraDrawOverrides;
        private CheckBox chkDrawOverrideId;
        private AltUI.Controls.DarkNumericUpDown numDrawOverrideId;
        private CheckBox chkDrawOverrideMult;
        private AltUI.Controls.DarkNumericUpDown numDrawOverrideMult;
        private AltUI.Controls.DarkGroupBox fraBoxCount;
        private CheckBox chkBonusBoxCount;
        private AltUI.Controls.DarkNumericUpDown numBonusBoxCount;
        private CheckBox chkBoxCount;
        private AltUI.Controls.DarkNumericUpDown numBoxCount;
        private AltUI.Controls.DarkGroupBox fraVictims;
        private AltUI.Controls.DarkNumericUpDown numEditVictimID;
        private AltUI.Controls.DarkListBox lbVictimID;
        private AltUI.Controls.DarkButton cmdClearAllVictims;
        private AltUI.Controls.DarkButton cmdRemoveVictim;
        private AltUI.Controls.DarkButton cmdInsertVictim;
        private Label lblVictimIndex;
        private CheckBox chkShowCameras;
        private AltUI.Controls.DarkGroupBox fraType;
        private CheckBox chkSubtype;
        private AltUI.Controls.DarkNumericUpDown numSubtype;
        private CheckBox chkType;
        private AltUI.Controls.DarkNumericUpDown numType;
        private AltUI.Controls.DarkGroupBox fraID;
        private CheckBox chkID;
        private AltUI.Controls.DarkNumericUpDown numID;
        private AltUI.Controls.DarkButton cmdCalculateVictims;
        private AltUI.Controls.DarkNumericUpDown numVictimDistance;
        private Label lblVictimDistance;
        private MetroSet_UI.Controls.MetroSetSwitch tglSyncEdit;
        private Label lblSyncEdit;
        private Panel pnSyncEdit;
        private PictureBox picHelpVictimDistance;
        private PictureBox picHelpOverrideMult;
        private PictureBox picHelpOverrideId;
        private Label lblGOOL;
        private Panel pnLists;
        private ToolStrip toolStrip1;
        private ToolStripTextBox tstSearch;
        private ToolStripLabel tslSearch;
        private CheckBox chkHideNoEntityZone;
        private ToolStripButton tsbEditDDA;
        private ToolStripButton tsbObjects;
    }
}