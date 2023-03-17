namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Service that allows remote logging.
    /// </summary>
    public interface ILogsService
    {
        /// <summary>
        /// Returns all logs for given process instance.
        /// </summary>
        /// <param name="wfId">Id of workflow.</param>
        /// <param name="piId">Id of process instance.</param>
        public List<Tuple<MsgType, string>> GetMessages(string wfId, string piId);

        /// <summary>
        /// Sends log message to master.
        /// </summary>
        /// <param name="globalPiId">Global id of concrete process isntance.</param>
        /// <param name="type">Importance of log.</param>
        /// <param name="info">Content of log.</param>
        public void Log(string globalPiId, MsgType type, string info);
    }
}
