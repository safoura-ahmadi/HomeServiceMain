using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class City
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    [Required]
    public string Title { get; set; } = null!;
    #endregion

    #region NavigationProperties
    public List<Customer> Customers { get; set; } = [];
    public List<Expert> Experts { get; set; } = [];
    #endregion
}
