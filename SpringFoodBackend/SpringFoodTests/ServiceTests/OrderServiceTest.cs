using NUnit.Framework;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.InMemRepos;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.ServiceTests
{
    class OrderServiceTest
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
        [TestCase("Raha", "test@email.com", 100.12, "123 St", "herndon", "5715250989",20170 )]
        public void AddOrderGoldenPathTest(string name, string email, decimal orderTotal,
            string street, string city, string homeNumber, int postalCode)
        {
            Order toAdd = new Order
            {
                Name = name,
                Email = email,
                OrderTotal = orderTotal,
                Street = street,
                City = city,
                HomeNumber = homeNumber,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetails>()

            };
           int addedId =  _service.AddOrder(toAdd);
            Assert.AreEqual(1, addedId);
            List<Order> allOrders = _service.GetAllOrders();
            Assert.AreEqual(1, allOrders.Count);
            Assert.AreEqual("Raha", allOrders[0].Name);
            Assert.AreEqual("test@email.com", allOrders[0].Email);
            Assert.AreEqual(100.12, allOrders[0].OrderTotal);
            Assert.AreEqual("123 St", allOrders[0].Street);
            Assert.AreEqual("herndon", allOrders[0].City);
            Assert.AreEqual("5715250989", allOrders[0].HomeNumber);
            Assert.AreEqual(20170, allOrders[0].PostalCode);
            _service.DeleteOrder(addedId);
            allOrders = _service.GetAllOrders();
            Assert.AreEqual(0, allOrders.Count);
        }
        public void AddOrderByNullOrderObject()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddOrder(null));
        }

        [TestCase(null, "test@email.com", 100.12, "123 St", "herndon", "5715250989", 20170)]
        [TestCase("Raha",null, 100.12, "123 St", "herndon", "5715250989", 20170)]
        [TestCase("Raha", "test@email.com", 100.12, null, "herndon", "5715250989", 20170)]
        [TestCase("Raha", "test@email.com", 100.12, "123 St", null, "5715250989", 20170)]
        [TestCase("Raha", "test@email.com", 100.12, "123 St", "herndon", null, 20170)]
        public void AddOrderByNullEntryTest(string name, string email, decimal orderTotal,
            string city, string street, string homeNumber, int postalCode)
        {
            Order toAdd = new Order
            {
                Name = name,
                Email = email,
                OrderTotal = orderTotal,
                City = city,
                Street = street,
                HomeNumber = homeNumber,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetails>()

            };
            Assert.Throws<ArgumentNullException>(()=> _service.AddOrder(toAdd));
        }
        [TestCase("", "test@email.com", 100.12, "123 St", "herndon", "5715250989", 20170)]
        [TestCase("    ", "test@email.com", 100.12, "123 St", "herndon", "5715250989", 20170)]
        public void AddOrderByInvalidNameTest(string name, string email, decimal orderTotal,
            string city, string street, string homeNumber, int postalCode)
        {
            Order toAdd = new Order
            {
                Name = name,
                Email = email,
                OrderTotal = orderTotal,
                City = city,
                Street = street,
                HomeNumber = homeNumber,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetails>()

            };
            Assert.Throws<InvalidNameException>(() => _service.AddOrder(toAdd));
        }
        [TestCase("raha", "", 100.12, "123 St", "herndon", "5715250989", 20170)]
        [TestCase("raha", "    ", 100.12, "123 St", "herndon", "5715250989", 20170)]
        public void AddOrderByInvalidEmailTest(string name, string email, decimal orderTotal,
            string city, string street, string homeNumber, int postalCode)
        {
            Order toAdd = new Order
            {
                Name = name,
                Email = email,
                OrderTotal = orderTotal,
                City = city,
                Street = street,
                HomeNumber = homeNumber,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetails>()

            };
            Assert.Throws<InvalidEmailException>(() => _service.AddOrder(toAdd));
        }


        [TestCase("Raha", "test@email.com", 100.12, "herndon", "123 St", "5715250989", 20170)]
        public void GetOrderByIdTest(string name, string email, decimal orderTotal,
            string city, string street, string homeNumber, int postalCode)
        {
            Order toAdd = new Order
            {
                Name = name,
                Email = email,
                OrderTotal = orderTotal,
                City = city,
                Street = street,
                HomeNumber = homeNumber,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetails>()

            };
           int orderId = _service.AddOrder(toAdd);
            Order toget = _service.GetOrderById(orderId);
            Assert.AreEqual("Raha", toget.Name);
            Assert.AreEqual("test@email.com", toget.Email);
            Assert.AreEqual(100.12, toget.OrderTotal);
            Assert.AreEqual("123 St", toget.Street);
            Assert.AreEqual("herndon", toget.City);
            Assert.AreEqual("5715250989", toget.HomeNumber);
            Assert.AreEqual(20170, toget.PostalCode);
        }
        [TestCase(0)]
        [TestCase(-1)]
        public void GetOrderByInvalidIdTest(int id)
        {
            Assert.Throws<InvalidIdException>(() => _service.GetOrderById(id));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void GetOrdersByInvalidUserIdTest(int userId)
        {
            Assert.Throws<InvalidIdException>(() => _service.getOrdersByUserId(userId));
        }
        [TestCase(0)]
        [TestCase(-1)]
        public void DeleteOrderInvalidIdTest(int id)
        {
            Assert.Throws<InvalidIdException>(() => _service.DeleteOrder(id));
        }
    }
}
