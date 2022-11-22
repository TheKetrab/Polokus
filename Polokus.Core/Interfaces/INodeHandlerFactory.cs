using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.NodeHandlers.Abstract;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface INodeHandlerFactory
    {
        INodeHandler CreateNodeHandler(IProcessInstance processInstance, IFlowNode node);

        void RegisterNodeHandlerType<TXml, TNodeHandler>()
            where TXml : tFlowNode where TNodeHandler : class, INodeHandler;

        void RegisterNodeHandlerForServiceTask<TNodeHandler>(string serviceTask)
            where TNodeHandler : class, INodeHandler;

        void RegisterNodeHandlerForServiceTask(Type nhType, string serviceTask);

        bool IsNodeHandlerForServiceTaskRegistered(string serviceTask);

    }
}
