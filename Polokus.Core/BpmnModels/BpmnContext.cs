using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Models
{
    public class BpmnContext : IBpmnContext
    {
        public tDefinitions? Definitions { get; set; }
        public IContextInstance? ContextInstance { get; }

        public IEnumerable<IBpmnProcess> BpmnProcesses { get; private set; }

        public BpmnContext()
        {
            BpmnProcesses = new List<IBpmnProcess>();
        }

        public void SetBpmnProcesses(IEnumerable<IBpmnProcess> bpmnProcesses)
        {
            if (BpmnProcesses.Any())
            {
                throw new Exception("Forbidden operation. BpmnProcesses are already read.");
            }

            BpmnProcesses = bpmnProcesses;
        }


    }
}
