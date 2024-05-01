using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.MarketDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MarketService;

public class MarketService : IMarketService
{
    #region ctor

    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public MarketService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    #endregion

    #region GetMarketsAsync

    public async Task<PagedResponse<List<GetMarketDto>>> GetMarketsAsync(MarketFilter filter)
    {
        try
        {
            var markets = _context.Markets.AsQueryable();
            if (!string.IsNullOrEmpty(filter.MarketName))
                markets = markets.Where(x => x.MarketName.ToLower().Contains(filter.MarketName.ToLower()));
            var result = await markets.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var total = await markets.CountAsync();

            var response = _mapper.Map<List<GetMarketDto>>(result);
            return new PagedResponse<List<GetMarketDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMarketDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region GetMarketByIdAsync

    public async Task<Response<GetMarketDto>> GetMarketByIdAsync(int marketId)
    {
        try
        {
            var existing = await _context.Markets.FirstOrDefaultAsync(x => x.Id == marketId);
            if (existing == null) return new Response<GetMarketDto>(HttpStatusCode.BadRequest, "Market not found");
            var market = _mapper.Map<GetMarketDto>(existing);
            return new Response<GetMarketDto>(market);
        }
        catch (Exception e)
        {
            return new Response<GetMarketDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateMarketAsync

    public async Task<Response<string>> CreateMarketAsync(CreateMarketDto market)
    {
        try
        {
            var existing = await _context.Markets.AnyAsync(x => x.MarketName == market.MarketName);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Market already exists");
            var newMarket = _mapper.Map<Market>(market);
            await _context.Markets.AddAsync(newMarket);
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

    #region UpdateMarketAsync

    public async Task<Response<string>> UpdateMarketAsync(UpdateMarketDto market)
    {
        try
        {
            var existing = await _context.Markets.AnyAsync(x => x.Id == market.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Market not found");
            var newMarket = _mapper.Map<Market>(market);
            _context.Markets.Update(newMarket);
            await _context.SaveChangesAsync();
            return new Response<string>("Market successfully updated");
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

    #region RemoveMarketAsync

    public async Task<Response<bool>> RemoveMarketAsync(int marketId)
    {
        try
        {
            var existing = await _context.Markets.Where(x => x.Id == marketId).ExecuteDeleteAsync();
            return existing == 0
                ? new Response<bool>(HttpStatusCode.BadRequest, "Market not found")
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