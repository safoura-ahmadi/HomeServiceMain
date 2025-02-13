using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Users;

public class GetExpertDto
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Fname { get; set; }
    [MaxLength(100)]
    public string? Lname { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    public int CityId { get; set; }
    public int UserId { get; set; }
    public List<int> ServicesId { get; set; } = [];

}
