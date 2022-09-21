using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class EmptyNodeHandler : INodeHandler
    {
        public int TaskId { get; private set; }

        public event EventHandler<NodeHandlerFinishedEventArgs>? Finished;
        public event EventHandler<NodeHandlerFailedEventArgs>? Failed;
        public event EventHandler<NodeHandlerSuspendedEventArgs>? Suspended;

        public Task Execute(FlowNode node, int taskId, string? predecessorId)
        {
            return Task.CompletedTask;
        }
    }
}
