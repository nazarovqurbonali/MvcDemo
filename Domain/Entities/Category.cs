namespace Domain.Entities;

public class Category:BaseEntity
{
    public string CategoryName { get; set; } = null!;
    public List<Product>? Products { get; set; }
}