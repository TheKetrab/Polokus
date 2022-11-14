using Polokus.App.Forms;
using Polokus.App.Views;
using Polokus.Core;
using Polokus.Core.Interfaces;
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
        private ServiceView _view;

        public AppHooksProvider(ContextInstance contextInstance)
        {
            _contextInstance = contextInstance;
            _view = MainWindow.Instance.GetServiceView();


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
            Thread.Sleep(3000); // delay execution
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

            _view.BeginInvoke(new Action(() => _view.UpdateProcessInstancesList(_contextInstance)));
        }

        public void OnTimeout(string processInstanceId)
        {

        }

        private void Log(string processInstanceId, string message)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(_contextInstance.Id, processInstanceId);

            var logger = _view.GetOrCreateLogger(globalInstanceId);
            if (logger == null)
            {
                return;
            }

            logger.Log(message);

            if (globalInstanceId == _view.GetOpenedProcessInstanceGlobalId())
            {
                _view.AppendLogLine(message);
            }
        }

        private void UpdateActiveNodesInGraph(string processInstanceId)
        {
            string globalInstanceId = Helpers.GetGlobalProcessInstanceId(_contextInstance.Id, processInstanceId);
            if (globalInstanceId != _view.GetOpenedProcessInstanceGlobalId())
            {
                return;
            }

            var instance = _contextInstance.ProcessInstances.FirstOrDefault(x => x.Id == processInstanceId);
            HashSet<string> activeNodesIds = instance.AvailableNodeHandlers.Values.Select(nh => nh.Node.Id).ToHashSet();

            var allNodesIds = instance.BpmnProcess.GetNodesIds();
            var inactiveNodesIds = allNodesIds.Where(x => !activeNodesIds.Contains(x));

            BpmnioClient.SetColours(_view.chromiumWindow, activeNodesIds, inactiveNodesIds);

        }

    }
}
