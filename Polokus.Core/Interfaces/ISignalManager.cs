namespace Polokus.Core.Interfaces
{
    public interface ISignalManager
    {
        IWorkflow Workflow { get; }

        /// <summary>
        /// This method gets all starters registered for this signal manager.
        /// </summary>
        IEnumerable<IProcessStarter> GetStarters();

        /// <summary>
        /// This method gets all waiters registered for this signal manager.
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
        void RegisterSignalListener(INodeHandlerWaiter waiter);

        /// <summary>
        /// This method registers starter.
        /// </summary>
        /// <param name="starter">Starter to register.</param>
        void RegisterSignalListener(IProcessStarter starter);

        /// <summary>
        /// This method emits signal to PolokusMaster object (global scope).
        /// </summary>
        /// <param name="signal">Id of signal.</param>
        void EmitSignal(string signal);

    }
}
