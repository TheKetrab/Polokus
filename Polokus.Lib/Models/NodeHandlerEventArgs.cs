using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{

    public class NodeHandlerFinishedEventArgs : EventArgs
    {
        public IFlowNode CurrentNode { get; set; }
        public Sequence[] SequencesToInvoke { get; set; }
        public int TaskId { get; set; }

        public NodeHandlerFinishedEventArgs(IFlowNode currentNode, Sequence[] sequencesToInvoke, int taskId)
        {
            CurrentNode = currentNode;
            SequencesToInvoke = sequencesToInvoke;
            TaskId = taskId;
        }
    }

    public class NodeHandlerFailedEventArgs : EventArgs
    {
        public IFlowNode CurrentNode { get; set; }
        public int TaskId { get; set; }

        public NodeHandlerFailedEventArgs(IFlowNode currentNode, int taskId)
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
