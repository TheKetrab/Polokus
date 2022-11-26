using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IBpmnProcess
    {
        string Id { get; }
        IBpmnContext BpmnContext { get; }
        IFlowNode? GetNodeById(string id);
        ISequence? GetSequenceById(string id);
        void SetNodes(IEnumerable<IFlowNode> nodes);
        void SetSequences(IEnumerable<ISequence> sequences);
        IEnumerable<IFlowNode> GetStartNodes();
        bool IsReachable(IFlowNode src, IFlowNode dest);
        IFlowNode GetManualStartNode();
        IEnumerable<string> GetNodesIds();
        IEnumerable<string> GetServiceTasksNames();
        bool ContainsNode(string id);

        IMessageCallerNode GetMessageCallerNode(string id);
        IMessageReceiverNode GetMessageReceiverNode(string id);
    }
}
