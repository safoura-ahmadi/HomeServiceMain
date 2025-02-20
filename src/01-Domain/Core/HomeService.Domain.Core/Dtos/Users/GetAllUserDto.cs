using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HomeService.Domain.Core.Enums.Users;

namespace HomeService.Domain.Core.Dtos.Users;

public class GetAllUserDto
{
    public int Id { get; set; }
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string? Lname { get; set; }
    public string? ImagePath { get; set; }
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Pending;
    public string? RoleName { get; set; }
    [EmailAddress]
    public string? Email { get; set; } 
}
