using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.CategoryDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CategoryService;

public class CategoryService : ICategoryService
{
    #region ctor

    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CategoryService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    #endregion

    #region GetCategoriesAsync

    public async Task<PagedResponse<List<GetCategoryDto>>> GetCategoriesAsync(CategoryFilter filter)
    {
        try
        {
            var categories = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter.CategoryName))
                categories = categories.Where(x => x.CategoryName.ToLower().Contains(filter.CategoryName.ToLower()));
            var result = await categories.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await categories.CountAsync();

            var response = _mapper.Map<List<GetCategoryDto>>(result);
            return new PagedResponse<List<GetCategoryDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCategoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region GetCategoryByIdAsync

    public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId)
    {
        try
        {
            var existing = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (existing == null) return new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not found");
            var category = _mapper.Map<GetCategoryDto>(existing);
            return new Response<GetCategoryDto>(category);
        }
        catch (Exception e)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateCategoryAsync

    public async Task<Response<string>> CreateCategoryAsync(CreateCategoryDto category)
    {
        try
        {
            var existing = await _context.Categories.AnyAsync(x => x.CategoryName == category.CategoryName);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Category already exists");
            var newCategory = _mapper.Map<Category>(category);
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully created ");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region UpdateCategoryAsync

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto category)
    {
        try
        {
            var existing = await _context.Categories.AnyAsync(x => x.Id == category.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Category not found");
            var newCategory = _mapper.Map<Category>(category);
            _context.Categories.Update(newCategory);
            await _context.SaveChangesAsync();
            return new Response<string>("Category successfully updated");
        }
        catch (DbException e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region RemoveCategoryAsync

    public async Task<Response<bool>> RemoveCategoryAsync(int categoryId)
    {
        try
        {
            var existing = await _context.Categories.Where(x => x.Id == categoryId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Category not found")
                : new Response<bool>(true);
        }
        catch (DbException e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion
}