using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class Category
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string ImagePath { get; set; } = null!;
    public bool IsActive { get; set; }
    //navigation
    public List<SubCategory> SubCategories { get; set; } = [];
}