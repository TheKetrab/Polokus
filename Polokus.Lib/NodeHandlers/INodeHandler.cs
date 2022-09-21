using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{

    public interface INodeHandler
    {
        event EventHandler<NodeHandlerFinishedEventArgs>? Finished;
        event EventHandler<NodeHandlerFailedEventArgs>? Failed;
        event EventHandler<NodeHandlerSuspendedEventArgs>? Suspended;

        int TaskId { get; }

        Task Execute(FlowNode node, int taskId, string? predecessor);
    }
}
