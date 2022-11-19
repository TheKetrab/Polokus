﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;

namespace Polokus.Core.Scripting
{
    public class ScriptProvider : IScriptProvider
    {
        public ScriptOptions ScriptOptions => ScriptOptions.Default
            .AddImports("System", "System.IO", "System.Collections.Generic", "System.Console",
            "System.Diagnostics", "System.Dynamic", "System.Linq", "System.Text", "System.Threading.Tasks")
            .AddReferences("System")
            .AddReferences("System.Core")
            .AddReferences("Microsoft.CSharp");


        public ScriptVariables Globals { get; } = new();

        public string MarkVariables(string script)
        {
            string pattern = @".*\$(.).*";

            string result = script;
            while (Regex.IsMatch(result, pattern))
            {
                Match m = Regex.Match(result, pattern);
                string replacement = $"globals[\"$1\"]";
                Regex rgx = new Regex(@"\$(.)");
                result = rgx.Replace(result, replacement, 1);
            }
            return result;
        }

        public string Decode(string script)
        {
            return WebUtility.HtmlDecode(script);
        }

        public async Task<T> EvalCSharpScriptAsync<T>(string script)
        {
            string script2 = MarkVariables(script);
            return await CSharpScript.EvaluateAsync<T>(script2, ScriptOptions, Globals, typeof(ScriptVariables));
        }

        public async Task EvalCSharpScriptAsync(string script)
        {
            string script2 = MarkVariables(script);
            await CSharpScript.EvaluateAsync(script2, ScriptOptions, Globals, typeof(ScriptVariables));
        }

    }
}