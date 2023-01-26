using Polokus.Core.Interfaces.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
