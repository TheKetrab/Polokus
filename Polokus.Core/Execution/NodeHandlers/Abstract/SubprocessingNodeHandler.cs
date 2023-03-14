using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.Execution.NodeHandlers.Abstract
{

    public abstract class SubprocessingNodeHandler<T> : NodeHandler<T>, ISubprocessingNodeHandler
        where T : tFlowNode
    {
        public IProcessInstance? SubProcessInstance { get; private set; }

        public SubprocessingNodeHandler(IProcessInstance processInstance, FlowNode<T> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            var wf = GetWorkflow();
            var bpmnProcess = GetBpmnProcess();
            var manualStartNode = bpmnProcess.GetManualStartNode();

            SubProcessInstance = this.ProcessInstance.CreateSubProcessInstance(bpmnProcess);
            bool success = await wf.RunProcessAsync(SubProcessInstance, manualStartNode, null);

            if (!success)
            {
                throw new Exception();
            }
        }

        protected abstract IWorkflow GetWorkflow();
        protected abstract IBpmnProcess GetBpmnProcess();
    }
}
