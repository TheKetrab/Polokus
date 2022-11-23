using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    /// <summary>
    /// Goal of this object is to manage currently working objects (eg. nodehandlers),
    /// add new workers, remove them or move responsibility for task to another object.
    /// </summary>
    public class ActiveTasksManager
    {
        private int _cnt = 0;
        private Dictionary<int,Tuple<CancellationTokenSource, INodeHandler>> ActiveTasks = new(); // taskId;<cts,worker>
        private List<INodeHandler> PausedNodeHandlers = new();
        public ProcessInstance ProcessInstance { get; }

        public ActiveTasksManager(ProcessInstance processInstance)
        {
            ProcessInstance = processInstance;
        }

        public bool AnyRunning()
        {
            return ActiveTasks.Any();
        }

        public Tuple<int,CancellationToken> AddNewTask(INodeHandler nh)
        {
            int taskId = _cnt++;
            CancellationTokenSource cts = new CancellationTokenSource();

            ActiveTasks.Add(taskId, Tuple.Create(cts,nh)); // TODO to bardzo wazne zeby to nie byl null
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Id);
            return Tuple.Create(taskId, cts.Token);
        }

        public void AssignTaskToAnotherNodeHandler(int taskId, INodeHandler nh)
        {
            var ctoken = ActiveTasks[taskId].Item1;
            ActiveTasks[taskId] = Tuple.Create(ctoken, nh);
            nh.CancellationToken = ctoken.Token;
            
        }

        public void RemoveRunningTask(int taskId)
        {
            ActiveTasks.Remove(taskId);
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Id);
        }

        
        public void Pause()
        {
            PausedNodeHandlers = ActiveTasks.Values.Select(x => x.Item2).ToList();
            ActiveTasks.Values.ForEach(x => x.Item1.Cancel());            
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Id);
        }
        public void Stop()
        {
            ActiveTasks.Values.ForEach(x => x.Item1.Cancel(true));
            ActiveTasks.Clear();
            ProcessInstance.HooksProvider?.OnTasksChanged(ProcessInstance.Id);
        }
        public void Resume()
        {
            foreach (var x in PausedNodeHandlers)
            {
                // TODO
            }
        }

        public IEnumerable<INodeHandler> GetNodeHandlers()
        {
            return ActiveTasks.Values.Select(x => x.Item2);
        }


    }
}
