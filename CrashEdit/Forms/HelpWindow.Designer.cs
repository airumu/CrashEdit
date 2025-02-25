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
            lbHexViewer = new Label();
            fraNSDBox = new DarkGroupBox();
            fraSpawns = new DarkGroupBox();
            lbNSDBox = new Label();
            fraTextureViewer = new DarkGroupBox();
            lbTextureViewer = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            fraEntity = new DarkGroupBox();
            darkGroupBox2 = new DarkGroupBox();
            lbEntityListBox = new Label();
            darkGroupBox1 = new DarkGroupBox();
            fraSavedProperties = new DarkGroupBox();
            lbSavedProperties = new Label();
            fraProperties = new DarkGroupBox();
            lbProperties = new Label();
            fraTextureChunk = new DarkGroupBox();
            lbTextureChunk = new Label();
            fraHexViewer.SuspendLayout();
            fraNSDBox.SuspendLayout();
            fraSpawns.SuspendLayout();
            fraTextureViewer.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            fraEntity.SuspendLayout();
            darkGroupBox2.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            fraSavedProperties.SuspendLayout();
            fraProperties.SuspendLayout();
            fraTextureChunk.SuspendLayout();
            SuspendLayout();
            // 
            // fraHexViewer
            // 
            fraHexViewer.AutoSize = true;
            fraHexViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraHexViewer.BackColor = Color.Transparent;
            fraHexViewer.Controls.Add(lbHexViewer);
            fraHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraHexViewer.Location = new Point(6, 388);
            fraHexViewer.Name = "fraHexViewer";
            fraHexViewer.Size = new Size(252, 238);
            fraHexViewer.TabIndex = 15;
            fraHexViewer.TabStop = false;
            fraHexViewer.Text = "Hex Viewer";
            // 
            // lbHexViewer
            // 
            lbHexViewer.AutoSize = true;
            lbHexViewer.Dock = DockStyle.Fill;
            lbHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbHexViewer.ForeColor = SystemColors.ControlText;
            lbHexViewer.Location = new Point(3, 19);
            lbHexViewer.Margin = new Padding(3);
            lbHexViewer.Name = "lbHexViewer";
            lbHexViewer.Padding = new Padding(3);
            lbHexViewer.Size = new Size(246, 216);
            lbHexViewer.TabIndex = 0;
            lbHexViewer.Text = resources.GetString("lbHexViewer.Text");
            // 
            // fraNSDBox
            // 
            fraNSDBox.AutoSize = true;
            fraNSDBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraNSDBox.BackColor = Color.Transparent;
            fraNSDBox.Controls.Add(fraSpawns);
            fraNSDBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraNSDBox.Location = new Point(273, 164);
            fraNSDBox.Name = "fraNSDBox";
            fraNSDBox.Size = new Size(225, 132);
            fraNSDBox.TabIndex = 15;
            fraNSDBox.TabStop = false;
            fraNSDBox.Text = "NSD";
            // 
            // fraSpawns
            // 
            fraSpawns.AutoSize = true;
            fraSpawns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSpawns.BackColor = Color.Transparent;
            fraSpawns.Controls.Add(lbNSDBox);
            fraSpawns.Font = new Font("Segoe UI", 9F);
            fraSpawns.Location = new Point(6, 22);
            fraSpawns.Name = "fraSpawns";
            fraSpawns.Size = new Size(213, 88);
            fraSpawns.TabIndex = 16;
            fraSpawns.TabStop = false;
            fraSpawns.Text = "Spawn Points";
            // 
            // lbNSDBox
            // 
            lbNSDBox.AutoSize = true;
            lbNSDBox.Dock = DockStyle.Fill;
            lbNSDBox.ForeColor = SystemColors.ControlText;
            lbNSDBox.Location = new Point(3, 19);
            lbNSDBox.Margin = new Padding(3);
            lbNSDBox.Name = "lbNSDBox";
            lbNSDBox.Padding = new Padding(3);
            lbNSDBox.Size = new Size(207, 66);
            lbNSDBox.TabIndex = 0;
            lbNSDBox.Text = "[Mouse drag] Move row\r\n[Right-click] Show context menu\r\n[Ctrl + C] Copy selected spawn point\r\n[Ctrl + V] Paste selected spawn point";
            // 
            // fraTextureViewer
            // 
            fraTextureViewer.AutoSize = true;
            fraTextureViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTextureViewer.BackColor = Color.Transparent;
            fraTextureViewer.Controls.Add(lbTextureViewer);
            fraTextureViewer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraTextureViewer.Location = new Point(273, 70);
            fraTextureViewer.Name = "fraTextureViewer";
            fraTextureViewer.Size = new Size(267, 88);
            fraTextureViewer.TabIndex = 15;
            fraTextureViewer.TabStop = false;
            fraTextureViewer.Text = "Texture Viewer";
            // 
            // lbTextureViewer
            // 
            lbTextureViewer.AutoSize = true;
            lbTextureViewer.Dock = DockStyle.Fill;
            lbTextureViewer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTextureViewer.ForeColor = SystemColors.ControlText;
            lbTextureViewer.Location = new Point(3, 19);
            lbTextureViewer.Margin = new Padding(3);
            lbTextureViewer.Name = "lbTextureViewer";
            lbTextureViewer.Padding = new Padding(3);
            lbTextureViewer.Size = new Size(261, 66);
            lbTextureViewer.TabIndex = 0;
            lbTextureViewer.Text = "[Right-click] Save selected texture region to file\r\n[Ctrl+C] Copy texture to buffer\r\n[Ctrl+X] Cut texture to buffer\r\n[Ctrl+V] Paste texture from buffer";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(fraEntity);
            flowLayoutPanel1.Controls.Add(darkGroupBox1);
            flowLayoutPanel1.Controls.Add(fraHexViewer);
            flowLayoutPanel1.Controls.Add(fraTextureChunk);
            flowLayoutPanel1.Controls.Add(fraTextureViewer);
            flowLayoutPanel1.Controls.Add(fraNSDBox);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(3);
            flowLayoutPanel1.Size = new Size(554, 639);
            flowLayoutPanel1.TabIndex = 16;
            // 
            // fraEntity
            // 
            fraEntity.AutoSize = true;
            fraEntity.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraEntity.BackColor = Color.Transparent;
            fraEntity.Controls.Add(darkGroupBox2);
            fraEntity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraEntity.Location = new Point(6, 6);
            fraEntity.Name = "fraEntity";
            fraEntity.Size = new Size(261, 162);
            fraEntity.TabIndex = 15;
            fraEntity.TabStop = false;
            fraEntity.Text = "Entity";
            // 
            // darkGroupBox2
            // 
            darkGroupBox2.AutoSize = true;
            darkGroupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox2.Controls.Add(lbEntityListBox);
            darkGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkGroupBox2.Location = new Point(6, 22);
            darkGroupBox2.Name = "darkGroupBox2";
            darkGroupBox2.Size = new Size(249, 118);
            darkGroupBox2.TabIndex = 1;
            darkGroupBox2.TabStop = false;
            darkGroupBox2.Text = "(ListBox Controls)";
            // 
            // lbEntityListBox
            // 
            lbEntityListBox.AutoSize = true;
            lbEntityListBox.Dock = DockStyle.Fill;
            lbEntityListBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbEntityListBox.ForeColor = SystemColors.ControlText;
            lbEntityListBox.Location = new Point(3, 19);
            lbEntityListBox.Margin = new Padding(3);
            lbEntityListBox.Name = "lbEntityListBox";
            lbEntityListBox.Padding = new Padding(3);
            lbEntityListBox.Size = new Size(243, 96);
            lbEntityListBox.TabIndex = 0;
            lbEntityListBox.Text = resources.GetString("lbEntityListBox.Text");
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.AutoSize = true;
            darkGroupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox1.BackColor = Color.Transparent;
            darkGroupBox1.Controls.Add(fraSavedProperties);
            darkGroupBox1.Controls.Add(fraProperties);
            darkGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            darkGroupBox1.Location = new Point(6, 174);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(248, 208);
            darkGroupBox1.TabIndex = 15;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Entity Property";
            // 
            // fraSavedProperties
            // 
            fraSavedProperties.AutoSize = true;
            fraSavedProperties.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraSavedProperties.Controls.Add(lbSavedProperties);
            fraSavedProperties.Font = new Font("Segoe UI", 9F);
            fraSavedProperties.Location = new Point(6, 83);
            fraSavedProperties.Name = "fraSavedProperties";
            fraSavedProperties.Size = new Size(236, 103);
            fraSavedProperties.TabIndex = 1;
            fraSavedProperties.TabStop = false;
            fraSavedProperties.Text = "Saved Properties";
            // 
            // lbSavedProperties
            // 
            lbSavedProperties.AutoSize = true;
            lbSavedProperties.Dock = DockStyle.Fill;
            lbSavedProperties.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbSavedProperties.ForeColor = SystemColors.ControlText;
            lbSavedProperties.Location = new Point(3, 19);
            lbSavedProperties.Margin = new Padding(3);
            lbSavedProperties.Name = "lbSavedProperties";
            lbSavedProperties.Padding = new Padding(3);
            lbSavedProperties.Size = new Size(230, 81);
            lbSavedProperties.TabIndex = 0;
            lbSavedProperties.Text = "[Ctrl+V] Copy fields in selected item\r\n[Double-click / F2] Rename selected item\r\n[Delete] Remove selected items\r\n--------------------------------\r\n[Ctrl+R] Reload list";
            // 
            // fraProperties
            // 
            fraProperties.AutoSize = true;
            fraProperties.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraProperties.Controls.Add(lbProperties);
            fraProperties.Font = new Font("Segoe UI", 9F);
            fraProperties.Location = new Point(6, 22);
            fraProperties.Name = "fraProperties";
            fraProperties.Size = new Size(183, 58);
            fraProperties.TabIndex = 1;
            fraProperties.TabStop = false;
            fraProperties.Text = "Properties";
            // 
            // lbProperties
            // 
            lbProperties.AutoSize = true;
            lbProperties.Dock = DockStyle.Fill;
            lbProperties.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbProperties.ForeColor = SystemColors.ControlText;
            lbProperties.Location = new Point(3, 19);
            lbProperties.Margin = new Padding(3);
            lbProperties.Name = "lbProperties";
            lbProperties.Padding = new Padding(3);
            lbProperties.Size = new Size(177, 36);
            lbProperties.TabIndex = 0;
            lbProperties.Text = "[Ctrl+C] Save selected fields\r\n[Delete] Remove selected fields";
            // 
            // fraTextureChunk
            // 
            fraTextureChunk.AutoSize = true;
            fraTextureChunk.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTextureChunk.BackColor = Color.Transparent;
            fraTextureChunk.Controls.Add(lbTextureChunk);
            fraTextureChunk.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraTextureChunk.Location = new Point(273, 6);
            fraTextureChunk.Name = "fraTextureChunk";
            fraTextureChunk.Size = new Size(122, 58);
            fraTextureChunk.TabIndex = 15;
            fraTextureChunk.TabStop = false;
            fraTextureChunk.Text = "Texture Chunk";
            // 
            // lbTextureChunk
            // 
            lbTextureChunk.AutoSize = true;
            lbTextureChunk.Dock = DockStyle.Fill;
            lbTextureChunk.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTextureChunk.ForeColor = SystemColors.ControlText;
            lbTextureChunk.Location = new Point(3, 19);
            lbTextureChunk.Margin = new Padding(3);
            lbTextureChunk.Name = "lbTextureChunk";
            lbTextureChunk.Padding = new Padding(3);
            lbTextureChunk.Size = new Size(116, 36);
            lbTextureChunk.TabIndex = 0;
            lbTextureChunk.Text = "[Click] Open viewer\r\n[Ctrl+R] Reload";
            // 
            // HelpWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(554, 639);
            Controls.Add(flowLayoutPanel1);
            CornerStyle = CornerPreference.Default;
            ForeColor = Color.Gainsboro;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HelpWindow";
            Text = "Help";
            TransparencyKey = Color.FromArgb(31, 31, 32);
            fraHexViewer.ResumeLayout(false);
            fraHexViewer.PerformLayout();
            fraNSDBox.ResumeLayout(false);
            fraNSDBox.PerformLayout();
            fraSpawns.ResumeLayout(false);
            fraSpawns.PerformLayout();
            fraTextureViewer.ResumeLayout(false);
            fraTextureViewer.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            fraEntity.ResumeLayout(false);
            fraEntity.PerformLayout();
            darkGroupBox2.ResumeLayout(false);
            darkGroupBox2.PerformLayout();
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            fraSavedProperties.ResumeLayout(false);
            fraSavedProperties.PerformLayout();
            fraProperties.ResumeLayout(false);
            fraProperties.PerformLayout();
            fraTextureChunk.ResumeLayout(false);
            fraTextureChunk.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DarkGroupBox fraHexViewer;
        private Label lbHexViewer;
        private DarkGroupBox fraNSDBox;
        private Label lbNSDBox;
        private DarkGroupBox fraSpawns;
        private DarkGroupBox fraTextureViewer;
        private Label lbTextureViewer;
        private FlowLayoutPanel flowLayoutPanel1;
        private DarkGroupBox darkGroupBox1;
        private Label lbProperties;
        private DarkGroupBox fraProperties;
        private DarkGroupBox fraSavedProperties;
        private Label lbSavedProperties;
        private DarkGroupBox fraTextureChunk;
        private Label lbTextureChunk;
        private DarkGroupBox fraEntity;
        private Label lbEntityListBox;
        private DarkGroupBox darkGroupBox2;
    }
}