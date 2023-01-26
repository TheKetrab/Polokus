using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Execution
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
        IHooksProvider? HooksProvider { get; }
    }
}
