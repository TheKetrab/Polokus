﻿using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.Execution;

namespace Polokus.Tests.Helpers
{
    internal static class Extensions
    {
        public static string SubstringBetween(this string str, string sep1, string sep2)
        {
            int idx1 = str.IndexOf(sep1);
            int idx2 = str.LastIndexOf(sep2);

            if (idx1 == -1 && idx2 == -1)
            {
                throw new Exception();
            }

            idx1 += sep1.Length;
            if (idx1 > idx2)
            {
                throw new Exception();
            }

            return str.Substring(idx1, idx2 - idx1);
        }

        public static IWorkflow GetFirstWorkflow(this IPolokusMaster polokus)
        {
            return polokus.GetWorkflows().First();
        }
    }
}
