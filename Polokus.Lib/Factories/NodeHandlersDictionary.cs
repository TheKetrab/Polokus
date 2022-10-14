using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.Models;
using Polokus.Lib.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Factories
{
    public class NodeHandlersDictionary
    {

        private ProcessInstance _process;
        private Dictionary<Type, Type> _nodeHandlers = new();
        private Dictionary<string, Type> _serviceTasksHandlers = new();

        public NodeHandlersDictionary(ProcessInstance process)
        {
            _process = process;
        }

        public void SetDefaultNodeHandlers()
        {
            SetNodeHandler<tStartEvent, StartEventHandler>();
            SetNodeHandler<tTask, TaskNodeHandler>();
            SetNodeHandler<tEndEvent, EndEventHandler>();
            SetNodeHandler<tExclusiveGateway, ExclusiveGatewayHandler>();
            SetNodeHandler<tInclusiveGateway, InclusiveGatewayHandler>();
            SetNodeHandler<tParallelGateway, ParallelGatewayNodeHandler>();
            SetNodeHandler<tServiceTask, ServiceTaskNodeHandler>();
            SetNodeHandler<tScriptTask, ScriptTaskNodeHandler>();
            SetNodeHandler<tManualTask, ManualTaskNodeHandler>();
            SetNodeHandler<tUserTask, UserTaskNodeHandler>();
            SetNodeHandler<tIntermediateCatchEvent, IntermediateCatchEventNodeHandler>();
        }

        private Tuple<Type, Type> NH<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler
        {
            return Tuple.Create(typeof(TXml), typeof(TNodeHandler));
        }


        public INodeHandler CreateNodeHandlerFor(IFlowNode node)
        {
            Type xmlType = node.XmlType;
            if (!_nodeHandlers.ContainsKey(xmlType))
            {
                Logger.LogError($"NodeHandler for type {xmlType.Name} not registered.");
                return new EmptyNodeHandler();
            }

            if (xmlType == typeof(tServiceTask))
            {
                Type t = _serviceTasksHandlers[node.Name];
                INodeHandler? nh = Activator.CreateInstance(t, new[] { node }) as INodeHandler;
                return nh;
            }

            Type nodeHandlerType = _nodeHandlers[xmlType];


            INodeHandler? handler = Activator.CreateInstance(nodeHandlerType, new[] { node }) as INodeHandler;
            //INodeHandler? handler = NodeHandlersFactory.CreateNodeHandler(node);
            return handler ?? throw new Exception("Unable to create nodehandler.");
        }


        public void SetNodeHandler<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler
        {
            _nodeHandlers[typeof(TXml)] = typeof(TNodeHandler);
        }

        public void SetNodeHandlerForServiceTask<TNodeHandler>(string serviceTask)
            where TNodeHandler : class, INodeHandler
        {
            _serviceTasksHandlers[serviceTask] = typeof(TNodeHandler);
        }

    }
}
