namespace Domain.Entities;

public class Market:BaseEntity
{
    public string MarketName { get; set; } = null!;
    public string? Description { get; set; } 
    public string Address { get; set; } = null!;
    public List<Product>? Products { get; set; }
}