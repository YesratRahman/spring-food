using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpringFoodBackend.Models;
using SpringFoodBackend.Models.Auth;

namespace SpringFoodBackend.Models.Auth
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;  }
        [Required]
        public string Email { get; set; }
        public List<UserRole> Roles { get; set; } = new List<UserRole>(); 




        //OneToMany 
        //public List<Cart> Carts { get; set; } = new List<Cart>(); 
        
        
        public User()
        {

        }
    }
}
