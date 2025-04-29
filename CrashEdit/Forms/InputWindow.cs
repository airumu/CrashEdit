using AltUI.Forms;

namespace CrashEdit.CE
{
    public partial class InputWindow : DarkForm
    {
        public InputWindow(string caption, string icon, string labelText, string curText, int maxLength)
        {
            InitializeComponent();
            Text = caption;
            if (!string.IsNullOrEmpty(icon))
            {
                Icon = Embeds.GetIcon(icon);
            }
            cmdCancel.Text = Properties.Resources.InputWindow_cmdCancel;

            panel2.Visible = false;
            tblPanel.RowStyles[1].Height = 0;
            ClientSize = tblPanel.PreferredSize;

            lblInput1.Text = labelText;
            txtInput1.Text = curText;
            if (maxLength > 0 ) txtInput1.MaxLength = maxLength;
        }

        public InputWindow(string caption, string icon, string labelText, string curText, int maxLength, string labelText2, string curText2, int maxLength2) : this(caption, icon, labelText, curText, maxLength)
        {
            panel2.Visible = true;
            tblPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 50F);
            ClientSize = tblPanel.PreferredSize;

            lblInput2.Text = labelText2;
            txtInput2.Text = curText2;
            if (maxLength2 > 0) txtInput2.MaxLength = maxLength2;
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
