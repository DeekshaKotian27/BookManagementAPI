using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using BookManagementAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AuthorDBContext _authorDBContext;
        public CategoryRepository( AuthorDBContext authorDBContext)
        {
            _authorDBContext = authorDBContext;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            await _authorDBContext.Category.AddAsync(category); 
            await _authorDBContext.SaveChangesAsync();
            return category;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var data= await _authorDBContext.Category.FindAsync(id);
            if (data != null)
            {
                _authorDBContext.Category.Remove(data);
                return await _authorDBContext.SaveChangesAsync();
            }
            return -1;
        }

        public Task<List<Category>> GetAllCategories()
        {
            return _authorDBContext.Category
                .Include(b=>b.Books).ThenInclude(a=>a.Author)
                .Include(b=>b.Books).ThenInclude(p=>p.Publisher)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _authorDBContext.Category
                .Include(b => b.Books).ThenInclude(a => a.Author)
                .Include(b => b.Books).ThenInclude(p => p.Publisher)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            var categoryData= await _authorDBContext.Category.FindAsync(id);
            if (categoryData == null)
            {
                return null;
            }
            categoryData.Name = category.Name;
            _authorDBContext.Category.Update(categoryData);
            await _authorDBContext.SaveChangesAsync();
            return categoryData;
        }
    }
}
