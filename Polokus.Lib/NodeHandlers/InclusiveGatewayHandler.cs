using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using Polokus.Lib.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    
    public class InclusiveGatewayHandler : JoiningNodeHandler<tInclusiveGateway>
    {
        public InclusiveGatewayHandler(FlowNode<tInclusiveGateway> typedNode) : base(typedNode)
        {
        }

        async Task<bool> IsValidSequence(Sequence sequence)
        {
            if (string.Equals(sequence.Id,this.TypedNode.XmlElement.@default))
            {
                return false;
            }

            string condition = ScriptProvider.Decode(sequence.Name);
            if (string.IsNullOrEmpty(condition))
            {
                return true; // accept by default
            }
            return await ScriptProvider.EvalCSharpScriptAsync<bool>(condition);
        }

        protected override async Task<ProcessResultInfo> Process(IFlowNode? caller)
        {
            var valid = new List<Sequence>();
            foreach (var sequence in Node.Outgoing)
            {
                if (await IsValidSequence(sequence))
                {
                    valid.Add(sequence);
                }
            }

            if (valid.Count > 0)
            {
                return new ProcessResultInfo(ProcessResultState.Success, valid);
            }

            if (!string.IsNullOrEmpty(this.TypedNode.XmlElement.@default))
            {
                var defaultSequenceId = this.TypedNode.XmlElement.@default;
                var defaultSequence = Node.Outgoing.FirstOrDefault(x => x.Id == defaultSequenceId);
                if (defaultSequence != null)
                {
                    return new ProcessResultInfo(ProcessResultState.Success, defaultSequence);
                }
            }

            return new ProcessResultInfo(ProcessResultState.Failure);

        }

    }
        
}
