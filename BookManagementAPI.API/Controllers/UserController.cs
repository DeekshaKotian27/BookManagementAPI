using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        [HttpPost("{validate}")]
        public async Task<IActionResult> Validate(UsersDTO usersDTO)
        {
            if (usersDTO == null)
            {
                return BadRequest();
            }
            var data = await _userService.Validate(usersDTO.EmailID);
            if (data != null)
            {
                return Ok(data);
            }
            return Unauthorized();
        }
    }
}
