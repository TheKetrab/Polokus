
namespace Polokus.Core.Interfaces.Execution
{
    public interface IFileMonitor
    {
        event EventHandler<string>? FileCreated;
        event EventHandler<string>? FileModified;
        event EventHandler<string>? FileRenamed;

        event EventHandler<string>? DirectoryCreated;
        event EventHandler<string>? DirectoryRenamed;

        event EventHandler<string>? ItemDeleted;


        bool IsMonitoring { get; }
        void StartMonitoring();
        void StopMonitoring();
    }
}
