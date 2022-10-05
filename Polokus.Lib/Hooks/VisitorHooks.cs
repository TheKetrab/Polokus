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
        IgnoreScriptTaskNames = 16,
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

        private void LogAction(IFlowNode node, VisitTime visitTime)
        {
            if ((_visitMask & (uint)VisitTime.IgnoreScriptTaskNames) != 0
                && node.XmlType == typeof(tScriptTask))
            {
                return;
            }

            if (!FitWithMask(visitTime))
            {
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
            LogAction(node, VisitTime.OnExecute);
        }

        public override void OnFinished(IFlowNode node, int taskId)
        {
            LogAction(node, VisitTime.OnFinished);
        }

        public override void OnFailure(IFlowNode node, int taskId)
        {
            LogAction(node, VisitTime.OnFailure);
        }

        public override void OnSuspension(IFlowNode node, int taskId)
        {
            LogAction(node, VisitTime.OnSuspension);
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
