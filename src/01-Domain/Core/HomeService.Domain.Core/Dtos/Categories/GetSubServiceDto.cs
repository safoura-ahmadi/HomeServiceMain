using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;
public class GetSubServiceDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    public int BasePrice { get; set; }
    public string Description { get; set; } = null!;
    
}
