using HomeService.Domain.Core.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.ValidationAtrribute;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredIfNewPasswordProvidedValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var instance = validationContext.ObjectInstance as UpdateUsertDto;
        if (instance == null)
            return ValidationResult.Success;

        // اگر رمز عبور جدید وارد نشده است، مقدار قدیمی را بررسی نکن
        if (string.IsNullOrEmpty(instance.NewPassword))
            return ValidationResult.Success;

        // اگر رمز عبور جدید وجود دارد، ولی مقدار قبلی وارد نشده، خطا بده
        if (string.IsNullOrEmpty(value?.ToString()))
        {
            return new ValidationResult($"وارد کردن رمز عبور پیشین هنگام تغییر رمز عبور الزامی است.");
        }

        return ValidationResult.Success;
    }
}

