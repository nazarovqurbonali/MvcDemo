using AutoMapper;
using Domain.DTOs.CategoryDTOs;
using Domain.DTOs.MarketDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Market, GetMarketDto>().ReverseMap();
        CreateMap<Market, CreateMarketDto>().ReverseMap();
        CreateMap<Market, UpdateMarketDto>().ReverseMap();

        CreateMap<Product, GetProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();

        CreateMap<Category, GetCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
    }
}