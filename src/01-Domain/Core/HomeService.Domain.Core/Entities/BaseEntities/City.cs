using HomeService.Domain.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class City
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    //navigation
    public List<Customer> Customers { get; set; } = [];
    public List<Expert> Experts { get; set; } = [];
}
