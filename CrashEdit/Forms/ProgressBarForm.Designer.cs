using MetroSet_UI.Controls;

namespace CrashEdit.CE.Forms
{
    partial class ProgressBarForm
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
            uxProgress = new MetroSetProgressBar();
            SuspendLayout();
            // 
            // uxProgress
            // 
            uxProgress.BackgroundColor = Color.FromArgb(38, 38, 38);
            uxProgress.BorderColor = Color.FromArgb(38, 38, 38);
            uxProgress.DisabledBackColor = Color.FromArgb(38, 38, 38);
            uxProgress.DisabledBorderColor = Color.FromArgb(38, 38, 38);
            uxProgress.DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
            uxProgress.IsDerivedStyle = true;
            uxProgress.Location = new Point(14, 14);
            uxProgress.Margin = new Padding(4, 3, 4, 3);
            uxProgress.Maximum = 100;
            uxProgress.Minimum = 0;
            uxProgress.Name = "uxProgress";
            uxProgress.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal;
            uxProgress.ProgressColor = Color.FromArgb(65, 177, 225);
            uxProgress.Size = new Size(435, 27);
            uxProgress.Style = MetroSet_UI.Enums.Style.Dark;
            uxProgress.StyleManager = null;
            uxProgress.TabIndex = 0;
            uxProgress.ThemeAuthor = "Narwin";
            uxProgress.ThemeName = "MetroDark";
            uxProgress.UseWaitCursor = true;
            uxProgress.Value = 0;
            // 
            // ProgressBarForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 54);
            ControlBox = false;
            Controls.Add(uxProgress);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressBarForm";
            Text = " ";
            TopMost = true;
            TransparencyKey = Color.FromArgb(31, 31, 32);
            UseWaitCursor = true;
            ResumeLayout(false);
        }

        #endregion

        private MetroSetProgressBar uxProgress;
    }
}