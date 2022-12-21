using Polokus.Core.Interfaces;
using Polokus.Monitors.FileMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Externals
{
    public class Externals
    {
        public List<WorkflowExternal> Workflows { get; set; } = new();
        public List<FileMonitor> FileMonitors { get; set; } = new();
        public HooksProviderExternal? HooksProvider { get; set; }
        public SettingsProviderExternal? SettingsProvider { get; set; }
    }
}
