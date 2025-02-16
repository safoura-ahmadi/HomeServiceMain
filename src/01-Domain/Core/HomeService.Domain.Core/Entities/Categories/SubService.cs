using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class SubService
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Required]
    public int BasePrice { get; set; }
    [MaxLength(255)]
    [Required]
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    #endregion

    #region NavigationProperties
    public int SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }
    public List<ExpertSubService> ExpertSubServices { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    #endregion
}
