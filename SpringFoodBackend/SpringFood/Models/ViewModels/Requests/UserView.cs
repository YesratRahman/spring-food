using SpringFoodBackend.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Models.ViewModels
{
    public class UserView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }


        public UserView(User org)
        {
            Email = org.Email;
            Username = org.Username;
            Id = org.Id; 
            
        }
    }
}
