using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using BookManagementAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Infrastructure.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly AuthorDBContext _authorDbContext;
        public AuthorRepository(AuthorDBContext authorDBContext )
        {
            _authorDbContext = authorDBContext;
        }
        public async Task<int> CreateAsync(Author author)
        {
            var duplicateData=await _authorDbContext.Authors.SingleOrDefaultAsync(a => a.FirstName==author.FirstName);
            if (duplicateData != null) 
            {
                return -1;
            }
            await _authorDbContext.Authors.AddAsync(author);
            var result=await _authorDbContext.SaveChangesAsync();
            if (result > 0) 
            {
                return 1;
            }
            return -2;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var author = await _authorDbContext.Authors.FindAsync(id);
            if (author != null)
            {
                _authorDbContext.Authors.Remove(author);
                var result= await _authorDbContext.SaveChangesAsync();
                if (result > 0) { return 1; }
                else { return -2; }
            }
            return -1;
        }

        public Task<List<Author>> GetAllAsync()
        {
            return _authorDbContext.Authors
                .Include(a => a.Books).ThenInclude(c=>c.Category)
                .Include(a=>a.Books).ThenInclude(p=>p.Publisher)
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            var author = await _authorDbContext.Authors
                        .Include(a => a.Books).ThenInclude(b => b.Publisher)
                        .Include(a => a.Books).ThenInclude(b => b.Category) 
                        .FirstOrDefaultAsync(a => a.AuthorId == id);
            if(author == null)
            {
                return null;
            }
            return author;
        }

        public async Task<int> UpdateAsync(int id, Author author)
        {
            var authorData = await _authorDbContext.Authors.FindAsync(id);
            if(authorData == null)
            {
                return -1;
            }
            authorData.FirstName = author.FirstName;
            authorData.LastName=author.LastName;
            _authorDbContext.Authors.Update(authorData);
            var result=await _authorDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return 1;
            }
            return -2;
        }
    }
}
