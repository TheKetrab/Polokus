using CefSharp.WinForms;
using CefSharp;
using Polokus.App.Forms;
using Polokus.Core.Helpers;

namespace Polokus.App
{
    internal static class Program
    {
        public static MainWindow MainWindow;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();



            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Load Settings
            
            if (string.IsNullOrEmpty(Properties.Settings.Default.BpmnPath))
            {
                Properties.Settings.Default.BpmnPath = Path.Combine(
                    Directory.GetCurrentDirectory(), "BPMN");
                
                Properties.Settings.Default.Save();
            }

            // CefSharp init
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            MainWindow = new MainWindow();
            Application.Run(MainWindow);


            // ----- EXIT -----
            if (Properties.Settings.Default.EnableLogs)
            {
                PrintLogs();
            }

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var dialog = new ErrorDialog(e.Exception);
            dialog.ShowDialog(MainWindow);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var dialog = new ErrorDialog((Exception)e.ExceptionObject);
            dialog.ShowDialog(MainWindow);

        }

        static void PrintLogs()
        {
            string logsPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
            }
            string filename = $"PolokusLog_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.txt";
            string logText = Logger.Global.GetFullLog(true);
            if (!string.IsNullOrEmpty(logText))
            {
                File.WriteAllText(Path.Combine(logsPath, filename), logText);
            }
        }
    }
}