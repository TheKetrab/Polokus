namespace Polokus.Core.Interfaces
{
    public enum BoundaryEventType
    {
        Undefined,
        Signal,
        Message,
        Timer,
        Error
    }

    public enum ProcessStatus
    {
        Initialized = 0,
        Running = 1 << 0,
        Paused = 1 << 1,
        Finished = 1 << 2,
        Stopped = 1 << 3
    }

    public enum WaiterType
    {
        None,
        Timer,
        Message,
        Signal
    }

    public enum MsgType
    {
        Simple,
        Warning,
        Error,
    }

    public enum ProcessResultState
    {
        Success,
        Failure,
        Suspension,
        Cancellation,
        ErrorBoundaryEvent
    }

    public enum ObjectType
    {
        None,
        Workflow,
        ProcessInstance,
        FlowNode,
        Sequence
    }
}
