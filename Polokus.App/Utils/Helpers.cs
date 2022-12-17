using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    internal static class Helpers
    {
        public static string GetGlobalProcessInstanceId(string wfId, string processInstanceId)
        {
            return $"{wfId}/{processInstanceId}";
        }
    }
}
