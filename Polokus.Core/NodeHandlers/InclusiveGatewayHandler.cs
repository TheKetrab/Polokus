using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{

    public class InclusiveGatewayHandler : JoiningNodeHandler<tInclusiveGateway>
    {
        public InclusiveGatewayHandler(ProcessInstance processInstance, FlowNode<tInclusiveGateway> typedNode)
            : base(processInstance,typedNode)
        {
        }

        async Task<bool> IsValidSequence(ISequence sequence)
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

        protected override async Task<ProcessResultInfo> Process(INodeCaller? caller)
        {
            var valid = new List<ISequence>();
            foreach (var sequence in Node.Outgoing)
            {
                if (await IsValidSequence(sequence))
                {
                    valid.Add(sequence);
                }
            }

            if (valid.Count > 0)
            {
                return new SuccessProcessResultInfo(valid);
            }

            if (!string.IsNullOrEmpty(this.TypedNode.XmlElement.@default))
            {
                var defaultSequenceId = this.TypedNode.XmlElement.@default;
                var defaultSequence = Node.Outgoing.FirstOrDefault(x => x.Id == defaultSequenceId);
                if (defaultSequence != null)
                {
                    return new SuccessProcessResultInfo(defaultSequence);
                }
            }

            return new ProcessResultInfo(ProcessResultState.Failure);

        }

    }
        
}
