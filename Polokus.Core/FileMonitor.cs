using System.IO;
using System.Security.Cryptography;

namespace Polokus
{
    /// <summary>
    /// File monitor inspects single directory and provides some events.
    /// </summary>
    public class FileMonitor : IDisposable
    {
        private string _path;
        private FileSystemWatcher _watcher;

        public enum FileEvtType
        {
            FileCreated,
            FileModified,
            FileRenamed,
            DirectoryCreated,
            DirectoryRenamed,
            ItemDeleted
        }

        public FileMonitor(string path)
        {
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
            ItemDeleted?.Invoke(this, e.FullPath);
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                FileCreated?.Invoke(this, e.FullPath);
            }
            else if (Directory.Exists(e.FullPath))
            {
                DirectoryCreated?.Invoke(this, e.FullPath);
            }
        }

        private void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                FileModified?.Invoke(this, e.FullPath);
            }
        }

        private void _watcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                FileRenamed?.Invoke(this, e.FullPath);
            }
            else if (Directory.Exists(e.FullPath))
            {
                DirectoryRenamed?.Invoke(this, e.FullPath);
            }
        }

        public event EventHandler<string>? FileCreated;
        public event EventHandler<string>? FileModified;
        public event EventHandler<string>? FileRenamed;

        public event EventHandler<string>? DirectoryCreated;
        public event EventHandler<string>? DirectoryRenamed;

        public event EventHandler<string>? ItemDeleted;

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