using Domain.DTOs.CategoryDTOs;
using Infrastructure.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain.Filters;

namespace MvcApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public  IActionResult CreateCategoryAsync( )
        {
            return View();
        }

        public IActionResult UpdateCategoryAsync()
        {
            return View();
        }
        public  IActionResult GetCategoriesAsync( )
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync(CategoryFilter filter)
        {
            try
            {
                var response = await _categoryService.GetCategoriesAsync(filter);
                if (response.StatusCode != (int)HttpStatusCode.OK)
                {
                    return StatusCode((int)response.StatusCode!, response.Errors);
                }

                var categories = response.Data;
                return View(categories);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var response = await _categoryService.GetCategoryByIdAsync(categoryId);
                return Ok(response.Data); 
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync( CreateCategoryDto category)
        {
            try
            {
                var response = await _categoryService.CreateCategoryAsync(category);
                return RedirectToAction("GetCategories"); 
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategoryAsync( UpdateCategoryDto category)
        {
            try
            {
                var response = await _categoryService.UpdateCategoryAsync(category);
                return RedirectToAction("GetCategories"); 
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCategoryAsync(int categoryId)
        {
            try
            {
                var response = await _categoryService.RemoveCategoryAsync(categoryId);
                return RedirectToAction("GetCategories");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
