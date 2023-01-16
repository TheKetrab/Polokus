namespace Polokus.Core.Interfaces.Communication.Models
{
    public class RawNodeHandlerWaiter
    {
        public string Id { get; set;  }
        public string NodeToCall { get; set; }
        public string WaiterType { get; set; }
    }
}
