using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.NodeHandlers
{
    public class EmptyNodeHandler : INodeHandler
    {
        public int CC { get; set; }

        public event EventHandler<NodeHandlerFinishedEventArgs>? Finished;
        public event EventHandler<NodeHandlerFailedEventArgs>? Failed;

        public Task Execute(FlowNode node)
        {
            return Task.CompletedTask;
        }
    }
}
