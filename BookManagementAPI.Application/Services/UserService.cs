using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> AdminUpdateUser(int id, UsersRegisterDTO usersRegisterDTO)
        {
            var userData = new Users()
            {
                UserName = usersRegisterDTO.UserName,
                EmailID = usersRegisterDTO.EmailID,
                Password = usersRegisterDTO.Password,
                Role = usersRegisterDTO.Role,
            };
            return await _userRepository.AdminUpdateUser(id, userData);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<Users> Register(string role, UsersRegisterDTO usersRegisterDTO)
        {
            var userData = new Users()
            {
                UserName = usersRegisterDTO.UserName,
                EmailID = usersRegisterDTO.EmailID,
                Password = usersRegisterDTO.Password,
                Role=role,
            };
            var user=await _userRepository.Register(userData);
            if (user != null)
            {
              return user;
            }
            return null;
        }

        public async Task<int> RemoveUser(int id)
        {
            return await _userRepository.RemoveUser(id);
        }

        public async Task<int> UpdateUserEmail(int id, string email)
        {
            return await _userRepository.UpdateUserEmail(id, email);
        }

        public async Task<int> UpdateUserName(int id, string userName)
        {
            return await (_userRepository.UpdateUserName(id, userName));
        }

        public async Task<int> UpdateUserPassword(int id, string currentPassword, string newPassword)
        {
            return await _userRepository.UpdateUserPassword(id, currentPassword,newPassword);
        }

        public async Task<Users> Validate(string emailID)
        {
            return await _userRepository.Validate(emailID);
        }
    }
}
