using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.ValidationAtrribute;

[AttributeUsage(AttributeTargets.Property)]
public class MobileValidation : ValidationAttribute
{

    public override bool IsValid(object? value)
    {

        if (value == null) return true;
        else
        {

            if (value.ToString()!.StartsWith("09") && value.ToString()!.Length == 11)
                return true;
            else if (value.ToString()!.StartsWith("+98") && value.ToString()!.Length == 13)
                return true;
            else if (value.ToString()!.StartsWith("098") && value.ToString()!.Length == 13)
                return true;
            return false;

        }
    }

}