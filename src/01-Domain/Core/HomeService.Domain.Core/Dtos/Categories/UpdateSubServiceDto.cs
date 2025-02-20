using HomeService.Domain.Core.Entities.ValidationAtrribute;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateSubServiceDto
{
    public int Id { get; set; }
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر های مجاز برای عنوان ،  100 کاراکتر مباشد")]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
    public string Title { get; set; } = null!;

    public string? ImagePath { get; set; }
    [PriceValidation(ErrorMessage = "قیمت وارد شده نا معتبر است")]
    [Required(ErrorMessage = "وارد کردن قیمت پایه الزامی است")]
    public int BasePrice { get; set; }
    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "وارد کردن توضیحات سرویس الزامی است")]
    [MaxLength(255, ErrorMessage = "متن توضیحات نمیتواند از 255 کاراکتر بیشتر باشد")]
    public string Description { get; set; } = null!;
    public int SubCategoryId { get; set; }

    public IFormFile? ImageFile { get; set; }
}
