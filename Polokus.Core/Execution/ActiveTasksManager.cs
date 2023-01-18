using Polokus.Core.Helpers;
using Polokus.Core.Interfaces.Execution;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.NodeHandlers.Abstract;

namespace Polokus.Core.Execution
{
    /// <summary>
    /// Goal of this object is to manage currently working objects (eg. nodehandlers),
    /// add new workers, remove them or move responsibility for task to another object.
    /// </summary>
    public class ActiveTasksManager : IActiveTasksManager
    {
        private int _cnt = 0;
        private Dictionary<int, Tuple<CancellationTokenSource, INodeHandler>> ActiveTasks = new(); // taskId;<cts,worker>
        public ProcessInstance ProcessInstance { get; }

        public ActiveTasksManager(ProcessInstance processInstance)
        {
            ProcessInstance = processInstance;
        }

        public void CancellNodeHandler(INodeHandler nh)
        {
            var toCancell = ActiveTasks.FirstOrDefault(x => x.Value.Item2 == nh);
            if (toCancell.Value != null)
            {
                ActiveTasks.Remove(toCancell.Key);

                toCancell.Value.Item1.Cancel();
                if (nh is ISubprocessingNodeHandler spnh && spnh.SubProcessInstance != null)
                {
                    spnh.SubProcessInstance.StatusManager.Stop();
                    spnh.SubProcessInstance.HooksProvider?.OnProcessFinished(
                        spnh.SubProcessInstance.Workflow.Id, spnh.SubProcessInstance.Id, "cancell");

                }

            }
        }

        public bool AnyRunning()
        {
            return ActiveTasks.Any();
        }

        public int Count()
        {
            return ActiveTasks.Count;
        }

        public Tuple<int, CancellationToken> AddNewTask(INodeHandler nh)
        {
            int taskId = _cnt++;
            CancellationTokenSource cts = new CancellationTokenSource();

            ActiveTasks.Add(taskId, Tuple.Create(cts, nh)); // TODO to bardzo wazne zeby to nie byl null
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
            return Tuple.Create(taskId, cts.Token);
        }

        public void AssignTaskToAnotherNodeHandler(int taskId, INodeHandler nh)
        {
            var ctoken = ActiveTasks[taskId].Item1;
            ActiveTasks[taskId] = Tuple.Create(ctoken, nh);
            nh.CancellationTokenSource = ctoken;

        }

        public void RemoveRunningTask(int taskId)
        {
            ActiveTasks.Remove(taskId);
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
        }

        public void Stop()
        {
            ActiveTasks.Values.ForEach(x => x.Item1.Cancel(true));
            ActiveTasks.Clear();
            ProcessInstance.AvailableNodeHandlers.Clear();
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Workflow.Id, ProcessInstance.Id);
        }

        public IEnumerable<INodeHandler> GetNodeHandlers()
        {
            return ActiveTasks.Values.Select(x => x.Item2);
        }

        public string[] Dump()
        {
            return ActiveTasks.Values.Select(x => x.Item2.Node.Id).ToArray();
        }

    }
}
