using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeService.Domain.Core.Entities.Users;

public class Admin
{
    #region Properties
    [Key]
    public int Id { get; set; }
  
 
    #endregion

    #region NavigationProperties
    public int UserId { get; set; }
    public User? User { get; set; }
    #endregion

}
