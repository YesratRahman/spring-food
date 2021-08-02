using System;
using System.Runtime.Serialization;

namespace SpringFoodBackend.Repos
{
    [Serializable]
    public class InvalidRoleNameException : Exception
    {
        public InvalidRoleNameException()
        {
        }

        public InvalidRoleNameException(string message) : base(message)
        {
        }

        public InvalidRoleNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRoleNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}