using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Services
{
    [Serializable]
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException()
        {
        }

        public InvalidQuantityException(string message) : base(message)
        {
        }

        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidQuantityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}