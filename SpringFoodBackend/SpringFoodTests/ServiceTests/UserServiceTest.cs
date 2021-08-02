using NUnit.Framework;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.InMemRepos;
using SpringFoodBackend.Models.ViewModels.Requests;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.ServiceTests
{
    class UserServiceTest
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

        [TestCase("testUsername", "test@email.com", "test123")]
        public void RegisterUserGoldenPathTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            _service.RegisterUser(toAdd);
        }
        [TestCase(null, "test@email.com", "test123")]
        public void RegisterUserWithNullUsernameTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<ArgumentNullException>(()=>_service.RegisterUser(toAdd));
        }
        [TestCase("testUsername", null, "test123")]
        public void RegisterUserWithNullEmailTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toAdd));
        }
        [TestCase("", "testEmail", "test123")]
        [TestCase("       ", "test@email.com", "test123")]
        public void RegisterUserWithInvalidusernameTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<InvalidNameException>(() => _service.RegisterUser(toAdd));
        }
        [TestCase("testUsername", "test@email.com", null)]
        public void RegisterUserWithNullPasswordTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<ArgumentNullException>(() => _service.RegisterUser(toAdd));
        }
        [TestCase("testUsername", "", "test123")]
        [TestCase("testUsername", "    ", "test123")]
        public void RegisterUserWithInvalidEmailTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<InvalidEmailException>(() => _service.RegisterUser(toAdd));
        }
        [TestCase("testUsername", "test@email.com", "test12345678")]
        [TestCase("testUsername", "test@email.com", "")]
        [TestCase("testUsername", "test@email.com", "    ")]

        public void RegisterUserWithOutOfLengthPasswordTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            Assert.Throws<InvalidPasswordException>(() => _service.RegisterUser(toAdd));
        }
        [TestCase("testUsername", "test@email.com", "test123")]
        public void RegisterUserWithSameUsernameTest(string username, string email, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = username,
                Email = email,
                Password = password
            };
            _service.RegisterUser(toAdd);
            Assert.Throws<UserNameInUseException>(() => _service.RegisterUser(toAdd));

        }

        [TestCase("testUsername", "test123")]
        public void LoginGoldenPathTest(string username, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = "testUsername",
                Email = "test@email.com",
                Password = "test123"
            };
            _service.RegisterUser(toAdd);
            LoginRequest toLogin = new LoginRequest
            {
                Username = username,
                Password = password
            };
            _service.Login(toLogin);
        }
        [TestCase(null, "test123")]
        [TestCase("testUsername", null)]
        public void LoginWithNullEntryTest(string username, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = "testUsername",
                Email = "test@email.com",
                Password = "test123"
            };
            _service.RegisterUser(toAdd);
            LoginRequest toLogin = new LoginRequest
            {
                Username = username,
                Password = password
            };
            Assert.Throws<ArgumentNullException>(()=>_service.Login(toLogin));
        }

        
        public void LoginWithInvalidPasswordTest(string username, string password)
        {
            RegisterUserViewModel toAdd = new RegisterUserViewModel
            {
                Username = "testUsername",
                Email = "test@email.com",
                Password = "test123"
            };
            _service.RegisterUser(toAdd);
            LoginRequest toLogin = new LoginRequest
            {
                Username = "testUsername",
                Password = "test12345"
            };
            Assert.Throws<InvalidPasswordException>(() => _service.Login(toLogin));
        }
    }
}
