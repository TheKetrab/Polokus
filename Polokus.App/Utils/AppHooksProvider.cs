using Polokus.App.Forms;
using Polokus.App.Views;
using Polokus.Core;
using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public class AppHooksProvider : IHooksProvider
    {
        private ContextInstance _contextInstance;
        private ServiceView? _view;
        private ServiceView? View => _view ??= MainWindow.Instance.ServiceView;

        public AppHooksProvider(ContextInstance contextInstance)
        {
            _contextInstance = contextInstance;
        }

        

        public void AfterExecuteNodeFailure(string processInstanceId, IFlowNode node, int taskId)
        {

        }

        public void AfterExecuteNodeSuccess(string processInstanceId, IFlowNode node, int taskId)
        {

        }

        public void AfterExecuteNodeSuspension(string processInstanceId, IFlowNode node, int taskId)
        {

        }

        public void BeforeExecuteNode(string processInstanceId, IFlowNode node, int taskId, INodeCaller? caller)
        {
            UpdateActiveNodesInGraph(processInstanceId);
            Log(processInstanceId, $"Executing: {node.Id} taskId = {taskId}");
            Thread.Sleep(300); // delay execution

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
                    MessageBox.Show(answer);
                }

            }

        }

        public void BeforeStartNewSequence(string processInstanceId, IFlowNode firstNode, INodeCaller? caller)
        {

        }

        public void OnStatusChanged(string processInstanceId)
        {
            var instance = _contextInstance.GetProcessInstanceById(processInstanceId);
            if (instance.Status == ProcessStatus.Finished)
            {
                Log(processInstanceId, $"Process finished. Time: {instance.TotalTime}");
            }

            View?.BeginInvoke(new Action(() => View.UpdateProcessInstancesList(_contextInstance)));
        }

        public void OnTasksChanged(string processInstanceId)
        {
            if (View?.GetActiveContextInstance() == _contextInstance)
            {
                View.BeginInvoke(new Action(() => View.UpdateProcessInstancesList(_contextInstance)));
            }
        }

        public void OnTimeout(string processInstanceId)
        {

        }

        private void Log(string processInstanceId, string message)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(_contextInstance.Id, processInstanceId);

            var logger = View?.GetOrCreateLogger(globalInstanceId);
            if (logger == null)
            {
                return;
            }

            logger.Log(message);

            if (globalInstanceId == View?.GetOpenedProcessInstanceGlobalId())
            {
                View?.AppendLogLine(message);
            }
        }

        private void UpdateActiveNodesInGraph(string processInstanceId)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(_contextInstance.Id, processInstanceId);
            if (globalInstanceId != View?.GetOpenedProcessInstanceGlobalId())
            {
                return;
            }

            var instance = _contextInstance.ProcessInstances.FirstOrDefault(x => x.Id == processInstanceId);
            HashSet<string> activeNodesIds = instance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = instance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(View.chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

    }
}
