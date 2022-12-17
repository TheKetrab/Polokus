using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polokus.Core.Helpers
{
    public static class Utils
    {
        public static string GetStarterName(string WorkflowId, string bpmnProcessId, string flowNodeId)
        {
            return $"Starter_({WorkflowId})_({bpmnProcessId})_({flowNodeId})";
        }
        
        public static string GetWaiterName(string WorkflowId, string processInstanceId, string bpmnProcessId, string nodeToCallId)
        {
            return $"Waiter_({WorkflowId})_({processInstanceId})_({bpmnProcessId})_({nodeToCallId})";
        }

        public static string GetProcessInstanceIdFromWaiter(string waiterName)
        {
            string pattern = @"Waiter_\((.*)\)_\((.*)\)_\((.*)\)_\((.*)\)";
            Match m = Regex.Match(waiterName, pattern);
            return m.Groups[2].Value;
        }

        public static string GetNodeIdFromWaiter(string waiterName)
        {
            string pattern = @"Waiter_\((.*)\)_\((.*)\)_\((.*)\)_\((.*)\)";
            Match m = Regex.Match(waiterName, pattern);
            return m.Groups[4].Value;
        }

        public static string GetBpmnProcessIdFromWaiter(string waiterName)
        {
            string pattern = @"Waiter_\((.*)\)_\((.*)\)_\((.*)\)_\((.*)\)";
            Match m = Regex.Match(waiterName, pattern);
            return m.Groups[3].Value;
        }
    }
}
