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
    public class BooksController : ControllerBase
    {
        public readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks() 
        {
            var books= await _bookService.GetBooksAsync();
            if (books == null)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooks(BooksDTO booksDTO)
        {
            if(booksDTO == null)
            {
                return BadRequest();
            }
            else
            {
                var book = await _bookService.CreateBooksAsync(booksDTO);
                if (book == -1) {
                    return Conflict("The resource already exists.");
                }
                if (book == -2) 
                {
                    return StatusCode(500, "An error occurred while processing your request.");
                }
                return StatusCode(201, "Book created successfully");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByID(int id)
        {
            var book=await _bookService.GetBookByIdAsync(id);
            if(book == null){
                return NotFound($"There is no data with ID {id}");
            }
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BooksDTO booksDTO)
        {
            if(booksDTO == null)
            {
                return BadRequest();
            }
            var updatebook = await _bookService.UpdateBooksAsync(id, booksDTO);
            if (updatebook == 1)
            {
                return Ok();
            }
            else if (updatebook == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var data = await _bookService.DeleteBooksAsync(id);
            if(data == 1)
            {
                return Ok("deleted succesfully book");
            }
            else if(data==-1) 
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
