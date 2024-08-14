using BookManagementAPI.Domain.Entities;
using BookManagementAPI.Domain.Interface;
using BookManagementAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthorDBContext _authorDBContext;
        public UserRepository(AuthorDBContext authorDBContext)
        {
            _authorDBContext = authorDBContext;
        }
        public async Task<Users> Register(Users user)
        {
            //var user = new Users()
            //{
            //    EmailID = emailID,
            //    Password = password,
            //    Role = "admin",
            //    UserName = userName,
            //    Image= image
            //};
            user.Role = "admin";
            try
            {
                await _authorDBContext.Users.AddAsync(user);
                await _authorDBContext.SaveChangesAsync();
                return user;
            }
            catch {
                return null;
            }
        }

        public async Task<Users> Validate(string emailID)
        {
            var user=await _authorDBContext.Users.SingleOrDefaultAsync(user=>user.EmailID == emailID);
            if (user != null)
            {
                return user;
            }
            else return null;
        }
    }
}
