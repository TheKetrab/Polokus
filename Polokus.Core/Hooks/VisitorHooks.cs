using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Core.Hooks
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

        public VisitorHooks(VisitTime visitTime = VisitTime.BeforeExecute)
        {
            _visitMask = (uint)visitTime;
        }

        private bool FitWithMask(VisitTime visitTime)
        {
            return (_visitMask & (uint)visitTime) != 0;
        }

        private void LogActionSafe(string nodeId, VisitTime visitTime)
        {
            lock(sb)
            {
                LogAction(nodeId, visitTime);
            }
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

        private void LogAction(string nodeId, VisitTime visitTime)
        {            
            // TODO

            //if (!FitWithMask(visitTime))
            //{
            //    return;
            //}

            //if (((_visitMask & (uint)VisitTime.MarkNameForSpecialNodes) != 0)
            //    &&
            //     (node.XmlType == typeof(tScriptTask)
            //     || node.XmlType == typeof(tIntermediateCatchEvent)))
            //{
            //    bool withDetails = (_visitMask & (uint)VisitTime.PutNameInParenthesis) != 0;

            //    LogMarked(node, withDetails);

            //    return;
            //}

            //if (sb.Length != 0)
            //{
            //    sb.Append(separator);
            //}

            //sb.Append(node.Name);
        }


        public override void BeforeExecuteNode(string wfId, string piId, string nodeId, int taskId, string? nodeCaller)
        {
            LogActionSafe(nodeId, VisitTime.BeforeExecute);
        }

        public override void AfterExecuteNodeSuccess(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(nodeId, VisitTime.AfterExecuteSuccess);
        }

        public override void AfterExecuteNodeFailure(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(nodeId, VisitTime.AfterExecuteFailure);
        }

        public override void AfterExecuteNodeSuspension(string wfId, string piId, string nodeId, int taskId)
        {
            LogActionSafe(nodeId, VisitTime.AfterExecuteSuspension);
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
