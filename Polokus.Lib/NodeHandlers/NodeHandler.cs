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
        public event EventHandler<NodeHandlerSuspendedEventArgs>? Suspended;

        public NodeHandler(ProcessInstance process)
        {
            this.process = process;
        }

        protected List<FlowNode> nextFlowNodes = new();

        public int TaskId { get; private set; }

        public async Task Execute(FlowNode node, int taskId, string? predecessor)
        {
            TaskId = taskId;

            int succeed = await ProcessNode(node, predecessor);
            if (succeed == 1)
            {
                Finished?.Invoke(this, new NodeHandlerFinishedEventArgs(
                    node, nextFlowNodes.ToArray(), TaskId));
            }
            else if (succeed == 0)
            {
                Suspended?.Invoke(this, new NodeHandlerSuspendedEventArgs(TaskId));
            }
            else
            {
                Failed?.Invoke(this, new NodeHandlerFailedEventArgs(node, TaskId));
            }
        }

        public virtual async Task<int> ProcessNode(FlowNode node, string? predecessor)
        {
            try
            {
                await Task.Delay(300);
                nextFlowNodes = node.Outgoing.ToList();
                Console.WriteLine($"Processing: {node.Id,30} ({typeof(T).Name}) ({node.Name}) ... DONE");
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }



        }

    }
}
