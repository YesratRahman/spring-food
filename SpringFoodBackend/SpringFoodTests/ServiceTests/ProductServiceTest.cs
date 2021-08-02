using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.InMemRepos;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests
{
    class ProductServiceTest
    {
        ProductInMemRepo _productRepo;
        OrderInMemRepo _orderRepo;
        UserInMemRepo _userRepo;
        CategoryInMemRepo _categoryRepo;
        SpringFoodService _service;


        [SetUp]
        public void Setup()
        {
            _productRepo = new ProductInMemRepo();
            _orderRepo = new OrderInMemRepo();
            _userRepo = new UserInMemRepo();
            _categoryRepo = new CategoryInMemRepo();
            _service = new SpringFoodService(_productRepo, _userRepo, _categoryRepo, _orderRepo);

        }

        [TestCase("apple", 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void AddProductTest(
            String name, decimal price, int quantity,
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

            int addedId = _service.AddProduct(toAdd);
            Product savedProduct = _service.GetProductByName(name);
            Assert.AreEqual(expectedId, addedId);
            Assert.AreEqual(savedProduct.Name, name);
            Assert.AreEqual(savedProduct.Price, price);
            Assert.AreEqual(savedProduct.Quantity, quantity);
            Assert.AreEqual(savedProduct.Description, description);
            Assert.AreEqual(savedProduct.CategoryId, categoryId);
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

            Assert.Throws<ArgumentNullException>(() => _service.AddProduct(toAdd));
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

            Assert.Throws<ArgumentNullException>(() => _service.AddProduct(toAdd));
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

            Assert.Throws<ArgumentNullException>(() => _service.AddProduct(toAdd));
        }

        [TestCase("apples", 12.99, 50, "imageUrl", "Fresh Apples", 0)]
        [TestCase("apples", 12.99, 50, "imageUrl", "Fresh Apples", -12333)]
        public void AddProductByInvalidCategoryIdTest(String name, decimal price, int quantity,
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

            Assert.Throws<InvalidIdException>(() => _service.AddProduct(toAdd));
        }
        [TestCase("apples", 0, 50, "imageUrl", "Fresh Apples", 1)]
        [TestCase("apples", -12, 50, "imageUrl", "Fresh Apples", 1)]

        public void AddProductByInvalidPriceTest(String name, decimal price, int quantity,
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

            Assert.Throws<InvalidPriceException>(() => _service.AddProduct(toAdd));
        }
        [TestCase("apples", 4.99, 0, "imageUrl", "Fresh Apples", 1)]
        [TestCase("apples", 4.99, -50, "imageUrl", "Fresh Apples", 1)]
        public void AddProductByInvalidQuantityTest(String name, decimal price, int quantity,
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

            Assert.Throws<InvalidQuantityException>(() => _service.AddProduct(toAdd));
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetProductByInvalidCatId(int catId)
        {
            Assert.Throws<InvalidIdException>(() => _service.GetProductByCatId(catId));

        }
        [TestCase(0)]
        [TestCase(-10)]
        public void GetProductByInvalidProId(int proId)
        {
            Assert.Throws<InvalidIdException>(() => _service.GetOrderById(proId));

        }
        [TestCase(null)]
        public void GetProductByNullName(string name)
        {
            Assert.Throws<ArgumentNullException>(() => _service.GetProductByName(name));

        }
        [TestCase("")]
        public void GetProductByEmptyName(string name)
        {
            Assert.Throws<InvalidNameException>(() => _service.GetProductByName(name));

        }

        [TestCase("apple", 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void DeleteProductGoldenPathTest(
           String name, decimal price, int quantity,
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

            int addedId = _service.AddProduct(toAdd);
            Assert.AreEqual(expectedId, addedId);
            List<Product> products = _productRepo.GetAllProducts();

            Assert.AreEqual(1, products.Count);

            _service.DeleteProduct(addedId);
            products = _productRepo.GetAllProducts();
            Assert.AreEqual(0, products.Count);
        }

        [TestCase("apple", 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void DeleteProductByInvalidIdTest(
           String name, decimal price, int quantity,
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

            _service.AddProduct(toAdd);
            Assert.Throws<InvalidIdException>(() => _service.DeleteProduct(0));
            Assert.Throws<InvalidIdException>(() => _service.DeleteProduct(-12));
        }

        [TestCase("apple", 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void EditProductGoldenPathTest(
          String name, decimal price, int quantity,
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

            _service.AddProduct(toAdd);
            List<Product> products = _service.GetAllProducts();

            Product newProduct = toAdd;
            newProduct.Name = "Apple";
            newProduct.Price = 12;
            newProduct.Image = "newImage";
            _service.EditProduct(newProduct);
            Assert.AreEqual("Apple", products[0].Name);
            Assert.AreEqual(12, products[0].Price);
            Assert.AreEqual("newImage", products[0].Image);
        }
        [TestCase(null, 10.99, 50, "imageUrl", "Fresh apples", 1, 1)]
        public void EditProductByNullNameTest(
          String name, decimal price, int quantity,
          string image, string description, int categoryId, int expectedId)
        {
            Product toEdit = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image,
                Description = description,
                CategoryId = categoryId
            };
            Assert.Throws<ArgumentNullException>(() => _service.EditProduct(toEdit)); 
        }
        [TestCase("", 10.99, 50, "imageUrl", "Fresh apples", 1)]
        [TestCase("     ", 10.99, 50, "imageUrl", "Fresh apples", 1)]
        public void EditProductByInvalidNameTest(
          String name, decimal price, int quantity,
          string image, string description, int categoryId)
        {
            Product toEdit = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image,
                Description = description,
                CategoryId = categoryId
            };
            Assert.Throws<InvalidNameException>(() => _service.EditProduct(toEdit));
        }
       
    }
}
