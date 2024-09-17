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

        public async Task<int> AdminUpdateUser(int id, Users userData)
        {
            var user = await _authorDBContext.Users.FindAsync(id);
            if (user == null)
            {
                return -1;
            }
            user.UserName = userData.UserName;
            user.EmailID = userData.EmailID;
            user.Password = userData.Password;
            user.Role = userData.Role;
            _authorDBContext.Users.Update(user);
            var result = await _authorDBContext.SaveChangesAsync();
            if (result > 0) { return 1; }
            return -2;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var users= await _authorDBContext.Users.ToListAsync();
            if(users==null)
            {
                return null;
            }
            return users;
        }

        public async Task<Users> Register(Users user)
        {
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

        public async Task<int> RemoveUser(int id)
        {
            var user = await _authorDBContext.Users.FindAsync(id);
            if (user != null)
            {
                _authorDBContext.Users.Remove(user);
                var result=await _authorDBContext.SaveChangesAsync();
                if (result > 0) { return 1; }
                return -2;
            }
            return -1;
        }

        public async Task<int> UpdateUserEmail(int id, string email)
        {
            var user= await _authorDBContext.Users.FindAsync(id);
            if (user == null) {
                return -1;
            }
            user.EmailID = email;
            _authorDBContext.Users.Update(user);
            var result = await _authorDBContext.SaveChangesAsync();
            if (result > 0) { return 1; }
            return -2;
        }

        public async Task<int> UpdateUserName(int id, string userName)
        {
            var user = await _authorDBContext.Users.FindAsync(id);
            if (user == null)
            {
                return -1;
            }
            user.UserName = userName;
            _authorDBContext.Users.Update(user);
            var result = await _authorDBContext.SaveChangesAsync();
            if (result > 0) { return 1; }
            return -2;
        }

        public async Task<int> UpdateUserPassword(int id, string currentPassword, string newPassword)
        {
            var user = await _authorDBContext.Users.FindAsync(id);
            if (user == null)
            {
                return -1;
            }
            if (currentPassword == user.Password)
            {
                user.Password = newPassword;
                _authorDBContext.Users.Update(user);
                var result = await _authorDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    return 1;
                }
                else return -3;
            }
            return -2;//incorrect current password
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
