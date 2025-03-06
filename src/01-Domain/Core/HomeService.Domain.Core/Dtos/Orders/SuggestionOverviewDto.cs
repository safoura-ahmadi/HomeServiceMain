namespace HomeService.Domain.Core.Dtos.Orders;

public class SuggestionOverviewDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ExpertId { get; set; }
    public DateTime TimeToDone { get; set; }
    public int Price { get; set; }
}
