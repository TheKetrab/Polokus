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
        public event EventHandler<FlowNode>? Finished;

        protected ProcessInstance process;
        public NodeHandler(ProcessInstance process)
        {
            this.process = process;

            Finished += (s, e) => RunNextNodes(e);

        }

        public virtual void RunNextNodes(FlowNode e)
        {
            foreach (var next in e.Outgoing)
            {
                this.process.NodeHandlerManager.Execute(next);
            }
        }
         
        public void Execute(FlowNode node)
        {
            ProcessNode(node);
            Finished?.Invoke(this, node);
        }

        public virtual void ProcessNode(FlowNode node)
        {
            Console.WriteLine($"Processing: {node.Id,30} ({typeof(T).Name})");
        }

    }
}
