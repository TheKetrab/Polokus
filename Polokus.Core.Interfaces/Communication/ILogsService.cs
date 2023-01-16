namespace Polokus.Core.Interfaces.Communication
{
    public interface ILogsService
    {
        public List<Tuple<MsgType, string>> GetMessages(string wfId, string piId);
        public void Log(string globalPiId, MsgType type, string info);
    }
}
