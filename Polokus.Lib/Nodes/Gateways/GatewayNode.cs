﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polokus.Lib.Interfaces;

namespace Polokus.Lib.Nodes.Gateways
{
    public class GatewayNode : IGatewayNode
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Incoming { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<INode> Outgoing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
