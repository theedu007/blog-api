using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models.DTOS;
using BlogAPI.Models.Models;
using BlogAPI.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult LogIn(UserRegisterDto request)
        {
            var response = _authService.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Registrate")]
        public IActionResult RegistrateUser(UserRegisterDto request)
        {
            var newUser = new User { Name = request.Name, UserName = request.Username };
            var respose = _authService.CreateUser(newUser, request.Password);
            if (!respose.Success)
            {
                return BadRequest(respose);
            }
            return Ok(respose);
        }
    }
}
