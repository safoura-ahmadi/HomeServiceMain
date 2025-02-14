using HomeService.Domain.Core.Entities.Categories;

namespace HomeService.Domain.Core.Entities.Users;

public class ExpertSubService
{
 
    public int ExpertId { get; set; }
    public Expert Expert { get; set; }
    public int SubServiceId { get; set; }
    public SubService SubService { get; set; }
}
