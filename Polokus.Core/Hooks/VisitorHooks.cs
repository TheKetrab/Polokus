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

        private void LogActionSafe(IFlowNode node, VisitTime visitTime)
        {
            lock(sb)
            {
                LogAction(node, visitTime);
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

        private void LogAction(IFlowNode node, VisitTime visitTime)
        {            
            if (!FitWithMask(visitTime))
            {
                return;
            }

            if (((_visitMask & (uint)VisitTime.MarkNameForSpecialNodes) != 0)
                &&
                 (node.XmlType == typeof(tScriptTask)
                 || node.XmlType == typeof(tIntermediateCatchEvent)))
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


        public override void BeforeExecuteNode(string processInstanceId, IFlowNode node, int taskId, INodeCaller? caller)
        {
            LogActionSafe(node, VisitTime.BeforeExecute);
        }

        public override void AfterExecuteNodeSuccess(string processInstanceId, IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.AfterExecuteSuccess);
        }

        public override void AfterExecuteNodeFailure(string processInstanceId, IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.AfterExecuteFailure);
        }

        public override void AfterExecuteNodeSuspension(string processInstanceId, IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.AfterExecuteSuspension);
        }

        public override void BeforeStartNewSequence(string processInstanceId, IFlowNode firstNode, INodeCaller? caller)
        {
            if (FitWithMask(VisitTime.StartNewSequence))
            {
                LogStringSafe(caller?.Id ?? "null");
            }
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
