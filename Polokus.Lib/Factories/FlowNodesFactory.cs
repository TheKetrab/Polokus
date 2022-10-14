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
            return CreateConcrete<tEndEvent>(process, xmlElement)
                ?? CreateConcrete<tExclusiveGateway>(process, xmlElement)
                ?? CreateConcrete<tInclusiveGateway>(process, xmlElement)
                ?? CreateConcrete<tParallelGateway>(process, xmlElement)
                ?? CreateConcrete<tStartEvent>(process, xmlElement)
                ?? CreateConcrete<tTask>(process, xmlElement)
                ?? CreateConcrete<tServiceTask>(process, xmlElement)
                ?? CreateConcrete<tScriptTask>(process, xmlElement)
                ?? CreateConcrete<tUserTask>(process, xmlElement)
                ?? CreateConcrete<tManualTask>(process, xmlElement)
                ?? CreateConcrete<tIntermediateCatchEvent>(process, xmlElement)
                ?? throw new Exception("!!!");


        }

        private static IFlowNode CreateConcrete<T>(BpmnProcess process, tFlowNode xmlElement)
            where T : tFlowNode
        {
            if (xmlElement.GetType() == typeof(T))
                return new FlowNode<T>(process, (T)xmlElement);
            return null;
        }
    }
}
