using HomeService.Domain.Core.Enums.Orders;

namespace HomeService.Domain.Core.Dtos.Orders;

public class GetAllOrderDto
{
    public int Id { get; set; }

    public DateTime CreateAt { get; set; }
    public DateTime TimeToDone { get; set; }
    public string CustomerLname { get; set; } = null!;
    public string SubServiceName { get; set; } = null!;
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.WaitingForExpertOffer;

}
