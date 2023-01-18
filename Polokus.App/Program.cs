using CefSharp;
using CefSharp.WinForms;
using Polokus.App.Forms;
using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;

namespace Polokus.App
{
    internal static class Program
    {
        internal static MainWindow? _mainWindow;

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
            
            if (string.IsNullOrEmpty(Settings.BpmnPath))
            {
                Settings.BpmnPath = Path.Combine(
                    Directory.GetCurrentDirectory(), "BPMN");
            }

            // CefSharp init
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            bool success = InitializeMaster();
            if (success)
            {
                _mainWindow = new MainWindow();
                PolokusApp.RegisterAppHooksProvider();
                Application.Run(_mainWindow);
            }


            // ----- EXIT -----
            if (Settings.EnableLogs)
            {
                PrintLogs();
            }

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var dialog = new ErrorDialog(e.Exception);
            dialog.ShowDialog(PolokusApp.MainWindow);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var dialog = new ErrorDialog((Exception)e.ExceptionObject);
            dialog.ShowDialog(PolokusApp.MainWindow);

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

        private static bool InitializeMaster()
        {
            if (Settings.UseRemotePolokus)
            {
                bool success = PolokusApp.TryRegisterRemotePolokus();
                if (success)
                {
                    return true;
                }

                var result = MessageBox.Show(
                    "Failed to connect to remote server. Do you want to start PolokusMaster within GUI App?", "Connection error",
                    MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    return PolokusApp.TryRegisterLocalPolokus();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return PolokusApp.TryRegisterLocalPolokus();
            }
        }
    }
}