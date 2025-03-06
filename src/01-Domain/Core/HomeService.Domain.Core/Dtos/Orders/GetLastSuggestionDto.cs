using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class GetlastSuggestionDto
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public string ExperLname { get; set; } = "UnKnown";
    public int Price { get; set; }
    public int ExpertId { get; set; }
    public string SubServiceName { get; set; } = null!;
}
