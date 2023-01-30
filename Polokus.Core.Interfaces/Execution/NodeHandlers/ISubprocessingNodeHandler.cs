namespace Polokus.Core.Interfaces.Execution.NodeHandlers
{
    public interface ISubprocessingNodeHandler : INodeHandler
    {
        IProcessInstance? SubProcessInstance { get; }
    }

}
