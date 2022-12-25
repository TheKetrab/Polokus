using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class Signal
    {
        public string Name { get; set; }
        public string? Params { get; set; }

        public Signal(string name)
        {
            Name = name;
        }

        public Signal(string name, string? @params) : this(name)
        {
            Params = @params;
        }
    }
}
