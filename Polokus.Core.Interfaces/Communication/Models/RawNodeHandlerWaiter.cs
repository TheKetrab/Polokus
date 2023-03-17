namespace Polokus.Core.Interfaces.Communication.Models
{
    /// <summary>
    /// Basic information about Waiter.
    /// </summary>
    public class RawNodeHandlerWaiter
    {
        /// <summary>
        /// Id of waiter.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Id of FlowNode that waiter will call.
        /// </summary>
        public string NodeToCall { get; set; } = string.Empty;

        /// <summary>
        /// Type of waiter (eg. Time, Message, Signal).
        /// </summary>
        public string WaiterType { get; set; } = string.Empty;
    }
}
