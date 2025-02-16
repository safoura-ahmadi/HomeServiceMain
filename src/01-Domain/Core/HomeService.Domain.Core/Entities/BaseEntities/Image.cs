using HomeService.Domain.Core.Entities.Orders;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class Image
{
    #region Properties
    public int Id { get; set; }
    [MaxLength(500)]
    [Required]
    public string Path { get; set; } = null!;
    #endregion

    #region NavigationProperties
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    #endregion
}

