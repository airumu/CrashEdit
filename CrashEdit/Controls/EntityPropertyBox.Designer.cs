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
            fraPropertyControls.SuspendLayout();
            fraPropertyViewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyMetaValues).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyValues).BeginInit();
            SuspendLayout();
            // 
            // cmdPasteProperty
            // 
            cmdPasteProperty.BorderColour = Color.Empty;
            cmdPasteProperty.CustomColour = false;
            cmdPasteProperty.FlatBottom = false;
            cmdPasteProperty.FlatTop = false;
            cmdPasteProperty.Location = new Point(7, 369);
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
            cmdCopyProperty.Location = new Point(7, 340);
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
            fraPropertyControls.Location = new Point(437, 162);
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
            fraPropertyViewControls.Location = new Point(437, 60);
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
            lblFieldType.Location = new Point(437, 39);
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
            lbPropertyRaw.Location = new Point(91, 60);
            lbPropertyRaw.Name = "lbPropertyRaw";
            lbPropertyRaw.Size = new Size(340, 47);
            lbPropertyRaw.TabIndex = 7;
            // 
            // lvPropertyHeader
            // 
            lvPropertyHeader.BorderStyle = BorderStyle.FixedSingle;
            lvPropertyHeader.FullRowSelect = true;
            lvPropertyHeader.Location = new Point(91, 6);
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
            lblUnsupportedProperty.Location = new Point(437, 6);
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
            txtProperty.Location = new Point(7, 239);
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
            cmdRemoveProperty.Location = new Point(7, 297);
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
            cmdAppendProperty.Location = new Point(7, 268);
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
            dgvPropertyMetaValues.Location = new Point(91, 113);
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
            dgvPropertyValues.Location = new Point(193, 113);
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
            lbProperties.Location = new Point(7, 6);
            lbProperties.Name = "lbProperties";
            lbProperties.SelectionMode = SelectionMode.MultiExtended;
            lbProperties.Size = new Size(75, 227);
            lbProperties.Sorted = true;
            lbProperties.TabIndex = 0;
            lbProperties.SelectedIndexChanged += lbProperties_SelectedIndexChanged;
            lbProperties.KeyDown += lbProperties_KeyDown;
            // 
            // EntityPropertyBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(cmdPasteProperty);
            Controls.Add(cmdCopyProperty);
            Controls.Add(fraPropertyControls);
            Controls.Add(fraPropertyViewControls);
            Controls.Add(lblFieldType);
            Controls.Add(lbPropertyRaw);
            Controls.Add(lvPropertyHeader);
            Controls.Add(lblUnsupportedProperty);
            Controls.Add(txtProperty);
            Controls.Add(cmdRemoveProperty);
            Controls.Add(cmdAppendProperty);
            Controls.Add(dgvPropertyMetaValues);
            Controls.Add(dgvPropertyValues);
            Controls.Add(lbProperties);
            Name = "EntityPropertyBox";
            Size = new Size(635, 668);
            fraPropertyControls.ResumeLayout(false);
            fraPropertyControls.PerformLayout();
            fraPropertyViewControls.ResumeLayout(false);
            fraPropertyViewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyMetaValues).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPropertyValues).EndInit();
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
    }
}
