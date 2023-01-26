namespace Polokus.Core.Interfaces.Extensibility
{
    public interface IMonitor : IDisposable
    {
        bool IsMonitoring { get; }
        void StartMonitoring();
        void StopMonitoring();
    }
}
