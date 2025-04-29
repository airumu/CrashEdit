namespace CrashEdit.CE
{
    public sealed class DoubleBufferedControls { }

    public class DoubleBufferedDataGridView
    {
        public static void Initialize(DataGridView dataGridView)
        {
            EnableDoubleBuffer(dataGridView);

            Color clrText = Color.Gainsboro;
            Color clrDefaultBackGround = Color.FromArgb(31, 31, 32);
            Color clrBackground = Color.FromArgb(40, 40, 40);
            Color clrAltBackground = Color.FromArgb(34, 34, 34);
            Color clrSelectionBackground = Color.FromArgb(70, 70, 70);
            Color clrGrid = Color.FromArgb(50, 50, 50);
            DrawControl(dataGridView, clrText, clrDefaultBackGround, clrBackground, clrAltBackground, clrSelectionBackground, clrGrid);
        }

        public static void Initialize_NoAltColor(DataGridView dataGridView)
        {
            EnableDoubleBuffer(dataGridView);

            Color clrText = Color.FromArgb(220, 220, 240);
            Color clrBackground = Color.FromArgb(31, 31, 40);
            Color clrSelectionBackground = Color.FromArgb(54, 54, 70);
            Color clrDefaultBackGround = clrBackground;
            Color clrAltBackground = clrBackground;
            Color clrGrid = clrBackground;
            DrawControl(dataGridView, clrText, clrDefaultBackGround, clrBackground, clrAltBackground, clrSelectionBackground, clrGrid);
        }

        private static void EnableDoubleBuffer(DataGridView dataGridView)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dataGridView, new object[] { true });
        }

        public static void DrawControl(DataGridView dataGridView, Color clrText, Color clrDefaultBackGround, Color clrBackground, Color clrAltBackground, Color clrSelectionBackground, Color clrGrid)
        {
            // Background color of the entire grid
            dataGridView.BackgroundColor = clrDefaultBackGround;

            // Color of the grid lines
            dataGridView.GridColor = clrGrid;

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
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

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

    public class DoubleBufferedListView : ListView
    {
        public DoubleBufferedListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }
}
