using Polokus.Core.BpmnModels;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Factories
{
    public class FlowNodeFactory
    {
        public IFlowNode CreateFlowNode(BpmnProcess process, tFlowNode xmlElement)
        {
            return CreateTypedFlowNode<tEndEvent>(process, xmlElement)
                ?? CreateTypedFlowNode<tExclusiveGateway>(process, xmlElement)
                ?? CreateTypedFlowNode<tInclusiveGateway>(process, xmlElement)
                ?? CreateTypedFlowNode<tParallelGateway>(process, xmlElement)
                ?? CreateTypedFlowNode<tStartEvent>(process, xmlElement)
                ?? CreateTypedFlowNode<tTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tServiceTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tScriptTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tUserTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tManualTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tIntermediateCatchEvent>(process, xmlElement)
                ?? CreateTypedFlowNode<tIntermediateThrowEvent>(process, xmlElement)
                ?? CreateTypedFlowNode<tReceiveTask>(process, xmlElement)
                ?? CreateTypedFlowNode<tSubProcess>(process, xmlElement)
                ?? CreateTypedFlowNode<tCallActivity>(process, xmlElement)
                ?? CreateBoundaryEvent(process, xmlElement)
                ?? throw new Exception($"Unable to create FlowNode for type: {xmlElement.GetType().Name}.");

        }

        private IFlowNode? CreateTypedFlowNode<T>(BpmnProcess process, tFlowNode xmlElement)
            where T : tFlowNode
        {
            if (xmlElement.GetType() == typeof(T))
                return new FlowNode<T>(process, (T)xmlElement);
            return null;
        }

        private IFlowNode? CreateBoundaryEvent(BpmnProcess process, tFlowNode xmlElement)
        {
            if (xmlElement.GetType() == typeof(tBoundaryEvent))
                return new BoundaryEvent(process, (tBoundaryEvent)xmlElement);
            return null;
        }

    }
}
