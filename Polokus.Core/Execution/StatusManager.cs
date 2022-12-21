using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class StatusManager : IStatusManager
    {
        private ProcessInstance _pi;

        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsActive { get; private set; }


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
            _pi.ActiveTasksManager.Pause();
            Status = ProcessStatus.Paused;
        }

        public void Stop()
        {
            _pi.ActiveTasksManager.Stop();
            Status = ProcessStatus.Stopped;
            IsActive = false;
        }

        public bool IsRunning()
        {
            return IsActive && (_pi.ActiveTasksManager.AnyRunning() || _pi.Waiters.Any());
        }

        public void Begin(IFlowNode startNode)
        {
            if (!startNode.IsStartNode())
            {
                throw new InvalidOperationException("Not allowed to start process on node which is not 'StartNode'.");
            }

            _beginTime = DateTime.Now;
            IsActive = true;
            Status = ProcessStatus.Running;
            _pi.StartNewSequence(startNode, null);
        }

        public void Resume()
        {
            _pi.ActiveTasksManager.Resume();
            Status = ProcessStatus.Running;
        }

        public void Finish()
        {
            if (IsRunning())
            {
                throw new Exception("Unable to finish running process.");
            }

            IsFinished = true;
            _finishTime = DateTime.Now;
            Status = ProcessStatus.Finished;
        }

    }
}
