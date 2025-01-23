using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class InputWindow
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
            txtInput = new DarkTextBox();
            cmdCancel = new DarkButton();
            cmdOK = new DarkButton();
            lblInput = new Label();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInput.BackColor = Color.FromArgb(26, 26, 28);
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.ForeColor = Color.FromArgb(213, 213, 213);
            txtInput.Location = new Point(12, 36);
            txtInput.Margin = new Padding(4, 3, 4, 3);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(276, 23);
            txtInput.TabIndex = 0;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.BorderColour = Color.Empty;
            cmdCancel.CustomColour = false;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.FlatBottom = false;
            cmdCancel.FlatTop = false;
            cmdCancel.Location = new Point(208, 84);
            cmdCancel.Margin = new Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Padding = new Padding(6);
            cmdCancel.Size = new Size(80, 27);
            cmdCancel.TabIndex = 1;
            cmdCancel.Text = "Cancel";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // cmdOK
            // 
            cmdOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdOK.BorderColour = Color.Empty;
            cmdOK.CustomColour = false;
            cmdOK.FlatBottom = false;
            cmdOK.FlatTop = false;
            cmdOK.Location = new Point(120, 84);
            cmdOK.Margin = new Padding(4, 3, 4, 3);
            cmdOK.Name = "cmdOK";
            cmdOK.Padding = new Padding(6);
            cmdOK.Size = new Size(80, 27);
            cmdOK.TabIndex = 2;
            cmdOK.Text = "OK";
            cmdOK.Click += cmdOK_Click;
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.BackColor = Color.Transparent;
            lblInput.Location = new Point(12, 12);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(32, 15);
            lblInput.TabIndex = 3;
            lblInput.Text = "label";
            // 
            // InputWindow
            // 
            AcceptButton = cmdOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(304, 123);
            Controls.Add(lblInput);
            Controls.Add(cmdOK);
            Controls.Add(cmdCancel);
            Controls.Add(txtInput);
            CornerStyle = CornerPreference.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputWindow";
            StartPosition = FormStartPosition.CenterScreen;
            TransparencyKey = Color.FromArgb(31, 31, 32);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkTextBox txtInput;
        private DarkButton cmdCancel;
        private DarkButton cmdOK;
        private Label lblInput;
    }
}