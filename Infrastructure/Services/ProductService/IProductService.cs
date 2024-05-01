using Domain.DTOs.ProductDTOs;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<PagedResponse<List<GetProductDto>>> GetProductsAsync(ProductFilter filter);
    Task<Response<GetProductDto>> GetProductByIdAsync(int productId);
    Task<Response<string>> CreateProductAsync(CreateProductDto product);
    Task<Response<string>> UpdateProductAsync(UpdateProductDto product);
    Task<Response<bool>> RemoveProductAsync(int productId);
}