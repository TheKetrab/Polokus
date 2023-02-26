using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Interfaces.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polokus.Core.Execution.Scripting
{
    public class CSScriptProvider : ScriptProvider
    {
        public static ScriptOptions ScriptOptions => ScriptOptions.Default
            .AddImports("System", "System.IO", "System.Collections.Generic", "System.Console",
            "System.Diagnostics", "System.Dynamic", "System.Linq", "System.Text", "System.Threading.Tasks")
            .AddReferences("System")
            .AddReferences("System.Core")
            .AddReferences("Microsoft.CSharp");


        public IScriptVariables Globals { get; } = new ScriptVariables();


        static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(3, 3);
        public override async Task<T> EvalScriptAsync<T>(string script)
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

        public override async Task EvalScriptAsync(string script)
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
