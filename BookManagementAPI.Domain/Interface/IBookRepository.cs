using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IBookRepository
    {
        Task<List<Books>> GetBooksAsync();
        Task<Books?> GetBookByIdAsync(int id);
        Task<int> CreateBooksAsync(Books book);
        Task<int> UpdateBooksAsync(int id, Books book);
        Task<int> DeleteBooksAsync(int id);
    }
}
