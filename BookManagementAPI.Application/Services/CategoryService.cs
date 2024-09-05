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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.CategoryName,
            };
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<List<ResponseMultipleCategoryDTO>> GetAllCategories()
        {
            var getAllCategories= await _categoryRepository.GetAllCategories();
            if (getAllCategories == null)
            {
                return null;
            }
            var data=new List<ResponseMultipleCategoryDTO>();
            foreach (var category in getAllCategories)
            {
                ResponseMultipleCategoryDTO categories = GetCategories(category);
                data.Add(categories);
            }
            return data;
        }

        private static ResponseMultipleCategoryDTO GetCategories(Category category)
        {
            var categories = new ResponseMultipleCategoryDTO();
            categories.CategoryID = category.CategoryId;
            categories.CategoryName = category.Name;
            var booksDataList = new List<ResponseCategoryBookDTO>();
            foreach (var book in category.Books)
            {
                var bookData = new ResponseCategoryBookDTO();
                bookData.BookID = book.BookId;
                bookData.Title = book?.Title;
                bookData.AuthorName = book?.Author?.FirstName + " " + book?.Author?.LastName;
                bookData.PublisherName = book?.Publisher?.PublisherName;
                booksDataList.Add(bookData);
            }
            categories.Books = booksDataList;
            return categories;
        }

        public async Task<ResponseMultipleCategoryDTO?> GetCategoryById(int id)
        {
            var getCategoryDataByID= await _categoryRepository.GetCategoryById(id);
            if(getCategoryDataByID == null)
            {
                return null;
            }
            var data = GetCategories(getCategoryDataByID);
            return data;
        }

        public async Task<int> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.CategoryName,
            };
            return await _categoryRepository.UpdateCategory(id, category);
        }

        public Task<int> DeleteCategory(int id)
        {
            return _categoryRepository.DeleteCategory(id);
        }
    }
}
