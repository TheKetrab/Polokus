using Polokus.Core.Interfaces.Communication.Models;

namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Service that allows remote control of workflow.
    /// </summary>
    public interface IWorkflowsService
    {
        /// <summary>
        /// Returns Ids of all Bpmn processes defined within the workflow.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public IEnumerable<string> GetBpmnProcessesIds(string wfId);

        /// <summary>
        /// Returns Ids of all Bpmn process defined within the workflow that are manual (not to be started by a starter).
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public IEnumerable<string> GetManualBpmnProcessesIds(string wfId);

        /// <summary>
        /// Returns basic information about all process instances.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public IEnumerable<RawProcessInstance> GetProcessInstancesInfos(string wfId);

        /// <summary>
        /// Returns basic information about all process starters defined for given Workflow.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public IEnumerable<RawProcessStarter> GetProcessStarters(string wfId);

        /// <summary>
        /// Returns XML string of source bpmn file that Workflow was made from.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public string GetRawString(string wfId);

        /// <summary>
        /// Returns basic information about all active waiters for given Workflow.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        public IEnumerable<RawNodeHandlerWaiter> GetNodeHandlerWaiters(string wfId);

        /// <summary>
        /// Starts a new process instance of given Bpmn process (starts iff there is single manual start node).
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="bpmnProcessId">Bpmn process Id.</param>
        public string StartProcessManually(string wfId, string bpmnProcessId);

        /// <summary>
        /// Stops running process instance (= kills, unable to resume).
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public void StopProcessInstance(string wfId, string piId);

        /// <summary>
        /// Pauses running process instance (possible to resume later).
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public void PauseProcessInstance(string wfId, string piId);

        /// <summary>
        /// Resumes paused process instance.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="piId">Process instance Id.</param>
        public void ResumeProcessInstance(string wfId, string piId);

        /// <summary>
        /// Sends request to Message Manager, therefore waiter can call FlowNode and process continues.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="listenerId">Id of listener to call (waiter).</param>
        public void PingListener(string wfId, string listenerId);

        /// <summary>
        /// Raises signal to Singal Manager, therefore waiter can call FlowNode and process continues.
        /// </summary>
        /// <param name="wfId">Workflow Id.</param>
        /// <param name="signal">Id of signal to call (waiter).</param>
        public void RaiseSignal(string wfId, string signal);
    }
}
