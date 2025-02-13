using HomeService.Domain.Core.Dtos.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class ExpertEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> IsConfirmedByAdmin(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Customers
                        .AsNoTracking()
                        .Where(e => e.UserId == userId)
                        .Select(e => e.IsConfirmed)
                        .FirstAsync(cancellationToken);
        return result;
    }
    public async Task<GetExpertDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Customers.AsNoTracking()
            .Where(e => e.UserId == userId)
            .Select(e => new GetExpertDto
            {
                Id = e.Id,
                CityId = e.CityId,
                Balance = e.Balance,
                Fname = e.Fname,
                ImagePath = e.ImagePath,
                Lname = e.Lname,
                UserId = userId,
            }
            ).FirstOrDefaultAsync(cancellationToken);
        return item;
    }

}
