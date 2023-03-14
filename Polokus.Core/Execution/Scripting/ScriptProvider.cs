using System.Net;
using System.Text.RegularExpressions;

namespace Polokus.Core.Execution.Scripting
{
    public abstract class ScriptProvider : IScriptProvider
    {
        public IScriptVariables Globals { get; } = new ScriptVariables();

        public string MarkVariables(string script)
        {
            script = script.Replace("\r\n", "\n");

            var splitted = script.Split('\n');
            for (int i = 0; i < splitted.Length; i++)
            {
                splitted[i] = MarkVariablesSingleLine(splitted[i]);
            }
            return string.Join('\n', splitted);
        }

        public bool IsValidOutgoingVariable(string str)
        {
            string pattern = @"^\$([a-zA-Z0-9_])+$";
            return Regex.IsMatch(str, pattern);
        }

        public string MarkVariablesSingleLine(string script)
        {
            string pattern = @"^.*\$([a-zA-Z0-9_]+)((;|\s+).*|$)";

            string result = script;
            while (Regex.IsMatch(result, pattern))
            {
                Match m = Regex.Match(result, pattern, RegexOptions.RightToLeft);
                string var = m.Groups[1].Value;
                result = Regex.Replace(result, $@"\${var}", $@"globals[""{var}""]");
            }
            return result;
        }

        public string Decode(string script)
        {
            return WebUtility.HtmlDecode(script);
        }

        public abstract Task<T> EvalScriptAsync<T>(string script);
        public abstract Task EvalScriptAsync(string script);
    }
}
