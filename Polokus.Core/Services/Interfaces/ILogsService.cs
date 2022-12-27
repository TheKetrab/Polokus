using Polokus.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.Interfaces
{
    public interface ILogsService
    {
        public List<Tuple<Logger.MsgType, string>> GetMessages(string wfId, string piId);
        public void Log(string globalPiId, Logger.MsgType type, string info);
    }
}
