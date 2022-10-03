using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Hooks
{
    public class EmptyHooksProvider : IHooksProvider
    {
        public virtual void OnExecute(IFlowNode node, int taskId, string? predecessor) { }
        public virtual void OnFailure(IFlowNode node, int taskId) { }
        public virtual void OnFinished(IFlowNode node, int taskId) { }
        public virtual void OnSuspension(IFlowNode node, int taskId) { }





        public virtual void OnNewSequence() { }
        public virtual void OnNodeHandlerFailed(object? sender, NodeHandlerFailedEventArgs e) { }
        public virtual void OnNodeHandlerFinished(object? sender, NodeHandlerFinishedEventArgs e) { }
        public virtual void OnNodeHandlerSuspended(object? sender, NodeHandlerSuspendedEventArgs e) { }


        public virtual void OnTimeOut() { }
        
    }
}
