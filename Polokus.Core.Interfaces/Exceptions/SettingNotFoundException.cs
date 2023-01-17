using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.Exceptions
{
    public class SettingNotFoundException : PolokusException
    {
        public SettingNotFoundException() { }
        public SettingNotFoundException(string setting)
            : base($"Not found setting: {setting}")
        {
        }
    }
}
