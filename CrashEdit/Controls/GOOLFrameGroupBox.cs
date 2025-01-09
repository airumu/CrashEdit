using CrashEdit.Crash;

namespace CrashEdit.CE
{
    public partial class GOOLFrameGroupBox : UserControl
    {
        GOOLEntryController controller;

        GOOLEntry goolentry;

        private Panel panel;
        private DataGridView dgvFrameGroup;

        public GOOLFrameGroupBox(GOOLEntryController controller)
        {
            this.controller = controller;
            goolentry = controller.GOOLEntry;
            InitializeComponent();
        }

        public void OnTabSelected()
        {
            GOOLFrameGroupBox_Enter(this, EventArgs.Empty);
        }

        private void GOOLFrameGroupBox_Enter(object sender, EventArgs e)
        {
            dgvFrameGroup = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ColumnHeadersHeight = 24,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowHeadersWidth = 24,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                ScrollBars = ScrollBars.Vertical
            };
            EnableDoubleBuffering();
            SetDarkTheme(dgvFrameGroup);

            //dgvFrameGroup.CellEndEdit += dgvFrameGroup_CellEndEdit;
            //dgvFrameGroup.CellValidating += dgvFrameGroup_CellValidating;
            //dgvFrameGroup.CellValueChanged += dgvFrameGroup_CellValueChanged;
            //dgvFrameGroup.EditingControlShowing += dgvFrameGroup_EditingControlShowing;
            //dgvFrameGroup.SelectionChanged += dgvFrameGroup_SelectionChanged;

            CreateColumns();
            CreateRows();
            AdjustColumnwidth();

            Controls.Add(dgvFrameGroup);
        }

        private void CreateColumns()
        {
            dgvFrameGroup.Columns.Add("Index", "Index");
            dgvFrameGroup.Columns.Add("IndexAlt", "Index (Alt)");
            dgvFrameGroup.Columns.Add("FrameCount", "Frames");
            dgvFrameGroup.Columns.Add("EID", "EID");
            dgvFrameGroup.Columns.Add("Interpolated", "Interpolated");
        }

        private void AdjustColumnwidth()
        {
            foreach (DataGridViewColumn column in dgvFrameGroup.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 80;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void GetIndex(int index,  out string index1, out string index2)
        {
            index1 = string.Format("{0:X}", NumberExt.TransformedString(index));
            index2 = string.Format("0x{0:X}00", index.ToString("X"));
        }

        private void CreateRows()
        {
            foreach (var group in goolentry.FrameGroups)
            {
                if (group is VertexGroup2)
                {
                    var vgroup = (VertexGroup2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is VertexGroup3to2)
                {
                    var vgroup = (VertexGroup3to2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), vgroup.Interpolated);
                    dgvFrameGroup.Rows.Add(row);
                }
                else if (group is SpriteGroup2)
                {
                    var vgroup = (SpriteGroup2)group;
                    DataGridViewRow row = new DataGridViewRow();

                    GetIndex(vgroup.Index / 4, out string index1, out string index2);

                    row.CreateCells(dgvFrameGroup, index1, index2, vgroup.FrameCount, Entry.EIDToEName(vgroup.EID), "-");
                    dgvFrameGroup.Rows.Add(row);
                }
            }
        }


        private void EnableDoubleBuffering()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvFrameGroup, new object[] { true });
        }

        private void SetDarkTheme(DataGridView dataGridView)
        {
            Color clrBackground = Color.FromArgb(40, 40, 40);
            Color clrAltBackground = Color.FromArgb(34, 34, 34);
            Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
            Color clrText = Color.Gainsboro;

            // Background color of the entire grid
            dataGridView.BackgroundColor = Color.FromArgb(31, 31, 32);

            // Color of the grid lines
            dataGridView.GridColor = Color.FromArgb(50, 50, 50);

            // Default style for cells
            dataGridView.DefaultCellStyle.BackColor = clrBackground;
            dataGridView.DefaultCellStyle.ForeColor = clrText;
            dataGridView.DefaultCellStyle.SelectionBackColor = clrSelectionBackground;
            dataGridView.DefaultCellStyle.SelectionForeColor = clrText;

            // Style for column headers
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = clrText;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Style for row headers
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = clrText;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 60, 60);
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = clrText;

            // Background color for odd and even rows
            dataGridView.RowsDefaultCellStyle.BackColor = clrBackground;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = clrAltBackground;

            // Row border style
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header and gridline styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Additional settings
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
    }
}