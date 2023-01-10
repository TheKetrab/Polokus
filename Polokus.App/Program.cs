using CefSharp.WinForms;
using CefSharp;
using Polokus.App.Forms;
using Polokus.Core.Helpers;
using Grpc.Net.Client;
using Polokus.App.Utils;
using Polokus.Core.Services.OnPremise;
using Polokus.Core.Services.Remote;
using Polokus.Core;
using Polokus.Core.Services.Interfaces;
using Grpc.Core;
using Polokus.App.Views;

namespace Polokus.App
{
    internal static class Program
    {
        public static MainWindow MainWindow;
        public static IServicesProvider SP;
        public static GrpcChannel? GrpcChannel;
        public static bool TunnelWorks = false;

        public enum AppMode
        {
            Local,
            Remote
        }

        public static AppMode ApplicationMode;

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

            bool success = InitializeMaster();
            if (success)
            {
                MainWindow = new MainWindow();
                Application.Run(MainWindow);
            }


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

        static bool InitializeMaster()
        {
            if (Properties.Settings.Default.UseRemotePolokus)
            {
                try
                {
                    RegisterRemotePolokus();
                    return true;
                }
                catch (RpcException e)
                {
                    GrpcChannel = null;

                    var result = MessageBox.Show("Failed to connect to remote server. Do you want to start PolokusMaster within GUI App?", "Error", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        RegisterLocalPolokus();
                        return true;
                    }   
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                RegisterLocalPolokus();
                return true;
            }
        }

        static void RegisterRemotePolokus()
        {
            string uri = Properties.Settings.Default.RemotePolokusUri;
            uri = "http://localhost:3000";
            //const string uri = "https://localhost:5022";
            var channel = GrpcChannel.ForAddress(uri);
            GrpcChannel = channel;
            SP = new GrpcRemoteServiceProvider(channel);
            
            // test connection
            SP.PolokusService.GetWorkflowsIds();


            ApplicationMode = AppMode.Remote;

            // send info that we are connected (once per sec)
            var timer = new System.Timers.Timer(500);
            timer.Elapsed += (s, e) => 
            {
                if (!TunnelWorks) {
                    // create another tunnel
                    ServiceView.SV?.RegisterAppHooksProviderRemote();
                }
                SP.PolokusService.SetClientConnected(); 
            };
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        static void RegisterLocalPolokus()
        {
            PolokusMaster polokus = new PolokusMaster(true);
            SP = new OnPremiseServicesProvider(polokus);

            ApplicationMode = AppMode.Local;
        }
    }
}