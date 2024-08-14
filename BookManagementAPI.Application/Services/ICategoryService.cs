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
        Task<ResponseMultipleCategoryDTO> GetCategoryById(int id); 
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(int id, Category category);
        Task<int> DeleteCategory(int id);
    }
}
