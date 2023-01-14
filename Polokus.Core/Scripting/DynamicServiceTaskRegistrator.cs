using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Execution;
using Polokus.Core.Externals;
using Polokus.Core.Interfaces;
using Polokus.Core.Interfaces.NodeHandlers;
using Polokus.Core.NodeHandlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Polokus.Core.Scripting
{
    public class DynamicServiceTaskRegistrator
    {
        private INodeHandlerFactory _factory;
        private Workflow _workflow;

        public DynamicServiceTaskRegistrator(Workflow Workflow)
        {
            _workflow = Workflow;
            _factory = Workflow.NodeHandlerFactory;
        }

        private static bool IsServiceNH(Type type)
        {
            for (Type tempType = type; tempType.BaseType != null; tempType = tempType.BaseType)
            {
                if (tempType == typeof(ServiceTaskNodeHandler))
                {
                    return true;
                }
            }

            return false;
        }

        public void RegisterServiceTask(string assemblyPath, string className, string name)
        {
            var asm = Assembly.LoadFile(assemblyPath);
            var nhType = asm.GetType(className);

            _factory.RegisterNodeHandlerForServiceTask(nhType, name);

        }

        public void RegisterServiceTask(string name)
        {
            string externalsPath = _workflow.SettingsProvider.ServiceTasksExternals;
            string jsonContent = File.ReadAllText(externalsPath);

            var externals = ExternalsManager.LoadExternals(jsonContent);

            var wf = externals.Workflows[0];
            var st = wf.ServiceTasks[0];

            RegisterServiceTask(st.Assembly, st.ClassName, name);
        }

    }
}
