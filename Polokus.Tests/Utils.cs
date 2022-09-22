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
    }

}
