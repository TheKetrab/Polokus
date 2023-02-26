using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Execution;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

namespace Polokus.Core.Execution.Scripting
{
    public class ScriptProvider : IScriptProvider
    {
        public static ScriptOptions ScriptOptions => ScriptOptions.Default
            .AddImports("System", "System.IO", "System.Collections.Generic", "System.Console",
            "System.Diagnostics", "System.Dynamic", "System.Linq", "System.Text", "System.Threading.Tasks")
            .AddReferences("System")
            .AddReferences("System.Core")
            .AddReferences("Microsoft.CSharp");


        public IScriptVariables Globals { get; } = new ScriptVariables();

        public string MarkVariables(string script)
        {
            script = script.Replace("\r\n", "\n");

            var splitted = script.Split('\n');
            for (int i=0; i<splitted.Length; i++)
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


        static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(3, 3);
        public async Task<T> EvalCSharpScriptAsync<T>(string script)
        {
            string script2 = MarkVariables(script);

            try
            {
                await _semaphoreSlim.WaitAsync();
                var res = await CSharpScript.EvaluateAsync<T>(script2, ScriptOptions, Globals, typeof(ScriptVariables));
                return res;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _semaphoreSlim.Release();
            }
        }

        public async Task EvalCSharpScriptAsync(string script)
        {
            string script2 = MarkVariables(script);

            try
            {
                await _semaphoreSlim.WaitAsync();
                await CSharpScript.EvaluateAsync(script2, ScriptOptions, Globals, typeof(ScriptVariables));
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                _semaphoreSlim.Release();
            }
        }
    }
}
