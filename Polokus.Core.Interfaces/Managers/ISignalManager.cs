namespace Polokus.Core.Interfaces.Managers
{
    public interface ISignalManager : ICallersManager
    {
        /// <summary>
        /// This method emits signal to PolokusMaster object (global scope).
        /// </summary>
        /// <param name="signal">Id of signal.</param>
        /// <param name="parameters">Parameters to invoke signal with.</param>
        void EmitSignal(string signal, params string[] parameters);

    }
}
