using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Polokus.Core.Execution;
using Polokus.Core.Interfaces;
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
        private ContextInstance _contextInstance;

        public DynamicServiceTaskRegistrator(ContextInstance contextInstance)
        {
            _contextInstance = contextInstance;
            _factory = contextInstance.NodeHandlerFactory;
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
            // TODO: fix this implementations

            string externalsPath = _contextInstance.SettingsProvider.ServiceTasksExternals;
            string jsonContent = File.ReadAllText(externalsPath);
            var doc = JsonDocument.Parse(jsonContent);

            string assembly = string.Empty;
            string className = string.Empty;

            var ci = doc.RootElement.GetProperty("contextInstances");
            int n = ci.GetArrayLength();
            for (int i = 0; i < n; i++)
            {
                var nn = ci[i].GetProperty("name").GetString();
                if (nn == _contextInstance.Id)         
                {
                    var st = ci[i].GetProperty("serviceTasks");
                    int n2 = st.GetArrayLength();
                    for (int j = 0; j < n2; j++)
                    {
                        var stn = st[j].GetProperty("serviceTaskName").GetString();
                        if (stn == name)
                        {
                            assembly = st[j].GetProperty("assembly").GetString();
                            className = st[j].GetProperty("className").GetString();
                        }
                    }
                }

            }

            ;
            RegisterServiceTask(assembly, className, name);
        }

    }
}
