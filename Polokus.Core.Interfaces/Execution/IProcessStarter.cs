using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Starter is an object that is responsible for starting new process instance, when an event occures.
    /// </summary>
    public interface IProcessStarter
    {
        /// <summary>
        /// Id of process starter.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// FlowNode that will be called by the starter. New instance of process will start from this node.
        /// </summary>
        IFlowNode StartNode { get; }

        /// <summary>
        /// Definition of BPMN process that will be a new instance started for.
        /// </summary>
        IBpmnProcess BpmnProcess { get; }

        /// <summary>
        /// Workflow of the starter.
        /// </summary>
        IWorkflow Workflow { get; }

        /// <summary>
        /// Hooks provider of engine.
        /// </summary>
        IHooksProvider? HooksProvider { get; }
    }
}
