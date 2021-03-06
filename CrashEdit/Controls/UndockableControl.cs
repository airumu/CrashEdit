using CrashEdit.Properties;
using DarkUI.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace CrashEdit
{
    public sealed class UndockableControl : UserControl
    {
        private Control control;
        private Form form;

        public UndockableControl(Control control)
        {
            this.control = control;
            form = null;
            Dock = control.Dock;
            control.Dock = DockStyle.Fill;
            Controls.Add(control);
            BackColor = Color.FromArgb(35, 35, 38);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (Settings.Default.NewKeyBinds)
            {
                switch (keyData)
                {
                    case Keys.Q:
                        return true;
                }
            }
            else
            {
                switch (keyData)
                {
                    case Keys.D:
                        return true;
                }
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (Settings.Default.NewKeyBinds)
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        if (form == null)
                        {
                            form = new DarkForm
                            {
                                Text = "Undocked Control",
                                Width = Width,
                                Height = Height,
                                FormBorderStyle = FormBorderStyle.SizableToolWindow
                            };
                            Controls.Remove(control);
                            form.Controls.Add(control);
                            form.FormClosed += delegate (object sender, FormClosedEventArgs ee)
                            {
                                form.Controls.Remove(control);
                                Controls.Add(control);
                                form = null;
                            };
                            form.Show();
                        }
                        else
                        {
                            form.Close();
                        }
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        if (form == null)
                        {
                            form = new DarkForm
                            {
                                Text = "Undocked Control",
                                Width = Width,
                                Height = Height,
                                FormBorderStyle = FormBorderStyle.SizableToolWindow
                            };
                            Controls.Remove(control);
                            form.Controls.Add(control);
                            form.FormClosed += delegate (object sender, FormClosedEventArgs ee)
                            {
                                form.Controls.Remove(control);
                                Controls.Add(control);
                                form = null;
                            };
                            form.Show();
                        }
                        else
                        {
                            form.Close();
                        }
                        break;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg,Keys keyData)
        {
            if (IsInputKey((Keys)msg.WParam))
            {
                OnKeyDown(new KeyEventArgs((Keys)msg.WParam));
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg,keyData);
            }
        }

        protected override void Dispose(bool disposing)
        {
            control.Dispose();
            if (form != null)
            {
                form.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
