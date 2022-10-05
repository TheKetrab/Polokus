using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class UserTaskNodeHandler : NodeHandler<tUserTask>
    {
        public UserTaskNodeHandler(FlowNode<tUserTask> typedNode) : base(typedNode)
        {
        }

        protected override Task Process(IFlowNode? caller)
        {
            Console.WriteLine($"Waiting for user task: {Node.Name}. Press enter.");
            Console.ReadLine();
            return Task.CompletedTask;
        }
    }
}
