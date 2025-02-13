using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class CreateOrderDto
{
    public DateTime? TimeToDone { get; set; }
    public int Price { get; set; }
    public string? Description { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    public int CustomerId { get; set; }
    public int SubServiceId { get; set; }
}
