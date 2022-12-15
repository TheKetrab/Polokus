using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IStatusManager
    {
        ProcessStatus Status { get; }
        bool IsStarted { get; }
        bool IsFinished { get; }
        bool IsActive { get; }
        TimeSpan TotalTime { get; }

        void Begin(IFlowNode startNode);
        void Stop();
        void Resume();
        void Pause();
        void Finish();
        bool IsRunning();


    }
}
