namespace Polokus.Core.Extensibility
{
    public class DefaultSettingsProvider : ISettingsProvider
    {
        public int MessageListenerPort => 8080;

        public string ServiceTasksExternals => "";
        public int TimeOutForProcessSec => -1;
        public int DelayForNodeHandlerMs => 0;
    }
}
