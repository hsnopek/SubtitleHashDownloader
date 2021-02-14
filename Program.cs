using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubtitleDownloader.Data.Client;
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

                var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Main>();
                    services.AddSingleton<ISubtitleClient, OpenSubtitlesClient>();
                });
                var host = builder.Build();

                using (var serviceScope = host.Services.CreateScope())
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var mainForm = serviceScope.ServiceProvider.GetRequiredService<Main>();
                    Application.Run(mainForm);
                }
            }
            finally { mutex.ReleaseMutex(); }
        }
    }
}
