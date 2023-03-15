using Polokus.Core.Extensibility.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Execution;
using Polokus.Tests.Helpers;

namespace Polokus.Tests.NodeHandlersTests
{
    public class MessageFlowTests : PolokusTestBase
    {
        [Test]
        public async Task MessageBetweenProcesses_TwoProcessesCommunication_ReachEnd()
        {
            // Arrange
            var master = TestHelper.ReadBpmn(Resources.MsgBetweenProcesses1,
                out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);
            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

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

            var master = BpmnLoader.LoadBpmnXmlIntoMaster(bpmnString);
            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);
            var wf = master.GetFirstWorkflow();

            string[] processIds = new string[3] { "Process_1pq1cix", "Process_0p4rtg3", "Process_1ylxybt" };
            IBpmnProcess[] bpmnProcesses = processIds.Select(pid => wf.BpmnWorkflow.BpmnProcesses.First(pr => pr.Id == pid)).ToArray();
            IFlowNode[] startNodes = bpmnProcesses.Select(pr => pr.GetManualStartNode()).ToArray();
            IProcessInstance[] processInstances = bpmnProcesses.Select(pr => wf.CreateProcessInstance(pr)).ToArray();

            const int timeout = 5;

            // Act
            for (int i = 0; i < permutation.Length; i++)
            {
                wf.StartProcessInstance(
                    processInstances[permutation[i]],
                    startNodes[permutation[i]],
                    timeout);

                await Task.Delay(1000);
            }

            await Task.Delay(timeout * 1000);

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
            var master = TestHelper.ReadBpmn(Resources.MsgBetweenProcesses4,
                 out IWorkflow wf, out IProcessInstance pi, out IFlowNode startNode);
            var visitor = new VisitorHooks(master);
            master.HooksManager.RegisterHooksProvider(visitor);

            // Act
            await wf.RunProcessAsync(pi, startNode);

            // Assert
            Assert.AreEqual(12, pi.ScriptProvider.Globals.TryGetValue<int>("z"));
        }
    }
}
