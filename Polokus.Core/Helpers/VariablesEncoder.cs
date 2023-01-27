using Polokus.Core.Execution;
using Polokus.Core.Interfaces.Execution;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Helpers
{
    public static class VariablesEncoder
    {

        public static string? GetVariablesQueryString(IScriptVariables globals, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            string[] variables = str.Split(',').Select(x => x.Trim().Substring(1)).ToArray(); // skip '$'
            if (variables.Length == 0)
            {
                return null;
            }

            if (variables.Any(x => !TypeHelper.IsSerializableType(globals.GetValue(x).GetType())))
            {
                throw new Exception("Type not serializable, cannot be sent."); // TODO for future, serialize object in file
            }

            var variablesAndValues = variables
                .Select(x => Tuple.Create(
                    x,
                    globals.GetValue(x).ToString() ?? "",
                    TypeHelper.GetTypeId(globals.GetValue(x).GetType())))
                .Select(x => string.Join(',', x.Item1, x.Item2, x.Item3));

            string variablesString = "variables={" + string.Join(';', variablesAndValues) + "}";

            return variablesString;
        }

        public static void SetVariablesFromQueryString(IScriptVariables globals, string str)
        {
            if (!str.StartsWith("{") || !str.EndsWith("}"))
            {
                throw new Exception($"Wrong type of querystring: {str}");
            }

            string inside = str[1..^1];
            var splitted = inside.Split(';')
                .Select(x => x.Split(','));

            foreach (var pair in splitted)
            {
                string variable = pair[0];
                string value = pair[1];
                TypeHelper.TypeId typeId = Enum.Parse<TypeHelper.TypeId>(pair[2]);

                object typedValue = TypeHelper.Convert(value, typeId);

                globals.SetValue(variable, typedValue);
            }

        }


    }
}
