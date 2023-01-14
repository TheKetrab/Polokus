﻿using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
using Polokus.Core.NodeHandlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.NodeHandlers
{
    public class ScriptTaskNodeHandler : NodeHandler<tScriptTask>
    {
        public ScriptTaskNodeHandler(ProcessInstance processInstance, FlowNode<tScriptTask> typedNode) 
            : base(processInstance, typedNode)
        {
        }

        public override async Task Action(INodeCaller? caller)
        {
            string script = ScriptProvider.Decode(Node.Name);
            await ScriptProvider.EvalCSharpScriptAsync(script);
        }

    }
}
