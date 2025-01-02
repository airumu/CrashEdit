using System.Drawing;
using System.Runtime;
using System.Windows.Forms;
using MetroSet_UI.Controls;

namespace CrashEdit
{

    public class ResourcePanel : UserControl
    {

        public ResourcePanel(Controller ctlr)
        {
            ArgumentNullException.ThrowIfNull(ctlr);

            Controller = ctlr;

            TabControl = new MetroSetTabControl
            {
                BackgroundColor = Color.FromArgb(31, 31, 32),
                Dock = DockStyle.Fill,
                IsDerivedStyle = false,
                ItemSize = new Size(100, 28),
                Style = MetroSet_UI.Enums.Style.Dark,
                TabStyle = MetroSet_UI.Enums.TabStyle.Style2,
            };
            Controls.Add(TabControl);

            Editors = Editor.AllEditors
                .Where(x => x.ApplicableForSubject(ctlr))
                .Select(x => (Editor)Activator.CreateInstance(x.GetType()))
                .ToList();

            if (Editors.Count == 0)
            {
                // No editors available for this resource.
                TabControl.TabPages.Add("None");
                TabControl.TabPages[0].Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(31, 31, 32),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "No editors are available for this resource."
                });
                return;
            }

            foreach (var editor in Editors)
            {
                editor.Initialize(ctlr);
                editor.Control.Dock = DockStyle.Fill;

                var tabPage = new TabPage();
                tabPage.Tag = editor;
                tabPage.Text = editor.Text;
                tabPage.Controls.Add(editor.Control);
                TabControl.TabPages.Add(tabPage);
            }
        }

        public bool IsEmpty => Editors.Count == 0;

        public Controller Controller { get; }

        public List<Editor> Editors { get; }

        public MetroSetTabControl TabControl { get; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var editor in Editors)
                {
                    editor.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    }

}
