using CefSharp.WinForms;
using CefSharp;
using Polokus.App.Forms;
using Polokus.Core;

namespace Polokus.App
{
    internal static class Program
    {
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


            // CefSharp init
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            Application.Run(new MainWindow());


            // ----- EXIT -----
            // TODO: if enable logs == true w app config
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

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
        }
    }
}