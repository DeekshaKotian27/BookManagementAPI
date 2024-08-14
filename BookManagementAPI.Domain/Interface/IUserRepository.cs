using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IUserRepository
    {
        Task<Users> Validate(string emailID);
        Task<Users> Register(Users user);
    }
}
