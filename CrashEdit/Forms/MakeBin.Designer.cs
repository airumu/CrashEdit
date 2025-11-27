using AltUI.Controls;

namespace CrashEdit.CE.Forms
{
    partial class MakeBin
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
            darkGroupBox1 = new DarkGroupBox();
            prgProgress = new MetroSet_UI.Controls.MetroSetProgressBar();
            dpdRegion = new DarkComboBox();
            lblSavePath = new Label();
            lblPath = new Label();
            btnMakeBin = new DarkButton();
            btnSavePath = new DarkButton();
            btnPath = new DarkButton();
            darkLabel3 = new Label();
            darkLabel2 = new Label();
            darkLabel1 = new Label();
            pnOptions = new Panel();
            pnOptions.SuspendLayout();
            SuspendLayout();
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.Location = new Point(10, 12);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(329, 220);
            darkGroupBox1.TabIndex = 1;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "darkGroupBox1";
            // 
            // prgProgress
            // 
            prgProgress.BackgroundColor = Color.FromArgb(38, 38, 38);
            prgProgress.BorderColor = Color.FromArgb(38, 38, 38);
            prgProgress.DisabledBackColor = Color.FromArgb(38, 38, 38);
            prgProgress.DisabledBorderColor = Color.FromArgb(38, 38, 38);
            prgProgress.DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
            prgProgress.IsDerivedStyle = true;
            prgProgress.Location = new Point(10, 166);
            prgProgress.Maximum = 100;
            prgProgress.Minimum = 0;
            prgProgress.Name = "prgProgress";
            prgProgress.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal;
            prgProgress.ProgressColor = Color.FromArgb(65, 177, 225);
            prgProgress.Size = new Size(318, 23);
            prgProgress.Style = MetroSet_UI.Enums.Style.Dark;
            prgProgress.StyleManager = null;
            prgProgress.TabIndex = 9;
            prgProgress.Text = "metroSetProgressBar1";
            prgProgress.ThemeAuthor = "Narwin";
            prgProgress.ThemeName = "MetroDark";
            prgProgress.Value = 0;
            // 
            // dpdRegion
            // 
            dpdRegion.DrawMode = DrawMode.OwnerDrawVariable;
            dpdRegion.FormattingEnabled = true;
            dpdRegion.Location = new Point(67, 124);
            dpdRegion.Name = "dpdRegion";
            dpdRegion.Size = new Size(94, 24);
            dpdRegion.TabIndex = 8;
            // 
            // lblSavePath
            // 
            lblSavePath.BackColor = Color.Transparent;
            lblSavePath.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSavePath.ForeColor = SystemColors.MenuText;
            lblSavePath.Location = new Point(67, 94);
            lblSavePath.Name = "lblSavePath";
            lblSavePath.Size = new Size(270, 27);
            lblSavePath.TabIndex = 7;
            lblSavePath.Text = "save path";
            // 
            // lblPath
            // 
            lblPath.BackColor = Color.Transparent;
            lblPath.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPath.ForeColor = SystemColors.MenuText;
            lblPath.Location = new Point(67, 38);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(270, 27);
            lblPath.TabIndex = 6;
            lblPath.Text = "path";
            // 
            // btnMakeBin
            // 
            btnMakeBin.BorderColour = Color.Empty;
            btnMakeBin.CustomColour = false;
            btnMakeBin.Enabled = false;
            btnMakeBin.FlatBottom = false;
            btnMakeBin.FlatTop = false;
            btnMakeBin.Location = new Point(232, 124);
            btnMakeBin.Name = "btnMakeBin";
            btnMakeBin.Padding = new Padding(5);
            btnMakeBin.Size = new Size(87, 34);
            btnMakeBin.TabIndex = 5;
            btnMakeBin.Text = "Make BIN";
            btnMakeBin.Click += btnMakeBin_Click;
            // 
            // btnSavePath
            // 
            btnSavePath.BorderColour = Color.Empty;
            btnSavePath.CustomColour = false;
            btnSavePath.FlatBottom = false;
            btnSavePath.FlatTop = false;
            btnSavePath.Location = new Point(67, 68);
            btnSavePath.Name = "btnSavePath";
            btnSavePath.Padding = new Padding(5);
            btnSavePath.Size = new Size(75, 23);
            btnSavePath.TabIndex = 4;
            btnSavePath.Text = "Browse...";
            btnSavePath.Click += btnSavePath_Click;
            // 
            // btnPath
            // 
            btnPath.BorderColour = Color.Empty;
            btnPath.CustomColour = false;
            btnPath.FlatBottom = false;
            btnPath.FlatTop = false;
            btnPath.Location = new Point(67, 12);
            btnPath.Name = "btnPath";
            btnPath.Padding = new Padding(5);
            btnPath.Size = new Size(75, 23);
            btnPath.TabIndex = 3;
            btnPath.Text = "Browse...";
            btnPath.Click += btnPath_Click;
            // 
            // darkLabel3
            // 
            darkLabel3.AutoSize = true;
            darkLabel3.BackColor = Color.Transparent;
            darkLabel3.Location = new Point(3, 127);
            darkLabel3.Name = "darkLabel3";
            darkLabel3.Size = new Size(44, 15);
            darkLabel3.TabIndex = 2;
            darkLabel3.Text = "Region";
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.BackColor = Color.Transparent;
            darkLabel2.Location = new Point(3, 72);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new Size(48, 15);
            darkLabel2.TabIndex = 1;
            darkLabel2.Text = "Save to:";
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.BackColor = Color.Transparent;
            darkLabel1.Location = new Point(3, 16);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new Size(46, 15);
            darkLabel1.TabIndex = 0;
            darkLabel1.Text = "Source:";
            // 
            // pnOptions
            // 
            pnOptions.Controls.Add(dpdRegion);
            pnOptions.Controls.Add(lblSavePath);
            pnOptions.Controls.Add(lblPath);
            pnOptions.Controls.Add(btnMakeBin);
            pnOptions.Controls.Add(btnSavePath);
            pnOptions.Controls.Add(btnPath);
            pnOptions.Controls.Add(darkLabel3);
            pnOptions.Controls.Add(darkLabel2);
            pnOptions.Controls.Add(darkLabel1);
            pnOptions.Location = new Point(0, 0);
            pnOptions.Name = "pnOptions";
            pnOptions.Size = new Size(340, 160);
            pnOptions.TabIndex = 10;
            // 
            // MakeBin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 199);
            Controls.Add(pnOptions);
            Controls.Add(prgProgress);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MakeBin";
            Text = "Make BIN";
            Icon = Embeds.GetIcon("CD");
            TransparencyKey = Color.FromArgb(31, 31, 32);
            pnOptions.ResumeLayout(false);
            pnOptions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DarkGroupBox darkGroupBox1;
        private Label darkLabel1;
        private Label darkLabel3;
        private Label darkLabel2;
        private DarkButton btnMakeBin;
        private DarkButton btnSavePath;
        private DarkButton btnPath;
        private Label lblSavePath;
        private Label lblPath;
        private DarkComboBox dpdRegion;
        private MetroSet_UI.Controls.MetroSetProgressBar prgProgress;
        private Panel pnOptions;
    }
}