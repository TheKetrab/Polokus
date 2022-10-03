using Polokus.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polokus.Lib;
using Polokus.Lib.Models;

namespace Polokus.Tests
{
    public static class Utils
    {
        public const string TestsDir = @"..\..\..\";
        public const string BpmnDir = TestsDir + @"NodeHandlersTests\Bpmn\";

        public static BpmnProcess GetSingleProcessFromFile(string bpmnFile)
        {
            var ctx = BpmnParser.ParseFile($@"{BpmnDir}\{bpmnFile}");
            return ctx.Processes?.FirstOrDefault() 
                ?? throw new ArgumentException("Bpmn file hasn't any process.");
        }

        public static string SubstringBetween(string str, string sep1, string sep2)
        {
            int idx1 = str.IndexOf(sep1);
            int idx2 = str.LastIndexOf(sep2);

            if (idx1 == -1 && idx2 == -1)
            {
                throw new Exception();
            }

            idx1 += sep1.Length;
            if (idx1 > idx2)
            {
                throw new Exception();
            }

            return str.Substring(idx1, idx2-idx1);
        }
    }

}
