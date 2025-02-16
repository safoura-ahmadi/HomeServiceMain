using HomeService.Domain.Core.Entities.Orders;
using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class Comment
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public string Text { get; set; }
    public int Score { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    #endregion

    #region NavigationProperties
    public int ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    #endregion
}
