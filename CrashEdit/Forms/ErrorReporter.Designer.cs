using AltUI.Controls;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    partial class ErrorReporter
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
            ErrorManager.Signal -= ErrorManager_Signal;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblMessage = new Label();
            pnOptions = new MetroSetPanel();
            optIgnore = new RadioButton();
            optIgnoreAll = new RadioButton();
            optSkip = new RadioButton();
            optAbort = new RadioButton();
            optBreak = new RadioButton();
            cmdOK = new DarkButton();
            pnOptions.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(14, 10);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(455, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "An error occurred.";
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.Location = new Point(14, 47);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(455, 46);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "<MESSAGE>";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnOptions
            // 
            pnOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnOptions.BackgroundColor = Color.FromArgb(31, 31, 32);
            pnOptions.BorderColor = Color.FromArgb(110, 110, 110);
            pnOptions.BorderThickness = 1;
            pnOptions.Controls.Add(optIgnore);
            pnOptions.Controls.Add(optIgnoreAll);
            pnOptions.Controls.Add(optSkip);
            pnOptions.Controls.Add(optAbort);
            pnOptions.Controls.Add(optBreak);
            pnOptions.IsDerivedStyle = false;
            pnOptions.Location = new Point(14, 97);
            pnOptions.Margin = new Padding(4, 3, 4, 3);
            pnOptions.Name = "pnOptions";
            pnOptions.Size = new Size(455, 141);
            pnOptions.Style = MetroSet_UI.Enums.Style.Dark;
            pnOptions.StyleManager = null;
            pnOptions.TabIndex = 2;
            pnOptions.ThemeAuthor = "Narwin";
            pnOptions.ThemeName = "MetroDark";
            // 
            // optIgnore
            // 
            optIgnore.AutoSize = true;
            optIgnore.Location = new Point(6, 57);
            optIgnore.Margin = new Padding(4, 3, 4, 3);
            optIgnore.Name = "optIgnore";
            optIgnore.Size = new Size(226, 19);
            optIgnore.TabIndex = 3;
            optIgnore.Text = "Ignore the error and continue anyway.";
            // 
            // optIgnoreAll
            // 
            optIgnoreAll.AutoSize = true;
            optIgnoreAll.Location = new Point(6, 83);
            optIgnoreAll.Margin = new Padding(4, 3, 4, 3);
            optIgnoreAll.Name = "optIgnoreAll";
            optIgnoreAll.Size = new Size(260, 19);
            optIgnoreAll.TabIndex = 3;
            optIgnoreAll.Text = "Ignore the error and all others for this object.";
            // 
            // optSkip
            // 
            optSkip.AutoSize = true;
            optSkip.Location = new Point(6, 30);
            optSkip.Margin = new Padding(4, 3, 4, 3);
            optSkip.Name = "optSkip";
            optSkip.Size = new Size(232, 19);
            optSkip.TabIndex = 2;
            optSkip.Text = "Skip this object, leaving it unprocessed.";
            // 
            // optAbort
            // 
            optAbort.AutoSize = true;
            optAbort.Checked = true;
            optAbort.Location = new Point(6, 3);
            optAbort.Margin = new Padding(4, 3, 4, 3);
            optAbort.Name = "optAbort";
            optAbort.Size = new Size(134, 19);
            optAbort.TabIndex = 1;
            optAbort.TabStop = true;
            optAbort.Text = "Abort this operation.";
            // 
            // optBreak
            // 
            optBreak.AutoSize = true;
            optBreak.Location = new Point(6, 110);
            optBreak.Margin = new Padding(4, 3, 4, 3);
            optBreak.Name = "optBreak";
            optBreak.Size = new Size(230, 19);
            optBreak.TabIndex = 0;
            optBreak.Text = "Break out to a debugger. (Experts only)";
            // 
            // cmdOK
            // 
            cmdOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdOK.BorderColour = Color.Empty;
            cmdOK.CustomColour = false;
            cmdOK.FlatBottom = false;
            cmdOK.FlatTop = false;
            cmdOK.Location = new Point(382, 253);
            cmdOK.Margin = new Padding(4, 3, 4, 3);
            cmdOK.Name = "cmdOK";
            cmdOK.Padding = new Padding(5);
            cmdOK.Size = new Size(88, 27);
            cmdOK.TabIndex = 3;
            cmdOK.Text = "OK";
            cmdOK.Click += cmdOK_Click;
            // 
            // ErrorReporter
            // 
            AcceptButton = cmdOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(31, 31, 32);
            ClientSize = new Size(483, 293);
            ControlBox = false;
            Controls.Add(cmdOK);
            Controls.Add(pnOptions);
            Controls.Add(lblMessage);
            Controls.Add(lblTitle);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ErrorReporter";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Error Reporter";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            pnOptions.ResumeLayout(false);
            pnOptions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblMessage;
        private MetroSetPanel pnOptions;
        private RadioButton optSkip;
        private RadioButton optAbort;
        private RadioButton optBreak;
        private RadioButton optIgnore;
        private RadioButton optIgnoreAll;
        private DarkButton cmdOK;
    }
}