
namespace Polokus.Core.Interfaces.Execution
{
    public interface IMonitor : IDisposable
    {
        bool IsMonitoring { get; }
        void StartMonitoring();
        void StopMonitoring();
    }
}
