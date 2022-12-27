using Polokus.Core.Helpers;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Remote
{
    public class RemoteLogsService : ILogsService
    {
        public List<Tuple<Logger.MsgType,string>> GetMessages(string wfId, string piId)
        {
            throw new NotImplementedException();
        }

        public void Log(string globalPiId, Logger.MsgType type, string info)
        {
            throw new NotImplementedException();
        }
    }
}
