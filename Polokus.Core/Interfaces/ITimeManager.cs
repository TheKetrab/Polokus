namespace Polokus.Core.Interfaces
{
    public interface ITimeManager
    {
        IEnumerable<INodeHandlerWaiter> GetWaiters();
        IEnumerable<IProcessStarter> GetStarters();

        void RegisterWaiter(string timeString, INodeHandlerWaiter waiter, bool oneTime);
        void RegisterStarter(string timeString, IProcessStarter starter);

    }
}
