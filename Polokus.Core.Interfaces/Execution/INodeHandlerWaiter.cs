using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// Waiter is an object that is responsible for calling FlowNode, when an event occures.
    /// </summary>
    public interface INodeHandlerWaiter : INodeCaller
    {
        IFlowNode NodeToCall { get; }
        IProcessInstance ProcessInstance { get; }
        IHooksProvider? HooksProvider { get; }
        void Invoke();
    }

}
