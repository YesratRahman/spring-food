using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.InMemRepos
{
    public class CategoryInMemRepo : ICategory
    {
        private List<Category> _allCategories = new List<Category>(); 
        public int AddCategory(Category toAdd)
        {
            toAdd.Id = _allCategories.Count + 1;
            _allCategories.Add(toAdd);
            return toAdd.Id;
        }

        public List<Category> GetAllCategories()
        {
            return _allCategories; 
        }

       
    }
}
