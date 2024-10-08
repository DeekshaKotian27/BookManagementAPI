﻿using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public interface IAuthorService
    {
        Task<List<ResponseAuthorDTO>> GetAllAsync();
        Task<ResponseAuthorDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(AuthorDTO author);
        Task<int> UpdateAsync(int id, AuthorDTO author);
        Task<int> DeleteAsync(int id);
    }
}
