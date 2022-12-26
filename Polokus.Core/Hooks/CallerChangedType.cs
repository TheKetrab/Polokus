using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Hooks
{
    public enum CallerChangedType
    {
        WaiterInserted,
        WaiterRemoved,

        StarterRegistered,
        StarterDeregistered,
        StarterStartedProcess
    }

    public static class CallerChangedTypeExcensions
    {
        public static CallerChangedType CallerChangedTypeFromString(string str)
        {
            return Enum.Parse<CallerChangedType>(str);
        }
    }
}
