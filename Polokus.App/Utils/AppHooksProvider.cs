using Polokus.App.Forms;
using Polokus.App.Views;
using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
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
            var instance = GetProcessInstanceById(wfId, piId);
            string time = instance.StatusManager.TotalTime.ToString(@"hh\:mm\:ss\.ff");
            Log(wfId, piId, $"Process finished with result: {result}. Time: {time}");
        }

        public void BeforeExecuteNode(string wfId, string piId, IFlowNode node, int taskId, INodeCaller? caller)
        {
            UpdateActiveNodesInGraphIfNeeded(wfId, piId);
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
                    var instance = GetProcessInstanceById(wfId, piId);

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
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            var workflow = GetWorkflowById(wfId);
            _serviceView.UpdateProcessInstancesListSafe(workflow);
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            var workflow = GetWorkflowById(wfId);
            _serviceView.UpdateProcessInstancesListSafe(workflow);
        }

        public void OnTimeout(string wfId, string piId)
        {

        }

        private void Log(string wfId, string piId, string message)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(wfId, piId);

            var logger = _serviceView.PolokusMaster.GetOrCreateLogger(globalInstanceId);

            logger.Log(message);

            if (IsWorkflowActive(wfId))
            {
                _serviceView?.AppendLogLineSafe(message);
            }
        }

        private void UpdateActiveNodesInGraphIfNeeded(string wfId, string piId)
        {
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            var instance = GetProcessInstanceById(wfId, piId);

            HashSet<string> activeNodesIds = instance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = instance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(_serviceView.chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

        public void OnCallerChanged(string callerId, CallerChangedType type)
        {
            var wfId = CallersIds.GetWorkflowIdFromCaller(callerId);
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            var wf = GetWorkflowById(wfId);

            _serviceView.UpdateNodeHandlerWaitersListSafe(wf);
            _serviceView.UpdateProcessStartersListSafe(wf);
        }

        private Workflow GetWorkflowFromCaller(string callerId)
        {
            string wfId = CallersIds.GetWorkflowIdFromCaller(callerId);
            Workflow workflow = (Workflow)_serviceView.PolokusMaster.GetWorkflow(wfId);
            return workflow;
        }

        private bool IsWorkflowActive(string wfId)
        {
            return string.Equals(wfId, _serviceView.GetActiveWorkflow()?.Id);
        }

        private Workflow GetWorkflowById(string wfId)
        {
            return (Workflow)_serviceView.PolokusMaster.GetWorkflow(wfId);
        }

        private ProcessInstance GetProcessInstanceById(string wfId, string piId)
        {
            Workflow wf = GetWorkflowById(wfId);
            var pi = wf.GetProcessInstanceById(piId);
            if (pi == null)
            {
                throw new Exception("Process instance not found");
            }

            return (ProcessInstance)pi;
        }
    }
}
