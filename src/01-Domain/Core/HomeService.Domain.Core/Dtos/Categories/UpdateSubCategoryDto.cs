namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateSubCategoryDto
{
    public int Id { get; set; }
    public string Tilte { get; set; } = null!;
    public int CategoryId { get; set; }
}
