using HomeService.Domain.Core.Entities.ValidationAtrribute;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Categories;

public class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "وارد کردن عنوان کتگوری الزامی است.")]
    [Display(Name = "عنوان")]
    [MaxLength(100, ErrorMessage = "عنوان نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
    [MinLength(2, ErrorMessage = "عنوان باید حداقل 2 کاراکتر باشد.")]
    public string Title { get; set; } = null!;

    public string? ImagePath { get; set; }

   
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نامعتبر است. فقط jpg، jpeg و png مجاز هستند.")]
    public IFormFile? ImageFile { get; set; }
}

// کلاس برای اعتبارسنجی پسوند فایل
