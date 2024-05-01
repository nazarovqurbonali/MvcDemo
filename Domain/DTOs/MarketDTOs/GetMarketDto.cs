namespace Domain.DTOs.MarketDTOs;

public class GetMarketDto
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public required string MarketName { get; set; } = " ";
    public string? Description { get; set; }
    public required string Address { get; set; } = " ";
}