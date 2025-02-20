
using HomeService.Domain.Core.Enums.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.BaseEntities;


public class CreateCommentDto
{
    public int CustomerId { get; set; }
    public int ExpertId { get; set; }
    [Required(ErrorMessage = "امتیاز دهید")]
    [Range(0, 5, ErrorMessage = "امتیاز باید بین 0 تا 10 باشد")]
    [Display(Name = "امتیاز")]
    public int Score { get; set; }
    [Display(Name = "کامنت")]
    [MaxLength(255, ErrorMessage = "متن کامنت نمیتواند از 255 کاراکتر بیتشر باشد")]
    public string? Text { get; set; } 
    public CommentStatusEnum Status { get; set; } = CommentStatusEnum.Pending;
}


