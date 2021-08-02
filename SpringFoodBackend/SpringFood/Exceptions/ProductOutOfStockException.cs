using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Exceptions
{
    public class ProductOutOfStockException: Exception
    {
        public ProductOutOfStockException(string message) : base(message)
        {
        }

        public ProductOutOfStockException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
