using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Orders;

public class Suggestion
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }
    public bool IsAccepted { get; set; } = false;
    public DateTime TimeToDone { get; set; }
    public DateTime CreateAt { get; set; }
    public bool IsActive { get; set; } = true;
    [Required]
    public int Price { get; set; }
    #endregion

    #region NavigationProperties
    public int ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    #endregion
}
