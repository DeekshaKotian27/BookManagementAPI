using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Books> CreateBooksAsync(Books book)
        {
            return await _bookRepository.CreateBooksAsync(book);
        }

        public async Task<int> DeleteBooksAsync(int id)
        {
            return await _bookRepository.DeleteBooksAsync(id);
        }

        public async Task<ResponseBooksDTO> GetBookByIdAsync(int id)
        {
            var bookByID = await _bookRepository.GetBookByIdAsync(id);
            if (bookByID == null)
            {
                return null;
            }
            ResponseBooksDTO data = GetBookData(bookByID);
            return data;
        }

        private static ResponseBooksDTO GetBookData(Books bookByID)
        {
            var model = new ResponseBooksDTO();
            model.BookID = bookByID.BookId;
            model.Title = bookByID.Title;
            if (bookByID.Category != null)
            {
                 model.CategoryId = bookByID.Category.CategoryId;
                 model.CategoryName = bookByID.Category.Name;
            }
            if(bookByID.Author != null)
            {
                 model.AuthorId = bookByID.Author.AuthorId;
                 model.AuthorName = bookByID?.Author?.FirstName + " " + bookByID?.Author?.LastName;
            }
            if(bookByID?.Publisher!= null)
            {
                model.PublisherId = bookByID.Publisher.PublisherId;
                model.PublisherName = bookByID.Publisher.PublisherName;
            }
            return model;
        }

        public async Task<List<ResponseBooksDTO>> GetBooksAsync()
        {
            var getAllBooks= await _bookRepository.GetBooksAsync();
            if(getAllBooks == null)
            {
                return null;
            }
            var books = new List<ResponseBooksDTO>();
            foreach (var book in getAllBooks)
            {
                var bookData = GetBookData(book);
                books.Add(bookData);
            }
            return books;
        }

        public async Task<Books> UpdateBooksAsync(int id, Books book)
        {
            return await _bookRepository.UpdateBooksAsync(id, book);
        }
    }
}
