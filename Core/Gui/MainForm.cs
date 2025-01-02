using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AltUI.Forms;
using MetroSet_UI.Controls;

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
                //TabStyle = MetroSet_UI.Enums.TabStyle.Style2,
                //Style = MetroSet_UI.Enums.Style.Dark,
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

            // Toolbar -> Undock
            ToolStrip.Items.Add(new ToolStripCommandButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Command = new UndockCommand(this)
            });

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
                ImageKey = "MagnifyingGlass"
            });

            // Menubar
            MenuStrip = new MenuStrip
            {
                ImageList = Embeds.ImageList
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
                ImageKey = "MagnifyingGlass",
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
        }

        public FlatTabControl TabControl { get; }

        public MenuStrip MenuStrip { get; }

        public ToolStripMenuItem FileMenu { get; }

        public ToolStripMenuItem EditMenu { get; }

        public ToolStripMenuItem ViewMenu { get; }

        public ToolStrip ToolStrip { get; }

        public ToolStripTextBox SearchBox { get; }

        public IWorkspaceHost? ActiveWorkspaceHost =>
            TabControl.SelectedTab?.Tag as MainControl;

        public OpenFileDialog ImportDialog { get; }

        public SaveFileDialog ExportDialog { get; }

        public void ShowError(string msg)
        {
            DarkMessageBox.ShowError(
                msg,
                "CrashEdit Error");
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
            }
            else
            {
                //SearchBox.Enabled = false;
                SearchBox.Text = "";
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

        private void AdjustTabWidth(System.Windows.Forms.TabControl tabControl)
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
    public class FlatTabControl : System.Windows.Forms.TabControl
    {
        #region Public Properties

        [Description("Color for a decorative line"), Category("Appearance")]
        public Color LineColor { get; set; } = SystemColors.Highlight;

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
        public override Color ForeColor { get; set; } = SystemColors.ControlText;

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
            Color HeaderColor = isSelected ? SelectTabColor : BackColor;
            using (Brush brush = new SolidBrush(HeaderColor))
            {
                g.FillPolygon(brush, points);
                g.DrawPolygon(new Pen(HeaderColor), points);

                if (isSelected)
                {
                    g.DrawLine(new Pen(BackColor),
                        new Point(tabRect.Left, tabRect.Top), new Point(tabRect.Left + 3, tabRect.Top));
                    g.DrawLine(new Pen(Color.DodgerBlue),
                        new Point(tabRect.Left + 3, tabRect.Top), new Point(tabRect.Left + tabRect.Width, tabRect.Top));
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
    }
}
