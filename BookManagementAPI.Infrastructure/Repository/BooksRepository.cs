using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using BookManagementAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Infrastructure.Repository
{
    public class BooksRepository : IBookRepository
    {
        public readonly AuthorDBContext _authorDbContext;
        public BooksRepository(AuthorDBContext authorDBContext)
        {
            _authorDbContext = authorDBContext;
        }
        public async Task<Books> CreateBooksAsync(Books book)
        {
            var isAuthor= await _authorDbContext.Authors.AnyAsync(a=>a.AuthorId==book.AuthorId);
            var isPublisher = await _authorDbContext.Publisher.AnyAsync(p => p.PublisherId == book.PublisherId);
            var isCategory = await _authorDbContext.Category.AnyAsync(c => c.CategoryId == book.CategoryID);
            await _authorDbContext.Books.AddAsync(book);
            await _authorDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<int> DeleteBooksAsync(int id)
        {
            var book = await _authorDbContext.Books.FindAsync(id);
            if (book != null)
            {
                _authorDbContext.Books.Remove(book);
                return await _authorDbContext.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<Books> GetBookByIdAsync(int id)
        {
            var book = await _authorDbContext.Books
                .Include(c => c.Category)
                .Include(p => p.Publisher)
                .Include(a=>a.Author)
                .FirstOrDefaultAsync(a=>a.BookId==id);
            return book;
        }

        public Task<List<Books>> GetBooksAsync()
        {
            return _authorDbContext.Books
                .Include(c=>c.Category)
                .Include(p=>p.Publisher)
                .Include(a=>a.Author)
                .ToListAsync();
        }

        public async Task<Books> UpdateBooksAsync(int id,Books book)
        {
            var bookbyid = await _authorDbContext.Books.FindAsync(id);
            if(bookbyid == null) 
            {
                return null;
            }
            bookbyid.Title=book.Title;
            bookbyid.AuthorId=book.AuthorId;
            bookbyid.PublisherId=book.PublisherId;
            bookbyid.CategoryID = book.CategoryID;
            _authorDbContext.Books.Update(bookbyid);
            await _authorDbContext.SaveChangesAsync();
            return bookbyid;
        }
    }
}
