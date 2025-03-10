using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class SuggestionDto
{
    public int Id { get; set; }
    [Display(Name = "توضیحات بیشتر")]
    [MaxLength(255, ErrorMessage = "استفاده کردن از 255 کاراکتر به بالا مجاز نیست")]
    public string? Description { get; set; }
    [Display(Name = "تاریخ اتمام کار")]
  
    [Required(ErrorMessage = "وارد کردن تاریخ الزامی است")]
    public DateTime TimeToDone { get; set; }
    
    [Display(Name = "قیمت پیشنهادی")]
    [PriceValidation(ErrorMessage = "قیمت وارد شده نامعتبر است")]
    [Required(ErrorMessage = "وارد کردن قیمت الزامی است")]
    public int Price { get; set; }
    //navigation
    public int ExpertId { get; set; }
    
    public int OrderId { get; set; }
    public int SubServiceId { get; set; }

}
