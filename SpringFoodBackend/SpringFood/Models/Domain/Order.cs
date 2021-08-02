using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpringFoodBackend.Models.Auth;

namespace SpringFoodBackend.Models.Domain
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set;  }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
     
        [Required]
        [Column(TypeName = "decimal(5,3")]
        public decimal OrderTotal { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get;  set; }
        [Required]
        public string HomeNumber { get; set; }
        [Required]
        public int PostalCode { get; set; }
   
        public List<OrderDetails> OrderDetails { get; set; }
        public User Purchaser { get; set; }

    }
}
