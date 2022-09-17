using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{

    public class NodeHandlerFinishedEventArgs : EventArgs
    {
        public FlowNode CurrentNode { get; set; }
        public FlowNode[] NextFlowNodes { get; set; }
        public int CC { get; set; }

        public NodeHandlerFinishedEventArgs(FlowNode currentNode, FlowNode[] nextFlowNodes, int cc)
        {
            CurrentNode = currentNode;
            NextFlowNodes = nextFlowNodes;
            CC = cc;
        }
    }

    public class NodeHandlerFailedEventArgs : EventArgs
    {
        public FlowNode CurrentNode { get; set; }
        public int CC { get; set; }

        public NodeHandlerFailedEventArgs(FlowNode currentNode, int cc)
        {
            CurrentNode = currentNode;
            CC = cc;
        }
    }
}
