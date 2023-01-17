﻿using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.Interfaces.Utils;

namespace Polokus.Core.Interfaces.Execution
{
    public interface IActiveTasksManager : IDumpable<string[]>
    {
        void CancellNodeHandler(INodeHandler nh);
        bool AnyRunning();
        int Count();
        Tuple<int, CancellationToken> AddNewTask(INodeHandler nh);
        void AssignTaskToAnotherNodeHandler(int taskId, INodeHandler nh);
        void RemoveRunningTask(int taskId);
        void Stop();
        IEnumerable<INodeHandler> GetNodeHandlers();
        IEnumerable<INodeHandler> GetPausedNodeHandlers();
        
    }
}