using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpringFoodBackend.Models.Domain
{
    [Table("Categories")]
    public class Category
    {
        
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        //public Category()
        //{

        //}
        //public Category(string name)
        //{
        //    this.Name = name; 
        //}
        //public Category(Category that)
        //{
        //    this.Id = that.Id;
        //    this.Name = that.Name; 
        //}
    
    }
}
