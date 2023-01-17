using Polokus;
using System;

namespace Polokus.Core.Externals.Models
{
    public class ExternalMonitor
    {
        public string Assembly { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string[] Arguments { get; set; } = new string[] { };
        public string[] Dependencies { get; set; } = new string[] { };
    }

}