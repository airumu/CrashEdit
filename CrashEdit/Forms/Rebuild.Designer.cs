using AltUI.Controls;

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
            btnPathCfg = new DarkButton();
            labelPathCfgValue = new Label();
            tooltip = new ToolTip();
            warningLabel = new Label();
            btnRebuild = new DarkButton();
            pnOptions = new Panel();
            pnOptions.SuspendLayout();
            SuspendLayout();

            labelPathExe.AutoSize = true;
            labelPathExe.BackColor = Color.Transparent;
            labelPathExe.Location = new Point(8, 16);
            labelPathExe.Name = "labelPathExe";
            labelPathExe.Size = new Size(46, 15);
            labelPathExe.TabIndex = 0;
            labelPathExe.Text = "Path to c2export exe:";

            btnPathExe.BorderColour = Color.Empty;
            btnPathExe.CustomColour = false;
            btnPathExe.FlatBottom = false;
            btnPathExe.FlatTop = false;
            btnPathExe.Location = new Point(590, 12);
            btnPathExe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPathExe.Name = "btnPathExe";
            btnPathExe.Padding = new Padding(5);
            btnPathExe.Size = new Size(100, 25);
            btnPathExe.TabIndex = 4;
            btnPathExe.Text = "...";
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
            labelPathExeValue.Location = new Point(67, 38);
            labelPathExeValue.Name = "labelPathExeValue";
            labelPathExeValue.Size = new Size(1000, 27);
            labelPathExeValue.TabIndex = 6;
            labelPathExeValue.Text = "exe path";
            labelPathExeValue.MouseHover += (e, a) =>
            {
                tooltip.Show(labelPathExeValue.Text, labelPathExeValue, labelPathExeValue.Width / 6, labelPathExeValue.Height / 2);
            };
            labelPathExeValue.MouseLeave += (e, a) =>
            {
                tooltip.Hide(labelPathExeValue);
            };

            // ------------------------------------------------------

            labelPathCfg.AutoSize = true;
            labelPathCfg.BackColor = Color.Transparent;
            labelPathCfg.Location = new Point(8, 72);
            labelPathCfg.Name = "labelPathCfg";
            labelPathCfg.Size = new Size(48, 15);
            labelPathCfg.TabIndex = 1;
            labelPathCfg.Text = "Rebuild arguments:";

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
                tooltip.Show("Autodetect looks in the NSF's folder for .txt files whose names contain 'rebuild', 'rebuilt' or 'args' - e.g. 'tree rebuild args.txt'", labelPathCfgInfo, labelPathCfgInfo.Width + 5, labelPathCfgInfo.Height / 2);
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
            btnPathCfg.Padding = new Padding(5);
            btnPathCfg.Size = new Size(100, 25);
            btnPathCfg.TabIndex = 3;
            btnPathCfg.Text = "...";
            btnPathCfg.Click += btnPathCfg_Click;
            btnPathCfg.MouseHover += (s, e) =>
            {
                tooltip.Show("Select config file", btnPathCfg, btnPathCfg.Width + 5, btnPathCfg.Height / 2);
            };
            btnPathCfg.MouseLeave += (s, e) =>
            {
                tooltip.Hide(btnPathCfg);
            };

            labelPathCfgValue.BackColor = Color.Transparent;
            labelPathCfgValue.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPathCfgValue.ForeColor = SystemColors.MenuText;
            labelPathCfgValue.Location = new Point(67, 94);
            labelPathCfgValue.Name = "labelPathCfgValue";
            labelPathCfgValue.Size = new Size(1000, 27);
            labelPathCfgValue.TabIndex = 7;
            labelPathCfgValue.Text = "config path";
            labelPathCfgValue.MouseHover += (s, e) =>
            {
                tooltip.Show(labelPathCfgValue.Text, labelPathCfgValue, labelPathCfgValue.Width / 6, labelPathCfgValue.Height / 2);
            };
            labelPathCfgValue.MouseLeave += (s, e) =>
            {
                tooltip.Hide(labelPathCfgValue);
            };

            // ----------------------------------------------------

            warningLabel.BackColor = Color.Transparent;
            warningLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            warningLabel.ForeColor = Color.Red;
            warningLabel.Location = new Point(8, 120);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(384, 30);
            warningLabel.TabIndex = 8;
            warningLabel.Text = "";
            warningLabel.Visible = false;

            btnRebuild.BorderColour = Color.Empty;
            btnRebuild.CustomColour = false;
            btnRebuild.Enabled = false;
            btnRebuild.FlatBottom = false;
            btnRebuild.FlatTop = false;
            btnRebuild.Location = new Point(590, 150);
            btnRebuild.Name = "btnRebuild";
            btnRebuild.Padding = new Padding(5);
            btnRebuild.Size = new Size(100, 30);
            btnRebuild.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRebuild.TabIndex = 5;
            btnRebuild.Text = "Rebuild";
            btnRebuild.Click += btnRebuild_Click;

            outputLog = new DarkTextBox();
            outputLog.Multiline = true;
            outputLog.ScrollBars = ScrollBars.Vertical;
            outputLog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            outputLog.Location = new Point(8, btnRebuild.Bottom + 12);
            outputLog.Size = new Size(700 - 16, this.Height - 150);
            outputLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            outputLog.Name = "outputLog";
            outputLog.TabIndex = 20;
            outputLog.Text = "";
            outputLog.ReadOnly = true;

            labelLog = new Label();
            labelLog.AutoSize = true;
            labelLog.BackColor = Color.Transparent;
            labelLog.Location = new Point(8, outputLog.Top - 8);
            labelLog.Name = "labelLog";
            labelLog.Size = new Size(80, 20);
            labelLog.Text = "Log:";

            pnOptions.Controls.Add(labelPathCfgValue);
            pnOptions.Controls.Add(labelPathExeValue);
            pnOptions.Controls.Add(btnRebuild);
            pnOptions.Controls.Add(btnPathCfg);
            pnOptions.Controls.Add(labelPathCfgInfo);
            pnOptions.Controls.Add(btnPathExe);
            pnOptions.Controls.Add(labelPathCfg);
            pnOptions.Controls.Add(labelPathExe);
            pnOptions.Controls.Add(warningLabel);
            pnOptions.Controls.Add(labelLog);
            pnOptions.Controls.Add(outputLog);
            pnOptions.Location = new Point(0, 0);
            pnOptions.Name = "pnOptions";
            pnOptions.Size = new Size(700, 350);
            pnOptions.TabIndex = 10;
            pnOptions.Dock = DockStyle.Fill;

            // ----------------------------------------------------

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 350);
            Controls.Add(pnOptions);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Rebuild (c2export)";
            Text = "Rebuild (c2export)";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            MinimumSize = new Size(600, 350);
            pnOptions.ResumeLayout(false);
            pnOptions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelPathExe;
        private DarkButton btnPathExe;
        private Label labelPathExeValue;

        private Label labelPathCfg;
        private Label labelPathCfgInfo;
        private DarkButton btnPathCfg;
        private Label labelPathCfgValue;

        private Label warningLabel;

        private Panel pnOptions;
        private ToolTip tooltip;
        private DarkButton btnRebuild;

        private Label labelLog;
        private DarkTextBox outputLog;

    }
}