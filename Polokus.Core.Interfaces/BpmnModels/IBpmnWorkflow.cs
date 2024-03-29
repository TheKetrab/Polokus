﻿using Polokus.Core.Interfaces.Execution;

namespace Polokus.Core.Interfaces.BpmnModels
{
    /// <summary>
    /// BpmnWorkflow is an object that represents parsed XML definitions file.
    /// </summary>
    public interface IBpmnWorkflow
    {
        /// <summary>
        /// XML string where Workflow is parsed from. Contains no line breaks! Minimalized xml.
        /// </summary>
        string? RawString { get; }

        /// <summary>
        /// Instance of Workflow.
        /// </summary>
        IWorkflow? Workflow { get; }

        /// <summary>
        /// List of BPMN processes defined within Workflow.
        /// </summary>
        IEnumerable<IBpmnProcess> BpmnProcesses { get; }
    }
}
