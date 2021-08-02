using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Services
{
    [Serializable]
    public class InvalidNameException : Exception
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException(string message) : base(message)
        {
        }

        public InvalidNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}