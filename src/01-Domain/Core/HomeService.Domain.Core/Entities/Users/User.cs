using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Enums.Users;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Domain.Core.Entities.Users;

public class User : IdentityUser<int>
{

    [MaxLength(100)]
    public string? Fname { get; set; }
    [MaxLength(100)]
    public string? Lname { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    public UserStatusEnum Status { get; set; } = UserStatusEnum.Pending;
    [MaxLength(255)]
    public string? Biography { get; set; }
    //navigation
    public int CityId { get; set; }
    public City? City { get; set; }
    public Admin? Admin { get; set; }
    public Expert? Expert { get; set; }
    public Customer? Customer { get; set; }

}
