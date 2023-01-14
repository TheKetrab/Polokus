using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.Execution
{
    public interface IScriptVariables
    {
        List<object> Values { get; }
        List<string> Variables { get; }
        T? TryGetValue<T>(string variable);
    }
}
