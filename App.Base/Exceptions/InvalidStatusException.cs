using System;

namespace App.Base.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException(string message = "Invalid Status Provided") : base(message)
        {
        }
    }
}