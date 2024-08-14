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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        [HttpGet("{id}")]
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
        public async Task<IActionResult> Create(AuthorDTO authorDTO)
        {
            if (authorDTO == null)
            {
                return BadRequest();
            }
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
            };
            var createBlog = await _authorService.CreateAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = createBlog.AuthorId }, createBlog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,AuthorDTO authorDTO) 
        { 
            if (authorDTO == null)
            {
                return BadRequest();
            }
            var author = new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
            };
            var authorData = await _authorService.UpdateAsync(id, author);
            if (authorData != -1)
            {
                return Ok("updated succesfully {authorData}");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var author= await _authorService.DeleteAsync(id);
            if (author != -1)
            {
                return Ok("deleted succesfully {authorData}");
            }
            else { return BadRequest(); }
        }
    }
}
