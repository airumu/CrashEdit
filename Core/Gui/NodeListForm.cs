using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Forms;
using AltUI.Controls;
using AltUI.Forms;

namespace CrashEdit
{

    public sealed class NodeListForm : DarkForm
    {
        private ICommandHost Host { get; }

        private IWorkspaceHost? WsHost => Host.ActiveWorkspaceHost;

        private MainForm? mainForm;

        private TableLayoutPanel OverallTable { get; }
        private DarkComboBox EntryType { get; }
        private DoubleBufferedListBox EntryList { get; }
        private DarkTextBox SearchBox { get; }

        private List<string>? originalItems;
        private bool mouseClicked;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public NodeListForm(ICommandHost host)
        {
            Host = host;
            mainForm = (MainForm?)host;

            //Text = mainForm?.TabControl.SelectedTab?.Text;
            Text = "Node List";
            Icon = Embeds.GetIcon("List");
            MinimumSize = new Size(140, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            MaximizeBox = false;

            OverallTable = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Padding = new Padding(12),
            };
            Controls.Add(OverallTable);

            EntryType = new()
            {
                Width = 100,
                Items = { "Entity", "Zone", "Scenery", "Sort List", "Model", "Animation", "GOOL", "Music", "Sound", "Texture" },
                DropDownHeight = 200,
            };
            EntryType.Click += EntryType_Click;
            EntryType.DropDownClosed += EntryType_DropDownClosed;
            EntryType.SelectedValueChanged += EntryType_SelectedValueChanged;
            EntryType.KeyDown += Event_KeyDown;
            OverallTable.Controls.Add(EntryType);

            SearchBox = new()
            {
                Enabled = false
            };
            SearchBox.Click += SearchBox_GotFocus;
            SearchBox.GotFocus += SearchBox_GotFocus;
            SearchBox.TextChanged += SearchBox_TextChanged;
            SearchBox.LostFocus += SearchBox_LostFocus;
            SearchBox.KeyDown += Event_KeyDown;
            OverallTable.Controls.Add(SearchBox);

            EntryList = new()
            {
                Dock = DockStyle.Fill,
                Size = new Size(100, 400),
                BackColor = Color.FromArgb(31, 31, 32),
                Font = new Font("Cascadia Code SemiLight", 9F, FontStyle.Regular, GraphicsUnit.Point, 0)
            };
            EntryList.SelectedValueChanged += EntryList_SelectedValueChanged;
            EntryList.KeyDown += Event_KeyDown;
            OverallTable.Controls.Add(EntryList);
        }

        private void Event_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {
                SearchBox.Focus();
            }
        }

        private void EntryType_Click(object? sender, EventArgs e)
        {
            EntryList.ForeColor = Color.DimGray;
        }

        private void EntryType_DropDownClosed(object? sender, EventArgs e)
        {
            EntryList.ForeColor = Color.Gainsboro;
        }

        private void AddListItems(List<string> items, bool ascending)
        {
            if (ascending)
                items.Sort();
            else
                items.Sort((a, b) => b.CompareTo(a));

            EntryList.Items.AddRange(items.ToArray());
        }

        static string ExtractEName(string text)
        {
            Match match = Regex.Match(text, @"\(([^)]+)\)");
            return match.Success ? match.Groups[1].Value : "";
        }

        private void EntryType_SelectedValueChanged(object? sender, EventArgs e)
        {
            originalItems = new List<string>();
            EntryList.Items.Clear();
            SearchBox.Text = string.Empty;
            string? type = EntryType.SelectedItem == null ? string.Empty : EntryType.SelectedItem.ToString();
            if (string.IsNullOrEmpty(type)) return;

            dirty.Push(true);

            if (!SearchBox.Enabled)
                SearchBox.Enabled = true;

            string query = string.Empty;
            if (type == "Entity")
            {
                query = @"^.*\[ID";
            }
            else if (type == "GOOL")
            {
                query = @"GOOLv?\d* \(";
            }
            else if (type == "Texture")
            {
                query = @"Texture Chunk \d* \(";
            }
            else
            {
                query = $@"{type} \(";
            }

            // Start from the last (by depth-first) controller, to the root controller.
            if (Host.ActiveWorkspaceHost is MainControl mainCtl)
            {
                string currentQuary = mainCtl.SearchQuery;
                mainCtl.IgnoreFilter =
                mainCtl.UseRegex = true;

                mainCtl.SearchQuery = "Workspace";

                var w = new Walker();
                w.Cursor = WsHost.RootController;

                // Get Entry list.
                while (w.MoveToLastChild()) { }
                while (!WsHost.SearchPredicate!(w.Cursor))
                {
                    if (Regex.IsMatch(w.Cursor.Text, query))
                    {
                        originalItems.Add(type == "Entity" ? w.Cursor.Text : ExtractEName(w.Cursor.Text));
                    }
                    if (!w.MoveToPreviousDFS())
                    {
                        dirty.Pop();
                        return;
                    }
                }
                if (Regex.IsMatch(w.Cursor.Text, query)) // Add the last one.
                {
                    originalItems.Add(type == "Entity" ? w.Cursor.Text : ExtractEName(w.Cursor.Text));
                }

                AddListItems(originalItems, ascending: true);

                mainCtl.IgnoreFilter = 
                mainCtl.UseRegex = false;
                mainCtl.SearchQuery = currentQuary;
            }

            dirty.Pop();
        }

        private void SearchBox_GotFocus(object? sender, EventArgs e)
        {
            if (!mouseClicked)
            {
                SearchBox.SelectAll();
                mouseClicked = true;
            }
        }

        private void SearchBox_TextChanged(object? sender, EventArgs e)
        {
            if (Dirty) return;

            string query = SearchBox.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                EntryList.Items.Clear();
                EntryList.Items.AddRange(originalItems.ToArray());
                return;
            }

            try
            {
                Regex regex = new Regex(query, RegexOptions.IgnoreCase);
                var filteredItems = originalItems.Where(item => regex.IsMatch(item)).ToArray();

                EntryList.Items.Clear();
                EntryList.Items.AddRange(filteredItems);
            }
            catch (RegexParseException)
            {
                return;
            }
        }

        private void SearchBox_LostFocus(object? sender, EventArgs e)
        {
            mouseClicked = false;
        }

        private void EntryList_SelectedValueChanged(object? sender, EventArgs e)
        {
            if (Dirty || EntryList.SelectedItem == null) return;

            if (Host.ActiveWorkspaceHost is MainControl mainCtl)
            {
                string currentQuary = mainCtl.SearchQuery;
                mainCtl.IgnoreFilter =
                mainCtl.IsCaseSensitive = true;

                mainCtl.SearchQuery = EntryList.SelectedItem.ToString()!;

                var w = new Walker();
                w.Cursor = WsHost.RootController;

                // Search selected entry.
                while (w.MoveToLastChild()) { }
                while (!WsHost.SearchPredicate!(w.Cursor))
                {
                    if (!w.MoveToPreviousDFS()) return;
                }

                mainCtl.IgnoreFilter =
                mainCtl.IsCaseSensitive = false;
                mainCtl.SearchQuery = currentQuary;
                WsHost.ActiveController = w.Cursor;
            }
        }

    }

    public class DoubleBufferedListBox : DarkListBox
    {
        public DoubleBufferedListBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }
}