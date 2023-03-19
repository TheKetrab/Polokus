using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Execution.NodeHandlers;

namespace Polokus.Core.Execution
{
    /// <summary>
    /// Goal of this object is to manage currently working objects (eg. nodehandlers),
    /// add new workers, remove them or move responsibility for task to another object.
    /// </summary>
    public class ActiveTasksManager : IActiveTasksManager
    {
        private int _cnt = 0;
        private object _mutex = new object();
        private Dictionary<int, Tuple<CancellationTokenSource, INodeHandler>> _activeTasks = new(); // taskId;<cts,worker>
        public IProcessInstance ProcessInstance { get; }

        public ActiveTasksManager(ProcessInstance processInstance)
        {
            ProcessInstance = processInstance;
        }

        public void CancellNodeHandler(INodeHandler nh)
        {
            lock (_mutex)
            {
                var toCancell = _activeTasks.FirstOrDefault(x => x.Value.Item2 == nh);
                if (toCancell.Value != null)
                {
                    _activeTasks.Remove(toCancell.Key);

                    toCancell.Value.Item1.Cancel();
                    if (nh is ISubprocessingNodeHandler spnh && spnh.SubProcessInstance != null)
                    {
                        spnh.SubProcessInstance.StatusManager.Stop();
                        spnh.SubProcessInstance.HooksProvider?.OnProcessFinished(
                            spnh.SubProcessInstance.Workflow.Id, spnh.SubProcessInstance.Id, "cancell");

                    }

                }
            }
        }

        public bool AnyRunning()
        {
            lock (_mutex)
            {
                return _activeTasks.Any();
            }
        }

        public int Count()
        {
            lock (_mutex)
            {
                return _activeTasks.Count;
            }
        }

        public Tuple<int, CancellationTokenSource> AddNewTask(INodeHandler nh)
        {
            lock (_mutex)
            {
                int taskId = _cnt++;
                CancellationTokenSource cts = new CancellationTokenSource();

                _activeTasks.Add(taskId, Tuple.Create(cts, nh)); // TODO to bardzo wazne zeby to nie byl null
                ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
                return Tuple.Create(taskId, cts);
            }
        }

        public void AssignTaskToAnotherNodeHandler(int taskId, INodeHandler nh)
        {
            lock (_mutex)
            {
                var ctoken = _activeTasks[taskId].Item1;
                _activeTasks[taskId] = Tuple.Create(ctoken, nh);
                nh.CancellationTokenSource = ctoken;
            }

        }

        public void RemoveRunningTask(int taskId)
        {
            lock (_mutex)
            {
                _activeTasks.Remove(taskId);
                ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
            }
        }

        public void Stop()
        {
            lock (_mutex)
            {
                _activeTasks.Values.ForEach(x => x.Item1.Cancel(true));
                _activeTasks.Clear();
                ProcessInstance.AvailableNodeHandlers.Clear();
                ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
            }
        }

        public IList<INodeHandler> GetNodeHandlers()
        {
            lock (_mutex)
            {
                return _activeTasks.Values.Select(x => x.Item2).ToList();
            }
        }

        public string[] Dump()
        {
            lock (_mutex)
            {
                return _activeTasks.Values.Select(x => x.Item2.Node.Id).ToArray();
            }
        }

    }
}
