using Grpc.Core;
using Grpc.Net.Client;
using Polokus.App.Forms;
using Polokus.App.Properties;
using Polokus.App.Utils;
using Polokus.App.Views;
using Polokus.Core;
using Polokus.Core.Interfaces.Communication;
using Polokus.Core.Services.OnPremise;
using Polokus.Core.Services.Remote;
using RemoteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App
{
    /// <summary>
    /// Static class that represents info about application mode and manages connection with Polokus.
    /// Single Polokus Application Client can have only one global Service Provider.
    /// </summary>
    public static class PolokusApp
    {
        public static string BpmnPath => Settings.Default.BpmnPath;
        public static MainWindow MainWindow => Program._mainWindow!;

        private static IServicesProvider? _servicesProvider;
        public static IServicesProvider ServicesProvider => _servicesProvider
            ?? throw new Exception("Service Provider not set!");

        private static AppHooksProvider? _appHooksProvider;
        public static AppHooksProvider AppHooksProvider => _appHooksProvider
            ?? throw new Exception("App Hooks Provider not set!");


        public static GrpcChannel? GrpcChannel { get; private set; }
        public static bool TunnelWorks { get; private set; } = false;
        public static AppMode ApplicationMode { get; private set; } = AppMode.None;

        private static System.Timers.Timer? _reconnector;
        private const int ReconnectorDelay = 500;

        public enum AppMode
        {
            None,
            Local,
            Remote
        }

        
        public static bool TryRegisterRemotePolokus()
        {
            CancelPreviousInfo();

            try
            {
                _servicesProvider = CreateRemotePolokus(out GrpcChannel channel);
                GrpcChannel = channel;
                TunnelWorks = true;
                ApplicationMode = AppMode.Remote;
                return true;
            }
            catch (RpcException)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates tunnel with Polokus Server.
        /// </summary>
        private static IServicesProvider CreateRemotePolokus(out GrpcChannel channel)
        {
            string uri = Properties.Settings.Default.RemotePolokusUri;
            uri = "http://localhost:3000"; // TODO
            channel = GrpcChannel.ForAddress(uri);
            var serviceProvider = new GrpcRemoteServiceProvider(channel);

            // test connection
            serviceProvider.PolokusService.GetWorkflowsIds();

            // send info that we are connected (once per sec)
            _reconnector = new System.Timers.Timer(ReconnectorDelay);
            _reconnector.Elapsed += (s, e) =>
            {
                if (!TunnelWorks)
                {
                    // create another tunnel
                    RegisterAppHooksProviderRemote();
                }
                serviceProvider.PolokusService.SetClientConnected();
            };
            _reconnector.AutoReset = true;
            _reconnector.Enabled = true;

            return serviceProvider;
        }

        public static bool TryRegisterLocalPolokus()
        {
            CancelPreviousInfo();
            _servicesProvider = CreateLocalPolokus();
            ApplicationMode = AppMode.Local;
            return true;
        }

        private static IServicesProvider CreateLocalPolokus()
        {
            PolokusMaster polokus = new PolokusMaster(true);
            var serviceProvider = new OnPremiseServicesProvider(polokus);
            return serviceProvider;
        }

        /// <summary>
        /// Resets info about connection and disables reconnector.
        /// </summary>
        private static void CancelPreviousInfo()
        {
            GrpcChannel = null;
            TunnelWorks = false;
            ApplicationMode = AppMode.None;
            if (_reconnector != null)
            {
                _reconnector.Enabled = false;
            }
        }


        private static void RegisterAppHooksProviderRemote()
        {
            Task.Run(async () =>
            {
                _appHooksProvider = new AppHooksProvider(MainWindow.MainPanel.ServiceView);
                var hooksClient = new RemoteHooksService.RemoteHooksServiceClient(GrpcChannel);
                using (var call = hooksClient.WaitForEvents(new Empty()))
                {
                    CancellationToken ct = new();
                    TunnelWorks = true;
                    while (await call.ResponseStream.MoveNext(ct))
                    {
                        var current = call.ResponseStream.Current;
                        CallAppHooksProvider(_appHooksProvider, current);
                    }
                    TunnelWorks = false;
                }
            });
        }
        private static void RegisterAppHooksProviderLocal()
        {
            _appHooksProvider = new AppHooksProvider(MainWindow.MainPanel.ServiceView);
            ServicesProvider.PolokusService.RegisterHooksProvider(_appHooksProvider);
        }

        public static void RegisterAppHooksProvider()
        {
            if (_appHooksProvider != null)
            {
                ServicesProvider.PolokusService.DeregisterHooksProvider(_appHooksProvider);
                _appHooksProvider = null;
            }

            switch (ApplicationMode)
            {
                case AppMode.Remote:
                    RegisterAppHooksProviderRemote();
                    break;
                case AppMode.Local:
                    RegisterAppHooksProviderLocal();
                    break;
                default:
                    throw new Exception("None application mode.");
            }
        }

        private static void CallAppHooksProvider(AppHooksProvider hooksProvider, HookReply reply)
        {
            switch (reply.Type)
            {
                case HookType.OnProcessFinished:
                    hooksProvider.OnProcessFinished(reply.WfId, reply.PiId, reply.Args[0]);
                    break;
                case HookType.AfterExecuteNodeSuccess:
                    hooksProvider.AfterExecuteNodeSuccess(reply.WfId, reply.PiId, reply.NodeId, int.Parse(reply.Args[0]));
                    break;
                case HookType.AfterExecuteNodeFailure:
                    hooksProvider.AfterExecuteNodeFailure(reply.WfId, reply.PiId, reply.NodeId, int.Parse(reply.Args[0]));
                    break;
                case HookType.AfterExecuteNodeSuspension:
                    hooksProvider.AfterExecuteNodeSuspension(reply.WfId, reply.PiId, reply.NodeId, int.Parse(reply.Args[0]));
                    break;
                case HookType.OnTimeout:
                    hooksProvider.OnTimeout(reply.WfId, reply.PiId);
                    break;
                case HookType.OnTasksChanged:
                    hooksProvider.OnTasksChanged(reply.WfId, reply.PiId);
                    break;
                case HookType.OnCallerChanged:
                    hooksProvider.OnCallerChanged(reply.Args[0], reply.Args[1]);
                    break;
                case HookType.OnStatusChanged:
                    hooksProvider.OnStatusChanged(reply.WfId, reply.PiId);
                    break;
                case HookType.BeforeStartNewSequence:
                    hooksProvider.BeforeStartNewSequence(reply.WfId, reply.PiId, reply.NodeId, FromSaveString(reply.Args[0]));
                    break;
                case HookType.BeforeExecuteNode:
                    hooksProvider.BeforeExecuteNode(reply.WfId, reply.PiId, reply.NodeId, int.Parse(reply.Args[0]), FromSaveString(reply.Args[1]));
                    break;
                case HookType.OnAwaitingTokenCreated:
                    hooksProvider.OnAwaitingTokenCreated(reply.WfId, reply.PiId, reply.NodeId, reply.Args[0]);
                    break;
                default:
                    throw new Exception($"Unknown HookType: {reply.Type}");
            }


        }


        private static string? FromSaveString(string str)
        {
            if (string.Equals(str, "null")) return null;
            return str;
        }



    }




}
