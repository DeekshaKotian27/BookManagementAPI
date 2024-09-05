using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Application.Services
{
    public interface IBookService
    {

        Task<List<ResponseBooksDTO>> GetBooksAsync();
        Task<ResponseBooksDTO?> GetBookByIdAsync(int id);
        Task<int> CreateBooksAsync(BooksDTO book);
        Task<int> UpdateBooksAsync(int id,BooksDTO book);
        Task<int> DeleteBooksAsync(int id);
    }
}
