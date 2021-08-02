using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.InMemRepos
{
    public class ProductInMemRepo : IProduct
    {
        private List<Product> _allProducts = new List<Product>(); 
        public int AddProduct(Product toAdd)
        {
            toAdd.Id = _allProducts.Count + 1;
            _allProducts.Add(toAdd);
            return toAdd.Id;
        }

        public void DeleteProduct(int id)
        {
            _allProducts.RemoveAll(p => p.Id == id); 
        }

        public void EditProduct(Product toEdit)
        {
            _allProducts = _allProducts.Select(w => w.Id == toEdit.Id ? toEdit : w).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _allProducts; 
        }

        public Product GetProductById(int id)
        {
            return _allProducts.SingleOrDefault(w => w.Id == id);
        }
        public Product GetProductByName(string name)
        {
            return _allProducts.SingleOrDefault(p => p.Name == name);
        }
        public List<Product> GetProductByCatId(int id)
        {
            List<Product> allProducts = new List<Product>();
            foreach (Product p in _allProducts)
            {
                if (p.Category.Id == id)
                {
                    allProducts.Add(p);
                }
            }
            return allProducts;
        }
    }
}
