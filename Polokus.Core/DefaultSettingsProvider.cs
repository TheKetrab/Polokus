using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public class DefaultSettingsProvider : ISettingsProvider
    {
        public int MessageListenerPort => 8080;

        public string ServiceTasksExternals => "";
    }
}
