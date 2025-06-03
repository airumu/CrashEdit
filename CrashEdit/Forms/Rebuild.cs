using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.CE.Properties;

namespace CrashEdit.CE.Forms
{
    public partial class RebuildForm : DarkForm
    {
        private OldMainForm owner;
        public string configFilePath { get; set; }

        private OpenFileDialog dlgOpenFileCfg = new OpenFileDialog();
        private OpenFileDialog dlgOpenFileExe = new OpenFileDialog();
        private FolderBrowserDialog dlgWorkingDir = new FolderBrowserDialog();
        private string workingDirectory = string.Empty;

        private bool RebuildRunning = false;
        private Process Process;

        private System.Windows.Forms.Timer checkArgsTimer;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (RebuildRunning)
            {
                e.Cancel = true;
                MessageBox.Show("Please wait for the rebuild process to complete or click Cancel.", "Rebuild in Progress", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            base.OnFormClosing(e);
        }

        public RebuildForm(OldMainForm ow)
        {
            owner = ow;
            InitializeComponentRebuild();

            this.Text = "Rebuild (c2export)";

            SearchForConfigFile();
            labelPathExeValue.Text = Settings.Default.C2ExportPath;

            dlgOpenFileCfg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dlgOpenFileCfg.Title = "Select a config file";
            dlgOpenFileCfg.FileName = configFilePath;

            dlgOpenFileExe.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            dlgOpenFileExe.Title = "Select c2export.exe";
            dlgOpenFileExe.FileName = Settings.Default.C2ExportPath;

            // Timer to check arguments every second
            checkArgsTimer = new System.Windows.Forms.Timer();
            checkArgsTimer.Interval = 2000;
            checkArgsTimer.Tick += (s, e) => CheckArgsValid();
            checkArgsTimer.Start();

            CheckArgsValid();
        }

        private void ShowWarning(string txt)
        {
            warningLabel.Visible = true;
            warningLabel.Text = txt;
        }

        public void UpdateConfigPath(string new_path)
        {
            configFilePath = new_path;
            labelPathCfgValue.Text = configFilePath;
            labelPathCfgValue.ForeColor = Color.White;
            CheckArgsValid();
        }

        private void CheckArgsValid()
        {
            //btnEditConfig.Enabled = !(string.IsNullOrEmpty(configFilePath) ||
            //                            !File.Exists(configFilePath));

            // make sure both c2export path and config file path are existing files
            if (string.IsNullOrEmpty(Settings.Default.C2ExportPath) || !File.Exists(Settings.Default.C2ExportPath))
            {
                btnRebuild.Enabled = false;
                ShowWarning("Path to c2export exe is not valid");
                return;
            }

            if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
            {
                btnRebuild.Enabled = false;
                ShowWarning("Path to rebuild arguments is not valid");
                return;
            }

            // make sure working directory is either valid or empty
            if (!string.IsNullOrEmpty(workingDirectory) && !Directory.Exists(workingDirectory))
            {
                btnRebuild.Enabled = false;
                ShowWarning("Working directory is not valid");
                return;
            }

            if (!RebuildRunning)
                btnRebuild.Enabled = true;

            warningLabel.Visible = false;
        }

        private void SearchForConfigFile()
        {
            var filename = owner.TabControl.SelectedTab?.Text;
            labelPathCfgValue.Text = "No config file autodetected";
            labelPathCfgValue.ForeColor = Color.Yellow;
            if (string.IsNullOrEmpty(filename))
                return;

            var parentPath = Path.GetDirectoryName(filename);
            if (string.IsNullOrEmpty(parentPath))
                return;

            var file = Directory.GetFiles(parentPath, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(f => Regex.IsMatch(Path.GetFileName(f), @"(args|rebuild|rebuilt)", RegexOptions.IgnoreCase))
                .FirstOrDefault<string>();

            if (!string.IsNullOrEmpty(file))
            {
                configFilePath = file;
                labelPathCfgValue.Text = configFilePath;
                labelPathCfgValue.ForeColor = Color.White;
            }
        }

        private void DoCfgPathDialog()
        {
            if (dlgOpenFileCfg.ShowDialog() == DialogResult.OK)
            {
                configFilePath = dlgOpenFileCfg.FileName;
                labelPathCfgValue.Text = configFilePath;
                labelPathCfgValue.ForeColor = Color.White;
            }
        }

        private void btnPathCfg_Click(object sender, EventArgs e)
        {
            DoCfgPathDialog();
            CheckArgsValid();
        }

        private void btnPathExe_Click(object sender, EventArgs e)
        {
            if (dlgOpenFileExe.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.C2ExportPath = dlgOpenFileExe.FileName;
                Settings.Default.Save();
                labelPathExeValue.Text = Settings.Default.C2ExportPath;
            }
            CheckArgsValid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (RebuildRunning)
            {
                Process?.Kill();
                outputLog.Text += "Rebuild cancelled by user." + Environment.NewLine;
                RebuildRunning = false;
            }
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            // run c2export with the config file
            outputLog.Text = string.Empty;

            bool did_err = false;
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(configFilePath, Encoding.UTF8);
            }
            catch (Exception)
            {
                did_err = true;
                fileContent = string.Empty;
            }

            if (string.IsNullOrEmpty(fileContent) || did_err)
            {
                ShowWarning("Error reading config file");
                return;
            }

            fileContent += Environment.NewLine;
            fileContent += "kill";
            fileContent += Environment.NewLine;

            Process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Settings.Default.C2ExportPath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = !string.IsNullOrEmpty(workingDirectory)
                                            ? workingDirectory
                                            : Path.GetDirectoryName(Settings.Default.C2ExportPath)
                },
                EnableRaisingEvents = true
            };

            outputLog.Text += "Running c2export with config file:" + Environment.NewLine;
            outputLog.Text += configFilePath + Environment.NewLine + Environment.NewLine;
            outputLog.Text += "File content:" + Environment.NewLine;
            outputLog.Text += fileContent + Environment.NewLine + Environment.NewLine;
            outputLog.Text += "Program output:" + Environment.NewLine + Environment.NewLine;

            Process.OutputDataReceived += (s, ea) =>
            {
                if (ea.Data != null)
                {
                    // Ensure UI update on the UI thread
                    outputLog.BeginInvoke(new Action(() =>
                    {
                        outputLog.AppendText(ea.Data + Environment.NewLine);
                    }));
                }
            };

            Process.Exited += (s, ea) =>
            {
                // Re-enable the button on process exit
                outputLog.BeginInvoke(new Action(() =>
                {
                    btnRebuild.Enabled = true;
                    btnCancel.Enabled = false;
                    RebuildRunning = false;
                }));

                if (Process.ExitCode != 0)
                {
                    Console.WriteLine($"!!! c2export exited with code {Process.ExitCode}");
                    ShowWarning($"!!! c2export exited with code {Process.ExitCode}");
                }
                else
                    Console.WriteLine("rebuild done :)");

                Process.Dispose();
            };

            // run
            try
            {
                Process.Start();
                btnRebuild.Enabled = false;
                btnCancel.Enabled = true;
                RebuildRunning = true;
                using (var writer = Process.StandardInput)
                {
                    writer.Write(fileContent);
                }
                Process.BeginOutputReadLine();
                Process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                outputLog.Text += $"Running process failed {ex.Message}";
                RebuildRunning = false;
            }
        }

        private void btnWorkingDir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(labelWorkingDirValue.Text))
                dlgWorkingDir.SelectedPath = labelWorkingDirValue.Text;
            else if (!string.IsNullOrEmpty(Settings.Default.C2ExportPath))
                dlgWorkingDir.SelectedPath = Path.GetDirectoryName(Settings.Default.C2ExportPath);

            if (dlgWorkingDir.ShowDialog() == DialogResult.OK)
            {
                workingDirectory = dlgWorkingDir.SelectedPath;
                labelWorkingDirValue.Text = workingDirectory;
            }
            else if (string.IsNullOrEmpty(labelWorkingDirValue.Text))
            {
                workingDirectory = string.Empty;
                labelWorkingDirValue.Text = "";
            }

            CheckArgsValid();
        }

        private void btnClearWorkingDir_Click(object sender, EventArgs e)
        {
            workingDirectory = string.Empty;
            labelWorkingDirValue.Text = "";
            CheckArgsValid();
        }

        private void btnMakeNewConfig_Click(object sender, EventArgs e)
        {
            var form = new RebuildConfig(this, null);
            form.ShowDialog(this);
        }

        private void btnEditConfig_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
            {
                DoCfgPathDialog();
                if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
                    return;
            }

            var form = new RebuildConfig(this, configFilePath);
            if (form.Cancelled)
                form.Close();
            else
                form.ShowDialog(this);
        }
    }
}
