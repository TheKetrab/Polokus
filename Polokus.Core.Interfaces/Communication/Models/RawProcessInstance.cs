namespace Polokus.Core.Interfaces.Communication.Models
{
    public class RawProcessInstance
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ActiveTasks { get; set; } = string.Empty;
    }
}
