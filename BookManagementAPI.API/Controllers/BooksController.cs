using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                var books = new Books
                {
                    AuthorId = booksDTO.AuthorId,
                    Title = booksDTO.Title,
                    PublisherId = booksDTO.PublisherId,
                    CategoryID=booksDTO.CategoryId,
                };
                var book = await _bookService.CreateBooksAsync(books);
                return Ok(book);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByID(int id)
        {
            var book=await _bookService.GetBookByIdAsync(id);
            if(book == null){
                return BadRequest();
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
            var book = new Books
            {
                Title = booksDTO.Title,
                AuthorId = booksDTO.AuthorId,
                PublisherId = booksDTO.PublisherId,
                CategoryID = booksDTO.CategoryId,
            };
            var updatebook = await _bookService.UpdateBooksAsync(id, book);
            return Ok(updatebook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var data = await _bookService.DeleteBooksAsync(id);
            if(data != -1)
            {
                return Ok("deleted succesfully book");
            }
            else { return BadRequest(); }
        }
    }
}
