using AltUI.Controls;
using CrashEdit.Crash.GOOLIns;
using MetroSet_UI.Controls;
using MetroSet_UI.Enums;

namespace CrashEdit.CE
{
    partial class GOOLBox
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
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            tbcTabs = new MetroSetTabControl();
            tbpGeneral = new TabPage();
            fraGoolClass = new DarkGroupBox();
            numGoolClass = new DarkNumericUpDown();
            fraGoolType = new DarkGroupBox();
            numGoolType = new DarkNumericUpDown();
            tbpCode = new TabPage();
            tbpDataPool = new TabPage();
            darkGroupBox1 = new DarkGroupBox();
            tglPoolView = new MetroSetSwitch();
            dgvPool = new DataGridView();
            tbpStateMap = new TabPage();
            dgvStateMap = new DataGridView();
            tbpStateDescriptors = new TabPage();
            fraExternalIndex = new DarkGroupBox();
            numExternalIndex = new DarkNumericUpDown();
            fraExternal = new DarkGroupBox();
            chkIsExternal = new CheckBox();
            lblExternalEID = new Label();
            dgvStateDescriptors = new DataGridView();
            tbcTabs.SuspendLayout();
            tbpGeneral.SuspendLayout();
            fraGoolClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGoolClass).BeginInit();
            fraGoolType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numGoolType).BeginInit();
            tbpDataPool.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPool).BeginInit();
            tbpStateMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStateMap).BeginInit();
            tbpStateDescriptors.SuspendLayout();
            fraExternalIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numExternalIndex).BeginInit();
            fraExternal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStateDescriptors).BeginInit();
            SuspendLayout();
            // 
            // tbcTabs
            // 
            tbcTabs.AnimateEasingType = EasingType.CubeOut;
            tbcTabs.AnimateTime = 200;
            tbcTabs.BackgroundColor = Color.FromArgb(31, 31, 32);
            tbcTabs.Controls.Add(tbpGeneral);
            tbcTabs.Controls.Add(tbpCode);
            tbcTabs.Controls.Add(tbpDataPool);
            tbcTabs.Controls.Add(tbpStateMap);
            tbcTabs.Controls.Add(tbpStateDescriptors);
            tbcTabs.Dock = DockStyle.Fill;
            tbcTabs.IsDerivedStyle = false;
            tbcTabs.ItemSize = new Size(100, 28);
            tbcTabs.Location = new Point(0, 0);
            tbcTabs.Name = "tbcTabs";
            tbcTabs.SelectedIndex = 1;
            tbcTabs.SelectedTextColor = Color.White;
            tbcTabs.Size = new Size(800, 800);
            tbcTabs.SizeMode = TabSizeMode.Fixed;
            tbcTabs.Speed = 100;
            tbcTabs.Style = Style.Dark;
            tbcTabs.StyleManager = null;
            tbcTabs.TabIndex = 0;
            tbcTabs.ThemeAuthor = "Narwin";
            tbcTabs.ThemeName = "MetroDark";
            tbcTabs.UnselectedTextColor = Color.Gray;
            tbcTabs.UseAnimation = false;
            // 
            // tbpGeneral
            // 
            tbpGeneral.AutoScroll = true;
            tbpGeneral.BackColor = Color.FromArgb(31, 31, 32);
            tbpGeneral.Controls.Add(fraGoolClass);
            tbpGeneral.Controls.Add(fraGoolType);
            tbpGeneral.Location = new Point(4, 32);
            tbpGeneral.Name = "tbpGeneral";
            tbpGeneral.Size = new Size(792, 764);
            tbpGeneral.TabIndex = 0;
            tbpGeneral.Text = "General";
            tbpGeneral.Enter += tbpGeneral_Enter;
            // 
            // fraGoolClass
            // 
            fraGoolClass.BackColor = Color.Transparent;
            fraGoolClass.Controls.Add(numGoolClass);
            fraGoolClass.Location = new Point(3, 69);
            fraGoolClass.Name = "fraGoolClass";
            fraGoolClass.Size = new Size(92, 60);
            fraGoolClass.TabIndex = 0;
            fraGoolClass.TabStop = false;
            fraGoolClass.Text = "Class";
            // 
            // numGoolClass
            // 
            numGoolClass.Location = new Point(6, 22);
            numGoolClass.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numGoolClass.Name = "numGoolClass";
            numGoolClass.Size = new Size(80, 23);
            numGoolClass.TabIndex = 0;
            numGoolClass.ValueChanged += numGoolClass_ValueChanged;
            // 
            // fraGoolType
            // 
            fraGoolType.BackColor = Color.Transparent;
            fraGoolType.Controls.Add(numGoolType);
            fraGoolType.Location = new Point(3, 3);
            fraGoolType.Name = "fraGoolType";
            fraGoolType.Size = new Size(92, 60);
            fraGoolType.TabIndex = 0;
            fraGoolType.TabStop = false;
            fraGoolType.Text = "Type";
            // 
            // numGoolType
            // 
            numGoolType.Location = new Point(6, 22);
            numGoolType.Maximum = new decimal(new int[] { 63, 0, 0, 0 });
            numGoolType.Name = "numGoolType";
            numGoolType.Size = new Size(80, 23);
            numGoolType.TabIndex = 0;
            numGoolType.ValueChanged += numGoolType_ValueChanged;
            // 
            // tbpCode
            // 
            tbpCode.AutoScroll = true;
            tbpCode.BackColor = Color.FromArgb(31, 31, 32);
            tbpCode.Location = new Point(4, 32);
            tbpCode.Name = "tbpCode";
            tbpCode.Size = new Size(792, 764);
            tbpCode.TabIndex = 1;
            tbpCode.Text = "Code";
            // 
            // tbpDataPool
            // 
            tbpDataPool.AutoScroll = true;
            tbpDataPool.BackColor = Color.FromArgb(31, 31, 32);
            tbpDataPool.Controls.Add(darkGroupBox1);
            tbpDataPool.Controls.Add(dgvPool);
            tbpDataPool.Location = new Point(4, 32);
            tbpDataPool.Name = "tbpDataPool";
            tbpDataPool.Size = new Size(792, 764);
            tbpDataPool.TabIndex = 2;
            tbpDataPool.Text = "Data Pool";
            tbpDataPool.Enter += tbpDataPool_Enter;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.BackColor = Color.Transparent;
            darkGroupBox1.Controls.Add(tglPoolView);
            darkGroupBox1.Location = new Point(347, 3);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(97, 65);
            darkGroupBox1.TabIndex = 2;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Toggle View";
            // 
            // tglPoolView
            // 
            tglPoolView.BackColor = Color.Transparent;
            tglPoolView.BackgroundColor = Color.Empty;
            tglPoolView.BorderColor = Color.FromArgb(155, 155, 155);
            tglPoolView.CheckColor = Color.FromArgb(65, 177, 225);
            tglPoolView.CheckState = MetroSet_UI.Enums.CheckState.Unchecked;
            tglPoolView.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            tglPoolView.DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
            tglPoolView.DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
            tglPoolView.IsDerivedStyle = true;
            tglPoolView.Location = new Point(10, 26);
            tglPoolView.Name = "tglPoolView";
            tglPoolView.Size = new Size(58, 22);
            tglPoolView.Style = Style.Dark;
            tglPoolView.StyleManager = null;
            tglPoolView.Switched = false;
            tglPoolView.SymbolColor = Color.FromArgb(92, 92, 92);
            tglPoolView.TabIndex = 0;
            tglPoolView.Text = "metroSetSwitch1";
            tglPoolView.ThemeAuthor = "Narwin";
            tglPoolView.ThemeName = "MetroDark";
            tglPoolView.UnCheckColor = Color.FromArgb(155, 155, 155);
            tglPoolView.SwitchedChanged += tglPoolView_SwitchedChanged;
            // 
            // dgvPool
            // 
            dgvPool.AllowUserToAddRows = false;
            dgvPool.AllowUserToResizeColumns = false;
            dgvPool.AllowUserToResizeRows = false;
            dgvPool.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPool.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPool.Location = new Point(3, 3);
            dgvPool.MultiSelect = false;
            dgvPool.Name = "dgvPool";
            dgvPool.RowHeadersWidth = 24;
            dgvPool.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPool.ScrollBars = ScrollBars.Vertical;
            dgvPool.ShowCellToolTips = false;
            dgvPool.Size = new Size(338, 548);
            dgvPool.TabIndex = 0;
            dgvPool.CellBeginEdit += dgvPool_CellBeginEdit;
            dgvPool.CellFormatting += dgvPool_CellFormatting;
            dgvPool.CellParsing += dgvPool_CellParsing;
            dgvPool.CellValidating += dgvPool_CellValidating;
            dgvPool.CellValueChanged += dgvPool_CellValueChanged;
            dgvPool.EditingControlShowing += dgvPool_EditingControlShowing;
            dgvPool.KeyDown += dgvPool_KeyDown;
            // 
            // tbpStateMap
            // 
            tbpStateMap.AutoScroll = true;
            tbpStateMap.BackColor = Color.FromArgb(31, 31, 32);
            tbpStateMap.Controls.Add(dgvStateMap);
            tbpStateMap.Location = new Point(4, 32);
            tbpStateMap.Name = "tbpStateMap";
            tbpStateMap.Size = new Size(792, 764);
            tbpStateMap.TabIndex = 3;
            tbpStateMap.Text = "State Map";
            tbpStateMap.Enter += tbpStateMap_Enter;
            // 
            // dgvStateMap
            // 
            dgvStateMap.AllowUserToAddRows = false;
            dgvStateMap.AllowUserToResizeColumns = false;
            dgvStateMap.AllowUserToResizeRows = false;
            dgvStateMap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStateMap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStateMap.Location = new Point(3, 3);
            dgvStateMap.MultiSelect = false;
            dgvStateMap.Name = "dgvStateMap";
            dgvStateMap.RowHeadersWidth = 24;
            dgvStateMap.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvStateMap.ScrollBars = ScrollBars.Vertical;
            dgvStateMap.ShowCellToolTips = false;
            dgvStateMap.Size = new Size(380, 596);
            dgvStateMap.TabIndex = 0;
            dgvStateMap.CellBeginEdit += dgvStateMap_CellBeginEdit;
            dgvStateMap.CellFormatting += dgvStateMap_CellFormatting;
            dgvStateMap.CellValidating += dgvStateMap_CellValidating;
            dgvStateMap.CellValueChanged += dgvStateMap_CellValueChanged;
            // 
            // tbpStateDescriptors
            // 
            tbpStateDescriptors.AutoScroll = true;
            tbpStateDescriptors.BackColor = Color.FromArgb(31, 31, 32);
            tbpStateDescriptors.Controls.Add(fraExternalIndex);
            tbpStateDescriptors.Controls.Add(fraExternal);
            tbpStateDescriptors.Controls.Add(dgvStateDescriptors);
            tbpStateDescriptors.Location = new Point(4, 32);
            tbpStateDescriptors.Name = "tbpStateDescriptors";
            tbpStateDescriptors.Size = new Size(792, 764);
            tbpStateDescriptors.TabIndex = 3;
            tbpStateDescriptors.Text = "State Descriptors";
            tbpStateDescriptors.Enter += tbpStateDescriptors_Enter;
            // 
            // fraExternalIndex
            // 
            fraExternalIndex.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraExternalIndex.BackColor = Color.Transparent;
            fraExternalIndex.Controls.Add(numExternalIndex);
            fraExternalIndex.Location = new Point(609, 87);
            fraExternalIndex.Name = "fraExternalIndex";
            fraExternalIndex.Size = new Size(141, 74);
            fraExternalIndex.TabIndex = 4;
            fraExternalIndex.TabStop = false;
            fraExternalIndex.Text = "External GOOL Index";
            // 
            // numExternalIndex
            // 
            numExternalIndex.Location = new Point(6, 22);
            numExternalIndex.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            numExternalIndex.Name = "numExternalIndex";
            numExternalIndex.Size = new Size(77, 23);
            numExternalIndex.TabIndex = 3;
            numExternalIndex.ValueChanged += numExternalIndex_ValueChanged;
            // 
            // fraExternal
            // 
            fraExternal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraExternal.BackColor = Color.Transparent;
            fraExternal.Controls.Add(chkIsExternal);
            fraExternal.Controls.Add(lblExternalEID);
            fraExternal.Location = new Point(609, 3);
            fraExternal.Name = "fraExternal";
            fraExternal.Size = new Size(141, 78);
            fraExternal.TabIndex = 3;
            fraExternal.TabStop = false;
            fraExternal.Text = "External GOOL";
            // 
            // chkIsExternal
            // 
            chkIsExternal.AutoSize = true;
            chkIsExternal.BackColor = Color.Transparent;
            chkIsExternal.Location = new Point(6, 22);
            chkIsExternal.Name = "chkIsExternal";
            chkIsExternal.Size = new Size(68, 19);
            chkIsExternal.TabIndex = 1;
            chkIsExternal.Text = "Enabled";
            chkIsExternal.UseVisualStyleBackColor = false;
            chkIsExternal.CheckedChanged += chkIsExternal_CheckedChanged;
            // 
            // lblExternalEID
            // 
            lblExternalEID.AutoSize = true;
            lblExternalEID.BackColor = Color.Transparent;
            lblExternalEID.ForeColor = Color.Turquoise;
            lblExternalEID.Location = new Point(6, 44);
            lblExternalEID.Name = "lblExternalEID";
            lblExternalEID.Size = new Size(77, 15);
            lblExternalEID.TabIndex = 2;
            lblExternalEID.Text = "{external EID}";
            // 
            // dgvStateDescriptors
            // 
            dgvStateDescriptors.AllowUserToAddRows = false;
            dgvStateDescriptors.AllowUserToResizeColumns = false;
            dgvStateDescriptors.AllowUserToResizeRows = false;
            dgvStateDescriptors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStateDescriptors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStateDescriptors.Location = new Point(3, 3);
            dgvStateDescriptors.MultiSelect = false;
            dgvStateDescriptors.Name = "dgvStateDescriptors";
            dgvStateDescriptors.RowHeadersWidth = 24;
            dgvStateDescriptors.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvStateDescriptors.ScrollBars = ScrollBars.Vertical;
            dgvStateDescriptors.ShowCellToolTips = false;
            dgvStateDescriptors.Size = new Size(600, 596);
            dgvStateDescriptors.TabIndex = 0;
            dgvStateDescriptors.CellBeginEdit += dgvStateDescriptors_CellBeginEdit;
            dgvStateDescriptors.CellFormatting += dgvStateDescriptors_CellFormatting;
            dgvStateDescriptors.CellParsing += dgvStateDescriptors_CellParsing;
            dgvStateDescriptors.CellValidating += dgvStateDescriptors_CellValidating;
            dgvStateDescriptors.CellValueChanged += dgvStateDescriptors_CellValueChanged;
            dgvStateDescriptors.SelectionChanged += dgvStateDescriptors_SelectionChanged;
            // 
            // GOOLBox
            // 
            Controls.Add(tbcTabs);
            Name = "GOOLBox";
            Size = new Size(800, 800);
            tbcTabs.ResumeLayout(false);
            tbpGeneral.ResumeLayout(false);
            fraGoolClass.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numGoolClass).EndInit();
            fraGoolType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numGoolType).EndInit();
            tbpDataPool.ResumeLayout(false);
            darkGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPool).EndInit();
            tbpStateMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStateMap).EndInit();
            tbpStateDescriptors.ResumeLayout(false);
            fraExternalIndex.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numExternalIndex).EndInit();
            fraExternal.ResumeLayout(false);
            fraExternal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStateDescriptors).EndInit();
            ResumeLayout(false);
        }





        #endregion

        private MetroSetTabControl tbcTabs;
        private TabPage tbpGeneral;
        private TabPage tbpCode;
        private TabPage tbpDataPool;
        private TabPage tbpStateMap;
        private TabPage tbpStateDescriptors;
        private DataGridView dgvPool;
        private DarkGroupBox fraGoolType;
        private DarkNumericUpDown numGoolType;
        private DarkGroupBox fraGoolClass;
        private DarkNumericUpDown numGoolClass;
        private DataGridView dgvStateMap;
        private DataGridView dgvStateDescriptors;
        private CheckBox chkIsExternal;
        private Label lblExternalEID;
        private DarkGroupBox fraExternal;
        private DarkNumericUpDown numExternalIndex;
        private DarkGroupBox fraExternalIndex;
        private DarkGroupBox darkGroupBox1;
        private MetroSetSwitch tglPoolView;
    }
}
