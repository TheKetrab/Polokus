using Polokus.Core.Interfaces.Execution;
using System.Collections.Concurrent;

namespace Polokus.Core.Execution.Scripting
{
    public class ScriptVariables : IScriptVariables
    {
        public Dictionary<string, dynamic> globals = new();

        private object _lock = new object();

        public List<object> Values => globals.Values.ToList();
        public List<string> Variables => globals.Keys.ToList();

        public object GetValue(string variable)
        {
            lock (_lock)
            {
                return globals[variable];
            }
        }

        public void SetValue(string variable, object value)
        {
            lock (_lock)
            {
                if (globals.ContainsKey(variable))
                {
                    globals[variable] = value;
                }
                else
                {
                    globals.Add(variable, value);
                }
            }
        }

        public T? TryGetValue<T>(string variable)
        {
            lock (_lock)
            {
                if (globals.ContainsKey(variable) && globals[variable] is T typedVal)
                {
                    return typedVal;
                }

                return default(T);
            }
        }

        public string GetJson()
        {
            lock (_lock)
            {
                return System.Text.Json.JsonSerializer.Serialize(globals);
            }
        }

        public void SetValues(IDictionary<string,object> values)
        {
            lock (_lock)
            {
                globals = new Dictionary<string, object>(values);
            }
        }

        public T GetValue<T>(string variable)
        {
            lock (_lock)
            {
                if (globals.ContainsKey(variable))
                {
                    if (globals[variable] is T typedVal)
                    {
                        return typedVal;
                    }

                    throw new Exception($"Variable {variable} is of type {globals[variable].GetType().FullName} but should be {typeof(T).FullName}");
                }

                throw new Exception($"Variable {variable} not found.");
            }
        }
    }
}
