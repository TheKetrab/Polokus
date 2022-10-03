using Polokus.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Hooks
{
    public class VisitorHooks : EmptyHooksProvider
    {
        StringBuilder sb = new StringBuilder();

        public override void OnExecute(IFlowNode node, int taskId, string? predecessor)
        {
            if (sb.Length != 0)
            {
                sb.Append(";");
            }

            sb.Append(node.Name);
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
