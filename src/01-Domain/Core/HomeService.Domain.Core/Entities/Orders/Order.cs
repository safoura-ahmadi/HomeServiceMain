using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Categories;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Domain.Core.Enums.Orders;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.Orders;

public class Order
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime TimeToDone { get; set; }
    public int Price { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    public OrderStatusEnum Status { get; set; }
    //navigation
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int SubServiceId { get; set; }
    public SubService SubService { get; set; }
    public List<Suggestion> Suggestions { get; set; } = [];
    public List<Image> Images { get; set; } = []; 

}
