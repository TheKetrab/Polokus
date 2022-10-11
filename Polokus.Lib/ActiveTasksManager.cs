using Polokus.Lib.Hooks;
using Polokus.Lib.Models;
using Polokus.Lib.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib
{
    /// <summary>
    /// Goal of this object is to manage currently working objects (eg. nodehandlers),
    /// add new workers, remove them or move responsibility for task to another object.
    /// </summary>
    public class ActiveTasksManager
    {
        private int _cnt = 0;
        public Dictionary<int,object> ActiveTasks = new(); // taskId;worker
        public ProcessInstance ProcessInstance { get; }

        public ActiveTasksManager(ProcessInstance processInstance)
        {
            ProcessInstance = processInstance;
        }

        public bool AnyRunning()
        {
            return ActiveTasks.Any();
        }

        public int AddNewTask(object o)
        {
            int taskId = _cnt++;
            ActiveTasks.Add(taskId, o); // TODO to bardzo wazne zeby to nie byl null
            return taskId;
        }

        public void RemoveRunningTask(int taskId)
        {
            ActiveTasks.Remove(taskId);
        }



    }
}
