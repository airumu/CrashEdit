using AltUI.Controls;
using MetroSet_UI.Controls;

namespace CrashEdit.CrashUI
{
    partial class GameVersionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise,false.</param>
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
            lblMessage = new MetroSetLabel();
            fraRelease = new DarkGroupBox();
            cmdCrash3 = new DarkButton();
            cmdCrash2 = new DarkButton();
            cmdCrash1 = new DarkButton();
            fraPrerelease = new DarkGroupBox();
            darkButton1 = new DarkButton();
            cmdCrash1Beta1995 = new DarkButton();
            cmdCrash2Beta = new DarkButton();
            cmdCrash1BetaMAY11 = new DarkButton();
            cmdCrash1BetaMAR08 = new DarkButton();
            cmdCancel = new DarkButton();
            fraRelease.SuspendLayout();
            fraPrerelease.SuspendLayout();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            lblMessage.IsDerivedStyle = true;
            lblMessage.Location = new System.Drawing.Point(14, 10);
            lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new System.Drawing.Size(438, 75);
            lblMessage.Style = MetroSet_UI.Enums.Style.Dark;
            lblMessage.StyleManager = null;
            lblMessage.TabIndex = 0;
            lblMessage.Text = "<SELECT GAME MESSAGE>";
            lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblMessage.ThemeAuthor = "Narwin";
            lblMessage.ThemeName = "MetroDark";
            // 
            // fraRelease
            // 
            fraRelease.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            fraRelease.BackColor = System.Drawing.Color.FromArgb(31, 31, 32);
            fraRelease.Controls.Add(cmdCrash3);
            fraRelease.Controls.Add(cmdCrash2);
            fraRelease.Controls.Add(cmdCrash1);
            fraRelease.Location = new System.Drawing.Point(14, 89);
            fraRelease.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fraRelease.Name = "fraRelease";
            fraRelease.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fraRelease.Size = new System.Drawing.Size(438, 204);
            fraRelease.TabIndex = 1;
            fraRelease.TabStop = false;
            fraRelease.Text = "<RELEASE>";
            // 
            // cmdCrash3
            // 
            cmdCrash3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash3.BorderColour = System.Drawing.Color.Empty;
            cmdCrash3.CustomColour = false;
            cmdCrash3.FlatBottom = false;
            cmdCrash3.FlatTop = false;
            cmdCrash3.Location = new System.Drawing.Point(7, 142);
            cmdCrash3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash3.Name = "cmdCrash3";
            cmdCrash3.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash3.Size = new System.Drawing.Size(424, 53);
            cmdCrash3.TabIndex = 4;
            cmdCrash3.Text = "Crash Bandicoot: Warped\r\nクラッシュバンディクー　3:　ブッとび！　世界一周";
            cmdCrash3.Click += cmdCrash3_Click;
            // 
            // cmdCrash2
            // 
            cmdCrash2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash2.BorderColour = System.Drawing.Color.Empty;
            cmdCrash2.CustomColour = false;
            cmdCrash2.FlatBottom = false;
            cmdCrash2.FlatTop = false;
            cmdCrash2.Location = new System.Drawing.Point(7, 82);
            cmdCrash2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash2.Name = "cmdCrash2";
            cmdCrash2.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash2.Size = new System.Drawing.Size(424, 53);
            cmdCrash2.TabIndex = 3;
            cmdCrash2.Text = "Crash Bandicoot 2: Cortex Strikes Back\r\nクラッシュバンディクー　2:　コルテックスのぎゃくしゅう！";
            cmdCrash2.Click += cmdCrash2_Click;
            // 
            // cmdCrash1
            // 
            cmdCrash1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash1.BorderColour = System.Drawing.Color.Empty;
            cmdCrash1.CustomColour = false;
            cmdCrash1.FlatBottom = false;
            cmdCrash1.FlatTop = false;
            cmdCrash1.Location = new System.Drawing.Point(7, 22);
            cmdCrash1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash1.Name = "cmdCrash1";
            cmdCrash1.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash1.Size = new System.Drawing.Size(424, 53);
            cmdCrash1.TabIndex = 2;
            cmdCrash1.Text = "Crash Bandicoot\r\nクラッシュバンディクー";
            cmdCrash1.Click += cmdCrash1_Click;
            // 
            // fraPrerelease
            // 
            fraPrerelease.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            fraPrerelease.Controls.Add(darkButton1);
            fraPrerelease.Controls.Add(cmdCrash1Beta1995);
            fraPrerelease.Controls.Add(cmdCrash2Beta);
            fraPrerelease.Controls.Add(cmdCrash1BetaMAY11);
            fraPrerelease.Controls.Add(cmdCrash1BetaMAR08);
            fraPrerelease.Location = new System.Drawing.Point(14, 300);
            fraPrerelease.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fraPrerelease.Name = "fraPrerelease";
            fraPrerelease.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fraPrerelease.Size = new System.Drawing.Size(438, 325);
            fraPrerelease.TabIndex = 5;
            fraPrerelease.TabStop = false;
            fraPrerelease.Text = "<PRERELEASE>";
            // 
            // darkButton1
            // 
            darkButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            darkButton1.BorderColour = System.Drawing.Color.Empty;
            darkButton1.CustomColour = false;
            darkButton1.FlatBottom = false;
            darkButton1.FlatTop = false;
            darkButton1.Location = new System.Drawing.Point(7, 261);
            darkButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            darkButton1.Name = "darkButton1";
            darkButton1.Padding = new System.Windows.Forms.Padding(5);
            darkButton1.Size = new System.Drawing.Size(424, 53);
            darkButton1.TabIndex = 10;
            darkButton1.Text = "Crash Bandicoot: Warped\r\n\"E3 Demo\" (May 14, 1998)";
            darkButton1.Click += cmdCrash3BetaMAY14_Click;
            // 
            // cmdCrash1Beta1995
            // 
            cmdCrash1Beta1995.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash1Beta1995.BorderColour = System.Drawing.Color.Empty;
            cmdCrash1Beta1995.CustomColour = false;
            cmdCrash1Beta1995.FlatBottom = false;
            cmdCrash1Beta1995.FlatTop = false;
            cmdCrash1Beta1995.Location = new System.Drawing.Point(7, 22);
            cmdCrash1Beta1995.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash1Beta1995.Name = "cmdCrash1Beta1995";
            cmdCrash1Beta1995.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash1Beta1995.Size = new System.Drawing.Size(424, 53);
            cmdCrash1Beta1995.TabIndex = 9;
            cmdCrash1Beta1995.Text = "Crash Bandicoot\r\n\"Early Prototype\" (1995)";
            cmdCrash1Beta1995.Click += cmdCrash1Beta1995_Click;
            // 
            // cmdCrash2Beta
            // 
            cmdCrash2Beta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash2Beta.BorderColour = System.Drawing.Color.Empty;
            cmdCrash2Beta.CustomColour = false;
            cmdCrash2Beta.FlatBottom = false;
            cmdCrash2Beta.FlatTop = false;
            cmdCrash2Beta.Location = new System.Drawing.Point(7, 202);
            cmdCrash2Beta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash2Beta.Name = "cmdCrash2Beta";
            cmdCrash2Beta.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash2Beta.Size = new System.Drawing.Size(424, 53);
            cmdCrash2Beta.TabIndex = 8;
            cmdCrash2Beta.Text = "Crash Bandicoot 2: Cortex Strikes Back\r\n\"Review Copy\"";
            cmdCrash2Beta.Click += cmdCrash2Beta_Click;
            // 
            // cmdCrash1BetaMAY11
            // 
            cmdCrash1BetaMAY11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash1BetaMAY11.BorderColour = System.Drawing.Color.Empty;
            cmdCrash1BetaMAY11.CustomColour = false;
            cmdCrash1BetaMAY11.FlatBottom = false;
            cmdCrash1BetaMAY11.FlatTop = false;
            cmdCrash1BetaMAY11.Location = new System.Drawing.Point(7, 142);
            cmdCrash1BetaMAY11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash1BetaMAY11.Name = "cmdCrash1BetaMAY11";
            cmdCrash1BetaMAY11.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash1BetaMAY11.Size = new System.Drawing.Size(424, 53);
            cmdCrash1BetaMAY11.TabIndex = 7;
            cmdCrash1BetaMAY11.Text = "Crash Bandicoot\r\n\"E3 Demo\" (May 11,1996)";
            cmdCrash1BetaMAY11.Click += cmdCrash1BetaMAY11_Click;
            // 
            // cmdCrash1BetaMAR08
            // 
            cmdCrash1BetaMAR08.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmdCrash1BetaMAR08.BorderColour = System.Drawing.Color.Empty;
            cmdCrash1BetaMAR08.CustomColour = false;
            cmdCrash1BetaMAR08.FlatBottom = false;
            cmdCrash1BetaMAR08.FlatTop = false;
            cmdCrash1BetaMAR08.Location = new System.Drawing.Point(7, 82);
            cmdCrash1BetaMAR08.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCrash1BetaMAR08.Name = "cmdCrash1BetaMAR08";
            cmdCrash1BetaMAR08.Padding = new System.Windows.Forms.Padding(5);
            cmdCrash1BetaMAR08.Size = new System.Drawing.Size(424, 53);
            cmdCrash1BetaMAR08.TabIndex = 6;
            cmdCrash1BetaMAR08.Text = "Crash Bandicoot\r\n\"Prototype\" (April 8,1996)";
            cmdCrash1BetaMAR08.Click += cmdCrash1BetaMAR08_Click;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cmdCancel.BorderColour = System.Drawing.Color.Empty;
            cmdCancel.CustomColour = false;
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.FlatBottom = false;
            cmdCancel.FlatTop = false;
            cmdCancel.Location = new System.Drawing.Point(364, 631);
            cmdCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Padding = new System.Windows.Forms.Padding(5);
            cmdCancel.Size = new System.Drawing.Size(88, 27);
            cmdCancel.TabIndex = 0;
            cmdCancel.Text = "<CANCEL>";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // GameVersionForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            BackColor = System.Drawing.Color.FromArgb(31, 31, 32);
            CancelButton = cmdCancel;
            ClientSize = new System.Drawing.Size(465, 671);
            Controls.Add(cmdCancel);
            Controls.Add(fraPrerelease);
            Controls.Add(fraRelease);
            Controls.Add(lblMessage);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GameVersionForm";
            Text = "<GAME VERSION SELECTION>";
            TransparencyKey = System.Drawing.Color.FromArgb(31, 31, 32);
            fraRelease.ResumeLayout(false);
            fraPrerelease.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MetroSetLabel lblMessage;
        private DarkGroupBox fraRelease;
        private DarkGroupBox fraPrerelease;
        private DarkButton cmdCrash1;
        private DarkButton cmdCrash3;
        private DarkButton cmdCrash2;
        private DarkButton cmdCrash1BetaMAY11;
        private DarkButton cmdCrash1BetaMAR08;
        private DarkButton cmdCrash2Beta;
        private DarkButton cmdCancel;
        private DarkButton cmdCrash1Beta1995;
        private DarkButton darkButton1;
    }
}