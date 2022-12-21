using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Hooks
{
    public class HooksManager : IHooksManager
    {
        private List<IHooksProvider> _hooksProviders = new();

        public void RegisterHooksProvider(IHooksProvider hookProvider)
        {
            _hooksProviders.Add(hookProvider);
        }

        public void DeregisterHooksProvider(IHooksProvider hookProvider)
        {
            _hooksProviders.Remove(hookProvider);
        }

        public IEnumerable<IHooksProvider> GetHooksProviders()
        {
            return _hooksProviders;
        }

        #region IHooksProviderImpl
        public void AfterExecuteNodeFailure(string wfId, string piId, IFlowNode node, int taskId)
        {
            _hooksProviders.ForEach(x => x.AfterExecuteNodeFailure(wfId, piId, node, taskId));
        }

        public void AfterExecuteNodeSuccess(string wfId, string piId, IFlowNode node, int taskId)
        {
            _hooksProviders.ForEach(x => x.AfterExecuteNodeSuccess(wfId, piId, node, taskId));
        }

        public void AfterExecuteNodeSuspension(string wfId, string piId, IFlowNode node, int taskId)
        {
            _hooksProviders.ForEach(x => x.AfterExecuteNodeSuspension(wfId, piId, node, taskId));
        }

        public void BeforeExecuteNode(string wfId, string piId, IFlowNode node, int taskId, INodeCaller? caller)
        {
            _hooksProviders.ForEach(x => x.BeforeExecuteNode(wfId, piId, node, taskId, caller));
        }

        public void BeforeStartNewSequence(string wfId, string piId, IFlowNode firstNode, INodeCaller? caller)
        {
            _hooksProviders.ForEach(x => x.BeforeStartNewSequence(wfId, piId, firstNode, caller));
        }

        public void OnProcessFinished(string wfId, string piId, string result)
        {
            _hooksProviders.ForEach(x => x.OnProcessFinished(wfId, piId, result));
        }

        public void OnStatusChanged(string wfId, string piId)
        {
            _hooksProviders.ForEach(x => x.OnStatusChanged(wfId, piId));
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            _hooksProviders.ForEach(x => x.OnTasksChanged(wfId, piId));
        }

        public void OnTimeout(string wfId, string piId)
        {
            _hooksProviders.ForEach(x => x.OnTimeout(wfId, piId));
        }

        public void OnCallerChanged(string callerId, CallerChangedType type)
        {
            _hooksProviders.ForEach(x => x.OnCallerChanged(callerId, type));
        }
        #endregion
    }
}
