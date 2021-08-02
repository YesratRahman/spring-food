using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SpringFoodBackend.Repos;
using System.Configuration; 
using Microsoft.Extensions.Configuration;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using System.Collections.Generic;
using SpringFoodBackend.Models.Auth;
using System;

namespace SpringFoodTests.RepoTests
{
    public class ProductRepoTests
    {
        ProductRepo _productRepo;
        Category _category;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            var builder = new DbContextOptionsBuilder<SpringFoodDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            SpringFoodDbContext newContext = new SpringFoodDbContext(builder.Options);
            _productRepo = new ProductRepo(newContext);
            newContext.Products.RemoveRange(newContext.Products);
            newContext.SaveChanges();

            newContext.Categories.RemoveRange(newContext.Categories);
            _category = new Category
            {
                Name = "Fruits"
            };
            newContext.Categories.Add(_category);

            newContext.SaveChanges();

           
        }

        [Test]
        public void AddProductGoldenPathTest()
        {
            Product toAdd = new Product
            {
                Name = "Coffee",
                Price = 10,
                Quantity = 120,
                Image = "https://images.albertsons-media.com/is/image/ABS/960094807?$ecom-pdp-tablet$&defaultImage=Not_Available&defaultImage=Not_Available",
                Description = "Starbucks Coffee Ground",
                CategoryId = _category.Id,
            };
            _productRepo.AddProduct(toAdd);

            List<Product> testAllProducts = _productRepo.GetAllProducts();
            Assert.AreEqual(1, testAllProducts.Count);
            Assert.AreEqual("Coffee", testAllProducts[0].Name);
            Assert.AreEqual(10, testAllProducts[0].Price);
            Assert.AreEqual(120, testAllProducts[0].Quantity);
            Assert.AreEqual("https://images.albertsons-media.com/is/image/ABS/960094807?$ecom-pdp-tablet$&defaultImage=Not_Available&defaultImage=Not_Available", testAllProducts[0].Image);
            Assert.AreEqual("Starbucks Coffee Ground", testAllProducts[0].Description);
            Assert.AreEqual(_category.Id, testAllProducts[0].CategoryId);

        }
        public void AddProductByNullProductTest()
        {
            Assert.Throws<ArgumentNullException>(() => _productRepo.AddProduct(null));
        }

        [TestCase(null, 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void AddProductByNullNameTest(String name, decimal price, int quantity,
           string image, string description, int categoryId, int expectedId)
        {
            Product toAdd = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image,
                Description = description,
                CategoryId = categoryId
            };

            Assert.Throws<ArgumentNullException>(() => _productRepo.AddProduct(toAdd));
        }
        [TestCase("apples", 10.99, 50, null, "Fresh apples", 1)]
        public void AddProductByNullImageTest(String name, decimal price, int quantity,
           string image, string description, int categoryId)
        {
            Product toAdd = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image,
                Description = description,
                CategoryId = categoryId
            };

            Assert.Throws<ArgumentNullException>(() => _productRepo.AddProduct(toAdd));
        }
        [TestCase("apples", 12.99, 50, "imageUrl", null, 1)]
        public void AddProductByNullDescriptionTest(String name, decimal price, int quantity,
          string image, string description, int categoryId)
        {
            Product toAdd = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image,
                Description = description,
                CategoryId = categoryId
            };

            Assert.Throws<ArgumentNullException>(() => _productRepo.AddProduct(toAdd));
        }

        [Test]
        public void GetProductById()
        {
            Product toAdd = new Product
            {
                Name = "Coffee",
                Price = 10,
                Quantity = 120,
                Image = "url",
                Description = "Starbucks Coffee Ground",
                CategoryId = _category.Id,
            };
            _productRepo.AddProduct(toAdd);

            Product productNew = _productRepo.GetProductById(toAdd.Id);

            Assert.AreEqual("Coffee", productNew.Name);
            Assert.AreEqual(10, productNew.Price);
            Assert.AreEqual(120, productNew.Quantity);
            Assert.AreEqual("url", productNew.Image);
            Assert.AreEqual("Starbucks Coffee Ground", productNew.Description);
            Assert.AreEqual(_category.Id, productNew.CategoryId);
            Assert.IsNotNull(productNew.Id);

        }

        [Test]
        public void GetAllProducts()
        {
            Product toAdd = new Product
            {
                Name = "Coffee",
                Price = 10,
                Quantity = 120,
                Image = "url",
                Description = "Starbucks Coffee Ground",
                CategoryId = _category.Id,
            };
            _productRepo.AddProduct(toAdd);

            List<Product> testAllProducts = _productRepo.GetAllProducts();
            Assert.AreEqual(1, testAllProducts.Count);
            Assert.AreEqual("Coffee", testAllProducts[0].Name);
            Assert.AreEqual(10, testAllProducts[0].Price);
            Assert.AreEqual(120, testAllProducts[0].Quantity);
            Assert.AreEqual("url", testAllProducts[0].Image);
            Assert.AreEqual("Starbucks Coffee Ground", testAllProducts[0].Description);
            Assert.AreEqual(_category.Id, testAllProducts[0].CategoryId);
            Assert.IsNotNull(testAllProducts[0].Id);
        }

        [Test]
        public void UpdateProductTest()
        {
            Product toAdd = new Product
            {
                Name = "Coffee",
                Price = 10,
                Quantity = 120,
                Image = "url",
                Description = "Starbucks Coffee Ground",
                CategoryId = _category.Id,
            };
            _productRepo.AddProduct(toAdd);
            List<Product> products = _productRepo.GetAllProducts();

            Product newProduct = toAdd;
            newProduct.Name = "Apple";
            newProduct.Price = 12;
            newProduct.Image = "newImage";
            _productRepo.EditProduct(newProduct);
            Assert.AreEqual("Apple", products[0].Name);
            Assert.AreEqual(12, products[0].Price);
            Assert.AreEqual("newImage", products[0].Image);

        }

        [Test]
        public void DeleteProductTest()
        {
            Product toAdd = new Product
            {
                Name = "Coffee",
                Price = 10,
                Quantity = 120,
                Image = "url",
                Description = "Starbucks Coffee Ground",
                CategoryId = _category.Id,
            };
            _productRepo.AddProduct(toAdd);
            List<Product> products = _productRepo.GetAllProducts();
            Assert.AreEqual(1, products.Count);

            _productRepo.DeleteProduct(toAdd.Id);
            products = _productRepo.GetAllProducts();
            Assert.AreEqual(0, products.Count);


        }

    }
}