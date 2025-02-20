namespace HomeService.Domain.Core.Dtos.Categories;

public class GetSubCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}
