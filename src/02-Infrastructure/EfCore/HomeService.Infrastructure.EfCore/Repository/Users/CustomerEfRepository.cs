using HomeService.Infrastructure.EfCore.Common;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class CustomerEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

}
