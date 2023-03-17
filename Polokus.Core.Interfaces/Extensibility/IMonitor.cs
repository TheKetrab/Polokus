namespace Polokus.Core.Interfaces.Extensibility
{
    /// <summary>
    /// Monitor is an object that tries to find out if something happened, if so - emits signal.
    /// </summary>
    public interface IMonitor : IDisposable
    {
        /// <summary>
        /// Information if monitor is monitoring (if not, it won't emit a signal).
        /// </summary>
        bool IsMonitoring { get; }

        /// <summary>
        /// Monitor starts to monitoring.
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Monitor stops to monitoring.
        /// </summary>
        void StopMonitoring();
    }
}
