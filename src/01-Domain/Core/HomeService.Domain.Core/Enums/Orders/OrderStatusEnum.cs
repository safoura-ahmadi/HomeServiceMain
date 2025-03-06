using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Enums.Orders;
public enum OrderStatusEnum
{
    [Display(Name = "حالت نامشخص")]
    UnDefined = 0,
    [Display(Name = "در انتظار پیشنهاد متخصص")]
    WaitingForExpertOffer = 1,
    [Display(Name = "در انتظار انتخاب متخصص")]
    WaitingForExpertSelection,
    [Display(Name = " پرداخت")]
    WorkCompletedAndPaid,
    [Display(Name = "خاتمه یافته")]
    Completed
}
