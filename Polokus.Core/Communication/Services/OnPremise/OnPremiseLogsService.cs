using Polokus.Core.Interfaces.Communication;

namespace Polokus.Core.Communication.Services.OnPremise
{
    public class OnPremiseLogsService : ILogsService
    {
        private PolokusMaster _polokus;

        public OnPremiseLogsService(PolokusMaster polokus)
        {
            _polokus = polokus;
        }

        public List<Tuple<MsgType, string>> GetMessages(string wfId, string piId)
        {
            var logger = _polokus.GetOrCreateLogger(wfId, piId);
            return logger.GetMessages().ToList();
        }

        public void Log(string globalPiId, MsgType type, string info)
        {
            var logger = _polokus.GetOrCreateLogger(globalPiId);
            switch (type)
            {
                case MsgType.Simple:
                    logger.Log(info);
                    break;
                case MsgType.Warning:
                    logger.LogWarning(info);
                    break;
                case MsgType.Error:
                    logger.LogError(info);
                    break;
            }

        }
    }
}
