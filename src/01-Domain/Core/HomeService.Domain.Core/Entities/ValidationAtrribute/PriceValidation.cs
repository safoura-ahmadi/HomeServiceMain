using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.ValidationAtrribute;

[AttributeUsage(AttributeTargets.Property)]
public class PriceValidation : ValidationAttribute
{
    

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }

        if (value is int price)
        {
            return price > 0;
        }
      
        return true;
    }
}
