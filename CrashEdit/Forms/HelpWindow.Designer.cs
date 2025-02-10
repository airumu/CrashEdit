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
            fraNSDBox = new DarkGroupBox();
            fraSpawns = new DarkGroupBox();
            lblNSDBox = new Label();
            fraHexViewer.SuspendLayout();
            fraNSDBox.SuspendLayout();
            fraSpawns.SuspendLayout();
            SuspendLayout();
            // 
            // fraHexViewer
            // 
            fraHexViewer.AutoSize = true;
            fraHexViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraHexViewer.BackColor = Color.Transparent;
            fraHexViewer.Controls.Add(lblHexViewer);
            fraHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
            lblHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHexViewer.ForeColor = SystemColors.ControlText;
            lblHexViewer.Location = new Point(6, 19);
            lblHexViewer.Name = "lblHexViewer";
            lblHexViewer.Size = new Size(246, 210);
            lblHexViewer.TabIndex = 0;
            lblHexViewer.Text = resources.GetString("lblHexViewer.Text");
            // 
            // fraNSDBox
            // 
            fraNSDBox.AutoSize = true;
            fraNSDBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraNSDBox.BackColor = Color.Transparent;
            fraNSDBox.Controls.Add(fraSpawns);
            fraNSDBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraNSDBox.Location = new Point(4, 257);
            fraNSDBox.Name = "fraNSDBox";
            fraNSDBox.Size = new Size(225, 142);
            fraNSDBox.TabIndex = 15;
            fraNSDBox.TabStop = false;
            fraNSDBox.Text = "NSDBox";
            // 
            // fraSpawns
            // 
            fraSpawns.AutoSize = true;
            fraSpawns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSpawns.BackColor = Color.Transparent;
            fraSpawns.Controls.Add(lblNSDBox);
            fraSpawns.Font = new Font("Segoe UI", 9F);
            fraSpawns.Location = new Point(6, 22);
            fraSpawns.Name = "fraSpawns";
            fraSpawns.Size = new Size(213, 98);
            fraSpawns.TabIndex = 16;
            fraSpawns.TabStop = false;
            fraSpawns.Text = "Spawn Point(s)";
            // 
            // lblNSDBox
            // 
            lblNSDBox.AutoSize = true;
            lblNSDBox.ForeColor = SystemColors.ControlText;
            lblNSDBox.Location = new Point(6, 19);
            lblNSDBox.Name = "lblNSDBox";
            lblNSDBox.Size = new Size(201, 60);
            lblNSDBox.TabIndex = 0;
            lblNSDBox.Text = "[Mouse drag] Move row\r\n[Right-click] Show context menu\r\n[Ctrl + C] Copy selected spawn point\r\n[Ctrl + V] Paste selected spawn point";
            // 
            // HelpWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(275, 414);
            Controls.Add(fraNSDBox);
            Controls.Add(fraHexViewer);
            CornerStyle = CornerPreference.Default;
            ForeColor = Color.Gainsboro;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelpWindow";
            Padding = new Padding(0, 0, 5, 5);
            Text = "Help";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraHexViewer.ResumeLayout(false);
            fraHexViewer.PerformLayout();
            fraNSDBox.ResumeLayout(false);
            fraNSDBox.PerformLayout();
            fraSpawns.ResumeLayout(false);
            fraSpawns.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DarkGroupBox fraHexViewer;
        private Label lblHexViewer;
        private DarkGroupBox fraNSDBox;
        private Label lblNSDBox;
        private DarkGroupBox fraSpawns;
    }
}