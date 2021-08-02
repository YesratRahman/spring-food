using NUnit.Framework;
using SpringFoodBackend.InMemRepos;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringFoodTests.ServiceTests
{
    class CategoryServiceTest
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
        [TestCase("Fruits")]
        public void AddCategoryGoldenPathTest(string name)
        {
            Category toAdd = new Category
            {
                Name = name
            };
            _service.AddCategory(toAdd);
            List<Category> categories = _service.GetAllCategories();
            Assert.AreEqual(1, categories.Count);
            Assert.AreEqual("Fruits", categories[0].Name);

        }
        public void AddCategoryByNullCategoryObjectTest()
        { 
            Assert.Throws<ArgumentNullException>(() => _service.AddCategory(null));
        }
        [TestCase(null)]
        public void AddCategoryByNullNameTest(string name)
        {
            Category toAdd = new Category
            {
                Name = name
            };
            Assert.Throws<ArgumentNullException>(() => _service.AddCategory(toAdd));
        }
        [TestCase("")]
        [TestCase("    ")]

        public void AddCategoryByInvlaidEntryTest(string name)
        {
            Category toAdd = new Category
            {
                Name = name
            };
            Assert.Throws<InvalidNameException>(() => _service.AddCategory(toAdd));
        }
    }
}
