using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryAPI.Models;
using LibraryAPI.Services;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase, IUsersController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Post(LoginRequest request)
        {
            var userService = new UserService();

            if(!userService.Login(request.Username, request.Password, out string token, out string errMsg))
            {
                return BadRequest(errMsg);
            }

            return Ok(token);
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            return Ok("admin only");
        }

        [HttpGet("user")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetUser()
        {
            return Ok("admin and user");
        }

        [HttpGet("any")]
        [AllowAnonymous]
        public IActionResult GetAny()
        {
            return Ok("anyone");
        }
    }
}