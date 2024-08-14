using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using System;
using System.Collections.Generic;
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
        public async Task<Users> Register(UsersRegisterDTO usersRegisterDTO)
        {
            var userData = new Users()
            {
                UserName = usersRegisterDTO.UserName,
                EmailID = usersRegisterDTO.EmailID,
                Password = usersRegisterDTO.Password,
            };
            var user=await _userRepository.Register(userData);
            if (user != null)
            {
              return user;
            }
            return null;
        }

        public async Task<Users> Validate(string emailID)
        {
            return await _userRepository.Validate(emailID);
        }
    }
}
