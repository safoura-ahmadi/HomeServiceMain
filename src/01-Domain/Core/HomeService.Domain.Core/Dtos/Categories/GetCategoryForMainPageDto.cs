namespace HomeService.Domain.Core.Dtos.Categories;

public class GetCategoryForMainPageDto
{
    public int Id { get; set; }
    public string Tilte { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public List<GetSubCategoryDto> SubCategories { get; set; } = [];
}
