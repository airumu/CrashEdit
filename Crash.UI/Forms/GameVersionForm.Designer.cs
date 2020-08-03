namespace Crash.UI
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
            this.lblMessage = new DarkUI.Controls.DarkLabel();
            this.fraRelease = new System.Windows.Forms.GroupBox();
            this.cmdCrash3 = new DarkUI.Controls.DarkButton();
            this.cmdCrash2 = new DarkUI.Controls.DarkButton();
            this.cmdCrash1 = new DarkUI.Controls.DarkButton();
            this.fraPrerelease = new System.Windows.Forms.GroupBox();
            this.cmdCrash1Beta1995 = new DarkUI.Controls.DarkButton();
            this.cmdCrash2Beta = new DarkUI.Controls.DarkButton();
            this.cmdCrash1BetaMAY11 = new DarkUI.Controls.DarkButton();
            this.cmdCrash1BetaMAR08 = new DarkUI.Controls.DarkButton();
            this.cmdCancel = new DarkUI.Controls.DarkButton();
            this.fraRelease.SuspendLayout();
            this.fraPrerelease.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMessage.Location = new System.Drawing.Point(14, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(376, 54);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "<SELECT GAME MESSAGE>";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fraRelease
            // 
            this.fraRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraRelease.Controls.Add(this.cmdCrash3);
            this.fraRelease.Controls.Add(this.cmdCrash2);
            this.fraRelease.Controls.Add(this.cmdCrash1);
            this.fraRelease.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.fraRelease.Location = new System.Drawing.Point(14, 68);
            this.fraRelease.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraRelease.Name = "fraRelease";
            this.fraRelease.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraRelease.Size = new System.Drawing.Size(376, 173);
            this.fraRelease.TabIndex = 1;
            this.fraRelease.TabStop = false;
            this.fraRelease.Text = "<RELEASE>";
            // 
            // cmdCrash3
            // 
            this.cmdCrash3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash3.Location = new System.Drawing.Point(7, 122);
            this.cmdCrash3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash3.Name = "cmdCrash3";
            this.cmdCrash3.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash3.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash3.TabIndex = 4;
            this.cmdCrash3.Text = "Crash Bandicoot 3: Warped\r\nクラッシュバンディクー　3:　ブッとび！　世界一周";
            this.cmdCrash3.Click += new System.EventHandler(this.cmdCrash3_Click);
            // 
            // cmdCrash2
            // 
            this.cmdCrash2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash2.Location = new System.Drawing.Point(7, 72);
            this.cmdCrash2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash2.Name = "cmdCrash2";
            this.cmdCrash2.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash2.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash2.TabIndex = 3;
            this.cmdCrash2.Text = "Crash Bandicoot 2: Cortex Strikes Back\r\nクラッシュバンディクー　2:　コルテックスのぎゃくしゅう！";
            this.cmdCrash2.Click += new System.EventHandler(this.cmdCrash2_Click);
            // 
            // cmdCrash1
            // 
            this.cmdCrash1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash1.Location = new System.Drawing.Point(7, 22);
            this.cmdCrash1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash1.Name = "cmdCrash1";
            this.cmdCrash1.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash1.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash1.TabIndex = 2;
            this.cmdCrash1.Text = "Crash Bandicoot\r\nクラッシュバンディクー";
            this.cmdCrash1.Click += new System.EventHandler(this.cmdCrash1_Click);
            // 
            // fraPrerelease
            // 
            this.fraPrerelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fraPrerelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.fraPrerelease.Controls.Add(this.cmdCrash1Beta1995);
            this.fraPrerelease.Controls.Add(this.cmdCrash2Beta);
            this.fraPrerelease.Controls.Add(this.cmdCrash1BetaMAY11);
            this.fraPrerelease.Controls.Add(this.cmdCrash1BetaMAR08);
            this.fraPrerelease.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.fraPrerelease.Location = new System.Drawing.Point(14, 249);
            this.fraPrerelease.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraPrerelease.Name = "fraPrerelease";
            this.fraPrerelease.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fraPrerelease.Size = new System.Drawing.Size(376, 223);
            this.fraPrerelease.TabIndex = 5;
            this.fraPrerelease.TabStop = false;
            this.fraPrerelease.Text = "<PRERELEASE>";
            // 
            // cmdCrash1Beta1995
            // 
            this.cmdCrash1Beta1995.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash1Beta1995.Location = new System.Drawing.Point(7, 22);
            this.cmdCrash1Beta1995.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash1Beta1995.Name = "cmdCrash1Beta1995";
            this.cmdCrash1Beta1995.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash1Beta1995.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash1Beta1995.TabIndex = 9;
            this.cmdCrash1Beta1995.Text = "Crash Bandicoot\r\n\"Early Prototype\" (1995)";
            this.cmdCrash1Beta1995.Click += new System.EventHandler(this.cmdCrash1Beta1995_Click);
            // 
            // cmdCrash2Beta
            // 
            this.cmdCrash2Beta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash2Beta.Location = new System.Drawing.Point(7, 172);
            this.cmdCrash2Beta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash2Beta.Name = "cmdCrash2Beta";
            this.cmdCrash2Beta.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash2Beta.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash2Beta.TabIndex = 8;
            this.cmdCrash2Beta.Text = "Crash Bandicoot 2: Cortex Strikes Back\r\n\"Review Copy\"";
            this.cmdCrash2Beta.Click += new System.EventHandler(this.cmdCrash2Beta_Click);
            // 
            // cmdCrash1BetaMAY11
            // 
            this.cmdCrash1BetaMAY11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash1BetaMAY11.Location = new System.Drawing.Point(7, 122);
            this.cmdCrash1BetaMAY11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash1BetaMAY11.Name = "cmdCrash1BetaMAY11";
            this.cmdCrash1BetaMAY11.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash1BetaMAY11.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash1BetaMAY11.TabIndex = 7;
            this.cmdCrash1BetaMAY11.Text = "Crash Bandicoot\r\n\"E3 Demo\" (May 11,1996)";
            this.cmdCrash1BetaMAY11.Click += new System.EventHandler(this.cmdCrash1BetaMAY11_Click);
            // 
            // cmdCrash1BetaMAR08
            // 
            this.cmdCrash1BetaMAR08.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCrash1BetaMAR08.Location = new System.Drawing.Point(7, 72);
            this.cmdCrash1BetaMAR08.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCrash1BetaMAR08.Name = "cmdCrash1BetaMAR08";
            this.cmdCrash1BetaMAR08.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCrash1BetaMAR08.Size = new System.Drawing.Size(362, 42);
            this.cmdCrash1BetaMAR08.TabIndex = 6;
            this.cmdCrash1BetaMAR08.Text = "Crash Bandicoot\r\n\"Prototype\" (March 8,1996)";
            this.cmdCrash1BetaMAR08.Click += new System.EventHandler(this.cmdCrash1BetaMAR08_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(305, 480);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Padding = new System.Windows.Forms.Padding(6);
            this.cmdCancel.Size = new System.Drawing.Size(87, 26);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "<CANCEL>";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // GameVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(404, 513);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.fraPrerelease);
            this.Controls.Add(this.fraRelease);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameVersionForm";
            this.Text = "<GAME VERSION SELECTION>";
            this.fraRelease.ResumeLayout(false);
            this.fraPrerelease.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkLabel lblMessage;
        private System.Windows.Forms.GroupBox fraRelease;
        private System.Windows.Forms.GroupBox fraPrerelease;
        private DarkUI.Controls.DarkButton cmdCrash1;
        private DarkUI.Controls.DarkButton cmdCrash3;
        private DarkUI.Controls.DarkButton cmdCrash2;
        private DarkUI.Controls.DarkButton cmdCrash1BetaMAY11;
        private DarkUI.Controls.DarkButton cmdCrash1BetaMAR08;
        private DarkUI.Controls.DarkButton cmdCrash2Beta;
        private DarkUI.Controls.DarkButton cmdCancel;
        private DarkUI.Controls.DarkButton cmdCrash1Beta1995;
    }
}