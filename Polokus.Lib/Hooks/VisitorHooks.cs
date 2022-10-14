using Polokus.Lib.Models;
using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Hooks
{
    public enum VisitTime
    {
        OnExecute = 1,
        OnFinished = 2,
        OnSuspension = 4,
        OnFailure = 8,
        MarkNameForSpecialNodes = 16,
        PutNameInParenthesis = 32,
        OnCanExecute = 64,
    }

    public class VisitorHooks : EmptyHooksProvider
    {
        const string separator = ";";
        private uint _visitMask;
        StringBuilder sb = new StringBuilder();

        public VisitorHooks(VisitTime visitTime = VisitTime.OnExecute)
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


        public override void OnExecute(IFlowNode node, int taskId, string? predecessor)
        {
            LogActionSafe(node, VisitTime.OnExecute);
        }

        public override void OnFinished(IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.OnFinished);
        }

        public override void OnFailure(IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.OnFailure);
        }

        public override void OnSuspension(IFlowNode node, int taskId)
        {
            LogActionSafe(node, VisitTime.OnSuspension);
        }

        public override void OnCanExecute(IFlowNode node, int taskId, string? caller)
        {
            LogActionSafe(node, VisitTime.OnCanExecute);
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
