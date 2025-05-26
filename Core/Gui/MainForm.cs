using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AltUI.Config;
using AltUI.Forms;

namespace CrashEdit
{

    public abstract class MainForm : DarkForm, ICommandHost
    {
        public MainForm()
        {
            TabControl = new FlatTabControl
            {
                Dock = DockStyle.Fill,
                ItemSize = new Size(100, 24),
                SizeMode = TabSizeMode.FillToRight,
                Padding = new Point(0, 0),
                DrawMode = TabDrawMode.OwnerDrawFixed,
                ShowTabCloseButton = false,
                SelectedForeColor = Color.WhiteSmoke,
                BackColor = Color.FromArgb(31, 31, 32)
            };
            AdjustTabWidth(TabControl);

            TabControl.SelectedIndexChanged += (sender, e) =>
            {
                OnResyncSuggested(EventArgs.Empty);
            };
            Controls.Add(TabControl);

            // Toolbar
            ToolStrip = new ToolStrip
            {
                ImageList = Embeds.ImageList
            };
            Controls.Add(ToolStrip);            

            // Right-side toolbar items below -- they must be added in
            // reverse order (i.e. right to left) !

            // Toolbar -> Find Last
            ToolStrip.Items.Add(new ToolStripCommandButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Command = new FindLastCommand(this)
            });

            // Toolbar -> Find Next
            ToolStrip.Items.Add(new ToolStripCommandButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Command = new FindNextCommand(this)
            });

            // Toolbar -> Find Previous
            ToolStrip.Items.Add(new ToolStripCommandButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Command = new FindPreviousCommand(this)
            });

            // Toolbar -> Find First
            ToolStrip.Items.Add(new ToolStripCommandButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Command = new FindFirstCommand(this)
            });

            // Toolbar -> Search box
            SearchBox = new ToolStripTextBox
            {
                Alignment = ToolStripItemAlignment.Right,
                Enabled = true
            };
            SearchBox.TextChanged += (sender, e) =>
            {
                if (ActiveWorkspaceHost is MainControl mainCtl)
                {
                    if (mainCtl.SearchQuery != SearchBox.Text)
                    {
                        mainCtl.SearchQuery = SearchBox.Text;
                        OnResyncSuggested(EventArgs.Empty);
                    }
                }
            };
            SearchBox.KeyPress += (sender, e) =>
            {
                if (ActiveWorkspaceHost is not MainControl mainCtl_) e.Handled = e.KeyChar != (char)Keys.Delete;
                if (e.KeyChar == '\r')
                {
                    // Start a search if the user pressed enter, if valid.
                    var findFirst = new FindFirstCommand(this);
                    if (findFirst.Ready)
                    {
                        e.Handled = true;
                        if (findFirst.Execute())
                        {
                            // Select the tree view after successful search.
                            if (ActiveWorkspaceHost is MainControl mainCtl)
                            {
                                mainCtl.ResourceTree.Focus();
                            }
                        }
                        else
                        {
                            // Reselect the search field otherwise.
                            SearchBox.Focus();
                            SearchBox.SelectAll();
                        }
                    }
                }
            };
            SearchBox.Enter += (sender, e) => { if (ActiveWorkspaceHost is not MainControl mainCtl_) TabControl.Focus(); };
            ToolStrip.Items.Add(SearchBox);

            // Toolbar -> Find (label and icon)
            ToolStrip.Items.Add(new ToolStripLabel
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Text = "Find",
                ImageKey = "Find"
            });

            // Toolbar -> Search Filter
            SearchFilter = new ToolStripDropDownButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                ImageKey = "Filter",
                Padding = new Padding(4, 0, 0, 0),
                Margin = new Padding(0, 0, 2, 0),
            };
            foreach (var (name, iconKey) in FilterList)
            {
                var item = new ToolStripMenuItem(name, Embeds.GetIcon(iconKey)?.ToBitmap())
                {
                    CheckOnClick = true,
                    Checked = name == "Default"
                };

                item.Click += (sender, e) =>
                {
                    var clickedItem = sender as ToolStripMenuItem;
                    if (clickedItem != null)
                    {
                        foreach (ToolStripMenuItem menuItem in clickedItem.GetCurrentParent().Items)
                        {
                            if (menuItem is ToolStripMenuItem item)
                            {
                                // Uncheck all items.
                                item.Checked = false;
                                item.Font = new Font(SearchFilter.Font, FontStyle.Regular);
                            }
                        }
                        // Check selected item.
                        clickedItem.Checked = true;
                        clickedItem.Font = new Font(SearchFilter.Font, FontStyle.Bold);
                    }
                };
                SearchFilter.DropDownItems.Add(item);
            }
            SearchFilter.DropDownItemClicked += (sender, e) =>
            {
                string item = e.ClickedItem?.Text ?? "Default";
                if (ActiveWorkspaceHost is MainControl mainCtl)
                {
                    mainCtl.Filter = item == "Default" ? string.Empty : item;
                    SearchFilter.ImageKey = FilterList.ContainsKey(item) ? FilterList[item] : "Filter";

                    // First update the SearchBox with the fake string, then update the SearchFilter image again.
                    // This is necessary to enable the Find commands.
                    if (string.IsNullOrEmpty(SearchBox.Text))
                    {
                        SearchBox.Text = "\u00A0";
                        SearchBox.Text = string.Empty;
                    }
                    mainCtl.Filter = item == "Default" ? string.Empty : item;
                    SearchFilter.ImageKey = FilterList.ContainsKey(item) ? FilterList[item] : "Filter";

                    // Update SearchQuery with the fake string.
                    mainCtl.SearchQuery = "\u00A0";
                    mainCtl.SearchQuery = SearchBox.Text;
                    mainCtl.FilterText = item;
                }
            };
            ToolStrip.Items.Add(SearchFilter);

            // EntryList
            EntryList = new ToolStripButton
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Text = "Node List",
                ImageKey = "List"
            };
            EntryList.Click += (sender, e) =>
            {
                if (frmEntryList == null || frmEntryList.IsDisposed)
                {
                    frmEntryList = new NodeListForm(this);
                    frmEntryList.FormClosing += (object? sender, FormClosingEventArgs e) =>
                    {
                        frmEntryList = null;
                    };
                }
                if (!frmEntryList.Visible)
                    frmEntryList.Show();
                else
                    frmEntryList.Activate();
            };
            ToolStrip.Items.Add(EntryList);

            // Menubar
            MenuStrip = new MenuStrip
            {
                ImageList = Embeds.ImageList,
                Visible = false
            };
            Controls.Add(MenuStrip);

            // Menubar -> File
            FileMenu = new ToolStripMenuItem
            {
                Text = "&File"
            };
            FileMenu.DropDown.ImageList = Embeds.ImageList;
            MenuStrip.Items.Add(FileMenu);

            // Menubar -> File -> Exit
            var exitMenuItem = new ToolStripMenuItem
            {
                Text = "&Exit"
            };
            exitMenuItem.Click += (sender, e) =>
            {
                Application.Exit();
            };
            FileMenu.DropDownItems.Add(exitMenuItem);

            // Menubar -> Edit
            EditMenu = new ToolStripMenuItem
            {
                Text = "&Edit"
            };
            EditMenu.DropDown.ImageList = Embeds.ImageList;
            MenuStrip.Items.Add(EditMenu);

            // Menubar -> Edit -> Find
            var findMenuItem = new ToolStripMenuItem
            {
                Text = "&Find",
                ImageKey = "Find",
                ShortcutKeys = Keys.Control | Keys.F
            };
            findMenuItem.Click += (sender, e) =>
            {
                SearchBox.Focus();
                SearchBox.SelectAll();
            };
            EditMenu.DropDownItems.Add(findMenuItem);

            // Menubar -> Edit -> Find Next
            EditMenu.DropDownItems.Add(new ToolStripCommandMenuItem
            {
                Command = new FindNextCommand(this),
                ShortcutKeys = Keys.F3
            });

            // Menubar -> Edit -> Find Previous
            EditMenu.DropDownItems.Add(new ToolStripCommandMenuItem
            {
                Command = new FindPreviousCommand(this),
                ShortcutKeys = Keys.Shift | Keys.F3
            });

            // Menubar -> View
            ViewMenu = new ToolStripMenuItem
            {
                Text = "&View"
            };
            ViewMenu.DropDown.ImageList = Embeds.ImageList;
            MenuStrip.Items.Add(ViewMenu);

            // Menubar -> View -> Undock
            ViewMenu.DropDownItems.Add(new ToolStripCommandMenuItem
            {
                Command = new UndockCommand(this),
                ShortcutKeys = Keys.Control | Keys.D
            });

            ImportDialog = new OpenFileDialog();
            ExportDialog = new SaveFileDialog();

            SearchFilter.Enabled = false;
            EntryList.Enabled = false;
        }

        private static Dictionary<string, string> FilterList { get; } = new Dictionary<string, string>
        {
            { "Default",   "Filter" },
            { "Entity",    "Arrow" },
            { "Zone",      "ThingViolet" },
            { "Scenery",   "ThingBlue" },
            { "Sort List", "ThingGray" },
            { "Model",     "ThingCrimson" },
            { "Animation", "ThingLime" },
            { "GOOL",      "ThingCode" },
            { "Music",     "MusicNoteBlue" },
            { "Sound",     "SpeakerBlue" },
            { "Texture",   "Painting" }
        };

        private NodeListForm? frmEntryList;

        public FlatTabControl TabControl { get; }

        public MenuStrip MenuStrip { get; }

        public ToolStripMenuItem FileMenu { get; }

        public ToolStripMenuItem EditMenu { get; }

        public ToolStripMenuItem ViewMenu { get; }

        public ToolStrip ToolStrip { get; }

        public ToolStripTextBox SearchBox { get; }

        public ToolStripDropDownButton SearchFilter { get; }

        public ToolStripButton EntryList { get; }

        public IWorkspaceHost? ActiveWorkspaceHost =>
            TabControl.SelectedTab?.Tag as MainControl;

        public OpenFileDialog ImportDialog { get; }

        public SaveFileDialog ExportDialog { get; }

        public void ShowInformation(string msg, string title)
        {
            DarkMessageBox.ShowInformation(msg, title);
        }

        public void ShowError(string msg)
        {
            DarkMessageBox.ShowError(msg, "CrashEdit Error");
        }

        public bool ShowImportDialog(out string? filename, string[] fileFilters)
        {
            ArgumentNullException.ThrowIfNull(fileFilters);

            var filter = string.Join("|", fileFilters);
            if (filter != "")
            {
                filter += '|';
            }
            filter += "All files (*.*)|*.*";
            ImportDialog.Filter = filter;
            ImportDialog.FilterIndex = 1;

            if (ImportDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = ImportDialog.FileName;
                return true;
            }
            else
            {
                filename = null;
                return false;
            }
        }

        public bool ShowExportDialog(out string? filename, string[] fileFilters)
        {
            ArgumentNullException.ThrowIfNull(fileFilters);

            var filter = string.Join("|", fileFilters);
            if (filter != "")
            {
                filter += '|';
            }
            filter += "All files (*.*)|*.*";
            ExportDialog.Filter = filter;
            ExportDialog.FilterIndex = 1;

            if (ExportDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = ExportDialog.FileName;
                return true;
            }
            else
            {
                filename = null;
                return false;
            }
        }

        public UserChoice? ShowChoiceDialog(string msg, IEnumerable<UserChoice> choices)
        {
            ArgumentNullException.ThrowIfNull(msg);
            ArgumentNullException.ThrowIfNull(choices);

            using (var dialog = new ChoiceDialog())
            {
                dialog.MessageText = msg;
                dialog.AddChoices(choices);
                var result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    return dialog.SelectedChoice;
                }
                else
                {
                    return null;
                }
            }
        }

        public event EventHandler? ResyncSuggested;

        protected virtual void OnResyncSuggested(EventArgs e)
        {
            if (ActiveWorkspaceHost is MainControl mainCtl)
            {
                SearchBox.Enabled = true;
                SearchBox.Text = mainCtl.SearchQuery;
                SearchFilter.Enabled = true;
                SearchFilter.ImageKey = FilterList.ContainsKey(mainCtl.FilterText) ? FilterList[mainCtl.FilterText] : "Filter";
                foreach (ToolStripMenuItem menuItem in SearchFilter.DropDownItems)
                {
                    if (menuItem is ToolStripMenuItem item)
                    {
                        item.Checked = false;
                        item.Font = new Font(SearchFilter.Font, FontStyle.Regular);
                        if (item.Text == mainCtl.FilterText)
                        {
                            item.Checked = true;
                            item.Font = new Font(SearchFilter.Font, FontStyle.Bold);
                        }
                    
                    }
                }
                EntryList.Enabled = true;
            }
            else
            {
                //SearchBox.Enabled = false;
                SearchBox.Text = string.Empty;
                SearchFilter.Enabled = false;
                EntryList.Enabled = false;
            }
            ResyncSuggested?.Invoke(this, e);
        }

        protected void MainControl_ActiveControllerChanged(object sender, EventArgs e)
        {
            if (sender == ActiveWorkspaceHost)
            {
                OnResyncSuggested(EventArgs.Empty);
            }
        }

        private void AdjustTabWidth(TabControl tabControl)
        {
            using (Graphics g = tabControl.CreateGraphics())
            {
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    SizeF textSize = g.MeasureString(tabControl.TabPages[i].Text, tabControl.Font);

                    int newWidth = (int)Math.Ceiling(textSize.Width) + 20;
                    tabControl.ItemSize = new Size(Math.Max(tabControl.ItemSize.Width, newWidth), tabControl.ItemSize.Height);
                }
            }
        }

    }

    public class FlatTabControl : TabControl
    {
        #region Public Properties

        [Description("Color for a decorative line"), Category("Appearance")]
        public Color LineColor { get; set; } = Color.DodgerBlue;

        [Description("Color for all Borders"), Category("Appearance")]
        public Color BorderColor { get; set; } = SystemColors.ControlDark;

        [Description("Back color for selected Tab"), Category("Appearance")]
        public Color SelectTabColor { get; set; } = SystemColors.ControlLight;

        [Description("Fore Color for Selected Tab"), Category("Appearance")]
        public Color SelectedForeColor { get; set; } = SystemColors.HighlightText;

        [Description("Back Color for un-selected tabs"), Category("Appearance")]
        public Color TabColor { get; set; } = SystemColors.ControlLight;

        [Description("Background color for the whole control"), Category("Appearance"), Browsable(true)]
        public override Color BackColor { get; set; } = SystemColors.Control;

        [Description("Fore Color for all Texts"), Category("Appearance")]
        public override Color ForeColor { get; set; } = SystemColors.InfoText;

        [Description("Shows a Close Button on each tab"), Category("Appearance")]
        public bool ShowTabCloseButton { get; set; } = true;

        [Description("Color for the Close Button on each tab"), Category("Appearance")]
        public Color TabCloseColor { get; set; }

        #endregion Public Properties

        public FlatTabControl()
        {
            try
            {
                Appearance = TabAppearance.Buttons;
                DrawMode = TabDrawMode.Normal;
                ItemSize = new Size(0, 0);
                SizeMode = TabSizeMode.Fixed;

                PreRemoveTabPage = null;
                this.DrawMode = TabDrawMode.OwnerDrawFixed;
            }
            catch { }
        }

        protected override void InitLayout()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            base.InitLayout();

            TabCloseColor = this.ForeColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawControl(e.Graphics);
        }

        private delegate bool PreRemoveTab(int indx);
        private PreRemoveTab PreRemoveTabPage;
        private bool OverCloseTab = false;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Reacts to the Click on the Close Tab Button:
            if (ShowTabCloseButton)
            {
                Point p = e.Location;
                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle r = GetTabRect(i);
                    r.Offset(6, 8);
                    r.Width = 12;
                    r.Height = 12;
                    if (r.Contains(p))
                    {
                        CloseTab(i);
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            /* Hightlighs the Close Button when the Mouse is over it  */
            if (ShowTabCloseButton)
            {
                Point p = e.Location;
                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle r = GetTabRect(i);
                    r.Offset(6, 8);
                    r.Width = 12;
                    r.Height = 12;

                    OverCloseTab = r.Contains(p); //<- Mouse is over the Close button

                    if (OverCloseTab)
                    {
                        DrawTab(this.CreateGraphics(), this.TabPages[i], i);
                    }
                    else
                    {
                        if (TabCloseColor == Color.Red)
                        {
                            DrawTab(this.CreateGraphics(), this.TabPages[i], i);
                        }
                    }
                }
            }
            base.OnMouseMove(e);

            Point mousePosition = e.Location;
            if (!ClientRectangle.Contains(mousePosition))
            {
                Cursor = Cursors.Default;
                return;
            }

            bool cursorOnOtherTab = false;
            for (int i = 0; i < TabPages.Count; i++)
            {
                if (i == SelectedIndex)
                    continue;

                Rectangle tabRect = GetTabRect(i);
                if (tabRect.Contains(mousePosition) && mousePosition.Y <= tabRect.Bottom)
                {
                    cursorOnOtherTab = true;
                    break;
                }
            }
            Cursor = cursorOnOtherTab ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void CloseTab(int i)
        {
            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            TabPages.Remove(TabPages[i]);
        }

        internal void DrawControl(Graphics g)
        {
            try
            {
                if (!Visible)
                {
                    return;
                }

                Rectangle clientRectangle = ClientRectangle;
                clientRectangle.Inflate(2, 2);

                // Whole Control Background:
                using (Brush bBackColor = new SolidBrush(BackColor))
                {
                    g.FillRectangle(bBackColor, ClientRectangle);
                }

                Region region = g.Clip;

                for (int i = 0; i < TabCount; i++)
                {
                    DrawTab(g, TabPages[i], i);
                    TabPages[i].BackColor = TabColor;
                }

                g.Clip = region;

                using (Pen border = new Pen(BorderColor))
                {
                    g.DrawRectangle(border, clientRectangle);

                    if (SelectedTab != null)
                    {
                        clientRectangle.Offset(1, 1);
                        clientRectangle.Width -= 2;
                        clientRectangle.Height -= 2;
                        g.DrawRectangle(border, clientRectangle);
                        clientRectangle.Width -= 1;
                        clientRectangle.Height -= 1;
                        g.DrawRectangle(border, clientRectangle);
                    }
                }
            }
            catch { }
        }

        internal void DrawTab(Graphics g, TabPage customTabPage, int nIndex)
        {
            Rectangle tabRect = GetTabRect(nIndex);
            Rectangle tabTextRect = GetTabRect(nIndex);
            bool isSelected = (SelectedIndex == nIndex);
            Point[] points;

            if (Alignment == TabAlignment.Top)
            {
                points = new[]
                {
                    new Point(tabRect.Left+3, tabRect.Bottom),
                    new Point(tabRect.Left+3, tabRect.Top + 0),
                    new Point(tabRect.Left + 0, tabRect.Top),
                    new Point(tabRect.Right - 0, tabRect.Top),
                    new Point(tabRect.Right, tabRect.Top + 0),
                    new Point(tabRect.Right, tabRect.Bottom),
                    new Point(tabRect.Left+3, tabRect.Bottom)
                };
            }
            else
            {
                points = new[]
                {
                    new Point(tabRect.Left, tabRect.Top),
                    new Point(tabRect.Right, tabRect.Top),
                    new Point(tabRect.Right, tabRect.Bottom - 0),
                    new Point(tabRect.Right - 0, tabRect.Bottom),
                    new Point(tabRect.Left + 0, tabRect.Bottom),
                    new Point(tabRect.Left, tabRect.Bottom - 0),
                    new Point(tabRect.Left, tabRect.Top)
                };
            }

            // Draws the Tab Header:
            //Color HeaderColor = isSelected ? SelectTabColor : BackColor;
            Color HeaderColor = BackColor;
            using (Brush brush = new SolidBrush(HeaderColor))
            {
                g.FillPolygon(brush, points);
                g.DrawPolygon(new Pen(HeaderColor), points);

                if (isSelected)
                {
                    g.DrawLine(new Pen(BackColor),
                        new Point(tabRect.Left, tabRect.Top), new Point(tabRect.Left + 3, tabRect.Top));
                    g.DrawLine(new Pen(LineColor),
                        new Point(tabRect.Left + 3, tabRect.Bottom), new Point(tabRect.Left + tabRect.Width, tabRect.Bottom));
                }
            }

            // Draws a Close Button:
            if (ShowTabCloseButton)
            {
                Rectangle r = tabTextRect;
                r = GetTabRect(nIndex);
                r.Offset(6, 8); //Vertically Centered
                r.Height = 5;
                r.Width = 5;

                // If Mouse is over the CloseButton, it Draws it in Red, otherwise uses default Color:
                TabCloseColor = OverCloseTab ? Color.Red : this.ForeColor;
                Brush b = new SolidBrush(TabCloseColor);
                Pen p = new Pen(b);

                // Draws an X:
                g.DrawLine(p, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
                g.DrawLine(p, r.X + r.Width, r.Y, r.X, r.Y + r.Height);
            }

            // Draws the Title of the Tab:
            Rectangle rectangleF = tabTextRect;
            rectangleF.X += 2; // Vertically Centered
            rectangleF.Y += 2; // Horizontally Centered
            TextRenderer.DrawText(g, customTabPage.Text, Font, rectangleF, isSelected ? SelectedForeColor : ForeColor);
        }

        private SubClass scUpDown;
        private bool bUpDown;
        private bool hasFocus;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            FindUpDown();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            FindUpDown();
            UpdateUpDown();

            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            FindUpDown();
            UpdateUpDown();

            base.OnControlRemoved(e);
        }

        private void FindUpDown()
        {
            var bFound = false;
            var pWnd = Win32.GetWindow(Handle, Win32.GW_CHILD);

            while (pWnd != IntPtr.Zero)
            {
                var className = new char[33];
                var length = Win32.GetClassName(pWnd, className, 32);
                var s = new string(className, 0, length);

                if (s == "msctls_updown32")
                {
                    bFound = true;

                    if (!bUpDown)
                    {
                        scUpDown = new SubClass(pWnd, true);
                        scUpDown.SubClassedWndProc += new SubClass.SubClassWndProcEventHandler(scUpDown_SubClassedWndProc);

                        bUpDown = true;
                    }
                    break;
                }

                pWnd = Win32.GetWindow(pWnd, Win32.GW_HWNDNEXT);
            }

            if ((!bFound) && (bUpDown))
            {
                bUpDown = false;
            }
        }

        private void UpdateUpDown()
        {
            if (!bUpDown) return;
            if (!Win32.IsWindowVisible(scUpDown.Handle)) return;
            var rect = new Rectangle();

            Win32.GetClientRect(scUpDown.Handle, ref rect);
            Win32.InvalidateRect(scUpDown.Handle, ref rect, true);
        }

        private int scUpDown_SubClassedWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_PAINT:
                    {
                        var hDC = Win32.GetWindowDC(scUpDown.Handle);
                        var g = Graphics.FromHdc(hDC);

                        DrawIcons(g);

                        g.Dispose();
                        Win32.ReleaseDC(scUpDown.Handle, hDC);
                        m.Result = IntPtr.Zero;

                        var rect = new Rectangle();

                        Win32.GetClientRect(scUpDown.Handle, ref rect);
                        Win32.ValidateRect(scUpDown.Handle, ref rect);
                    }
                    return 1;
            }

            return 0;
        }

        internal void DrawIcons(Graphics g)
        {
            var TabControlArea = ClientRectangle;
            var r0 = new Rectangle();
            Win32.GetClientRect(scUpDown.Handle, ref r0);

            Brush br = new SolidBrush(ThemeProvider.Theme.Colors.LighterBackground);
            g.FillRectangle(br, r0);
            br.Dispose();

            g.DrawString("◀", new Font(Font.FontFamily, 12f),
                new SolidBrush(ThemeProvider.Theme.Colors.DisabledText), r0);

            g.DrawString("▶", new Font(Font.FontFamily, 12f),
                new SolidBrush(ThemeProvider.Theme.Colors.DisabledText),
                new Rectangle(r0.X + 20, r0.Y, r0.Width, r0.Height));
        }
    }

    internal static class Win32
    {
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_PAINT = 0xF;
        public const int WM_CREATE = 0x1;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCPAINT = 0x85;
        public const int WM_PRINT = 0x317;
        public const int WM_DESTROY = 0x2;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_SHARED_MENU = 0x1E2;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgc;
            public WINDOWPOS wndpos;
        }
    }

    internal class SubClass : NativeWindow
    {
        public delegate int SubClassWndProcEventHandler(ref Message m);

        public event SubClassWndProcEventHandler SubClassedWndProc;

        public SubClass(IntPtr Handle, bool _SubClass)
        {
            AssignHandle(Handle);
            SubClassed = _SubClass;
        }

        public bool SubClassed { get; set; }

        protected override void WndProc(ref Message m)
        {
            if (SubClassed)
            {
                if (OnSubClassedWndProc(ref m) != 0)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private int OnSubClassedWndProc(ref Message m)
        {
            return SubClassedWndProc?.Invoke(ref m) ?? 0;
        }
    }

}
