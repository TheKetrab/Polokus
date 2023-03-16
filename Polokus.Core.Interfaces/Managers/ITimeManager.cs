using Polokus.Core.Interfaces.Exceptions;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.Managers
{
    public interface ITimeManager : ICallersManager
    {
        Task RegisterWaiterCrone(string timeString, INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null);
        void RegisterWaiterNotCrone(string timeString, INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null);
    }
}
