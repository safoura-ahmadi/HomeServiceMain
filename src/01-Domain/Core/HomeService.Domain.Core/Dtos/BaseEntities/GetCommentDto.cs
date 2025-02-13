using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.BaseEntities;

public class GetCommentDto
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Text { get; set; }
    public int Score { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    public int ExpertId { get; set; }
    public int CustomerId { get; set; }

}
