using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.BpmnModels;
using Polokus.Core.Execution.NodeHandlers.Abstract;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Execution.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        string? _defaultSequence;

        public ExclusiveGatewayHandler(ProcessInstance processInstance, FlowNode<tExclusiveGateway> typedNode)
            : base(processInstance, typedNode)
        {
            if (!string.IsNullOrEmpty(typedNode.XmlElement.@default))
            {
                _defaultSequence = typedNode.XmlElement.@default;
            }
        }

        async Task<bool> IsValidSequence(ISequence sequence)
        {
            string condition = ScriptProvider.Decode(sequence.Name);
            if (string.IsNullOrEmpty(condition))
            {
                if (_defaultSequence == null)
                {
                    return true; // accept by default
                }

                return false; // if default sequence exists, choose it on the end
            }
            return await ScriptProvider.EvalScriptAsync<bool>(condition);
        }

        protected override async Task<ProcessResultInfo> Process(INodeCaller? caller)
        {
            foreach (var sequence in Node.Outgoing)
            {
                if (await IsValidSequence(sequence))
                {
                    return new SuccessProcessResultInfo(sequence);
                }
            }

            if (_defaultSequence != null)
            {
                var seq = Node.Outgoing.FirstOrDefault(x => x.Id == _defaultSequence);
                if (seq != null)
                {
                    return new SuccessProcessResultInfo(seq);
                }
            }

            return new ProcessResultInfo(ProcessResultState.Failure);
        }

    }
}
