namespace HomeService.Domain.Core.Dtos.Categories;

public class GetCategoryForAdminPageDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int SubServiceCount { get; set; }

}
