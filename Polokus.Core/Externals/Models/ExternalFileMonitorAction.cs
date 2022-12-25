using Polokus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Externals.Models
{
    public class ExternalFileMonitorAction
    {
        public FileMonitor.FileEvtType Event { get; set; }
        public string Signal { get; set; } = string.Empty;
        public string Params { get; set; } = string.Empty;
    }
}
