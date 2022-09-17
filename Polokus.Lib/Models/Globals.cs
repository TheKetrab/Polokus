using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public class Globals
    {
        public Dictionary<string, object> globals;
        public Globals(Dictionary<string, object> globals)
        {
            this.globals = globals;
        }
    }
}
