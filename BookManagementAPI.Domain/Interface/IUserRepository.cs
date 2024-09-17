using BookManagementAPI.Domain.Entities;

namespace BookManagementAPI.Domain.Interface
{
    public interface IUserRepository
    {
        Task<Users> Validate(string emailID);
        Task<Users> Register(Users user);
        Task<int> UpdateUserName(int id, string userName);
        Task<int> UpdateUserEmail(int id, string email);
        Task<int> UpdateUserPassword(int id, string currentPassword, string newPassword);
        Task<List<Users>> GetAllUsers();
        Task<int> RemoveUser(int id);
        Task<int> AdminUpdateUser(int id, Users userData);
    }
}
