using Polokus.Core.Interfaces.BpmnModels;

namespace Polokus.Core.Interfaces.Managers
{
    /// <summary>
    /// Status Manager is an object that controls state of execution of Process Instance.
    /// </summary>
    public interface IStatusManager
    {
        ProcessStatus Status { get; }
        bool IsStarted { get; }
        bool IsStopped { get; }
        bool IsPaused { get; }
        bool IsFinished { get; }
        bool IsActive { get; }
        TimeSpan TotalTime { get; }

        /// <summary>
        /// Starts processing the process at given node.
        /// </summary>
        /// <param name="startNode">Node to start processing on.</param>
        void Begin(IFlowNode startNode);

        /// <summary>
        /// Stops processing this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Resume process instance after being paused.
        /// </summary>
        void Resume();

        /// <summary>
        /// Pauses process instance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops process if is no more running.
        /// </summary>
        void Finish();

        /// <summary>
        /// Returns false iif there is nothing that can continue this process instance
        /// (none active task, none waiter, not paused)
        /// </summary>
        bool IsRunning();

        /// <summary>
        /// Stops all waiters and active tasks.
        /// </summary>
        void KillEverythingRunning();

    }
}
