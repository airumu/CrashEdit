using AltUI.Controls;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;

namespace CrashEdit.CE.Forms
{
    partial class RebuildForm
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

        const int BASE_HEIGHT = 425;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponentRebuild()
        {
            labelPathExe = new Label();
            btnPathExe = new DarkButton();
            labelPathExeValue = new Label();
            labelPathCfg = new Label();
            labelPathCfgInfo = new Label();
            btnMakeNewConfig = new DarkButton();
            btnEditConfig = new DarkButton();
            btnClearConfig = new DarkButton();
            btnRecheckConfig = new DarkButton();
            btnPathCfg = new DarkButton();
            labelPathCfgValue = new Label();
            tooltip = new ToolTip();
            warningLabel = new Label();
            warningLabel2 = new Label();
            btnRebuild = new DarkButton();
            btnCancel = new DarkButton();
            labelLog = new Label();
            outputLog = new DarkRichTextBox();
            pnOptions = new Panel();
            txtSearch = new TextBox();
            labelSearchCount = new Label();
            comboSpawns = new DarkComboBox();
            pnOptions.SuspendLayout();
            SuspendLayout();

            labelPathExe.AutoSize = true;
            labelPathExe.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPathExe.BackColor = Color.Transparent;
            labelPathExe.Location = new Point(8, 16);
            labelPathExe.Name = "labelPathExe";
            labelPathExe.Size = new Size(46, 15);
            labelPathExe.Text = "Path to c2export exe:";

            btnPathExe.BorderColour = Color.Empty;
            btnPathExe.CustomColour = false;
            btnPathExe.FlatBottom = false;
            btnPathExe.FlatTop = false;
            btnPathExe.Location = new Point(590, 12);
            btnPathExe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPathExe.Name = "btnPathExe";
            btnPathExe.Padding = new Padding(0);
            btnPathExe.Size = new Size(100, 25);
            btnPathExe.TabIndex = 1;
            btnPathExe.Image = Embeds.Bitmaps["Find"];
            btnPathExe.Click += btnPathExe_Click;
            btnPathExe.MouseHover += (s, e) =>
            {
                tooltip.Show("Select c2export.exe", btnPathExe, btnPathExe.Width + 5, btnPathExe.Height / 2);
            };
            btnPathExe.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnPathExe);
            };

            labelPathExeValue.BackColor = Color.Transparent;
            labelPathExeValue.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPathExeValue.ForeColor = SystemColors.MenuText;
            labelPathExeValue.Location = new Point(100, 38);
            labelPathExeValue.Name = "labelPathExeValue";
            labelPathExeValue.Size = new Size(1000, 27);
            labelPathExeValue.Text = "exe path";
            labelPathExeValue.MouseHover += (e, a) =>
            {
                tooltip.Show(labelPathExeValue.Text, labelPathExeValue, labelPathExeValue.Width / 6, labelPathExeValue.Height);
            };
            labelPathExeValue.MouseLeave += (e, a) =>
            {
                tooltip.Hide(labelPathExeValue);
            };

            // ------------------------------------------------------

            labelPathCfg.AutoSize = true;
            labelPathCfg.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPathCfg.BackColor = Color.Transparent;
            labelPathCfg.Location = new Point(8, 72);
            labelPathCfg.Name = "labelPathCfg";
            labelPathCfg.Size = new Size(48, 15);
            labelPathCfg.Text = "Path to arguments:";

            labelPathCfgInfo.AutoSize = false;
            labelPathCfgInfo.BackColor = Color.Transparent;
            labelPathCfgInfo.Text = "🛈"; // Unicode info symbol, or use "i"
            labelPathCfgInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPathCfgInfo.ForeColor = Color.DodgerBlue;
            labelPathCfgInfo.TextAlign = ContentAlignment.MiddleCenter;
            labelPathCfgInfo.Cursor = Cursors.Hand;
            labelPathCfgInfo.Size = new Size(18, 18);
            labelPathCfgInfo.Location = new Point(120, labelPathCfg.Top - 2);
            labelPathCfgInfo.MouseHover += (s, e) =>
            {
                tooltip.Show("Autodetect looks in current NSF's folder for .txt files whose names contain 'rebuild', 'rebuilt' or 'args' - e.g. 'tree rebuild args.txt'", labelPathCfgInfo, labelPathCfgInfo.Width + 5, labelPathCfgInfo.Height / 2);
            };
            labelPathCfgInfo.MouseLeave += (s, e) =>
            {
                tooltip.Hide(labelPathCfgInfo);
            };

            btnPathCfg.BorderColour = Color.Empty;
            btnPathCfg.CustomColour = false;
            btnPathCfg.FlatBottom = false;
            btnPathCfg.FlatTop = false;
            btnPathCfg.Location = new Point(590, 68);
            btnPathCfg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPathCfg.Name = "btnPathCfg";
            btnPathCfg.Padding = new Padding(0);
            btnPathCfg.Size = new Size(100, 25);
            btnPathCfg.TabIndex = 2;
            btnPathCfg.Image = Embeds.Bitmaps["Find"];
            btnPathCfg.Click += btnPathCfg_Click;
            btnPathCfg.MouseHover += (s, e) =>
            {
                tooltip.Show("Select config file", btnPathCfg, btnPathCfg.Width + 5, btnPathCfg.Height / 2);
            };
            btnPathCfg.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnPathCfg);
            };

            btnMakeNewConfig.Size = new Size(20, 20);
            btnMakeNewConfig.Location = new Point(12, 90);
            btnMakeNewConfig.Name = "btnMakeNewConfig";
            btnMakeNewConfig.Image = new Bitmap(Embeds.Bitmaps["Add"], new Size(16, 16));
            btnMakeNewConfig.TabIndex = 6;
            btnMakeNewConfig.Click += btnMakeNewConfig_Click;
            btnMakeNewConfig.MouseHover += (s, e) =>
            {
                tooltip.Show("Create new rebuild config file", btnMakeNewConfig, btnMakeNewConfig.Width + 5, btnMakeNewConfig.Height / 2);
            };
            btnMakeNewConfig.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnMakeNewConfig);
            };

            btnEditConfig.Size = new Size(20, 20);
            btnEditConfig.Location = new Point(34, 90);
            btnEditConfig.Name = "btnEditConfig";
            btnEditConfig.Image = new Bitmap(Embeds.Bitmaps["Modify"], new Size(16, 16));
            btnEditConfig.TabIndex = 7;
            btnEditConfig.Click += btnEditConfig_Click;
            btnEditConfig.MouseHover += (s, e) =>
            {
                tooltip.Show("Edit config file", btnEditConfig, btnEditConfig.Width + 5, btnEditConfig.Height / 2);
            };
            btnEditConfig.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnEditConfig);
            };

            btnClearConfig.Size = new Size(20, 20);
            btnClearConfig.Location = new Point(56, 90);
            btnClearConfig.Name = "btnClearConfig";
            btnClearConfig.Image = new Bitmap(Embeds.Bitmaps["Erase"], new Size(16, 16));
            btnClearConfig.TabIndex = 8;
            btnClearConfig.Click += (s, e) =>
            {
                configFilePath = "<no config path selected>";
                labelPathCfgValue.Text = configFilePath;
                labelPathCfgValue.ForeColor = Color.Yellow;
                CheckArgsValid();
                comboSpawns.SelectedIndex = -1;
            };
            btnClearConfig.MouseHover += (s, e) =>
            {
                tooltip.Show("Clear config path", btnClearConfig, btnClearConfig.Width + 5, btnClearConfig.Height / 2);
            };
            btnClearConfig.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnClearConfig);
            };

            btnRecheckConfig.Size = new Size(20, 20);
            btnRecheckConfig.Location = new Point(78, 90);
            btnRecheckConfig.Name = "btnClearConfig";
            btnRecheckConfig.Image = new Bitmap(Embeds.Bitmaps["ArrowRefresh"], new Size(16, 16));
            btnRecheckConfig.TabIndex = 9;
            btnRecheckConfig.Click += (s, e) =>
            {
                SearchForConfigFile();
                CheckArgsValid();
                comboSpawns.SelectedIndex = -1;
            };
            btnRecheckConfig.MouseHover += (s, e) =>
            {
                tooltip.Show("Re-detect config path for current NSF", btnRecheckConfig, btnRecheckConfig.Width + 5, btnRecheckConfig.Height / 2);
            };
            btnRecheckConfig.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnRecheckConfig);
            };

            labelPathCfgValue.BackColor = Color.Transparent;
            labelPathCfgValue.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPathCfgValue.ForeColor = SystemColors.MenuText;
            labelPathCfgValue.Location = new Point(100, 94);
            labelPathCfgValue.Name = "labelPathCfgValue";
            labelPathCfgValue.Size = new Size(1000, 27);
            labelPathCfgValue.Text = "config path";
            labelPathCfgValue.MouseHover += (s, e) =>
            {
                tooltip.Show(labelPathCfgValue.Text, labelPathCfgValue, labelPathCfgValue.Width / 6, labelPathCfgValue.Height);
            };
            labelPathCfgValue.MouseLeave += (s, e) =>
            {
                tooltip.Hide(labelPathCfgValue);
            };

            // ----------------------------------------------------

            warningLabel.BackColor = Color.Transparent;
            warningLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            warningLabel.ForeColor = Color.Orange;
            warningLabel.Location = new Point(35, BASE_HEIGHT - 230);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(200, 30);
            warningLabel.Text = "";
            warningLabel.Visible = false;

            warningLabel2.BackColor = Color.Transparent;
            warningLabel2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            warningLabel2.ForeColor = Color.Red;
            warningLabel2.Location = new Point(35, BASE_HEIGHT - 210);
            warningLabel2.Name = "warningLabel2";
            warningLabel2.Size = new Size(380, 25);
            warningLabel2.Text = "";
            warningLabel2.Visible = false;

            btnRebuild.BorderColour = Color.Empty;
            btnRebuild.CustomColour = false;
            btnRebuild.Enabled = false;
            btnRebuild.FlatBottom = false;
            btnRebuild.FlatTop = false;
            btnRebuild.Location = new Point(590, BASE_HEIGHT - 220);
            btnRebuild.Name = "btnRebuild";
            btnRebuild.Padding = new Padding(5);
            btnRebuild.Size = new Size(100, 30);
            btnRebuild.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRebuild.TabIndex = 5;
            btnRebuild.Text = "Rebuild";
            btnRebuild.Click += btnRebuild_Click;
            btnRebuild.MouseHover += (e, a) =>
            {
                tooltip.Show("Start rebuild", btnRebuild, btnRebuild.Width + 5, btnRebuild.Height);
            };
            btnRebuild.MouseLeave += (e, a) =>
            {
                tooltip.Hide(btnRebuild);
            };

            btnCancel.BorderColour = Color.Empty;
            btnCancel.CustomColour = false;
            btnCancel.Enabled = false;
            btnCancel.FlatBottom = false;
            btnCancel.FlatTop = false;
            btnCancel.Location = new Point(480, BASE_HEIGHT - 220);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(5);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            btnCancel.MouseHover += (e, a) =>
            {
                tooltip.Show("Cancel rebuild", btnCancel, btnCancel.Width + 5, btnCancel.Height);
            };
            btnCancel.MouseLeave += (e, a) =>
            {
                tooltip.Hide(btnCancel);
            };

            outputLog.Multiline = true;
            outputLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            outputLog.Font = new Font("Consolas", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outputLog.Location = new Point(8, btnRebuild.Bottom + 12);
            outputLog.Size = new Size(700 - 16, this.Height - 150);
            outputLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outputLog.Name = "outputLog";
            outputLog.Text = "";
            outputLog.ReadOnly = true;

            labelLog.AutoSize = true;
            labelLog.BackColor = Color.Transparent;
            labelLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLog.Location = new Point(8, outputLog.Top - 12);
            labelLog.Name = "labelLog";
            labelLog.Size = new Size(80, 20);
            labelLog.Text = "Log:";

            labelWorkingDir = new Label();
            labelWorkingDir.AutoSize = true;
            labelWorkingDir.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelWorkingDir.BackColor = Color.Transparent;
            labelWorkingDir.Location = new Point(8, 128);
            labelWorkingDir.Name = "labelWorkingDir";
            labelWorkingDir.Size = new Size(120, 15);
            labelWorkingDir.Text = "Working directory:";

            labelWorkingDirInfo = new Label();
            labelWorkingDirInfo.AutoSize = false;
            labelWorkingDirInfo.BackColor = Color.Transparent;
            labelWorkingDirInfo.Text = "🛈"; // Unicode info symbol, or use "i"
            labelWorkingDirInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelWorkingDirInfo.ForeColor = Color.DodgerBlue;
            labelWorkingDirInfo.TextAlign = ContentAlignment.MiddleCenter;
            labelWorkingDirInfo.Cursor = Cursors.Hand;
            labelWorkingDirInfo.Size = new Size(18, 18);
            labelWorkingDirInfo.Location = new Point(120, labelWorkingDir.Top - 2);
            labelWorkingDirInfo.MouseHover += (s, e) =>
            {
                tooltip.Show("Select WD to be used by c2export (uses exe directory if none is set)", labelWorkingDirInfo, labelWorkingDirInfo.Width + 5, labelWorkingDirInfo.Height / 2);
            };
            labelWorkingDirInfo.MouseLeave += (s, e) =>
            {
                tooltip.Hide(labelWorkingDirInfo);
            };
            btnWorkingDir = new DarkButton();
            btnWorkingDir.BorderColour = Color.Empty;
            btnWorkingDir.CustomColour = false;
            btnWorkingDir.FlatBottom = false;
            btnWorkingDir.FlatTop = false;
            btnWorkingDir.Location = new Point(590, 124);
            btnWorkingDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnWorkingDir.Name = "btnWorkingDir";
            btnWorkingDir.Padding = new Padding(0);
            btnWorkingDir.Size = new Size(100, 25);
            btnWorkingDir.TabIndex = 3;
            btnWorkingDir.Image = Embeds.Bitmaps["Find"];
            btnWorkingDir.Click += btnWorkingDir_Click;
            btnWorkingDir.MouseHover += (s, e) =>
            {
                tooltip.Show("Select working directory", btnWorkingDir, btnWorkingDir.Width + 5, btnWorkingDir.Height / 2);
            };
            btnWorkingDir.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnWorkingDir);
            };

            spawnLabel = new Label();
            spawnLabel.AutoSize = true;
            spawnLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            spawnLabel.BackColor = Color.Transparent;
            spawnLabel.Location = new Point(460, 174); 
            spawnLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spawnLabel.Name = "spawnLabel";
            spawnLabel.Size = new Size(100, 15);
            spawnLabel.Text = "Spawn override";

            comboSpawns = new DarkComboBox();
            comboSpawnsOnOpen();
            comboSpawns.SelectedIndex = -1;
            comboSpawns.Name = "comboSpawns";
            comboSpawns.Padding = new Padding(0);
            comboSpawns.TabIndex = 11;
            comboSpawns.Size = new Size(100, 25);
            comboSpawns.Location = new Point(590, 170);
            comboSpawns.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboSpawns.Font = new Font("Consolas", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboSpawns.Click += (s, e) => comboSpawnsOnOpen();
            comboSpawns.SelectedIndexChanged += (s, e) => ComboSpawns_SelectedIndexChanged();

            spawnLabel2 = new Label();
            spawnLabel2.AutoSize = true;
            spawnLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            spawnLabel2.BackColor = Color.Transparent;
            spawnLabel2.Location = new Point(560, 174); 
            spawnLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            spawnLabel2.Name = "spawnLabel2";
            spawnLabel2.Size = new Size(20, 15);
            spawnLabel2.Text = "-1";

            labelWorkingDirValue = new Label();
            labelWorkingDirValue.BackColor = Color.Transparent;
            labelWorkingDirValue.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelWorkingDirValue.ForeColor = SystemColors.MenuText;
            labelWorkingDirValue.Location = new Point(100, 150);
            labelWorkingDirValue.Name = "labelWorkingDirValue";
            labelWorkingDirValue.Size = new Size(1000, 20);
            labelWorkingDirValue.Text = "";
            labelWorkingDirValue.MouseHover += (s, e) =>
            {
                tooltip.Show(labelWorkingDirValue.Text, labelWorkingDirValue, labelWorkingDirValue.Width / 6, labelWorkingDirValue.Height);
            };
            labelWorkingDirValue.MouseLeave += (s, e) =>
            {
                tooltip.Hide(labelWorkingDirValue);
            };
            btnClearWorkingDir = new DarkButton();
            btnClearWorkingDir.Size = new Size(20, 20);
            btnClearWorkingDir.Location = new Point(19, 146);
            btnClearWorkingDir.Name = "btnClearWorkingDir";
            btnClearWorkingDir.Image = new Bitmap(Embeds.Bitmaps["Erase"], new Size(16, 16)); // Use a suitable icon key
            btnClearWorkingDir.TabIndex = 10;
            btnClearWorkingDir.Click += btnClearWorkingDir_Click;

            btnClearWorkingDir.MouseHover += (s, e) =>
            {
                tooltip.Show("Clear working directory", btnClearWorkingDir, btnClearWorkingDir.Width + 5, btnClearWorkingDir.Height / 2);
            };
            btnClearWorkingDir.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnClearWorkingDir);
            };

            // Search log controls
            txtSearch.Size = new Size(100, 30);
            txtSearch.Location = new Point(btnCancel.Left - 110, btnCancel.Top + 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search log...";
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtSearch.TextChanged += (s, e) =>
            {
                string search = txtSearch.Text;
                int selStart = outputLog.SelectionStart;
                int selLength = outputLog.SelectionLength;

                // Remove previous highlights
                outputLog.SelectAll();
                outputLog.SelectionBackColor = outputLog.BackColor;

                if (search.Trim().Length >= 3)
                {
                    int count = 0;
                    int idx = 0;
                    while ((idx = outputLog.Text.IndexOf(search, idx, StringComparison.OrdinalIgnoreCase)) != -1)
                    {
                        outputLog.Select(idx, search.Length);
                        outputLog.SelectionBackColor = Color.Gray;
                        idx += search.Length;
                        count++;
                    }

                    labelSearchCount.Text = $"{count} result(s) found";
                } else
                {
                    labelSearchCount.Text = "";
                }

                // restore original selection
                outputLog.Select(selStart, selLength);
            };

            labelSearchCount.AutoSize = true;
            labelSearchCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSearchCount.TextAlign = ContentAlignment.MiddleLeft;
            labelSearchCount.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSearchCount.ForeColor = SystemColors.GrayText;
            labelSearchCount.BackColor = Color.Transparent;
            labelSearchCount.Location = new Point(txtSearch.Left, txtSearch.Top - 18);
            labelSearchCount.Name = "labelSearchCount";
            labelSearchCount.Size = new Size(120, 15);
            labelSearchCount.Text = ""; // Initially empty

            // Ctrl+F on outputLog focuses txtSearch
            outputLog.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.F)
                {
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.F3 && !e.Shift)
                {
                    ScrollToNextSearchResult();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.F3 && e.Shift)
                {
                    ScrollToPreviousSearchResult();
                    e.SuppressKeyPress = true;
                }
            };

            // F3 on txtSearch also scrolls to next result
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.F3 && !e.Shift)
                {
                    ScrollToNextSearchResult();
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.F3 && e.Shift)
                {
                    ScrollToPreviousSearchResult();
                    e.SuppressKeyPress = true;
                }
            };

            txtSearch.MouseHover += (s, e) =>
            {
                tooltip.Show("Use F3 or Shift+F3 for navigating search results", txtSearch, txtSearch.Width + 5, txtSearch.Height / 2);
            };
            txtSearch.MouseLeave += (s, e) =>
            {
                tooltip.Hide(txtSearch);
            };

            pnOptions.Controls.Add(labelPathCfgValue);
            pnOptions.Controls.Add(labelPathExeValue);
            pnOptions.Controls.Add(btnRebuild);
            pnOptions.Controls.Add(btnCancel);
            pnOptions.Controls.Add(btnMakeNewConfig);
            pnOptions.Controls.Add(btnEditConfig);
            pnOptions.Controls.Add(btnClearConfig);
            pnOptions.Controls.Add(btnRecheckConfig);
            pnOptions.Controls.Add(btnPathCfg);
            pnOptions.Controls.Add(labelPathCfgInfo);
            pnOptions.Controls.Add(btnPathExe);
            pnOptions.Controls.Add(labelPathCfg);
            pnOptions.Controls.Add(labelPathExe);
            pnOptions.Controls.Add(labelWorkingDir);
            pnOptions.Controls.Add(labelWorkingDirInfo);
            pnOptions.Controls.Add(labelWorkingDirValue);
            pnOptions.Controls.Add(btnWorkingDir);
            pnOptions.Controls.Add(warningLabel);
            pnOptions.Controls.Add(warningLabel2);
            pnOptions.Controls.Add(btnClearWorkingDir);
            pnOptions.Controls.Add(labelLog);
            pnOptions.Controls.Add(outputLog);
            pnOptions.Controls.Add(txtSearch);
            pnOptions.Controls.Add(labelSearchCount);
            pnOptions.Controls.Add(spawnLabel);
            pnOptions.Controls.Add(comboSpawns);
            pnOptions.Controls.Add(spawnLabel2);
            pnOptions.Location = new Point(0, 0);
            pnOptions.Name = "pnOptions";
            pnOptions.Size = new Size(700, BASE_HEIGHT);
            pnOptions.Dock = DockStyle.Fill;

            // ----------------------------------------------------

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, BASE_HEIGHT);
            Controls.Add(pnOptions);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            Name = "Rebuild (c2export)";
            Text = "Rebuild (c2export)";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            MinimumSize = new Size(700, BASE_HEIGHT - 50);
            pnOptions.ResumeLayout(false);
            pnOptions.PerformLayout();
            ResumeLayout(false);
        }

        private void ComboSpawns_SelectedIndexChanged()
        {
            if (spawnLabel2 is not null)
                spawnLabel2.Text = comboSpawns.SelectedIndex == -1 ? "-" : (comboSpawns.SelectedIndex + 1).ToString();
        }

        private void comboSpawnsOnOpen()
        {
            comboSpawns.Items.Clear();
            comboSpawns.SelectedIndex = -1;

            NSFBox nsfbox = null;
            try {
                nsfbox = (NSFBox)owner.TabControl.SelectedTab?.Tag;
            } 
            catch {
                return;
            }

            if (nsfbox is null)
                return;

            NSF nsf = nsfbox.NSF;
            var entries = nsf.GetEntries<ZoneEntry>();
            entries.Sort((a, b) => a.EID.CompareTo(b.EID));

            foreach (ZoneEntry zone in entries)
            {
                if (zone.CameraCount == 0)
                    continue;

                foreach (Entity entity in zone.Entities)
                {
                    if (!entity.ID.HasValue)
                        continue;
                    if ((entity.Type == 34 && entity.Subtype == 4) || (entity.Type == 0 && entity.Subtype == 0))
                    {
                        string itemText = $"{zone.EName} ID {entity.ID.Value}";
                        if (!comboSpawns.Items.Contains(itemText))                       
                            comboSpawns.Items.Add(itemText);                        
                    }
                }
            }
            ComboSpawns_SelectedIndexChanged();
        }

        private void ScrollToNextSearchResult()
        {
            string search = txtSearch.Text;
            if (string.IsNullOrEmpty(search))
                return;

            string text = outputLog.Text;
            int start = outputLog.SelectionStart + outputLog.SelectionLength;
            int idx = text.IndexOf(search, start, StringComparison.OrdinalIgnoreCase);

            if (idx == -1 && start > 0)
            {
                // Loop to start
                idx = text.IndexOf(search, 0, StringComparison.OrdinalIgnoreCase);
            }

            if (idx != -1)
            {
                outputLog.Select(idx, search.Length);
                outputLog.ScrollToCaret();
                lastSearchIndex = idx;
            }
        }

        private void ScrollToPreviousSearchResult()
        {
            string search = txtSearch.Text;
            if (string.IsNullOrEmpty(search))
                return;

            string text = outputLog.Text;
            int start = outputLog.SelectionStart;

            // Search backwards from just before the current selection
            int idx = -1;
            int lastIdx = -1;
            int searchLen = search.Length;
            int searchStart = 0;

            while (true)
            {
                idx = text.IndexOf(search, searchStart, StringComparison.OrdinalIgnoreCase);
                if (idx == -1 || idx >= start - 1)
                    break;
                lastIdx = idx;
                searchStart = idx + 1;
            }

            // If not found, wrap to the last occurrence
            if (lastIdx == -1)
            {
                // Find the last occurrence in the text
                searchStart = 0;
                while (true)
                {
                    idx = text.IndexOf(search, searchStart, StringComparison.OrdinalIgnoreCase);
                    if (idx == -1)
                        break;
                    lastIdx = idx;
                    searchStart = idx + 1;
                }
            }

            if (lastIdx != -1)
            {
                outputLog.Select(lastIdx, searchLen);
                outputLog.ScrollToCaret();
                lastSearchIndex = lastIdx;
            }
        }

        #endregion

        private Label labelPathExe;
        private DarkButton btnPathExe;
        private Label labelPathExeValue;

        private Label labelPathCfg;
        private DarkButton btnMakeNewConfig;
        private DarkButton btnEditConfig;
        private DarkButton btnClearConfig;
        private DarkButton btnRecheckConfig;
        private Label labelPathCfgInfo;
        private DarkButton btnPathCfg;
        private Label labelPathCfgValue;

        private Label warningLabel;
        private Label warningLabel2;

        private Label labelWorkingDir;
        private Label labelWorkingDirInfo;
        private DarkButton btnWorkingDir;
        private Label labelWorkingDirValue;
        private DarkButton btnClearWorkingDir;

        private Panel pnOptions;
        private ToolTip tooltip;
        private DarkButton btnRebuild;
        private DarkButton btnCancel;

        private Label labelLog;
        private DarkRichTextBox outputLog;

        private TextBox txtSearch;
        private Label labelSearchCount;
        private int lastSearchIndex = -1;

        private Label spawnLabel;
        private DarkComboBox comboSpawns;
        private Label spawnLabel2;
    }
}