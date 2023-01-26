using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.BpmnModels
{
    public class Sequence : ISequence
    {
        private IFlowNode? _source;
        private IFlowNode? _target;

        public string Id { get; set; }
        public string Name { get; set; }
        public tSequenceFlow XmlElement { get; set; }
        public IBpmnProcess BpmnProcess { get; }

        public IFlowNode? Source => _source ??= BpmnProcess.GetNodeById(XmlElement.sourceRef);
        public IFlowNode? Target
        {
            get { return _target ??= BpmnProcess.GetNodeById(XmlElement.targetRef); }
            private set { _target = value; }
        }

        private Sequence() { }

        public Sequence(BpmnProcess bpmnProcess, tSequenceFlow xmlElement)
        {
            XmlElement = xmlElement;
            Name = xmlElement.name ?? "";
            Id = xmlElement.id;
            BpmnProcess = bpmnProcess;
        }

        public static Sequence CreateFakeSequence(IFlowNode target)
        {
            var sequence = new Sequence();
            sequence.Target = target;
            return sequence;
        }

    }
}
