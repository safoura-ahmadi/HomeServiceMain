using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateSubCategoryDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان را وارد کنید ")]
    [MaxLength(100,ErrorMessage = "حداکثر کاراکتر مجاز برای عنوان، 100 کاراکتر میباشد")]
    public string Tilte { get; set; } = null!;
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }

}
