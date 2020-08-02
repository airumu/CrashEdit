using System;
using System.Windows.Forms;
using DarkUI.Forms;

namespace CrashEdit
{
    public partial class InputWindow : DarkForm
    {
        public InputWindow()
        {
            InitializeComponent();

            cmdCancel.Text = Properties.Resources.InputWindow_cmdCancel;
        }

        public string Input => txtInput.Text;

        private void cmdOK_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
