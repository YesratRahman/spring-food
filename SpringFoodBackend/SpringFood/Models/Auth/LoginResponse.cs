using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Models.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set;  }
    }
}
