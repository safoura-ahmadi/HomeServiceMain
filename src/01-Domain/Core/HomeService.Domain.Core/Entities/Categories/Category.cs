using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class Category
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string ImagePath { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    #endregion


    #region NavigationProperties
    public List<SubCategory> SubCategories { get; set; } = [];
    #endregion
}