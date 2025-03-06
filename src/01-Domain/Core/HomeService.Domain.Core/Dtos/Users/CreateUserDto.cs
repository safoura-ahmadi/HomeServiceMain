using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Users;

public class CreateUserDto
{
    [Compare(nameof(Password), ErrorMessage = "تکرار رمز عبور با رمز عبور مطابقت ندارد.")]
    public string? RePassword { get; set; }

    [MinLength(6, ErrorMessage = "پسورد حداقل باید شش رقم باشد.")]
    [Required(ErrorMessage = "وارد کردن پسورد الزامی است.")]
    public string Password { get; set; } = null!;

    [EmailAddress(ErrorMessage = "لطفاً یک ایمیل معتبر وارد کنید.")]
    [Required(ErrorMessage = "وارد کردن ایمیل الزامی است.")]
    public string Email { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک شهر انتخاب کنید.")]
    public int CityId { get; set; }

    [Required(ErrorMessage = "لطفاً یک نقش انتخاب کنید.")]
    public RoleEnum Role { get; set; }
}

public enum RoleEnum
{
    Customer = 1,
    Expert,
}