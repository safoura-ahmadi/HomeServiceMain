using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;

namespace HomeService.Domain.Core.Dtos.Orders;

public class SuggestionDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DateTime? TimeToDone { get; set; }
    public string? ExperLname { get; set; }
    public int Price { get; set; }
    //navigation
    public int ExpertId { get; set; }
    
    public int OrderId { get; set; }

}
