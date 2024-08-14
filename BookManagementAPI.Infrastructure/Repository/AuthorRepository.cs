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
        public async Task<Author> CreateAsync(Author author)
        {
            await _authorDbContext.Authors.AddAsync(author);
            await _authorDbContext.SaveChangesAsync();
            return author;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var author = await _authorDbContext.Authors.FindAsync(id);
            if (author != null)
            {
                _authorDbContext.Authors.Remove(author);
                return await _authorDbContext.SaveChangesAsync();
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

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _authorDbContext.Authors
                        .Include(a => a.Books).ThenInclude(b => b.Publisher)
                        .Include(a => a.Books).ThenInclude(b => b.Category) 
                        .FirstOrDefaultAsync(a => a.AuthorId == id);
            if(author == null)
            {
                throw new Exception();
            }
            return author;
        }

        public async Task<int> UpdateAsync(int id, Author author)
        {
            var authorData = await _authorDbContext.Authors.FindAsync(id);
            if(authorData != null)
            {
                authorData.FirstName = author.FirstName;
                authorData.LastName=author.LastName;
                _authorDbContext.Authors.Update(authorData);
                await _authorDbContext.SaveChangesAsync();
                return authorData.AuthorId;
            }
            return -1;
        }
    }
}
