using BookManagementAPI.Application.DTOs;
using BookManagementAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            if (category == null)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category=await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound($"There is no data with ID {id}");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }
            var categoryData = await _categoryService.CreateCategory(categoryDTO);
            if (categoryData == -1)
            {
                return Conflict("The resource already exists.");
            }
            if (categoryData == -2)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return StatusCode(201, "Category created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByID(int id,CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                return BadRequest();
            }
            var categoryData = await _categoryService.UpdateCategory(id, categoryDTO);
            if (categoryData == 1)
            {
                return Ok();
            }
            else if (categoryData == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data=await _categoryService.DeleteCategory(id);
            if (data == 1)
            {
                return Ok("deleted succesfully category");
            }
            else if (data == -1)
            {
                return NotFound("The requested resource was not found.");
            }
            else
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
    }
}
