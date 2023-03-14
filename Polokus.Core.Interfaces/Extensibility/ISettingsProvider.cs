namespace Polokus.Core.Interfaces.Extensibility
{
    public interface ISettingsProvider
    {
        string BpmnPath { get; set; }
        bool RestoreProcessesOnStart { get; set; }
        int MessageListenerPort { get; set; }
        string ExternalsPath { get; set; }
        int TimeoutForProcessSec { get; set; }
        int DelayPerNodeHandlerMs { get; set; }
        bool UseRemotePolokus { get; set; }
        string RemotePolokusUri { get; set; }
        bool LightMode { get; set; }
        string OnStartFunctions { get; set; }
        bool SerializePiSnapshots { get; set; }

    }
}
