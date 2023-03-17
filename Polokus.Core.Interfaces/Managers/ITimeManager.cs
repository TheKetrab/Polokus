using Polokus.Core.Interfaces.Exceptions;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.Managers
{
    public interface ITimeManager : ICallersManager
    {
        /// <summary>
        /// Register given waiter with cron-style definition.
        /// </summary>
        /// <param name="timeString">Cron-style definition (eg. "0 0 12 ? * WED" means "Every Wednesday at 12:00 pm.").</param>
        /// <param name="waiter">Waiter to be registered.</param>
        /// <param name="oneTime">Should it be cyclic or not?</param>
        /// <param name="continuation">Possible continuation to invoke after node was invoked by waiter. For cleanups.</param>
        Task RegisterWaiterCrone(string timeString, INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null);

        /// <summary>
        /// Register given waiter with non-cron-style definition.
        /// </summary>
        /// <param name="timeString">Non-cron-style definition (eg. "1h 30s" means "Wait 1 hour and 30 seconds.")
        /// (h - hours, m - minutes, s - seconds, ms - milseconds)."</param>
        /// <param name="waiter">Waiter to be registered.</param>
        /// <param name="oneTime">Should it be cyclic or not?</param>
        /// <param name="continuation">Possible continuation to invoke after node was invoked by waiter. For cleanups.</param>
        void RegisterWaiterNotCrone(string timeString, INodeHandlerWaiter waiter, bool oneTime, Action? continuation = null);
    }
}
