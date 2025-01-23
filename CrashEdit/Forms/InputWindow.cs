using AltUI.Forms;

namespace CrashEdit.CE
{
    public partial class InputWindow : DarkForm
    {
        public InputWindow(string labelText, string caption, string curText)
        {
            InitializeComponent();

            lblInput.Text = labelText;
            Text = caption;
            txtInput.Text = curText;
            cmdCancel.Text = Properties.Resources.InputWindow_cmdCancel;
        }

        public string Input => txtInput.Text;

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
