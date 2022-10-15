using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Polokus.Core.Scripting.ScriptProvider;

namespace Polokus.Core.Interfaces
{
    public interface IScriptProvider
    {
        ScriptVariables Globals { get; }
        string Decode(string script);
        Task<T> EvalCSharpScriptAsync<T>(string script);
        Task EvalCSharpScriptAsync(string script);
    }
}
