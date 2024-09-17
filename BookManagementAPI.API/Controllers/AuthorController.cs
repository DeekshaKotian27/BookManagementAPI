using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("GetAllAuthor")]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            if (authors == null)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(authors);
        }
        
        [HttpGet]
        [Route("GetAuthorByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound($"There is no data with ID {id}");
            }
            return Ok(author);
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> Create(AuthorDTO authorDTO)
        {
            if (authorDTO == null)
            {
                return BadRequest();
            }
            var data = await _authorService.CreateAsync(authorDTO);
            if (data == -2)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            if (data == -1)
            {
                return Conflict("The resource already exists.");
            }
            return StatusCode(201, "Author created successfully");
        }

        [HttpPut]
        [Route("UpdateAuthor/{id}")]
        public async Task<IActionResult> Update(int id,AuthorDTO authorDTO) 
        { 
            if (authorDTO == null)
            {
                return BadRequest();
            }
            var authorData = await _authorService.UpdateAsync(id, authorDTO);
            if (authorData == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else if(authorData == -2)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok("updated succesfully");
        }

        [HttpDelete]
        [Route("DeleteAuthor/{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var author= await _authorService.DeleteAsync(id);
            if (author == 1)
            {
                return Ok("deleted succesfully");
            }
            else if (author == -1)
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
