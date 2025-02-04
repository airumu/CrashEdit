using AltUI.Controls;

namespace CrashEdit.CE
{
    partial class HelpWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpWindow));
            fraHexViewer = new DarkGroupBox();
            lblHexViewer = new Label();
            fraHexViewer.SuspendLayout();
            SuspendLayout();
            // 
            // fraHexViewer
            // 
            fraHexViewer.AutoSize = true;
            fraHexViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraHexViewer.Controls.Add(lblHexViewer);
            fraHexViewer.Location = new Point(4, 3);
            fraHexViewer.Name = "fraHexViewer";
            fraHexViewer.Size = new Size(258, 248);
            fraHexViewer.TabIndex = 15;
            fraHexViewer.TabStop = false;
            fraHexViewer.Text = "Hex Viewer";
            // 
            // lblHexViewer
            // 
            lblHexViewer.AutoSize = true;
            lblHexViewer.ForeColor = SystemColors.ControlText;
            lblHexViewer.Location = new Point(6, 19);
            lblHexViewer.Name = "lblHexViewer";
            lblHexViewer.Size = new Size(246, 210);
            lblHexViewer.TabIndex = 0;
            lblHexViewer.Text = resources.GetString("lblHexViewer.Text");
            // 
            // HelpWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(274, 250);
            Controls.Add(fraHexViewer);
            CornerStyle = CornerPreference.Default;
            ForeColor = Color.Gainsboro;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelpWindow";
            Padding = new Padding(0, 0, 5, 5);
            ShowIcon = false;
            Text = "Help";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraHexViewer.ResumeLayout(false);
            fraHexViewer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DarkGroupBox fraHexViewer;
        private Label lblHexViewer;
    }
}