namespace Domain.Filters;

public class CategoryFilter:PaginationFilter
{
    public string? CategoryName { get; set; }
}