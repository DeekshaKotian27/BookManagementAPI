using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public interface IUserService
    {
        Task<Users> Validate(string emailID);
        Task<Users> Register(string role,UsersRegisterDTO usersRegisterDTO);
        Task<int> UpdateUserName(int id, string userName);
        Task<int> UpdateUserEmail(int id, string email);
        Task<int> UpdateUserPassword(int id, string currentPassword,string newPassword);
        Task<List<Users>> GetAllUsers();
        Task<int> RemoveUser(int id);
        Task<int> AdminUpdateUser(int id, UsersRegisterDTO user);
    }
}
