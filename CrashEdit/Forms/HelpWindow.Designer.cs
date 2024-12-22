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
            label5 = new Label();
            label6 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.MediumSpringGreen;
            label5.Location = new Point(4, 273);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(105, 42);
            label5.TabIndex = 9;
            label5.Text = "\r\nShortcut Keys";
            label5.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(14, 323);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(139, 136);
            label6.TabIndex = 10;
            label6.Text = "Open (Ctrl + O)\r\nSave (Ctrl + Shift + S)\r\nPatch NSD (Ctrl + S)\r\nClose (Ctrl + Shift + C)\r\nFind (Ctrl + F)\r\nFind Next (F3)\r\nFind first node (Enter)\r\nPlay (F1)";
            label6.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.DarkTurquoise;
            label3.Location = new Point(4, 4);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(88, 21);
            label3.TabIndex = 14;
            label3.Text = "Hex Viewer";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(14, 25);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(206, 136);
            label4.TabIndex = 13;
            label4.Text = resources.GetString("label4.Text");
            // 
            // HelpWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(333, 472);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(label5);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private Label label6;
        private Label label3;
        private Label label4;
    }
}