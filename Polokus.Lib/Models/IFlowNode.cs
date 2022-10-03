﻿using Polokus.Lib.Models.BpmnObjects.Xsd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public interface IFlowNode
    {
        string Name { get; set; }
        string Id { get; set; }

        ICollection<Sequence> Incoming { get; set; }
        ICollection<Sequence> Outgoing { get; set; }

        Type XmlType { get; }
    }

    public interface IFlowNode<T> : IFlowNode
        where T : tFlowNode
    {
        T XmlElement { get; set; }
    }

}
