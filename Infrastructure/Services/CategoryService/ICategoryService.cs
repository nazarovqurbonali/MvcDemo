using Domain.DTOs.CategoryDTOs;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    Task<PagedResponse<List<GetCategoryDto>>> GetCategoriesAsync(CategoryFilter filter);
    Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId);
    Task<Response<string>> CreateCategoryAsync(CreateCategoryDto category);
    Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto category);
    Task<Response<bool>> RemoveCategoryAsync(int categoryId);

}