using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;

namespace Polokus.Core.Models
{
    public class BpmnContext : IBpmnContext
    {
        public string? RawString { get; set; }
        public IContextInstance? ContextInstance { get; }
        public IEnumerable<IBpmnProcess> BpmnProcesses { get; private set; }

        public tDefinitions? Definitions { get; set; }

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
