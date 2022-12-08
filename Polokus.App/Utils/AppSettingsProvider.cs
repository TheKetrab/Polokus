using Polokus.Core.Interfaces;
using System;

namespace Polokus.App.Utils
{
    public class AppSettingsProvider : ISettingsProvider
    {
        public int MessageListenerPort => Properties.Settings.Default.MessageListenerPort;
        public string ServiceTasksExternals => Properties.Settings.Default.ServiceTasksExternals;
        public int TimeOutForProcessSec => Properties.Settings.Default.TimeoutForProcessSec;
        public int DelayForNodeHandlerMs => Properties.Settings.Default.DelayPerNodeHandlerMs;
    }

}