using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<int> CreateAsync(Author author);
        Task<int> UpdateAsync(int id, Author author);
        Task<int> DeleteAsync(int id);
    }
}
