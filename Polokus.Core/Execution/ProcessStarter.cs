using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution
{
    public class ProcessStarter : IProcessStarter
    {
        public string Id { get; }
        public IFlowNode StartNode { get; }
        public IBpmnProcess BpmnProcess { get; }
        public IWorkflow Workflow { get; }
        public IHooksProvider? HooksProvider => Workflow.PolokusMaster.HooksManager;


        public ProcessStarter(IWorkflow workflow, IBpmnProcess bpmnProcess, IFlowNode startNode)
        {
            Workflow = workflow;
            BpmnProcess = bpmnProcess;
            StartNode = startNode;
            Id = EncodingIds.GetStarterId(Workflow.Id, BpmnProcess.Id, StartNode.Id);
        }
    }
}
