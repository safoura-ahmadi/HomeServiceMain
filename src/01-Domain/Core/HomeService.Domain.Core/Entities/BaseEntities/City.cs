using HomeService.Domain.Core.Entities.Users;

namespace HomeService.Domain.Core.Entities.BaseEntities;

public class City
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    //navigation
    public List<Customer> Customers { get; set; } = [];
    public List<Expert> Experts { get; set; } = [];
}
