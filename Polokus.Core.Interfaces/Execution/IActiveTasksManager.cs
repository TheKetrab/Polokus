using Polokus.Core.Interfaces.Execution.NodeHandlers;
using Polokus.Core.Interfaces.Serialization;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Active Tasks Manager is an object that provides data
    /// regarding NodeHandlers currently being processed.
    /// </summary>
    public interface IActiveTasksManager : IDumpable<string[]>
    {
        IProcessInstance ProcessInstance { get; }

        /// <summary>
        /// Returns count of active tasks.
        /// </summary>
        int Count();

        /// <summary>
        /// Returns true iff Count() > 0
        /// </summary>
        bool AnyRunning();

        /// <summary>
        /// Stops all active tasks.
        /// </summary>
        void Stop();

        /// <summary>
        /// Returns list of NodeHandlers that are active.
        /// </summary>
        IList<INodeHandler> GetNodeHandlers();

        /// <summary>
        /// If NodeHandler is active, it removes concrete active task and cancell NodeHandler.
        /// </summary>
        /// <param name="nh">NodeHandler to cancel.</param>
        void CancellNodeHandler(INodeHandler nh);

        /// <summary>
        /// Starts new task for concrete NodeHandler. Returns pair: id of task and cancellation token.
        /// </summary>
        /// <param name="nh">NodeHandler to start on another task.</param>
        Tuple<int, CancellationTokenSource> AddNewTask(INodeHandler nh);

        /// <summary>
        /// Switches actual processing NodeHandler for concrete task.
        /// </summary>
        /// <param name="taskId">Id of task to change NodeHandler for.</param>
        /// <param name="nh">New NodeHandler for this task.</param>
        void AssignTaskToAnotherNodeHandler(int taskId, INodeHandler nh);

        /// <summary>
        /// Removes task with given id.
        /// </summary>
        /// <param name="taskId">Id of task to remove.</param>
        void RemoveRunningTask(int taskId);
    }
}
