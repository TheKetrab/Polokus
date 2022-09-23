using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.Models;
using Polokus.Lib.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib
{
    public class NodeHandlersDictionary
    {

        private ProcessInstance _process;
        private Dictionary<Type, Type> _nodeHandlers = new();

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
            //SetNodeHandler<tInclusiveGateway, InclusiveGatewayHandler>();
            SetNodeHandler<tParallelGateway, ParallelGatewayNodeHandler>();
        }

        private Tuple<Type, Type> NH<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler
        {
            return Tuple.Create(typeof(TXml), typeof(TNodeHandler));
        }

        public INodeHandler CreateNodeHandlerOfType(Type xmlType)
        {
            if (!_nodeHandlers.ContainsKey(xmlType))
            {
                Logger.LogError($"NodeHandler for type {xmlType.Name} not registered.");
                return new EmptyNodeHandler();
            }

            Type nodeHandlerType = _nodeHandlers[xmlType];
            INodeHandler? handler = Activator.CreateInstance(nodeHandlerType, new[] { _process }) as INodeHandler;
            return handler ?? throw new Exception("Unable to create nodehandler.");
        }

        public INodeHandler CreateNodeHandlerFor(FlowNode node)
        {
            return CreateNodeHandlerOfType(node.XmlType);
        }


        public void SetNodeHandler<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler
        {
            _nodeHandlers[typeof(TXml)] = typeof(TNodeHandler);
        }

    }
}
