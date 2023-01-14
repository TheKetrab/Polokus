using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Interfaces.BpmnModels;

namespace Polokus.Core.NodeHandlers.Abstract
{
    public interface ISubprocessingNodeHandler : INodeHandler
    {
        IProcessInstance? SubProcessInstance { get; }
    }

    public abstract class SubprocessingNodeHandler<T> : NodeHandler<T> where T : tFlowNode
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
