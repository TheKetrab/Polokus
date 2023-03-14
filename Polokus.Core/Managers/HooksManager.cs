using Polokus.Core.Interfaces.Managers;

namespace Polokus.Core.Managers
{
    public class HooksManager : IHooksManager
    {
        public PolokusMaster Master { get; }

        private struct HooksProviderInfo
        {
            public IHooksProvider Object { get; }
            public bool WaitFor { get; }

            public HooksProviderInfo(IHooksProvider @object, bool waitFor)
            {
                Object = @object;
                WaitFor = waitFor;
            }
        }

        public HooksManager(PolokusMaster master)
        {
            Master = master;
        }

        private List<HooksProviderInfo> _hooksProviders = new();
        private object _mutex = new();

        public void RegisterHooksProvider(IHooksProvider hooksProvider, bool waitFor = true)
        {
            lock (_mutex)
            {
                _hooksProviders.Add(new HooksProviderInfo(hooksProvider, waitFor));
            }
        }

        public void DeregisterHooksProvider(IHooksProvider hooksProvider)
        {
            lock (_mutex)
            {
                _hooksProviders.RemoveAll(x => x.Object == hooksProvider);
            }
        }

        public IEnumerable<IHooksProvider> GetHooksProviders()
        {
            lock (_mutex)
            {
                return _hooksProviders.Select(x => x.Object);
            }
        }

        private void ExecuteAction(bool waitFor, Action action)
        {
            if (waitFor)
            {
                action();
            }
            else
            {
                Task.Run(() => action);
            }
        }

        #region IHooksProviderImpl
        public void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.AfterExecuteNodeFailure(wfId, piId, nodeId, taskId)));
            }
        }

        public void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.AfterExecuteNodeSuccess(wfId, piId, nodeId, taskId)));
            }
        }

        public void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.AfterExecuteNodeSuspension(wfId, piId, nodeId, taskId)));
            }
        }

        public void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? callerNodeId)
        {
            if (Master.ClientConnected)
            {
                Thread.Sleep(1000); // TODO: settings provider
            }

            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.BeforeExecuteNode(wfId, piId, nodeId, taskId, callerNodeId)));
            }
        }

        public void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? callerNodeId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.BeforeStartNewSequence(wfId, piId, nodeId, callerNodeId)));
            }
        }

        public void OnProcessFinished(string wfId, string piId, string result)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnProcessFinished(wfId, piId, result)));
            }
        }

        public void OnStatusChanged(string wfId, string piId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnStatusChanged(wfId, piId)));
            }
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnTasksChanged(wfId, piId)));
            }
        }

        public void OnTimeout(string wfId, string piId)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnTimeout(wfId, piId)));
            }
        }

        public void OnCallerChanged(string callerId, string callerChangedType)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnCallerChanged(callerId, callerChangedType)));
            }
        }

        public void OnAwaitingTokenCreated(string wfId, string piId, string nodeId, string token)
        {
            lock (_mutex)
            {
                _hooksProviders.ForEach(x => ExecuteAction(x.WaitFor,
                    () => x.Object.OnAwaitingTokenCreated(wfId, piId, nodeId, token)));
            }
        }
        #endregion
    }
}
