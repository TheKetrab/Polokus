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
        public int TaskId { get; set; }

        public NodeHandlerFinishedEventArgs(FlowNode currentNode, FlowNode[] nextFlowNodes, int taskId)
        {
            CurrentNode = currentNode;
            NextFlowNodes = nextFlowNodes;
            TaskId = taskId;
        }
    }

    public class NodeHandlerFailedEventArgs : EventArgs
    {
        public FlowNode CurrentNode { get; set; }
        public int TaskId { get; set; }

        public NodeHandlerFailedEventArgs(FlowNode currentNode, int taskId)
        {
            CurrentNode = currentNode;
            TaskId = taskId;
        }
    }

    public class NodeHandlerSuspendedEventArgs : EventArgs
    {
        public int TaskId { get; set; }
        public NodeHandlerSuspendedEventArgs(int taskId)
        {
            TaskId = taskId;
        }
    }
}
