using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib
{
    public interface IHooksProvider
    {
        void OnNodeHandlerFinished(object? sender, NodeHandlerFinishedEventArgs e);
        void OnNodeHandlerFailed(object? sender, NodeHandlerFailedEventArgs e);
        void OnNodeHandlerSuspended(object? sender, NodeHandlerSuspendedEventArgs e);

        void OnTimeOut();
        void OnNewSequence();
        void OnExecute(FlowNode node, int taskId, string? predecessor);

    }
}
