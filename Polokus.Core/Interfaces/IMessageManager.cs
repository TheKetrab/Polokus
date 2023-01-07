namespace Polokus.Core.Interfaces
{
    public interface IMessageManager : ICallersManager
    {
        /// <summary>
        /// This method asynchronously pings listener: sends http request with optional querystring.
        /// </summary>
        /// <param name="listenerId">Id of starter or waiter to ping.</param>
        /// <param name="queryString">Arguments that listener can handle.</param>
        /// <returns></returns>
        Task PingListener(string listenerId, string? queryString = null);
    }
}
