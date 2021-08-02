using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Models.ViewModels;
using SpringFoodBackend.Models.ViewModels.Requests;
using SpringFoodBackend.Repos;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        ISpringFoodService _service;
        public UserController(ISpringFoodService service)
        {
            _service =service;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserViewModel toAdd)
        {
            _service.RegisterUser(toAdd);
            return Ok(true);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRe)
        {
            LoginResponse response = _service.Login(loginRe);
            
            
            return Ok(response); 
        }
        
        [HttpGet]
        public IActionResult GetAllUsers()
        {
           List<User> userList = _service.GetAllUsers();
            return Ok(userList.Select(u => new UserView(u))); 
        }
       
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _service.DeleteUser(id);
            return this.Accepted();
        }


    }

} 
 
