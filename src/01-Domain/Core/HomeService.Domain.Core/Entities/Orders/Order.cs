using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Entities.ValidationAtrribute;
using HomeService.Domain.Core.Enums.Orders;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Orders;

public class Order
{
    #region Properties
    [Key]
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreateAt { get; set; }
    public DateTime TimeToDone { get; set; }
    [Required]
    [PriceValidation]
    public int Price { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.WaitingForExpertOffer;
    #endregion

    #region NavigationProperties
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int SubServiceId { get; set; }
    public SubService? SubService { get; set; }
    public List<Suggestion> Suggestions { get; set; } = [];
    public List<Image> Images { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    #endregion

}
