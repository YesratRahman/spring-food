using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.RepoTests
{
    class OrderRepoTests
    {
        OrderRepo _orderRepo;
        User _user;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            var builder = new DbContextOptionsBuilder<SpringFoodDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            SpringFoodDbContext newContext = new SpringFoodDbContext(builder.Options);
            _orderRepo = new OrderRepo(newContext);
            newContext.Orders.RemoveRange(newContext.Orders);
            newContext.SaveChanges();

            newContext.RemoveRange(newContext.Users);
            _user = new User
            {
                Username = "testName",
                Email = "test@gmail.com"
            };
            newContext.Users.Add(_user);
            newContext.SaveChanges();
        }

        [Test]
        public void AddOrderGoldenPathTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            List<Order> testAllOrders = _orderRepo.GetAllOrders();
            Assert.AreEqual(1, testAllOrders.Count);
            Assert.AreEqual("Test", testAllOrders[0].Name);
            Assert.AreEqual("test@gmail.com", testAllOrders[0].Email);
            Assert.AreEqual(100, testAllOrders[0].OrderTotal);
            Assert.AreEqual("Herndon", testAllOrders[0].City);
            Assert.AreEqual("st", testAllOrders[0].Street);
            Assert.AreEqual("0", testAllOrders[0].HomeNumber);
            Assert.AreEqual(20170, testAllOrders[0].PostalCode);
            Assert.AreEqual(_user, testAllOrders[0].Purchaser);
        }

        public void AddOrderByNullOrderObject()
        {
            Assert.Throws<ArgumentNullException>(() => _orderRepo.AddOrder(null));
        }

        [TestCase(null, "test@email.com", 100.12, "123 St", "herndon", "5715250989", 20170)]
        [TestCase("Raha", null, 100.12, "123 St", "herndon", "5715250989", 20170)]
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
            Assert.Throws<ArgumentNullException>(() => _orderRepo.AddOrder(toAdd));
        }
        [Test]
        public void GetOrderByIdTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            Order newOrder = _orderRepo.GetOrderById(toAdd.Id);
            Assert.AreEqual("Test", newOrder.Name);
            Assert.IsNotNull(newOrder.Id);
        }

        [Test]
        public void GetAllOrdersTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            List<Order> testAllOrders = _orderRepo.GetAllOrders();
            Assert.AreEqual(1, testAllOrders.Count);
            Assert.AreEqual("Test", testAllOrders[0].Name);
            Assert.AreEqual("test@gmail.com", testAllOrders[0].Email);
            Assert.AreEqual(100, testAllOrders[0].OrderTotal);
            Assert.AreEqual("Herndon", testAllOrders[0].City);
            Assert.AreEqual("st", testAllOrders[0].Street);
            Assert.AreEqual("0", testAllOrders[0].HomeNumber);
            Assert.AreEqual(20170, testAllOrders[0].PostalCode);
            Assert.AreEqual(_user, testAllOrders[0].Purchaser);
            Assert.IsNotNull(testAllOrders[0].Id);
        }
        [Test]
        public void GetOrdersByUserIdTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            List<Order> orders = _orderRepo.GetOrdersByUserId(_user.Id);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual("Test", orders[0].Name);
            Assert.AreEqual("test@gmail.com", orders[0].Email);
            Assert.AreEqual(100, orders[0].OrderTotal);
            Assert.AreEqual("Herndon", orders[0].City);
            Assert.AreEqual("st", orders[0].Street);
            Assert.AreEqual("0", orders[0].HomeNumber);
            Assert.AreEqual(20170, orders[0].PostalCode);
            Assert.AreEqual(_user, orders[0].Purchaser);
            Assert.IsNotNull(orders[0].Id);
        }

        [Test]
        public void UpdateOrderTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            List<Order> orders = _orderRepo.GetAllOrders();
            Order newOrder = toAdd;
            toAdd.Name = "neworder";
            toAdd.City = "newCity";
            toAdd.HomeNumber = "12";
            _orderRepo.EditOrder(newOrder);

            Assert.AreEqual("neworder", orders[0].Name);
            Assert.AreEqual("newCity", orders[0].City);
            Assert.AreEqual("12", orders[0].HomeNumber);
            Assert.AreEqual(20170, orders[0].PostalCode);
        }
        [Test]
        public void DeleteOrderTest()
        {
            Order toAdd = new Order
            {
                Name = "Test",
                Email = "test@gmail.com",
                OrderTotal = 100,
                City = "Herndon",
                Street = "st",
                HomeNumber = "0",
                PostalCode = 20170,
                Purchaser = _user,
                OrderDetails = new List<OrderDetails>()

            };
            _orderRepo.AddOrder(toAdd);
            _orderRepo.DeleteOrder(toAdd.Id);

            List<Order> orders = new List<Order>();
            Assert.AreEqual(0, orders.Count);
        }


    }
} 
