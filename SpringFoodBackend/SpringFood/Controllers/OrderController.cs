using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Repos;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpringFoodBackend.Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController: ControllerBase
    {
        ISpringFoodService _service; 
        public OrderController(ISpringFoodService service)
        {
            _service = service; 
        }

        [HttpPost]
        public IActionResult AddOrder(Order toAdd)
        {
            _service.AddOrder(toAdd);
            return Accepted(toAdd); 
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllOrders()
        {
            if (this.User.Claims.Any(c => c.Type == ClaimTypes.Role.ToString() && c.Value == "Admin"))
            {
                return this.Accepted(_service.GetAllOrders());

            }
            else
            {
                int curUserId = int.Parse(this.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier.ToString()).Value);
                return Ok(_service.getOrdersByUserId(curUserId)); 
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            return Accepted(_service.GetOrderById(id));
        }
        [HttpPut]
        public IActionResult EditOrder(Order toEdit)
        {
            _service.EditOrder(toEdit);
            return this.Accepted(toEdit);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _service.DeleteOrder(id);
            return this.Accepted();
        }

    }
}
