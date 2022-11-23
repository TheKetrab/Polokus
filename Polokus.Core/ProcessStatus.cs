using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core
{
    public enum ProcessStatus
    {
        Initialized,
        Running,
        Paused,
        Finished
    }

    public static class ProcessStatusExtensions
    {
        public static string ToStringExt(this ProcessStatus status)
        {
            return Enum.GetName(status) ?? "";
        }

    }

}
