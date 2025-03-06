namespace HomeService.Domain.Core.Dtos.Orders;

public class SuggestionDetailsDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ExpertId { get; set; }
    public string? ExpertLname { get; set; }
    public string? Description { get; set; }
    public float Score { get; set; }
}
