using System;

namespace Base.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException(string message = "Invalid Status Provided") : base(message)
        {
        }
    }
}