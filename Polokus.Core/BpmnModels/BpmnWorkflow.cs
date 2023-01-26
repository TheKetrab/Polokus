using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.BpmnModels
{
    public class BpmnWorkflow : IBpmnWorkflow
    {
        public string? RawString { get; set; }
        public IWorkflow? Workflow { get; }
        public IEnumerable<IBpmnProcess> BpmnProcesses { get; private set; }

        public tDefinitions? Definitions { get; set; }

        public BpmnWorkflow()
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
