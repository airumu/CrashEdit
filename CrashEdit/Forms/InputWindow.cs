using AltUI.Forms;

namespace CrashEdit.CE
{
    public partial class InputWindow : DarkForm
    {
        public InputWindow(string labelText, string caption, string curText)
        {
            InitializeComponent();
            panel2.Visible = false;
            tblPanel.RowStyles[1].Height = 0;
            ClientSize = tblPanel.PreferredSize;

            lblInput1.Text = labelText;
            Text = caption;
            txtInput1.Text = curText;
            cmdCancel.Text = Properties.Resources.InputWindow_cmdCancel;
        }

        public InputWindow(string labelText, string caption, string curText, int maxLength) : this(labelText, caption, curText)
        {
            txtInput1.MaxLength = maxLength;
        }

        public InputWindow(string labelText, string caption, string curText, string labelText2, string curText2) : this(labelText, caption, curText)
        {
            panel2.Visible = true;
            tblPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 50F);
            ClientSize = tblPanel.PreferredSize;

            lblInput2.Text = labelText2;
            txtInput2.Text = curText2;
        }

        public InputWindow(string labelText, string caption, string curText, int maxLength, string labelText2, string curText2, int maxLength2) : this(labelText, caption, curText, labelText2, curText2)
        {
            txtInput1.MaxLength = maxLength;
            txtInput2.MaxLength = maxLength2;
        }

        public string Input => txtInput1.Text;
        public string Input2 => txtInput2.Text;

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
