using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.NodeHandlers;
using Polokus.Core.NodeHandlers.Special;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Core.Factories
{
    public class NodeHandlerFactory : INodeHandlerFactory
    {
        private Dictionary<Type, Type> _nodeHandlers = new();
        private Dictionary<string, Type> _serviceTasksHandlers = new();
 
        public void RegisterNodeHandlerForServiceTask<TNodeHandler>(string serviceTask)
            where TNodeHandler : ServiceTaskNodeHandlerImpl
        {
            _serviceTasksHandlers[serviceTask] = typeof(TNodeHandler);
        }

        public void RegisterNodeHandlerForServiceTask(Type nhType, string serviceTask)
        {
            _serviceTasksHandlers[serviceTask] = nhType;
        }

        public void RegisterNodeHandlerType<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler
        {
            _nodeHandlers[typeof(TXml)] = typeof(TNodeHandler);
        }

        public void SetDefaultNodeHandlers()
        {
            RegisterNodeHandlerType<tStartEvent, StartEventHandler>();
            RegisterNodeHandlerType<tTask, TaskNodeHandler>();
            RegisterNodeHandlerType<tEndEvent, EndEventHandler>();
            RegisterNodeHandlerType<tExclusiveGateway, ExclusiveGatewayHandler>();
            RegisterNodeHandlerType<tInclusiveGateway, InclusiveGatewayHandler>();
            RegisterNodeHandlerType<tParallelGateway, ParallelGatewayNodeHandler>();
            RegisterNodeHandlerType<tServiceTask, ServiceTaskNodeHandler>();
            RegisterNodeHandlerType<tScriptTask, ScriptTaskNodeHandler>();
            RegisterNodeHandlerType<tManualTask, ManualTaskNodeHandler>();
            RegisterNodeHandlerType<tUserTask, UserTaskNodeHandler>();
            RegisterNodeHandlerType<tIntermediateCatchEvent, IntermediateCatchEventNodeHandler>();
            RegisterNodeHandlerType<tIntermediateThrowEvent, IntermediateThrowEventNodeHandler>();
            RegisterNodeHandlerType<tReceiveTask, ReceiveTaskNodeHandler>();
            RegisterNodeHandlerType<tSendTask, SendTaskNodeHandler>();
            RegisterNodeHandlerType<tSubProcess, SubProcessNodeHandler>();
            RegisterNodeHandlerType<tCallActivity, CallActivityNodeHandler>();
            RegisterNodeHandlerType<tBoundaryEvent, BoundaryEventNodeHandler>();
        }

        private Type GetTypeForNodeHandler(IFlowNode node)
        {
            if (!_nodeHandlers.ContainsKey(node.XmlType))
            {
                throw new Exception($"NodeHandler for type {node.XmlType.Name} not registered.");
            }

            return _nodeHandlers[node.XmlType];

        }

        public INodeHandler CreateNodeHandler(IProcessInstance processInstance, IFlowNode node)
        {
            Type nodeHandlerType = GetTypeForNodeHandler(node);

            INodeHandler? nodeHandler = null;
            Exception? activatorException = null;

            try
            {
                nodeHandler = Activator.CreateInstance(nodeHandlerType,
                    new object[] { processInstance, node }) as INodeHandler;
            }
            catch (MissingMethodException exc)
            {
                activatorException = exc;
            }
            
            return nodeHandler ?? throw new Exception("Unable to create nodehandler.", activatorException);
        }

        public ServiceTaskNodeHandlerImpl CreateServiceTaskNodeHandlerImpl(INodeHandler parent, string serviceTaskName)
        {
            if (!IsNodeHandlerForServiceTaskRegistered(serviceTaskName))
            {
                throw new Exception($"Not registered service task for name {serviceTaskName}");
            }

            Type serviceTaskNodeHandlerImpl = _serviceTasksHandlers[serviceTaskName];
            return Activator.CreateInstance(serviceTaskNodeHandlerImpl, new object[] { parent }) as ServiceTaskNodeHandlerImpl
                ?? throw new Exception($"Unable to create ServiceTaskNodeHandlerImpl: {serviceTaskNodeHandlerImpl.FullName}");
        }

        public bool IsNodeHandlerForServiceTaskRegistered(string serviceTask)
        {
            return _serviceTasksHandlers.ContainsKey(serviceTask);
        }


    }
}
