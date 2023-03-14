namespace Polokus.Core.Interfaces.Exceptions
{
    public class PolokusException : Exception
    {
        public PolokusException() { }
        public PolokusException(string message) : base(message) { }
        public PolokusException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
