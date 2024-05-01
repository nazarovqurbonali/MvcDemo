using Domain.DTOs.MarketDTOs;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MarketService;

public interface IMarketService
{
    Task<PagedResponse<List<GetMarketDto>>> GetMarketsAsync(MarketFilter filter);
    Task<Response<GetMarketDto>> GetMarketByIdAsync(int marketId);
    Task<Response<string>> CreateMarketAsync(CreateMarketDto market);
    Task<Response<string>> UpdateMarketAsync(UpdateMarketDto market);
    Task<Response<bool>> RemoveMarketAsync(int marketId);
}