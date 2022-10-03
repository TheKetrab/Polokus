
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Polokus.Lib.Models
{
    public class FlowNode<T> : IFlowNode where T : tFlowNode
    {
        public T XmlElement { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public ICollection<Sequence> Incoming { get; set; } = new List<Sequence>();
        public ICollection<Sequence> Outgoing { get; set; } = new List<Sequence>();
        public Type XmlType { get => typeof(T); }
        public BpmnProcess Process { get;set; }
 
        public FlowNode(BpmnProcess process, T xmlElement) 
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            Process = process;
        }


    }

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
