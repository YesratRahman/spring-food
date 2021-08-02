using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Repos
{
    [Serializable]
    internal class InvalidUsernameException : Exception
    {
        public InvalidUsernameException()
        {
        }

        public InvalidUsernameException(string message) : base(message)
        {
        }

        public InvalidUsernameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUsernameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}