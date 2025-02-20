using HomeService.Domain.Core.Entities.ValidationAtrribute;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Orders;

public class GetSuggestionForOrderDto
{
    public int Id { get; set; }
    public DateTime TimeToDone { get; set; }
    public DateTime CreateAt { get; set; }
    public string? ExperLname { get; set; }
    public int Price { get; set; }
    public string ExpertLname { get; set; } = null!;
    public bool IsAccepted { get; set; }
}
