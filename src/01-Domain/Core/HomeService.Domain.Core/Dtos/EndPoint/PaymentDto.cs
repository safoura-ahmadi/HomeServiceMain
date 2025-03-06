namespace HomeService.Domain.Core.Dtos.EndPoint;

public class PaymentDto
{
    public int AdminId { get; set; }
    public int ExpertId { get; set; }
    public int CustomerId { get; set; }
    public int Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal SiteFee { get; set; }
}
