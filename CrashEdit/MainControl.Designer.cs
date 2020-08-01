namespace CrashEdit
{
    partial class MainControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uxSplit = new System.Windows.Forms.SplitContainer();
            this.uxTree = new System.Windows.Forms.TreeView();
            this.uxImageList = new System.Windows.Forms.ImageList(this.components);
            this.pnError = new System.Windows.Forms.Panel();
            this.txtException = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uxSplit)).BeginInit();
            this.uxSplit.Panel1.SuspendLayout();
            this.uxSplit.Panel2.SuspendLayout();
            this.uxSplit.SuspendLayout();
            this.pnError.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxSplit
            // 
            this.uxSplit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.uxSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSplit.ForeColor = System.Drawing.SystemColors.Control;
            this.uxSplit.Location = new System.Drawing.Point(0, 0);
            this.uxSplit.Name = "uxSplit";
            // 
            // uxSplit.Panel1
            // 
            this.uxSplit.Panel1.Controls.Add(this.uxTree);
            // 
            // uxSplit.Panel2
            // 
            this.uxSplit.Panel2.Controls.Add(this.pnError);
            this.uxSplit.Size = new System.Drawing.Size(512, 319);
            this.uxSplit.SplitterDistance = 170;
            this.uxSplit.TabIndex = 0;
            // 
            // uxTree
            // 
            this.uxTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.uxTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxTree.ForeColor = System.Drawing.SystemColors.Control;
            this.uxTree.HideSelection = false;
            this.uxTree.ImageIndex = 0;
            this.uxTree.ImageList = this.uxImageList;
            this.uxTree.Location = new System.Drawing.Point(0, 0);
            this.uxTree.Name = "uxTree";
            this.uxTree.PathSeparator = " -> ";
            this.uxTree.SelectedImageIndex = 0;
            this.uxTree.Size = new System.Drawing.Size(170, 319);
            this.uxTree.TabIndex = 0;
            this.uxTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uxTree_AfterSelect);
            // 
            // uxImageList
            // 
            this.uxImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.uxImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.uxImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnError
            // 
            this.pnError.Controls.Add(this.txtException);
            this.pnError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnError.Location = new System.Drawing.Point(0, 0);
            this.pnError.Name = "pnError";
            this.pnError.Size = new System.Drawing.Size(338, 319);
            this.pnError.TabIndex = 0;
            this.pnError.Visible = false;
            // 
            // txtException
            // 
            this.txtException.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtException.ForeColor = System.Drawing.SystemColors.Control;
            this.txtException.Location = new System.Drawing.Point(0, 0);
            this.txtException.Multiline = true;
            this.txtException.Name = "txtException";
            this.txtException.ReadOnly = true;
            this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtException.Size = new System.Drawing.Size(338, 319);
            this.txtException.TabIndex = 0;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.uxSplit);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(512, 319);
            this.uxSplit.Panel1.ResumeLayout(false);
            this.uxSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxSplit)).EndInit();
            this.uxSplit.ResumeLayout(false);
            this.pnError.ResumeLayout(false);
            this.pnError.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer uxSplit;
        private System.Windows.Forms.TreeView uxTree;
        private System.Windows.Forms.ImageList uxImageList;
        private System.Windows.Forms.Panel pnError;
        private System.Windows.Forms.TextBox txtException;
    }
}
