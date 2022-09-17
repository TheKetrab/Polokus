﻿using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{

    public interface INodeHandler
    {
        event EventHandler<NodeHandlerFinishedEventArgs>? Finished;
        event EventHandler<NodeHandlerFailedEventArgs>? Failed;

        int CC { get; set; }

        Task Execute(FlowNode node);
    }
}
