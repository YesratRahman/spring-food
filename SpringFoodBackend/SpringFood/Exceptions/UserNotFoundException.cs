using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Exceptions
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
