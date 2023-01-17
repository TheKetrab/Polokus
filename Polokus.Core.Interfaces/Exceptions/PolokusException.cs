using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.Exceptions
{
    public class PolokusException : Exception
    {
        public PolokusException() { }
        public PolokusException(string message) : base(message) { }
        public PolokusException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
