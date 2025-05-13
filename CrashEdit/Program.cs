using CrashEdit.Crash;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CrashEdit.CE
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetDllDirectory(string path);

        public static GLViewerLoader TopLevelGLViewer { get; set; } = null;

        [STAThread]
        internal static void Main(string[] args)
        {

            AllocConsole();

            string nsdSavePath = null;
            foreach (var arg in args)
            {
                if (arg.StartsWith("/c2_nsd_patch=", StringComparison.OrdinalIgnoreCase))
                {
                    nsdSavePath = arg.Substring("/c2_nsd_patch=".Length).Trim('"');
                    break;
                }
            }            

            PlatformID pid = Environment.OSVersion.Platform;
#if __MonoCS__
            if (pid != PlatformID.Unix && pid != PlatformID.MacOSX)
#else
            if (pid != PlatformID.Unix)
#endif
            {
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                path = Path.Combine(path, IntPtr.Size == 8 ? "Win64" : "Win32");

                if (!SetDllDirectory(path))
                    throw new System.ComponentModel.Win32Exception();
            }

            Registrar.Init();
            Registrar.RegisterAssembly(typeof(Program).Assembly);

            if (Properties.Settings.Default.UpgradeSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeSettings = false;
                Properties.Settings.Default.Save();
            }
            try
            {
                Properties.Resources.Culture = CrashUI.Properties.Resources.Culture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            }
            catch
            {
                Properties.Settings.Default.Language = "en";
            }
            if (Properties.Settings.Default.DefaultFormW < 640)
                Properties.Settings.Default.DefaultFormW = 640;
            if (Properties.Settings.Default.DefaultFormH < 480)
                Properties.Settings.Default.DefaultFormH = 480;
            Properties.Settings.Default.Save();
            EntityVisual.LoadMaps();
            EntityVisual.SaveMaps();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (OldMainForm mainform = new OldMainForm())
            using (ErrorReporter errorform = new ErrorReporter(mainform))
            {
                FileUtil.Owner = mainform;
                TopLevelGLViewer = new GLViewerLoader();
                mainform.Controls.Add(TopLevelGLViewer);
                Application.SetColorMode(SystemColorMode.Dark);


                if (!string.IsNullOrEmpty(nsdSavePath))
                {
                    Console.WriteLine($"!!! nsd save: {nsdSavePath}");
                    try
                    {
                        byte[] nsfdata = File.ReadAllBytes(nsdSavePath);                        
                        NSF nsf = NSF.LoadAndProcess(nsfdata, GameVersion.Crash2);
                        mainform.OpenNSF(nsdSavePath, nsf, GameVersion.Crash2);                        
                        mainform.PatchNSD(true);
                        mainform.SaveNSF(true);
                        Console.WriteLine("!!! nsd resave done");
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine("!!!", ex.Message);
                        return;
                    }
                }

                Application.Run(mainform);
            }            

            FreeConsole();
        }
    }
}
