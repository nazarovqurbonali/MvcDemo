namespace Domain.DTOs.MarketDTOs;

public class UpdateMarketDto
{
    public int Id { get; set; }
    public required string MarketName { get; set; } = " ";
    public string? Description { get; set; }
    public required string Address { get; set; } = " ";
}