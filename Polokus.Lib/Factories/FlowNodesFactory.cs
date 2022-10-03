using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Factories
{
    public static class FlowNodesFactory
    {
        public static IFlowNode CreateFlowNode(BpmnProcess process, tFlowNode xmlElement)
        {

            switch (xmlElement)
            {
                case tEndEvent t: return new FlowNode<tEndEvent>(process, t);
                case tExclusiveGateway t: return new FlowNode<tExclusiveGateway>(process, t);
                case tInclusiveGateway t: return new FlowNode<tInclusiveGateway>(process, t);
                case tParallelGateway t: return new FlowNode<tParallelGateway>(process, t);
                case tStartEvent t: return new FlowNode<tStartEvent>(process, t);
                case tTask t: return new FlowNode<tTask>(process, t);
            }

            throw new Exception();

        }
    }
}
