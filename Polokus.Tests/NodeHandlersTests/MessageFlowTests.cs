using Polokus.Core.Execution;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Tests.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.NodeHandlersTests
{
    public class MessageFlowTests
    {
        [Test]
        public async Task MessageBetweenProcesses_TwoProcessesCommunication_ReachEnd()
        {
            // Arrange
            VisitorHooks visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var wfm = BpmnLoader.LoadBpmnXmlIntoWorkflowsManager(Resources.MsgBetweenProcesses1, visitor);
            var wf = wfm.Workflows.First().Value;
            var bpmnProcess = wf.BpmnWorkflow.BpmnProcesses.First(x => x.Id == "Process_1fqg2b6");
            var startNode = bpmnProcess.GetManualStartNode();
            var pi = wf.CreateProcessInstance(bpmnProcess);

            // Act
            await wf.RunProcessAsync(pi, startNode, null);

            // Assert
            Assert.AreEqual("start;;;;;;;;;;;;;;;end", visitor.GetResult()); // every node visited and end achieved

        }

        [Test]
        [TestCase(2, new int[] { 0, 1, 2 })]
        [TestCase(2, new int[] { 0, 2, 1 })]
        [TestCase(2, new int[] { 1, 0, 2 })]
        [TestCase(2, new int[] { 1, 2, 0 })]
        [TestCase(2, new int[] { 2, 0, 1 })]
        [TestCase(2, new int[] { 2, 1, 0 })]
        [TestCase(3, new int[] { 0, 1, 2 })]
        [TestCase(3, new int[] { 0, 2, 1 })]
        [TestCase(3, new int[] { 1, 0, 2 })]
        [TestCase(3, new int[] { 1, 2, 0 })]
        [TestCase(3, new int[] { 2, 0, 1 })]
        [TestCase(3, new int[] { 2, 1, 0 })]
        public async Task MessageBetweenProcesses_ThreeManualProcesses1_WaitingForEachOther(int bpmnTestFile, int[] permutation)
        {
            // NOTE: processes ids are the same in both files

            // Arrange

            string bpmnString = bpmnTestFile switch
            {
                2 => Resources.MsgBetweenProcesses2,
                3 => Resources.MsgBetweenProcesses3,
                _ => throw new Exception("bpmn process undefined")
            };

            VisitorHooks visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var wfm = BpmnLoader.LoadBpmnXmlIntoWorkflowsManager(bpmnString, visitor);
            var wf = wfm.Workflows.First().Value;

            string[] processIds = new string[3] { "Process_1pq1cix", "Process_0p4rtg3", "Process_1ylxybt" };
            IBpmnProcess[] bpmnProcesses = processIds.Select(pid => wf.BpmnWorkflow.BpmnProcesses.First(pr => pr.Id == pid)).ToArray();
            IFlowNode[] startNodes = bpmnProcesses.Select(pr => pr.GetManualStartNode()).ToArray();
            IProcessInstance[] processInstances = bpmnProcesses.Select(pr => wf.CreateProcessInstance(pr)).ToArray();

            const int timeout = 5;

            // Act
            for (int i = 0; i<permutation.Length; i++)
            {
                wf.StartProcessInstance(
                    processInstances[permutation[i]],
                    startNodes[permutation[i]],
                    timeout);

                await Task.Delay(1000);
            }

            await Task.Delay(timeout);

            // Assert
            for (int i = 0; i < permutation.Length; i++)
            {
                Assert.AreEqual(ProcessStatus.Finished, processInstances[i].StatusManager.Status);
            }
        }


        [Test]
        public async Task MessageBetweenProcesses_SendingData_VariableSent()
        {
            // Arrange
            var wfm = BpmnLoader.LoadBpmnXmlIntoWorkflowsManager(Resources.MsgBetweenProcesses4);
            var wf = wfm.Workflows.First().Value;
            var bpmnProcess = wf.BpmnWorkflow.BpmnProcesses.First(x => x.Id == "Process_05l3o9f");
            var startNode = bpmnProcess.GetManualStartNode();
            var pi = wf.CreateProcessInstance(bpmnProcess);

            // Act
            await wf.RunProcessAsync(pi, startNode, null);

            // Assert
            Assert.AreEqual(12, wf.ScriptProvider.Globals.globals["z"]);
        }
    }
}
