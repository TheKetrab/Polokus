using Grpc.Core;
using log4net;
using Polokus.Core;
using Polokus.Core.Communication.Services.OnPremise;
using Polokus.Service.Communication.Services;

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


        WebApplication _server;

        public PolokusService()
        {
            _server = CreateGrpcServer();
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
            PrintHelper.PrintInfo("Service started.");
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
