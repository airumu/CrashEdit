using System.Windows.Forms;

namespace CrashEdit.CE.Controls
{
    partial class CLUTBox
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            grdCLUT = new DataGridView();
            cmdLoadCLUT = new AltUI.Controls.DarkButton();
            numLoadClut = new AltUI.Controls.DarkNumericUpDown();
            lblCount = new Label();
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            ((System.ComponentModel.ISupportInitialize)grdCLUT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).BeginInit();
            SuspendLayout();
            // 
            // grdCLUT
            // 
            grdCLUT.AllowUserToAddRows = false;
            grdCLUT.AllowUserToDeleteRows = false;
            grdCLUT.AllowUserToResizeColumns = false;
            grdCLUT.AllowUserToResizeRows = false;
            grdCLUT.ColumnHeadersHeight = 24;
            grdCLUT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grdCLUT.Location = new Point(0, 0);
            grdCLUT.Name = "grdCLUT";
            grdCLUT.ReadOnly = true;
            grdCLUT.RowHeadersWidth = 24;
            grdCLUT.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            grdCLUT.ScrollBars = ScrollBars.Vertical;
            grdCLUT.Size = new Size(538, 600);
            grdCLUT.TabIndex = 0;
            grdCLUT.CellPainting += grdCLUT_CellPainting;
            grdCLUT.SelectionChanged += grdCLUT_SelectionChanged;
            grdCLUT.KeyDown += grdCLUT_KeyDown;
            // 
            // cmdLoadCLUT
            // 
            cmdLoadCLUT.BorderColour = Color.Empty;
            cmdLoadCLUT.CustomColour = false;
            cmdLoadCLUT.FlatBottom = false;
            cmdLoadCLUT.FlatTop = false;
            cmdLoadCLUT.Location = new Point(543, 56);
            cmdLoadCLUT.Name = "cmdLoadCLUT";
            cmdLoadCLUT.Padding = new Padding(5);
            cmdLoadCLUT.Size = new Size(75, 23);
            cmdLoadCLUT.TabIndex = 1;
            cmdLoadCLUT.Text = "Load";
            cmdLoadCLUT.Click += cmdLoadCLUT_Click;
            // 
            // numLoadClut
            // 
            numLoadClut.Location = new Point(543, 27);
            numLoadClut.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            numLoadClut.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numLoadClut.Name = "numLoadClut";
            numLoadClut.Size = new Size(75, 23);
            numLoadClut.TabIndex = 2;
            numLoadClut.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new Point(544, 3);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(40, 15);
            lblCount.TabIndex = 3;
            lblCount.Text = "Count";
            // 
            // colorEditor
            // 
            colorEditor.Color = Color.FromArgb(0, 0, 0);
            colorEditor.Enabled = false;
            colorEditor.Location = new Point(543, 85);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Padding = new Padding(9);
            colorEditor.ShowAlphaChannel = false;
            colorEditor.ShowColorSpaceLabels = false;
            colorEditor.Size = new Size(284, 197);
            colorEditor.TabIndex = 0;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // CLUTBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 32);
            Controls.Add(lblCount);
            Controls.Add(numLoadClut);
            Controls.Add(cmdLoadCLUT);
            Controls.Add(grdCLUT);
            Controls.Add(colorEditor);
            Name = "CLUTBox";
            Size = new Size(1000, 1000);
            ((System.ComponentModel.ISupportInitialize)grdCLUT).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLoadClut).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView grdCLUT;
        private AltUI.Controls.DarkButton cmdLoadCLUT;
        private AltUI.Controls.DarkNumericUpDown numLoadClut;
        private Label lblCount;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
    }
}
