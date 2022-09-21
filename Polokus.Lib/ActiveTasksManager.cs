using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib
{
    public class ActiveTasksManager
    {
        private int _cnt = 0;
        public List<int> ActiveTasks = new();

        public bool AnyRunning()
        {
            return ActiveTasks.Any();
        }

        public int AddNewTask()
        {
            int taskId = _cnt++;
            ActiveTasks.Add(taskId);
            return taskId;
        }

        public void RemoveRunningTask(int taskId)
        {
            ActiveTasks.RemoveAll(x => x == taskId);
        }
    }
}
