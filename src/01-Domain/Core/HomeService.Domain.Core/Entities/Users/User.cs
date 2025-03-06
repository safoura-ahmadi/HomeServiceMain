using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.ValidationAtrribute;
using HomeService.Domain.Core.Enums.Users;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Domain.Core.Entities.Users;

public class User : IdentityUser<int>
{

    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    [MinLength(2, ErrorMessage = "تعداد کارکتر های مجاز برای نام حداقل دو کارکتر است")]
    public string? Fname { get; set; }
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    [MinLength(2, ErrorMessage = "تعداد کارکتر های مجاز برای نام خانوداگی حداقل دو کارکتر است")]
    public string? Lname { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [PriceValidation(ErrorMessage = "مبلغ انتخابی برای شارژ کیف پول مجاز نیست")]
    public decimal Balance { get; set; }
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Pending;
    [MobileValidation(ErrorMessage = "شماره موبایل وارد شده معتبر نمیباشد")]
    public string? Mobile { get; set; }
    //navigation
    public int CityId { get; set; }
    public City? City { get; set; }
    public Admin? Admin { get; set; }
    public Expert? Expert { get; set; }
    public Customer? Customer { get; set; }

}
