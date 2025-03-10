using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace HomeService.Domain.Core.Dtos.Orders;

public class CreateOrderDto
{

    public DateTime TimeToDone { get; set; }
    [Display(Name = "قیمت پیشنهادی")]
    [Required(ErrorMessage = "وارد کردن قیمت الزامی است")]
    [PriceValidation(ErrorMessage = "قیمت وارد شده نامعتبر است")]
    public int Price { get; set; }
    [Display(Name = "توضیحات بیشتر")]
    [MaxLength(255, ErrorMessage = "استفاده کردن از 255 کاراکتر به بالا مجاز نیست")]
    public string? Description { get; set; }
    public int CustomerId { get; set; }
    public int SubServiceId { get; set; }
    [AllowedExtensionsValidation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "فرمت فایل نامعتبر است. فقط jpg، jpeg و png مجاز هستند.")]

    public List<IFormFile>? Images { get; set; }
}
