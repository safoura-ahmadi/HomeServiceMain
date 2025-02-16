
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
    [MaxLength(255)]
    public string? Biography { get; set; }
    [MaxLength(100)]
    public string? Fname { get; set; }
    [MaxLength(100)]
    public string? Lname { get; set; }
    [MaxLength(100)]
    public string? Address { get; set; }
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    public bool IsConfirmed { get; set; }
    #endregion


    #region NavigationProperties
    public int CityId { get; set; }
    public City? City { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<Order> CompletedOrders { get; set; } = [];
    public List<ExpertSubService> ExpertSubServices { get; set; } = [];
    public List<Suggestion> Suggestions { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    #endregion


}
