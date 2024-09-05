using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementAPI.Application.Services
{
    public interface ICategoryService
    {
        Task<List<ResponseMultipleCategoryDTO>> GetAllCategories();
        Task<ResponseMultipleCategoryDTO?> GetCategoryById(int id); 
        Task<int> CreateCategory(CategoryDTO category);
        Task<int> UpdateCategory(int id, CategoryDTO category);
        Task<int> DeleteCategory(int id);
    }
}
