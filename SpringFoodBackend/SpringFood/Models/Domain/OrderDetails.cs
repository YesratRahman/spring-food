using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace SpringFoodBackend.Models.Domain
{
    public class OrderDetails
    {
        [Required]
        public int Quantity { get; set; }
        public Order Order { get; set; }
        [Key, Column(Order = 0)]
        public int? OrderId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [Key, Column(Order = 1)]
        public int? ProductId { get; set; }
       
    }
}
