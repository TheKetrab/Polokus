using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.App.Utils
{
    public interface IBpmnClient
    {
        Task<string> GetBpmnSvg(string bpmnXml);
    }
}
