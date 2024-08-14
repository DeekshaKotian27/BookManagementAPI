using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Application.Services
{
    public interface IBookService
    {

        Task<List<ResponseBooksDTO>> GetBooksAsync();
        Task<ResponseBooksDTO> GetBookByIdAsync(int id);
        Task<Books> CreateBooksAsync(Books book);
        Task<Books> UpdateBooksAsync(int id,Books book);
        Task<int> DeleteBooksAsync(int id);
    }
}
