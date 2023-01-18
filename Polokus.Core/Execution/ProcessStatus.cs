using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public static class ProcessStatusExtensions
    {
        public static string ToStringExt(this ProcessStatus status)
        {
            return Enum.GetName(status) ?? "";
        }

    }

}
