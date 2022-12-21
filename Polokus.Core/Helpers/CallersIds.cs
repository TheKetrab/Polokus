﻿using Polokus.Core.Interfaces;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polokus.Core.Helpers
{
    public static class CallersIds
    {
        const string WaiterPattern = @"Waiter_\((.*)\)_\((.*)\)_\((.*)\)_\((.*)\)";
        const string StarterPattern = @"Starter_\((.*)\)_\((.*)\)_\((.*)\)";

        public static string GetStarterId(string workflowId, string bpmnProcessId, string flowNodeId)
        {
            return $"Starter_({workflowId})_({bpmnProcessId})_({flowNodeId})";
        }
        
        public static string GetWaiterId(string workflowId, string processInstanceId, string bpmnProcessId, string nodeToCallId)
        {
            return $"Waiter_({workflowId})_({processInstanceId})_({bpmnProcessId})_({nodeToCallId})";
        }

        public static bool IsWaiter(string name)
        {
            return name.StartsWith("Waiter_");
        }

        public static bool IsStarter(string name)
        {
            return name.StartsWith("Starter_");
        }

        private static string GetGroup(string str, string pattern, int gr)
        {
            Match m = Regex.Match(str, pattern);
            return m.Groups[gr].Value;
        }

        public static string GetWorkflowIdFromWaiter(string waiterId)
        {
            return GetGroup(waiterId, WaiterPattern, 1);
        }

        public static string GetProcessInstanceIdFromWaiter(string waiterId)
        {
            return GetGroup(waiterId, WaiterPattern, 2);
        }

        public static string GetBpmnProcessIdFromWaiter(string waiterId)
        {
            return GetGroup(waiterId, WaiterPattern, 3);
        }

        public static string GetNodeIdFromWaiter(string waiterId)
        {
            return GetGroup(waiterId, WaiterPattern, 4);
        }

        public static string GetWorkflowIdFromStarter(string starterId)
        {
            return GetGroup(starterId, StarterPattern, 1);
        }

        public static string GetBpmnProcessIdFromStarter(string starterId)
        {
            return GetGroup(starterId, StarterPattern, 2);
        }

        public static string GetNodeIdFromStarter(string starterId)
        {
            return GetGroup(starterId, StarterPattern, 3);
        }

        public static string GetWorkflowIdFromCaller(string callerId)
        {
            if (IsWaiter(callerId)) 
            {
                return GetWorkflowIdFromWaiter(callerId);
            }
            
            if (IsStarter(callerId))
            {
                return GetWorkflowIdFromStarter(callerId);
            }

            throw new Exception("Not found");
        }
    }
}
