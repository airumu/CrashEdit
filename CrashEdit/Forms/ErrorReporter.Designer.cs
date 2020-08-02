using Crash;
using DarkUI.Controls;

namespace CrashEdit
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
            this.lblTitle = new DarkUI.Controls.DarkLabel();
            this.lblMessage = new DarkUI.Controls.DarkLabel();
            this.pnOptions = new System.Windows.Forms.Panel();
            this.optIgnore = new DarkUI.Controls.DarkRadioButton();
            this.optIgnoreAll = new DarkUI.Controls.DarkRadioButton();
            this.optSkip = new DarkUI.Controls.DarkRadioButton();
            this.optAbort = new DarkUI.Controls.DarkRadioButton();
            this.optBreak = new DarkUI.Controls.DarkRadioButton();
            this.cmdOK = new DarkUI.Controls.DarkButton();
            this.pnOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(406, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "An error occurred.";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblMessage.Location = new System.Drawing.Point(12, 38);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(406, 37);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "<MESSAGE>";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnOptions
            // 
            this.pnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnOptions.Controls.Add(this.optIgnore);
            this.pnOptions.Controls.Add(this.optIgnoreAll);
            this.pnOptions.Controls.Add(this.optSkip);
            this.pnOptions.Controls.Add(this.optAbort);
            this.pnOptions.Controls.Add(this.optBreak);
            this.pnOptions.Location = new System.Drawing.Point(12, 78);
            this.pnOptions.Name = "pnOptions";
            this.pnOptions.Size = new System.Drawing.Size(406, 109);
            this.pnOptions.TabIndex = 2;
            // 
            // optIgnore
            // 
            this.optIgnore.AutoSize = true;
            this.optIgnore.Location = new System.Drawing.Point(5, 45);
            this.optIgnore.Name = "optIgnore";
            this.optIgnore.Size = new System.Drawing.Size(215, 16);
            this.optIgnore.TabIndex = 3;
            this.optIgnore.Text = "Ignore the error and continue anyway.";
            // 
            // optIgnoreAll
            // 
            this.optIgnoreAll.AutoSize = true;
            this.optIgnoreAll.Location = new System.Drawing.Point(5, 66);
            this.optIgnoreAll.Name = "optIgnoreAll";
            this.optIgnoreAll.Size = new System.Drawing.Size(254, 16);
            this.optIgnoreAll.TabIndex = 3;
            this.optIgnoreAll.Text = "Ignore the error and all others for this object.";
            // 
            // optSkip
            // 
            this.optSkip.AutoSize = true;
            this.optSkip.Location = new System.Drawing.Point(5, 24);
            this.optSkip.Name = "optSkip";
            this.optSkip.Size = new System.Drawing.Size(226, 16);
            this.optSkip.TabIndex = 2;
            this.optSkip.Text = "Skip this object, leaving it unprocessed.";
            // 
            // optAbort
            // 
            this.optAbort.AutoSize = true;
            this.optAbort.Checked = true;
            this.optAbort.Location = new System.Drawing.Point(5, 3);
            this.optAbort.Name = "optAbort";
            this.optAbort.Size = new System.Drawing.Size(127, 16);
            this.optAbort.TabIndex = 1;
            this.optAbort.TabStop = true;
            this.optAbort.Text = "Abort this operation.";
            // 
            // optBreak
            // 
            this.optBreak.AutoSize = true;
            this.optBreak.Location = new System.Drawing.Point(5, 88);
            this.optBreak.Name = "optBreak";
            this.optBreak.Size = new System.Drawing.Size(225, 16);
            this.optBreak.TabIndex = 0;
            this.optBreak.Text = "Break out to a debugger. (Experts only)";
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(343, 189);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Padding = new System.Windows.Forms.Padding(5);
            this.cmdOK.Size = new System.Drawing.Size(75, 21);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // ErrorReporter
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 222);
            this.ControlBox = false;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.pnOptions);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorReporter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Reporter";
            this.pnOptions.ResumeLayout(false);
            this.pnOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkLabel lblTitle;
        private DarkLabel lblMessage;
        private System.Windows.Forms.Panel pnOptions;
        private DarkRadioButton optSkip;
        private DarkRadioButton optAbort;
        private DarkRadioButton optBreak;
        private DarkRadioButton optIgnore;
        private DarkRadioButton optIgnoreAll;
        private DarkButton cmdOK;
    }
}