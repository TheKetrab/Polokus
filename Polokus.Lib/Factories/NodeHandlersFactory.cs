using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Factories
{
    //public static class NodeHandlersFactory
    //{
    //    public static INodeHandler CreateNodeHandler(IFlowNode node)
    //    {
    //        //if (node.XmlType == typeof(tEndEvent))
    //        //    return new EndEventHandler(node);
    //        //if (node.XmlType == typeof(tExclusiveGateway))
    //        //    return new ExclusiveGatewayHandler(node);
    //        //if (node.XmlType == typeof(tInclusiveGateway))
    //        //    return null; //TODO  new Inclusi(node);
    //        //if (node.XmlType == typeof(tParallelGateway))
    //        //    return new ParallelGatewayNodeHandler(node);
    //        if (node.XmlType == typeof(tStartEvent))
    //            return new StartEventHandler((IFlowNode<tStartEvent>)node);
    //        //if (node.XmlType == typeof(tTask))
    //        //    return new TaskNodeHandler(node);
 

    //        throw new Exception();

    //    }
    //}
}
