using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.ExternalsExample.FileMonitoring
{
    /// <summary>
    /// File monitor inspects single directory and provides some events.
    /// </summary>
    public class FileMonitor : IMonitor
    {
        private string _path;
        private FileSystemWatcher _watcher;
        private IPolokusMaster _master;

        public FileMonitor(IPolokusMaster master, string path)
        {
            _master = master;

            if (path.StartsWith('.'))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _path = path;

            _watcher = new FileSystemWatcher(_path);
            _watcher.EnableRaisingEvents = false;

            _watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

            _watcher.Renamed += _watcher_Renamed;
            _watcher.Changed += _watcher_Changed;
            _watcher.Created += _watcher_Created;
            _watcher.Deleted += _watcher_Deleted;
        }

        private void _watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _master.EmitSignal(this, FileEvtType.ItemDeleted.ToString(), e.FullPath);
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                _master.EmitSignal(this, FileEvtType.FileCreated.ToString(), e.FullPath);
            }
            else if (Directory.Exists(e.FullPath))
            {
                _master.EmitSignal(this, FileEvtType.DirectoryCreated.ToString(), e.FullPath);
            }
        }

        private void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                _master.EmitSignal(this, FileEvtType.FileModified.ToString(), e.FullPath);
            }
        }

        private void _watcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                _master.EmitSignal(this, FileEvtType.FileRenamed.ToString(), e.FullPath);
            }
            else if (Directory.Exists(e.FullPath))
            {
                _master.EmitSignal(this, FileEvtType.DirectoryCreated.ToString(), e.FullPath);
            }
        }

        public bool IsMonitoring { get; private set; }
        public void StartMonitoring()
        {
            _watcher.EnableRaisingEvents = true;
            IsMonitoring = true;
        }

        public void StopMonitoring()
        {
            _watcher.EnableRaisingEvents = false;
            IsMonitoring = false;
        }



        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}