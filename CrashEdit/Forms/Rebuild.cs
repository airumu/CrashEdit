using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using DiscUtils.Iso9660;

namespace CrashEdit.CE.Forms
{
    public partial class RebuildForm : DarkForm
    {
        private OldMainForm owner;
        private string configFilePath = string.Empty;

        private OpenFileDialog dlgOpenFileCfg = new OpenFileDialog();
        private OpenFileDialog dlgOpenFileExe = new OpenFileDialog();

        private System.Windows.Forms.Timer checkArgsTimer;

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
            checkArgsTimer.Interval = 1000; // 1 second
            checkArgsTimer.Tick += (s, e) => CheckArgsValid();
            checkArgsTimer.Start();

            CheckArgsValid();
        }

        private void ShowWarning(string txt)
        {
            warningLabel.Visible = true;
            warningLabel.Text = txt;
        }

        private void CheckArgsValid()
        {
            // make sure both c2export path and config file path are existing files
            if (string.IsNullOrEmpty(Settings.Default.C2ExportPath) || !System.IO.File.Exists(Settings.Default.C2ExportPath))
            {
                btnRebuild.Enabled = false;
                ShowWarning("c2export path is not valid");
                return;
            }

            if (string.IsNullOrEmpty(configFilePath) || !System.IO.File.Exists(configFilePath))
            {
                btnRebuild.Enabled = false;
                ShowWarning("Config file path is not valid");
                return;
            }

            btnRebuild.Enabled = true;
            warningLabel.Visible = false;
        }

        private void SearchForConfigFile()
        {
            var filename = owner.TabControl.SelectedTab?.Text;
            labelPathCfgValue.Text = "No config file autodetected";
            if (string.IsNullOrEmpty(filename))
                return;

            var parentPath = System.IO.Path.GetDirectoryName(filename);
            if (string.IsNullOrEmpty(parentPath))
                return;

            var files = System.IO.Directory.GetFiles(parentPath, "*.txt", System.IO.SearchOption.TopDirectoryOnly)
                .Where(f => Regex.IsMatch(System.IO.Path.GetFileName(f), @"(args|rebuild|rebuilt)", RegexOptions.IgnoreCase))
                .ToList();

            if (files.Count > 0)
            {
                configFilePath = files[0];
                labelPathCfgValue.Text = configFilePath;
            }
        }

        private void btnPathCfg_Click(object sender, EventArgs e)
        {
            if (dlgOpenFileCfg.ShowDialog() == DialogResult.OK)
            {
                configFilePath = dlgOpenFileCfg.FileName;
                labelPathCfgValue.Text = configFilePath;
            }
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

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            // run c2export with the config file
            btnRebuild.Enabled = false;
            outputLog.Text = string.Empty;

            bool did_err = false;
            string fileContent;
            try
            {
                fileContent = System.IO.File.ReadAllText(configFilePath, Encoding.UTF8);
            }
            catch (Exception)
            {
                did_err = true;
                fileContent = string.Empty;
            }

            if (string.IsNullOrEmpty(fileContent) || did_err)
            {
                ShowWarning("Error reading config file");
                btnRebuild.Enabled = false;
                return;
            }

            fileContent += "\n";
            fileContent += "kill\n";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Settings.Default.C2ExportPath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = System.IO.Path.GetDirectoryName(Settings.Default.C2ExportPath)
                },
                EnableRaisingEvents = true
            };

            outputLog.Text += "Running c2export with config file:";
            outputLog.Text += Environment.NewLine + configFilePath + Environment.NewLine + Environment.NewLine;
            outputLog.Text += fileContent + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            outputLog.Text += "Program output:" + Environment.NewLine + Environment.NewLine;

            process.OutputDataReceived += (s, ea) =>
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

            process.Exited += (s, ea) =>
            {
                // Re-enable the button on process exit
                outputLog.BeginInvoke(new Action(() =>
                {
                    btnRebuild.Enabled = true;
                }));

                if (process.ExitCode != 0)
                    Console.WriteLine($"!!! c2export exited with EC {process.ExitCode}");
                else
                    Console.WriteLine("rebuild done :)");

                process.Dispose();
            };


            // run
            try
            {
                process.Start();
                using (var writer = process.StandardInput)
                {
                    writer.Write(fileContent);
                }
                process.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                outputLog.Text = $"Running process failed {ex.Message}";
            }
        }
    }
}
