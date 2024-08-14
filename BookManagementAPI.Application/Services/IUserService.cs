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
        Task<Users> Register(UsersRegisterDTO usersRegisterDTO);
    }
}
