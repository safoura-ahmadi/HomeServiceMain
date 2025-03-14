namespace HomeService.Domain.Core.Dtos.Categories;

public class GetAllSubCategoryWithServiceDto
{
    public GetSubCategoryDto SubCategory { get; set; } = null!;
    public List<GetSubCategoryServiceDto> SubService { get; set; } = [];

}
public class GetSubCategoryServiceDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ImagePath { get; set; }

    public int BasePrice { get; set; }
    public string Description { get; set; } = null!;


}
