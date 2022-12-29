using Polokus.Core;
using Polokus.Core.Interfaces;
using Polokus.Core.Services.OnPremise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Grpc.Core;
using Grpc.Net.Client;
using Polokus.Core.Remote;
using Polokus.Core.Services.Remote;
using Polokus.Service.Communication.Services;

namespace Polokus.Service
{
    public class PolokusService
    {
        public static PolokusMaster Master { get; } = new PolokusMaster();
        public static OnPremiseServicesProvider SP { get; } = new OnPremiseServicesProvider(Master);



        WebApplication _server;

        public PolokusService()
        {
            //_polokus = new PolokusMaster();
            //_servicesProvider = new OnPremiseServicesProvider(_polokus);

            _server = CreateGrpcServer();
            _server.Run();
        }

        private WebApplication CreateGrpcServer()
        {
            PrintHelper.PrintInfo("Initializing Grpc Server...");

            var builder = WebApplication.CreateBuilder();
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
            _server.Start();
        }
        public void Stop()
        {            
            _server.StopAsync();
        }

    }
}
