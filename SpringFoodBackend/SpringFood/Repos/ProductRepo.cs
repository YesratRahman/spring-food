using Microsoft.EntityFrameworkCore;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace SpringFoodBackend.Repos
{
    public class ProductRepo: IProduct
    {
        private SpringFoodDbContext _context;

        public ProductRepo(SpringFoodDbContext context)
        {
            _context = context;
        }

        public int AddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException("A null product can't be added!"); 
            }
            if (product.Name == null)
            {
                throw new ArgumentNullException("Can't add a product with null name");
            }
            if (product.Description == null)
            {
                throw new ArgumentNullException("Can't add a product with null description");
            }

            if (product.Image == null)
            {
                throw new ArgumentNullException("Can't add a product with null image");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            
            return product.Id; 
        }
        public List<Product> GetAllProducts()
        {
            
            return _context.Products.ToList();

        }

        public Product GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Product Id is invalid!"); 
            }
            Product product = _context.Products.Find(id);
            if(product == null)
            {
                throw new ProductNotFoundException($"Couldn't find product with id {id}");
            }
           
            return product;
        }

        public Product GetProductByName(string name)
        {
            if(name == null)
            {
                throw new ArgumentNullException("Product name is null.");
            }
            Product product = _context.Products.Where(p => p.Name == name).SingleOrDefault();
            if (product == null)
            {
                throw new ProductNotFoundException($"Couldn't find product with name {name}");
            }
            return product;
        }

        public void EditProduct(Product toEdit)
        {
            if(toEdit == null)
            {
                throw new ArgumentNullException("Null product can't be edited!"); 
            }
            if(toEdit.Id <= 0)
            {
                throw new InvalidIdException("Product Id is Invalid"); 
            }
            if (toEdit.Name == null)
            {
                throw new ArgumentNullException("Can't add a product with null name");
            }
            if (toEdit.Description == null)
            {
                throw new ArgumentNullException("Can't add a product with null description");
            }

            if (toEdit.Image == null)
            {
                throw new ArgumentNullException("Can't add a product with null image");
            }
            if (_context.Products.Find(toEdit.Id) == null)
            {
                throw new ProductNotFoundException($"Couldn't find product with id {toEdit.Id}");
            }
            _context.Attach(toEdit);
            _context.Entry(toEdit).State = EntityState.Modified;
            _context.SaveChanges(); 

        }
        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Product Id is invalid!");
            }

            Product toDelete = _context.Products.Find(id);
            if(toDelete == null)
            {
                throw new ProductNotFoundException($"Couldn't find product with id {id}");
            }
            _context.Attach(toDelete);
            _context.Remove(toDelete);
            _context.SaveChanges();
        }
        public List<Product> GetProductByCatId(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Product Id is invalid!");
            }
            List<Product> products = _context.Products.Where(x => x.CategoryId == id).ToList();

            Category toCat = _context.Categories.Find(id);
            if (toCat == null)
            {
                throw new CategoryNotFoundException($"Couldn't find category with id {toCat.Id}");
            }
            _context.Attach(toCat);
            return products;
        }

    }
}
