using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foodea.Data;
using Foodea.Models;
using Foodea.Services;

namespace Foodea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userService;

        public UsersController(IUserServices userService)
        {
            this.userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUser()
        {
            try {
                var users = this.userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("Id/{id}")]
        public IActionResult GetUserById(Guid id)
        {
            try {
                var user = this.userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public IActionResult GetUserByEmail(string email) {
            try {
                var user = this.userService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
