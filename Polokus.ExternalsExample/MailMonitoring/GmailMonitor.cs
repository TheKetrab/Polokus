using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Extensibility;

namespace Polokus.ExternalsExample.MailMonitoring
{
    public class GmailMonitor : IMonitor
    {
        private string? _lastMailId;
        private GmailManager _manager;
        private IPolokusMaster _master;

        private Task? _activeTask;
        private CancellationTokenSource? _activeCts;

        const int DelayMs = 5000;

        public GmailMonitor(IPolokusMaster master, string gmailCredentialsPath)
        {
            _master = master;
            _manager = new GmailManager(gmailCredentialsPath);
        }

        public bool IsMonitoring => _activeCts != null && _activeTask != null;

        public void StartMonitoring()
        {
            StopMonitoring();

            CancellationTokenSource cts = new();
            _activeCts = cts;
            _activeTask = new Task(async () =>
            {
                _lastMailId = await _manager.GetNewestMailId();

                while (!cts.IsCancellationRequested)
                {
                    string? mailId = await _manager.GetNewestMailId();
                    if (mailId != null && _lastMailId != mailId)
                    {
                        _lastMailId = mailId;

                        var data = await _manager.GetMailData(mailId);
                        string parameters = string.Join(';', data.Sender, data.Topic);
                        if (!cts.IsCancellationRequested)
                        {
                            _master.EmitSignal(this, "NewMail", parameters);
                        }
                    }
                    await Task.Delay(DelayMs);
                }

            }, cts.Token);

            _activeTask.Start();

        }

        public void StopMonitoring()
        {
            _activeCts?.Cancel();
            _activeCts = null;
            _activeTask = null;
        }

        public void Dispose()
        {
            StopMonitoring();
        }
    }

}
