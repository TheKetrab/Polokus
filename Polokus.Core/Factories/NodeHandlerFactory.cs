using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Factories
{
    public class NodeHandlerFactory : INodeHandlerFactory
    {
        private Dictionary<Type, Type> _nodeHandlers = new();
        private Dictionary<string, Type> _serviceTasksHandlers = new();
 
        public void RegisterNodeHandlerForServiceTask<TNodeHandler>(string serviceTask)
            where TNodeHandler : class, INodeHandler
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
        }

        private Type GetTypeForNodeHandler(IFlowNode node)
        {
            if (!_nodeHandlers.ContainsKey(node.XmlType))
            {
                throw new Exception($"NodeHandler for type {node.XmlType.Name} not registered.");
            }

            if (node.XmlType == typeof(tServiceTask))
            {
                if (!_serviceTasksHandlers.ContainsKey(node.Name))
                {
                    throw new Exception($"NodeHandler for service task {node.Name} not registered.");
                }

                return _serviceTasksHandlers[node.Name];
                
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

        public bool IsNodeHandlerForServiceTaskRegistered(string serviceTask)
        {
            return _serviceTasksHandlers.ContainsKey(serviceTask);
        }


    }
}
