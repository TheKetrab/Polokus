using Polokus.Core.Helpers;
using Polokus.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Services.OnPremise
{
    public class OnPremiseLogsService : ILogsService
    {
        private PolokusMaster _polokus;

        public OnPremiseLogsService(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public List<Tuple<Logger.MsgType, string>> GetMessages(string wfId, string piId)
        {
            var logger = _polokus.GetOrCreateLogger(wfId, piId);
            return logger.GetMessages().ToList();
        }

        public void Log(string globalPiId, Logger.MsgType type, string info)
        {
            var logger = _polokus.GetOrCreateLogger(globalPiId);
            switch (type)
            {
                case Logger.MsgType.Simple:
                    logger.Log(info);
                    break;
                case Logger.MsgType.Warning:
                    logger.LogWarning(info);
                    break;
                case Logger.MsgType.Error:
                    logger.LogError(info);
                    break;
            }

        }
    }
}
