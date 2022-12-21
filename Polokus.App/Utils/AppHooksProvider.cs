using Polokus.App.Forms;
using Polokus.App.Views;
using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public class AppHooksProvider : IHooksProvider
    {
        private ServiceView _serviceView;

        public AppHooksProvider(ServiceView serviceView)
        {
            _serviceView = serviceView;
        }

        public ProcessInstance GetProcessInstance(string wfId, string piId)
        {
            Workflow wf = _serviceView.GetWorkflowById(wfId);
            return (ProcessInstance)wf.GetProcessInstanceById(piId);
        }
        

        public void AfterExecuteNodeFailure(string wfId, string piId, IFlowNode node, int taskId)
        {

        }

        public void AfterExecuteNodeSuccess(string wfId, string piId, IFlowNode node, int taskId)
        {

        }

        public void AfterExecuteNodeSuspension(string wfId, string piId, IFlowNode node, int taskId)
        {

        }

        public void OnProcessFinished(string wfId, string piId, string result)
        {
            var instance = GetProcessInstance(wfId, piId);
            string time = instance.StatusManager.TotalTime.ToString(@"hh\:mm\:ss\.ff");
            Log(wfId, piId, $"Process finished with result: {result}. Time: {time}");
        }

        public void BeforeExecuteNode(string wfId, string piId, IFlowNode node, int taskId, INodeCaller? caller)
        {
            UpdateActiveNodesInGraph(wfId, piId);
            Log(wfId, piId, $"Executing: {node.Id} taskId = {taskId}");
            Thread.Sleep(_serviceView.PolokusMaster.SettingsProvider.DelayForNodeHandlerMs); // delay execution

            if (node.XmlType == typeof(tManualTask))
            {
                MessageBox.Show($"Waiting for manual task: {node.Name}");
            }
            else if (node.XmlType == typeof(tUserTask))
            {
                var dialog = new UserTaskDialog(node.Name);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string answer = dialog.Answer;
                    var instance = GetProcessInstance(wfId, piId);

                    var nodeHandler = instance.GetNodeHandlerForNodeIfExists(node);

                    if (nodeHandler is UserTaskNodeHandler nh)
                    {
                        nh.SetUserDecision(answer);
                    }
                }

            }

        }

        public void BeforeStartNewSequence(string wfId, string piId, IFlowNode firstNode, INodeCaller? caller)
        {

        }

        public void OnStatusChanged(string wfId, string piId)
        {
            var instance = GetProcessInstance(wfId, piId);

            // TODO safe invocation
            //_serviceView?.BeginInvoke(new Action(() => View.UpdateProcessInstancesList(_workflow)));
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            if (_serviceView.GetActiveWorkflow().Id == wfId)
            {
                // TODO safe invocation
                //View.BeginInvoke(new Action(() => View.UpdateProcessInstancesList(_workflow)));
            }
        }

        public void OnTimeout(string wfId, string piId)
        {

        }

        private void Log(string wfId, string piId, string message)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(wfId, piId);

            var logger = _serviceView?.GetOrCreateLogger(globalInstanceId);
            if (logger == null)
            {
                return;
            }

            logger.Log(message);

            if (globalInstanceId == _serviceView?.GetOpenedProcessInstanceGlobalId())
            {
                _serviceView?.AppendLogLine(message);
            }
        }

        private void UpdateActiveNodesInGraph(string wfId, string piId)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(wfId, piId);
            if (globalInstanceId != _serviceView?.GetOpenedProcessInstanceGlobalId())
            {
                return;
            }

            var instance = GetProcessInstance(wfId, piId);

            HashSet<string> activeNodesIds = instance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = instance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(_serviceView.chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

    }
}
