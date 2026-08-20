using AltUI.Controls;

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
            cmdCopyProperty = new DarkButton();
            fraPropertyControls = new DarkGroupBox();
            chkPropertyMetaValue = new CheckBox();
            fraPropertyViewControls = new DarkGroupBox();
            chkPropertyShowAllFields = new CheckBox();
            chkPropertyShowAsHex = new CheckBox();
            chkPropertyStyle = new CheckBox();
            lblFieldType = new Label();
            lbPropertyRaw = new DarkListBox();
            lvPropertyHeader = new ListView();
            lblUnsupportedProperty = new Label();
            txtProperty = new DarkTextBox();
            cmdRemoveProperty = new DarkButton();
            cmdAppendProperty = new DarkButton();
            dgvPropertyMetaValues = new DataGridView();
            dgvPropertyValues = new DataGridView();
            lbProperties = new DarkListBox();
            fraPropertyID = new DarkGroupBox();
            fraPropertyField = new DarkGroupBox();
            rbtReload2 = new MetroSet_UI.Controls.MetroSetRadioButton();
            fraSaveProperties = new DarkGroupBox();
            rbtReload = new MetroSet_UI.Controls.MetroSetRadioButton();
            pnControlsSaved = new Panel();
            cmdRenameSavedList = new DarkButton();
            cmdCopyFromSaved = new DarkButton();
            cmdRemoveSavedList = new DarkButton();
            lbSavedProperties = new DarkListBox();
            dgvSavePropertyValues = new DataGridView();
            fraPropertyControls.SuspendLayout();
            fraPropertyViewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyMetaValues).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyValues).BeginInit();
            fraPropertyID.SuspendLayout();
            fraPropertyField.SuspendLayout();
            fraSaveProperties.SuspendLayout();
            pnControlsSaved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSavePropertyValues).BeginInit();
            SuspendLayout();
            // 
            // cmdCopyProperty
            // 
            cmdCopyProperty.BorderColour = Color.Empty;
            cmdCopyProperty.CustomColour = false;
            cmdCopyProperty.FlatBottom = false;
            cmdCopyProperty.FlatTop = false;
            cmdCopyProperty.Location = new Point(9, 652);
            cmdCopyProperty.Margin = new Padding(4, 5, 4, 5);
            cmdCopyProperty.Name = "cmdCopyProperty";
            cmdCopyProperty.Padding = new Padding(7, 8, 7, 8);
            cmdCopyProperty.Size = new Size(107, 43);
            cmdCopyProperty.TabIndex = 14;
            cmdCopyProperty.Text = "Save";
            cmdCopyProperty.Click += cmdCopyProperty_Click;
            // 
            // fraPropertyControls
            // 
            fraPropertyControls.Controls.Add(chkPropertyMetaValue);
            fraPropertyControls.Location = new Point(650, 265);
            fraPropertyControls.Margin = new Padding(4, 5, 4, 5);
            fraPropertyControls.Name = "fraPropertyControls";
            fraPropertyControls.Padding = new Padding(4, 5, 4, 5);
            fraPropertyControls.Size = new Size(211, 78);
            fraPropertyControls.TabIndex = 13;
            fraPropertyControls.TabStop = false;
            fraPropertyControls.Text = "Data Editor";
            // 
            // chkPropertyMetaValue
            // 
            chkPropertyMetaValue.AutoSize = true;
            chkPropertyMetaValue.Enabled = false;
            chkPropertyMetaValue.Location = new Point(9, 37);
            chkPropertyMetaValue.Margin = new Padding(4, 5, 4, 5);
            chkPropertyMetaValue.Name = "chkPropertyMetaValue";
            chkPropertyMetaValue.Size = new Size(186, 29);
            chkPropertyMetaValue.TabIndex = 10;
            chkPropertyMetaValue.Text = "Toggle MetaValues";
            chkPropertyMetaValue.UseVisualStyleBackColor = true;
            chkPropertyMetaValue.Click += chkPropertyMetaValue_Click;
            // 
            // fraPropertyViewControls
            // 
            fraPropertyViewControls.Controls.Add(chkPropertyShowAllFields);
            fraPropertyViewControls.Controls.Add(chkPropertyShowAsHex);
            fraPropertyViewControls.Controls.Add(chkPropertyStyle);
            fraPropertyViewControls.Location = new Point(650, 95);
            fraPropertyViewControls.Margin = new Padding(4, 5, 4, 5);
            fraPropertyViewControls.Name = "fraPropertyViewControls";
            fraPropertyViewControls.Padding = new Padding(4, 5, 4, 5);
            fraPropertyViewControls.Size = new Size(211, 160);
            fraPropertyViewControls.TabIndex = 12;
            fraPropertyViewControls.TabStop = false;
            fraPropertyViewControls.Text = "Visuals";
            // 
            // chkPropertyShowAllFields
            // 
            chkPropertyShowAllFields.AutoSize = true;
            chkPropertyShowAllFields.Location = new Point(9, 35);
            chkPropertyShowAllFields.Margin = new Padding(4, 5, 4, 5);
            chkPropertyShowAllFields.Name = "chkPropertyShowAllFields";
            chkPropertyShowAllFields.Size = new Size(190, 29);
            chkPropertyShowAllFields.TabIndex = 9;
            chkPropertyShowAllFields.Text = "Show all properties";
            chkPropertyShowAllFields.UseVisualStyleBackColor = true;
            chkPropertyShowAllFields.CheckedChanged += chkPropertyShowAllFields_CheckedChanged;
            // 
            // chkPropertyShowAsHex
            // 
            chkPropertyShowAsHex.AutoSize = true;
            chkPropertyShowAsHex.Checked = true;
            chkPropertyShowAsHex.CheckState = CheckState.Checked;
            chkPropertyShowAsHex.Location = new Point(9, 118);
            chkPropertyShowAsHex.Margin = new Padding(4, 5, 4, 5);
            chkPropertyShowAsHex.Name = "chkPropertyShowAsHex";
            chkPropertyShowAsHex.Size = new Size(68, 29);
            chkPropertyShowAsHex.TabIndex = 11;
            chkPropertyShowAsHex.Text = "Hex";
            chkPropertyShowAsHex.UseVisualStyleBackColor = true;
            chkPropertyShowAsHex.Click += chkPropertyShowAsHex_Click;
            // 
            // chkPropertyStyle
            // 
            chkPropertyStyle.AutoSize = true;
            chkPropertyStyle.Location = new Point(9, 77);
            chkPropertyStyle.Margin = new Padding(4, 5, 4, 5);
            chkPropertyStyle.Name = "chkPropertyStyle";
            chkPropertyStyle.Size = new Size(133, 29);
            chkPropertyStyle.TabIndex = 5;
            chkPropertyStyle.Text = "Toggle View";
            chkPropertyStyle.UseVisualStyleBackColor = true;
            chkPropertyStyle.CheckedChanged += chkPropertyStyle_CheckedChanged;
            // 
            // lblFieldType
            // 
            lblFieldType.AutoSize = true;
            lblFieldType.Location = new Point(650, 60);
            lblFieldType.Margin = new Padding(4, 0, 4, 0);
            lblFieldType.Name = "lblFieldType";
            lblFieldType.Size = new Size(62, 25);
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
            lbPropertyRaw.Location = new Point(9, 127);
            lbPropertyRaw.Margin = new Padding(4, 5, 4, 5);
            lbPropertyRaw.Name = "lbPropertyRaw";
            lbPropertyRaw.Size = new Size(485, 77);
            lbPropertyRaw.TabIndex = 7;
            // 
            // lvPropertyHeader
            // 
            lvPropertyHeader.BorderStyle = BorderStyle.FixedSingle;
            lvPropertyHeader.FullRowSelect = true;
            lvPropertyHeader.Location = new Point(9, 37);
            lvPropertyHeader.Margin = new Padding(4, 5, 4, 5);
            lvPropertyHeader.Name = "lvPropertyHeader";
            lvPropertyHeader.Scrollable = false;
            lvPropertyHeader.Size = new Size(485, 79);
            lvPropertyHeader.TabIndex = 6;
            lvPropertyHeader.UseCompatibleStateImageBehavior = false;
            lvPropertyHeader.View = View.Details;
            // 
            // lblUnsupportedProperty
            // 
            lblUnsupportedProperty.AutoSize = true;
            lblUnsupportedProperty.ForeColor = Color.Red;
            lblUnsupportedProperty.Location = new Point(650, 5);
            lblUnsupportedProperty.Margin = new Padding(4, 0, 4, 0);
            lblUnsupportedProperty.Name = "lblUnsupportedProperty";
            lblUnsupportedProperty.Size = new Size(235, 50);
            lblUnsupportedProperty.TabIndex = 4;
            lblUnsupportedProperty.Text = "Unsupported property field!\r\n(unknown)";
            lblUnsupportedProperty.Visible = false;
            // 
            // txtProperty
            // 
            txtProperty.BackColor = Color.FromArgb(26, 26, 28);
            txtProperty.BorderStyle = BorderStyle.FixedSingle;
            txtProperty.ForeColor = Color.FromArgb(213, 213, 213);
            txtProperty.Location = new Point(9, 425);
            txtProperty.Margin = new Padding(4, 5, 4, 5);
            txtProperty.MaxLength = 4;
            txtProperty.Name = "txtProperty";
            txtProperty.Size = new Size(106, 31);
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
            cmdRemoveProperty.Location = new Point(9, 527);
            cmdRemoveProperty.Margin = new Padding(4, 5, 4, 5);
            cmdRemoveProperty.Name = "cmdRemoveProperty";
            cmdRemoveProperty.Padding = new Padding(7, 8, 7, 8);
            cmdRemoveProperty.Size = new Size(107, 43);
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
            cmdAppendProperty.Location = new Point(9, 473);
            cmdAppendProperty.Margin = new Padding(4, 5, 4, 5);
            cmdAppendProperty.Name = "cmdAppendProperty";
            cmdAppendProperty.Padding = new Padding(7, 8, 7, 8);
            cmdAppendProperty.Size = new Size(107, 43);
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
            dgvPropertyMetaValues.Location = new Point(9, 215);
            dgvPropertyMetaValues.Margin = new Padding(4, 5, 4, 5);
            dgvPropertyMetaValues.Name = "dgvPropertyMetaValues";
            dgvPropertyMetaValues.RowHeadersWidth = 24;
            dgvPropertyMetaValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPropertyMetaValues.ScrollBars = ScrollBars.Vertical;
            dgvPropertyMetaValues.ShowCellToolTips = false;
            dgvPropertyMetaValues.Size = new Size(137, 465);
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
            dgvPropertyValues.Location = new Point(154, 215);
            dgvPropertyValues.Margin = new Padding(4, 5, 4, 5);
            dgvPropertyValues.Name = "dgvPropertyValues";
            dgvPropertyValues.RowHeadersWidth = 24;
            dgvPropertyValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPropertyValues.ScrollBars = ScrollBars.Vertical;
            dgvPropertyValues.ShowCellToolTips = false;
            dgvPropertyValues.Size = new Size(340, 465);
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
            lbProperties.Location = new Point(9, 37);
            lbProperties.Margin = new Padding(4, 5, 4, 5);
            lbProperties.Name = "lbProperties";
            lbProperties.SelectionMode = SelectionMode.MultiExtended;
            lbProperties.Size = new Size(106, 377);
            lbProperties.Sorted = true;
            lbProperties.TabIndex = 0;
            lbProperties.SelectedIndexChanged += lbProperties_SelectedIndexChanged;
            lbProperties.KeyDown += lbProperties_KeyDown;
            // 
            // fraPropertyID
            // 
            fraPropertyID.BackColor = Color.Transparent;
            fraPropertyID.Controls.Add(lbProperties);
            fraPropertyID.Controls.Add(txtProperty);
            fraPropertyID.Controls.Add(cmdAppendProperty);
            fraPropertyID.Controls.Add(cmdRemoveProperty);
            fraPropertyID.Controls.Add(cmdCopyProperty);
            fraPropertyID.Location = new Point(4, 5);
            fraPropertyID.Margin = new Padding(4, 5, 4, 5);
            fraPropertyID.Name = "fraPropertyID";
            fraPropertyID.Padding = new Padding(4, 5, 4, 5);
            fraPropertyID.Size = new Size(124, 705);
            fraPropertyID.TabIndex = 15;
            fraPropertyID.TabStop = false;
            fraPropertyID.Text = "IDs";
            // 
            // fraPropertyField
            // 
            fraPropertyField.BackColor = Color.Transparent;
            fraPropertyField.Controls.Add(rbtReload2);
            fraPropertyField.Controls.Add(lvPropertyHeader);
            fraPropertyField.Controls.Add(dgvPropertyValues);
            fraPropertyField.Controls.Add(dgvPropertyMetaValues);
            fraPropertyField.Controls.Add(lbPropertyRaw);
            fraPropertyField.Location = new Point(137, 7);
            fraPropertyField.Margin = new Padding(4, 5, 4, 5);
            fraPropertyField.Name = "fraPropertyField";
            fraPropertyField.Padding = new Padding(4, 5, 4, 5);
            fraPropertyField.Size = new Size(504, 721);
            fraPropertyField.TabIndex = 16;
            fraPropertyField.TabStop = false;
            fraPropertyField.Text = "Properties";
            // 
            // rbtReload2
            // 
            rbtReload2.BackgroundColor = Color.FromArgb(30, 30, 30);
            rbtReload2.BorderColor = Color.FromArgb(155, 155, 155);
            rbtReload2.Checked = true;
            rbtReload2.CheckSignColor = Color.FromArgb(65, 177, 225);
            rbtReload2.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            rbtReload2.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rbtReload2.Font = new Font("Microsoft Sans Serif", 10F);
            rbtReload2.Group = 0;
            rbtReload2.IsDerivedStyle = true;
            rbtReload2.Location = new Point(468, 686);
            rbtReload2.Margin = new Padding(4, 5, 4, 5);
            rbtReload2.Name = "rbtReload2";
            rbtReload2.Size = new Size(27, 17);
            rbtReload2.Style = MetroSet_UI.Enums.Style.Dark;
            rbtReload2.StyleManager = null;
            rbtReload2.TabIndex = 22;
            rbtReload2.ThemeAuthor = "Narwin";
            rbtReload2.ThemeName = "MetroDark";
            rbtReload2.Click += rbtReload2_Click;
            // 
            // fraSaveProperties
            // 
            fraSaveProperties.Controls.Add(rbtReload);
            fraSaveProperties.Controls.Add(pnControlsSaved);
            fraSaveProperties.Controls.Add(lbSavedProperties);
            fraSaveProperties.Controls.Add(dgvSavePropertyValues);
            fraSaveProperties.Location = new Point(0, 768);
            fraSaveProperties.Margin = new Padding(4, 5, 4, 5);
            fraSaveProperties.Name = "fraSaveProperties";
            fraSaveProperties.Padding = new Padding(4, 5, 4, 5);
            fraSaveProperties.Size = new Size(857, 470);
            fraSaveProperties.TabIndex = 17;
            fraSaveProperties.TabStop = false;
            fraSaveProperties.Text = "Saved Properties";
            // 
            // rbtReload
            // 
            rbtReload.BackgroundColor = Color.FromArgb(30, 30, 30);
            rbtReload.BorderColor = Color.FromArgb(155, 155, 155);
            rbtReload.Checked = true;
            rbtReload.CheckSignColor = Color.FromArgb(65, 177, 225);
            rbtReload.CheckState = MetroSet_UI.Enums.CheckState.Checked;
            rbtReload.DisabledBorderColor = Color.FromArgb(85, 85, 85);
            rbtReload.Font = new Font("Microsoft Sans Serif", 10F);
            rbtReload.Group = 0;
            rbtReload.IsDerivedStyle = true;
            rbtReload.Location = new Point(823, 427);
            rbtReload.Margin = new Padding(4, 5, 4, 5);
            rbtReload.Name = "rbtReload";
            rbtReload.Size = new Size(27, 17);
            rbtReload.Style = MetroSet_UI.Enums.Style.Dark;
            rbtReload.StyleManager = null;
            rbtReload.TabIndex = 21;
            rbtReload.ThemeAuthor = "Narwin";
            rbtReload.ThemeName = "MetroDark";
            rbtReload.Click += rbtReload_Click;
            // 
            // pnControlsSaved
            // 
            pnControlsSaved.AutoSize = true;
            pnControlsSaved.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnControlsSaved.Controls.Add(cmdRenameSavedList);
            pnControlsSaved.Controls.Add(cmdCopyFromSaved);
            pnControlsSaved.Controls.Add(cmdRemoveSavedList);
            pnControlsSaved.Enabled = false;
            pnControlsSaved.Location = new Point(9, 350);
            pnControlsSaved.Margin = new Padding(4, 5, 4, 5);
            pnControlsSaved.Name = "pnControlsSaved";
            pnControlsSaved.Size = new Size(291, 106);
            pnControlsSaved.TabIndex = 20;
            // 
            // cmdRenameSavedList
            // 
            cmdRenameSavedList.BorderColour = Color.Empty;
            cmdRenameSavedList.CustomColour = false;
            cmdRenameSavedList.FlatBottom = false;
            cmdRenameSavedList.FlatTop = false;
            cmdRenameSavedList.Location = new Point(4, 5);
            cmdRenameSavedList.Margin = new Padding(4, 5, 4, 5);
            cmdRenameSavedList.Name = "cmdRenameSavedList";
            cmdRenameSavedList.Padding = new Padding(7, 8, 7, 8);
            cmdRenameSavedList.Size = new Size(107, 43);
            cmdRenameSavedList.TabIndex = 19;
            cmdRenameSavedList.Text = "Rename";
            cmdRenameSavedList.Click += cmdRenameSavedList_Click;
            // 
            // cmdCopyFromSaved
            // 
            cmdCopyFromSaved.BorderColour = Color.Empty;
            cmdCopyFromSaved.CustomColour = false;
            cmdCopyFromSaved.FlatBottom = false;
            cmdCopyFromSaved.FlatTop = false;
            cmdCopyFromSaved.Location = new Point(180, 5);
            cmdCopyFromSaved.Margin = new Padding(4, 5, 4, 5);
            cmdCopyFromSaved.Name = "cmdCopyFromSaved";
            cmdCopyFromSaved.Padding = new Padding(7, 8, 7, 8);
            cmdCopyFromSaved.Size = new Size(107, 43);
            cmdCopyFromSaved.TabIndex = 18;
            cmdCopyFromSaved.Text = "Apply";
            cmdCopyFromSaved.Click += cmdCopyFromSaved_Click;
            // 
            // cmdRemoveSavedList
            // 
            cmdRemoveSavedList.BorderColour = Color.Empty;
            cmdRemoveSavedList.CustomColour = false;
            cmdRemoveSavedList.FlatBottom = false;
            cmdRemoveSavedList.FlatTop = false;
            cmdRemoveSavedList.Location = new Point(4, 58);
            cmdRemoveSavedList.Margin = new Padding(4, 5, 4, 5);
            cmdRemoveSavedList.Name = "cmdRemoveSavedList";
            cmdRemoveSavedList.Padding = new Padding(7, 8, 7, 8);
            cmdRemoveSavedList.Size = new Size(107, 43);
            cmdRemoveSavedList.TabIndex = 19;
            cmdRemoveSavedList.Text = "Remove";
            cmdRemoveSavedList.Click += cmdRemoveSavedList_Click;
            // 
            // lbSavedProperties
            // 
            lbSavedProperties.BackColor = Color.FromArgb(26, 26, 28);
            lbSavedProperties.BorderStyle = BorderStyle.FixedSingle;
            lbSavedProperties.ForeColor = Color.FromArgb(213, 213, 213);
            lbSavedProperties.Location = new Point(9, 37);
            lbSavedProperties.Margin = new Padding(4, 5, 4, 5);
            lbSavedProperties.Name = "lbSavedProperties";
            lbSavedProperties.SelectionMode = SelectionMode.MultiExtended;
            lbSavedProperties.Size = new Size(171, 302);
            lbSavedProperties.TabIndex = 6;
            lbSavedProperties.SelectedIndexChanged += lbSavedProperties_SelectedIndexChanged;
            lbSavedProperties.KeyDown += lbSavedProperties_KeyDown;
            // 
            // dgvSavePropertyValues
            // 
            dgvSavePropertyValues.AllowUserToAddRows = false;
            dgvSavePropertyValues.AllowUserToResizeColumns = false;
            dgvSavePropertyValues.AllowUserToResizeRows = false;
            dgvSavePropertyValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSavePropertyValues.ColumnHeadersHeight = 24;
            dgvSavePropertyValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSavePropertyValues.Location = new Point(189, 37);
            dgvSavePropertyValues.Margin = new Padding(4, 5, 4, 5);
            dgvSavePropertyValues.Name = "dgvSavePropertyValues";
            dgvSavePropertyValues.RowHeadersWidth = 24;
            dgvSavePropertyValues.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvSavePropertyValues.ScrollBars = ScrollBars.Vertical;
            dgvSavePropertyValues.ShowCellToolTips = false;
            dgvSavePropertyValues.Size = new Size(657, 303);
            dgvSavePropertyValues.TabIndex = 1;
            dgvSavePropertyValues.CellBeginEdit += dgvSavePropertyValues_CellBeginEdit;
            dgvSavePropertyValues.CellValueChanged += dgvSavePropertyValues_CellValueChanged;
            // 
            // EntityPropertyBox
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(fraSaveProperties);
            Controls.Add(fraPropertyField);
            Controls.Add(fraPropertyID);
            Controls.Add(fraPropertyControls);
            Controls.Add(fraPropertyViewControls);
            Controls.Add(lblFieldType);
            Controls.Add(lblUnsupportedProperty);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EntityPropertyBox";
            Size = new Size(1309, 1387);
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
            fraSaveProperties.PerformLayout();
            pnControlsSaved.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSavePropertyValues).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private DarkListBox lbSavedProperties;
        private DataGridView dgvSavePropertyValues;
        private AltUI.Controls.DarkButton cmdCopyFromSaved;
        private AltUI.Controls.DarkButton cmdRenameSavedList;
        private DarkButton cmdRemoveSavedList;
        private Panel pnControlsSaved;
        private MetroSet_UI.Controls.MetroSetRadioButton rbtReload;
        private MetroSet_UI.Controls.MetroSetRadioButton rbtReload2;
    }
}
