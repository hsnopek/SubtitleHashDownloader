using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SubtitleDownloader
{
    static class Program
    {
        static Mutex mutex = new Mutex(false, "com.hsnopek.subtitledownloader");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            if (!mutex.WaitOne(TimeSpan.FromSeconds(0.5), false))
            {
                MessageBox.Show("Aplikacija je već pokrenuta!");
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(args));
            }
            finally { mutex.ReleaseMutex(); }
        }
    }
}
