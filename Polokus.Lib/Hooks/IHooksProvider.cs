using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Hooks
{
    public interface IHooksProvider
    {
        void OnNodeHandlerFinished(object? sender, NodeHandlerFinishedEventArgs e);
        void OnNodeHandlerFailed(object? sender, NodeHandlerFailedEventArgs e);
        void OnNodeHandlerSuspended(object? sender, NodeHandlerSuspendedEventArgs e);

        void OnTimeOut();
        void OnNewSequence();



        // ----- nowe
        void OnExecute(IFlowNode node, int taskId, string? caller);
        void OnFailure(IFlowNode node, int taskId);
        void OnSuspension(IFlowNode node, int taskId);
        void OnFinished(IFlowNode node, int taskId);

    }
}
