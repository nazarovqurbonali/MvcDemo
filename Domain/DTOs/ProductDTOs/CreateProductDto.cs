namespace Domain.DTOs.ProductDTOs;

public class CreateProductDto
{
    public required string ProductName { get; set; } = " ";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int MarketId { get; set; }
    public int CategoryId { get; set; }
}