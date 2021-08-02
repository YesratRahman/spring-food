using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Services
{
    [Serializable]
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException()
        {
        }

        public InvalidPriceException(string message) : base(message)
        {
        }

        public InvalidPriceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidPriceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}