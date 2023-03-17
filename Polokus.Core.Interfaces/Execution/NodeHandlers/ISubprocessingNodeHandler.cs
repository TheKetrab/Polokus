namespace Polokus.Core.Interfaces.Execution.NodeHandlers
{
    /// <summary>
    /// NodeHandler that handles execution of subprocesses.
    /// </summary>
    public interface ISubprocessingNodeHandler : INodeHandler
    {
        /// <summary>
        /// Instance of subprocess.
        /// </summary>
        IProcessInstance? SubProcessInstance { get; }
    }

}
