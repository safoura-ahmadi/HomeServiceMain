using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Categories;

public class SubService
{
    public int Id { get; set; }
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string Title { get; set; } = null!;
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    public int BasePrice { get; set; }
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
    //navigation
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public List<ExpertSubService> ExpertSubServices { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
}
