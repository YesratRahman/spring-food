using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;

namespace SpringFoodBackend.Repos
{
    public class CategoryRepo : ICategory
    {
        private SpringFoodDbContext _context;

        public CategoryRepo(SpringFoodDbContext context)
        {
            _context = context;
        }
        public int AddCategory(Category toAdd)
        {
            if(toAdd == null)
            {
                throw new ArgumentNullException("Category is null.");
            }
            if (toAdd.Name == null)
                throw new ArgumentNullException("Category name is null"); 
            _context.Categories.Add(toAdd);
            _context.SaveChanges();
            return toAdd.Id;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.Include(category => category.Products).ToList() ; 
        }

        
    }
}