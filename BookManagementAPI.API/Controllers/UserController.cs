using BookManagementAPI.API.Helpers;
using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Register/{role}")]
        public async Task<IActionResult> Register(string role,UsersRegisterDTO usersDTO)
        {
            if (usersDTO == null || string.IsNullOrWhiteSpace(role))
            {
                return BadRequest();
            }
            var data = await _userService.Register(role,usersDTO);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }
        
        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] UsersDTO usersDTO)
        {
            if (usersDTO == null || string.IsNullOrWhiteSpace(usersDTO.EmailID) || string.IsNullOrWhiteSpace(usersDTO.Password))
            {
                return BadRequest();
            }
            var data = await _userService.Validate(usersDTO.EmailID);
            if (data != null)
            {
                if (data.Password == usersDTO.Password)
                {
                    var jwtSettings = _configuration.GetSection("JWT");
                    var token = TokenGenerator.GetToken(data, jwtSettings);
                    return Ok(new { Data = data, Token = token });
                }
                else
                {
                    return StatusCode(403, new { Message = "Incorrect password." });
                }
            }
            return Unauthorized("UserID is not registered");
        }

        [HttpPut()]
        [Authorize]
        [Route("UpdateUserName")]
        public async Task<IActionResult> UpdateUserName([FromBody] UpdateUserNameDTO updateUserNameDTO)
        {
            if (updateUserNameDTO.Id <= 0 && string.IsNullOrWhiteSpace(updateUserNameDTO.UserName))
            {
                return BadRequest();
            }
            var data = await _userService.UpdateUserName(updateUserNameDTO.Id, updateUserNameDTO.UserName);
            if (data == 1)
            {
                return Ok();
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut()]
        [Authorize]
        [Route("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailDTO updateUserEmailDTO)
        {
            if (updateUserEmailDTO.Id <= 0 && string.IsNullOrWhiteSpace(updateUserEmailDTO.EmailID))
            {
                return BadRequest();
            }
            var data = await _userService.UpdateUserEmail(updateUserEmailDTO.Id, updateUserEmailDTO.EmailID);
            if (data == 1)
            {
                return Ok();
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut()]
        [Authorize]
        [Route("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPassword updateUserPassword)
        {
            if (updateUserPassword.Id <= 0 && string.IsNullOrWhiteSpace(updateUserPassword.CurrentPassword) && string.IsNullOrWhiteSpace(updateUserPassword.NewPassword))
            {
                return BadRequest();
            }
            var data = await _userService.UpdateUserPassword(updateUserPassword.Id, updateUserPassword.CurrentPassword, updateUserPassword.NewPassword);
            if (data == 1)
            {
                return Ok();
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else if(data == -2)
            {
                return StatusCode(403, new { Message = "Incorrect password." });
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetAllUsers();
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpDelete("RemoveUser/{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveUser(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var data = await _userService.RemoveUser(id);
            if (data == 1)
            {
                return Ok("deleted succesfully user");
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        
        [HttpPut]
        [Route("AdminUpdateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> AdminUpdateUser(int id, UsersRegisterDTO usersRegisterDTO)
        {
            if (usersRegisterDTO == null && id<1)
            {
                return BadRequest();
            }
            var data = await _userService.AdminUpdateUser(id, usersRegisterDTO);
            if (data == 1)
            {
                return Ok();
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
