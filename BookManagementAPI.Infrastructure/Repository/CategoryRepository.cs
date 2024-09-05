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
        public async Task<int> CreateCategory(Category category)
        {
            var dulpicateData= await _authorDBContext.Category.SingleOrDefaultAsync(c=>c.Name == category.Name);
            if (dulpicateData != null)
            {
                return -1;
            }
            await _authorDBContext.Category.AddAsync(category); 
            var result=await _authorDBContext.SaveChangesAsync();
            if (result > 0) {
                return 1;
            }
            return -2;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var data= await _authorDBContext.Category.FindAsync(id);
            if (data != null)
            {
                _authorDBContext.Category.Remove(data);
                var result= await _authorDBContext.SaveChangesAsync();
                if(result > 0) { return 1; }
                return -2;
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

        public async Task<Category?> GetCategoryById(int id)
        {
            var category = await _authorDBContext.Category
                .Include(b => b.Books).ThenInclude(a => a.Author)
                .Include(b => b.Books).ThenInclude(p => p.Publisher)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        public async Task<int> UpdateCategory(int id, Category category)
        {
            var categoryData= await _authorDBContext.Category.FindAsync(id);
            if (categoryData == null)
            {
                return -1;
            }
            categoryData.Name = category.Name;
            _authorDBContext.Category.Update(categoryData);
            var result=await _authorDBContext.SaveChangesAsync();
            if (result > 0)
            {
                return 1;
            }
            return -2;
        }
    }
}
