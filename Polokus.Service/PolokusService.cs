using Grpc.Core;
using log4net;
using Polokus.Core;
using Polokus.Core.Communication.Services.OnPremise;
using Polokus.Core.Interfaces;
using Polokus.Service.Communication.Services;
using System.Text;

namespace Polokus.Service
{
    public class PolokusService
    {
        public static PolokusMaster Master { get; } = new PolokusMaster();
        public static OnPremiseServicesProvider SP { get; } = new OnPremiseServicesProvider(Master);

        private static readonly ILog _log = LogManager.GetLogger(typeof(PolokusService));

        public static void Proxy(object request, ServerCallContext context)
        {
            Master.ConnectionManager.SetHaveConnection();
        }


        private WebApplication _server;
        private System.Timers.Timer _piLoggingTimer;

        public PolokusService()
        {
            _server = CreateGrpcServer();
            _piLoggingTimer = new System.Timers.Timer();
            InitPiLoggingTimer();
        }

        private void InitPiLoggingTimer()
        {
            _piLoggingTimer.Interval = 1000;
            _piLoggingTimer.Start();
            _piLoggingTimer.AutoReset = true;
            _piLoggingTimer.Enabled = true;
            _piLoggingTimer.Elapsed += (s, e) =>
            {
                try
                {
                    var runningProcessInstances = Master.GetWorkflows().ToList()
                        .SelectMany(x => x.ProcessInstances.GetAll()).ToList();
                    PrintHelper.PrintInfo($"Running instances: {runningProcessInstances.Count()}");
                }
                catch (Exception ex)
                {
                    PrintHelper.PrintInfo(ex.Message);
                    PrintHelper.PrintInfo(ex.StackTrace ?? "");
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        PrintHelper.PrintInfo("---");
                        PrintHelper.PrintInfo(ex.Message);
                        PrintHelper.PrintInfo(ex.StackTrace ?? "");
                    }
                }
            };
        }

        private WebApplication CreateGrpcServer()
        {
            PrintHelper.PrintInfo("Initializing Grpc Server...");

            var builder = WebApplication.CreateBuilder();

            // Service listens on http://localhost:3000
            // --> to communicate with gRPC by http, server must enforce HTTP2 communication
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.ListenLocalhost(3000, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                });
            });

            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GrpcLogsService>();
            app.MapGrpcService<GrpcPolokusService>();
            app.MapGrpcService<GrpcProcessInstanceService>();
            app.MapGrpcService<GrpcWorkflowsService>();
            app.MapGrpcService<GrpcHooksService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            PrintHelper.PrintInfo("Grpc Server initialized.");

            return app;
        }

        public void Start()
        {
            Task.Run(() => StartServer());
            Master.LoadWorkflows();

            var workflows = Master.GetWorkflows();
            foreach (var wf in workflows)
            {
                PrintHelper.PrintInfo($"Loaded workflow: {wf.Id}");
            }

            if (Settings.RestoreProcessesOnStart)
            {
                Master.RestoreNotFinishedProcesses();
            }


            PrintHelper.PrintInfo("Service started.");

            if (Master.HooksManager.GetHooksProviders().Count() > 0)
            {
                StringBuilder sb = new StringBuilder("Registered Hooks Providers:");
                sb.AppendLine();
                foreach (var hp in Master.HooksManager.GetHooksProviders())
                {
                    sb.AppendLine(hp.GetType().FullName);
                }
                PrintHelper.PrintInfo(sb.ToString());
            }
            else
            {
                PrintHelper.PrintInfo("Registered Hooks Providers: 0");
            }

            if (!string.IsNullOrEmpty(Settings.OnStartFunctions))
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(1000); // 1s
                        PrintHelper.PrintInfo("On Start Functions - starting.");
                        DoOnStartFunctions();
                        PrintHelper.PrintInfo("On Start Functions run succeed.");
                    }
                    catch (Exception e)
                    {
                        PrintHelper.PrintInfo("!!! ERROR while processing on start functions." + e.Message);
                    }
                });
            }

        }

        private void DoOnStartFunctions()
        {
            string[] functions = Settings.OnStartFunctions.Split('#');

            foreach (var f in functions)
            {
                string funcname = f.Substring(0, f.IndexOf('('));
                string argspart = f[(f.IndexOf('(') + 1)..(f.IndexOf(')'))];

                string[][] args = argspart.Split(',')
                    .Select(x => x.Split(':', StringSplitOptions.TrimEntries))
                    .ToArray();

                switch (funcname)
                {
                    case "StartProcessManually":
                        {
                            if (args[0][0] == "wfId" && args[1][0] == "piId" && args[2][0] == "cnt")
                            {
                                string wfId = args[0][1];
                                string bpmnProcessId = args[1][1];
                                int count = int.Parse(args[2][1]);

                                var workflow = Master.GetWorkflow(wfId);
                                for (int i = 0; i < count; i++)
                                {
                                    workflow.StartProcessManually(bpmnProcessId);
                                }

                            }
                            else
                            {
                                throw new Exception("Invalid argument names.");
                            }
                            break;
                        }
                    default:
                        throw new Exception();
                }
            }

        }

        private async Task StartServer()
        {
            try
            {
                await _server.StartAsync();
                _log.Info($"Server is listening on: {string.Join(" , ", _server.Urls)}");
            }
            catch (Exception e)
            {
                _log.Error("Failed to start server.", e);
            }
        }

        public async void Stop()
        {
            PrintHelper.PrintInfo("Service stopped.");
            await _server.StopAsync();
            _log.Info($"Server is no more listening.");
        }

    }

}
