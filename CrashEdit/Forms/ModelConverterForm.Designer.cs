using AltUI.Controls;
using System.Data.Common;

namespace CrashEdit.CE
{
    partial class ModelConverterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmdOpen = new DarkButton();
            cmdConvert = new DarkButton();
            fraSettings = new DarkGroupBox();
            fraObjectList = new DarkGroupBox();
            dgvBatch = new DataGridView();
            pnScaleDiffs = new Panel();
            chkAutoScale = new CheckBox();
            lblRatioX = new Label();
            lblRatioY = new Label();
            lblRatioZ = new Label();
            numScaleMod = new DarkNumericUpDown();
            lblScaleMod = new Label();
            cmdSetExportPath = new DarkButton();
            label2 = new Label();
            lblExportPath = new Label();
            fraScaleFactor = new DarkGroupBox();
            chkLinkScaleFactor = new CheckBox();
            lblScaleFX = new Label();
            numScaleFZ = new DarkNumericUpDown();
            numScaleFY = new DarkNumericUpDown();
            numScaleFX = new DarkNumericUpDown();
            lblScaleFY = new Label();
            lblScaleFZ = new Label();
            darkGroupBox3 = new DarkGroupBox();
            tableCfgStrip = new TableLayoutPanel();
            lblStripIterations = new Label();
            numMaxStripIterations = new DarkNumericUpDown();
            label4 = new Label();
            numStripCountWeight = new DarkNumericUpDown();
            lblMaxKeyWeight = new Label();
            label3 = new Label();
            numMaxLiveKeysWeight = new DarkNumericUpDown();
            numAvgKeysWeight = new DarkNumericUpDown();
            pnCompressModel = new Panel();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            chkSkipOddFrames = new CheckBox();
            chkCompressModel = new CheckBox();
            darkGroupBox1 = new DarkGroupBox();
            chkLinkModelScale = new CheckBox();
            lblScaleX = new Label();
            numScaleX = new DarkNumericUpDown();
            numScaleY = new DarkNumericUpDown();
            numScaleZ = new DarkNumericUpDown();
            lblScaleY = new Label();
            lblScaleZ = new Label();
            cmdSaveSettings = new DarkButton();
            lblPath = new Label();
            label1 = new Label();
            chkDebug = new CheckBox();
            lblVersion = new Label();
            chkTestCompression = new CheckBox();
            pnBottom = new Panel();
            panel1 = new Panel();
            fraSettings.SuspendLayout();
            fraObjectList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBatch).BeginInit();
            pnScaleDiffs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleMod).BeginInit();
            fraScaleFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleFZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFX).BeginInit();
            darkGroupBox3.SuspendLayout();
            tableCfgStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxStripIterations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStripCountWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxLiveKeysWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAvgKeysWeight).BeginInit();
            pnCompressModel.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).BeginInit();
            pnBottom.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdOpen
            // 
            cmdOpen.BorderColour = Color.Empty;
            cmdOpen.CustomColour = false;
            cmdOpen.FlatBottom = false;
            cmdOpen.FlatTop = false;
            cmdOpen.Location = new Point(12, 12);
            cmdOpen.Name = "cmdOpen";
            cmdOpen.Padding = new Padding(5);
            cmdOpen.Size = new Size(75, 40);
            cmdOpen.TabIndex = 0;
            cmdOpen.Text = "Open";
            cmdOpen.Click += cmdOpen_Click;
            // 
            // cmdConvert
            // 
            cmdConvert.BorderColour = Color.Empty;
            cmdConvert.CustomColour = false;
            cmdConvert.FlatBottom = false;
            cmdConvert.FlatTop = false;
            cmdConvert.Location = new Point(341, 37);
            cmdConvert.Name = "cmdConvert";
            cmdConvert.Padding = new Padding(5);
            cmdConvert.Size = new Size(351, 40);
            cmdConvert.TabIndex = 0;
            cmdConvert.Text = "Convert";
            cmdConvert.Click += cmdConvert_Click;
            // 
            // fraSettings
            // 
            fraSettings.BackColor = Color.Transparent;
            fraSettings.Controls.Add(panel1);
            fraSettings.Controls.Add(fraObjectList);
            fraSettings.Controls.Add(pnScaleDiffs);
            fraSettings.Controls.Add(fraScaleFactor);
            fraSettings.Controls.Add(darkGroupBox3);
            fraSettings.Controls.Add(darkGroupBox1);
            fraSettings.Enabled = false;
            fraSettings.Location = new Point(12, 61);
            fraSettings.Name = "fraSettings";
            fraSettings.Size = new Size(695, 435);
            fraSettings.TabIndex = 2;
            fraSettings.TabStop = false;
            // 
            // fraObjectList
            // 
            fraObjectList.Controls.Add(dgvBatch);
            fraObjectList.Location = new Point(6, 10);
            fraObjectList.Name = "fraObjectList";
            fraObjectList.Size = new Size(268, 326);
            fraObjectList.TabIndex = 13;
            fraObjectList.TabStop = false;
            fraObjectList.Text = "Object List";
            // 
            // dgvBatch
            // 
            dgvBatch.AllowUserToAddRows = false;
            dgvBatch.AllowUserToResizeColumns = false;
            dgvBatch.AllowUserToResizeRows = false;
            dgvBatch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvBatch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBatch.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvBatch.Location = new Point(6, 22);
            dgvBatch.Name = "dgvBatch";
            dgvBatch.ReadOnly = true;
            dgvBatch.RowHeadersVisible = false;
            dgvBatch.RowHeadersWidth = 20;
            dgvBatch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvBatch.ScrollBars = ScrollBars.Vertical;
            dgvBatch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBatch.ShowCellToolTips = false;
            dgvBatch.Size = new Size(256, 295);
            dgvBatch.TabIndex = 12;
            dgvBatch.CellBeginEdit += dgvBatch_CellBeginEdit;
            // 
            // pnScaleDiffs
            // 
            pnScaleDiffs.Controls.Add(chkAutoScale);
            pnScaleDiffs.Controls.Add(lblRatioX);
            pnScaleDiffs.Controls.Add(lblRatioY);
            pnScaleDiffs.Controls.Add(lblRatioZ);
            pnScaleDiffs.Controls.Add(numScaleMod);
            pnScaleDiffs.Controls.Add(lblScaleMod);
            pnScaleDiffs.Location = new Point(539, 16);
            pnScaleDiffs.Name = "pnScaleDiffs";
            pnScaleDiffs.Size = new Size(141, 126);
            pnScaleDiffs.TabIndex = 12;
            // 
            // chkAutoScale
            // 
            chkAutoScale.AutoSize = true;
            chkAutoScale.Checked = true;
            chkAutoScale.CheckState = CheckState.Checked;
            chkAutoScale.Location = new Point(3, 49);
            chkAutoScale.Name = "chkAutoScale";
            chkAutoScale.Size = new Size(79, 19);
            chkAutoScale.TabIndex = 5;
            chkAutoScale.Text = "AutoScale";
            chkAutoScale.UseVisualStyleBackColor = true;
            // 
            // lblRatioX
            // 
            lblRatioX.AutoSize = true;
            lblRatioX.BackColor = Color.Transparent;
            lblRatioX.Location = new Point(3, 1);
            lblRatioX.Name = "lblRatioX";
            lblRatioX.Size = new Size(130, 15);
            lblRatioX.TabIndex = 3;
            lblRatioX.Text = "Scale:1.0000  diff:0.0000";
            // 
            // lblRatioY
            // 
            lblRatioY.AutoSize = true;
            lblRatioY.BackColor = Color.Transparent;
            lblRatioY.Location = new Point(3, 16);
            lblRatioY.Name = "lblRatioY";
            lblRatioY.Size = new Size(130, 15);
            lblRatioY.TabIndex = 3;
            lblRatioY.Text = "Scale:1.0000  diff:0.0000";
            // 
            // lblRatioZ
            // 
            lblRatioZ.AutoSize = true;
            lblRatioZ.BackColor = Color.Transparent;
            lblRatioZ.Location = new Point(3, 31);
            lblRatioZ.Name = "lblRatioZ";
            lblRatioZ.Size = new Size(130, 15);
            lblRatioZ.TabIndex = 3;
            lblRatioZ.Text = "Scale:1.0000  diff:0.0000";
            // 
            // numScaleMod
            // 
            numScaleMod.DecimalPlaces = 2;
            numScaleMod.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numScaleMod.Location = new Point(71, 98);
            numScaleMod.Maximum = new decimal(new int[] { 1000, 0, 0, 131072 });
            numScaleMod.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numScaleMod.Name = "numScaleMod";
            numScaleMod.Size = new Size(64, 23);
            numScaleMod.TabIndex = 0;
            numScaleMod.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblScaleMod
            // 
            lblScaleMod.AutoSize = true;
            lblScaleMod.BackColor = Color.Transparent;
            lblScaleMod.Location = new Point(3, 102);
            lblScaleMod.Name = "lblScaleMod";
            lblScaleMod.Size = new Size(59, 15);
            lblScaleMod.TabIndex = 3;
            lblScaleMod.Text = "ScaleMod";
            // 
            // cmdSetExportPath
            // 
            cmdSetExportPath.BorderColour = Color.Empty;
            cmdSetExportPath.CustomColour = false;
            cmdSetExportPath.FlatBottom = false;
            cmdSetExportPath.FlatTop = false;
            cmdSetExportPath.Location = new Point(77, 9);
            cmdSetExportPath.Name = "cmdSetExportPath";
            cmdSetExportPath.Padding = new Padding(5);
            cmdSetExportPath.Size = new Size(24, 24);
            cmdSetExportPath.TabIndex = 8;
            cmdSetExportPath.Click += cmdSetExportPath_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 13);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Export Path:";
            // 
            // lblExportPath
            // 
            lblExportPath.BackColor = Color.Transparent;
            lblExportPath.Location = new Point(3, 35);
            lblExportPath.Name = "lblExportPath";
            lblExportPath.Size = new Size(400, 30);
            lblExportPath.TabIndex = 3;
            lblExportPath.Text = "PATH---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            // 
            // fraScaleFactor
            // 
            fraScaleFactor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraScaleFactor.Controls.Add(chkLinkScaleFactor);
            fraScaleFactor.Controls.Add(lblScaleFX);
            fraScaleFactor.Controls.Add(numScaleFZ);
            fraScaleFactor.Controls.Add(numScaleFY);
            fraScaleFactor.Controls.Add(numScaleFX);
            fraScaleFactor.Controls.Add(lblScaleFY);
            fraScaleFactor.Controls.Add(lblScaleFZ);
            fraScaleFactor.Location = new Point(280, 10);
            fraScaleFactor.Name = "fraScaleFactor";
            fraScaleFactor.Size = new Size(115, 132);
            fraScaleFactor.TabIndex = 6;
            fraScaleFactor.TabStop = false;
            fraScaleFactor.Text = "Scale Factors";
            // 
            // chkLinkScaleFactor
            // 
            chkLinkScaleFactor.AutoSize = true;
            chkLinkScaleFactor.Checked = true;
            chkLinkScaleFactor.CheckState = CheckState.Checked;
            chkLinkScaleFactor.Location = new Point(42, 107);
            chkLinkScaleFactor.Name = "chkLinkScaleFactor";
            chkLinkScaleFactor.Size = new Size(48, 19);
            chkLinkScaleFactor.TabIndex = 5;
            chkLinkScaleFactor.Text = "Link";
            chkLinkScaleFactor.UseVisualStyleBackColor = true;
            // 
            // lblScaleFX
            // 
            lblScaleFX.AutoSize = true;
            lblScaleFX.BackColor = Color.Transparent;
            lblScaleFX.Location = new Point(10, 26);
            lblScaleFX.Name = "lblScaleFX";
            lblScaleFX.Size = new Size(14, 15);
            lblScaleFX.TabIndex = 3;
            lblScaleFX.Text = "X";
            // 
            // numScaleFZ
            // 
            numScaleFZ.DecimalPlaces = 2;
            numScaleFZ.Location = new Point(42, 80);
            numScaleFZ.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFZ.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numScaleFZ.Name = "numScaleFZ";
            numScaleFZ.Size = new Size(64, 23);
            numScaleFZ.TabIndex = 0;
            numScaleFZ.Value = new decimal(new int[] { 127, 0, 0, 0 });
            numScaleFZ.ValueChanged += numScaleFactor_ValueChanged;
            // 
            // numScaleFY
            // 
            numScaleFY.DecimalPlaces = 2;
            numScaleFY.Location = new Point(42, 51);
            numScaleFY.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFY.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numScaleFY.Name = "numScaleFY";
            numScaleFY.Size = new Size(64, 23);
            numScaleFY.TabIndex = 0;
            numScaleFY.Value = new decimal(new int[] { 127, 0, 0, 0 });
            numScaleFY.ValueChanged += numScaleFactor_ValueChanged;
            // 
            // numScaleFX
            // 
            numScaleFX.DecimalPlaces = 2;
            numScaleFX.Location = new Point(42, 22);
            numScaleFX.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numScaleFX.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numScaleFX.Name = "numScaleFX";
            numScaleFX.Size = new Size(64, 23);
            numScaleFX.TabIndex = 0;
            numScaleFX.Value = new decimal(new int[] { 127, 0, 0, 0 });
            numScaleFX.ValueChanged += numScaleFactor_ValueChanged;
            // 
            // lblScaleFY
            // 
            lblScaleFY.AutoSize = true;
            lblScaleFY.BackColor = Color.Transparent;
            lblScaleFY.Location = new Point(10, 55);
            lblScaleFY.Name = "lblScaleFY";
            lblScaleFY.Size = new Size(14, 15);
            lblScaleFY.TabIndex = 3;
            lblScaleFY.Text = "Y";
            // 
            // lblScaleFZ
            // 
            lblScaleFZ.AutoSize = true;
            lblScaleFZ.BackColor = Color.Transparent;
            lblScaleFZ.Location = new Point(10, 84);
            lblScaleFZ.Name = "lblScaleFZ";
            lblScaleFZ.Size = new Size(14, 15);
            lblScaleFZ.TabIndex = 3;
            lblScaleFZ.Text = "Z";
            // 
            // darkGroupBox3
            // 
            darkGroupBox3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox3.Controls.Add(tableCfgStrip);
            darkGroupBox3.Controls.Add(pnCompressModel);
            darkGroupBox3.Controls.Add(chkSkipOddFrames);
            darkGroupBox3.Controls.Add(chkCompressModel);
            darkGroupBox3.Location = new Point(280, 148);
            darkGroupBox3.Name = "darkGroupBox3";
            darkGroupBox3.Size = new Size(400, 169);
            darkGroupBox3.TabIndex = 6;
            darkGroupBox3.TabStop = false;
            darkGroupBox3.Text = "Advanced";
            // 
            // tableCfgStrip
            // 
            tableCfgStrip.ColumnCount = 2;
            tableCfgStrip.ColumnStyles.Add(new ColumnStyle());
            tableCfgStrip.ColumnStyles.Add(new ColumnStyle());
            tableCfgStrip.Controls.Add(lblStripIterations, 0, 0);
            tableCfgStrip.Controls.Add(numMaxStripIterations, 1, 0);
            tableCfgStrip.Controls.Add(label4, 0, 3);
            tableCfgStrip.Controls.Add(numStripCountWeight, 1, 3);
            tableCfgStrip.Controls.Add(lblMaxKeyWeight, 0, 1);
            tableCfgStrip.Controls.Add(label3, 0, 2);
            tableCfgStrip.Controls.Add(numMaxLiveKeysWeight, 1, 1);
            tableCfgStrip.Controls.Add(numAvgKeysWeight, 1, 2);
            tableCfgStrip.Location = new Point(6, 22);
            tableCfgStrip.Name = "tableCfgStrip";
            tableCfgStrip.RowCount = 4;
            tableCfgStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableCfgStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableCfgStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableCfgStrip.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableCfgStrip.Size = new Size(236, 115);
            tableCfgStrip.TabIndex = 13;
            // 
            // lblStripIterations
            // 
            lblStripIterations.AutoSize = true;
            lblStripIterations.BackColor = Color.Transparent;
            lblStripIterations.Location = new Point(3, 3);
            lblStripIterations.Margin = new Padding(3, 3, 3, 0);
            lblStripIterations.Name = "lblStripIterations";
            lblStripIterations.Size = new Size(139, 15);
            lblStripIterations.TabIndex = 3;
            lblStripIterations.Text = "Max Strip Build Iterations";
            // 
            // numMaxStripIterations
            // 
            numMaxStripIterations.Location = new Point(148, 3);
            numMaxStripIterations.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numMaxStripIterations.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxStripIterations.Name = "numMaxStripIterations";
            numMaxStripIterations.Size = new Size(80, 23);
            numMaxStripIterations.TabIndex = 0;
            numMaxStripIterations.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(3, 87);
            label4.Margin = new Padding(3, 3, 3, 0);
            label4.Name = "label4";
            label4.Size = new Size(108, 15);
            label4.TabIndex = 3;
            label4.Text = "Strip Count Weight";
            // 
            // numStripCountWeight
            // 
            numStripCountWeight.Location = new Point(148, 87);
            numStripCountWeight.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numStripCountWeight.Name = "numStripCountWeight";
            numStripCountWeight.Size = new Size(80, 23);
            numStripCountWeight.TabIndex = 0;
            numStripCountWeight.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblMaxKeyWeight
            // 
            lblMaxKeyWeight.AutoSize = true;
            lblMaxKeyWeight.BackColor = Color.Transparent;
            lblMaxKeyWeight.Location = new Point(3, 31);
            lblMaxKeyWeight.Margin = new Padding(3, 3, 3, 0);
            lblMaxKeyWeight.Name = "lblMaxKeyWeight";
            lblMaxKeyWeight.Size = new Size(119, 15);
            lblMaxKeyWeight.TabIndex = 3;
            lblMaxKeyWeight.Text = "Max LiveKeys Weight";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(3, 59);
            label3.Margin = new Padding(3, 3, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(118, 15);
            label3.TabIndex = 3;
            label3.Text = "Average Keys Weight";
            // 
            // numMaxLiveKeysWeight
            // 
            numMaxLiveKeysWeight.Location = new Point(148, 31);
            numMaxLiveKeysWeight.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numMaxLiveKeysWeight.Name = "numMaxLiveKeysWeight";
            numMaxLiveKeysWeight.Size = new Size(80, 23);
            numMaxLiveKeysWeight.TabIndex = 0;
            numMaxLiveKeysWeight.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numAvgKeysWeight
            // 
            numAvgKeysWeight.Location = new Point(148, 59);
            numAvgKeysWeight.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numAvgKeysWeight.Name = "numAvgKeysWeight";
            numAvgKeysWeight.Size = new Size(80, 23);
            numAvgKeysWeight.TabIndex = 0;
            numAvgKeysWeight.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // pnCompressModel
            // 
            pnCompressModel.Controls.Add(radioButton3);
            pnCompressModel.Controls.Add(radioButton2);
            pnCompressModel.Controls.Add(radioButton1);
            pnCompressModel.Enabled = false;
            pnCompressModel.Location = new Point(290, 36);
            pnCompressModel.Name = "pnCompressModel";
            pnCompressModel.Size = new Size(104, 68);
            pnCompressModel.TabIndex = 12;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(3, 43);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(66, 19);
            radioButton3.TabIndex = 11;
            radioButton3.Tag = "2";
            radioButton3.Text = "All-zero";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(3, 23);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(68, 19);
            radioButton2.TabIndex = 11;
            radioButton2.Tag = "1";
            radioButton2.Text = "Average";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(65, 19);
            radioButton1.TabIndex = 11;
            radioButton1.TabStop = true;
            radioButton1.Tag = "0";
            radioButton1.Text = "Median";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            // 
            // chkSkipOddFrames
            // 
            chkSkipOddFrames.AutoSize = true;
            chkSkipOddFrames.Location = new Point(6, 143);
            chkSkipOddFrames.Name = "chkSkipOddFrames";
            chkSkipOddFrames.Size = new Size(167, 19);
            chkSkipOddFrames.TabIndex = 10;
            chkSkipOddFrames.Text = "Skip output on odd frames";
            chkSkipOddFrames.UseVisualStyleBackColor = true;
            // 
            // chkCompressModel
            // 
            chkCompressModel.AutoSize = true;
            chkCompressModel.Location = new Point(278, 15);
            chkCompressModel.Name = "chkCompressModel";
            chkCompressModel.Size = new Size(116, 19);
            chkCompressModel.TabIndex = 10;
            chkCompressModel.Text = "Compress model";
            chkCompressModel.UseVisualStyleBackColor = true;
            chkCompressModel.CheckedChanged += chkCompressModel_CheckedChanged;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox1.Controls.Add(chkLinkModelScale);
            darkGroupBox1.Controls.Add(lblScaleX);
            darkGroupBox1.Controls.Add(numScaleX);
            darkGroupBox1.Controls.Add(numScaleY);
            darkGroupBox1.Controls.Add(numScaleZ);
            darkGroupBox1.Controls.Add(lblScaleY);
            darkGroupBox1.Controls.Add(lblScaleZ);
            darkGroupBox1.Location = new Point(401, 10);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(132, 132);
            darkGroupBox1.TabIndex = 6;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Model Scale";
            // 
            // chkLinkModelScale
            // 
            chkLinkModelScale.AutoSize = true;
            chkLinkModelScale.Checked = true;
            chkLinkModelScale.CheckState = CheckState.Checked;
            chkLinkModelScale.Location = new Point(62, 107);
            chkLinkModelScale.Name = "chkLinkModelScale";
            chkLinkModelScale.Size = new Size(48, 19);
            chkLinkModelScale.TabIndex = 5;
            chkLinkModelScale.Text = "Link";
            chkLinkModelScale.UseVisualStyleBackColor = true;
            // 
            // lblScaleX
            // 
            lblScaleX.AutoSize = true;
            lblScaleX.BackColor = Color.Transparent;
            lblScaleX.Location = new Point(12, 26);
            lblScaleX.Name = "lblScaleX";
            lblScaleX.Size = new Size(44, 15);
            lblScaleX.TabIndex = 3;
            lblScaleX.Text = "Scale X";
            // 
            // numScaleX
            // 
            numScaleX.Hexadecimal = true;
            numScaleX.Location = new Point(62, 22);
            numScaleX.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleX.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleX.Name = "numScaleX";
            numScaleX.Size = new Size(64, 23);
            numScaleX.TabIndex = 0;
            numScaleX.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            numScaleX.ValueChanged += numModelScale_ValueChanged;
            // 
            // numScaleY
            // 
            numScaleY.Hexadecimal = true;
            numScaleY.Location = new Point(62, 51);
            numScaleY.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleY.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleY.Name = "numScaleY";
            numScaleY.Size = new Size(64, 23);
            numScaleY.TabIndex = 0;
            numScaleY.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            numScaleY.ValueChanged += numModelScale_ValueChanged;
            // 
            // numScaleZ
            // 
            numScaleZ.Hexadecimal = true;
            numScaleZ.Location = new Point(62, 80);
            numScaleZ.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numScaleZ.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numScaleZ.Name = "numScaleZ";
            numScaleZ.Size = new Size(64, 23);
            numScaleZ.TabIndex = 0;
            numScaleZ.Value = new decimal(new int[] { 1606, 0, 0, 0 });
            numScaleZ.ValueChanged += numModelScale_ValueChanged;
            // 
            // lblScaleY
            // 
            lblScaleY.AutoSize = true;
            lblScaleY.BackColor = Color.Transparent;
            lblScaleY.Location = new Point(12, 55);
            lblScaleY.Name = "lblScaleY";
            lblScaleY.Size = new Size(44, 15);
            lblScaleY.TabIndex = 3;
            lblScaleY.Text = "Scale Y";
            // 
            // lblScaleZ
            // 
            lblScaleZ.AutoSize = true;
            lblScaleZ.BackColor = Color.Transparent;
            lblScaleZ.Location = new Point(12, 84);
            lblScaleZ.Name = "lblScaleZ";
            lblScaleZ.Size = new Size(44, 15);
            lblScaleZ.TabIndex = 3;
            lblScaleZ.Text = "Scale Z";
            // 
            // cmdSaveSettings
            // 
            cmdSaveSettings.BorderColour = Color.Empty;
            cmdSaveSettings.CustomColour = false;
            cmdSaveSettings.FlatBottom = false;
            cmdSaveSettings.FlatTop = false;
            cmdSaveSettings.Location = new Point(596, 3);
            cmdSaveSettings.Name = "cmdSaveSettings";
            cmdSaveSettings.Padding = new Padding(5);
            cmdSaveSettings.Size = new Size(96, 28);
            cmdSaveSettings.TabIndex = 7;
            cmdSaveSettings.Text = "Save Settings";
            cmdSaveSettings.Click += cmdSaveSettings_Click;
            // 
            // lblPath
            // 
            lblPath.BackColor = Color.Transparent;
            lblPath.Location = new Point(93, 25);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(326, 30);
            lblPath.TabIndex = 3;
            lblPath.Text = "PATH---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(93, 8);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "File Path:";
            // 
            // chkDebug
            // 
            chkDebug.AutoSize = true;
            chkDebug.Location = new Point(215, 37);
            chkDebug.Name = "chkDebug";
            chkDebug.Size = new Size(107, 19);
            chkDebug.TabIndex = 5;
            chkDebug.Text = "Log debug info";
            chkDebug.UseVisualStyleBackColor = true;
            chkDebug.CheckedChanged += chkDebug_CheckedChanged;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Location = new Point(3, 47);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(86, 30);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "Exporter: v1.0\r\nConverter: v1.0";
            // 
            // chkTestCompression
            // 
            chkTestCompression.AutoSize = true;
            chkTestCompression.Location = new Point(215, 58);
            chkTestCompression.Name = "chkTestCompression";
            chkTestCompression.Size = new Size(117, 19);
            chkTestCompression.TabIndex = 5;
            chkTestCompression.Text = "Test compression";
            chkTestCompression.UseVisualStyleBackColor = true;
            chkTestCompression.CheckedChanged += chkTestCompression_CheckedChanged;
            // 
            // pnBottom
            // 
            pnBottom.BackColor = Color.Transparent;
            pnBottom.Controls.Add(cmdConvert);
            pnBottom.Controls.Add(lblVersion);
            pnBottom.Controls.Add(chkDebug);
            pnBottom.Controls.Add(cmdSaveSettings);
            pnBottom.Controls.Add(chkTestCompression);
            pnBottom.Enabled = false;
            pnBottom.Location = new Point(12, 502);
            pnBottom.Name = "pnBottom";
            pnBottom.Size = new Size(695, 80);
            pnBottom.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblExportPath);
            panel1.Controls.Add(cmdSetExportPath);
            panel1.Location = new Point(3, 342);
            panel1.Name = "panel1";
            panel1.Size = new Size(414, 78);
            panel1.TabIndex = 13;
            // 
            // ModelConverterForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(719, 595);
            Controls.Add(pnBottom);
            Controls.Add(label1);
            Controls.Add(lblPath);
            Controls.Add(fraSettings);
            Controls.Add(cmdOpen);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ModelConverterForm";
            Text = "Model Converter";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            DragDrop += ModelConverterForm_DragDrop;
            DragEnter += ModelConverterForm_DragEnter;
            fraSettings.ResumeLayout(false);
            fraObjectList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBatch).EndInit();
            pnScaleDiffs.ResumeLayout(false);
            pnScaleDiffs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleMod).EndInit();
            fraScaleFactor.ResumeLayout(false);
            fraScaleFactor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleFZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleFX).EndInit();
            darkGroupBox3.ResumeLayout(false);
            darkGroupBox3.PerformLayout();
            tableCfgStrip.ResumeLayout(false);
            tableCfgStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxStripIterations).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStripCountWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxLiveKeysWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAvgKeysWeight).EndInit();
            pnCompressModel.ResumeLayout(false);
            pnCompressModel.PerformLayout();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScaleX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numScaleZ).EndInit();
            pnBottom.ResumeLayout(false);
            pnBottom.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkButton cmdOpen;
        private DarkButton cmdConvert;
        private DarkGroupBox fraSettings;
        private Label lblPath;
        private Label lblScaleZ;
        private Label lblScaleX;
        private DarkNumericUpDown numScaleX;
        private CheckBox chkLinkModelScale;
        private Label lblScaleY;
        private DarkNumericUpDown numScaleZ;
        private DarkNumericUpDown numScaleY;
        private DarkGroupBox darkGroupBox1;
        private DarkButton cmdSaveSettings;
        private Label label1;
        private Label label2;
        private Label lblExportPath;
        private DarkButton cmdSetExportPath;
        private CheckBox chkDebug;
        private DarkGroupBox fraScaleFactor;
        private Label lblScaleFX;
        private DarkNumericUpDown numScaleFX;
        private Label lblScaleFY;
        private Label lblScaleFZ;
        private DarkNumericUpDown numScaleFZ;
        private DarkNumericUpDown numScaleFY;
        private CheckBox chkSkipOddFrames;
        private Label lblVersion;
        private DarkGroupBox darkGroupBox3;
        private Label lblMaxKeyWeight;
        private DarkNumericUpDown numMaxLiveKeysWeight;
        private DarkNumericUpDown darkNumericUpDown2;
        private DarkNumericUpDown darkNumericUpDown3;
        private Label label4;
        private CheckBox chkLinkScaleFactor;
        private Label lblStripIterations;
        private DarkNumericUpDown numMaxStripIterations;
        private Panel pnCompressModel;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private CheckBox chkCompressModel;
        private CheckBox chkTestCompression;
        private DarkTextBox txtBaseModelEID;
        private DataGridView dgvBatch;
        private DarkNumericUpDown numAvgKeysWeight;
        private DarkNumericUpDown numStripCountWeight;
        private Label label3;
        private Label lblRatioX;
        private Label lblRatioZ;
        private Label lblRatioY;
        private CheckBox chkAutoScale;
        private Panel pnScaleDiffs;
        private DarkNumericUpDown numScaleMod;
        private Label lblScaleMod;
        private Panel pnBottom;
        private TableLayoutPanel tableCfgStrip;
        private DarkGroupBox fraObjectList;
        private Panel panel1;
    }
}