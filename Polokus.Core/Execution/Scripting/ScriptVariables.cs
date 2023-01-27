using Polokus.Core.Interfaces.Execution;
using System.Collections.Concurrent;

namespace Polokus.Core.Execution.Scripting
{
    public class ScriptVariables : IScriptVariables
    {
        public ConcurrentDictionary<string, dynamic> globals = new ();


        public List<object> Values => globals.Values.ToList();
        public List<string> Variables => globals.Keys.ToList();

        public object GetValue(string variable)
        {
            return globals[variable];
        }

        public void SetValue(string variable, object value)
        {
            globals.AddOrUpdate(variable, value, (k, v) => value);
        }

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
