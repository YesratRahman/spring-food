using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Services
{
    [Serializable]
    public class UserNameInUseException : Exception
    {
        public UserNameInUseException()
        {
        }

        public UserNameInUseException(string message) : base(message)
        {
        }

        public UserNameInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNameInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}