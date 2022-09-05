using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.Interfaces
{
    public interface IBpmnObject
    {
        string Id { get; set; }
        string Name { get; set; }

    }
}
