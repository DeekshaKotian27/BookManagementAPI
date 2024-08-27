using BookManagementAPI.API.Helpers;
using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsersRegisterDTO usersDTO)
        {
            if (usersDTO == null)
            {
                return BadRequest();
            }
            var data = await _userService.Register(usersDTO);
            if(data!=null)
            {
                return Ok(data);
            }
            return BadRequest();
        }
        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody]UsersDTO usersDTO)
        {
            if (usersDTO == null)
            {
                return BadRequest();
            }
            var data = await _userService.Validate(usersDTO.EmailID);
            if (data != null)
            {
                var jwtSettings = _configuration.GetSection("JWT");
                var token = TokenGenerator.GetToken(data, jwtSettings);
                return Ok(new {Data=data,Token=token});
            }
            return Unauthorized();
        }
        
    }
}
