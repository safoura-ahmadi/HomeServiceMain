
using HomeService.Domain.Core.Entities.BaseEntities;
using HomeService.Domain.Core.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HomeService.Domain.Core.Entities.Users;

public class Expert
{
    #region Properties
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Lname { get; set; }

    #endregion


    #region NavigationProperties

    public int UserId { get; set; }
    public User? User { get; set; }
    public List<Order> CompletedOrders { get; set; } = [];
    public List<ExpertSubService> ExpertSubServices { get; set; } = [];
    public List<Suggestion> Suggestions { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    #endregion


}
