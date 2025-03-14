using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using CrashEdit.Crash;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class HexView : UserControl
    {
        private dynamic? controller;
        private HexBox hexBox;
        private HexBoxHeader hexBoxHeader;

        private TableLayoutPanel pnMain;

        public DarkTextBox txtGoto;
        public Label lblPosition;

        public HexView(dynamic controller, ReadOnlyMemory<byte> data, Func<int, int, byte[], bool>? dataChangeHandler)
        {
            this.controller = controller;
            MainInit(data, dataChangeHandler);
        }

        public HexView(ReadOnlyMemory<byte> data, Func<int, int, byte[], bool>? dataChangeHandler) : this(null, data, dataChangeHandler)
        {
        }

        private void MainInit(ReadOnlyMemory<byte> data, Func<int, int, byte[], bool>? dataChangeHandler)
        {
            BackColor = Color.FromArgb(31, 31, 32);

            ToolStrip toolStrip = new ToolStrip();
            ToolStripButton tsbImport = new ToolStripButton()
            {
                Text = "Import"
            };
            tsbImport.Click += new EventHandler(tsbImport_Click);
            ToolStripButton tsbExport = new ToolStripButton()
            {
                Text = "Export"
            };
            tsbExport.Click += new EventHandler(tsbExport_Click);
            if (controller != null)
                toolStrip.Items.Add(tsbImport);
            toolStrip.Items.Add(tsbExport);

            // HexBox
            hexBox = new HexBox(this)
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Data = data,
                DataChangeHandler = dataChangeHandler
            };

            // Header
            hexBoxHeader = new HexBoxHeader(this, hexBox)
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Height = HexBox.TextPadding + hexBox.CharSize.Height + HexBox.TextPadding
            };

            // Footer
            Label lblGoto = new Label()
            {
                Text = "Goto",
                Padding = new Padding(0, 6, 0, 4)
            };
            using (Graphics g = lblGoto.CreateGraphics())
            {
                int width = (int)g.MeasureString(lblGoto.Text, lblGoto.Font).Width + 10;
                lblGoto.Width = width;
            }
            txtGoto = new DarkTextBox()
            {
                Width = 60,
                Padding = new Padding(0, 4, 0, 4)
            };
            txtGoto.KeyDown += new KeyEventHandler(txtGoto_KeyDown);
            txtGoto.KeyPress += new KeyPressEventHandler(txtGoto_KeyPress);
            lblPosition = new Label()
            {
                Width = 400,
                Padding = new Padding(16, 6, 0, 4)
            };
            FlowLayoutPanel pnFooter = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                BackColor = Color.FromArgb(27, 27, 28),
                Height = txtGoto.Height + 8,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false
            };
            pnFooter.Controls.Add(lblGoto);
            pnFooter.Controls.Add(txtGoto);
            pnFooter.Controls.Add(lblPosition);

            // Main Control
            pnMain = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            pnMain.Controls.Add(toolStrip);
            pnMain.Controls.Add(hexBoxHeader);
            pnMain.Controls.Add(hexBox);
            pnMain.Controls.Add(pnFooter);

            pnMain.RowStyles.Clear();
            pnMain.RowStyles.Add(new RowStyle(SizeType.Absolute, toolStrip.Height));
            pnMain.RowStyles.Add(new RowStyle(SizeType.Absolute, hexBoxHeader.Height));
            pnMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnMain.RowStyles.Add(new RowStyle(SizeType.Absolute, pnFooter.Height));

            Controls.Add(pnMain);
        }

        private void tsbImport_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] newData = File.ReadAllBytes(openFileDialog.FileName);

                    if (controller != null)
                    {
                        controller.ReplaceData(newData);
                        hexBox.Data = newData;
                        hexBox.Invalidate();
                    }
                }
            }
        }

        private void tsbExport_Click(object? sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, hexBox.Data.ToArray());
                }
            }
        }

        private void txtGoto_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G && (e.Modifiers & Keys.Control) == Keys.Control)
            {
                txtGoto.SelectAll();
            }
        }

        private void txtGoto_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                // avoid to play "Ding" sound
                e.Handled = true;
                e.KeyChar = (char)Keys.D0;

                if (int.TryParse(txtGoto.Text, System.Globalization.NumberStyles.HexNumber, null, out int pos))
                {
                    hexBox.MoveTo(pos, false);
                    hexBox.ResetAnchor();
                    hexBox.Invalidate();
                    hexBox.Focus();
                    e.Handled = true;
                }
                else
                {
                    DarkMessageBox.ShowError($"Invalid address '{txtGoto.Text}'.", "HexView");
                }
            }
        }

        public void UpdateHeader()
        {
            hexBoxHeader.Invalidate();
        }
    }

    public sealed class HexBoxHeader : UserControl
    {
        private HexView hexView;
        private HexBox hexBox;

        private static Brush _bgAddressBrush = Brushes.DarkGray;

        public HexBoxHeader(HexView hexview, HexBox hexbox)
        {
            hexView = hexview;
            hexBox = hexbox;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Trimming = StringTrimming.None;
            strFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            int row = 0;
            int rowY = hexBox.YStart + hexBox.YStep * row;
            int rowFirstByte = row * hexBox.ColumnCount - hexBox.FirstByteColumn;

            int colFirst = 0;
            int colLast = hexBox.ColumnCount;

            for (int col = colFirst; col < colLast; col++)
            {
                int colX = hexBox.XStart + hexBox.XStep * col;

                Brush fgBrush = _bgAddressBrush;
                Brush bgBrush = new SolidBrush(Color.Transparent);

                bool leftBorderDrawn = col == 0;

                var cellInnerRect = new Rectangle();
                cellInnerRect.X = colX + (leftBorderDrawn ? HexBox.BorderSize : 0);
                cellInnerRect.Y = rowY + HexBox.BorderSize;
                cellInnerRect.Width = HexBox.TextPadding + hexBox.CharSize.Width * 2 + HexBox.TextPadding + (leftBorderDrawn ? 0 : HexBox.BorderSize);
                cellInnerRect.Height = HexBox.TextPadding + hexBox.CharSize.Height + HexBox.TextPadding;

                e.Graphics.FillRectangle(bgBrush, cellInnerRect);

                var text = (col % 16).ToString("X");
                e.Graphics.DrawString(
                    text,
                    HexBox.Font,
                    fgBrush,
                    cellInnerRect,
                    strFormat);
            }
        }
    }

    public sealed class HexBox : UserControl
    {
        private HexView hexView;

        public HexBox(HexView hexview)
        {
            hexView = hexview;
            DoubleBuffered = true;
            BackColor = Color.FromArgb(31, 31, 32);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem copyBytes = new ToolStripMenuItem("Copy as Bytes");
            ToolStripMenuItem cutBytes = new ToolStripMenuItem("Cut as Bytes");
            ToolStripMenuItem pasteBytes = new ToolStripMenuItem("Paste as Bytes");
            ToolStripMenuItem copyEID = new ToolStripMenuItem("Copy as EID");
            ToolStripMenuItem cutEID = new ToolStripMenuItem("Cut as EID");
            ToolStripMenuItem pasteEID = new ToolStripMenuItem("Paste as EID");
            //copyBytes.Image = Embeds.GetIcon("Copy")?.ToBitmap();
            //cutBytes.Image = Embeds.GetIcon("Cut")?.ToBitmap();
            //pasteBytes.Image = Embeds.GetIcon("Paste")?.ToBitmap();
            //copyEID.Image = Embeds.GetIcon("Copy")?.ToBitmap();
            //cutEID.Image = Embeds.GetIcon("Cut")?.ToBitmap();
            //pasteEID.Image = Embeds.GetIcon("Paste")?.ToBitmap();
            copyBytes.Click += CopyBytes_Click;
            cutBytes.Click += CutBytes_Click;
            pasteBytes.Click += PasteBytes_Click;
            copyEID.Click += CopyEID_Click;
            cutEID.Click += CutEID_Click;
            pasteEID.Click += PasteEID_Click;
            contextMenu.Items.Add(copyBytes);
            contextMenu.Items.Add(cutBytes);
            contextMenu.Items.Add(pasteBytes);
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(copyEID);
            contextMenu.Items.Add(cutEID);
            contextMenu.Items.Add(pasteEID);
            ContextMenuStrip = contextMenu;

            ResetLayout();
        }

        // The number of columns in the viewer.
        private int _columnCount = 0x10;
        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                if (value == _columnCount)
                    return;
                if (value < 1)
                    throw new ArgumentException();

                _columnCount = value;
                ResetLayout();
                Invalidate();
            }
        }

        // The number of columns per "group". Each group of columns has an alternating set
        // of background colors in the viewer, to make it easier to identify columns and word
        // boundaries. If set to zero, this functionality is disabled.
        private int _columnsPerGroup = 4;
        public int ColumnsPerGroup
        {
            get => _columnsPerGroup;
            set
            {
                if (value == _columnsPerGroup)
                    return;
                if (value < 0)
                    throw new ArgumentException();

                _columnsPerGroup = value;
                Invalidate();
            }
        }

        // The number of rows in the viewer. This is computed based on the column count and
        // data length. A cell is always included for one past the last byte.
        public int RowCount => (Data.Length / ColumnCount) + 1;

        // The number of rows advanced when the user hits page-up or page-down.
        public int RowsPerPage { get; set; } = 0x10;

        // The number of columns to skip before the first byte shown to the user.
        private int _firstByteColumn = 0;
        public int FirstByteColumn
        {
            get => _firstByteColumn;
            set
            {
                if (value == _firstByteColumn)
                    return;
                if (value < 0)
                    throw new ArgumentException();

                _firstByteColumn = value;
                ResetLayout();
                Invalidate();
            }
        }

        // The address of the first byte. This is for display purposes only; the first byte
        // is always accessed at Data[0], the second at Data[1], then Data[2], etc.
        private long _firstByteAddress = 0;
        public long FirstByteAddress
        {
            get => _firstByteAddress;
            set
            {
                if (value == _firstByteAddress)
                    return;

                _firstByteAddress = value;
                ResetLayout();
                Invalidate();
            }
        }

        // The address of the first row to display to the user.
        public long FirstRowAddress => FirstByteAddress - FirstByteColumn;

        // The currently selected byte, with zero being the first byte. This is limited to the
        // range [0 ... Data.Length] inclusive.
        public int ByteCursor { get; private set; }

        // The column number of the currently selected byte, zero being the first column.
        public int ByteCursorColumn => (FirstByteColumn + ByteCursor) % ColumnCount;

        // The row number of the currently selected byte, zero being the first row.
        public int ByteCursorRow => (FirstByteColumn + ByteCursor) / ColumnCount;

        // The address of the currently selected byte.
        public long ByteCursorAddress => FirstByteAddress + ByteCursor;

        // Anchor set by right-clicking.
        public int ByteAnchor { get; private set; }

        // The data on which the HexView operates. The memory must remain valid until replaced
        // or until the control is disposed.
        //
        // Replacing this value with a shorter memory view will also clamp ByteCursor to the new
        // range.
        //
        // Invalidate() must be called manually whenever the contents of the memory change.
        private ReadOnlyMemory<byte> _data;
        public ReadOnlyMemory<byte> Data
        {
            get => _data;
            set
            {
                _data = value;
                if (ByteCursor > _data.Length)
                {
                    ByteCursor = _data.Length;
                }
                ResetLayout();
                Invalidate();
            }
        }

        // Optional function for implementing data changes.
        //
        // If not null, editing functionality will be enabled. When the user inputs changes
        // to the data, the HexView will not apply these changes directly itself, but will call
        // this function to attempt to do so.
        //
        // Parameters:
        //
        //  * int destOffset: Starting offset in Data of the bytes to be replaced.
        //  * int destLength: Length of the bytes to be replaced.
        //  * byte[] source:  The new bytes which should replace the previous bytes.
        //
        // If AllowResize is true, destLength may be different from source.Length. This represents
        // a request to resize the data. Be aware especially of:
        //
        //  * deletion (source.Length == 0)
        //  * insertion (destLength == 0)
        //  * appending (destLength == 0 && destOffset == Data.Length)
        //
        // The function should return true if successful, and false otherwise. If the function
        // returns true, Invalidate() will be called automatically to update the control graphics,
        // but only for the regions covered by the *requested* change. If this does not match the
        // actual effected changes, Invalidate() should be called manually unless Data was
        // reassigned.
        //
        // If this property is null, editing is disabled entirely.
        private Func<int, int, byte[], bool>? _dataChangeHandler = null;
        public Func<int, int, byte[], bool>? DataChangeHandler
        {
            get => _dataChangeHandler;
            set
            {
                if (value == _dataChangeHandler)
                    return;

                ClearInput();
                _dataChangeHandler = value;
            }
        }

        // If editing is enabled, setting this true further allows editing features which require
        // data resizing, such as insertions and deletions.
        private bool _allowResize = false;
        public bool AllowResize
        {
            get => _allowResize;
            set
            {
                _allowResize = value;

                // Cancel any in-progress append inputs if resizing was just disabled.
                if (!value && _pendingInput != null && ByteCursor == Data.Length)
                {
                    ClearInput();
                }
            }
        }

        // Attempts to move the byte cursor, returning the distance actually traveled.
        // Positive values advance forward, but will stop on Data.Length if reached. Negative
        // values advance backward similarly, stopping on zero.
        //
        // This also scrolls the view in the control to fully display the new target.
        public int MoveBy(int delta)
        {
            int oldCursor = ByteCursor;
            MoveTo(ByteCursor + delta, false);
            return ByteCursor - oldCursor;
        }

        // Attempts to move the byte cursor, returning true if the destination was valid (in
        // bounds). If the destination is out of bounds, the cursor position is clamped to the
        // valid range and false is returned.
        //
        // This also scrolls the view in the control to fully display the new target.
        public bool MoveTo(int target, bool isAnchor)
        {
            int oldCursor;
            bool inRange;
            if (isAnchor)
            {
                oldCursor = ByteAnchor;

                if (target < 0)
                {
                    ByteAnchor = 0;
                    inRange = false;
                }
                else if (target > Data.Length)
                {
                    ByteAnchor = Data.Length;
                    inRange = false;
                }
                else
                {
                    ByteAnchor = target;
                    inRange = true;
                }
            }
            else
            {
                oldCursor = ByteCursor;

                if (target < 0)
                {
                    ByteCursor = 0;
                    inRange = false;
                }
                else if (target > Data.Length)
                {
                    ByteCursor = Data.Length;
                    inRange = false;
                }
                else
                {
                    ByteCursor = target;
                    inRange = true;
                }
            }

            ClearInput();

            int oldCol = (oldCursor + FirstByteColumn) % ColumnCount;
            int oldRow = (oldCursor + FirstByteColumn) / ColumnCount;
            InvalidateCell(oldCol, oldRow);

            int newCol;
            int newRow;
            if (isAnchor)
            {
                newCol = (ByteAnchor + FirstByteColumn) % ColumnCount;
                newRow = (ByteAnchor + FirstByteColumn) / ColumnCount;
            }
            else
            {
                newCol = (ByteCursor + FirstByteColumn) % ColumnCount;
                newRow = (ByteCursor + FirstByteColumn) / ColumnCount;
            }

            var newRect = new Rectangle();
            newRect.X = XStart + XStep * newCol + AutoScrollPosition.X;
            newRect.Y = YStart + YStep * newRow + AutoScrollPosition.Y;
            newRect.Width = XStep + BorderSize;
            newRect.Height = YStep + BorderSize;
            InvalidateCell(newCol, newRow);

            Point newScrollPos = AutoScrollPosition;
            if (newRect.Left < 0)
            {
                newScrollPos.X -= newRect.Left;
            }
            else if (newRect.Right > ClientSize.Width)
            {
                newScrollPos.X -= (newRect.Right - ClientSize.Width);
            }
            if (newRect.Top < 0)
            {
                newScrollPos.Y -= newRect.Top;
            }
            else if (newRect.Bottom > ClientSize.Height)
            {
                newScrollPos.Y -= (newRect.Bottom - ClientSize.Height);
            }
            if (newScrollPos != AutoScrollPosition)
            {
                // AutoScrollPosition is a poor API which requires you to set the inverse values
                // of what you expect to get back.
                AutoScrollPosition = new Point(
                    -newScrollPos.X,
                    -newScrollPos.Y);
            }
            if (isAnchor)
            {
                SetAnchor(target);
            }
            else
            {
                ResetAnchor();
            }
             
            Invalidate();
            return inRange;
        }

        // Enable special keys which would otherwise not be delivered to OnKeyDown.
        protected override bool IsInputKey(Keys keyData) =>
            keyData switch
            {
                Keys.Up => true,
                Keys.Down => true,
                Keys.Left => true,
                Keys.Right => true,
                _ => base.IsInputKey(keyData),
            };

        // Handle keyboard inputs.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == (Keys.Control | Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        // Copy chunks as EID
                        CopyBytes(false, true);
                        break;

                    case Keys.X:
                        // Cut chunks as EID
                        CopyBytes(true, true);
                        break;

                    case Keys.V:
                        // Paste chunks as EID
                        PasteBytes(true);
                        break;
                }
            }
            else if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        // Copy chunks as bytes.
                        CopyBytes(false, false);
                        break;

                    case Keys.X:
                        // Cut chunks as bytes.
                        CopyBytes(true, false);
                        break;

                    case Keys.V:
                        // Paste chunks as bytes.
                        PasteBytes(false);
                        break;

                    case Keys.Space:
                        // Input zero.
                        InputZero(4);
                        break;

                    case Keys.G:
                        // Goto.
                        hexView.txtGoto.SelectAll();
                        hexView.txtGoto.Focus();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        // Move up by one cell.
                        MoveBy(-ColumnCount);
                        break;

                    case Keys.Down:
                        // Move down by one cell.
                        MoveBy(ColumnCount);
                        break;

                    case Keys.Left:
                        // Move backward by one cell.
                        MoveBy(-1);
                        break;

                    case Keys.Right:
                        // Move forward by one cell.
                        MoveBy(1);
                        break;

                    case Keys.PageUp:
                        // Move up by one "page".
                        MoveBy(-ColumnCount * RowsPerPage);
                        break;

                    case Keys.PageDown:
                        // Move down by one "page".
                        MoveBy(ColumnCount * RowsPerPage);
                        break;

                    case Keys.Home:
                        // Move to the start ...
                        if (e.Control)
                        {
                            // ... of the entire data.
                            MoveTo(0, false);
                        }
                        else
                        {
                            // ... of the current row.
                            MoveBy(-ByteCursorColumn);
                        }
                        break;

                    case Keys.End:
                        // Move to the end ...
                        if (e.Control)
                        {
                            // ... of the entire data.
                            MoveTo(Data.Length, false);
                        }
                        else
                        {
                            // ... of the current row.
                            MoveBy(ColumnCount - ByteCursorColumn - 1);
                        }
                        break;

                    case Keys.Add:
                        if (ColumnCount < 24)
                        {
                            ColumnCount += 4;
                        }
                        hexView.UpdateHeader();
                        break;

                    case Keys.Subtract:
                        if (ColumnCount > 4)
                        {
                            ColumnCount -= 4;
                        }
                        hexView.UpdateHeader();
                        break;

                    case Keys k when (k >= Keys.D0 && k <= Keys.D9):
                        // Input hex digit 0-9.
                        InputNybble(k - Keys.D0);
                        break;

                    case Keys k when (k >= Keys.NumPad0 && k <= Keys.NumPad9):
                        // Input hex digit 0-9 on numpad.
                        InputNybble(k - Keys.NumPad0);
                        break;

                    case Keys k when (k >= Keys.A && k <= Keys.F):
                        // Input hex digit A-F.
                        InputNybble(k - Keys.A + 0xA);
                        break;

                    case Keys.Back:
                        // Backspace input, if possible.
                        ClearInput();
                        break;

                    case Keys.N:
                        // Input the EID for "NONE!"
                        InputNone();
                        break;

                    case Keys.Space:
                        // Input zero
                        InputZero(1);
                        break;

                    case Keys.Z:
                        // Toggle chunk name view mode
                        _modeChunkName = !_modeChunkName;
                        Invalidate();
                        break;

                    default:
                        base.OnKeyDown(e);
                        break;
                }
            }
        }

        private static Brush brush_borderBrush = new SolidBrush(Color.FromArgb(36, 36, 40));
        private static Brush brush_borderWordBrush = new SolidBrush(Color.FromArgb(40, 40, 44));
        private static Brush brush_bgNormalBrush = new SolidBrush(Color.FromArgb(32, 32, 34));
        private static Brush brush_bgAlternateBrush = new SolidBrush(Color.FromArgb(25, 25, 26));
        private static Brush brush_bgSelectedBrush = new SolidBrush(Color.FromArgb(37, 37, 40));
        private static Brush brush_bgChunkBrush = new SolidBrush(Color.FromArgb(38, 75, 104));
        private static Brush brush_bgSelectedChunkBrush = new SolidBrush(Color.FromArgb(41, 91, 132));
        private static Brush brush_bgSelectedAnchorBrush = new SolidBrush(Color.FromArgb(28, 43, 56));

        // Border color drawn around cells.
        private static Brush _borderBrush = brush_borderBrush;

        // Border color drawn around cells of the same word.
        private static Brush _borderWordBrush = brush_borderWordBrush;

        // Border color drawn around the selected cell.
        private static Brush _selectedBorderBrush = Brushes.DarkCyan;

        // Border color drawn around the anchor cell.
        private static Brush _anchorBorderBrush = Brushes.DarkTurquoise;

        // Color for data being typed in.
        private static Brush _inputBrush = Brushes.Turquoise;

        // Colors for normal cells.
        private static Brush _fgNormalBrush = Brushes.GhostWhite;
        private static Brush _bgNormalBrush = brush_bgNormalBrush;

        // Colors for normal cells, but for every-other column group.
        private static Brush _fgAlternateBrush = Brushes.GhostWhite;
        private static Brush _bgAlternateBrush = brush_bgAlternateBrush;

        // Color for zero-value cells.
        private static Brush _fgZeroBrush = Brushes.DimGray;

        // Color for the selected cell. This overrides the other colors.
        private static Brush _fgSelectedBrush = Brushes.GhostWhite;
        private static Brush _bgSelectedBrush = brush_bgSelectedBrush;

        // Color for cells when they're being displayed as chunk names
        private static Brush _fgChunkBrush = Brushes.GhostWhite;
        private static Brush _bgChunkBrush = brush_bgChunkBrush;
        private static Brush _bgSelectedChunkBrush = brush_bgSelectedChunkBrush;

        // Color for selected cells with the anchor.
        private static Brush _bgSelectedAnchorBrush = brush_bgSelectedAnchorBrush;

        // Color for address.
        private static Brush _bgAddressBrush = Brushes.DarkGray;

        // Size of borders between and around cells, in pixels.
        public static int BorderSize => 2;

        // Size of padding around the text, in pixels.
        public static int TextPadding => Settings.Default.HexViewCellSize == "Small" ? 4 : Settings.Default.HexViewCellSize == "Medium" ? 6 : 8;

        // Font used for displaying numbers.
        public static new Font Font => new Font(FontFamily.GenericMonospace, 10);

        // The space occupied by one character of text. This assumes a fixed-width font.
        public Size CharSize { get; set; }

        // The number of characters in an address.
        public int AddressCharCount { get; set; }

        // The distance between the left side of the control and the left side of the first column.
        public int XStart => TextPadding + CharSize.Width * AddressCharCount + TextPadding;

        // The distance between the left side of one column and the left side of the next.
        public int XStep => BorderSize + TextPadding + CharSize.Width * 2 + TextPadding;

        // The distance between the top of the control and the top of the first row.
        public int YStart => 0;

        // The distance between the top of one row and the top of the next.
        public int YStep => BorderSize + TextPadding + CharSize.Height + TextPadding;

        // Whether we're trying to display cells as chunk names or not
        private bool _modeChunkName = false;

        // Recomputes and applies the control's layout and desired display size.
        private void ResetLayout()
        {
            using (var g = CreateGraphics())
            {
                CharSize = TextRenderer.MeasureText(g, "A", Font, Size.Empty, TextFormatFlags.NoPadding);
            }
            AddressCharCount = 5; // sensible minimum
            AddressCharCount = Math.Max(AddressCharCount, FirstByteAddress.ToString("x").Length);
            AddressCharCount = Math.Max(AddressCharCount, (FirstByteAddress + Data.Length).ToString("x").Length);
            AutoScrollMinSize = new Size(
                XStart + XStep * ColumnCount + BorderSize,
                YStart + YStep * RowCount + BorderSize
            );
        }

        public void ResetAnchor()
        {
            ByteAnchor = ByteCursor;
            hexView.lblPosition.Text = $"Pos: {ByteCursor.ToString("X")}";
        }

        private void SetAnchor(int pos)
        {
            ByteAnchor = pos;
            int position = Math.Min(ByteCursor, ByteAnchor);
            string block = $"{Math.Min(ByteCursor, ByteAnchor).ToString("X")}-{Math.Max(ByteCursor, ByteAnchor).ToString("X")}";
            int length = Math.Max(ByteCursor, ByteAnchor) - Math.Min(ByteCursor, ByteAnchor) + 1;
            hexView.lblPosition.Text = $"Pos: {position.ToString("X")}        Block: {block}        Length: {length.ToString("X")}";
        }

        private int? CheckPosition(MouseEventArgs e)
        {
            if (e.X - AutoScrollPosition.X < XStart) return null;
            if (e.Y - AutoScrollPosition.Y < YStart) return null;

            int col = (e.X - AutoScrollPosition.X - XStart) / XStep;
            int row = (e.Y - AutoScrollPosition.Y - YStart) / YStep;
            if (col < 0 || col >= ColumnCount) return null;
            if (row < 0 || row >= RowCount) return null;

            int pos = row * ColumnCount + col - FirstByteColumn;
            if (pos < 0 || pos > Data.Length) return null;

            return pos;
        }

        // Dragging flag  
        private bool _isDragging = false;

        // Drag start position 
        private Point _dragStartPoint;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                int? pos = CheckPosition(e);
                if (!pos.HasValue) return;

                _dragStartPoint = e.Location;
                _isDragging = false;
                
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    MoveTo(pos.Value, true);
                }
                else
                {
                    MoveTo(pos.Value, false);
                    ResetAnchor();
                }
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (!_isDragging)
                {
                    if (Math.Abs(e.X - _dragStartPoint.X) > 5 || Math.Abs(e.Y - _dragStartPoint.Y) > 5)
                    {
                        _isDragging = true;
                    }
                }
                if (_isDragging)
                {
                    int? pos = CheckPosition(e);
                    if (!pos.HasValue) return;

                    MoveTo(pos.Value, true);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_isDragging)
            {
                _isDragging = false;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            InvalidateCell(ByteCursorColumn, ByteCursorRow);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            InvalidateCell(ByteCursorColumn, ByteCursorRow);
        }

        // Handles drawing the HexView.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var clipRect = e.ClipRectangle;
            clipRect.X -= AutoScrollPosition.X;
            clipRect.Y -= AutoScrollPosition.Y;
            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            var data = Data.Span;

            var strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Trimming = StringTrimming.None;
            strFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            // Determine which rows need to be drawn.
            int visRowFirst = (int)Math.Floor((clipRect.Top - YStart) / (float)YStep);
            int visRowLast = (int)Math.Ceiling((clipRect.Bottom - YStart + BorderSize) / (float)YStep);
            if (visRowFirst < 0)
            {
                visRowFirst = 0;
            }
            if (visRowLast >= RowCount)
            {
                visRowLast = RowCount - 1;
            }

            // Draw each visible row.
            for (int row = visRowFirst; row <= visRowLast; row++)
            {
                int rowY = YStart + YStep * row;
                int rowFirstByte = row * ColumnCount - FirstByteColumn;
                long rowAddress = FirstRowAddress + row * ColumnCount;

                // Draw the row address text.
                var rowAddrRect = new Rectangle();
                rowAddrRect.X = 0;
                rowAddrRect.Y = rowY;
                rowAddrRect.Width = XStart;
                rowAddrRect.Height = YStep + BorderSize;
                e.Graphics.DrawString(
                    rowAddress.ToString("X").PadLeft(AddressCharCount),
                    Font,
                    _bgAddressBrush,
                    rowAddrRect,
                    strFormat);

                // Determine which columns need to be drawn.
                int colFirst;
                if (row == 0)
                {
                    colFirst = FirstByteColumn;
                }
                else
                {
                    colFirst = 0;
                }
                int colLast;
                if (row == RowCount - 1)
                {
                    colLast = data.Length - rowFirstByte; // include one-past-the-end
                }
                else
                {
                    colLast = ColumnCount - 1;
                }

                // Draw the top border for this row.
                e.Graphics.FillRectangle(
                    _borderBrush,
                    XStart + XStep * colFirst,
                    YStart + YStep * row,
                    XStep * (colLast - colFirst + 1) + BorderSize,
                    BorderSize);

                // Draw each cell.
                for (int col = colFirst; col <= colLast; col++)
                {
                    int colX = XStart + XStep * col;
                    int cellByte = rowFirstByte + col;
                    int chunkNameByteOfs = cellByte % 4;
                    bool showChunkName = _modeChunkName && (cellByte - chunkNameByteOfs) + 3 < data.Length && (data[cellByte - chunkNameByteOfs] & 1) != 0 && (data[cellByte - chunkNameByteOfs + 3] & 0x80) == 0;

                    // Draw the left border.
                    // In chunk name mode, only if we're at the left edge of a chunk name (or first column)
                    bool leftBorderDrawn = !showChunkName || cellByte % 4 == 0 || col == 0;
                    if (leftBorderDrawn)
                    {
                        e.Graphics.FillRectangle(
                            cellByte % 4 == 0 ? _borderBrush : _borderWordBrush,
                            XStart + XStep * col,
                            YStart + YStep * row + BorderSize,
                            BorderSize,
                            YStep - BorderSize);
                    }

                    // If this is the last column, also draw the right border.
                    if (col == colLast)
                    {
                        e.Graphics.FillRectangle(
                            _borderBrush,
                            XStart + XStep * (col + 1),
                            YStart + YStep * row + BorderSize,
                            BorderSize,
                            YStep - BorderSize);
                    }

                    // The final cell corresponds to the position one past the end of the
                    // data. Leave that one empty with no background or text.
                    if (cellByte == data.Length)
                        break;

                    bool isAnchor = false;
                    Brush fgBrush;
                    Brush bgBrush;
                    if (cellByte == ByteAnchor)
                    {
                        fgBrush = _fgSelectedBrush;
                        bgBrush = showChunkName ? _bgSelectedChunkBrush : _bgSelectedAnchorBrush;
                    }
                    else if ((cellByte >= ByteAnchor && cellByte <= ByteCursor) && ByteCursor != ByteAnchor)
                    {
                        fgBrush = _fgSelectedBrush;
                        bgBrush = showChunkName ? _bgSelectedChunkBrush : _bgSelectedAnchorBrush;
                    }
                    else if ((cellByte >= ByteCursor && cellByte <= ByteAnchor) && ByteCursor != ByteAnchor)
                    {
                        fgBrush = _fgSelectedBrush;
                        bgBrush = showChunkName ? _bgSelectedChunkBrush : _bgSelectedAnchorBrush;
                    }
                    else if (cellByte == ByteCursor)
                    {
                        fgBrush = _fgSelectedBrush;
                        bgBrush = showChunkName ? _bgSelectedChunkBrush : _bgSelectedBrush;
                    }
                    else if (ColumnsPerGroup != 0 && col / ColumnsPerGroup % 2 == 1)
                    {
                        fgBrush = data[cellByte] == 0 ? _fgZeroBrush : _fgAlternateBrush;
                        bgBrush = showChunkName ? _bgChunkBrush : _bgAlternateBrush;
                    }
                    else
                    {
                        fgBrush = data[cellByte] == 0 ? _fgZeroBrush : _fgNormalBrush;
                        bgBrush = showChunkName ? _bgChunkBrush : _bgNormalBrush;
                    }

                    var cellInnerRect = new Rectangle();
                    cellInnerRect.X = colX + (leftBorderDrawn ? BorderSize : 0);
                    cellInnerRect.Y = rowY + BorderSize;
                    cellInnerRect.Width = TextPadding + CharSize.Width * 2 + TextPadding + (leftBorderDrawn ? 0 : BorderSize);
                    cellInnerRect.Height = TextPadding + CharSize.Height + TextPadding;

                    // Draw the background.
                    e.Graphics.FillRectangle(bgBrush, cellInnerRect);

                    // Draw the cell value.
                    if (!showChunkName)
                    {
                        var text = data[cellByte].ToString("X2");
                        if (_pendingInput != null && cellByte == ByteCursor)
                        {
                            // Skip the first nybble if the user is typing a new byte value.
                            text = " " + text[1];
                        }
                        e.Graphics.DrawString(
                            text,
                            Font,
                            fgBrush,
                            cellInnerRect,
                            strFormat);
                    }
                    else if (chunkNameByteOfs == 3)
                    {
                        // draw chunk name, occupying as many cells of the row as possible
                        var text = Entry.EIDToEName(BitConv.FromInt32(data, cellByte - chunkNameByteOfs));
                        int colSpan = (col % 4) - chunkNameByteOfs + 4;
                        var oldWidth = cellInnerRect.Width;
                        cellInnerRect.Width = oldWidth * colSpan - BorderSize;
                        cellInnerRect.X -= cellInnerRect.Width - oldWidth;
                        e.Graphics.DrawString(
                            text,
                            Font,
                            _fgChunkBrush,
                            cellInnerRect,
                            strFormat);
                    }
                }

                // Draw the bottom border for this row, unless the next row will draw it for us
                // as its top border.
                if (row == visRowLast || row == RowCount - 2)
                {
                    e.Graphics.FillRectangle(
                        _borderBrush,
                        XStart + XStep * colFirst,
                        YStart + YStep * (row + 1),
                        XStep * (colLast - colFirst + 1) + BorderSize,
                        BorderSize);
                }
            }

            // Draw a special border around the anchor and the selected cell, if we have focus.
            if (Focused)
            {
                // Anchor
                int anchorCursorColumn = (FirstByteColumn + ByteAnchor) % ColumnCount;
                int anchorCursorRow = (FirstByteColumn + ByteAnchor) / ColumnCount;
                e.Graphics.FillRectangle(
                     _anchorBorderBrush,
                     XStart + XStep * anchorCursorColumn,
                     YStart + YStep * anchorCursorRow,
                     XStep + BorderSize,
                     BorderSize);
                e.Graphics.FillRectangle(
                    _anchorBorderBrush,
                    XStart + XStep * anchorCursorColumn,
                    YStart + YStep * (anchorCursorRow + 1),
                    XStep + BorderSize,
                    BorderSize);
                e.Graphics.FillRectangle(
                    _anchorBorderBrush,
                    XStart + XStep * anchorCursorColumn,
                    YStart + YStep * anchorCursorRow + BorderSize,
                    BorderSize,
                    YStep - BorderSize);
                e.Graphics.FillRectangle(
                    _anchorBorderBrush,
                    XStart + XStep * (anchorCursorColumn + 1),
                    YStart + YStep * anchorCursorRow + BorderSize,
                    BorderSize,
                    YStep - BorderSize);

                // Selected cell
                e.Graphics.FillRectangle(
                    _selectedBorderBrush,
                    XStart + XStep * ByteCursorColumn,
                    YStart + YStep * ByteCursorRow,
                    XStep + BorderSize,
                    BorderSize);
                e.Graphics.FillRectangle(
                    _selectedBorderBrush,
                    XStart + XStep * ByteCursorColumn,
                    YStart + YStep * (ByteCursorRow + 1),
                    XStep + BorderSize,
                    BorderSize);
                e.Graphics.FillRectangle(
                    _selectedBorderBrush,
                    XStart + XStep * ByteCursorColumn,
                    YStart + YStep * ByteCursorRow + BorderSize,
                    BorderSize,
                    YStep - BorderSize);
                e.Graphics.FillRectangle(
                    _selectedBorderBrush,
                    XStart + XStep * (ByteCursorColumn + 1),
                    YStart + YStep * ByteCursorRow + BorderSize,
                    BorderSize,
                    YStep - BorderSize);
            }

            // Draw the input nybble if input is in progress as well.
            if (_pendingInput != null)
            {
                var cellInnerRect = new Rectangle();
                cellInnerRect.X = XStart + XStep * ByteCursorColumn + BorderSize;
                cellInnerRect.Y = YStart + YStep * ByteCursorRow + BorderSize;
                cellInnerRect.Width = TextPadding + CharSize.Width * 2 + TextPadding;
                cellInnerRect.Height = TextPadding + CharSize.Height + TextPadding;

                e.Graphics.DrawString(
                    _pendingInput.Value.ToString("X") + " ",
                    Font,
                    _inputBrush,
                    cellInnerRect,
                    strFormat);
            }
        }

        public void InvalidateCell(int col, int row)
        {
            if (col < 0 || col >= ColumnCount)
                throw new ArgumentException();
            if (row < 0 || row >= RowCount)
                throw new ArgumentException();

            var rect = new Rectangle();
            rect.X = XStart + XStep * col + AutoScrollPosition.X;
            rect.Y = YStart + YStep * row + AutoScrollPosition.Y;
            rect.Width = XStep + BorderSize;
            rect.Height = YStep + BorderSize;
            Invalidate(rect);
            if (_modeChunkName)
            {
                int cellByte = row * ColumnCount - FirstByteColumn + col;
                int cellByteChunkNameOfs = cellByte % 4;
                int cellByteWordStart = cellByte - cellByteChunkNameOfs;
                // temporarily disable this to prevent infinite recursion...
                _modeChunkName = false;
                // also update the remaining 3 cells in this word
                for (int i = 0; i < 4; ++i)
                {
                    if (i == cellByteChunkNameOfs)
                        continue;
                    int thisByte = cellByteWordStart + i;
                    int thisCol = (thisByte + FirstByteColumn) % ColumnCount;
                    int thisRow = (thisByte + FirstByteColumn) / ColumnCount;
                    InvalidateCell(thisCol, thisRow);
                }
                _modeChunkName = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _data = Memory<byte>.Empty;
            }

            base.Dispose(disposing);
        }

        private int? _pendingInput;

        public bool InputNybble(int value)
        {
            if (value < 0 || value > 0xF)
                throw new ArgumentException();

            // If edits are not allowed, fail now.
            if (DataChangeHandler == null)
                return false;

            // If resizing is not allowed, fail if trying to append.
            if (ByteCursor == Data.Length && !AllowResize)
                return false;

            if (_pendingInput == null)
            {
                // First half of the input (upper 4 bits).
                _pendingInput = value;
                InvalidateCell(ByteCursorColumn, ByteCursorRow);
                return true;
            }
            else
            {
                // Second half of the input (lower 4 bits).
                value |= _pendingInput.Value << 4;
                _pendingInput = null;

                if (ByteCursor == Data.Length)
                {
                    // Attempt to append.
                    bool ok = DataChangeHandler(ByteCursor, 0, new byte[] { (byte)value });
                    if (ok)
                    {
                        MoveBy(1); // this also invalidates the cell
                    }
                    return ok;
                }
                else
                {
                    // Attempt to overwrite.
                    bool ok = DataChangeHandler(ByteCursor, 1, new byte[] { (byte)value });
                    if (ok)
                    {
                        MoveBy(1); // this also invalidates the cell
                    }
                    return ok;
                }
            }
        }

        public bool InputNone()
        {
            // If edits are not allowed, fail now.
            if (DataChangeHandler == null)
                return false;

            // If resizing is not allowed, fail if trying to append.
            if (ByteCursor + 3 == Data.Length && !AllowResize)
                return false;

            // If cursor is not word-aligned
            while (ByteCursor % 4 != 0)
                MoveBy(-1);

            if (_pendingInput != null)
                _pendingInput = null;

            bool ok = DataChangeHandler(ByteCursor, 4, [Entry.NullEID & 0xFF, (Entry.NullEID >> 8) & 0xFF, (Entry.NullEID >> 16) & 0xFF, (Entry.NullEID >> 24) & 0xFF]);
            if (ok)
            {
                MoveBy(1);
                MoveBy(1);
                MoveBy(1);
                MoveBy(1);
            }

            ResetAnchor();
            Invalidate();
            return true;
        }

        public void ClearInput()
        {
            if (_pendingInput != null)
            {
                _pendingInput = null;
                InvalidateCell(ByteCursorColumn, ByteCursorRow);
            }
        }

        public bool InputZero(int length)
        {
            // If edits are not allowed, fail now.
            if (DataChangeHandler == null)
                return false;

            // If cursor is not word-aligned
            while (ByteCursor % 4 != 0 && length > 1)
                MoveBy(-1);

            if (_pendingInput != null)
                _pendingInput = null;

            for (int i = 0; i < length; ++i)
            {
                InputNybble(0);
                InputNybble(0);
            }

            ResetAnchor();
            Invalidate();
            return true;
        }

        public bool CopyBytes(bool cut, bool asEID)
        {
            if (ByteCursor == _data.Length)
                return false;

            int start = ByteCursor, end;
            if (ByteCursor <= ByteAnchor)
            {
                end = ByteAnchor;
            }
            else
            {
                end = ByteCursor;
                ByteCursor = ByteAnchor;
            }

            // If cursor is not word-aligned
            while (ByteCursor % 4 != 0)
                MoveBy(-1);

            if (_pendingInput != null)
                _pendingInput = null;

            int col = ByteCursorColumn, row = ByteCursorRow;

            StringBuilder sb = new StringBuilder();
            for (var i = ByteCursor; i <= end; i += 4)
            {
                var data = Data.Span;
                int cellByte = row * ColumnCount - FirstByteColumn + col;
                int cellByteChunkNameOfs = cellByte % 4;
                int offset = cellByte - cellByteChunkNameOfs;
                string str;
                if (asEID)
                {
                    int chunk = BitConv.FromInt32(data, offset);
                    str = Entry.EIDToEName(chunk);
                }
                else
                {
                    byte[] chunk = [data[offset], data[offset + 1], data[offset + 2], data[offset + 3]];
                    str = Convert.ToHexString(chunk);
                }
                sb.Append(str).Append("\n");
                if (cut)
                {
                    InputZero(4);
                }
                col += 4;
            }
            if (sb.Length > 0)
            {
                sb.Length--; // Remove the last "\n".
                Clipboard.SetDataObject(sb.ToString(), true, 10, 100);
            }

            if (!cut)
            {
                ByteCursor = start;
            }
            ResetAnchor();
            Invalidate();
            return true;
        }

        public bool PasteBytes(bool asEID)
        {
            if (DataChangeHandler == null)
                return false;

            while (ByteCursor % 4 != 0)
                MoveBy(-1);

            if (_pendingInput != null)
                _pendingInput = null;

            StringReader sr = new StringReader(Clipboard.GetText());
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (asEID)
                {
                    if (CheckEname(line).Length > 0)
                    {
                        int chunk = Entry.ENameToEID(line);
                        if (chunk == 1) chunk = 0;

                        int temp = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            if (i % 2 == 0)
                            {
                                temp = chunk & 0xF;
                                chunk >>= 4;
                                InputNybble(chunk & 0xF);
                            }
                            else
                            {
                                InputNybble(temp);
                                chunk >>= 4;
                            }
                        }
                    }
                }
                else
                {
                    if (IsHexString(line) && line.Length == 8)
                    {
                        byte[] chunk = Convert.FromHexString(line);
                        int chunkLength = chunk.Length;
                        for (int i = 0; i < chunkLength; i++)
                        {
                            InputNybble(chunk[i] >> 4);
                            InputNybble(chunk[i] & 0xF);
                        }
                        if (chunkLength % 4 != 0)
                        {
                            for (int i = 0; i < 4 - chunkLength % 4; i++)
                            {
                                InputNybble(0);
                                InputNybble(0);
                            }
                        }
                    }
                }
            }
            ResetAnchor();
            Invalidate();
            return true;
        }

        public static string CheckEname(string ename)
        {
            if (ename.Length != 5) return string.Empty;
            int eid = Entry.NullEID;
            try { eid = Entry.ENameToEID(ename); }
            catch (ArgumentException) { return string.Empty; }
            return ename;
        }

        public bool IsHexString(string value)
        {
            string hx = "0123456789ABCDEF";
            foreach (char c in value.ToUpper())
            {
                if (!hx.Contains(c))
                    return false;
            }
            return true;
        }

        private void CopyBytes_Click(object? sender, EventArgs e)
        {
            CopyBytes(false, false);
        }

        private void CutBytes_Click(object? sender, EventArgs e)
        {
            CopyBytes(true, false);
        }

        private void PasteBytes_Click(object? sender, EventArgs e)
        {
            PasteBytes(false);
        }

        private void CopyEID_Click(object? sender, EventArgs e)
        {
            CopyBytes(false, true);
        }

        private void CutEID_Click(object? sender, EventArgs e)
        {
            CopyBytes(true, true);
        }

        private void PasteEID_Click(object? sender, EventArgs e)
        {
            PasteBytes(true);
        }
    }
}
