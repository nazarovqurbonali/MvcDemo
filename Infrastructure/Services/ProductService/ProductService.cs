using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.ProductDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProductService;

public class ProductService : IProductService
{
    #region ctor

    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    #endregion

    #region GetProductsAsync

    public async Task<PagedResponse<List<GetProductDto>>> GetProductsAsync(ProductFilter filter)
    {
        try
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter.ProductName))
                products = products.Where(x => x.ProductName.ToLower().Contains(filter.ProductName.ToLower()));
            var result = await products.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await products.CountAsync();

            var response = _mapper.Map<List<GetProductDto>>(result);
            return new PagedResponse<List<GetProductDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetProductDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region GetProductByIdAsync

 

    public async Task<Response<GetProductDto>> GetProductByIdAsync(int productId)
    {
        try
        {
            var existing = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (existing == null) return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not found");
            var product = _mapper.Map<GetProductDto>(existing);
            return new Response<GetProductDto>(product);
        }
        catch (Exception e)
        {
            return new Response<GetProductDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateProductAsync

    public async Task<Response<string>> CreateProductAsync(CreateProductDto product)
    {
        try
        {
            var existing = await _context.Products.AnyAsync(x => x.ProductName == product.ProductName);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Product already exists");
            var newProduct = _mapper.Map<Product>(product);
            await _context.Products.AddAsync(newProduct);
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

    #region UpdateProductAsync

    public async Task<Response<string>> UpdateProductAsync(UpdateProductDto product)
    {
        try
        {
            var existing = await _context.Products.AnyAsync(x => x.Id == product.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Product not found");
            var newProduct = _mapper.Map<Product>(product);
            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();
            return new Response<string>("Product successfully updated");
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

    #region RemoveProductAsync

    public async Task<Response<bool>> RemoveProductAsync(int productId)
    {
        try
        {
            var existing = await _context.Products.Where(x => x.Id == productId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Product not found")
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