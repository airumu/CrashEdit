using System.ComponentModel;
using System.Media;
using System.Text;
using AltUI.Forms;
using CrashEdit.CE.Properties;
using DiscUtils.Iso9660;

namespace CrashEdit.CE.Forms
{
    public partial class MakeBin : DarkForm
    {
        private MakeBin makebin;
        private OldMainForm owner;

        private FolderBrowserDialog dlgMakeBINDir = new FolderBrowserDialog();
        private SaveFileDialog dlgMakeBINFile = new SaveFileDialog();

        private BackgroundWorker bgwMakeBIN;

        private bool isprogressing;
        private bool automake;

        public MakeBin(OldMainForm oldmainform, bool auto)
        {
            InitializeComponent();
            owner = oldmainform;
            automake = auto;
            makebin = this;
            makebin.isprogressing = false;
            FormClosing += (sender, e) =>
            {
                if (makebin.isprogressing)
                {
                    e.Cancel = true;
                    SystemSounds.Asterisk.Play();
                }
            };

            bgwMakeBIN = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            bgwMakeBIN.DoWork += new DoWorkEventHandler(bgwMakeBIN_DoWork);
            bgwMakeBIN.ProgressChanged += new ProgressChangedEventHandler(bgwMakeBIN_ProgressChanged);
            bgwMakeBIN.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwMakeBIN_RunWorkerCompleted);
            dlgMakeBINFile.Filter = "Playstation Disc Images (*.bin)|*.bin";

            dpdRegion.Items.Add("no region");
            dpdRegion.Items.Add("NTSC-U/C");
            dpdRegion.Items.Add("PAL");
            dpdRegion.Items.Add("NTSC-J");
            dpdRegion.SelectedIndex = Settings.Default.MakeBinRegion;
            dpdRegion.SelectedIndexChanged += (sender, e) =>
            {
                Settings.Default.MakeBinRegion = dpdRegion.SelectedIndex;
                Settings.Default.Save();
            };

            lblPath.Text = Settings.Default.MakeBinPath;
            lblSavePath.Text = Settings.Default.MakeBinSavePath;
            dlgMakeBINDir.SelectedPath = Settings.Default.MakeBinPath;
            dlgMakeBINFile.FileName = Settings.Default.MakeBinSavePath;
            UpdatebtnMakeBin();

            if (automake && btnMakeBin.Enabled == true)
                btnMakeBin_Click(this, EventArgs.Empty);
            else
                automake = false;
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (dlgMakeBINDir.ShowDialog(this) != DialogResult.OK)
                return;
            string cnffile = Path.Combine(dlgMakeBINDir.SelectedPath, "SYSTEM.CNF");
            string exefile = Path.Combine(dlgMakeBINDir.SelectedPath, "PSX.EXE");
            if (!File.Exists(cnffile) && !File.Exists(exefile))
            {
                if (DarkMessageBox.ShowWarning(Resources.MakeBIN_NoSystemFiles, Resources.MakeBIN_Title, DarkDialogButton.YesNo) != DialogResult.Yes)
                    return;
            }
            lblPath.Text = dlgMakeBINDir.SelectedPath;
            Settings.Default.MakeBinPath = dlgMakeBINDir.SelectedPath;
            Settings.Default.Save();
            UpdatebtnMakeBin();
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            if (dlgMakeBINFile.ShowDialog(this) != DialogResult.OK)
                return;
            lblSavePath.Text = dlgMakeBINFile.FileName;
            Settings.Default.MakeBinSavePath = dlgMakeBINFile.FileName;
            Settings.Default.Save();
            UpdatebtnMakeBin();
        }

        private void UpdatebtnMakeBin()
        {
            if (dlgMakeBINDir.SelectedPath != string.Empty && dlgMakeBINFile.FileName != string.Empty)
                btnMakeBin.Enabled = true;
        }

        private void btnMakeBin_Click(object sender, EventArgs e)
        {
            var fs = new CDBuilder();
            AddDirectoryToISO(fs, "", new DirectoryInfo(dlgMakeBINDir.SelectedPath));
            MakeBinWithProgressBar(fs, dlgMakeBINFile.FileName);
        }

        void AddDirectoryToISO(CDBuilder fs, string prefix, DirectoryInfo dir)
        {
            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                AddDirectoryToISO(fs, $"{prefix}{subdir.Name}\\", subdir);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                fs.AddFile($"{prefix}{file.Name};1", file.FullName);
            }
        }

        private void bgwMakeBIN_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            CDBuilder fs = (CDBuilder)args[0];
            string filename = (string)args[1];
            while (!makebin.isprogressing) ;
            using (FileStream output = new FileStream(filename, FileMode.Create, FileAccess.Write))
            using (Stream input = fs.Build())
            {
                ISO2PSX.Run(input, output, bgwMakeBIN);
            }
        }

        private void bgwMakeBIN_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgProgress.Value = e.ProgressPercentage;
        }

        private void bgwMakeBIN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            makebin.isprogressing = false;

            bool haserror = false;
            var log = new StringBuilder();
            log.AppendLine(Resources.MakeBIN_NoRegOK);
            log.AppendLine();

            string cueFilename = Path.ChangeExtension(dlgMakeBINFile.FileName, ".cue");
            if (!File.Exists(cueFilename))
            {
                try
                {
                    using (var cue = new StreamWriter(cueFilename))
                    {
                        cue.WriteLine($"FILE \"{Path.GetFileName(dlgMakeBINFile.FileName)}\" BINARY");
                        cue.WriteLine("  TRACK 01 MODE2/2352");
                        cue.WriteLine("    INDEX 01 00:00:00");
                    }
                    log.AppendLine(Resources.MakeBIN_CueSuccess);
                    log.AppendLine();
                }
                catch (IOException ex)
                {
                    log.AppendLine(string.Format(Resources.MakeBIN_CueFail, ex));
                    log.AppendLine();
                    haserror = true;
                }
            }
            else
            {
                log.AppendLine(Resources.MakeBIN_CueExists);
                log.AppendLine();
            }

            if (dpdRegion.SelectedIndex > 0)
            {
                string imprintOpt = string.Empty;
                if (dpdRegion.SelectedIndex == 1)
                {
                    imprintOpt = ":cdxa-imprint --psx-scea";
                }
                else if (dpdRegion.SelectedIndex == 2)
                {
                    imprintOpt = ":cdxa-imprint --psx-scee";
                }
                else if (dpdRegion.SelectedIndex == 3)
                {
                    imprintOpt = ":cdxa-imprint --psx-scei";
                }
                log.AppendLine(Resources.MakeBIN_DRNSF_Launch);
                try
                {
                    if (ExternalTool.Invoke("drnsf", $"{imprintOpt} -- \"{dlgMakeBINFile.FileName}\"") != 0)
                    {
                        log.AppendLine(Resources.MakeBIN_DRNSF_Error);
                        log.AppendLine();
                        haserror = true;
                    }
                    else
                    {
                        log.AppendLine(Resources.MakeBIN_DRNSF_Success);
                        log.AppendLine();
                    }
                }
                catch (FileNotFoundException)
                {
                    log.AppendLine(Resources.MakeBIN_DRNSF_Unavailable);
                    log.AppendLine();
                    haserror = true;
                }
                catch (Exception ex)
                {
                    log.AppendLine(string.Format(Resources.MakeBIN_DRNSF_Fail, ex));
                    log.AppendLine();
                    haserror = true;
                }
            }
            log.Append(Resources.Done);

            //DarkMessageBox.ShowMessage(log.ToString(), Resources.MakeBIN_Title);
            Console.WriteLine(log.ToString());
            if (haserror)
                SystemSounds.Hand.Play();
            else
                SystemSounds.Asterisk.Play();

            pnOptions.Enabled = 
            owner.Enabled = true;

            if (automake) Close(); 
        }

        internal void MakeBinWithProgressBar(CDBuilder fs, string filename)
        {
            makebin.isprogressing = true;
            prgProgress.Value = 0;
            pnOptions.Enabled =
            owner.Enabled = false;
            bgwMakeBIN.RunWorkerAsync(new object[] { fs, filename });
        }
    }
}
