namespace Polokus.Core.Interfaces
{
    public interface IMessageManager
    {
        /// <summary>
        /// This method gets all starters registered for this message manager.
        /// </summary>
        IEnumerable<IProcessStarter> GetStarters();

        /// <summary>
        /// This method gets all waiters registered for this message manager.
        /// </summary>
        IEnumerable<INodeHandlerWaiter> GetWaiters();

        /// <summary>
        /// This method returns true iif. exists any waiter or starter.
        /// </summary>
        bool IsAnyWaiting();

        /// <summary>
        /// This method registers waiter.
        /// </summary>
        /// <param name="waiter">Waiter to register.</param>
        void RegisterMessageListener(INodeHandlerWaiter waiter);

        /// <summary>
        /// This method registers starter.
        /// </summary>
        /// <param name="starter">Starter to register.</param>
        void RegisterMessageListener(IProcessStarter starter);

        /// <summary>
        /// This method asynchronously pings listener: sends http request with optional querystring.
        /// </summary>
        /// <param name="listenerId">Id of starter or waiter to ping.</param>
        /// <param name="queryString">Arguments that listener can handle.</param>
        /// <returns></returns>
        Task PingListener(string listenerId, string? queryString = null);
    }
}
