using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class NodeHandlersManager
    {

        private ProcessInstance _process;
        public NodeHandlersManager(ProcessInstance process)
        {
            _process = process;
            SetDefaultNodeHandlers();
        }

        private static readonly Type baseNodeType = typeof(tFlowNode);

        public void SetDefaultNodeHandlers()
        {
            SetNodeHandlers(new[] { 
                NH<tStartEvent,StartEventHandler>(),
                NH<tTask,TaskNodeHandler>(),
                NH<tEndEvent,EndEventHandler>(),
                NH<tExclusiveGateway,ExclusiveGatewayHandler>(),
                NH<tInclusiveGateway,InclusiveGatewayHandler>(),
            });
        }

        private Tuple<Type,INodeHandler> NH<TXml,TNodeHandler>()
        {
            INodeHandler handler = (INodeHandler)Activator.CreateInstance(typeof(TNodeHandler), new[] { _process });
            return Tuple.Create(typeof(TXml), handler);
        }

        public void RegisterAction(Type type, EventHandler<FlowNode> action)
        {
            if (!_nodeHandlers.ContainsKey(type))
            {
                Logger.LogWarning($"RegisterAction: Not found key {type.FullName} in nodeHandlers.");
                return;
            }

            _nodeHandlers[type].Finished += action;
        }

        public IEnumerable<INodeHandler> GetNodeHandlers()
        {
            return _nodeHandlers.Values;
        }

        private Dictionary<Type, INodeHandler> _nodeHandlers = new();
        public void SetNodeHandlers(IEnumerable<Tuple<Type,INodeHandler>> nodeHandlers)
        {
            ValidateArguments(nodeHandlers);
            _nodeHandlers = nodeHandlers.ToDictionary(x => x.Item1, y => y.Item2);
        }

        public void SetNodeHandler(Type type, INodeHandler nodeHandler)
        {
            ValidateArguments(new List<Tuple<Type, INodeHandler>> { Tuple.Create(type, nodeHandler) });            
            _nodeHandlers[type] = nodeHandler;
        }

        public INodeHandler GetNodeHandler(Type type)
        {
            return _nodeHandlers[type];
        }

        private void ValidateArguments(IEnumerable<Tuple<Type, INodeHandler>> nodeHandlers)
        {
            var invalidArguments = nodeHandlers.Where(x => !x.Item1.IsSameOrSubclass(baseNodeType));
            if (invalidArguments.Any())
            {
                throw new ArgumentException(string.Format(
                    "Given types: {0} ; do not derive from {1}.",
                    string.Join(", ", invalidArguments.Select(x => x.Item1.Name)),
                    baseNodeType.Name));
            }
        }

        public void Execute(FlowNode node)
        {
            if (!_nodeHandlers.ContainsKey(node.XmlType))
            {
                Logger.LogWarning($"Cannot execute node {node.Id}; nodeHandler for type {node.XmlType.Name} not registered.");
                return;
            }

            _nodeHandlers[node.XmlType].Execute(node);
        }

    }
}
