using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Extensibility;
using Polokus.Core.Interfaces.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Core.Extensibility.Hooks
{
    public enum VisitTime
    {
        BeforeExecute = 1,
        AfterExecuteSuccess = 1 << 1,
        AfterExecuteSuspension = 1 << 2,
        AfterExecuteFailure =  1 << 3,
        
        MarkNameForSpecialNodes = 1 << 4,
        PutNameInParenthesis = 1 << 5,

        StartNewSequence = 1 << 6,
    }

    public class VisitorHooks : EmptyHooksProvider
    {
        const string separator = ";";
        private uint _visitMask;
        StringBuilder sb = new StringBuilder();
        IPolokusMaster _master;

        public VisitorHooks(IPolokusMaster master, VisitTime visitTime = VisitTime.BeforeExecute)
        {
            _master = master;
            _visitMask = (uint)visitTime;
        }

        private bool FitWithMask(VisitTime visitTime)
        {
            return (_visitMask & (uint)visitTime) != 0;
        }

        private void LogActionSafe(IFlowNode node, VisitTime visitTime)
        {
            lock(sb)
            {
                LogAction(node, visitTime);
            }
        }

        private IFlowNode GetFlowNode(string wfId, string piId, string nodeId)
        {
            return _master.GetFlowNode(wfId, piId, nodeId)
                ?? throw new Exception("Can find node by id");
        }

        private void LogMarked(IFlowNode node, bool withDetails)
        {
            if (sb.Length != 0) sb.Append(separator);

            sb.Append(node.XmlType.Name);

            if (withDetails)
            {
                sb.Append($"({node.Name})");
            }
        }

        private void LogStringSafe(string str)
        {
            lock (sb)
            {
                sb.Append(sb.Length == 0 ? str : $"{separator}{str}");
            }
        }

        private void LogAction(IFlowNode node, VisitTime visitTime)
        {
            if (!FitWithMask(visitTime))
            {
                return;
            }

            if (((_visitMask & (uint)VisitTime.MarkNameForSpecialNodes) != 0)
                &&
                 (node.XmlType == typeof(tScriptTask)
                 || node.XmlType == typeof(tIntermediateCatchEvent)
                 || node.XmlType == typeof(tBoundaryEvent)))
            {
                bool withDetails = (_visitMask & (uint)VisitTime.PutNameInParenthesis) != 0;

                LogMarked(node, withDetails);

                return;
            }

            if (sb.Length != 0)
            {
                sb.Append(separator);
            }

            sb.Append(node.Name);
        }


        public override void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? nodeCaller)
        {
            LogActionSafe(GetFlowNode(wfId,piId,nodeId), VisitTime.BeforeExecute);
        }

        public override void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(GetFlowNode(wfId, piId, nodeId), VisitTime.AfterExecuteSuccess);
        }

        public override void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(GetFlowNode(wfId, piId, nodeId), VisitTime.AfterExecuteFailure);
        }

        public override void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(GetFlowNode(wfId, piId, nodeId), VisitTime.AfterExecuteSuspension);
        }

        public override void BeforeStartNewSequence(string wfId, string piId, string nodeId, string? nodeCallerId)
        {
            if (FitWithMask(VisitTime.StartNewSequence))
            {
                LogStringSafe(nodeCallerId ?? "null");
            }
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
