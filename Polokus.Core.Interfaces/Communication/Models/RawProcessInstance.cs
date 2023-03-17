namespace Polokus.Core.Interfaces.Communication.Models
{
    /// <summary>
    /// Basic information about Process Instance.
    /// </summary>
    public class RawProcessInstance
    {
        /// <summary>
        /// Id of Process Instance.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Status of Process Instance (eg. Running, Cancelled, Finished).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Number of active tasks.
        /// </summary>
        public string ActiveTasks { get; set; } = string.Empty;
    }
}
