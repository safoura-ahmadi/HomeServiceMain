using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;
public class GetSubServiceDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ImagePath { get; set; }

    public int BasePrice { get; set; }
    public string Description { get; set; } = null!;
    public string SubCategoryTitle { get; set; } = null!;
    public string CategoryTitle { get; set; } = null!;

}
