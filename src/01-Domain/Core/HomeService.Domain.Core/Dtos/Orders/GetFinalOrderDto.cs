namespace HomeService.Domain.Core.Dtos.Orders;

public class GetFinalOrderDto
{
    public int Id { get; set; }
    public string? ExpertLname { get; set; } 
    public int Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int? ExpertId { get; set; }
    public int ExpertUserId { get; set; }
    public decimal SiteFee { get; set; }
}
