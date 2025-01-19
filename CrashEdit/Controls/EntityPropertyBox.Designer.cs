namespace CrashEdit.CE
{
    partial class EntityPropertyBox
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            cmdPasteProperty = new AltUI.Controls.DarkButton();
            cmdCopyProperty = new AltUI.Controls.DarkButton();
            fraPropertyControls = new AltUI.Controls.DarkGroupBox();
            chkPropertyMetaValue = new CheckBox();
            fraPropertyViewControls = new AltUI.Controls.DarkGroupBox();
            chkPropertyShowAllFields = new CheckBox();
            chkPropertyShowAsHex = new CheckBox();
            chkPropertyStyle = new CheckBox();
            lblFieldType = new Label();
            lbPropertyRaw = new AltUI.Controls.DarkListBox();
            lvPropertyHeader = new ListView();
            lblUnsupportedProperty = new Label();
            txtProperty = new AltUI.Controls.DarkTextBox();
            cmdRemoveProperty = new AltUI.Controls.DarkButton();
            cmdAppendProperty = new AltUI.Controls.DarkButton();
            dgvPropertyMetaValues = new DataGridView();
            dgvPropertyValues = new DataGridView();
            lbProperties = new AltUI.Controls.DarkListBox();
            fraPropertyID = new AltUI.Controls.DarkGroupBox();
            fraPropertyField = new AltUI.Controls.DarkGroupBox();
            fraSaveProperties = new AltUI.Controls.DarkGroupBox();
            lvSavedProperties = new ListView();
            dgvSavePropertyValues = new DataGridView();
            cmdCopyFromSaved = new AltUI.Controls.DarkButton();
            cmdRenameSavedList = new AltUI.Controls.DarkButton();
            fraPropertyControls.SuspendLayout();
            fraPropertyViewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyMetaValues).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyValues).BeginInit();
            fraPropertyID.SuspendLayout();
            fraPropertyField.SuspendLayout();
            fraSaveProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSavePropertyValues).BeginInit();
            SuspendLayout();
            // 
            // cmdPasteProperty
            // 
            cmdPasteProperty.BorderColour = Color.Empty;
            cmdPasteProperty.CustomColour = false;
            cmdPasteProperty.FlatBottom = false;
            cmdPasteProperty.FlatTop = false;
            cmdPasteProperty.Location = new Point(6, 390);
            cmdPasteProperty.Name = "cmdPasteProperty";
            cmdPasteProperty.Padding = new Padding(5);
            cmdPasteProperty.Size = new Size(75, 23);
            cmdPasteProperty.TabIndex = 14;
            cmdPasteProperty.Text = "Paste";
            cmdPasteProperty.Click += cmdPasteProperty_Click;
            // 
            // cmdCopyProperty
            // 
            cmdCopyProperty.BorderColour = Color.Empty;
            cmdCopyProperty.CustomColour = false;
            cmdCopyProperty.FlatBottom = false;
            cmdCopyProperty.FlatTop = false;
            cmdCopyProperty.Location = new Point(6, 361);
            cmdCopyProperty.Name = "cmdCopyProperty";
            cmdCopyProperty.Padding = new Padding(5);
            cmdCopyProperty.Size = new Size(75, 23);
            cmdCopyProperty.TabIndex = 14;
            cmdCopyProperty.Text = "Copy";
            cmdCopyProperty.Click += cmdCopyProperty_Click;
            // 
            // fraPropertyControls
            // 
            fraPropertyControls.Controls.Add(chkPropertyMetaValue);
            fraPropertyControls.Location = new Point(455, 159);
            fraPropertyControls.Name = "fraPropertyControls";
            fraPropertyControls.Size = new Size(148, 47);
            fraPropertyControls.TabIndex = 13;
            fraPropertyControls.TabStop = false;
            fraPropertyControls.Text = "Editor";
            // 
            // chkPropertyMetaValue
            // 
            chkPropertyMetaValue.AutoSize = true;
            chkPropertyMetaValue.Enabled = false;
            chkPropertyMetaValue.Location = new Point(6, 22);
            chkPropertyMetaValue.Name = "chkPropertyMetaValue";
            chkPropertyMetaValue.Size = new Size(119, 19);
            chkPropertyMetaValue.TabIndex = 10;
            chkPropertyMetaValue.Text = "Toggle MetaValue";
            chkPropertyMetaValue.UseVisualStyleBackColor = true;
            chkPropertyMetaValue.Click += chkPropertyMetaValue_Click;
            // 
            // fraPropertyViewControls
            // 
            fraPropertyViewControls.Controls.Add(chkPropertyShowAllFields);
            fraPropertyViewControls.Controls.Add(chkPropertyShowAsHex);
            fraPropertyViewControls.Controls.Add(chkPropertyStyle);
            fraPropertyViewControls.Location = new Point(455, 57);
            fraPropertyViewControls.Name = "fraPropertyViewControls";
            fraPropertyViewControls.Size = new Size(148, 96);
            fraPropertyViewControls.TabIndex = 12;
            fraPropertyViewControls.TabStop = false;
            fraPropertyViewControls.Text = "Visual";
            // 
            // chkPropertyShowAllFields
            // 
            chkPropertyShowAllFields.AutoSize = true;
            chkPropertyShowAllFields.Location = new Point(6, 21);
            chkPropertyShowAllFields.Name = "chkPropertyShowAllFields";
            chkPropertyShowAllFields.Size = new Size(101, 19);
            chkPropertyShowAllFields.TabIndex = 9;
            chkPropertyShowAllFields.Text = "Show all fields";
            chkPropertyShowAllFields.UseVisualStyleBackColor = true;
            chkPropertyShowAllFields.CheckedChanged += chkPropertyShowAllFields_CheckedChanged;
            // 
            // chkPropertyShowAsHex
            // 
            chkPropertyShowAsHex.AutoSize = true;
            chkPropertyShowAsHex.Checked = true;
            chkPropertyShowAsHex.CheckState = CheckState.Checked;
            chkPropertyShowAsHex.Location = new Point(6, 71);
            chkPropertyShowAsHex.Name = "chkPropertyShowAsHex";
            chkPropertyShowAsHex.Size = new Size(47, 19);
            chkPropertyShowAsHex.TabIndex = 11;
            chkPropertyShowAsHex.Text = "Hex";
            chkPropertyShowAsHex.UseVisualStyleBackColor = true;
            chkPropertyShowAsHex.Click += chkPropertyShowAsHex_Click;
            // 
            // chkPropertyStyle
            // 
            chkPropertyStyle.AutoSize = true;
            chkPropertyStyle.Location = new Point(6, 46);
            chkPropertyStyle.Name = "chkPropertyStyle";
            chkPropertyStyle.Size = new Size(89, 19);
            chkPropertyStyle.TabIndex = 5;
            chkPropertyStyle.Text = "Toggle View";
            chkPropertyStyle.UseVisualStyleBackColor = true;
            chkPropertyStyle.CheckedChanged += chkPropertyStyle_CheckedChanged;
            // 
            // lblFieldType
            // 
            lblFieldType.AutoSize = true;
            lblFieldType.Location = new Point(455, 36);
            lblFieldType.Name = "lblFieldType";
            lblFieldType.Size = new Size(41, 15);
            lblFieldType.TabIndex = 8;
            lblFieldType.Text = "(int32)";
            // 
            // lbPropertyRaw
            // 
            lbPropertyRaw.BackColor = Color.FromArgb(26, 26, 28);
            lbPropertyRaw.BorderStyle = BorderStyle.FixedSingle;
            lbPropertyRaw.ForeColor = Color.FromArgb(213, 213, 213);
            lbPropertyRaw.FormattingEnabled = true;
            lbPropertyRaw.HorizontalScrollbar = true;
            lbPropertyRaw.Location = new Point(6, 76);
            lbPropertyRaw.Name = "lbPropertyRaw";
            lbPropertyRaw.Size = new Size(340, 47);
            lbPropertyRaw.TabIndex = 7;
            // 
            // lvPropertyHeader
            // 
            lvPropertyHeader.BorderStyle = BorderStyle.FixedSingle;
            lvPropertyHeader.FullRowSelect = true;
            lvPropertyHeader.Location = new Point(6, 22);
            lvPropertyHeader.Name = "lvPropertyHeader";
            lvPropertyHeader.Scrollable = false;
            lvPropertyHeader.Size = new Size(340, 48);
            lvPropertyHeader.TabIndex = 6;
            lvPropertyHeader.UseCompatibleStateImageBehavior = false;
            lvPropertyHeader.View = View.Details;
            // 
            // lblUnsupportedProperty
            // 
            lblUnsupportedProperty.AutoSize = true;
            lblUnsupportedProperty.ForeColor = Color.Red;
            lblUnsupportedProperty.Location = new Point(455, 3);
            lblUnsupportedProperty.Name = "lblUnsupportedProperty";
            lblUnsupportedProperty.Size = new Size(153, 30);
            lblUnsupportedProperty.TabIndex = 4;
            lblUnsupportedProperty.Text = "Unsupported property field!\r\n(unknown)";
            lblUnsupportedProperty.Visible = false;
            // 
            // txtProperty
            // 
            txtProperty.BackColor = Color.FromArgb(26, 26, 28);
            txtProperty.BorderStyle = BorderStyle.FixedSingle;
            txtProperty.ForeColor = Color.FromArgb(213, 213, 213);
            txtProperty.Location = new Point(6, 255);
            txtProperty.MaxLength = 4;
            txtProperty.Name = "txtProperty";
            txtProperty.Size = new Size(75, 23);
            txtProperty.TabIndex = 3;
            txtProperty.TextChanged += txtProperty_TextChanged;
            txtProperty.KeyPress += txtProperty_KeyPress;
            // 
            // cmdRemoveProperty
            // 
            cmdRemoveProperty.BorderColour = Color.Empty;
            cmdRemoveProperty.CustomColour = false;
            cmdRemoveProperty.FlatBottom = false;
            cmdRemoveProperty.FlatTop = false;
            cmdRemoveProperty.Location = new Point(6, 313);
            cmdRemoveProperty.Name = "cmdRemoveProperty";
            cmdRemoveProperty.Padding = new Padding(5);
            cmdRemoveProperty.Size = new Size(75, 23);
            cmdRemoveProperty.TabIndex = 2;
            cmdRemoveProperty.Text = "Remove";
            cmdRemoveProperty.Click += cmdRemoveProperty_Click;
            // 
            // cmdAppendProperty
            // 
            cmdAppendProperty.BorderColour = Color.Empty;
            cmdAppendProperty.CustomColour = false;
            cmdAppendProperty.FlatBottom = false;
            cmdAppendProperty.FlatTop = false;
            cmdAppendProperty.Location = new Point(6, 284);
            cmdAppendProperty.Name = "cmdAppendProperty";
            cmdAppendProperty.Padding = new Padding(5);
            cmdAppendProperty.Size = new Size(75, 23);
            cmdAppendProperty.TabIndex = 2;
            cmdAppendProperty.Text = "Append";
            cmdAppendProperty.Click += cmdAppendProperty_Click;
            // 
            // dgvPropertyMetaValues
            // 
            dgvPropertyMetaValues.AllowUserToAddRows = false;
            dgvPropertyMetaValues.AllowUserToResizeColumns = false;
            dgvPropertyMetaValues.AllowUserToResizeRows = false;
            dgvPropertyMetaValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPropertyMetaValues.ColumnHeadersHeight = 24;
            dgvPropertyMetaValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPropertyMetaValues.Location = new Point(6, 129);
            dgvPropertyMetaValues.Name = "dgvPropertyMetaValues";
            dgvPropertyMetaValues.RowHeadersWidth = 24;
            dgvPropertyMetaValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPropertyMetaValues.ScrollBars = ScrollBars.Vertical;
            dgvPropertyMetaValues.ShowCellToolTips = false;
            dgvPropertyMetaValues.Size = new Size(96, 279);
            dgvPropertyMetaValues.TabIndex = 1;
            dgvPropertyMetaValues.CellBeginEdit += dgvPropertyMetaValues_CellBeginEdit;
            dgvPropertyMetaValues.CellValidating += dgvPropertyMetaValues_CellValidating;
            dgvPropertyMetaValues.CellValueChanged += dgvPropertyMetaValues_CellValueChanged;
            dgvPropertyMetaValues.EditingControlShowing += dgvPropertyMetaValues_EditingControlShowing;
            dgvPropertyMetaValues.SelectionChanged += dgvPropertyMetaValues_SelectionChanged;
            // 
            // dgvPropertyValues
            // 
            dgvPropertyValues.AllowUserToAddRows = false;
            dgvPropertyValues.AllowUserToResizeColumns = false;
            dgvPropertyValues.AllowUserToResizeRows = false;
            dgvPropertyValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPropertyValues.ColumnHeadersHeight = 24;
            dgvPropertyValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPropertyValues.Location = new Point(108, 129);
            dgvPropertyValues.Name = "dgvPropertyValues";
            dgvPropertyValues.RowHeadersWidth = 24;
            dgvPropertyValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPropertyValues.ScrollBars = ScrollBars.Vertical;
            dgvPropertyValues.ShowCellToolTips = false;
            dgvPropertyValues.Size = new Size(238, 279);
            dgvPropertyValues.TabIndex = 1;
            dgvPropertyValues.CellFormatting += dgvPropertyValues_CellFormatting;
            dgvPropertyValues.CellParsing += dgvPropertyValues_CellParsing;
            dgvPropertyValues.CellValidating += dgvPropertyValues_CellValidating;
            dgvPropertyValues.CellValueChanged += dgvPropertyValues_CellValueChanged;
            dgvPropertyValues.EditingControlShowing += dgvPropertyValues_EditingControlShowing;
            // 
            // lbProperties
            // 
            lbProperties.BackColor = Color.FromArgb(26, 26, 28);
            lbProperties.BorderStyle = BorderStyle.FixedSingle;
            lbProperties.ForeColor = Color.FromArgb(213, 213, 213);
            lbProperties.FormattingEnabled = true;
            lbProperties.Location = new Point(6, 22);
            lbProperties.Name = "lbProperties";
            lbProperties.SelectionMode = SelectionMode.MultiExtended;
            lbProperties.Size = new Size(75, 227);
            lbProperties.Sorted = true;
            lbProperties.TabIndex = 0;
            lbProperties.SelectedIndexChanged += lbProperties_SelectedIndexChanged;
            lbProperties.KeyDown += lbProperties_KeyDown;
            // 
            // fraPropertyID
            // 
            fraPropertyID.BackColor = Color.Transparent;
            fraPropertyID.Controls.Add(lbProperties);
            fraPropertyID.Controls.Add(cmdPasteProperty);
            fraPropertyID.Controls.Add(txtProperty);
            fraPropertyID.Controls.Add(cmdCopyProperty);
            fraPropertyID.Controls.Add(cmdAppendProperty);
            fraPropertyID.Controls.Add(cmdRemoveProperty);
            fraPropertyID.Location = new Point(3, 3);
            fraPropertyID.Name = "fraPropertyID";
            fraPropertyID.Size = new Size(87, 423);
            fraPropertyID.TabIndex = 15;
            fraPropertyID.TabStop = false;
            fraPropertyID.Text = "ID";
            // 
            // fraPropertyField
            // 
            fraPropertyField.BackColor = Color.Transparent;
            fraPropertyField.Controls.Add(lvPropertyHeader);
            fraPropertyField.Controls.Add(dgvPropertyValues);
            fraPropertyField.Controls.Add(dgvPropertyMetaValues);
            fraPropertyField.Controls.Add(lbPropertyRaw);
            fraPropertyField.Location = new Point(96, 3);
            fraPropertyField.Name = "fraPropertyField";
            fraPropertyField.Size = new Size(353, 423);
            fraPropertyField.TabIndex = 16;
            fraPropertyField.TabStop = false;
            fraPropertyField.Text = "Field";
            // 
            // fraSaveProperties
            // 
            fraSaveProperties.Controls.Add(lvSavedProperties);
            fraSaveProperties.Controls.Add(dgvSavePropertyValues);
            fraSaveProperties.Location = new Point(0, 461);
            fraSaveProperties.Name = "fraSaveProperties";
            fraSaveProperties.Size = new Size(600, 212);
            fraSaveProperties.TabIndex = 17;
            fraSaveProperties.TabStop = false;
            fraSaveProperties.Text = "Saved Properties";
            // 
            // lvSavedProperties
            // 
            lvSavedProperties.BorderStyle = BorderStyle.FixedSingle;
            lvSavedProperties.FullRowSelect = true;
            lvSavedProperties.LabelEdit = true;
            lvSavedProperties.Location = new Point(6, 22);
            lvSavedProperties.Name = "lvSavedProperties";
            lvSavedProperties.Size = new Size(120, 184);
            lvSavedProperties.TabIndex = 6;
            lvSavedProperties.UseCompatibleStateImageBehavior = false;
            lvSavedProperties.View = View.Details;
            lvSavedProperties.SelectedIndexChanged += lvSavedProperties_SelectedIndexChanged;
            lvSavedProperties.KeyDown += lvSavedProperties_KeyDown;
            lvSavedProperties.AfterLabelEdit += lvSavedProperties_AfterLabelEdit;
            // 
            // dgvSavePropertyValues
            // 
            dgvSavePropertyValues.AllowUserToAddRows = false;
            dgvSavePropertyValues.AllowUserToResizeColumns = false;
            dgvSavePropertyValues.AllowUserToResizeRows = false;
            dgvSavePropertyValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSavePropertyValues.ColumnHeadersHeight = 24;
            dgvSavePropertyValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSavePropertyValues.Location = new Point(132, 22);
            dgvSavePropertyValues.Name = "dgvSavePropertyValues";
            dgvSavePropertyValues.RowHeadersWidth = 24;
            dgvSavePropertyValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvSavePropertyValues.ScrollBars = ScrollBars.Vertical;
            dgvSavePropertyValues.ShowCellToolTips = false;
            dgvSavePropertyValues.Size = new Size(462, 184);
            dgvSavePropertyValues.TabIndex = 1;
            dgvSavePropertyValues.CellBeginEdit += dgvSavePropertyValues_CellBeginEdit;
            dgvSavePropertyValues.CellValueChanged += dgvSavePropertyValues_CellValueChanged;
            // 
            // cmdCopyFromSaved
            // 
            cmdCopyFromSaved.BorderColour = Color.Empty;
            cmdCopyFromSaved.CustomColour = false;
            cmdCopyFromSaved.FlatBottom = false;
            cmdCopyFromSaved.FlatTop = false;
            cmdCopyFromSaved.Location = new Point(9, 432);
            cmdCopyFromSaved.Name = "cmdCopyFromSaved";
            cmdCopyFromSaved.Padding = new Padding(5);
            cmdCopyFromSaved.Size = new Size(117, 23);
            cmdCopyFromSaved.TabIndex = 18;
            cmdCopyFromSaved.Text = "Copy From Saved";
            cmdCopyFromSaved.Click += cmdCopyFromSaved_Click;
            // 
            // cmdRenameSavedList
            // 
            cmdRenameSavedList.BorderColour = Color.Empty;
            cmdRenameSavedList.CustomColour = false;
            cmdRenameSavedList.FlatBottom = false;
            cmdRenameSavedList.FlatTop = false;
            cmdRenameSavedList.Location = new Point(3, 679);
            cmdRenameSavedList.Name = "cmdRenameSavedList";
            cmdRenameSavedList.Padding = new Padding(5);
            cmdRenameSavedList.Size = new Size(75, 23);
            cmdRenameSavedList.TabIndex = 19;
            cmdRenameSavedList.Text = "Rename";
            cmdRenameSavedList.Click += cmdRenameSavedList_Click;
            // 
            // EntityPropertyBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(cmdRenameSavedList);
            Controls.Add(cmdCopyFromSaved);
            Controls.Add(fraSaveProperties);
            Controls.Add(fraPropertyField);
            Controls.Add(fraPropertyID);
            Controls.Add(fraPropertyControls);
            Controls.Add(fraPropertyViewControls);
            Controls.Add(lblFieldType);
            Controls.Add(lblUnsupportedProperty);
            Name = "EntityPropertyBox";
            Size = new Size(916, 832);
            fraPropertyControls.ResumeLayout(false);
            fraPropertyControls.PerformLayout();
            fraPropertyViewControls.ResumeLayout(false);
            fraPropertyViewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyMetaValues).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyValues).EndInit();
            fraPropertyID.ResumeLayout(false);
            fraPropertyID.PerformLayout();
            fraPropertyField.ResumeLayout(false);
            fraSaveProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSavePropertyValues).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AltUI.Controls.DarkButton cmdPasteProperty;
        private AltUI.Controls.DarkButton cmdCopyProperty;
        private AltUI.Controls.DarkGroupBox fraPropertyControls;
        private CheckBox chkPropertyMetaValue;
        private AltUI.Controls.DarkGroupBox fraPropertyViewControls;
        private CheckBox chkPropertyShowAllFields;
        private CheckBox chkPropertyShowAsHex;
        private CheckBox chkPropertyStyle;
        private Label lblFieldType;
        private AltUI.Controls.DarkListBox lbPropertyRaw;
        private ListView lvPropertyHeader;
        private Label lblUnsupportedProperty;
        private AltUI.Controls.DarkTextBox txtProperty;
        private AltUI.Controls.DarkButton cmdRemoveProperty;
        private AltUI.Controls.DarkButton cmdAppendProperty;
        private DataGridView dgvPropertyMetaValues;
        private DataGridView dgvPropertyValues;
        private AltUI.Controls.DarkListBox lbProperties;
        private AltUI.Controls.DarkGroupBox fraPropertyID;
        private AltUI.Controls.DarkGroupBox fraPropertyField;
        private AltUI.Controls.DarkGroupBox fraSaveProperties;
        private ListView lvSavedProperties;
        private DataGridView dgvSavePropertyValues;
        private AltUI.Controls.DarkButton cmdCopyFromSaved;
        private AltUI.Controls.DarkButton cmdRenameSavedList;
    }
}
