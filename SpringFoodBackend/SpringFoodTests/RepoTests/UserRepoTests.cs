using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.RepoTests
{
    class UserRepoTests
    {
        UserRepo _userRepo;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            var builder = new DbContextOptionsBuilder<SpringFoodDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            SpringFoodDbContext newContext = new SpringFoodDbContext(builder.Options);
            _userRepo = new UserRepo(newContext);
            newContext.RemoveRange(newContext.Orders);
            newContext.RemoveRange(newContext.Users);
            newContext.SaveChanges();
        }

        [Test]
        public void AddUserTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);
            Assert.AreEqual("testUser", toAdd.Username);
            Assert.AreEqual("test@gmail.com", toAdd.Email);
        }

        [Test]
        public void GetUserByIdTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);
            User getUser = _userRepo.GetUserById(toAdd.Id);
            Assert.AreEqual("testUser", getUser.Username);
            Assert.AreEqual("test@gmail.com", getUser.Email);
        }

        [Test]
        public void GetUserByUsernameTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);

            User user = _userRepo.GetUserByUserName("testUser");

            Assert.IsNotNull(user.Id);
            Assert.AreEqual("testUser", user.Username);
            Assert.AreEqual("test@gmail.com", user.Email);
        } 

        [Test]
        public void GetAllUsersTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);
            List<User> users = _userRepo.GetAllUsers();
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("testUser", users[0].Username);
            Assert.AreEqual("test@gmail.com", users[0].Email);
        }

        [Test]
        public void UpdateUserTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);
            User newUser = toAdd;
            newUser.Email = "newTest@gmail.com";
            newUser.Username = "newusername";
            _userRepo.EditUser(newUser);
            Assert.AreEqual("newusername", newUser.Username);
            Assert.AreEqual("newTest@gmail.com", newUser.Email);
        }
        [Test]
        public void DeleteUserTest()
        {
            User toAdd = new User
            {
                Username = "testUser",
                Email = "test@gmail.com",

            };
            _userRepo.AddUser(toAdd);
            _userRepo.DeleteUser(toAdd.Id);
            List<User> users = new List<User>();
            Assert.AreEqual(0, users.Count); 
        }
    } 
}
