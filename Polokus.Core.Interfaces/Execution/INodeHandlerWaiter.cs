using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Waiter is an object that is responsible for calling FlowNode, when an event occures.
    /// </summary>
    public interface INodeHandlerWaiter : INodeCaller
    {
        /// <summary>
        /// Flow node that this waiter will call when an event occurs.
        /// </summary>
        IFlowNode NodeToCall { get; }

        /// <summary>
        /// Instance of process in which this waiter exists.
        /// </summary>
        IProcessInstance ProcessInstance { get; }

        /// <summary>
        /// Hooks provider of engine that can raise events.
        /// </summary>
        IHooksProvider? HooksProvider { get; }

        /// <summary>
        /// This method 'calls' NodeToCall object - tries to 'Execute' it.
        /// </summary>
        void Invoke();
    }

}
