using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace HomeService.Domain.Core.Dtos.Orders;

public class CreateOrderDto
{
    [Display(Name = "تاریخ اتمام کار")]
    [Required(ErrorMessage = "وارد کردن تاریخ الزامی است")]
    [DateValidation(ErrorMessage = "تاریخ وارد شده نامعتبر است")]
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
    public List<IFormFile>? Images { get; set; }
}
