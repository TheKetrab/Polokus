using Polokus.Core.Interfaces;
using Polokus.Core.Models.BpmnObjects.Xsd;
using Polokus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.BpmnModels
{
    /// <summary>
    /// Handles communication between processes and sending messages.
    /// </summary>
    public class MessageFlow : IMessageFlow
    {
        private IFlowNode? _source;
        private IFlowNode? _target;

        public string Id { get; set; }
        public string Name { get; set; }
        public tMessageFlow XmlElement { get; set; }
        public IBpmnProcess SourceProcess { get; }
        public IBpmnProcess TargetProcess { get; }

        public IFlowNode? Source => _source ??= SourceProcess.GetNodeById(XmlElement.sourceRef.Name);
        public IFlowNode? Target => _target ??= TargetProcess.GetNodeById(XmlElement.targetRef.Name);


        public MessageFlow(IBpmnProcess sourceProcess, IBpmnProcess targetProcess, tMessageFlow xmlElement)
        {
            XmlElement = xmlElement;
            Name = xmlElement.name;
            Id = xmlElement.id;
            SourceProcess = sourceProcess;
            TargetProcess = targetProcess;
        }

    }
}
