using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Helpers
{
    internal static class BpmnXmlHelpers
    {
        internal static bool IsStartNode(this IFlowNode node)
        {
            return node.XmlType == typeof(tStartEvent);
        }
    }
}
