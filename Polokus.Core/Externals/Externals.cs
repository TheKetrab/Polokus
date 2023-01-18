using Polokus.Core.Interfaces;
using Polokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polokus.Core.Externals.Models;

namespace Polokus.Core.Externals
{
    public class Externals
    {
        public List<ExternalServiceTask> ServiceTasks { get; set; } = new();
        public List<ExternalMonitor> Monitors { get; set; } = new();
        public List<ExternalHooksProvider>? HooksProviders { get; set; }
        public ExternalSettingsProvider? SettingsProvider { get; set; }
    }
}
