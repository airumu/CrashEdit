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
            fraTextureViewer = new DarkGroupBox();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            fraEntity = new DarkGroupBox();
            label6 = new Label();
            darkGroupBox1 = new DarkGroupBox();
            fraSavedProperties = new DarkGroupBox();
            label4 = new Label();
            fraProperties = new DarkGroupBox();
            label3 = new Label();
            fraTextureChunk = new DarkGroupBox();
            label5 = new Label();
            darkGroupBox2 = new DarkGroupBox();
            fraHexViewer.SuspendLayout();
            fraNSDBox.SuspendLayout();
            fraSpawns.SuspendLayout();
            fraTextureViewer.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            fraEntity.SuspendLayout();
            darkGroupBox1.SuspendLayout();
            fraSavedProperties.SuspendLayout();
            fraProperties.SuspendLayout();
            fraTextureChunk.SuspendLayout();
            darkGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // fraHexViewer
            // 
            fraHexViewer.AutoSize = true;
            fraHexViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraHexViewer.BackColor = Color.Transparent;
            fraHexViewer.Controls.Add(lblHexViewer);
            fraHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraHexViewer.Location = new Point(6, 388);
            fraHexViewer.Name = "fraHexViewer";
            fraHexViewer.Size = new Size(252, 238);
            fraHexViewer.TabIndex = 15;
            fraHexViewer.TabStop = false;
            fraHexViewer.Text = "Hex Viewer";
            // 
            // lblHexViewer
            // 
            lblHexViewer.AutoSize = true;
            lblHexViewer.Dock = DockStyle.Fill;
            lblHexViewer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHexViewer.ForeColor = SystemColors.ControlText;
            lblHexViewer.Location = new Point(3, 19);
            lblHexViewer.Margin = new Padding(3);
            lblHexViewer.Name = "lblHexViewer";
            lblHexViewer.Padding = new Padding(3);
            lblHexViewer.Size = new Size(246, 216);
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
            fraSpawns.Controls.Add(lblNSDBox);
            fraSpawns.Font = new Font("Segoe UI", 9F);
            fraSpawns.Location = new Point(6, 22);
            fraSpawns.Name = "fraSpawns";
            fraSpawns.Size = new Size(213, 88);
            fraSpawns.TabIndex = 16;
            fraSpawns.TabStop = false;
            fraSpawns.Text = "Spawn Points";
            // 
            // lblNSDBox
            // 
            lblNSDBox.AutoSize = true;
            lblNSDBox.Dock = DockStyle.Fill;
            lblNSDBox.ForeColor = SystemColors.ControlText;
            lblNSDBox.Location = new Point(3, 19);
            lblNSDBox.Margin = new Padding(3);
            lblNSDBox.Name = "lblNSDBox";
            lblNSDBox.Padding = new Padding(3);
            lblNSDBox.Size = new Size(207, 66);
            lblNSDBox.TabIndex = 0;
            lblNSDBox.Text = "[Mouse drag] Move row\r\n[Right-click] Show context menu\r\n[Ctrl + C] Copy selected spawn point\r\n[Ctrl + V] Paste selected spawn point";
            // 
            // fraTextureViewer
            // 
            fraTextureViewer.AutoSize = true;
            fraTextureViewer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTextureViewer.BackColor = Color.Transparent;
            fraTextureViewer.Controls.Add(label1);
            fraTextureViewer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraTextureViewer.Location = new Point(273, 70);
            fraTextureViewer.Name = "fraTextureViewer";
            fraTextureViewer.Size = new Size(267, 88);
            fraTextureViewer.TabIndex = 15;
            fraTextureViewer.TabStop = false;
            fraTextureViewer.Text = "Texture Viewer";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(3, 19);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Padding = new Padding(3);
            label1.Size = new Size(261, 66);
            label1.TabIndex = 0;
            label1.Text = "[Right-click] Save selected texture region to file\r\n[Ctrl+C] Copy texture to buffer\r\n[Ctrl+X] Cut texture to buffer\r\n[Ctrl+V] Paste texture from buffer";
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
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(3, 19);
            label6.Margin = new Padding(3);
            label6.Name = "label6";
            label6.Padding = new Padding(3);
            label6.Size = new Size(243, 96);
            label6.TabIndex = 0;
            label6.Text = resources.GetString("label6.Text");
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
            fraSavedProperties.Controls.Add(label4);
            fraSavedProperties.Font = new Font("Segoe UI", 9F);
            fraSavedProperties.Location = new Point(6, 83);
            fraSavedProperties.Name = "fraSavedProperties";
            fraSavedProperties.Size = new Size(236, 103);
            fraSavedProperties.TabIndex = 1;
            fraSavedProperties.TabStop = false;
            fraSavedProperties.Text = "Saved Properties";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(3, 19);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Padding = new Padding(3);
            label4.Size = new Size(230, 81);
            label4.TabIndex = 0;
            label4.Text = "[Ctrl+V] Copy fields in selected item\r\n[Double-click / F2] Rename selected item\r\n[Delete] Remove selected items\r\n--------------------------------\r\n[Ctrl+R] Reload list";
            // 
            // fraProperties
            // 
            fraProperties.AutoSize = true;
            fraProperties.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraProperties.Controls.Add(label3);
            fraProperties.Font = new Font("Segoe UI", 9F);
            fraProperties.Location = new Point(6, 22);
            fraProperties.Name = "fraProperties";
            fraProperties.Size = new Size(183, 58);
            fraProperties.TabIndex = 1;
            fraProperties.TabStop = false;
            fraProperties.Text = "Properties";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(3, 19);
            label3.Margin = new Padding(3);
            label3.Name = "label3";
            label3.Padding = new Padding(3);
            label3.Size = new Size(177, 36);
            label3.TabIndex = 0;
            label3.Text = "[Ctrl+C] Save selected fields\r\n[Delete] Remove selected fields";
            // 
            // fraTextureChunk
            // 
            fraTextureChunk.AutoSize = true;
            fraTextureChunk.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fraTextureChunk.BackColor = Color.Transparent;
            fraTextureChunk.Controls.Add(label5);
            fraTextureChunk.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fraTextureChunk.Location = new Point(273, 6);
            fraTextureChunk.Name = "fraTextureChunk";
            fraTextureChunk.Size = new Size(122, 58);
            fraTextureChunk.TabIndex = 15;
            fraTextureChunk.TabStop = false;
            fraTextureChunk.Text = "Texture Chunk";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(3, 19);
            label5.Margin = new Padding(3);
            label5.Name = "label5";
            label5.Padding = new Padding(3);
            label5.Size = new Size(116, 36);
            label5.TabIndex = 0;
            label5.Text = "[Click] Open viewer\r\n[Ctrl+R] Reload";
            // 
            // darkGroupBox2
            // 
            darkGroupBox2.AutoSize = true;
            darkGroupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            darkGroupBox2.Controls.Add(label6);
            darkGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            darkGroupBox2.Location = new Point(6, 22);
            darkGroupBox2.Name = "darkGroupBox2";
            darkGroupBox2.Size = new Size(249, 118);
            darkGroupBox2.TabIndex = 1;
            darkGroupBox2.TabStop = false;
            darkGroupBox2.Text = "(ListBox Controls)";
            // 
            // HelpWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(554, 639);
            Controls.Add(flowLayoutPanel1);
            CornerStyle = CornerPreference.Default;
            ForeColor = Color.Gainsboro;
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
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            fraSavedProperties.ResumeLayout(false);
            fraSavedProperties.PerformLayout();
            fraProperties.ResumeLayout(false);
            fraProperties.PerformLayout();
            fraTextureChunk.ResumeLayout(false);
            fraTextureChunk.PerformLayout();
            darkGroupBox2.ResumeLayout(false);
            darkGroupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DarkGroupBox fraHexViewer;
        private Label lblHexViewer;
        private DarkGroupBox fraNSDBox;
        private Label lblNSDBox;
        private DarkGroupBox fraSpawns;
        private DarkGroupBox fraTextureViewer;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private DarkGroupBox darkGroupBox1;
        private Label label3;
        private DarkGroupBox fraProperties;
        private DarkGroupBox fraSavedProperties;
        private Label label4;
        private DarkGroupBox fraTextureChunk;
        private Label label5;
        private DarkGroupBox fraEntity;
        private Label label6;
        private DarkGroupBox darkGroupBox2;
    }
}