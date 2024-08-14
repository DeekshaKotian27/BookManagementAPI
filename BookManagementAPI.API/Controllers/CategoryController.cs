using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using BookManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAllCategories();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category=await _categoryService.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var category = new Category()
            {
                Name = categoryDTO.CategoryName,
            };
            var categoryData = await _categoryService.CreateCategory(category);
            return Ok(categoryData);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByID(int id,CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                return BadRequest();
            }
            var category = new Category()
            {
                Name = categoryDTO.CategoryName,
            };
            var categoryData = await _categoryService.UpdateCategory(id, category);
            return Ok(categoryData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data=await _categoryService.DeleteCategory(id);
            if (data != -1)
            {
                return Ok("Deleted category successufully");
            }
            else {return BadRequest(); }
            
        }
    }
}
