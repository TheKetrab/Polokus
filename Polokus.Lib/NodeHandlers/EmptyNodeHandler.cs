using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class EmptyNodeHandler : INodeHandler
    {
        public IFlowNode Node => throw new NotImplementedException();

        public ProcessInstance? ProcessInstance { get; set; }

        public Task<ProcessResultInfo> Execute(IFlowNode? caller, int taskId)
        {
            return Task.FromResult(new ProcessResultInfo(ProcessResultState.Failure));
        }
    }
}
