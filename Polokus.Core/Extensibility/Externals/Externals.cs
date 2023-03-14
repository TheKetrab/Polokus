using Polokus.Core.Extensibility.Externals.Models;

namespace Polokus.Core.Extensibility.Externals
{
    public class Externals
    {
        public List<ExternalServiceTask> ServiceTasks { get; set; } = new();
        public List<ExternalMonitor> Monitors { get; set; } = new();
        public List<ExternalHooksProvider>? HooksProviders { get; set; }
        public ExternalSettingsProvider? SettingsProvider { get; set; }
    }
}
