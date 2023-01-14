using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.Interfaces;
using Polokus.Core.Execution;
using Microsoft.Extensions.Logging;
using Polokus.Core.Interfaces.Xsd;

namespace Polokus.Core.NodeHandlers
{
    public class UserTaskNodeHandler : NodeHandler<tUserTask>
    {
        private string? _userDecision = null;

        public UserTaskNodeHandler(ProcessInstance processInstance, FlowNode<tUserTask> typedNode)
            : base(processInstance, typedNode)
        {
        }

        public async override Task Action(INodeCaller? caller)
        {
            if (Master.ClientConnected)
            {
                string token = AwaitingTokensHelper.CreateAwaitingToken(
                    Workflow.Id, ProcessInstance.Id, Node.Id);

                ProcessInstance.AwaitingTokens.Add(token);
                Master.HooksManager.OnAwaitingTokenCreated(Workflow.Id, ProcessInstance.Id, Node.Id, token);
                while (Master.ClientConnected && ProcessInstance.AwaitingTokens.Contains(token))
                {
                    await Task.Delay(100);
                }
            }

            if (_userDecision != null)
            {
                var outgoing = this.Node.Outgoing.SingleOrDefault();
                if (outgoing != null)
                {
                    // it can be int or bool, if something else it will be treated as a string

                    string userDecision = string.Empty;
                    if (int.TryParse(_userDecision, out int i))
                    {
                        userDecision = i.ToString();
                    }
                    else if (bool.TryParse(_userDecision, out bool b))
                    {
                        userDecision = b ? "true" : "false";
                    }
                    else
                    {
                        userDecision = $@"""{_userDecision}"""; // string requires quotes
                    }

                    if (ScriptProvider.IsValidOutgoingVariable(outgoing.Name))
                    {
                        // perform: $x = decision
                        string script = $"{outgoing.Name} = {userDecision};";
                        await ScriptProvider.EvalCSharpScriptAsync(script);
                    }
                    else
                    {
                        this.ProcessInstance.Log(
                            $"Node {this.Node.Id} has outgoing edge with invalid name {outgoing.Name}. Unable to evaluate as variable.",
                            MsgType.Warning);
                    }
                }
            }
        }

        public void SetUserDecision(string userDecision)
        {
            _userDecision = userDecision;
        }
    }
}
