using Polokus.App.Forms;
using Polokus.App.Views;
using Polokus.Core.Execution;
using Polokus.Core.Helpers;
using Polokus.Core.Hooks;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.Services.Interfaces;
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
        private IServicesProvider _services;

        public AppHooksProvider(ServiceView serviceView, IServicesProvider services)
        {
            _serviceView = serviceView;
            _services = services;
        }        

        public void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId)
        {
            Log(wfId, piId, $"Node {nodeId} finished with failure. task: {taskId}");
        }

        public void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            Log(wfId, piId, $"Node {nodeId} finished with success. task: {taskId}");
        }

        public void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId)
        {
            Log(wfId, piId, $"Node {nodeId} finished with suspension. task: {taskId}");
        }

        public void OnProcessFinished(string wfId, string piId, string result)
        {
            string time = _services.ProcessInstancesService.GetTotalTime(wfId, piId);
            Log(wfId, piId, $"Process {piId} finished with result: {result}. Time: {time}");
        }

        public void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? nodeCallerId)
        {
            UpdateActiveNodesInGraphIfNeeded(wfId, piId);
            Log(wfId, piId, $"Executing: {nodeId} taskId = {taskId}");
            //Thread.Sleep(_serviceView.PolokusMaster.SettingsProvider.DelayForNodeHandlerMs); // delay execution
            //Thread.Sleep(500); // TODO



        }

        public void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? nodeCallerId)
        {

        }

        public void OnStatusChanged(string wfId, string piId)
        {
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            _serviceView.UpdateProcessInstancesListSafe(wfId);
        }

        public void OnTasksChanged(string wfId, string piId)
        {
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            _serviceView.UpdateProcessInstancesListSafe(wfId);
        }

        public void OnTimeout(string wfId, string piId)
        {

        }

        private void Log(string wfId, string piId, string message)
        {
            string globalPiId = Helpers.GetGlobalProcessInstanceId(wfId, piId);
            _services.LogsService.Log(globalPiId, Logger.MsgType.Simple, message);

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

            HashSet<string> activeNodesIds = _services.ProcessInstancesService
                .GetActiveNodesIds(wfId, piId).ToHashSet();

            var allNodesIds = _services.ProcessInstancesService.GetAllNodesIds(wfId, piId);
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(_serviceView.chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

        public void OnCallerChanged(string callerId, string callerChangedType)
        {
            var wfId = EncodingIds.GetWorkflowIdFromCaller(callerId);
            if (!IsWorkflowActive(wfId))
            {
                return;
            }

            _serviceView.UpdateNodeHandlerWaitersListSafe(wfId);
            _serviceView.UpdateProcessStartersListSafe(wfId);
        }

        private bool IsWorkflowActive(string wfId)
        {
            return string.Equals(wfId, _serviceView.ActiveWorkflow);
        }

        public void OnAwaitingTokenCreated(string wfId, string piId, string nodeId, string token)
        {
            Type nodeXmlType = _services.ProcessInstancesService.GetNodeXmlType(wfId, piId, nodeId);
            if (nodeXmlType == typeof(tManualTask))
            {
                string nodeName = _services.ProcessInstancesService.GetNodeName(wfId, piId, nodeId);
                MessageBox.Show($"Waiting for manual task: {nodeName}");
                _services.ProcessInstancesService.RemoveAwaitingToken(wfId,piId,token);
            }
            else if (nodeXmlType == typeof(tUserTask))
            {
                string nodeName = _services.ProcessInstancesService.GetNodeName(wfId, piId, nodeId);

                var dialog = new UserTaskDialog(nodeName);
                if (dialog.ShowDialog() == DialogResult.OK && dialog.Answer != null)
                {
                    _services.ProcessInstancesService
                        .SetUserDecisionForUserTaskNH(wfId, piId, nodeId, dialog.Answer);
                }
                _services.ProcessInstancesService.RemoveAwaitingToken(wfId, piId, token);
            }
        }
    }
}
