namespace Domain.DTOs.ProductDTOs;

public class UpdateProductDto
{
    public int Id { get; set; }
    public required string ProductName { get; set; } = " ";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int MarketId { get; set; }
    public int CategoryId { get; set; }
}