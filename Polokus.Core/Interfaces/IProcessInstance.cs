using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IProcessInstance
    {
        IContextInstance ContextInstance { get; }
        IBpmnProcess BpmnProcess { get; }
        ActiveTasksManager ActiveTasksManager { get; }
        IDictionary<string,INodeHandler> AvailableNodeHandlers { get; }
        ICollection<INodeHandlerWaiter> Waiters { get; }
        IEnumerable<ICatchingIntermediateEvent> CatchingIntermediateEvents { get; }



        bool ExistsAnotherTaskAbleToCallTarget(IFlowNode target, List<string> callers);
        void StartNewSequence(IFlowNode firstNode, INodeCaller? caller);


        bool IsStarted { get; }
        bool IsFinished { get; }
        bool IsActive { get; }
        TimeSpan TotalTime { get; }

        void Begin(IFlowNode startNode);
        void Stop();
        void Run();
        void Kill();
        void Finish();
        bool IsRunning();

    }
}
