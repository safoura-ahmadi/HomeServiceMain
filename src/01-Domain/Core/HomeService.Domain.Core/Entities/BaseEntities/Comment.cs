using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class Comment
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Text { get; set; }
    public int Score { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    //navigation
    public int ExpertId { get; set; }
    public Expert Expert { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
