using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Users;

public class UsertDto
{
    public int Id { get; set; }
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string? Fname { get; set; }
    [MaxLength(100, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string? Lname { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    [MaxLength(255, ErrorMessage = "تعداد کاراکتر استفاده شده مجاز نیست")]
    public string? Biography { get; set; }
    public int CityId { get; set; }
    public bool IsConfirmed { get; set; }
    public string? RoleName { get; set; }
    public string CityName { get; set; } = null!;


}
