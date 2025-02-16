using HomeService.Domain.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Domain.Core.Entities.Users;

public class User : IdentityUser<int>
{

    
    public Admin? Admin { get; set; }
    public Expert? Expert { get; set; }
    public Customer? Customer { get; set; }

}
