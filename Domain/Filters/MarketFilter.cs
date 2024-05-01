namespace Domain.Filters;

public class MarketFilter:PaginationFilter
{
    public string? MarketName { get; set; }
    public string? Address { get; set; }
}