using Polokus;
using System;

namespace Polokus.Core.Externals.Models
{
    public class ExternalFileMonitor
    {
        public string Path { get; set; } = string.Empty;
        public List<ExternalFileMonitorAction> Actions { get; set; } = new();
    }

}