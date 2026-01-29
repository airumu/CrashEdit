using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;

namespace CrashEdit
{

    public sealed class NodeListForm : DarkForm
    {
        private ICommandHost Host { get; }

        private IWorkspaceHost? WsHost => Host.ActiveWorkspaceHost;

        private MainForm? mainForm;

        private DarkComboBox EntryType { get; }
        private DoubleBufferedListBox EntryList { get; }
        private DarkTextBox SearchBox { get; }
        private DarkButton cmdExport { get; }

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
            MinimumSize = new Size(160, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = false;
            MaximizeBox = false;

            TableLayoutPanel OverallTable = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Padding = new Padding(8),
            };

            TableLayoutPanel table1 = new()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
            };
            table1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            table1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));

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
            table1.Controls.Add(EntryType, 0, 0);

            SearchBox = new()
            {
                Enabled = false
            };
            SearchBox.Click += SearchBox_GotFocus;
            SearchBox.GotFocus += SearchBox_GotFocus;
            SearchBox.TextChanged += SearchBox_TextChanged;
            SearchBox.LostFocus += SearchBox_LostFocus;
            SearchBox.KeyDown += Event_KeyDown;
            table1.Controls.Add(SearchBox, 0, 1);

            PictureBox pictureBox = new()
            {
                Size = new Size(16, 16 + 4),
                Image = Embeds.GetIcon("Hint")!.ToBitmap(),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Padding = new Padding(0, 4, 0, 0)
            };
            table1.Controls.Add(pictureBox, 1, 1);

            cmdExport = new()
            {
                Text = "Export",
                Enabled = false
            };
            cmdExport.Click += cmdExport_Click;
            table1.Controls.Add(cmdExport, 1, 0);

            DarkToolTip tip1 = new();
            tip1.SetToolTip(pictureBox, "The filter supports regex.");

            EntryList = new()
            {
                Dock = DockStyle.Fill,
                Size = new Size(100, 400),
                BackColor = Color.FromArgb(31, 31, 32),
                Font = new Font("Cascadia Code SemiLight", 9F, FontStyle.Regular, GraphicsUnit.Point, 0)
            };
            EntryList.SelectedValueChanged += EntryList_SelectedValueChanged;
            EntryList.KeyDown += Event_KeyDown;

            OverallTable.Controls.Add(table1);
            OverallTable.Controls.Add(EntryList);
            Controls.Add(OverallTable);
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

            cmdExport.Enabled = type != "Entity";

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

        private static readonly Dictionary<string, Type> EntryTypeMap = new()
        {
            ["Zone"] = typeof(ZoneEntry),
            ["Scenery"] = typeof(SceneryEntry),
            ["Sort List"] = typeof(SLSTEntry),
            ["Model"] = typeof(ModelEntry),
            ["Animation"] = typeof(AnimationEntry),
            ["GOOL"] = typeof(GOOLEntry),
            ["Music"] = typeof(MusicEntry),
            ["Sound"] = typeof(SoundEntry),
            ["Texture"] = typeof(TextureChunk),
        };

        private static dynamic GetEntryType(string type, object rsc)
        {
            if (!EntryTypeMap.TryGetValue(type, out var t))
                throw new NotImplementedException(type);

            return rsc.GetType() == t ? rsc : null;
        }

        private static dynamic GetEntryList(string type)
        {
            if (!EntryTypeMap.TryGetValue(type, out var t))
                throw new NotImplementedException(type);

            var listType = typeof(List<>).MakeGenericType(t);
            return Activator.CreateInstance(listType);
        }

        private void cmdExport_Click(object? sender, EventArgs e)
        {
            string? type = EntryType.SelectedItem == null ? string.Empty : EntryType.SelectedItem.ToString();
            if (type == null) return;

            var entries = GetEntryList(type);

            if (Host.ActiveWorkspaceHost is MainControl mainCtl)
            {
                string currentQuary = mainCtl.SearchQuery;
                mainCtl.IgnoreFilter =
                mainCtl.UseRegex = true;

                foreach (var item in EntryList.Items)
                {
                    mainCtl.SearchQuery = "Workspace";
                    string query = item.ToString() == null ? string.Empty : Regex.Escape(item.ToString()!);

                    var w = new Walker();
                    w.Cursor = WsHost.RootController;

                    // Get Entry list.
                    while (w.MoveToLastChild()) { }
                    while (!WsHost.SearchPredicate!(w.Cursor))
                    {
                        if (Regex.IsMatch(w.Cursor.Text, query))
                        {
                            entries.Add(GetEntryType(type, w.Cursor.Resource));
                        }
                        if (!w.MoveToPreviousDFS())
                        {
                            dirty.Pop();
                            return;
                        }
                    }
                    if (Regex.IsMatch(w.Cursor.Text, query)) // Add the last one.
                    {
                        entries.Add(GetEntryType(type, w.Cursor.Resource));
                    }
                }

                mainCtl.IgnoreFilter =
                mainCtl.UseRegex = false;
                mainCtl.SearchQuery = currentQuary;
            }

            if (entries.Count > 0)
            {
                using FolderBrowserDialog fbd = new()
                {
                    ShowNewFolderButton = true
                };
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = fbd.SelectedPath;
                    foreach (var ent in entries)
                    {
                        if (type == "Texture")
                        {
                            string fileName = $"{type}_{Entry.EIDToEName(ent.EID)}.nschunk";
                            string filePath = Path.Combine(folderPath, fileName);
                            File.WriteAllBytes(filePath, ent.Data);
                        }
                        else
                        {
                            string fileName = $"{type}_{Entry.EIDToEName(ent.EID)}.nsentry";
                            string filePath = Path.Combine(folderPath, fileName);
                            File.WriteAllBytes(filePath, ent.Save());
                        }
                    }
                    Console.WriteLine($"Exported {entries.Count} entries to {folderPath}.");
                }
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