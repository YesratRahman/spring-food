using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Repos;
using SpringFoodBackend.Services;
using System.Linq;
using System.Security.Claims;

namespace SpringFoodBackend.Controllers
{

    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {

        ISpringFoodService _service;
        public ProductController(ISpringFoodService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(Product product)
        {

            _service.AddProduct(product);
            return this.Accepted(product);


        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product product = _service.GetProductById(id);
            return this.Accepted(product);

        }
        //[HttpGet("name/{name}")]
        //public IActionResult GetProductByName(string name)
        //{
        //    Product product = _service.GetProductByName(name);
        //    return this.Accepted(product);

        //}
        [HttpGet]
        public IActionResult GetAllProducts()
        {

            return this.Accepted(_service.GetAllProducts());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public IActionResult EditProduct(Product product)
        {

            _service.EditProduct(product);
            return this.Accepted(product);



        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {

            _service.DeleteProduct(id);
            return this.Accepted();

        }


    }
}
