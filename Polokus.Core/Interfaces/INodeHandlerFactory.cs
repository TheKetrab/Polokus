using Polokus.Core.Models.BpmnObjects.Xsd;

namespace Polokus.Core.Interfaces
{
    /// <summary>
    /// This object is responsible for creating NodeHandlers 
    /// using defined rules for tFlowNodes and ServiceTasks.
    /// </summary>
    public interface INodeHandlerFactory
    {
        /// <summary>
        /// This method creates NodeHandler for concret instance of BPMN process for given node.
        /// </summary>
        /// <param name="processInstance">Instance of BPMN process.</param>
        /// <param name="node">FlowNode for which NodeHandler is to be created.</param>
        INodeHandler CreateNodeHandler(IProcessInstance processInstance, IFlowNode node);

        /// <summary>
        /// This method registers type to use for handling FlowNode of type <typeparamref name="TXml"/>.
        /// </summary>
        /// <typeparam name="TXml">Type of FlowNode.</typeparam>
        /// <typeparam name="TNodeHandler">Type of NodeHandler.</typeparam>
        void RegisterNodeHandlerType<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler;

        /// <summary>
        /// This method registers type to use for service task of name <paramref name="serviceTask"/>.
        /// </summary>
        /// <typeparam name="TNodeHandler">Type that derives from ServiceTaskNodeHandler.</typeparam>
        /// <param name="serviceTask">Name of ServiceTask to handling.</param>
        void RegisterNodeHandlerForServiceTask<TNodeHandler>(string serviceTask)
            where TNodeHandler : class, INodeHandler;

        /// <summary>
        /// This method registers type to use for service task of name <paramref name="serviceTask"/>.
        /// </summary>
        /// <param name="nhType">Type that derives from ServiceTaskNodeHandler.</param>
        /// <param name="serviceTask">Name of ServiceTask to handling.</param>
        void RegisterNodeHandlerForServiceTask(Type nhType, string serviceTask);

        /// <summary>
        /// This method returns true iif any type is registred for <paramref name="serviceTask"/>.
        /// </summary>
        /// <param name="serviceTask">Name of service task.</param>
        bool IsNodeHandlerForServiceTaskRegistered(string serviceTask);

    }
}
