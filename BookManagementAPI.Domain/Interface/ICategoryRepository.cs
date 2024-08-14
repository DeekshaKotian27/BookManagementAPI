using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(int id, Category category);
        Task<int> DeleteCategory(int id);
    }
}
