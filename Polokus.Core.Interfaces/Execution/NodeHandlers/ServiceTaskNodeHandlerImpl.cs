namespace Polokus.Core.Interfaces.Execution.NodeHandlers
{
    /// <summary>
    /// This class allows to provide own implementation of service task.
    /// </summary>
    public abstract class ServiceTaskNodeHandlerImpl
    {
        /// <summary>
        /// Owner of the implementation (ServiceTaskNodeHandler)
        /// </summary>
        protected INodeHandler Parent { get; }


        public ServiceTaskNodeHandlerImpl(INodeHandler parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// Override this method to provide custom actions while execution this service task.
        /// </summary>
        public abstract Task Run();

        /// <summary>
        /// Global variables of current process instance.
        /// </summary>
        public IScriptVariables Variables => Parent.ProcessInstance.ScriptProvider.Globals;

    }
}
