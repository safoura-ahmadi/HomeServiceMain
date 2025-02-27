using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateSubCategoryDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان را وارد کنید ")]
    [MaxLength(100, ErrorMessage = "حداکثر کاراکتر مجاز برای عنوان، 100 کاراکتر میباشد")]
    [MinLength(2,ErrorMessage ="حداقل کاراکتر مجاز برای نام 2 کارکتر میباشد")]
    public string Title { get; set; } = null!;
    public int CategoryId { get; set; }

}
