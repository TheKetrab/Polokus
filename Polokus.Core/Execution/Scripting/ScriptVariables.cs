using Polokus.Core.Interfaces.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution.Scripting
{
    public class ScriptVariables : IScriptVariables
    {
        public Dictionary<string, dynamic> globals = new();

        public List<object> Values => globals.Values.ToList();
        public List<string> Variables => globals.Keys.ToList();
        public T? TryGetValue<T>(string variable)
        {
            if (globals.ContainsKey(variable) && globals[variable] is T typedVal)
            {
                return typedVal;
            }

            return default(T);
        }
    }
}
