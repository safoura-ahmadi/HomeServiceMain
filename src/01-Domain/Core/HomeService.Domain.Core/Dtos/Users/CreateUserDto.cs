using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Dtos.Users;

public class CreateUserDto
{
   
    public string Password { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public int CityId { get; set; }
    public RoleEnum Role { get; set; }
}
public enum RoleEnum
{


    Customer = 1,
    Expert,
}