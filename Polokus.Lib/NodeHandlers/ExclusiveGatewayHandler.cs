﻿using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class ExclusiveGatewayHandler : NodeHandler<tExclusiveGateway>
    {
        public ExclusiveGatewayHandler(ProcessInstance process) : base(process)
        {
            
        }

        public override Task<int> ProcessNode(FlowNode node, string? predecessorId)
        {
            return base.ProcessNode(node, predecessorId);
            //return Task.FromResult(true);
        }


    }
}
