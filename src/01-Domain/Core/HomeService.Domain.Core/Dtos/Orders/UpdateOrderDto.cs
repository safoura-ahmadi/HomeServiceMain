using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class UpdateOrderDto
{
    public int Id { get; set; }
    public DateTime TimeToDone { get; set; }
    public int Price { get; set; }
    public int ExpertId { get; set; }
}
