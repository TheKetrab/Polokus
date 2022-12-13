namespace Polokus.Core.Interfaces
{
    public interface ISettingsProvider
    {
        int MessageListenerPort { get; }
        string ServiceTasksExternals { get; }
        int TimeOutForProcessSec { get; }
        int DelayForNodeHandlerMs { get; }

    }
}
