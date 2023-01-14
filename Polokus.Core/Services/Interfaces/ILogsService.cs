using Polokus.Core.Helpers;
using Polokus.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Interfaces
{
    public interface ILogsService
    {
        public List<Tuple<MsgType, string>> GetMessages(string wfId, string piId);
        public void Log(string globalPiId, MsgType type, string info);
    }
}
