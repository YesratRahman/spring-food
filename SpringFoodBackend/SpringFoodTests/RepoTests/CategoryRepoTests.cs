using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.RepoTests
{
    class CategoryRepoTests
    {
        CategoryRepo _categoryRepo;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            var builder = new DbContextOptionsBuilder<SpringFoodDbContext>();
            builder.UseSqlServer(config.GetConnectionString("TestDb"));

            SpringFoodDbContext newContext = new SpringFoodDbContext(builder.Options);
            _categoryRepo = new CategoryRepo(newContext);
            newContext.RemoveRange(newContext.Categories);
            newContext.SaveChanges();
        }
        [Test]
        public void AddCategoryTest()
        {
            Category toAdd = new Category
            {
                Name = "Fruits"
            };
            _categoryRepo.AddCategory(toAdd);

            Category toAdd1 = new Category
            {
                Name = "Bakery"
            };
            _categoryRepo.AddCategory(toAdd1);

            List<Category> categories = _categoryRepo.GetAllCategories();
            Assert.AreEqual(2, categories.Count);
            Assert.AreEqual("Fruits", categories[0].Name);
            Assert.AreEqual("Bakery", categories[1].Name);

        }

        [Test]
        public void GetCategoryByIdTest()
        {
            Category toAdd = new Category
            {
                Name = "Fruits"
            };
            _categoryRepo.AddCategory(toAdd);
            List<Category> categories = _categoryRepo.GetAllCategories();

            Assert.AreEqual(1, categories.Count);
            Assert.AreEqual("Fruits", categories[0].Name); 
        }
        public void AddCategoryByNullCategoryObjectTest()
        {
            Assert.Throws<ArgumentNullException>(() => _categoryRepo.AddCategory(null));
        }
        [TestCase(null)]
        public void AddCategoryByNullNameTest(string name)
        {
            Category toAdd = new Category
            {
                Name = name
            };
            Assert.Throws<ArgumentNullException>(() => _categoryRepo.AddCategory(toAdd));
        }

    } 
}
