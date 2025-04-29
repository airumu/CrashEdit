using AltUI.Forms;
using MetroSet_UI.Controls;

namespace CrashEdit.CE.Forms
{
    public partial class ProgressBarForm : DarkForm
    {
        public ProgressBarForm()
        {
            InitializeComponent();
            uxProgress.Invalidated += uxProgress_Invalidated;
            Shown += ProgressBarForm_Shown;
        }

        private void ProgressBarForm_Shown(object sender, System.EventArgs e)
        {
            IsShown = true;
        }

        private void uxProgress_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (uxProgress.Value == 100)
            {
                Close();
            }
        }

        public bool IsShown { get; set; }
        public MetroSetProgressBar ProgressBar => uxProgress;
    }
}
