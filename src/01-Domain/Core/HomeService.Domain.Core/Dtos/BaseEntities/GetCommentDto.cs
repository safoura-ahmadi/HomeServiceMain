using HomeService.Domain.Core.Enums.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.BaseEntities;

public class GetCommentDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public int Score { get; set; } = 0;
    public CommentStatusEnum Status { get; set; }
    public string ExpertLname { get; set; } = null!;
    public string CustomerLname { get; set; } = null!;
    public DateTime CreateAt { get; set; }
}
