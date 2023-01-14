using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.BpmnModels;
using Polokus.Core.Interfaces.Xsd;
using Polokus.Core.Models;
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

        internal static bool IsManualStartNode(this IFlowNode node)
        {
            if (node.XmlType != typeof(tStartEvent))
            {
                return false;
            }

            var typedStartNode = (FlowNode<tStartEvent>)node;
            return !typedStartNode.XmlElement.Items?.Any() ?? true; // none subelements
        }
    }
}
