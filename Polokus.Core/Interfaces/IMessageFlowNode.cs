using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IMessageFlowNode : IFlowNode, IMessageCallerNode, IMessageReceiverNode
    {
    }

    public interface IMessageCallerNode
    {
        public ICollection<IMessageFlow> OutgoingMessages { get; }
    }

    public interface IMessageReceiverNode
    {
        public ICollection<IMessageFlow> IncommingMessages { get; }
    }
}
