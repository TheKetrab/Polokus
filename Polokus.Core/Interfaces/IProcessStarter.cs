namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// Starter is an object that is responsible for starting new process instance, when an event occures.
    /// </summary>
    public interface IProcessStarter
    {
        string Id { get; }
        IFlowNode StartNode { get; }
        IBpmnProcess BpmnProcess { get; }
        IWorkflow Workflow { get; }
    }
}
