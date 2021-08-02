using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Exceptions
{
    public class ProductNotFoundException: Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
