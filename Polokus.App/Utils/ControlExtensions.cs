using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public static class ControlExtensions
    {

        public static string DimString(this Control form)
        {
            return $"{form.Width}x{form.Height}";
        }
    }
}
