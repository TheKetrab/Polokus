﻿using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class SubProcessNodeHandler : SubprocessingNodeHandler<tSubProcess>
    {        
        public SubProcessNodeHandler(IProcessInstance processInstance, FlowNode<tSubProcess> typedNode)
            : base(processInstance, typedNode)
        {
        }

        protected override IBpmnProcess GetBpmnProcess()
        {
            return Workflow.BpmnWorkflow.BpmnProcesses.First(x => x.Id == this.Node.Id);
        }

        protected override IWorkflow GetWorkflow()
        {
            return this.Workflow;
        }
    }
}