using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.Interfaces;

namespace Polokus.Core.NodeHandlers
{
    public class UserTaskNodeHandler : NodeHandler<tUserTask>
    {
        public UserTaskNodeHandler(ProcessInstance processInstance, FlowNode<tUserTask> typedNode) : base(processInstance, typedNode)
        {
        }

        protected override Task Action(INodeCaller? caller)
        {
            Console.WriteLine($"Waiting for user task: {Node.Name}. Press enter.");
            Console.ReadLine();
            return Task.CompletedTask;
        }
    }
}
