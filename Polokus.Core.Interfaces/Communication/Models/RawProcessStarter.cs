namespace Polokus.Core.Interfaces.Communication.Models
{
    /// <summary>
    /// Basic information about Process Starter.
    /// </summary>
    public class RawProcessStarter
    {
        /// <summary>
        /// Id of Process Starter.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Id of FlowNode to start process instance from.
        /// </summary>
        public string StartNode { get; set; } = string.Empty;

        /// <summary>
        /// Type of starter (eg. Time, Message, Signal).
        /// </summary>
        public string StarterType { get; set; } = string.Empty;
    }
}
