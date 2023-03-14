using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.App.Utils
{
    public class AppSettingsProvider : ISettingsProvider
    {
        public int MessageListenerPort => Settings.MessageListenerPort;
        public string ServiceTasksExternals => Settings.ExternalsPath ?? "";
        public int TimeOutForProcessSec => Settings.TimeoutForProcessSec;
        public int DelayForNodeHandlerMs => Settings.DelayPerNodeHandlerMs;
    }

}