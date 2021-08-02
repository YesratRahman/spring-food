using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Repos;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController: ControllerBase 
    {
        ISpringFoodService _service;
        public CategoryController(ISpringFoodService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult AddCategory(Category toAdd)
        {
            _service.AddCategory(toAdd);
            return this.Accepted(toAdd);

        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {

            return this.Accepted(_service.GetAllCategories());
        }
        [HttpGet("{id}")]
        public IActionResult GetProductByCatId(int id)
        {
            List<Product> product = _service.GetProductByCatId(id);
            return this.Accepted(product);

        }
    }
}
