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
        public async Task<int> CreateBooksAsync(Books book)
        {
            var duplicateData = await _authorDbContext.Books.SingleOrDefaultAsync(b=>b.Title == book.Title);
            if (duplicateData!=null) 
            {
                return -1;
            }
            await _authorDbContext.Books.AddAsync(book);
            var result=await _authorDbContext.SaveChangesAsync();
            if(result>0 && book.BookId>0)
            {
                return 1;
            }
            return -2;
        }

        public async Task<int> DeleteBooksAsync(int id)
        {
            var book = await _authorDbContext.Books.FindAsync(id);
            if (book == null)
            {
                return -1;
            }
            _authorDbContext.Books.Remove(book);
            var result = await _authorDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return 1;
            }
            return -2;
        }

        public async Task<Books?> GetBookByIdAsync(int id)
        {
            var book = await _authorDbContext.Books
                .Include(c => c.Category)
                .Include(p => p.Publisher)
                .Include(a=>a.Author)
                .FirstOrDefaultAsync(a=>a.BookId==id);
            if (book == null)
            {
                return null;
            }
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

        public async Task<int> UpdateBooksAsync(int id,Books book)
        {
            var bookbyid = await _authorDbContext.Books.FindAsync(id);
            if(bookbyid == null) 
            {
                return -1;
            }
            bookbyid.Title=book.Title;
            bookbyid.AuthorId=book.AuthorId;
            bookbyid.PublisherId=book.PublisherId;
            bookbyid.CategoryID = book.CategoryID;
            _authorDbContext.Books.Update(bookbyid);
            var result=await _authorDbContext.SaveChangesAsync();
            if (result > 0) 
            {
                return 1;
            }
            return -2;
        }
    }
}
