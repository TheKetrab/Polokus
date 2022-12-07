﻿using Polokus.Core;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Tests.Helpers;
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
            var cm = BpmnLoader.LoadBpmnXmlIntoContextsManager("msgBetweenProcesses1.bpmn", visitor);
            var ci = cm.ContextInstances.First().Value;
            var bpmnProcess = ci.BpmnContext.BpmnProcesses.First(x => x.Id == "Process_1fqg2b6");
            var startNode = bpmnProcess.GetManualStartNode();
            var pi = ci.CreateProcessInstance(bpmnProcess);

            // Act
            await ci.RunProcessAsync(pi, startNode, null);

            // Assert
            Assert.AreEqual("start;;;;;;;;;;;;;;;end", visitor.GetResult()); // every node visited and end achieved

        }

        [Test]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 0, 1, 2 })]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 0, 2, 1 })]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 1, 0, 2 })]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 1, 2, 0 })]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 2, 0, 1 })]
        [TestCase("msgBetweenProcesses2.bpmn", new int[] { 2, 1, 0 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 0, 1, 2 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 0, 2, 1 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 1, 0, 2 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 1, 2, 0 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 2, 0, 1 })]
        [TestCase("msgBetweenProcesses3.bpmn", new int[] { 2, 1, 0 })]
        public async Task MessageBetweenProcesses_ThreeManualProcesses1_WaitingForEachOther(string processName, int[] permutation)
        {
            // NOTE: processes ids are the same in both files

            // Arrange
            VisitorHooks visitor = new VisitorHooks(VisitTime.BeforeExecute);
            var cm = BpmnLoader.LoadBpmnXmlIntoContextsManager(processName, visitor);
            var ci = cm.ContextInstances.First().Value;

            string[] processIds = new string[3] { "Process_1pq1cix", "Process_0p4rtg3", "Process_1ylxybt" };
            IBpmnProcess[] bpmnProcesses = processIds.Select(pid => ci.BpmnContext.BpmnProcesses.First(pr => pr.Id == pid)).ToArray();
            IFlowNode[] startNodes = bpmnProcesses.Select(pr => pr.GetManualStartNode()).ToArray();
            IProcessInstance[] processInstances = bpmnProcesses.Select(pr => ci.CreateProcessInstance(pr)).ToArray();

            const int timeout = 5;

            // Act
            for (int i = 0; i<permutation.Length; i++)
            {
                ci.StartProcessInstance(
                    processInstances[permutation[i]],
                    startNodes[permutation[i]],
                    timeout);

                await Task.Delay(1000);
            }

            await Task.Delay(timeout);

            // Assert
            for (int i = 0; i < permutation.Length; i++)
            {
                Assert.AreEqual(ProcessStatus.Finished, processInstances[i].Status);
            }
        }


        [Test]
        public async Task MessageBetweenProcesses_SendingData_VariableSent()
        {
            // Arrange
            var cm = BpmnLoader.LoadBpmnXmlIntoContextsManager("msgBetweenProcesses4.bpmn");
            var ci = cm.ContextInstances.First().Value;
            var bpmnProcess = ci.BpmnContext.BpmnProcesses.First(x => x.Id == "Process_05l3o9f");
            var startNode = bpmnProcess.GetManualStartNode();
            var pi = ci.CreateProcessInstance(bpmnProcess);

            // Act
            await ci.RunProcessAsync(pi, startNode, null);

            // Assert
            Assert.AreEqual(12, ci.ScriptProvider.Globals.globals["z"]);
        }
    }
}
