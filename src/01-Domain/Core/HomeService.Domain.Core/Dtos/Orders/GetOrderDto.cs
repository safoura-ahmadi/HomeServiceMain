using HomeService.Domain.Core.Enums.Orders;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class GetOrderDto
{
    public int Id { get; set; }

    public DateTime CreateAt { get; set; }
    public DateTime TimeToDone { get; set; }
    public int Price { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerLname { get; set; }


}
