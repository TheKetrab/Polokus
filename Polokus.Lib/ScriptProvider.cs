using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Polokus.Lib
{
    public class ScriptProvider
    {
        public class VarsTest { public int x = 8; }
        public VarsTest globals = new VarsTest();
        
        public async Task<T> EvalCSharpScriptAsync<T>(string script)
        {
            return await CSharpScript.EvaluateAsync<T>(script, ScriptOptions.Default, globals, typeof(VarsTest));
        }

        public async Task EvalCSharpScriptAsync(string script)
        {
            await CSharpScript.EvaluateAsync(script, ScriptOptions.Default, globals, typeof(VarsTest));
        }

    }
}
