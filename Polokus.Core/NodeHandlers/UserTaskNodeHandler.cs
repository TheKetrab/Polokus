using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.Interfaces;

namespace Polokus.Core.NodeHandlers
{
    public class UserTaskNodeHandler : NodeHandler<tUserTask>
    {
        private string? _userDecision = null;

        public UserTaskNodeHandler(ProcessInstance processInstance, FlowNode<tUserTask> typedNode) : base(processInstance, typedNode)
        {
        }

        protected async override Task Action(INodeCaller? caller)
        {
            if (_userDecision != null)
            {
                var outgoing = this.Node.Outgoing.SingleOrDefault();
                if (outgoing != null)
                {
                    // it can be int, if something else it will be treated as a string

                    string userDecision = string.Empty;
                    if (int.TryParse(_userDecision, out int i))
                    {
                        userDecision = i.ToString();
                    }
                    else
                    {
                        userDecision = $@"""{_userDecision}"""; // string requires quotes
                    }

                    // perform: $x = decision
                    string script = $"{outgoing.Name} = {userDecision};";
                    await ScriptProvider.EvalCSharpScriptAsync(script);
                }
            }
        }

        public void SetUserDecision(string userDecision)
        {
            _userDecision = userDecision;
        }
    }
}
