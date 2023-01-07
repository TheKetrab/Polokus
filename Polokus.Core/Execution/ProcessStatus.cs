using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public enum ProcessStatus
    {
        Initialized = 0,
        Running = 1 << 0,
        Paused = 1 << 1,
        Finished = 1 << 2,
        Stopped = 1 << 3
    }

    public static class ProcessStatusExtensions
    {
        public static string ToStringExt(this ProcessStatus status)
        {
            return Enum.GetName(status) ?? "";
        }

    }

}
