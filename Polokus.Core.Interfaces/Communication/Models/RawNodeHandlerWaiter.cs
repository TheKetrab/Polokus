namespace Polokus.Core.Interfaces.Communication.Models
{
    public class RawNodeHandlerWaiter
    {
        public string Id { get; set; } = string.Empty;
        public string NodeToCall { get; set; } = string.Empty;
        public string WaiterType { get; set; } = string.Empty;
    }
}
