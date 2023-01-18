using Polokus.Core.Interfaces.BpmnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.NodeHandlers
{
    public abstract class ServiceTaskNodeHandlerImpl
    {
        protected INodeHandler Parent { get; }

        public ServiceTaskNodeHandlerImpl(INodeHandler parent) 
        {
            Parent = parent;
        }

        public abstract Task Run();

    }
}
