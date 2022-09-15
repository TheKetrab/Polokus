using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public class BpmnContext
    {
        public tDefinitions? Definitions { get; set; }
        public List<BpmnProcess>? Processes { get; set; }
    }
}
