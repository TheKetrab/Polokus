using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{

    public class EventLogger : IHooksProvider
    {
        StringBuilder sb = new StringBuilder();

        public Action<StringBuilder, object?, NodeHandlerFailedEventArgs>? onNodeHandlerFailed;
        public Action<StringBuilder, object?, NodeHandlerFinishedEventArgs>? onNodeHandlerFinished;
        public Action<StringBuilder, object?, NodeHandlerSuspendedEventArgs>? onNodeHandlerSuspended;

        public void OnNodeHandlerFailed(object? sender, NodeHandlerFailedEventArgs e)
        {
            onNodeHandlerFailed?.Invoke(sb, sender, e);
        }

        public void OnNodeHandlerFinished(object? sender, NodeHandlerFinishedEventArgs e)
        {
            onNodeHandlerFinished?.Invoke(sb, sender, e);
        }

        public void OnNodeHandlerSuspended(object? sender, NodeHandlerSuspendedEventArgs e)
        {
            onNodeHandlerSuspended?.Invoke(sb, sender, e);
        }

        public string GetResult()
        {
            return sb.ToString();
        }

        public void OnTimeOut()
        {
        }

        public void OnNewSequence()
        {
        }

        public void OnExecute()
        {
            
        }

        public void OnExecute(IFlowNode node, int taskId, string? caller)
        {
            throw new NotImplementedException();
        }

        public void OnFailure(IFlowNode node, int taskId)
        {
            throw new NotImplementedException();
        }

        public void OnSuspension(IFlowNode node, int taskId)
        {
            throw new NotImplementedException();
        }

        public void OnFinished(IFlowNode node, int taskId)
        {
            throw new NotImplementedException();
        }

        public void OnCanExecute(IFlowNode node, int taskId, string? caller)
        {
            throw new NotImplementedException();
        }
    }

}
