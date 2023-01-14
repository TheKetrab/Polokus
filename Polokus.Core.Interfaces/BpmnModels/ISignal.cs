using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.BpmnModels
{
    public interface ISignal
    {
        string Name { get; set; }
        string? Params { get; set; }
    }
}
