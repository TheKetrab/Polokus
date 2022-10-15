﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IHooksProvider
    {
        void AfterExecuteNodeSuccess(IFlowNode node, int taskId);
        void AfterExecuteNodeFailure(IFlowNode node, int taskId);
        void AfterExecuteNodeSuspension(IFlowNode node, int taskId);
        void BeforeStartNewSequence(IFlowNode firstNode, INodeCaller? caller);
        void BeforeExecuteNode(IFlowNode node, int taskId, INodeCaller? caller);
        void OnTimeout();
    }
}
