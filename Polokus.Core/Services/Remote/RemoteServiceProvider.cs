using Polokus.Core.Remote;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Remote
{
    public class RemoteServiceProvider : IServicesProvider
    {
        public ILogsService LogsService => new RemoteLogsService();

        public IPolokusService PolokusService => new RemotePolokusService();

        public IProcessInstancesService ProcessInstancesService => new RemoteProcessInstancesService();

        public IWorkflowsService WorkflowsService => new RemoteWorkflowsService();
    }
}
