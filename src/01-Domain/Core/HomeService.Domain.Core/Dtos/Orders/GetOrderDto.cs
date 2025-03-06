using HomeService.Domain.Core.Entities.BaseEntities;
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
    public string? CustomerLname { get; set; }
    public string SubServiceName { get; set; } = null!;
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.WaitingForExpertOffer;
    public List<Image> Images { get; set; } = [];

}
