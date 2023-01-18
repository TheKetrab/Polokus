using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.Managers
{
    public interface IHooksManager : IHooksProvider
    {
        /// <summary>
        /// This method registers given hooks provider to be invoked.
        /// <paramref name="waitFor"/> toggle is responsible for deciding if manager should wait
        /// for hooks provider to finish before it will call another hooks provider.
        /// </summary>
        /// <param name="hooksProvider">Hooks provider to register.</param>
        /// <param name="waitFor">Switch to decide if manager should wait for invoked method to finish.</param>
        void RegisterHooksProvider(IHooksProvider hooksProvider, bool waitFor = true);

        /// <summary>
        /// This method deregisters given provider - it no longer will be called.
        /// </summary>
        /// <param name="hooksProvider">Hooks provider to deregister.</param>
        void DeregisterHooksProvider(IHooksProvider hooksProvider);

        /// <summary>
        /// This method returns all registered providers that will be invoked.
        /// </summary>
        IEnumerable<IHooksProvider> GetHooksProviders();
    }
}
