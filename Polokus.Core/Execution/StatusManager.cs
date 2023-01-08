using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class StatusManager : IStatusManager, IRestorable<string>
    {
        private ProcessInstance _pi;

        public bool IsStarted => !IS(ProcessStatus.Initialized);
        public bool IsFinished => IS(ProcessStatus.Finished) || IS(ProcessStatus.Stopped);
        public bool IsActive => IsStarted && !IsFinished;
        public bool IsStopped => IS(ProcessStatus.Stopped);
        public bool IsPaused => IS(ProcessStatus.Paused);

        private bool IS(ProcessStatus status)
        {
            return ((uint)_status & (uint)status) > 0;
        }

        private ProcessStatus _status = ProcessStatus.Initialized;
        public ProcessStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                _pi.HooksProvider?.OnStatusChanged(_pi.Workflow.Id, _pi.Id);
            }
        }

        private DateTime? _beginTime;
        private DateTime? _finishTime;
        public TimeSpan TotalTime =>
            _beginTime == null ? TimeSpan.Zero
                : _finishTime == null ? DateTime.Now - _beginTime.Value
                : _finishTime.Value - _beginTime.Value;


        public StatusManager(ProcessInstance processInstance)
        {
            _pi = processInstance;
        }

        public void Pause()
        {
            var snapshot = _pi.Dump();

            _pi.ActiveTasksManager.Stop();
            _pi.KillWaiters();
            Status = ProcessStatus.Paused;

            _pi.Workflow.Paused.Add(_pi, snapshot);
        }

        public void Stop()
        {
            _pi.ActiveTasksManager.Stop();
            _pi.KillWaiters();
            Status = ProcessStatus.Stopped;
        }

        public bool IsRunning()
        {
            return _pi.ActiveTasksManager.AnyRunning() || _pi.Waiters.Any() || _pi.StatusManager.IsPaused;
        }

        public void Begin(IFlowNode startNode)
        {
            if (!startNode.IsStartNode())
            {
                throw new InvalidOperationException("Not allowed to start process on node which is not 'StartNode'.");
            }

            _beginTime = DateTime.Now;
            Status = ProcessStatus.Running;
            _pi.StartNewSequence(startNode, null);
        }

        public void Resume()
        {
            var snapshot = _pi.Workflow.Paused[_pi];
            _pi.Workflow.Paused.Remove(_pi);

            _pi.Restore(_pi.Workflow.PolokusMaster, snapshot);
            Status = ProcessStatus.Running;
        }

        public void Finish()
        {
            if (IsRunning())
            {
                throw new Exception("Unable to finish running process.");
            }

            _finishTime = DateTime.Now;

            if (!IsFinished)
            {
                Status = ProcessStatus.Finished;
            }
        }

        public void Restore(IPolokusMaster master, string source)
        {
            _status = Enum.Parse<ProcessStatus>(source);
        }
    }
}
