using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public abstract class NodeHandler<T> : INodeHandler
        where T : tFlowNode
    {

        // nodehandler przetwarza to co ma zrobic i wybiera kolejne node'y do potencjalnego wywolania

        protected ProcessInstance process;

        public event EventHandler<NodeHandlerFinishedEventArgs>? Finished;
        public event EventHandler<NodeHandlerFailedEventArgs>? Failed;

        public NodeHandler(ProcessInstance process)
        {
            this.process = process;
        }

        private List<FlowNode> nextFlowNodes = new();

        public int CC { get; set; }

        public async Task Execute(FlowNode node)
        {
            bool succeed = await ProcessNode(node);
            if (succeed)
            {
                Finished?.Invoke(this, new NodeHandlerFinishedEventArgs(
                    node, nextFlowNodes.ToArray(), CC));
            }
            else
            {
                Failed?.Invoke(this, new NodeHandlerFailedEventArgs(node,CC));
            }
        }

        public virtual async Task<bool> ProcessNode(FlowNode node)
        {
            try
            {
                await Task.Delay(300);
                nextFlowNodes = node.Outgoing.ToList();
                Console.WriteLine($"Processing: {node.Id,30} ({typeof(T).Name}) ... DONE");
                return true;
            }
            catch (Exception)
            {
                return false;
            }



        }

    }
}
