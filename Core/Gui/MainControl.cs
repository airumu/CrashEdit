using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CrashEdit
{

    public class MainControl : UserControl, IWorkspaceHost, IVerbExecutor
    {

        public MainControl(IUserInterface ui, Controller rootController)
        {
            ArgumentNullException.ThrowIfNull(ui);
            ArgumentNullException.ThrowIfNull(rootController);

            Ui = ui;
            RootController = rootController;
            BackColor = Color.FromArgb(31, 31, 32);

            FilterText = "Default";

            Split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = ClientSize.Width / 100 * 40,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            Controls.Add(Split);

            ResourceTree = new ResourceTreeView(this)
            {
                Dock = DockStyle.Fill,
                RootController = RootController,
                HideSelection = false,
            };
            ResourceTree.SelectedControllerChanged += (sender, e) =>
            {
                _activeController = ResourceTree.SelectedController;
                ResourceBox.ActiveController = _activeController;
                OnActiveControllerChanged(EventArgs.Empty);
            };
            Split.Panel1.Controls.Add(ResourceTree);
            Split.Panel1.BackColor = Color.FromArgb(31, 31, 32);

            ResourceBox = new ResourceBox
            {
                Dock = DockStyle.Fill
            };
            Split.Panel2.Controls.Add(ResourceBox);
            Split.Panel2.BackColor = Color.FromArgb(31, 31, 32);
        }

        public IUserInterface Ui { get; }

        public Controller RootController { get; }

        private Controller? _activeController;

        public Controller? ActiveController
        {
            get { return _activeController; }
            set
            {
                if (_activeController == value)
                    return;

                // This raises an event which sets the field.
                ResourceTree.SelectedController = value;
            }
        }

        public event EventHandler? ActiveControllerChanged;

      

        public string? Filter { get; set; }

        public bool IgnoreFilter { get; set; }

        public bool UseRegex { get; set; }

        public bool IsCaseSensitive { get; set; }

        private string _searchQuery = string.Empty;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (_searchQuery == value)
                    return;

                _searchQuery = value;

                string query = value;

                // Apply filter.
                if (!string.IsNullOrEmpty(Filter) && !IgnoreFilter)
                {
                    if (Filter == "Entity")
                    {
                        query = $@"{Regex.Escape(value)}.*\[ID|.*\[ID {Regex.Escape(value)}";
                    }
                    else if (Filter == "GOOL")
                    {
                        query = $@"GOOLv?\d* \({Regex.Escape(value)}";
                    }
                    else if (Filter == "Texture")
                    {
                        query = $@"Texture Chunk \d* \({Regex.Escape(value)}";
                    }
                    else
                    {
                        query = $@"{Filter} \({Regex.Escape(value)}";
                    }
                    UseRegex = true;
                }
                else
                {
                    UseRegex = false;
                }

                if (string.IsNullOrEmpty(query))
                {
                    SearchPredicate = null;
                }
                else
                {
                    if (UseRegex)
                    {
                        if (IsCaseSensitive)
                        {
                            SearchPredicate = (x =>
                                Regex.IsMatch(x.Text, query));
                        }
                        else
                        {
                            SearchPredicate = (x =>
                                Regex.IsMatch(x.Text, query, RegexOptions.IgnoreCase));
                        }
                    }
                    else
                    {
                        if (IsCaseSensitive)
                        {
                            SearchPredicate = (x =>
                                x.Text.Contains(query));
                        }
                        else
                        {
                            SearchPredicate = (x =>
                                x.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase));
                        }
                    }
                }
            }
        }

        public string FilterText { get; set; }

        public Predicate<Controller>? SearchPredicate { get; private set; }

        public SplitContainer Split { get; }

        public ResourceTreeView ResourceTree { get; }

        public ResourceBox ResourceBox { get; }

        public virtual void Sync()
        {
            RootController.Sync();
            ResourceTree.Sync();
            ResourceBox.Sync();
        }

        public virtual void Kill()
        {
            Controls.Clear();
            RootController.Kill();
            ResourceTree.Dispose();
            ResourceBox.Dispose();
        }

        public void ExecuteVerb(Verb verb)
        {
            ArgumentNullException.ThrowIfNull(verb);

            verb.Execute(Ui);

            Sync();
        }

        public void ExecuteVerbChoice(List<Verb> verbs)
        {
            ArgumentNullException.ThrowIfNull(verbs);

            // Don't bother if there are no choices.
            if (verbs.Count == 0)
                return;

            // TODO let the user choose one
            ExecuteVerb(verbs[0]);
        }

        protected virtual void OnActiveControllerChanged(EventArgs e)
        {
            ActiveControllerChanged?.Invoke(this, e);
        }

    }

}
