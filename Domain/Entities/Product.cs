using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product:BaseEntity
{
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int MarketId { get; set; }
    public Market? Market { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}