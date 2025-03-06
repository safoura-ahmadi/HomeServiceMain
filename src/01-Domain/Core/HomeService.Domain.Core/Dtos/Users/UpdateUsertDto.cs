using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using HomeService.Domain.Core.Entities.ValidationAtrribute;

namespace HomeService.Domain.Core.Dtos.Users;

public class UpdateUsertDto
{
    public int Id { get; set; }
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    [MinLength(2, ErrorMessage = "تعداد کارکتر های مجاز برای نام حداقل دو کارکتر است")]
    public string? Fname { get; set; }
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    [MinLength(2, ErrorMessage = "تعداد کارکتر های مجاز برای نام خانوداگی حداقل دو کارکتر است")]
    public string? Lname { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]

    public decimal Balance { get; set; }
    [PriceValidation(ErrorMessage = "مبلغ انتخابی برای شارژ کیف پول مجاز نیست")]
    public int Amount { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
    public int CityId { get; set; }
    [MobileValidation(ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
    public string? Mobile { get; set; }
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نامعتبر است. فقط jpg، jpeg و png مجاز هستند.")]
    public IFormFile? ImageFile { get; set; } 
    public string? Email { get; set; }
    [MinLength(6, ErrorMessage = "پسورد باید حداقل شامل شش کاراکتر باشد")]
    public string? Password { get; set; }
}
