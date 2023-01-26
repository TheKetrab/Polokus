namespace Polokus.Core.Interfaces.Execution.NodeHandlers
{
    public abstract class ServiceTaskNodeHandlerImpl
    {
        protected INodeHandler Parent { get; }

        public ServiceTaskNodeHandlerImpl(INodeHandler parent) 
        {
            Parent = parent;
        }

        public abstract Task Run();

    }
}
