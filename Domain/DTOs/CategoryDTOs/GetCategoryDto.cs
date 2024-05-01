namespace Domain.DTOs.CategoryDTOs;

public class GetCategoryDto
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public required string CategoryName { get; set; } = " ";
}