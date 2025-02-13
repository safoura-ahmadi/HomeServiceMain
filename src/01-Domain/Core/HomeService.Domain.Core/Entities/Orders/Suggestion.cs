using HomeService.Domain.Core.Entities.Users;

namespace HomeService.Domain.Core.Entities.Orders;

public class Suggestion
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public bool IsAccepted { get; set; }
    public DateTime? TimeToDone { get; set; }
    public int Price { get; set; }
    //navigation
    public int ExpertId { get; set; }
    public Expert Expert { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
