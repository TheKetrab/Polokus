using Polokus.Core;
using Polokus.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.ConsoleApp.ManualTests
{
    internal class ManualTest
    {
        public const string TestsDir = @"..\..\..\";
        public const string BpmnDir = TestsDir + @"ManualTests\Bpmn\";

        private string _bpmnFile;
        private string? _hint;

        public ManualTest(string bpmnFile, string? hint = null)
        {
            _bpmnFile = bpmnFile;
            _hint = hint;
        }

        public async Task<bool> RunTest(int? id = null)
        {
            Console.WriteLine(new string('-', 100));
            if (id != null)
            {
                Console.WriteLine($"{" ",30} --> Test no {id} <-- ");
                Console.WriteLine(new string('-', 100));
            }
            Console.WriteLine($" -> Running test: {this.GetType().Name}({_bpmnFile})");
            if (_hint != null)
            {
                Console.WriteLine($" -> Hint: {_hint}");
            }
            Console.WriteLine();

            var wfManager = new WorkflowsManager();
            string path = Path.Combine(BpmnDir, _bpmnFile);
            if (!File.Exists(path))
            {
                Console.WriteLine($" -> Path does not exists: {path}");
                Console.WriteLine(" -> Test skipped");
                return false;
            }

            string str = File.ReadAllText(path);
            wfManager.LoadXmlString(str, "Test");
            var wf = wfManager.Workflows.First().Value;
            var bpmnProcess = wf.BpmnWorkflow.BpmnProcesses.First();
            var startNode = bpmnProcess.GetManualStartNode();
            var pi = wf.CreateProcessInstance(bpmnProcess);

            InitTest(wfManager);

            bool success = await wf.RunProcessAsync(pi,startNode);
            Console.WriteLine(success ? " -> Process finished sucessfully" : " -> Timeout");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(" -> Log:");
            Console.WriteLine(Logger.Global.GetFullLog(true));
            Console.WriteLine(new string('-', 100));
            Console.ReadLine();

            return success;
        }

        protected virtual void InitTest(WorkflowsManager wfManager) { }

    }
}
