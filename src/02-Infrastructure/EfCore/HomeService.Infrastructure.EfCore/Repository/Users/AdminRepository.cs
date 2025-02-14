using HomeService.Domain.Core.Contracts.Repository.Users;
using HomeService.Domain.Core.Entities.Users;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace HomeService.Infrastructure.EfCore.Repository.Users;

public class AdminRepository(ApplicationDbContext dbContext) : IAdminRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> CharcheBalnce(decimal money, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Admins
                .Select(c => c.Balance)
                .FirstAsync(cancellationToken);

            item += money;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;


        }
        catch
        {
            return false;
        }
    }
}
