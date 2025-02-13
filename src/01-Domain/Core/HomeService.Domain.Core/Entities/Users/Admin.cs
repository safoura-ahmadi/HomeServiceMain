using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Domain.Core.Entities.Users;

public class Admin
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Fname { get; set; }
    [MaxLength(100)]
    public string? Lname { get; set; }
    [MaxLength(100)]
    public string? Address { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }

    //navigation
    public int UserId { get; set; }
    public User User { get; set; }

}
