namespace Domain.DTOs.CategoryDTOs;

public class UpdateCategoryDto
{
    public int Id { get; set; }
    public required string CategoryName { get; set; } = " ";
}