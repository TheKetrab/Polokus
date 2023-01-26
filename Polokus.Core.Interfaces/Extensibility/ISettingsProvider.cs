namespace Polokus.Core.Interfaces.Extensibility
{
    public interface ISettingsProvider
    {
        int MessageListenerPort { get; }
        string ServiceTasksExternals { get; }
        int TimeOutForProcessSec { get; }
        int DelayForNodeHandlerMs { get; }

    }
}
